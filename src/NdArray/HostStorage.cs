// <copyright file="HostStorage.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    [DebuggerDisplay("Count={DataSize}, InBytes={DataSizeInBytes}")]
    internal class HostStorage<T> : IStorage<T>, IHostStorage<T>
    {
        private readonly int unitSize = Marshal.SizeOf(typeof(T));

        public HostStorage(T[] data)
        {
            Data = data;
        }

        public HostStorage(long numberOfElements)
        {
            if (numberOfElements > int.MaxValue)
            {
                var errorMessage = string.Format("Cannot create host NdArray storage for {0} elements, the current limit is {1} elements.", numberOfElements, int.MaxValue);
                throw new ArgumentOutOfRangeException(errorMessage);
            }

            Data = new T[numberOfElements];
        }

        public T[] Data { get; }

        public IConfig<T> Config => ConfigManager.Instance.GetConfig<T>();

        public IDevice Device => Config.Device;

        public int DataSize => Data.Length;

        public int DataSizeInBytes => DataSize * unitSize;

        public IBackend<T> Backend(Layout layout)
        {
            return new HostBackend<T>(layout, this);
        }

        public PinnedMemory Pin()
        {
            var handle = GCHandle.Alloc(Data, GCHandleType.Pinned);
            return new PinnedMemory(handle, Data.LongLength * unitSize);
        }
    }
}