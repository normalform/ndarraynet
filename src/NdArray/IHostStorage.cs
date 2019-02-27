// <copyright file="IHostStorage.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    internal interface IHostStorage<out T>
    {
        T[] Data { get; }

        int DataSize { get; }

        int DataSizeInBytes { get; }

        PinnedMemory Pin();
    }
}