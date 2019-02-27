// <copyright file="HostDevice.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    internal sealed class HostDevice : BaseDevice
    {
        private static HostDevice instance;

        private HostDevice()
        {
        }

        public static HostDevice Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HostDevice();
                }

                return instance;
            }
        }

        public override string Id => "Host";

        public override bool Zeroed => true;

        public override IStorage<T> Create<T>(int numElements)
        {
            return new HostStorage<T>(numElements);
        }
    }
}