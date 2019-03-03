// <copyright file="DataFunction.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction
{
    using NdArrayNet;

    internal class DataFunction<T> : FunctionBase
    {
        /// <summary>
        /// Copies elements from a NdArray of different data type into the target NdArray and converts their type.
        /// </summary>
        /// <param name="target">The target NdArray.</param>
        /// <typeparam name="TC">The data type to convert from.</typeparam>
        /// <param name="source">The NdArray to copy from.</param>
        public static void FillConvert<TC>(IFrontend<T> target, IFrontend<TC> source)
        {
            FillConvert(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Convert the elements of a NdArray to the specifed type.
        /// </summary>
        /// <typeparam name="TC">The data type to convert from.</typeparam>
        /// <param name="source">The NdArray to convert.</param>
        /// <returns>A NdArray of the new data type.</returns>
        public static NdArray<T> Convert<TC>(NdArray<TC> source)
        {
            return Convert(StaticMethod.Value, source);
        }

        internal static void FillConvert<TC>(IStaticMethod staticMethod, IFrontend<T> target, IFrontend<TC> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Convert(target, preparedSource);
        }

        internal static NdArray<T> Convert<TC>(IStaticMethod staticMethod, IFrontend<TC> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, TC>(source, Order.RowMajor);
            FillConvert(preparedTarget, preparedSource);

            return preparedTarget as NdArray<T>;
        }
    }
}