// <copyright file="IStorage.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    /// <summary>
    /// NdArray storage (type neutral).
    /// </summary>
    public interface IStorage
    {
        IDevice Device { get; }
    }

    /// <summary>
    /// NdArray storage.
    /// </summary>
    public interface IStorage<T> : IStorage
    {
        IConfig<T> Config { get; }

        IBackend<T> Backend(Layout layout);
    }
}