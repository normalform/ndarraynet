//Copyright(c) 2019, Jaeho Kim
//All rights reserved.

//Redistribution and use in source and binary forms, with or without
//modification, are permitted provided that the following conditions are met:

//1. Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
//2. Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.

//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
//ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

//The views and conclusions contained in the software and documentation are those
//of the authors and should not be interpreted as representing official policies,
//either expressed or implied, of the NdArrayNet project.


namespace NdArrayNet
{
    using System;

    /// <summary>
    /// An N-dimensional array with elements of type 'T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NdArray<T> : IFrontend, IFrontend<T>
    {
        private readonly IStorage<T> storage;

        public int NumDimensions => Layout.NumDimensions;
        public int NumElements => Layout.NumElements;

        public IBackend<T> Backend => storage.Backend(this.Layout);

        public Layout Layout { get; }

        /// <summary>
        /// Implicit constructor.
        /// </summary>
        /// <param name="layout"></param>
        public NdArray(Layout layout, IStorage<T> storage)
        {
            this.Layout = layout;
            this.storage = storage;
        }

        /// <summary>
        /// Creates a new, uninitialized NdArray with a new storage.
        /// </summary>
        /// <param name="shape">The shape of the NdArray to create.</param>
        /// <param name="device">The device to store the data of the NdArray on.</param>
        /// <param name="order">The memory layout to use for the new NdArray. (default: row-major)</param>
        public NdArray(int[] shape, IDevice device, Order order = Order.RowMajor)
        {
            if(order == Order.RowMajor)
            {
                this.Layout = Layout.NewC(shape);
            }
            else
            {
                this.Layout = Layout.NewF(shape);
            }

            this.storage = device.Create<T>(this.Layout.NumElements);
        }

        public static NdArray<T> Arange(IDevice device, T start, T stop, T step)
        {
            var op = ScalarPrimitives.For<T, T>();
            var opc = ScalarPrimitives.For<int, T>();

            var numberOfElementT = op.Divide(op.Subtract(stop, start), step);
            var numberOfElementInt = opc.Convert(numberOfElementT);
            var numberOfElement = Math.Max(0, numberOfElementInt);

            var shape = new[] { numberOfElement };

            var ndArray = new NdArray<T>(shape, device);
            ndArray.FillIncrementing(start, step);

            return ndArray;
        }

        public void FillIncrementing(T start, T step)
        {
            if(this.NumDimensions != 1)
            {
                throw new InvalidOperationException("FillIncrementing requires a vector.");
            }

            this.Backend.FillIncrementing(start, step, this);
        }

        /// <summary>
        /// Creates a NdArray with the specified layout sharing its storage with the original NdArray.
        /// </summary>
        /// <param name="layout">The new NdArray memory layout.</param>
        /// <param name="array">The NdArray to operate on.</param>
        /// <returns>The resulting NdArray.</returns>
        public NdArray<T> Relayout(Layout layout)
        {
            return new NdArray<T>(layout, this.storage);
        }
    }
}