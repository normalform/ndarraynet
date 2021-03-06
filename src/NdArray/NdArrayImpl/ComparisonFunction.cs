﻿// <copyright file="ComparisonFunction.cs" company="NdArrayNet">
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
    using System.Linq;
    using NdArrayNet;

    internal static class ComparisonFunction<T>
    {
        public static void FillEqual<TP>(NdArray<bool> target, NdArray<TP> lhs, NdArray<TP> rhs)
        {
            var (preparedLhs, preparedRhs) = NdArray<TP>.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Equal(target, preparedLhs, preparedRhs);
        }

        public static NdArray<bool> Equal(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preapredLhs, preparedRhs) = NdArray<T>.PrepareElemwise<bool, T, T>(lhs, rhs);
            preparedTarget.AssertBool();

            FillEqual(preparedTarget, preapredLhs, preparedRhs);

            return preparedTarget;
        }

        public static void FillNotEqual<TP>(NdArray<bool> target, NdArray<TP> lhs, NdArray<TP> rhs)
        {
            var (preparedLhs, preparedRhs) = NdArray<TP>.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.NotEqual(target, preparedLhs, preparedRhs);
        }

        public static NdArray<bool> NotEqual(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwise<bool, T, T>(lhs, rhs);
            preparedTarget.AssertBool();

            FillNotEqual(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        public static void FillLess<TP>(NdArray<bool> target, NdArray<TP> lhs, NdArray<TP> rhs)
        {
            var (preparedLhs, preparedRhs) = NdArray<TP>.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Less(target, preparedLhs, preparedRhs);
        }

        public static NdArray<bool> Less(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwise<bool, T, T>(lhs, rhs);
            preparedTarget.AssertBool();

            FillLess(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        public static void FillLessOrEqual<TP>(NdArray<bool> target, NdArray<TP> lhs, NdArray<TP> rhs)
        {
            var (preparedLhs, preparedRhs) = NdArray<TP>.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.LessOrEqual(target, preparedLhs, preparedRhs);
        }

        public static NdArray<bool> LessOrEqual(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwise<bool, T, T>(lhs, rhs);
            preparedTarget.AssertBool();

            FillLessOrEqual(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        public static void FillGreater<TP>(NdArray<bool> target, NdArray<TP> lhs, NdArray<TP> rhs)
        {
            var (preparedLhs, preparedRhs) = NdArray<TP>.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Greater(target, preparedLhs, preparedRhs);
        }

        public static NdArray<bool> Greater(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwise<bool, T, T>(lhs, rhs);
            preparedTarget.AssertBool();

            FillGreater(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        public static void FillGreaterOrEqual<TP>(NdArray<bool> target, NdArray<TP> lhs, NdArray<TP> rhs)
        {
            var (preparedLhs, preparedRhs) = NdArray<TP>.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.GreaterOrEqual(target, preparedLhs, preparedRhs);
        }

        public static NdArray<bool> GreaterOrEqual(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = NdArray<T>.PrepareElemwise<bool, T, T>(lhs, rhs);
            preparedTarget.AssertBool();

            FillGreaterOrEqual(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        /// <summary>
        /// Element-wise check if two NdArrays have same (within machine precision) values.
        /// The default absolute tolerance is 1e-8.
        /// The default relative tolerance is 1e-5.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> IsClose(NdArray<T> lhs, NdArray<T> rhs)
        {
            return IsCloseWithoutTolerence(lhs, rhs);
        }

        /// <summary>
        /// Element-wise check if two NdArrays have same (within machine precision) values.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <param name="absoluteTolerence">The absolute tolerance. (default 1e-8)</param>
        /// <param name="relativeTolerence">The relative tolerance. (default 1e-5)</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> IsClose(NdArray<double> lhs, NdArray<double> rhs, double absoluteTolerence = 1e-8, double relativeTolerence = 1e-5)
        {
            return IsCloseWithTolerence(lhs, rhs, absoluteTolerence, relativeTolerence);
        }

        /// <summary>
        /// Element-wise check if two NdArrays have same (within machine precision) values.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <param name="absoluteTolerence">The absolute tolerance. (default 1e-8)</param>
        /// <param name="relativeTolerence">The relative tolerance. (default 1e-5)</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> IsClose(NdArray<float> lhs, NdArray<float> rhs, float absoluteTolerence = 1e-8f, float relativeTolerence = 1e-5f)
        {
            return IsCloseWithTolerence(lhs, rhs, absoluteTolerence, relativeTolerence);
        }

        /// <summary>
        /// Checks if two NdArrays have the same (within machine precision) values in all elements.
        /// The default absolute tolerance is 1e-8.
        /// The default relative tolerance is 1e-5.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>true if two NdArrays have same (within specified precision) values in all elements, otherwise false.</returns>
        public static bool AlmostEqual(NdArray<T> lhs, NdArray<T> rhs)
        {
            if (Enumerable.SequenceEqual(lhs.Shape, rhs.Shape))
            {
                return NdArray<T>.All(IsClose(lhs, rhs));
            }

            return false;
        }

        /// <summary>
        /// Checks if two NdArrays have the same (within machine precision) values in all elements.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <param name="absoluteTolerence">The absolute tolerance. (default 1e-8)</param>
        /// <param name="relativeTolerence">The relative tolerance. (default 1e-5)</param>
        /// <returns>true if two NdArrays have same (within specified precision) values in all elements, otherwise false.</returns>
        public static bool AlmostEqual(NdArray<double> lhs, NdArray<double> rhs, double absoluteTolerence = 1e-8, double relativeTolerence = 1e-5)
        {
            if (Enumerable.SequenceEqual(lhs.Shape, rhs.Shape))
            {
                return NdArray<double>.All(IsClose(lhs, rhs, absoluteTolerence, relativeTolerence));
            }

            return false;
        }

        /// <summary>
        /// Checks if two NdArrays have the same (within machine precision) values in all elements.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <param name="absoluteTolerence">The absolute tolerance. (default 1e-8)</param>
        /// <param name="relativeTolerence">The relative tolerance. (default 1e-5)</param>
        /// <returns>true if two NdArrays have same (within specified precision) values in all elements, otherwise false.</returns>
        public static bool AlmostEqual(NdArray<float> lhs, NdArray<float> rhs, float absoluteTolerence = 1e-8f, float relativeTolerence = 1e-5f)
        {
            if (Enumerable.SequenceEqual(lhs.Shape, rhs.Shape))
            {
                return NdArray<float>.All(IsClose(lhs, rhs, absoluteTolerence, relativeTolerence));
            }

            return false;
        }

        public static void FillIsFinite<TP>(NdArray<bool> target, NdArray<TP> source)
        {
            var preparedSource = NdArray<TP>.PrepareElemwiseSources(target, source);
            target.Backend.IsFinite(target, preparedSource);
        }

        /// <summary>
        /// Element-wise finity check (not -Inf, Inf or NaN).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> IsFinite(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = NdArray<T>.PrepareElemwise<bool, T>(source);
            preparedTarget.AssertBool();

            FillIsFinite(preparedTarget, preparedSource);

            return preparedTarget;
        }

        private static NdArray<bool> IsCloseWithoutTolerence<P>(NdArray<P> lhs, NdArray<P> rhs)
        {
            if (typeof(P) == typeof(double) || typeof(P) == typeof(float))
            {
                var op = ScalarPrimitivesRegistry.For<P, double>();
                P absoluteTolerenceT = op.Convert(1e-8);
                P relativeTolerenceT = op.Convert(1e-5);

                return IsCloseWithTolerence(lhs, rhs, absoluteTolerenceT, relativeTolerenceT);
            }

            return lhs == rhs;
        }

        private static NdArray<bool> IsCloseWithTolerence<P>(NdArray<P> lhs, NdArray<P> rhs, P absoluteTolerence, P relativeTolerence)
        {
            var absoluteTolerenceScalar = NdArray<P>.ScalarLike(lhs, absoluteTolerence);
            var relativeTolerenceScalar = NdArray<P>.ScalarLike(lhs, relativeTolerence);

            /// NOTE This is not symmetric.
            /// https://docs.scipy.org/doc/numpy-1.15.0/reference/generated/numpy.isclose.html
            var absDiff = NdArray<P>.Abs(lhs - rhs);
            var absRhs = NdArray<P>.Abs(rhs);
            return absDiff <= absoluteTolerenceScalar + (relativeTolerenceScalar * absRhs);
        }
    }
}