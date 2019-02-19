// <copyright file="NdArrayOperator.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// 1. Redistributions of source code must retain the above copyright notice, this
//    list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// The views and conclusions contained in the software and documentation are those
// of the authors and should not be interpreted as representing official policies,
// either expressed or implied, of the NdArrayNet project.
// </copyright>

namespace NdArray.NdArrayImpl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NdArrayNet;

    internal static class NdArrayOperator<T>
    {
        /// <summary>
        /// Returns a view of the diagonal along the given axes.
        /// </summary>
        /// <param name="ax1">The first dimension of the diagonal.</param>
        /// <param name="ax2">The seconds dimension of the diagonal.</param>
        /// <param name="input">The NdArray to operate on.</param>
        /// <returns>A NdArray where dimension <paramref name="ax1"/> is the diagonal and dimension
        public static NdArray<T> DiagAxis(int ax1, int ax2, NdArray<T> input)
        {
            return input.Relayout(Layout.DiagAxis(ax1, ax2, input.Layout));
        }

        /// <summary>
        /// Returns a view of the diagonal of the NdArray.
        /// </summary>
        /// <param name="input">A square NdArray.</param>
        /// <returns>The diagonal NdArray.</returns>
        public static NdArray<T> Diag(NdArray<T> input)
        {
            if (input.NumDimensions < 2)
            {
                var message = string.Format("Need at least a two dimensional array for diagonal but got shape {0}.", input.Shape);
                throw new ArgumentException(message, "array");
            }

            return DiagAxis(input.NumDimensions - 2, input.NumDimensions - 1, input);
        }

        /// <summary>
        /// Concatenates NdArrays along an axis.
        /// </summary>
        /// <param name="axis">The concatenation axis.</param>
        /// <param name="inputs">Sequence of NdArrays to concatenate.</param>
        /// <returns>The concatenated NdArray.</returns>
        public static NdArray<T> Concat(int axis, NdArray<T>[] inputs)
        {
            if (inputs.Length == 0)
            {
                throw new ArgumentException("Cannot concatenate empty sequence of NdArray.", "inputs");
            }

            var shape = inputs[0].Shape.Select(s => s).ToArray();
            if (!(axis >= 0 && axis < shape.Length))
            {
                var message = string.Format("Concatenation axis {0} is out of range for shape {1}.", axis, shape);
                throw new ArgumentOutOfRangeException("axis", message);
            }

            var arrayIndex = 0;
            foreach (var input in inputs)
            {
                if (!Enumerable.SequenceEqual(List.Without(axis, input.Shape), List.Without(axis, shape)))
                {
                    var message = string.Format("Concatentation element with index {0} with shape{1} must be equal to shape {2} of the first element, except in the concatenation axis {3}", arrayIndex, input.Shape, shape, axis);
                    throw new ArgumentException(message, "inputs");
                }

                arrayIndex++;
            }

            var totalSize = inputs.Sum(i => i.Shape[axis]);
            var concatShape = List.Set(axis, totalSize, shape);

            var result = new NdArray<T>(concatShape, inputs[0].Storage.Device);
            var position = 0;
            foreach (var input in inputs)
            {
                var arrayLength = input.Shape[axis];
                if (arrayLength > 0)
                {
                    var range = Enumerable.Range(0, shape.Length).Select(idx =>
                    {
                        if (idx == axis)
                        {
                            return RangeFactory.Range(position, position + arrayLength - 1);
                        }

                        return RangeFactory.All;
                    });
                    result[range.ToArray()] = input;
                    position += arrayLength;
                }
            }

            return result;
        }

        /// <summary>Returns a copy of the NdArray.</summary>
        /// <param name="input">The NdArray to copy.</param>
        /// <param name="order">The memory layout of the copy. (default: row-major)</param>
        /// <returns>A copy of the NdArray.</returns>
        public static NdArray<T> Copy(NdArray<T> input, Order order = Order.RowMajor)
        {
            return input.Copy(order);
        }

        /// <summary
        /// >Creates a NdArray with the specified diagonal along the given axes.
        /// </summary>
        /// <param name="axis1">The first dimension of the diagonal.</param>
        /// <param name="axis2">The seconds dimension of the diagonal.</param>
        /// <param name="input">The values for the diagonal.</param>
        /// <returns>A NdArray having the values <paramref name="a"/> on the diagonal specified by the axes
        public static NdArray<T> DiagMatAxis(int axis1, int axis2, NdArray<T> input)
        {
            if (axis1 == axis2)
            {
                throw new ArgumentException("axes to use for diagonal must be different", "axis1");
            }

            var ax1 = axis1 < axis2 ? axis1 : axis2;
            var ax2 = axis1 < axis2 ? axis2 : axis1;

            NdArray<T>.CheckAxis(ax1, input);
            if (!(ax2 >= 0 && ax2 <= input.NumDimensions))
            {
                var message = string.Format("Cannot insert axis at position {0} into array of shape {1}.", ax2, input.Shape);
                throw new ArgumentException(message, "axis2");
            }

            var shape = List.Insert(ax2, input.Shape[ax1], input.Shape);
            var result = NdArray<T>.Zeros(input.Storage.Device, shape);
            var diag = DiagAxis(ax1, ax2, result);
            FillFrom(diag, input);

            return result;
        }

        /// <summary>
        /// Creates a matrix with the specified diagonal.
        /// </summary>
        /// <param name="input">The vector containing the values for the diagonal.</param>
        /// <returns>A matrix having the values <paramref name="input"/> on its diagonal.</returns>
        public static NdArray<T> DiagMat(NdArray<T> input)
        {
            if (input.NumDimensions < 1)
            {
                throw new ArgumentException("need at leat a one-dimensional array to create a diagonal matrix", "input");
            }

            return DiagMatAxis(input.NumDimensions - 1, input.NumDimensions, input);
        }

        /// <summary>
        /// Calculates the difference between adjoining elements along the specified axes.
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>The differences NdArray. It has one element less in dimension <paramref name="axis"/> as the input NdArray.</returns>
        public static NdArray<T> DiffAxis(int axis, NdArray<T> input)
        {
            NdArray<T>.CheckAxis(axis, input);
            var shiftRanges = new List<IRange>();
            var cutRanges = new List<IRange>();

            for (var index = 0; index < input.NumDimensions; index++)
            {
                if (index == axis)
                {
                    shiftRanges.Add(RangeFactory.Range(1, SpecialIdx.None));
                    cutRanges.Add(RangeFactory.Range(SpecialIdx.None, input.Shape[index] - 2));
                }
                else
                {
                    shiftRanges.Add(RangeFactory.All);
                    cutRanges.Add(RangeFactory.All);
                }
            }

            var shiftArray = input[shiftRanges.ToArray()];
            var cutArray = input[cutRanges.ToArray()];

            return shiftArray - cutArray;
        }

        /// <summary>
        /// Calculates the difference between adjoining elements of the vector.
        /// </summary>
        /// <param name="input">The vector containing the source values.</param>
        /// <returns>The differences vector. It has one element less than the input NdArray.</returns>
        public static NdArray<T> Diff(NdArray<T> input)
        {
            if (input.NumDimensions < 1)
            {
                throw new ArgumentException("Need at least a vector to calculate diff.", "input");
            }

            return DiffAxis(input.NumDimensions - 1, input);
        }

        /// <summary>
        /// Repeats the NdArray along an axis.
        /// </summary>
        /// <param name="axis">The axis to repeat along.</param>
        /// <param name="repeats">The number of repetitions.</param>
        /// <param name="input">The NdArray to repeat.</param>
        /// <returns>The repeated NdArray.</returns>
        public static NdArray<T> Replicate(int axis, int repeats, NdArray<T> input)
        {
            NdArray<T>.CheckAxis(axis, input);
            if (repeats < 0)
            {
                throw new ArgumentException("Number of repetitions cannot be negative.", "repeats");
            }

            // 1. insert axis of size one left to repetition axis
            // 2. broadcast along the new axis to number of repetitions
            // 3. reshape to result shape
            var step1 = input.Reshape(List.Insert(axis, 1, input.Shape));
            var step2 = NdArray<T>.BraodcastDim(axis, repeats, step1);
            var step3 = step2.Reshape(List.Set(axis, repeats * input.Shape[axis], input.Shape));

            return step3;
        }

        /// <summary>
        /// Transpose of a matrix.
        /// </summary>
        /// <param name="input">The NdArray to operate on.</param>
        /// <returns>The result of this operation.</returns>
        public static NdArray<T> Transpos(NdArray<T> input)
        {
            return input.Relayout(Layout.Transpos(input.Layout));
        }

        internal static NdArray<T> FillFrom(NdArray<T> target, NdArray<T> input)
        {
            var src1 = NdArray<T>.PrepareElemwiseSources(target, input);
            target.CopyFrom(src1);

            return target;
        }
    }
}