// <copyright file="ReductionFunction.cs" company="NdArrayNet">
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
    using NdArrayNet;

    internal static class ReductionFunction<T>
    {
        /// <summary>
        /// Calculates the maximum value of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the maximum along.</param>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillMaxAxis(int axis, NdArray<T> input)
        {
            var (result, src) = NdArray<T>.PrepareAxisReduceTarget<T, T>(axis, input);
            var (src1, _) = NdArray<T>.PrepareAxisReduceSources(result, axis, input, null);
            result.Backend.MaxLastAxis(result, src1);

            return result;
        }

        /// <summary>
        /// Calculates the minimum value of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the minimum along.</param>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillMinAxis(int axis, NdArray<T> input)
        {
            var (result, src) = NdArray<T>.PrepareAxisReduceTarget<T, T>(axis, input);
            var (src1, _) = NdArray<T>.PrepareAxisReduceSources(result, axis, input, null);
            result.Backend.MinLastAxis(result, src1);

            return result;
        }

        /// <summary>
        /// Calculates the maximum all elements returning a NdArray.
        /// </summary>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<T> MaxNdArray(NdArray<T> input)
        {
            return FillMaxAxis(0, NdArray<T>.Flattern(input));
        }

        /// <summary>
        /// Calculates the minimum all elements returning a NdArray.
        /// </summary>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<T> MinNdArray(NdArray<T> input)
        {
            return FillMinAxis(0, NdArray<T>.Flattern(input));
        }

        /// <summary>
        /// Sums the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to sum along.</param>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillSumAxis(int axis, NdArray<T> input)
        {
            var (result, src) = NdArray<T>.PrepareAxisReduceTarget<T, T>(axis, input);
            var (src1, _) = NdArray<T>.PrepareAxisReduceSources(result, axis, input, null);
            result.Backend.SumLastAxis(result, src1);

            return result;
        }

        /// <summary>
        /// Sums all elements returning a NdArray.
        /// </summary>
        /// <param name="sinputrc">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<T> SumNdArray(NdArray<T> input)
        {
            return FillSumAxis(0, NdArray<T>.Flattern(input));
        }

        /// <summary>
        /// Calculates the mean of the elements along the specified axis
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> MeanAxis(int axis, NdArray<T> input)
        {
            var sp = ScalarPrimitives.For<T, int>();

            var sum = FillSumAxis(axis, input);
            var scalar = NdArray<T>.ScalarLike(input, sp.Convert(input.Shape[axis]));

            return sum / scalar;
        }

        /// <summary>
        /// Calculates the mean of the NdArray.
        /// </summary>
        /// <param name="input">The tensor containing the source values.</param>
        /// <returns>The mean estimate.</returns>
        public static T Mean(NdArray<T> input)
        {
            return MeanAxis(0, NdArray<T>.Flattern(input)).Value;
        }

        /// <summary>
        /// Calculates the product of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the product along.</param>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillProductAxis(int axis, NdArray<T> input)
        {
            var (result, src) = NdArray<T>.PrepareAxisReduceTarget<T, T>(axis, input);
            var (src1, _) = NdArray<T>.PrepareAxisReduceSources(result, axis, input, null);
            result.Backend.ProductLastAxis(result, src1);

            return result;
        }

        /// <summary>
        /// Calculates the product all elements returning a NdArray.
        /// </summary>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<T> ProductNdArray(NdArray<T> input)
        {
            return FillProductAxis(0, NdArray<T>.Flattern(input));
        }

        /// <summary>
        /// Calculates the variance of the elements along the specified axis.
        /// The default delta degrees of freedom is 0.
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> VarAxis(int axis, NdArray<T> input)
        {
            var deltaDegreeOfFreedom = Primitives.Zero<T>();

            return VarAxis(axis, input, deltaDegreeOfFreedom);
        }

        /// <summary>
        /// Calculates the variance of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <param name="ddof">The delta degrees of freedom.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> VarAxis(int axis, NdArray<T> input, T deltaDegreeOfFreedom)
        {
            var sp = ScalarPrimitives.For<T, int>();
            var spc = ScalarPrimitives.For<int, T>();

            var mean = NdArray<T>.InsertAxis(axis, NdArray<T>.MeanAxis(axis, input));
            var v = input - mean;
            var n = sp.Convert(input.Shape[axis] - spc.Convert(deltaDegreeOfFreedom));

            return FillSumAxis(axis, (v * v) / NdArray<T>.ScalarLike(input, n));
        }

        /// <summary>
        /// Calculates the standard deviation of the elements along the specified axis.
        /// The default delta degrees of freedom is 0.
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <param name="deltaDegreeOfFreedom">The delta degrees of freedom. (default: 0)</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> StdAxis(int axis, NdArray<T> input)
        {
            var deltaDegreeOfFreedom = Primitives.Zero<T>();

            return StdAxis(axis, input, deltaDegreeOfFreedom);
        }

        /// <summary>
        /// Calculates the standard deviation of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <param name="deltaDegreeOfFreedom">The delta degrees of freedom.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> StdAxis(int axis, NdArray<T> input, T deltaDegreeOfFreedom)
        {
            return NdArray<T>.Sqrt(VarAxis(axis, input, deltaDegreeOfFreedom));
        }

        /// <summary>
        /// Calculates the trace along the specified axes.
        /// </summary>
        /// <param name="axis1">The first axis of the diagonal to compute the trace along.</param>
        /// <param name="axis2">The second axis of the diagonal to compute the trace along.</param>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> TraceAxis(int axis1, int axis2, NdArray<T> input)
        {
            var tax = axis1 < axis2 ? axis1 : axis1 - 1;

            return FillSumAxis(tax, NdArrayOperator<T>.DiagAxis(input, axis1, axis2));
        }

        /// <summary>
        /// Calculates the trace of the matrix.
        /// </summary>
        /// <param name="input">A square matrix.</param>
        /// <returns>The trace of the matrix.</returns>
        public static NdArray<T> Trace(NdArray<T> input)
        {
            if (input.NumDimensions < 2)
            {
                var msg = string.Format("Need at least a two dimensional array for trace but got shape {0}.", input.Shape);
                throw new ArgumentException(msg);
            }

            return TraceAxis(input.NumDimensions - 2, input.NumDimensions - 1, input);
        }
    }
}