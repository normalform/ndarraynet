// <copyright file="INdArrayComparison.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction.Comparison
{
    using NdArrayNet;

    public interface INdArrayComparison
    {

    }

    public interface INdArrayComparison<T> : INdArrayComparison
    {
        NdArray<bool> Equal(NdArray<T> lhs, NdArray<T> rhs);
        NdArray<bool> NotEqual(NdArray<T> lhs, NdArray<T> rhs);
        NdArray<bool> Less(NdArray<T> lhs, NdArray<T> rhs);
        NdArray<bool> LessOrEqual(NdArray<T> lhs, NdArray<T> rhs);
        NdArray<bool> Greater(NdArray<T> lhs, NdArray<T> rhs);
        NdArray<bool> GreaterOrEqual(NdArray<T> lhs, NdArray<T> rhs);
        NdArray<bool> IsClose(NdArray<T> lhs, NdArray<T> rhs, T absoluteTolerence, T relativeTolerence);
        NdArray<bool> IsFinite(NdArray<T> source);
        bool AlmostEqual(NdArray<T> lhs, NdArray<T> rhs, T absoluteTolerence, T relativeTolerence);

        void FillEqual(IFrontend<bool> result, IFrontend<T> lhs, IFrontend<T> rhs);
        void FillNotEqual(NdArray<bool> result, NdArray<T> lhs, NdArray<T> rhs);
        void FillLess(NdArray<bool> result, NdArray<T> lhs, NdArray<T> rhs);
        void FillLessOrEqual(NdArray<bool> result, NdArray<T> lhs, NdArray<T> rhs);
        void FillGreater(NdArray<bool> result, NdArray<T> lhs, NdArray<T> rhs);
        void FillGreaterOrEqual(NdArray<bool> result, NdArray<T> lhs, NdArray<T> rhs);
        void FillIsFinite(NdArray<bool> result, NdArray<T> source);
    }
}
