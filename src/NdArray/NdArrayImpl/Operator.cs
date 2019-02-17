// <copyright file="Operator.cs" company="NdArrayNet">
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

    internal static class Operator<T>
    {
        public static NdArray<T> FillUnaryPlus(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.UnaryPlus(target, src2);
            return target;
        }

        public static NdArray<T> FillUnaryMinus(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.UnaryMinus(target, src2);
            return target;
        }

        public static NdArray<bool> FillEqual(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (target, l, r) = NdArray<T>.PrepareElemwise<bool, T, T>(lhs, rhs);
            target.AssertBool();

            var (l2, r2) = NdArray<T>.PrepareElemwiseSources(target, l, r);
            target.Backend.Equal(target, l2, r2);

            return target;
        }

        public static NdArray<bool> FillNotEqual(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (target, l, r) = NdArray<T>.PrepareElemwise<bool, T, T>(lhs, rhs);
            target.AssertBool();

            var (l2, r2) = NdArray<T>.PrepareElemwiseSources(target, l, r);
            target.Backend.NotEqual(target, l2, r2);

            return target;
        }

        public static NdArray<bool> FillLess(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (target, l, r) = NdArray<T>.PrepareElemwise<bool, T, T>(lhs, rhs);
            target.AssertBool();

            var (l2, r2) = NdArray<T>.PrepareElemwiseSources(target, l, r);
            target.Backend.Less(target, l2, r2);

            return target;
        }

        public static NdArray<bool> FillLessOrEqual(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (target, l, r) = NdArray<T>.PrepareElemwise<bool, T, T>(lhs, rhs);
            target.AssertBool();

            var (l2, r2) = NdArray<T>.PrepareElemwiseSources(target, l, r);
            target.Backend.LessOrEqual(target, l2, r2);

            return target;
        }

        public static NdArray<bool> FillGreater(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (target, l, r) = NdArray<T>.PrepareElemwise<bool, T, T>(lhs, rhs);
            target.AssertBool();

            var (l2, r2) = NdArray<T>.PrepareElemwiseSources(target, l, r);
            target.Backend.Greater(target, l2, r2);

            return target;
        }

        public static NdArray<bool> FillGreaterOrEqual(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (target, l, r) = NdArray<T>.PrepareElemwise<bool, T, T>(lhs, rhs);
            target.AssertBool();

            var (l2, r2) = NdArray<T>.PrepareElemwiseSources(target, l, r);
            target.Backend.GreaterOrEqual(target, l2, r2);

            return target;
        }

        public static NdArray<T> FillAdd(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (target, l, r) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            var (l2, r2) = NdArray<T>.PrepareElemwiseSources(target, l, r);
            target.Backend.Add(target, l2, r2);

            return target;
        }

        public static NdArray<T> FillSubtract(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (target, l, r) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            var (l2, r2) = NdArray<T>.PrepareElemwiseSources(target, l, r);
            target.Backend.Subtract(target, l2, r2);

            return target;
        }

        internal static NdArray<T> FillMultiply(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (target, l, r) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            var (l2, r2) = NdArray<T>.PrepareElemwiseSources(target, l, r);
            target.Backend.Multiply(target, l2, r2);

            return target;
        }

        internal static NdArray<T> FillDivide(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (target, l, r) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            var (l2, r2) = NdArray<T>.PrepareElemwiseSources(target, l, r);
            target.Backend.Divide(target, l2, r2);

            return target;
        }

        internal static NdArray<T> FillModulo(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (target, l, r) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            var (l2, r2) = NdArray<T>.PrepareElemwiseSources(target, l, r);
            target.Backend.Modulo(target, l2, r2);

            return target;
        }
    }
}