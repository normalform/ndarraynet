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
            Layout.Check(layout);

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

        public string Pretty => ToString(maxElems: 10);

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

        internal IStorage<T> Storage { get; }

        internal IBackend<T> Backend => Storage.Backend(Layout);

        public T this[int[] idx]
        {
            get
            {
                return Backend[idx];
            }

            set
            {
                Backend[idx] = value;
            }
        }

        public NdArray<T> this[int idx]
        {
            get
            {
                var args = new[] { idx }.Cast<object>().ToArray();
                return GetRange(args);
            }

            set
            {
                var args = new[] { idx }.Cast<object>().ToArray();
                SetRange(args, value);
            }
        }

        public NdArray<T> this[IRange[] ranges]
        {
            get
            {
                return GetRange(ranges);
            }

            set
            {
                SetRange(ranges, value);
            }
        }

        public static T Get(NdArray<T> array, int[] pos)
        {
            return array[pos];
        }

        public static void Set(NdArray<T> array, int[] pos, T value)
        {
            array[pos] = value;
        }

        public static (NdArray<TA>, NdArray<TB>) ApplyLayoutFn<TA, TB>(Func<Layout[], Layout[]> fn, NdArray<TA> a, NdArray<TB> b)
        {
            var layouts = new[] { a.Layout, b.Layout };
            var newLayouts = fn(layouts);
            if (newLayouts.Length == 2 && newLayouts[0] != null && newLayouts[1] != null)
            {
                return (a.Relayout(newLayouts[0]), b.Relayout(newLayouts[1]));
            }

            throw new InvalidOperationException("unexpected layout function result");
        }

        public static (NdArray<TA>, NdArray<TB>) BroadCastToSame<TA, TB>(NdArray<TA> a, NdArray<TB> b)
        {
            return ApplyLayoutFn(Layout.BroadcastToSameMany, a, b);
        }

        public static NdArray<TA> BroadCastTo<TA>(int[] shp, NdArray<TA> target)
        {
            var layout = Layout.BroadcastToShape(shp, target.Layout);
            return target.Relayout(layout);
        }

        public static NdArray<T> operator *(NdArray<T> a, NdArray<T> b)
        {
            var (trgt, arrA, arrB) = PrepareElemwise<T, T, T>(a, b);
            trgt.FillMultiply(a, b);

            return trgt;
        }

        public override string ToString()
        {
            return Pretty;
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

        public NdArray<T> Reshape(int[] shp)
        {
            var newView = TryReshapeView(shp);
            if (newView == null)
            {
                var copy = Copy().ReshapeView(shp);
                return copy;
            }

            return newView;
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

        internal static string[] SubPrint(int maxElems, int[] idx, string lineSpace, NdArray<T> array)
        {
            return idx.Select(i => PrettyDim(maxElems, lineSpace + " ", array[new[] { RangeFactory.Elem(i), RangeFactory.AllFill }])).ToArray();
        }

        internal static string[] SubStrings(int maxElems, int numLineSpace, string strLineSpace, NdArray<T> array)
        {
            if (numLineSpace <= maxElems)
            {
                return SubPrint(maxElems, Enumerable.Range(0, numLineSpace).ToArray(), strLineSpace, array);
            }
            else
            {
                var leftTo = (maxElems / 2) - 1;
                var remaning = maxElems - leftTo - 2;
                var rightFrom = numLineSpace - remaning;
                var leftIndex = Enumerable.Range(0, leftTo + 1).ToArray();
                var rightIndex = Enumerable.Range(rightFrom, numLineSpace - rightFrom).ToArray();
                string elipsis;
                if (typeof(float) is T)
                {
                    elipsis = "      ...";
                }
                else if (typeof(double) is T)
                {
                    elipsis = "      ...";
                }
                else if (typeof(int) is T)
                {
                    elipsis = " ...";
                }
                else if (typeof(byte) is T)
                {
                    elipsis = "...";
                }
                else if (typeof(bool) is T)
                {
                    elipsis = " ... ";
                }
                else
                {
                    elipsis = "...";
                }

                var result = SubPrint(maxElems, leftIndex, strLineSpace, array).Concat(new[] { elipsis }).Concat(SubPrint(maxElems, rightIndex, strLineSpace, array));
                return result.ToArray();
            }
        }

        internal static string ScalarString(NdArray<T> array)
        {
            string msg;
            var val = array.Value;
            if (val is float)
            {
                var fval = Convert.ToSingle(val);
                if (fval >= 0.0f)
                {
                    msg = string.Format("{0,9:F4}", fval);
                }
                else
                {
                    msg = string.Format("{0,9:F3}", fval);
                }
            }
            else if (val is double)
            {
                var fval = Convert.ToDouble(val);
                if (fval >= 0.0)
                {
                    msg = string.Format("{0,9:F4}", fval);
                }
                else
                {
                    msg = string.Format("{0,9:F3}", fval);
                }
            }
            else if (val is int)
            {
                var fval = Convert.ToInt32(val);
                msg = string.Format("{0,4:D}", fval);
            }
            else if (val is long)
            {
                var fval = Convert.ToInt64(val);
                msg = string.Format("{0,4:D}", fval);
            }
            else if (val is byte)
            {
                var fval = Convert.ToByte(val);
                msg = string.Format("{0,3:D}", fval);
            }
            else if (val is bool)
            {
                var fval = Convert.ToBoolean(val);
                if (fval is true)
                {
                    msg = "true";
                }
                else
                {
                    msg = "false";
                }
            }
            else
            {
                msg = val.ToString();
            }

            return msg;
        }

        internal static string PrettyDim(int maxElems, string lineSpace, NdArray<T> array)
        {
            var numLineSpece = array.Shape.Length > 0 ? array.Shape[0] : 0;

            string msg;
            if (array.NumDimensions == 0)
            {
                msg = ScalarString(array);
            }
            else if (array.NumDimensions == 1)
            {
                var midStr = string.Join(" ", SubStrings(maxElems, numLineSpece, lineSpace, array));
                msg = "[" + midStr + "]";
            }
            else
            {
                var midStr = string.Join("\n" + lineSpace, SubStrings(maxElems, numLineSpece, lineSpace, array));
                msg = "[" + midStr + "]";
            }

            return msg;
        }

        internal static (NdArray<TR>, NdArray<TA>) PrepareElemwise<TR, TA>(NdArray<TA> array, Order order = Order.RowMajor)
        {
            var target = new NdArray<TR>(array.Shape, array.Storage.Device, order);
            return (target, array);
        }

        internal static (NdArray<TR>, NdArray<TA>, NdArray<TB>) PrepareElemwise<TR, TA, TB>(NdArray<TA> arrayA, NdArray<TB> arrayB, Order order = Order.RowMajor)
        {
            // AssertSameStorage [later..]
            var (arrA, arrB) = BroadCastToSame(arrayA, arrayB);
            var target = new NdArray<TR>(arrA.Shape, arrA.Storage.Device, order);

            return (target, arrayA, arrB);
        }

        internal static (NdArray<TA>, NdArray<TB>) PrepareElemwiseSources<TR, TA, TB>(NdArray<TR> target, NdArray<TA> arrayA, NdArray<TB> arrayB)
        {
            // AssertSameStorage [later..]
            var arrA = BroadCastTo(target.Shape, arrayA);
            var arrB = BroadCastTo(target.Shape, arrayB);

            return (arrA, arrB);
        }

        internal void FillMultiply(NdArray<T> a, NdArray<T> b)
        {
            var (aa, bb) = PrepareElemwiseSources(this, a, b);
            Backend.Multiply(this, aa, bb);
        }

        internal NdArray<T> Copy(Order order = Order.RowMajor)
        {
            var (target, src) = PrepareElemwise<T, T>(this, order);
            target.Backend.Copy(target, src);
            return target;
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

        internal NdArray<T> TryReshapeView(int[] shape)
        {
            var newLayout = Layout.TryReshape(shape, this);
            if (newLayout is null)
            {
                return null;
            }

            return Relayout(newLayout);
        }

        internal NdArray<T> ReshapeView(int[] shape)
        {
            var newNdArray = TryReshapeView(shape);
            if (newNdArray is null)
            {
                var msg = string.Format("Cannot reshape NdArray of shape {0} and strides {1} without copying.", Shape, Layout.Stride);
                throw new InvalidOperationException(msg);
            }

            return newNdArray;
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
            return PrettyDim(maxElems, " ", this);
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