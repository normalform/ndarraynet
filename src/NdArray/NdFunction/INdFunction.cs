// <copyright file="INdFunction.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction
{
    public interface INdFunction<T>
    {
        Comparison.INdArrayComparison<T> Comparison { get; }
    }
}