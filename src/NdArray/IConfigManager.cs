// <copyright file="IConfigManager.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    public interface IConfigManager
    {
        IConfig<T> GetConfig<T>();
    }
}