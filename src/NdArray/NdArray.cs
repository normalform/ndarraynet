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
    using System.Collections.Generic;
    using System.Linq;
    using NdArray.NdArrayImpl;

    /// <summary>
    /// An N-dimensional array with elements of type 'T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NdArray<T> : IFrontend, IFrontend<T>
    {
        private static readonly Dictionary<Type, Func<T, string>> StringCoverter = new Dictionary<Type, Func<T, string>>
        {
            { typeof(float), (T v) => { var val = Convert.ToSingle(v); return val >= 0.0f ? string.Format("{0,9:F4}", val) : string.Format("{0,9:F3}", val); } },
            { typeof(double), (T v) => { var val = Convert.ToDouble(v); return val >= 0.0f ? string.Format("{0,9:F4}", val) : string.Format("{0,9:F3}", val); } },
            { typeof(int), (T v) => { var val = Convert.ToInt32(v); return string.Format("{0,4:D}", val); } },
            { typeof(long), (T v) => { var val = Convert.ToUInt64(v); return string.Format("{0,4:D}", val); } },
            { typeof(byte), (T v) => { var val = Convert.ToByte(v); return string.Format("{0,3:D}", val); } },
            { typeof(bool), (T v) => { var val = Convert.ToBoolean(v); return val ? "true" : "false"; } },
        };

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

        public Type DataType => typeof(T);

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

        public static NdArray<T> operator +(NdArray<T> a) => ElementWiseOperator<T>.FillUnaryPlus(a);

        public static NdArray<T> operator -(NdArray<T> a) => ElementWiseOperator<T>.FillUnaryMinus(a);

        public static NdArray<T> operator +(NdArray<T> a, NdArray<T> b) => ElementWiseOperator<T>.FillAdd(a, b);

        public static NdArray<T> operator +(NdArray<T> a, T b) => ElementWiseOperator<T>.FillAdd(a, ScalarLike(a, b));

        public static NdArray<T> operator +(T a, NdArray<T> b) => ElementWiseOperator<T>.FillAdd(ScalarLike(b, a), b);

        public static NdArray<T> operator -(NdArray<T> a, NdArray<T> b) => ElementWiseOperator<T>.FillSubtract(a, b);

        public static NdArray<T> operator -(NdArray<T> a, T b) => ElementWiseOperator<T>.FillSubtract(a, ScalarLike(a, b));

        public static NdArray<T> operator -(T a, NdArray<T> b) => ElementWiseOperator<T>.FillSubtract(ScalarLike(b, a), b);

        public static NdArray<T> operator *(NdArray<T> a, NdArray<T> b) => ElementWiseOperator<T>.FillMultiply(a, b);

        public static NdArray<T> operator *(NdArray<T> a, T b) => ElementWiseOperator<T>.FillMultiply(a, ScalarLike(a, b));

        public static NdArray<T> operator *(T a, NdArray<T> b) => ElementWiseOperator<T>.FillMultiply(ScalarLike(b, a), b);

        public static NdArray<T> operator /(NdArray<T> a, NdArray<T> b) => ElementWiseOperator<T>.FillDivide(a, b);

        public static NdArray<T> operator /(NdArray<T> a, T b) => ElementWiseOperator<T>.FillDivide(a, ScalarLike(a, b));

        public static NdArray<T> operator /(T a, NdArray<T> b) => ElementWiseOperator<T>.FillDivide(ScalarLike(b, a), b);

        public static NdArray<T> operator %(NdArray<T> a, NdArray<T> b) => ElementWiseOperator<T>.FillModulo(a, b);

        public static NdArray<T> operator %(NdArray<T> a, T b) => ElementWiseOperator<T>.FillModulo(a, ScalarLike(a, b));

        public static NdArray<T> operator %(T a, NdArray<T> b) => ElementWiseOperator<T>.FillModulo(ScalarLike(b, a), b);

        public static NdArray<bool> operator ==(NdArray<T> a, NdArray<T> b) => ComparisonFunction<T>.FillEqual(a, b);

        public static NdArray<bool> operator ==(NdArray<T> a, T b) => ComparisonFunction<T>.FillEqual(a, ScalarLike(a, b));

        public static NdArray<bool> operator ==(T a, NdArray<T> b) => ComparisonFunction<T>.FillEqual(ScalarLike(b, a), b);

        public static NdArray<bool> operator !=(NdArray<T> a, NdArray<T> b) => ComparisonFunction<T>.FillNotEqual(a, b);

        public static NdArray<bool> operator !=(NdArray<T> a, T b) => ComparisonFunction<T>.FillNotEqual(a, ScalarLike(a, b));

        public static NdArray<bool> operator !=(T a, NdArray<T> b) => ComparisonFunction<T>.FillNotEqual(ScalarLike(b, a), b);

        public static NdArray<bool> operator <(NdArray<T> a, NdArray<T> b) => ComparisonFunction<T>.FillLess(a, b);

        public static NdArray<bool> operator <(NdArray<T> a, T b) => ComparisonFunction<T>.FillLess(a, ScalarLike(a, b));

        public static NdArray<bool> operator <(T a, NdArray<T> b) => ComparisonFunction<T>.FillLess(ScalarLike(b, a), b);

        public static NdArray<bool> operator <=(NdArray<T> a, NdArray<T> b) => ComparisonFunction<T>.FillLessOrEqual(a, b);

        public static NdArray<bool> operator <=(NdArray<T> a, T b) => ComparisonFunction<T>.FillLessOrEqual(a, ScalarLike(a, b));

        public static NdArray<bool> operator <=(T a, NdArray<T> b) => ComparisonFunction<T>.FillLessOrEqual(ScalarLike(b, a), b);

        public static NdArray<bool> operator >(NdArray<T> a, NdArray<T> b) => ComparisonFunction<T>.FillGreater(a, b);

        public static NdArray<bool> operator >(NdArray<T> a, T b) => ComparisonFunction<T>.FillGreater(a, ScalarLike(a, b));

        public static NdArray<bool> operator >(T a, NdArray<T> b) => ComparisonFunction<T>.FillGreater(ScalarLike(b, a), b);

        public static NdArray<bool> operator >=(NdArray<T> a, NdArray<T> b) => ComparisonFunction<T>.FillGreaterOrEqual(a, b);

        public static NdArray<bool> operator >=(NdArray<T> a, T b) => ComparisonFunction<T>.FillGreaterOrEqual(a, ScalarLike(a, b));

        public static NdArray<bool> operator >=(T a, NdArray<T> b) => ComparisonFunction<T>.FillGreaterOrEqual(ScalarLike(b, a), b);

        /// <summary>
        /// Element-wise check if two NdArrays have same (within machine precision) values.
        /// The default absolute tolerance is 1e-8.
        /// The default relative tolerance is 1e-5.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> IsClose(NdArray<T> lhs, NdArray<T> rhs) => ComparisonFunction<T>.IsClose(lhs, rhs);

        /// <summary>
        /// Element-wise check if two NdArrays have same (within machine precision) values.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <param name="absoluteTolerence">The absolute tolerance. (default 1e-8)</param>
        /// <param name="relativeTolerence">The relative tolerance. (default 1e-5)</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> IsClose(NdArray<double> lhs, NdArray<double> rhs, double absoluteTolerence = 1e-8, double relativeTolerence = 1e-5) => ComparisonFunction<double>.IsClose(lhs, rhs, absoluteTolerence, relativeTolerence);

        /// <summary>
        /// Element-wise check if two NdArrays have same (within machine precision) values.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <param name="absoluteTolerence">The absolute tolerance. (default 1e-8)</param>
        /// <param name="relativeTolerence">The relative tolerance. (default 1e-5)</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> IsClose(NdArray<float> lhs, NdArray<float> rhs, float absoluteTolerence = 1e-8f, float relativeTolerence = 1e-5f) => ComparisonFunction<float>.IsClose(lhs, rhs, absoluteTolerence, relativeTolerence);

        /// <summary>
        /// Checks if two NdArrays have the same (within machine precision) values in all elements.
        /// The default absolute tolerance is 1e-8.
        /// The default relative tolerance is 1e-5.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>true if two NdArrays have same (within specified precision) values in all elements, otherwise false.</returns>
        public static bool AlmostEqual(NdArray<T> lhs, NdArray<T> rhs) => ComparisonFunction<T>.AlmostEqual(lhs, rhs);

        /// <summary>
        /// Checks if two NdArrays have the same (within machine precision) values in all elements.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <param name="absoluteTolerence">The absolute tolerance. (default 1e-8)</param>
        /// <param name="relativeTolerence">The relative tolerance. (default 1e-5)</param>
        /// <returns>true if two NdArrays have same (within specified precision) values in all elements, otherwise false.</returns>
        public static bool AlmostEqual(NdArray<double> lhs, NdArray<double> rhs, double absoluteTolerence = 1e-8, double relativeTolerence = 1e-5) => ComparisonFunction<double>.AlmostEqual(lhs, rhs, absoluteTolerence, relativeTolerence);

        /// <summary>
        /// Checks if two NdArrays have the same (within machine precision) values in all elements.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <param name="absoluteTolerence">The absolute tolerance. (default 1e-8)</param>
        /// <param name="relativeTolerence">The relative tolerance. (default 1e-5)</param>
        /// <returns>true if two NdArrays have same (within specified precision) values in all elements, otherwise false.</returns>
        public static bool AlmostEqual(NdArray<float> lhs, NdArray<float> rhs, float absoluteTolerence = 1e-8f, float relativeTolerence = 1e-5f) => ComparisonFunction<float>.AlmostEqual(lhs, rhs, absoluteTolerence, relativeTolerence);

        /// <summary>
        /// Flattens the NdArray into a (one-dimensional) vector.
        /// </summary>
        /// <param name="a">The NdArray to operate on.</param>
        /// <returns>A vector.</returns>
        public static NdArray<T1> Flattern<T1>(NdArray<T1> array)
        {
            return array.Reshape(new[] { SpecialIdx.Remainder });
        }

        public static NdArray<bool> FillAllAxis(NdArray<bool> target, int axis, NdArray<bool> src)
        {
            var (newSrc, _) = PrepareAxisReduceSources(target, axis, src, null);
            target.Backend.AllLastAxis(target, newSrc);
            return target;
        }

        public static NdArray<bool> AllAxis(int axis, NdArray<bool> array)
        {
            var (targt, src) = PrepareAxisReduceTarget<bool, bool>(axis, array);
            FillAllAxis(targt, axis, src);
            return targt;
        }

        public static NdArray<bool> AllNdArray(NdArray<bool> array)
        {
            var flattendArray = Flattern(array);
            return AllAxis(0, flattendArray);
        }

        public static bool All(NdArray<bool> src)
        {
            return AllNdArray(src).Value;
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
            if (newView is null)
            {
                var copy = Copy().ReshapeView(shp);
                return copy;
            }

            return newView;
        }

        public override bool Equals(object obj)
        {
            var array = obj as NdArray<T>;
            return !(array is null) &&
                   EqualityComparer<Type>.Default.Equals(DataType, array.DataType) &&
                   EqualityComparer<Layout>.Default.Equals(Layout, array.Layout) &&
                   EqualityComparer<IStorage<T>>.Default.Equals(Storage, array.Storage);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(DataType);
            hash.Add(Layout);
            hash.Add(Storage);
            return hash.ToHashCode();
        }

        /// <summary>
        /// Creates a new NdArray filled with equaly spaced values using a specifed increment.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="start">The starting value.</param>
        /// <param name="stop">The end value, which is not included.</param>
        /// <param name="step">The increment between successive element.</param>
        /// <typeparam name="T">The new NdArray.</typeparam>
        internal static NdArray<T> Arange(IDevice device, T start, T stop, T step) => Constructor<T>.Arange(device, start, stop, step);

        /// <summary>
        /// Creates a new NdArray filled with the integers from zero to the specified maximum.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="numElements">The number of elements of the new NdArray.</param>
        /// <returns>The new NdArray.</returns>
        internal static NdArray<T> Counting(IDevice device, int numElements) => Constructor<T>.Counting(device, numElements);

        /// <summary>
        /// Creates a new empty NdArray with the given number of dimensions.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="numDimension">The number of dimensions of the new, empty NdArray.</param>
        /// <returns>The new empty NdArray.</returns>
        internal static NdArray<T> Empty(IDevice device, int numDimension) => Constructor<T>.Empty(device, numDimension);

        /// <summary>
        /// Creates a new boolean NdArray filled with falses.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="shape">The shape of the new NdArray.</param>
        /// <returns>The new NdArray.</returns>
        internal static NdArray<bool> Falses(IDevice device, int[] shape) => Constructor<bool>.Falses(device, shape);

        /// <summary>
        /// Creates a new NdArray filled with the specified value.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="shape">The shape of the new NdArray.</param>
        /// <param name="value">The value to fill the new NdArray with.</param>
        /// <returns>The new NdArray.</returns>
        internal static NdArray<T> Filled(IDevice device, int[] shape, T value) => Constructor<T>.Filled(device, shape, value);

        /// <summary>
        /// Creates a new identity matrix.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="size">The size of the square identity matrix.</param>
        /// <returns>The new NdArray.</returns>
        internal static NdArray<T> Identity(IDevice device, int size) => Constructor<T>.Identity(device, size);

        /// <summary>
        /// Creates a new NdArray filled with ones (1).
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="shape">The shape of the new NdArray.</param>
        /// <returns>The new NdArray.</returns>
        internal static NdArray<T> Ones(IDevice device, int[] shape) => Constructor<T>.Ones(device, shape);

        /// <summary>
        /// Creates a new NdArray filled with ones using the specified NdArray as template.
        /// </summary>
        /// <param name="template">The template NdArray.</param>
        /// <returns>The new NdArray.</returns>
        internal static NdArray<T> OnesLike(NdArray<T> template) => Constructor<T>.OnesLike(template);

        /// <summary>
        /// Creates a new NdArray of given size filled with equaly spaced values.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="start">The starting value.</param>
        /// <param name="stop">The end value, which is not included.</param>
        /// <param name="numElement">The size of the vector.</param>
        /// <returns>The new NdArray.</returns>
        internal static NdArray<T> Linspace(IDevice device, T start, T stop, int numElement) => Constructor<T>.Linspace(device, start, stop, numElement);

        /// <summary>
        /// Creates a new zero-dimensional (scalar) NdArray with the specified value.
        /// </summary>
        /// <param name="dev">The device to create the NdArray on.</param>
        /// <param name="value">The value of the new, scalar NdArray.</param>
        /// <returns>The new NdArray.</returns>
        internal static NdArray<T> Scalar(IDevice device, T value) => Constructor<T>.Scalar(device, value);

        /// <summary>
        /// Creates a new zero-dimensional (scalar) NdArray using the specified NdArray as template and with
        /// the specified value.
        /// </summary>
        /// <param name="tmpl">template template NdArray.</param>
        /// <param name="value">The value of the new, scalar NdArray.</param>
        /// <returns>The new NdArray.</returns>
        internal static NdArray<T> ScalarLike(NdArray<T> array, T value) => Constructor<T>.ScalarLike(array, value);

        /// <summary>
        /// Creates a new boolean NdArray filled with trues.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="shape">The shape of the new NdArray.</param>
        /// <returns>The new NdArray.</returns>
        internal static NdArray<bool> Trues(IDevice device, int[] shape) => Constructor<T>.Trues(device, shape);

        /// <summary>
        /// Creates a new NdArray filled with zeros (0).
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="shape">The shape of the new NdArray.</param>
        /// <returns>The new NdArray.</returns>
        internal static NdArray<T> Zeros(IDevice device, int[] shape) => Constructor<T>.Zeros(device, shape);

        /// <summary>
        /// Creates a new NdArray filled with zeros using the specified NdArray as template.
        /// </summary>
        /// <param name="template">The template NdArray.</param>
        /// <returns>The new NdArray.</returns>
        internal static NdArray<T> ZerosLike(NdArray<T> template) => Constructor<T>.ZerosLike(template);

        /// <summary>
        /// Element-wise absolute value.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Abs(NdArray<T> input) => ElementWiseMathFunction<T>.FillAbs(input);

        /// <summary>
        /// Element-wise arccosine (inverse cosine).
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Acos(NdArray<T> input) => ElementWiseMathFunction<T>.FillAcos(input);

        /// <summary>
        /// Element-wise arcsine (inverse sine).
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Asin(NdArray<T> input) => ElementWiseMathFunction<T>.FillAsin(input);

        /// <summary>
        /// Element-wise arctanget (inverse tangent).
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Atan(NdArray<T> input) => ElementWiseMathFunction<T>.FillAtan(input);

        /// <summary>
        /// Element-wise ceiling (round towards positive infinity).
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Ceiling(NdArray<T> input) => ElementWiseMathFunction<T>.FillCeiling(input);

        /// <summary>
        /// Element-wise cosine.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Cos(NdArray<T> input) => ElementWiseMathFunction<T>.FillCos(input);

        /// <summary>
        /// Element-wise hyperbolic cosine.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Cosh(NdArray<T> input) => ElementWiseMathFunction<T>.FillCosh(input);

        /// <summary>
        /// Element-wise exponential function.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Exp(NdArray<T> input) => ElementWiseMathFunction<T>.FillExp(input);

        /// <summary>
        /// Element-wise floor (round towards negative infinity).
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Floor(NdArray<T> input) => ElementWiseMathFunction<T>.FillFloor(input);

        /// <summary>
        /// Element-wise natural logarithm.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Log(NdArray<T> input) => ElementWiseMathFunction<T>.FillLog(input);

        /// <summary>
        /// Element-wise common logarithm.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Log10(NdArray<T> input) => ElementWiseMathFunction<T>.FillLog10(input);

        /// <summary>
        /// Element-wise maximum.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Maximum(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseMathFunction<T>.FillMaximum(lhs, rhs);

        /// <summary>
        /// Element-wise minimum.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Minimum(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseMathFunction<T>.FillMinimum(lhs, rhs);

        /// <summary>
        /// Fills this NdArray with the element-wise exponentiation.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Pow(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseMathFunction<T>.FillPow(lhs, rhs);

        /// <summary>
        /// Element-wise rounding.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Round(NdArray<T> input) => ElementWiseMathFunction<T>.FillRound(input);

        /// <summary>
        /// Element-wise sign.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Sign(NdArray<T> input) => ElementWiseMathFunction<T>.FillSign(input);

        /// <summary>
        /// Element-wise sine.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Sin(NdArray<T> input) => ElementWiseMathFunction<T>.FillSin(input);

        /// <summary>
        /// Element-wise hyperbolic sine.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Sinh(NdArray<T> input) => ElementWiseMathFunction<T>.FillSinh(input);

        /// <summary>
        /// Element-wise square root.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Sqrt(NdArray<T> input) => ElementWiseMathFunction<T>.FillSqrt(input);

        /// <summary>
        /// Element-wise tangent.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Tan(NdArray<T> input) => ElementWiseMathFunction<T>.FillTan(input);

        /// <summary>
        /// Element-wise hyperbolic tangent.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Tanh(NdArray<T> input) => ElementWiseMathFunction<T>.FillTanh(input);

        /// <summary>
        /// Element-wise truncation (rounding towards zero).
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        internal static NdArray<T> Truncate(NdArray<T> input) => ElementWiseMathFunction<T>.FillTruncate(input);

        internal static NdArray<bool> FillAnyAxis(NdArray<bool> target, int axis, NdArray<bool> src)
        {
            var (newSrc, _) = PrepareAxisReduceSources(target, axis, src, null);
            target.Backend.AnyLastAxis(target, newSrc);
            return target;
        }

        internal static NdArray<T1> PermuteAxes<T1>(int[] permut, NdArray<T1> src)
        {
            var permutedLayout = Layout.PermuteAxes(permut, src.Layout);

            return src.Relayout(permutedLayout);
        }

        internal static NdArray<bool> AnyAxis(int axis, NdArray<bool> src)
        {
            var (target, src1) = PrepareAxisReduceTarget<bool, bool>(axis, src);
            FillAnyAxis(target, axis, src);
            return target;
        }

        internal static void CheckAxis<TA>(int axis, NdArray<TA> array)
        {
            Layout.CheckAxis(array.Layout, axis);
        }

        internal static (NdArray<TA>, NdArray<TR>) PrepareAxisReduceSources<TR, TA>(NdArray<TR> target, int axis, NdArray<TA> array, NdArray<TR> initial = null, Order order = Order.RowMajor)
        {
            // AssertSameStorage. Later. Note might need to support the different TR and TA types.
            CheckAxis(axis, array);

            var reducedShaped = List.Without(axis, array.Shape);
            if (!Enumerable.SequenceEqual(target.Shape, reducedShaped))
            {
                var msg = string.Format("Reduction of tensor {0} along axis {1} gives shape {2} but target has shape {3}.", array.Shape, axis, reducedShaped, target.Shape);
                throw new InvalidOperationException(msg);
            }

            if(!(initial is null))
            {
                AssertSameStorage(new[] { target, initial });
                BroadCastTo(reducedShaped, initial);
            }

            var axisToLastTemp = Enumerable.Range(0, array.NumDimensions).ToList();
            axisToLastTemp.RemoveAt(axis);
            axisToLastTemp.Add(axis);

            var axisToLast = axisToLastTemp.ToArray();
            var newArray = PermuteAxes(axisToLast, array);
            if (!Enumerable.SequenceEqual(target.Shape, newArray.Shape.Take(newArray.NumDimensions - 1)))
            {
                throw new InvalidOperationException("Internal axis reduce shape computation error.");
            }

            return (newArray, initial);
        }

        internal static (NdArray<TR>, NdArray<TA>) PrepareAxisReduceTarget<TR, TA>(int axis, NdArray<TA> array, Order order = Order.RowMajor)
        {
            CheckAxis(axis, array);
            var reducedShaped = List.Without(axis, array.Shape);
            var target = new NdArray<TR>(reducedShaped, array.Storage.Device, order);

            return (target, array);
        }

        internal static void AssertSameShape(NdArray<T> a, NdArray<T> b)
        {
            if (!Enumerable.SequenceEqual(a.Shape, b.Shape))
            {
                var msg = string.Format("NdArrays of shapes {0} and {1} were expected to have same shape", a.Shape, b.Shape);
                throw new ArgumentOutOfRangeException(msg);
            }
        }

        internal static void AssertSameStorage<T1>(NdArray<T1>[] arrays)
        {
            // skip this for now because of it supports only one storage type for now.
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
            var val = array.Value;
            if (StringCoverter.TryGetValue(val.GetType(), out Func<T, string> converter))
            {
                return converter(val);
            }

            return val.ToString();
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

        internal static NdArray<TA> PrepareElemwiseSources<TR, TA>(NdArray<TR> target, NdArray<TA> array)
        {
            // AssertSameStorage [later..]
            return BroadCastTo(target.Shape, array);
        }

        internal static (NdArray<TA>, NdArray<TB>) PrepareElemwiseSources<TR, TA, TB>(NdArray<TR> target, NdArray<TA> arrayA, NdArray<TB> arrayB)
        {
            // AssertSameStorage [later..]
            var arrA = BroadCastTo(target.Shape, arrayA);
            var arrB = BroadCastTo(target.Shape, arrayB);

            return (arrA, arrB);
        }

        internal NdArray<T> AssertBool()
        {
            if (DataType != typeof(bool))
            {
                var msg = string.Format("The operation requires a NdArray<bool> but the data type of the specified NdArray is {0}.", DataType);
                throw new InvalidOperationException(msg);
            }

            return this;
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