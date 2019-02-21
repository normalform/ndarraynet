// <copyright file="ElementWiseOperator.cs" company="NdArrayNet">
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
    using NdArrayNet;

    internal static class ElementWiseOperator<T>
    {
        public static void FillUnaryPlus(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.UnaryPlus(target, preparedSource);
        }

        public static NdArray<T> UnaryPlus(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillUnaryPlus(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillUnaryMinus(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);

            target.Backend.UnaryMinus(target, preparedSource);
        }

        public static NdArray<T> UnaryMinus(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillUnaryMinus(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillAdd(NdArray<T> target, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Add(target, preparedLhs, preparedRhs);
        }

        public static NdArray<T> Add(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            FillAdd(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        public static void FillSubtract(NdArray<T> target, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Subtract(target, preparedLhs, preparedRhs);
        }

        public static NdArray<T> Subtract(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            FillSubtract(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        internal static void FillMultiply(NdArray<T> target, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Multiply(target, preparedLhs, preparedRhs);
        }

        internal static NdArray<T> Multiply(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            FillMultiply(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        internal static void FillDivide(NdArray<T> target, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Divide(target, preparedLhs, preparedRhs);
        }

        internal static NdArray<T> Divide(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            FillDivide(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        internal static void FillModulo(NdArray<T> target, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Modulo(target, preparedLhs, preparedRhs);
        }

        internal static NdArray<T> Modulo(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            FillModulo(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }
    }
}