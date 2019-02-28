// <copyright file="BaseComparison.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction.Comparison
{
    using System.Linq;
    using NdArrayNet;

    internal abstract class BaseComparison<T> : INdComparison<T>
    {
        private IStaticMethod staticMethod;

        protected BaseComparison(IStaticMethod staticMethod)
        {
            this.staticMethod = staticMethod;
        }

        public virtual bool AlmostEqual(NdArray<T> lhs, NdArray<T> rhs, T absoluteTolerence, T relativeTolerence)
        {
            if (Enumerable.SequenceEqual(lhs.Shape, rhs.Shape))
            {
                return staticMethod.All(IsClose(lhs, rhs, absoluteTolerence, relativeTolerence));
            }

            return false;
        }

        public virtual NdArray<bool> Equal(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preapredLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, T, T>(lhs, rhs, Order.RowMajor);
            preparedTarget.AssertBool();

            FillEqual(preparedTarget, preapredLhs, preparedRhs);

            return preparedTarget;
        }

        public virtual void FillEqual(IFrontend<bool> result, IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(result, lhs, rhs);
            result.Backend.Equal(result, preparedLhs, preparedRhs);
        }

        public virtual void FillGreater(NdArray<bool> result, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(result, lhs, rhs);
            result.Backend.Greater(result, preparedLhs, preparedRhs);
        }

        public virtual void FillGreaterOrEqual(NdArray<bool> result, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(result, lhs, rhs);
            result.Backend.GreaterOrEqual(result, preparedLhs, preparedRhs);
        }

        public virtual void FillIsFinite(NdArray<bool> result, NdArray<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(result, source);
            result.Backend.IsFinite(result, preparedSource);
        }

        public virtual void FillLess(NdArray<bool> result, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(result, lhs, rhs);
            result.Backend.Less(result, preparedLhs, preparedRhs);
        }

        public virtual void FillLessOrEqual(NdArray<bool> result, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(result, lhs, rhs);
            result.Backend.LessOrEqual(result, preparedLhs, preparedRhs);
        }

        public virtual void FillNotEqual(NdArray<bool> result, NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(result, lhs, rhs);
            result.Backend.NotEqual(result, preparedLhs, preparedRhs);
        }

        public virtual NdArray<bool> Greater(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, T, T>(lhs, rhs, Order.RowMajor);
            preparedTarget.AssertBool();

            FillGreater(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        public virtual NdArray<bool> GreaterOrEqual(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, T, T>(lhs, rhs, Order.RowMajor);
            preparedTarget.AssertBool();

            FillGreaterOrEqual(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        public virtual NdArray<bool> IsClose(NdArray<T> lhs, NdArray<T> rhs, T absoluteTolerence, T relativeTolerence)
        {
            return IsCloseWithTolerence(lhs, rhs, absoluteTolerence, relativeTolerence);
        }

        public virtual NdArray<bool> IsFinite(NdArray<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<bool, T>(source, Order.RowMajor);
            preparedTarget.AssertBool();

            FillIsFinite(preparedTarget, source);

            return preparedTarget;
        }

        public virtual NdArray<bool> Less(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, T, T>(lhs, rhs, Order.RowMajor);
            preparedTarget.AssertBool();

            FillLess(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        public virtual NdArray<bool> LessOrEqual(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, T, T>(lhs, rhs, Order.RowMajor);
            preparedTarget.AssertBool();

            FillLessOrEqual(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        public virtual NdArray<bool> NotEqual(NdArray<T> lhs, NdArray<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, T, T>(lhs, rhs, Order.RowMajor);
            preparedTarget.AssertBool();

            FillNotEqual(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        private static NdArray<bool> IsCloseWithoutTolerence(NdArray<T> lhs, NdArray<T> rhs)
        {
            if (typeof(T) == typeof(double) || typeof(T) == typeof(float))
            {
                var op = ScalarPrimitivesRegistry.For<T, double>();
                T absoluteTolerenceT = op.Convert(1e-8);
                T relativeTolerenceT = op.Convert(1e-5);

                return IsCloseWithTolerence(lhs, rhs, absoluteTolerenceT, relativeTolerenceT);
            }

            return lhs == rhs;
        }

        private static NdArray<bool> IsCloseWithTolerence(NdArray<T> lhs, NdArray<T> rhs, T absoluteTolerence, T relativeTolerence)
        {
            var absoluteTolerenceScalar = NdArray<T>.ScalarLike(lhs, absoluteTolerence);
            var relativeTolerenceScalar = NdArray<T>.ScalarLike(lhs, relativeTolerence);

            /// NOTE This is not symmetric.
            /// https://docs.scipy.org/doc/numpy-1.15.0/reference/generated/numpy.isclose.html
            var absDiff = NdArray<T>.Abs(lhs - rhs);
            var absRhs = NdArray<T>.Abs(rhs);
            return absDiff <= absoluteTolerenceScalar + (relativeTolerenceScalar * absRhs);
        }
    }
}