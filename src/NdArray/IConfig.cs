// <copyright file="IConfig.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using NdArray.NdFunction.Comparison;

    public interface IConfig
    {
        IDevice Device { get; }

    }

    public interface IConfig<T> : IConfig
    {
        INdArrayComparison<T> ComparisonFunction { get; }

        IStorage<T> Create(Layout layout);
    }
}