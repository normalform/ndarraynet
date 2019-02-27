// <copyright file="ReductionFunction.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction
{
    using System;
    using NdArrayNet;

    internal class ReductionFunction<T> : FunctionBase
    {
        public static void FillMaxAxis(NdArray<T> target, int axis, NdArray<T> source)
        {
            FillMaxAxis(StaticMethod.Value, target, axis, source);
        }

        /// <summary>
        /// Calculates the maximum value of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the maximum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> MaxAxis(int axis, NdArray<T> source)
        {
            return MaxAxis(StaticMethod.Value, axis, source);
        }

        public static void FillMinAxis(NdArray<T> target, int axis, NdArray<T> source)
        {
            FillMinAxis(StaticMethod.Value, target, axis, source);
        }

        /// <summary>
        /// Calculates the minimum value of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the minimum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> MinAxis(int axis, NdArray<T> source)
        {
            return MinAxis(StaticMethod.Value, axis, source);
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
            FillSumAxis(StaticMethod.Value, target, axis, source);
        }

        /// <summary>
        /// Sums the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to sum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> SumAxis(int axis, NdArray<T> source)
        {
            return SumAxis(StaticMethod.Value, axis, source);
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
            FillProductAxis(StaticMethod.Value, target, axis, source);
        }

        /// <summary>
        /// Calculates the product of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the product along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> ProductAxis(int axis, NdArray<T> source)
        {
            return ProductAxis(StaticMethod.Value, axis, source);
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
                var errorMessage = string.Format("Need at least a two dimensional array for trace but got {0} dimensional and it's shape is {1}.", source.NumDimensions, ErrorMessage.ShapeToString(source.Shape));
                throw new ArgumentException(errorMessage, "source");
            }

            return TraceAxis(source.NumDimensions - 2, source.NumDimensions - 1, source);
        }

        internal static void FillMaxAxis(IStaticMethod staticMethod, NdArray<T> target, int axis, NdArray<T> source)
        {
            var (preparedSource, _) = staticMethod.PrepareAxisReduceSources(target, axis, source, null, Order.RowMajor);
            target.Backend.MaxLastAxis(target, preparedSource);
        }

        internal static NdArray<T> MaxAxis(IStaticMethod staticMethod, int axis, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareAxisReduceTarget<T, T>(axis, source, Order.RowMajor);
            FillMaxAxis(preparedTarget, axis, preparedSource);

            return preparedTarget;
        }

        internal static void FillMinAxis(IStaticMethod staticMethod, NdArray<T> target, int axis, NdArray<T> source)
        {
            var (preparedSource, _) = staticMethod.PrepareAxisReduceSources(target, axis, source, null, Order.RowMajor);
            target.Backend.MinLastAxis(target, preparedSource);
        }

        internal static NdArray<T> MinAxis(IStaticMethod staticMethod, int axis, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareAxisReduceTarget<T, T>(axis, source, Order.RowMajor);
            FillMinAxis(preparedTarget, axis, preparedSource);

            return preparedTarget;
        }

        internal static void FillSumAxis(IStaticMethod staticMethod, NdArray<T> target, int axis, NdArray<T> source)
        {
            var (preparedSource, _) = staticMethod.PrepareAxisReduceSources(target, axis, source, null, Order.RowMajor);
            target.Backend.SumLastAxis(target, preparedSource);
        }

        internal static NdArray<T> SumAxis(IStaticMethod staticMethod, int axis, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareAxisReduceTarget<T, T>(axis, source, Order.RowMajor);
            FillSumAxis(preparedTarget, axis, preparedSource);

            return preparedTarget;
        }

        internal static void FillProductAxis(IStaticMethod staticMethod, NdArray<T> target, int axis, NdArray<T> source)
        {
            var (preparedSource, _) = staticMethod.PrepareAxisReduceSources(target, axis, source, null, Order.RowMajor);
            target.Backend.ProductLastAxis(target, preparedSource);
        }

        internal static NdArray<T> ProductAxis(IStaticMethod staticMethod, int axis, NdArray<T> source)
        {
            var (result, src) = staticMethod.PrepareAxisReduceTarget<T, T>(axis, source, Order.RowMajor);
            var (src1, _) = staticMethod.PrepareAxisReduceSources(result, axis, source, null, Order.RowMajor);
            result.Backend.ProductLastAxis(result, src1);

            return result;
        }
    }
}