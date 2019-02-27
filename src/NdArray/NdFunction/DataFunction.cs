// <copyright file="DataFunction.cs" company="NdArrayNet">
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
        public static void FillConvert<TC>(NdArray<T> target, NdArray<TC> source)
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

        internal static void FillConvert<TC>(IStaticMethod staticMethod, NdArray<T> target, NdArray<TC> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Convert(target, preparedSource);
        }

        internal static NdArray<T> Convert<TC>(IStaticMethod staticMethod, NdArray<TC> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, TC>(source, Order.RowMajor);
            FillConvert(preparedTarget, preparedSource);

            return preparedTarget;
        }
    }
}