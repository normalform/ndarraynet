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
    using NdArrayNet;

    internal static class NdArrayOperator<T>
    {
        /// <summary>
        /// Returns a view of the diagonal along the given axes.
        /// </summary>
        /// <param name="ax1">The first dimension of the diagonal.</param>
        /// <param name="ax2">The seconds dimension of the diagonal.</param>
        /// <param name="array">The NdArray to operate on.</param>
        /// <returns>A NdArray where dimension <paramref name="ax1"/> is the diagonal and dimension
        public static NdArray<T> DiagAxis(NdArray<T> array, int ax1, int ax2)
        {
            return array.Relayout(Layout.DiagAxis(array.Layout, ax1, ax2));
        }

        /// <summary>
        /// Returns a view of the diagonal of the NdArray.
        /// </summary>
        /// <param name="array">A square NdArray.</param>
        /// <returns>The diagonal NdArray.</returns>
        public static NdArray<T> Diag(NdArray<T> array)
        {
            if (array.NumDimensions < 2)
            {
                var message = string.Format("Need at least a two dimensional array for diagonal but got shape {0}.", array.Shape);
                throw new ArgumentException(message, "array");
            }

            return DiagAxis(array, array.NumDimensions - 2, array.NumDimensions - 1);
        }
    }
}