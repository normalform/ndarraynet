// <copyright file="IndexFunction.cs" company="NdArrayNet">
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
    using System.Linq;
    using NdArrayNet;

    internal static class IndexFunction<T>
    {
        private static readonly Lazy<IStaticMethod> StaticMethod = new Lazy<IStaticMethod>(() => new StaticMethod());

        /// <summary>
        /// Gets a sequence of all indices to enumerate all elements within the NdArray.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>Sequence of indicies.</returns>
        public static int[][] AllIndex(NdArray<T> source)
        {
            return Layout.AllIndex(source.Layout);
        }

        /// <summary>
        /// Gets a sequence of all all elements within the NdArray.</summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>Sequence of elements.</returns>
        public static T[] AllElements(NdArray<T> source)
        {
            var allIdx = AllIndex(source);
            return allIdx.Select(idx => source[idx]).ToArray();
        }

        /// <summary>
        /// Finds the index of the maximum value along the specified axis and writes it into the target NdArray.
        /// </summary>
        /// <param name="target">A target NdArray containing the result of this operation.</param>
        /// <param name="axis">The axis to calculate the maximum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public static void FillArgMaxAxis(NdArray<int> target, int axis, NdArray<T> source)
        {
            FillArgMaxAxis(StaticMethod.Value, target, axis, source);
        }

        /// <summary>
        /// Finds the index of the maximum value along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the maximum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<int> ArgMaxAxis(int axis, NdArray<T> source)
        {
            return ArgMaxAxis(StaticMethod.Value, axis, source);
        }

        /// <summary>
        /// Finds the indicies of the maximum value of the NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>The indices of the position of the maximum value.</returns>
        public static int[] ArgMax(NdArray<T> source)
        {
            return Layout.LinearToIndex(source.Layout, ArgMaxAxis(0, NdArray<T>.Flatten(source)).Value);
        }

        /// <summary>
        /// Finds the index of the minimum value along the specified axis and writes it into the target NdArray.
        /// </summary>
        /// <param name="target">A target NdArray containing the result of this operation.</param>
        /// <param name="axis">The axis to calculate the minimum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public static void FillArgMinAxis(NdArray<int> target, int axis, NdArray<T> source)
        {
            FillArgMinAxis(StaticMethod.Value, target, axis, source);
        }

        /// <summary>
        /// Finds the index of the minimum value along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the minimum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<int> ArgMinAxis(int axis, NdArray<T> source)
        {
            return ArgMinAxis(StaticMethod.Value, axis, source);
        }

        /// <summary>
        /// Finds the indicies of the minimum value of the NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>The indices of the position of the minimum value.</returns>
        public static int[] ArgMin(NdArray<T> source)
        {
            return Layout.LinearToIndex(source.Layout, ArgMinAxis(0, NdArray<T>.Flatten(source)).Value);
        }

        /// <summary>
        /// Finds the first occurence of the specfied value along the specified axis and write its index into the target NdArray.
        /// </summary>
        /// <param name="target">A target NdArray containing the result of this operation.</param>
        /// <param name="value">The value to find.</param>
        /// <param name="axis">The axis to find the value along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public static void FillFindAxis(NdArray<int> target, T value, int axis, NdArray<T> source)
        {
            FillFindAxis(StaticMethod.Value, target, value, axis, source);
        }

        /// <summary>
        /// Finds the first occurence of the specfied value along the specified axis and returns its index.
        /// </summary>
        /// <param name="value">The value to find.</param>
        /// <param name="axis">The axis to find the value along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the indices of the first occurence of <paramref name="value"/>.</returns>
        public static NdArray<int> FindAxis(T value, int axis, NdArray<T> source)
        {
            return FindAxis(StaticMethod.Value, value, axis, source);
        }

        /// <summary>
        /// Finds the first occurence of the specfied value along the specified axis and returns its index.
        /// </summary>
        /// <param name="value">The value to find.</param>
        /// <param name="axis">The axis to find the value along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the indices of the first occurence of <paramref name="value"/>.</returns>
        public static int[] TryFind(T value, NdArray<T> source)
        {
            var pos = FindAxis(value, 0, NdArray<T>.Flatten(source)).Value;
            if (pos != SpecialIdx.NotFound)
            {
                return Layout.LinearToIndex(source.Layout, pos);
            }

            return new int[] { };
        }

        /// <summary>
        /// Finds the first occurence of the specfied value and returns its indices.
        /// </summary>
        /// <param name="value">The value to find.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>The indices of the value.</returns>
        public static int[] Find(T value, NdArray<T> source)
        {
            var result = TryFind(value, source);
            if (result.Length != 0)
            {
                return result;
            }

            var errorMessage = string.Format("Value {0} was not found in specifed NdArray.", value);
            throw new InvalidOperationException(errorMessage);
        }

        /// <summary>
        /// Counts the elements being true along the specified axis and writes the result into this NdArray.
        /// </summary>
        /// <param name="target">A target NdArray containing the result of this operation.</param>
        /// <param name="axis">The axis the count along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public static void FillCountTrueAxis(NdArray<int> target, int axis, NdArray<bool> source)
        {
            FillCountTrueAxis(StaticMethod.Value, target, axis, source);
        }

        /// <summary>
        /// Counts the elements being true along the specified axis.
        /// </summary>
        /// <param name="axis">The axis the count along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<int> CountTrueAxis(int axis, NdArray<bool> source)
        {
            return CountTrueAxis(StaticMethod.Value, axis, source);
        }

        /// <summary>
        /// Counts the elements being true returning the result as a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<int> CountTrueNdArray(NdArray<bool> source)
        {
            return CountTrueAxis(0, NdArray<bool>.Flatten(source));
        }

        /// <summary>
        /// Counts the elements being true.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A scalar containing the result of this operation.</returns>
        public static int CountTrue(NdArray<bool> source)
        {
            return CountTrueNdArray(source).Value;
        }

        /// <summary>
        /// Finds the indices of all element that are true.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A matrix that has one row per true entry in <paramref name="source"/>.
        public static NdArray<int> TrueIndices(NdArray<bool> source)
        {
            var count = CountTrue(source);
            var target = new NdArray<int>(new[] { count, source.NumDimensions }, source.Storage.Device);
            target.Backend.TrueIndices(target, source);

            return target;
        }

        internal static void FillArgMaxAxis(IStaticMethod staticMethod, NdArray<int> target, int axis, NdArray<T> source)
        {
            var (preparedSource, _) = staticMethod.PrepareAxisReduceSources(target, axis, source, null, Order.RowMajor);
            target.Backend.ArgMaxLastAxis(target, preparedSource);
        }

        internal static NdArray<int> ArgMaxAxis(IStaticMethod staticMethod, int axis, NdArray<T> source)
        {
            var (preparedTargt, preparedSource) = staticMethod.PrepareAxisReduceTarget<int, T>(axis, source, Order.RowMajor);
            FillArgMaxAxis(preparedTargt, axis, preparedSource);

            return preparedTargt;
        }

        internal static void FillArgMinAxis(IStaticMethod staticMethod, NdArray<int> target, int axis, NdArray<T> source)
        {
            var (preparedSource, _) = staticMethod.PrepareAxisReduceSources(target, axis, source, null, Order.RowMajor);
            target.Backend.ArgMinLastAxis(target, preparedSource);
        }

        internal static NdArray<int> ArgMinAxis(IStaticMethod staticMethod, int axis, NdArray<T> source)
        {
            var (preparedTargt, preparedSource) = staticMethod.PrepareAxisReduceTarget<int, T>(axis, source, Order.RowMajor);
            FillArgMinAxis(preparedTargt, axis, preparedSource);

            return preparedTargt;
        }

        internal static void FillFindAxis(IStaticMethod staticMethod, NdArray<int> target, T value, int axis, NdArray<T> source)
        {
            var (preparedSource, _) = staticMethod.PrepareAxisReduceSources(target, axis, source, null, Order.RowMajor);
            target.Backend.FindLastAxis(value, target, preparedSource);
        }

        internal static NdArray<int> FindAxis(IStaticMethod staticMethod, T value, int axis, NdArray<T> source)
        {
            var (preparedTargt, preparedSource) = staticMethod.PrepareAxisReduceTarget<int, T>(axis, source, Order.RowMajor);
            FillFindAxis(preparedTargt, value, axis, preparedSource);

            return preparedTargt;
        }

        internal static void FillCountTrueAxis(IStaticMethod staticMethod, NdArray<int> target, int axis, NdArray<bool> source)
        {
            var (preparedSource, _) = staticMethod.PrepareAxisReduceSources(target, axis, source, null, Order.RowMajor);
            target.Backend.CountTrueLastAxis(target, preparedSource);
        }

        internal static NdArray<int> CountTrueAxis(IStaticMethod staticMethod, int axis, NdArray<bool> source)
        {
            var (preparedTargt, preparedSource) = staticMethod.PrepareAxisReduceTarget<int, bool>(axis, source, Order.RowMajor);
            FillCountTrueAxis(preparedTargt, axis, preparedSource);

            return preparedTargt;
        }
    }
}