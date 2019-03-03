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

        public virtual NdArray<bool> Equal(IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (preparedTarget, preapredLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, T, T>(lhs, rhs, Order.RowMajor);
            staticMethod.AssertBool(preparedTarget);

            FillEqual(preparedTarget, preapredLhs, preparedRhs);

            return preparedTarget as NdArray<bool>;
        }

        public virtual void FillEqual(IFrontend<bool> result, IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(result, lhs, rhs);
            result.Backend.Equal(result, preparedLhs, preparedRhs);
        }

        public virtual void FillGreater(IFrontend<bool> result, IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(result, lhs, rhs);
            result.Backend.Greater(result, preparedLhs, preparedRhs);
        }

        public virtual void FillGreaterOrEqual(IFrontend<bool> result, IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(result, lhs, rhs);
            result.Backend.GreaterOrEqual(result, preparedLhs, preparedRhs);
        }

        public virtual void FillIsFinite(IFrontend<bool> result, IFrontend<T> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(result, source);
            result.Backend.IsFinite(result, preparedSource);
        }

        public virtual void FillLess(IFrontend<bool> result, IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(result, lhs, rhs);
            result.Backend.Less(result, preparedLhs, preparedRhs);
        }

        public virtual void FillLessOrEqual(IFrontend<bool> result, IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(result, lhs, rhs);
            result.Backend.LessOrEqual(result, preparedLhs, preparedRhs);
        }

        public virtual void FillNotEqual(IFrontend<bool> result, IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(result, lhs, rhs);
            result.Backend.NotEqual(result, preparedLhs, preparedRhs);
        }

        public virtual NdArray<bool> Greater(IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, T, T>(lhs, rhs, Order.RowMajor);
            staticMethod.AssertBool(preparedTarget);

            FillGreater(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget as NdArray<bool>;
        }

        public virtual NdArray<bool> GreaterOrEqual(IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, T, T>(lhs, rhs, Order.RowMajor);
            staticMethod.AssertBool(preparedTarget);

            FillGreaterOrEqual(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget as NdArray<bool>;
        }

        public virtual NdArray<bool> Less(IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, T, T>(lhs, rhs, Order.RowMajor);
            staticMethod.AssertBool(preparedTarget);

            FillLess(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget as NdArray<bool>;
        }

        public virtual NdArray<bool> LessOrEqual(IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, T, T>(lhs, rhs, Order.RowMajor);
            staticMethod.AssertBool(preparedTarget);

            FillLessOrEqual(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget as NdArray<bool>;
        }

        public virtual NdArray<bool> NotEqual(IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, T, T>(lhs, rhs, Order.RowMajor);
            staticMethod.AssertBool(preparedTarget);

            FillNotEqual(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget as NdArray<bool>;
        }

        public virtual NdArray<bool> IsFinite(IFrontend<T> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<bool, T>(source, Order.RowMajor);
            staticMethod.AssertBool(preparedTarget);

            FillIsFinite(preparedTarget, source);

            return preparedTarget as NdArray<bool>;
        }

        public virtual NdArray<bool> IsClose(IFrontend<T> lhs, IFrontend<T> rhs, T absoluteTolerence, T relativeTolerence)
        {
            return staticMethod.IsCloseWithTolerence(lhs, rhs, absoluteTolerence, relativeTolerence) as NdArray<bool>;
        }

        public virtual bool AlmostEqual(IFrontend<T> lhs, IFrontend<T> rhs, T absoluteTolerence, T relativeTolerence)
        {
            if (Enumerable.SequenceEqual(lhs.Shape, rhs.Shape))
            {
                return staticMethod.All(IsClose(lhs, rhs, absoluteTolerence, relativeTolerence));
            }

            return false;
        }
    }
}