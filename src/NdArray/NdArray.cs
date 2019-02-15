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
        /// <summary>
        /// Implicit constructor.
        /// </summary>
        /// <param name="layout"></param>
        internal NdArray(Layout layout, IStorage<T> storage)
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
        internal NdArray(int[] shape, IDevice device, Order order = Order.RowMajor)
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

        public int NumDimensions => Layout.NumDimensions;

        public int NumElements => Layout.NumElements;

        public int[] Shape => Layout.Shape;

        public Layout Layout { get; }

        public T Value
        {
            get
            {
                AssertScalar();
                var noDim = new int[] { };
                return Backend[noDim];
            }

            set
            {
                AssertScalar();
                var noDim = new int[] { };
                Backend[noDim] = value;
            }
        }

        public string Pretty => ToString(maxElems: 10);

        internal IStorage<T> Storage { get; }

        internal IBackend<T> Backend => Storage.Backend(Layout);

        public NdArray<T> this[int i]
        {
            get
            {
                var args = new[] { i }.Cast<object>().ToArray();
                return GetRange(args);
            }

            set
            {
                var args = new[] { i }.Cast<object>().ToArray();
                SetRange(args, value);
            }
        }

        public override string ToString() => Pretty;

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

        public NdArray<T> BroadCastTo(int[] shp, NdArray<T> target)
        {
            return target.Relayout(Layout.BroadcastToShape(shp, target.Layout));
        }

        internal static NdArray<T> Arange(IDevice device, T start, T stop, T step)
        {
            var op = ScalarPrimitives.For<T, T>();
            var opc = ScalarPrimitives.For<int, T>();

            var numberOfElementT = op.Divide(op.Subtract(stop, start), step);
            var numberOfElementInt = opc.Convert(numberOfElementT);
            var numberOfElement = Math.Max(0, numberOfElementInt);

            var shape = new[] { numberOfElement };

            var newArray = new NdArray<T>(shape, device);
            newArray.FillIncrementing(start, step);

            return newArray;
        }

        internal static NdArray<T> Ones(IDevice device, int[] shape)
        {
            var newArray = new NdArray<T>(shape, device);
            newArray.FillConst(Primitives.One<T>());

            return newArray;
        }

        internal static NdArray<T> Zeros(IDevice device, int[] shape)
        {
            var newArray = new NdArray<T>(shape, device);
            return newArray;
        }

        internal static void AssertSameShape(NdArray<T> a, NdArray<T> b)
        {
            if (!Enumerable.SequenceEqual(a.Shape, b.Shape))
            {
                var msg = string.Format("Tensors of shapes {0} and {1} were expected to have same shape", a.Shape, b.Shape);
                throw new ArgumentOutOfRangeException(msg);
            }
        }

        internal static void AssertSameStorage(NdArray<T>[] arrays)
        {
            // skip this for now because of it support only one storage type for now.
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

        internal NdArray<T> Range(IRange[] ranges)
        {
            return Relayout(Layout.View(ranges, Layout));
        }

        internal NdArray<T> GetRange(object[] rngArgs)
        {
            return Range(RangeArgParser.Parse(rngArgs));
        }

        internal void SetRange(object[] rngArgs, NdArray<T> value)
        {
            AssertSameStorage(new[] { this, value });
            var rng = Range(RangeArgParser.Parse(rngArgs));
            rng.CopyFrom(BroadCastTo(rng.Shape, value));
        }

        internal void CopyFrom(NdArray<T> src)
        {
            AssertSameShape(this, src);
            AssertSameStorage(new[] { this, src });

            Backend.Copy(this, src);
        }

        internal string ToString(int maxElems)
        {
            return string.Empty;
        }

        internal void AssertScalar()
        {
            if (NumDimensions != 0)
            {
                var msg = string.Format("This operation requires a scalar (0-dimensional) NdArray, but its shape is {0}", Shape);
                throw new InvalidOperationException(msg);
            }
        }
    }
}