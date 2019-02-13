// <copyright file="NdArray.cs" company="NdArrayNet">
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
    using System.Linq;

    /// <summary>
    /// An N-dimensional array with elements of type 'T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NdArray<T> : IFrontend, IFrontend<T>
    {
        public IStorage<T> Storage { get; }

        public int NumDimensions => Layout.NumDimensions;
        public int NumElements => Layout.NumElements;

        public int[] Shape => Layout.Shape;

        internal IBackend<T> Backend => Storage.Backend(Layout);

        public Layout Layout { get; }

        /// <summary>
        /// Implicit constructor.
        /// </summary>
        /// <param name="layout"></param>
        public NdArray(Layout layout, IStorage<T> storage)
        {
            Layout = layout;
            Storage = storage;
        }

        /// <summary>
        /// Creates a new, uninitialized NdArray with a new storage.
        /// </summary>
        /// <param name="shape">The shape of the NdArray to create.</param>
        /// <param name="device">The device to store the data of the NdArray on.</param>
        /// <param name="order">The memory layout to use for the new NdArray. (default: row-major)</param>
        public NdArray(int[] shape, IDevice device, Order order = Order.RowMajor)
        {
            if (order == Order.RowMajor)
            {
                Layout = Layout.NewC(shape);
            }
            else
            {
                Layout = Layout.NewF(shape);
            }

            Storage = device.Create<T>(Layout.NumElements);
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

        public static NdArray<T> Ones(IDevice device, int[] shape)
        {
            var ndArray = new NdArray<T>(shape, device);
            ndArray.FillConst(Utils.Primitives.One<T>());

            return ndArray;
        }

        public static NdArray<T> Zeros(IDevice device, int[] shape)
        {
            var ndArray = new NdArray<T>(shape, device);

            return ndArray;
        }

        internal void FillConst(T value)
        {
            Backend.FillConst(value, this);
        }

        internal void FillIncrementing(T start, T step)
        {
            if (NumDimensions != 1)
            {
                throw new InvalidOperationException("FillIncrementing requires a vector.");
            }

            Backend.FillIncrementing(start, step, this);
        }

        /// <summary>
        /// Creates a NdArray with the specified layout sharing its storage with the original NdArray.
        /// </summary>
        /// <param name="layout">The new NdArray memory layout.</param>
        /// <param name="array">The NdArray to operate on.</param>
        /// <returns>The resulting NdArray.</returns>
        public NdArray<T> Relayout(Layout layout)
        {
            return new NdArray<T>(layout, Storage);
        }

        internal NdArray<T> Range(IRange[] ranges)
        {
            return Relayout(Layout.View(ranges, Layout));
        }

        internal NdArray<T> GetRange(object[] rngArgs)
        {
            return Range(RangeArgParser.ofItemOrSliceArgs(rngArgs));
        }

        public NdArray<T> this[int i]
        {
            get { return GetRange((new[] { i }).Cast<object>().ToArray()); }
            //set { }
        }

        internal string ToString(int maxElems)
        {
            return "";
            //string PreetyDim(string lineSpace, NdArray<T> array)
            //{
            //    var ls = array.Shape[0];

            //    string SubPrint(int[] idxes)
            //    {
            //        idxes.Select(i => PreetyDim(lineSpace + " "))
            //        return "";
            //    }

            //    return "";
            //}

            //return PreetyDim(" ", this);
        }

        public string Pretty => ToString(maxElems: 10);

        public override string ToString() => Pretty;
    }
}