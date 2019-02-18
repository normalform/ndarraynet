// <copyright file="ElementWiseMathFunction.cs" company="NdArrayNet">
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

    internal static class ElementWiseMathFunction<T>
    {
        /// <summary>
        /// Element-wise absolute value.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillAbs(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Abs(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise arccosine (inverse cosine).
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillAcos(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Acos(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise arcsine (inverse sine).
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillAsin(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Asin(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise arctanget (inverse tangent).
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillAtan(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Atan(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise ceiling (round towards positive infinity).
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillCeiling(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Ceiling(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise cosine.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillCos(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Cos(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise hyperbolic cosine.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillCosh(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Cosh(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise exponential function.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillExp(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Exp(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise floor (round towards negative infinity).
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillFloor(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Floor(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise natural logarithm.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillLog(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Log(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise common logarithm.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillLog10(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Log10(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise maximum.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillMaximum(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (target, src1, src2) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            var (lsh1, rhs1) = NdArray<T>.PrepareElemwiseSources(target, src1, src2);

            target.Backend.Maximum(target, lsh1, rhs1);
            return target;
        }

        /// <summary>
        /// Element-wise minimum.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillMinimum(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (target, src1, src2) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            var (lsh1, rhs1) = NdArray<T>.PrepareElemwiseSources(target, src1, src2);

            target.Backend.Minimum(target, lsh1, rhs1);
            return target;
        }

        /// <summary>
        /// Fills this NdArray with the element-wise exponentiation.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillPow(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (target, src1, src2) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            var (lsh1, rhs1) = NdArray<T>.PrepareElemwiseSources(target, src1, src2);

            target.Backend.Pow(target, lsh1, rhs1);
            return target;
        }

        /// <summary>
        /// Element-wise rounding.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillRound(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Round(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise sign.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillSign(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Sign(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise sine.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillSin(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Sin(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise hyperbolic sine.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillSinh(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Sinh(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise square root.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillSqrt(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Sqrt(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise tangent.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillTan(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Tan(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise hyperbolic tangent.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillTanh(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Tanh(target, src2);
            return target;
        }

        /// <summary>
        /// Element-wise truncation (rounding towards zero).
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> FillTruncate(NdArray<T> input)
        {
            var (target, src) = NdArray<T>.PrepareElemwise<T, T>(input);
            var src2 = NdArray<T>.PrepareElemwiseSources(target, src);

            target.Backend.Truncate(target, src2);
            return target;
        }
    }
}