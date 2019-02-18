// <copyright file="ComparisonFunction.cs" company="NdArrayNet">
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

        public static bool AlmostEqual(NdArray<T> lhs, NdArray<T> rhs)
        {
            if (Enumerable.SequenceEqual(lhs.Shape, rhs.Shape))
            {
                return NdArray<T>.All(IsClose(lhs, rhs));
            }

            return false;
        }

        public static bool AlmostEqual(NdArray<double> lhs, NdArray<double> rhs, double absoluteTolerence = 1e-8, double relativeTolerence = 1e-5)
        {
            if (Enumerable.SequenceEqual(lhs.Shape, rhs.Shape))
            {
                return NdArray<double>.All(IsClose(lhs, rhs, absoluteTolerence, relativeTolerence));
            }

            return false;
        }

        public static bool AlmostEqual(NdArray<float> lhs, NdArray<float> rhs, float absoluteTolerence = 1e-8f, float relativeTolerence = 1e-5f)
        {
            if (Enumerable.SequenceEqual(lhs.Shape, rhs.Shape))
            {
                return NdArray<float>.All(IsClose(lhs, rhs, absoluteTolerence, relativeTolerence));
            }

            return false;
        }

        private static NdArray<bool> IsCloseWithoutTolerence<P>(NdArray<P> lhs, NdArray<P> rhs)
        {
            if (typeof(P) == typeof(double) || typeof(P) == typeof(float))
            {
                var op = ScalarPrimitives.For<P, double>();
                P absoluteTolerenceT = absoluteTolerenceT = op.Convert(1e-8);
                P relativeTolerenceT = relativeTolerenceT = op.Convert(1e-5);

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