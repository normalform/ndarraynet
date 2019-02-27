// <copyright file="IDevice.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    public interface IDevice
    {
        IStorage<T> Create<T>(int numElements);

        string Id { get; }

        bool Zeroed { get; }
    }
}