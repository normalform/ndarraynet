// <copyright file="HostStorage.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// 1. Redistributions of source code must retain the above copyright notice, this
//    list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// The views and conclusions contained in the software and documentation are those
// of the authors and should not be interpreted as representing official policies,
// either expressed or implied, of the NdArrayNet project.
// </copyright>

namespace NdArrayNet
{
    using System;
    using System.Runtime.InteropServices;

    public class HostStorage<T> : IStorage<T>, IStorage, IHostStorage<T>
    {
        private readonly int UnitSize = Marshal.SizeOf(typeof(T));

        public HostStorage(T[] data)
        {
            Data = data;
        }

        public HostStorage(long numberOfElements)
        {
            if (numberOfElements > int.MaxValue)
            {
                var msg = string.Format("Cannot create host NdArray storage for {0} elements, the current limit is {1} elements.", numberOfElements, int.MaxValue);
                throw new ArgumentOutOfRangeException(msg);
            }

            Data = new T[numberOfElements];
        }

        public T[] Data { get; }

        public IDevice Device => HostDevice.Instance;

        public int DataSize => Data.Length;

        public int DataSizeInBytes => DataSize * UnitSize;

        public IBackend<T> Backend(Layout layout)
        {
            return new HostBackend<T>(layout, this);
        }

        public PinnedMemory Pin()
        {
            var gcHnd = GCHandle.Alloc(Data, GCHandleType.Pinned);
            return new PinnedMemory(gcHnd, Data.LongLength * UnitSize);
        }
    }
}