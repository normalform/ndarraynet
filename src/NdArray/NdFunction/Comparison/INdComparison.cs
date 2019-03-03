// <copyright file="INdComparison.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction.Comparison
{
    using NdArrayNet;

    public interface INdComparison<T>
    {
        NdArray<bool> Equal(IFrontend<T> lhs, IFrontend<T> rhs);
        NdArray<bool> NotEqual(IFrontend<T> lhs, IFrontend<T> rhs);
        NdArray<bool> Less(IFrontend<T> lhs, IFrontend<T> rhs);
        NdArray<bool> LessOrEqual(IFrontend<T> lhs, IFrontend<T> rhs);
        NdArray<bool> Greater(IFrontend<T> lhs, IFrontend<T> rhs);
        NdArray<bool> GreaterOrEqual(IFrontend<T> lhs, IFrontend<T> rhs);
        NdArray<bool> IsClose(IFrontend<T> lhs, IFrontend<T> rhs, T absoluteTolerence, T relativeTolerence);
        NdArray<bool> IsFinite(IFrontend<T> source);
        bool AlmostEqual(IFrontend<T> lhs, IFrontend<T> rhs, T absoluteTolerence, T relativeTolerence);

        void FillEqual(IFrontend<bool> result, IFrontend<T> lhs, IFrontend<T> rhs);
        void FillNotEqual(IFrontend<bool> result, IFrontend<T> lhs, IFrontend<T> rhs);
        void FillLess(IFrontend<bool> result, IFrontend<T> lhs, IFrontend<T> rhs);
        void FillLessOrEqual(IFrontend<bool> result, IFrontend<T> lhs, IFrontend<T> rhs);
        void FillGreater(IFrontend<bool> result, IFrontend<T> lhs, IFrontend<T> rhs);
        void FillGreaterOrEqual(IFrontend<bool> result, IFrontend<T> lhs, IFrontend<T> rhs);
        void FillIsFinite(IFrontend<bool> result, IFrontend<T> source);
    }
}
