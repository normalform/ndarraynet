// <copyright file="BaseDevice.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using System;

    internal abstract class BaseDevice : IDevice
    {
        public virtual string Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool Zeroed
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual IStorage<T> Create<T>(int numElements)
        {
            throw new NotImplementedException();
        }
    }
}