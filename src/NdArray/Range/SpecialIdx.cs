// <copyright file="SpecialIdx.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    internal static class SpecialIdx
    {
        /// <summary>
        /// For slicing: inserts a new axis of size one.
        /// </summary>
        public static readonly int NewAxis = int.MinValue + 1;

        /// <summary>
        /// For slicing: fills all remaining axes with size one.
        /// Cannot be used together with NewAxis.
        /// </summary>
        public static readonly int Fill = int.MinValue + 2;

        /// <summary>
        /// For reshape: remainder, so that number of elements stays constant.
        /// </summary>
        public static readonly int Remainder = int.MinValue + 3;

        /// <summary>
        /// For search: value was not found.
        /// </summary>
        public static readonly int NotFound = int.MinValue + 4;

        public static readonly int None = int.MinValue + 5;
    }
}