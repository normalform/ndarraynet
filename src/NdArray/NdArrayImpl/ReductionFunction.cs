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
        public static void FillMaxAxis(NdArray<T> target, int axis, NdArray<T> source)
        {
            var (preparedSource, _) = NdArray<T>.PrepareAxisReduceSources(target, axis, source, null);
            target.Backend.MaxLastAxis(target, preparedSource);
        }

        /// <summary>
        /// Calculates the maximum value of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the maximum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> MaxAxis(int axis, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareAxisReduceTarget<T, T>(axis, source);
            FillMaxAxis(preparedTarget, axis, preparedSource);

            return preparedTarget;
        }

        public static void FillMinAxis(NdArray<T> target, int axis, NdArray<T> source)
        {
            var (preparedSource, _) = NdArray<T>.PrepareAxisReduceSources(target, axis, source, null);
            target.Backend.MinLastAxis(target, preparedSource);
        }

        /// <summary>
        /// Calculates the minimum value of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the minimum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> MinAxis(int axis, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareAxisReduceTarget<T, T>(axis, source);
            FillMinAxis(preparedTarget, axis, preparedSource);

            return preparedTarget;
        }

        /// <summary>
        /// Calculates the maximum all elements returning a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<T> MaxNdArray(NdArray<T> source)
        {
            return MaxAxis(0, NdArray<T>.Flattern(source));
        }

        /// <summary>
        /// Calculates the minimum all elements returning a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<T> MinNdArray(NdArray<T> source)
        {
            return MinAxis(0, NdArray<T>.Flattern(source));
        }

        public static void FillSumAxis(NdArray<T> target, int axis, NdArray<T> source)
        {
            var (preparedSource, _) = NdArray<T>.PrepareAxisReduceSources(target, axis, source, null);
            target.Backend.SumLastAxis(target, preparedSource);
        }

        /// <summary>
        /// Sums the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to sum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> SumAxis(int axis, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareAxisReduceTarget<T, T>(axis, source);
            FillSumAxis(preparedTarget, axis, preparedSource);

            return preparedTarget;
        }

        /// <summary>
        /// Sums all elements returning a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<T> SumNdArray(NdArray<T> source)
        {
            return SumAxis(0, NdArray<T>.Flattern(source));
        }

        /// <summary>
        /// Calculates the mean of the elements along the specified axis
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> MeanAxis(int axis, NdArray<T> source)
        {
            var sp = ScalarPrimitivesRegistry.For<T, int>();

            var sum = SumAxis(axis, source);
            var scalar = NdArray<T>.ScalarLike(source, sp.Convert(source.Shape[axis]));

            return sum / scalar;
        }

        /// <summary>
        /// Calculates the mean of the NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>The mean estimate.</returns>
        public static T Mean(NdArray<T> source)
        {
            return MeanAxis(0, NdArray<T>.Flattern(source)).Value;
        }

        public static void FillProductAxis(NdArray<T> target, int axis, NdArray<T> source)
        {
            var (preparedSource, _) = NdArray<T>.PrepareAxisReduceSources(target, axis, source, null);
            target.Backend.ProductLastAxis(target, preparedSource);
        }

        /// <summary>
        /// Calculates the product of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the product along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> ProductAxis(int axis, NdArray<T> source)
        {
            var (result, src) = NdArray<T>.PrepareAxisReduceTarget<T, T>(axis, source);
            var (src1, _) = NdArray<T>.PrepareAxisReduceSources(result, axis, source, null);
            result.Backend.ProductLastAxis(result, src1);

            return result;
        }

        /// <summary>
        /// Calculates the product all elements returning a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<T> ProductNdArray(NdArray<T> source)
        {
            return ProductAxis(0, NdArray<T>.Flattern(source));
        }

        /// <summary>
        /// Calculates the variance of the elements along the specified axis.
        /// The default delta degrees of freedom is 0.
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> VarAxis(int axis, NdArray<T> source)
        {
            var deltaDegreeOfFreedom = Primitives.Zero<T>();

            return VarAxis(axis, source, deltaDegreeOfFreedom);
        }

        /// <summary>
        /// Calculates the variance of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <param name="ddof">The delta degrees of freedom.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> VarAxis(int axis, NdArray<T> source, T deltaDegreeOfFreedom)
        {
            var sp = ScalarPrimitivesRegistry.For<T, int>();
            var spc = ScalarPrimitivesRegistry.For<int, T>();

            var mean = NdArray<T>.InsertAxis(axis, NdArray<T>.MeanAxis(axis, source));
            var v = source - mean;
            var n = sp.Convert(source.Shape[axis] - spc.Convert(deltaDegreeOfFreedom));

            return SumAxis(axis, (v * v) / NdArray<T>.ScalarLike(source, n));
        }

        /// <summary>
        /// Calculates the standard deviation of the elements along the specified axis.
        /// The default delta degrees of freedom is 0.
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <param name="deltaDegreeOfFreedom">The delta degrees of freedom. (default: 0)</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> StdAxis(int axis, NdArray<T> source)
        {
            var deltaDegreeOfFreedom = Primitives.Zero<T>();

            return StdAxis(axis, source, deltaDegreeOfFreedom);
        }

        /// <summary>
        /// Calculates the standard deviation of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <param name="deltaDegreeOfFreedom">The delta degrees of freedom.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> StdAxis(int axis, NdArray<T> source, T deltaDegreeOfFreedom)
        {
            return NdArray<T>.Sqrt(VarAxis(axis, source, deltaDegreeOfFreedom));
        }

        /// <summary>
        /// Calculates the trace along the specified axes.
        /// </summary>
        /// <param name="axis1">The first axis of the diagonal to compute the trace along.</param>
        /// <param name="axis2">The second axis of the diagonal to compute the trace along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> TraceAxis(int axis1, int axis2, NdArray<T> source)
        {
            var tax = axis1 < axis2 ? axis1 : axis1 - 1;

            return SumAxis(tax, NdArrayOperator<T>.DiagAxis(axis1, axis2, source));
        }

        /// <summary>
        /// Calculates the trace of the matrix.
        /// </summary>
        /// <param name="source">A square matrix.</param>
        /// <returns>The trace of the matrix.</returns>
        public static NdArray<T> Trace(NdArray<T> source)
        {
            if (source.NumDimensions < 2)
            {
                var msg = string.Format("Need at least a two dimensional array for trace but got shape {0}.", source.Shape);
                throw new ArgumentException(msg);
            }

            return TraceAxis(source.NumDimensions - 2, source.NumDimensions - 1, source);
        }
    }
}