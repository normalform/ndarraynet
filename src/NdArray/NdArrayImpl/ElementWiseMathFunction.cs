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
        public static void FillAbs(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Abs(target, preparedSource);
        }

        /// <summary>
        /// Element-wise absolute value.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Abs(NdArray<T> source)
        {
            var (preparedTarget, preparedSrouce) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillAbs(preparedTarget, preparedSrouce);

            return preparedTarget;
        }

        public static void FillAcos(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Acos(target, preparedSource);
        }

        /// <summary>
        /// Element-wise arccosine (inverse cosine).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Acos(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillAcos(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillAsin(NdArray<T> target, NdArray<T> source)
        {
            var src2 = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Asin(target, src2);
        }

        /// <summary>
        /// Element-wise arcsine (inverse sine).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Asin(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillAsin(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillAtan(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Atan(target, preparedSource);
        }

        /// <summary>
        /// Element-wise arctanget (inverse tangent).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Atan(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillAtan(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillCeiling(NdArray<T> target, NdArray<T> source)
        {
            var src2 = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Ceiling(target, src2);
        }

        /// <summary>
        /// Element-wise ceiling (round towards positive infinity).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Ceiling(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillCeiling(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillCos(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Cos(target, preparedSource);
        }

        /// <summary>
        /// Element-wise cosine.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Cos(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillCos(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillCosh(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Cosh(target, preparedSource);
        }

        /// <summary>
        /// Element-wise hyperbolic cosine.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Cosh(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillCosh(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillExp(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Exp(target, preparedSource);
        }

        /// <summary>
        /// Element-wise exponential function.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Exp(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillExp(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillFloor(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Floor(target, preparedSource);
        }

        /// <summary>
        /// Element-wise floor (round towards negative infinity).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Floor(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillFloor(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillLog(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Log(target, preparedSource);
        }

        /// <summary>
        /// Element-wise natural logarithm.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Log(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillLog(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillLog10(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Log10(target, preparedSource);
        }

        /// <summary>
        /// Element-wise common logarithm.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Log10(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillLog10(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillMaximum(NdArray<T> target, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Maximum(target, preparedLhs, preparedRhs);
        }

        /// <summary>
        /// Element-wise maximum.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Maximum(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            FillMaximum(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        public static void FillMinimum(NdArray<T> target, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Minimum(target, preparedLhs, preparedRhs);
        }

        /// <summary>
        /// Element-wise minimum.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Minimum(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            FillMinimum(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        public static void FillPow(NdArray<T> target, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Pow(target, preparedLhs, preparedRhs);
        }

        /// <summary>
        /// Fills this NdArray with the element-wise exponentiation.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Pow(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwise<T, T, T>(lhs, rhs);
            FillPow(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        public static void FillRound(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Round(target, preparedSource);
        }

        /// <summary>
        /// Element-wise rounding.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Round(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillRound(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillSign(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Sign(target, preparedSource);
        }

        /// <summary>
        /// Element-wise sign.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Sign(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillSign(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillSin(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Sin(target, preparedSource);
        }

        /// <summary>
        /// Element-wise sine.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Sin(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillSin(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillSinh(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Sinh(target, preparedSource);
        }

        /// <summary>
        /// Element-wise hyperbolic sine.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Sinh(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillSinh(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillSqrt(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Sqrt(target, preparedSource);
        }

        /// <summary>
        /// Element-wise square root.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Sqrt(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillSqrt(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillTan(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Tan(target, preparedSource);
        }

        /// <summary>
        /// Element-wise tangent.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Tan(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillTan(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillTanh(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Tanh(target, preparedSource);
        }

        /// <summary>
        /// Element-wise hyperbolic tangent.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Tanh(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillTanh(preparedTarget, preparedSource);

            return preparedTarget;
        }

        public static void FillTruncate(NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = NdArray<T>.PrepareElemwiseSources(target, source);
            target.Backend.Truncate(target, preparedSource);
        }

        /// <summary>
        /// Element-wise truncation (rounding towards zero).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Truncate(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<T, T>(source);
            FillTruncate(preparedTarget, preparedSource);

            return preparedTarget;
        }
    }
}