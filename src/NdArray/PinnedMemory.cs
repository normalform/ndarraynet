// <copyright file="PinnedMemory.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using System;
    using System.Runtime.InteropServices;

    public class PinnedMemory : IDisposable
    {
        private readonly GCHandle garbageCollectorHandleHnd;
        private bool disposed = false;

        public PinnedMemory(GCHandle handle, long size)
        {
            garbageCollectorHandleHnd = handle;
            Ptr = handle.AddrOfPinnedObject();
            Size = size;
        }

        public long Size { get; }

        public IntPtr Ptr { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                garbageCollectorHandleHnd.Free();
            }

            disposed = true;
        }
    }
}