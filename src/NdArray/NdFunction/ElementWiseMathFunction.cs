// <copyright file="ElementWiseMathFunction.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction
{
    using NdArrayNet;

    internal class ElementWiseMathFunction<T> : FunctionBase
    {
        public static void FillAbs(NdArray<T> target, NdArray<T> source)
        {
            FillAbs(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise absolute value.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Abs(NdArray<T> source)
        {
            return Abs(StaticMethod.Value, source);
        }

        public static void FillAcos(NdArray<T> target, NdArray<T> source)
        {
            FillAcos(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise arccosine (inverse cosine).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Acos(NdArray<T> source)
        {
            return Acos(StaticMethod.Value, source);
        }

        public static void FillAsin(NdArray<T> target, NdArray<T> source)
        {
            FillAsin(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise arcsine (inverse sine).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Asin(NdArray<T> source)
        {
            return Asin(StaticMethod.Value, source);
        }

        public static void FillAtan(NdArray<T> target, NdArray<T> source)
        {
            FillAtan(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise arctanget (inverse tangent).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Atan(NdArray<T> source)
        {
            return Atan(StaticMethod.Value, source);
        }

        public static void FillCeiling(NdArray<T> target, NdArray<T> source)
        {
            FillCeiling(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise ceiling (round towards positive infinity).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Ceiling(NdArray<T> source)
        {
            return Ceiling(StaticMethod.Value, source);
        }

        public static void FillCos(NdArray<T> target, NdArray<T> source)
        {
            FillCos(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise cosine.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Cos(NdArray<T> source)
        {
            return Cos(StaticMethod.Value, source);
        }

        public static void FillCosh(NdArray<T> target, NdArray<T> source)
        {
            FillCosh(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise hyperbolic cosine.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Cosh(NdArray<T> source)
        {
            return Cosh(StaticMethod.Value, source);
        }

        public static void FillExp(NdArray<T> target, NdArray<T> source)
        {
            FillExp(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise exponential function.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Exp(NdArray<T> source)
        {
            return Exp(StaticMethod.Value, source);
        }

        public static void FillFloor(NdArray<T> target, NdArray<T> source)
        {
            FillFloor(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise floor (round towards negative infinity).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Floor(NdArray<T> source)
        {
            return Floor(StaticMethod.Value, source);
        }

        public static void FillLog(NdArray<T> target, NdArray<T> source)
        {
            FillLog(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise natural logarithm.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Log(NdArray<T> source)
        {
            return Log(StaticMethod.Value, source);
        }

        public static void FillLog10(NdArray<T> target, NdArray<T> source)
        {
            FillLog10(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise common logarithm.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Log10(NdArray<T> source)
        {
            return Log10(StaticMethod.Value, source);
        }

        public static void FillMaximum(NdArray<T> target, NdArray<T> lhs, NdArray<T> rhs)
        {
            FillMaximum(StaticMethod.Value, target, lhs, rhs);
        }

        /// <summary>
        /// Element-wise maximum.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Maximum(NdArray<T> lhs, NdArray<T> rhs)
        {
            return Maximum(StaticMethod.Value, lhs, rhs);
        }

        public static void FillMinimum(NdArray<T> target, NdArray<T> lhs, NdArray<T> rhs)
        {
            FillMinimum(StaticMethod.Value, target, lhs, rhs);
        }

        /// <summary>
        /// Element-wise minimum.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Minimum(NdArray<T> lhs, NdArray<T> rhs)
        {
            return Minimum(StaticMethod.Value, lhs, rhs);
        }

        /// <summary>
        /// Element-wise minimum.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static void FillPow(NdArray<T> target, NdArray<T> lhs, NdArray<T> rhs)
        {
            FillPow(StaticMethod.Value, target, lhs, rhs);
        }

        /// <summary>
        /// Fills this NdArray with the element-wise exponentiation.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Pow(NdArray<T> lhs, NdArray<T> rhs)
        {
            return Pow(StaticMethod.Value, lhs, rhs);
        }

        public static void FillRound(NdArray<T> target, NdArray<T> source)
        {
            FillRound(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise rounding.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Round(NdArray<T> source)
        {
            return Round(StaticMethod.Value, source);
        }

        public static void FillSign(NdArray<T> target, NdArray<T> source)
        {
            FillSign(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise sign.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Sign(NdArray<T> source)
        {
            return Sign(StaticMethod.Value, source);
        }

        public static void FillSin(NdArray<T> target, NdArray<T> source)
        {
            FillSin(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise sine.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Sin(NdArray<T> source)
        {
            return Sin(StaticMethod.Value, source);
        }

        public static void FillSinh(NdArray<T> target, NdArray<T> source)
        {
            FillSinh(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise hyperbolic sine.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Sinh(NdArray<T> source)
        {
            return Sinh(StaticMethod.Value, source);
        }

        public static void FillSqrt(NdArray<T> target, NdArray<T> source)
        {
            FillSqrt(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise square root.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Sqrt(NdArray<T> source)
        {
            return Sqrt(StaticMethod.Value, source);
        }

        public static void FillTan(NdArray<T> target, NdArray<T> source)
        {
            FillTan(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise tangent.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Tan(NdArray<T> source)
        {
            return Tan(StaticMethod.Value, source);
        }

        public static void FillTanh(NdArray<T> target, NdArray<T> source)
        {
            FillTanh(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise hyperbolic tangent.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Tanh(NdArray<T> source)
        {
            return Tanh(StaticMethod.Value, source);
        }

        public static void FillTruncate(NdArray<T> target, NdArray<T> source)
        {
            FillTruncate(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise truncation (rounding towards zero).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Truncate(NdArray<T> source)
        {
            return Truncate(StaticMethod.Value, source);
        }

        internal static NdArray<T> Truncate(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillTruncate(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillAbs(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Abs(target, preparedSource);
        }

        internal static NdArray<T> Abs(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSrouce) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillAbs(preparedTarget, preparedSrouce);

            return preparedTarget;
        }

        internal static void FillAcos(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Acos(target, preparedSource);
        }

        internal static NdArray<T> Acos(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillAcos(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillAsin(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var src2 = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Asin(target, src2);
        }

        internal static NdArray<T> Asin(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillAsin(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillAtan(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Atan(target, preparedSource);
        }

        internal static NdArray<T> Atan(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillAtan(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillCeiling(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var src2 = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Ceiling(target, src2);
        }

        internal static NdArray<T> Ceiling(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillCeiling(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillCos(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Cos(target, preparedSource);
        }

        internal static NdArray<T> Cos(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillCos(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillCosh(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Cosh(target, preparedSource);
        }

        internal static NdArray<T> Cosh(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillCosh(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillExp(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Exp(target, preparedSource);
        }

        internal static NdArray<T> Exp(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillExp(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillFloor(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Floor(target, preparedSource);
        }

        internal static NdArray<T> Floor(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillFloor(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillLog(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Log(target, preparedSource);
        }

        internal static NdArray<T> Log(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillLog(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillLog10(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Log10(target, preparedSource);
        }

        internal static NdArray<T> Log10(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillLog10(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillMaximum(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Maximum(target, preparedLhs, preparedRhs);
        }

        internal static NdArray<T> Maximum(IStaticMethod staticMethod, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<T, T, T>(lhs, rhs, Order.RowMajor);
            FillMaximum(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        internal static void FillMinimum(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Minimum(target, preparedLhs, preparedRhs);
        }

        internal static NdArray<T> Minimum(IStaticMethod staticMethod, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<T, T, T>(lhs, rhs, Order.RowMajor);
            FillMinimum(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        internal static void FillPow(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Pow(target, preparedLhs, preparedRhs);
        }

        internal static NdArray<T> Pow(IStaticMethod staticMethod, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<T, T, T>(lhs, rhs, Order.RowMajor);
            FillPow(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        internal static void FillRound(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Round(target, preparedSource);
        }

        internal static NdArray<T> Round(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillRound(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillSign(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Sign(target, preparedSource);
        }

        internal static NdArray<T> Sign(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillSign(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillSin(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Sin(target, preparedSource);
        }

        internal static NdArray<T> Sin(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillSin(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static NdArray<T> Sinh(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillSinh(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillSinh(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Sinh(target, preparedSource);
        }

        internal static void FillSqrt(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Sqrt(target, preparedSource);
        }

        internal static NdArray<T> Sqrt(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillSqrt(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillTan(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Tan(target, preparedSource);
        }

        internal static NdArray<T> Tan(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillTan(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillTanh(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Tanh(target, preparedSource);
        }

        internal static NdArray<T> Tanh(IStaticMethod staticMethod, NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<T, T>(source, Order.RowMajor);
            FillTanh(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillTruncate(IStaticMethod staticMethod, NdArray<T> target, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Truncate(target, preparedSource);
        }
    }
}