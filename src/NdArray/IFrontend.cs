// <copyright file="IFrontend.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using NdArray.NdFunction;

    public interface IFrontend
    {
        Layout Layout { get; }

        int[] Shape { get; }
    }

    public interface IFrontend<T> : IFrontend
    {
        NdArray<T> Relayout(Layout layout);

        IBackend<T> Backend { get; }

        IConfig<T> Config { get; }

        INdFunction<T> NdFunction { get; }

        IConfigManager ConfigManager { get; }
    }
}