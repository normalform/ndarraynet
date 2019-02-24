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
    /// An N-dimensional array with elements of type T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NdArray<T> : IFrontend<T>
    {
        internal readonly ComparisonFunction<T> ComparisonFunction;

        private static readonly Dictionary<Type, Func<T, string>> StringCoverter = new Dictionary<Type, Func<T, string>>
        {
            { typeof(float), (T v) => { var val = Convert.ToSingle(v); return val >= 0.0f ? string.Format("{0,9:F4}", val) : string.Format("{0,9:F3}", val); } },
            { typeof(double), (T v) => { var val = Convert.ToDouble(v); return val >= 0.0f ? string.Format("{0,9:F4}", val) : string.Format("{0,9:F3}", val); } },
            { typeof(int), (T v) => { var val = Convert.ToInt32(v); return string.Format("{0,4:D}", val); } },
            { typeof(long), (T v) => { var val = Convert.ToUInt64(v); return string.Format("{0,4:D}", val); } },
            { typeof(byte), (T v) => { var val = Convert.ToByte(v); return string.Format("{0,3:D}", val); } },
            { typeof(bool), (T v) => { var val = Convert.ToBoolean(v); return val ? "true" : "false"; } },
        };

        private static readonly Lazy<IStaticMethod> StaticMethod = new Lazy<IStaticMethod>(() => new StaticMethod());

        /// <summary>
        /// Implicit constructor.
        /// </summary>
        /// <param name="layout"></param>
        internal NdArray(Layout layout, IStorage<T> storage)
        {
            Layout.Check(layout);

            Layout = layout;
            Storage = storage;

            ComparisonFunction = new ComparisonFunction<T>(this, storage.Backend(Layout));
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

            ComparisonFunction = new ComparisonFunction<T>(this, Storage.Backend(Layout));
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

        public static NdArray<T> BraodcastDim(int dim, int size, NdArray<T> target)
        {
            var layout = Layout.BraodcastDim(dim, size, target.Layout);
            return target.Relayout(layout);
        }

        public static NdArray<T> operator +(NdArray<T> source) => ElementWiseOperator<T>.UnaryPlus(source);

        public static NdArray<T> operator -(NdArray<T> source) => ElementWiseOperator<T>.UnaryMinus(source);

        public static NdArray<T> operator +(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseOperator<T>.Add(lhs, rhs);

        public static NdArray<T> operator +(NdArray<T> lhs, T rhs) => ElementWiseOperator<T>.Add(lhs, ScalarLike(lhs, rhs));

        public static NdArray<T> operator +(T lhs, NdArray<T> rhs) => ElementWiseOperator<T>.Add(ScalarLike(rhs, lhs), rhs);

        public static NdArray<T> operator -(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseOperator<T>.Subtract(lhs, rhs);

        public static NdArray<T> operator -(NdArray<T> lhs, T rhs) => ElementWiseOperator<T>.Subtract(lhs, ScalarLike(lhs, rhs));

        public static NdArray<T> operator -(T lhs, NdArray<T> rhs) => ElementWiseOperator<T>.Subtract(ScalarLike(rhs, lhs), rhs);

        public static NdArray<T> operator *(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseOperator<T>.Multiply(lhs, rhs);

        public static NdArray<T> operator *(NdArray<T> lhs, T rhs) => ElementWiseOperator<T>.Multiply(lhs, ScalarLike(lhs, rhs));

        public static NdArray<T> operator *(T lhs, NdArray<T> rhs) => ElementWiseOperator<T>.Multiply(ScalarLike(rhs, lhs), rhs);

        public static NdArray<T> operator /(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseOperator<T>.Divide(lhs, rhs);

        public static NdArray<T> operator /(NdArray<T> lhs, T rhs) => ElementWiseOperator<T>.Divide(lhs, ScalarLike(lhs, rhs));

        public static NdArray<T> operator /(T lhs, NdArray<T> rhs) => ElementWiseOperator<T>.Divide(ScalarLike(rhs, lhs), rhs);

        public static NdArray<T> operator %(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseOperator<T>.Modulo(lhs, rhs);

        public static NdArray<T> operator %(NdArray<T> lhs, T rhs) => ElementWiseOperator<T>.Modulo(lhs, ScalarLike(lhs, rhs));

        public static NdArray<T> operator %(T lhs, NdArray<T> rhs) => ElementWiseOperator<T>.Modulo(ScalarLike(rhs, lhs), rhs);

        public static NdArray<bool> operator ==(NdArray<T> lhs, NdArray<T> rhs) => ComparisonFunction<T>.Equal(lhs, rhs);

        public static NdArray<bool> operator ==(NdArray<T> lhs, T rhs) => ComparisonFunction<T>.Equal(lhs, ScalarLike(lhs, rhs));

        public static NdArray<bool> operator ==(T lhs, NdArray<T> rhs) => ComparisonFunction<T>.Equal(ScalarLike(rhs, lhs), rhs);

        public static NdArray<bool> operator !=(NdArray<T> lhs, NdArray<T> rhs) => ComparisonFunction<T>.NotEqual(lhs, rhs);

        public static NdArray<bool> operator !=(NdArray<T> lhs, T rhs) => ComparisonFunction<T>.NotEqual(lhs, ScalarLike(lhs, rhs));

        public static NdArray<bool> operator !=(T lhs, NdArray<T> rhs) => ComparisonFunction<T>.NotEqual(ScalarLike(rhs, lhs), rhs);

        public static NdArray<bool> operator <(NdArray<T> lhs, NdArray<T> rhs) => ComparisonFunction<T>.Less(lhs, rhs);

        public static NdArray<bool> operator <(NdArray<T> lhs, T rhs) => ComparisonFunction<T>.Less(lhs, ScalarLike(lhs, rhs));

        public static NdArray<bool> operator <(T lhs, NdArray<T> rhs) => ComparisonFunction<T>.Less(ScalarLike(rhs, lhs), rhs);

        public static NdArray<bool> operator <=(NdArray<T> lhs, NdArray<T> rhs) => ComparisonFunction<T>.LessOrEqual(lhs, rhs);

        public static NdArray<bool> operator <=(NdArray<T> lhs, T rhs) => ComparisonFunction<T>.LessOrEqual(lhs, ScalarLike(lhs, rhs));

        public static NdArray<bool> operator <=(T lhs, NdArray<T> rhs) => ComparisonFunction<T>.LessOrEqual(ScalarLike(rhs, lhs), rhs);

        public static NdArray<bool> operator >(NdArray<T> lhs, NdArray<T> rhs) => ComparisonFunction<T>.Greater(lhs, rhs);

        public static NdArray<bool> operator >(NdArray<T> lhs, T rhs) => ComparisonFunction<T>.Greater(lhs, ScalarLike(lhs, rhs));

        public static NdArray<bool> operator >(T lhs, NdArray<T> rhs) => ComparisonFunction<T>.Greater(ScalarLike(rhs, lhs), rhs);

        public static NdArray<bool> operator >=(NdArray<T> lhs, NdArray<T> rhs) => ComparisonFunction<T>.GreaterOrEqual(lhs, rhs);

        public static NdArray<bool> operator >=(NdArray<T> lhs, T rhs) => ComparisonFunction<T>.GreaterOrEqual(lhs, ScalarLike(lhs, rhs));

        public static NdArray<bool> operator >=(T lhs, NdArray<T> rhs) => ComparisonFunction<T>.GreaterOrEqual(ScalarLike(rhs, lhs), rhs);

        /// <summary>
        /// Element-wise logical negation.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> Not(NdArray<bool> source) => LogicalFunction<bool>.Negate(source);

        /// <summary>
        /// Element-wise loigcal and.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> And(NdArray<bool> lhs, NdArray<bool> rhs) => LogicalFunction<bool>.And(lhs, rhs);

        /// <summary>
        /// Element-wise loigcal or.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> Or(NdArray<bool> lhs, NdArray<bool> rhs) => LogicalFunction<bool>.Or(lhs, rhs);

        /// <summary>
        /// Element-wise loigcal xor.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> Xor(NdArray<bool> lhs, NdArray<bool> rhs) => LogicalFunction<bool>.Xor(lhs, rhs);

        /// <summary>
        /// Checks if all elements of the NdArray are true.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A scalar containing the result of this operation.</returns>
        public static bool All(NdArray<bool> source) => LogicalFunction<bool>.All(source);

        /// <summary>
        /// Checks if all elements of the NdArray are true returning the result as a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> AllNdArray(NdArray<bool> source) => LogicalFunction<bool>.AllNdArray(source);

        /// <summary>
        /// Checks if all elements along the specified axis are true.
        /// </summary>
        /// <param name="axis">The axis to check along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> AllAxis(int axis, NdArray<bool> source) => LogicalFunction<bool>.AllAxis(axis, source);

        /// <summary>
        /// Checks if any elements of the NdArray are true.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A scalar containing the result of this operation.</returns>
        public static bool Any(NdArray<bool> source) => LogicalFunction<bool>.Any(source);

        /// <summary>
        /// Checks if any element of the NdArray is true returning the result as a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> AnyNdArray(NdArray<bool> source) => LogicalFunction<bool>.AnyNdArray(source);

        /// <summary>
        /// Checks if any element along the specified axis is true.
        /// </summary>
        /// <param name="axis">The axis to check along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> AnyAxis(int axis, NdArray<bool> source) => LogicalFunction<bool>.AnyAxis(axis, source);

        /// <summary>Counts the elements being true.</summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A scalar containing the result of this operation.</returns>
        public static int CountTrue(NdArray<bool> source) => LogicalFunction<bool>.CountTrue(source);

        /// <summary>
        /// Counts the elements being true returning the result as a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<int> CountTrueNdArray(NdArray<bool> source) => LogicalFunction<bool>.CountTrueNdArray(source);

        /// <summary>
        /// Counts the elements being true along the specified axis.
        /// </summary>
        /// <param name="axis">The axis the count along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public static NdArray<int> CountTrueAxis(int axis, NdArray<bool> source) => LogicalFunction<bool>.CountTrueAxis(axis, source);

        /// <summary>
        /// Element-wise choice between two sources depending on a condition.
        /// </summary>
        /// <param name="cond">The condition NdArray.</param>
        /// <param name="ifTrue">The NdArray containing the values to use for when an element of the condition is true.</param>
        /// <param name="ifFalse">The NdArray containing the values to use for when an element of the condition is false.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> IfThenElse(NdArray<bool> condition, NdArray<T> ifTrue, NdArray<T> ifFalse) => LogicalFunction<T>.IfThenElse(condition, ifTrue, ifFalse);

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
        /// Element-wise finity check (not -Inf, Inf or NaN).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> IsFinite(NdArray<T> source) => ComparisonFunction<T>.IsFinite(source);

        /// <summary>
        /// Checks that all elements of the NdArray are finite.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>true if all elements are finite, otherwise false.</returns>
        public static bool AllFinite(NdArray<T> source) => All(ComparisonFunction<T>.IsFinite(source));

        /// <summary>
        /// Flattens the NdArray into a (one-dimensional) vector.
        /// </summary>
        /// <param name="a">The NdArray to operate on.</param>
        /// <returns>A vector.</returns>
        public static NdArray<T1> Flattern<T1>(NdArray<T1> array)
        {
            return array.Reshape(new[] { SpecialIdx.Remainder });
        }

        /// <summary>
        /// Creates a new NdArray filled with zeros using the specified NdArray as template.
        /// </summary>
        /// <param name="template">The template NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public static NdArray<T> ZerosLike(NdArray<T> template) => Constructor<T>.ZerosLike(template);

        /// <summary>
        /// Element-wise absolute value.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Abs(NdArray<T> source) => ElementWiseMathFunction<T>.Abs(source);

        /// <summary>
        /// Element-wise arccosine (inverse cosine).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Acos(NdArray<T> source) => ElementWiseMathFunction<T>.Acos(source);

        /// <summary>
        /// Element-wise arcsine (inverse sine).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Asin(NdArray<T> source) => ElementWiseMathFunction<T>.Asin(source);

        /// <summary>
        /// Element-wise arctanget (inverse tangent).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Atan(NdArray<T> source) => ElementWiseMathFunction<T>.Atan(source);

        /// <summary>
        /// Element-wise ceiling (round towards positive infinity).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Ceiling(NdArray<T> source) => ElementWiseMathFunction<T>.Ceiling(source);

        /// <summary>
        /// Element-wise cosine.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Cos(NdArray<T> source) => ElementWiseMathFunction<T>.Cos(source);

        /// <summary>
        /// Element-wise hyperbolic cosine.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Cosh(NdArray<T> source) => ElementWiseMathFunction<T>.Cosh(source);

        /// <summary>
        /// Element-wise exponential function.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Exp(NdArray<T> source) => ElementWiseMathFunction<T>.Exp(source);

        /// <summary>
        /// Element-wise floor (round towards negative infinity).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Floor(NdArray<T> source) => ElementWiseMathFunction<T>.Floor(source);

        /// <summary>
        /// Element-wise natural logarithm.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Log(NdArray<T> source) => ElementWiseMathFunction<T>.Log(source);

        /// <summary>
        /// Element-wise common logarithm.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Log10(NdArray<T> source) => ElementWiseMathFunction<T>.Log10(source);

        /// <summary>
        /// Element-wise maximum.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Maximum(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseMathFunction<T>.Maximum(lhs, rhs);

        /// <summary>
        /// Element-wise minimum.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Minimum(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseMathFunction<T>.Minimum(lhs, rhs);

        /// <summary>
        /// Fills this NdArray with the element-wise exponentiation.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Pow(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseMathFunction<T>.Pow(lhs, rhs);

        /// <summary>
        /// Element-wise rounding.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Round(NdArray<T> source) => ElementWiseMathFunction<T>.Round(source);

        /// <summary>
        /// Element-wise sign.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Sign(NdArray<T> source) => ElementWiseMathFunction<T>.Sign(source);

        /// <summary>
        /// Element-wise sine.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Sin(NdArray<T> source) => ElementWiseMathFunction<T>.Sin(source);

        /// <summary>
        /// Element-wise hyperbolic sine.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Sinh(NdArray<T> source) => ElementWiseMathFunction<T>.Sinh(source);

        /// <summary>
        /// Element-wise square root.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Sqrt(NdArray<T> source) => ElementWiseMathFunction<T>.Sqrt(source);

        /// <summary>
        /// Element-wise tangent.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Tan(NdArray<T> source) => ElementWiseMathFunction<T>.Tan(source);

        /// <summary>
        /// Element-wise hyperbolic tangent.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Tanh(NdArray<T> source) => ElementWiseMathFunction<T>.Tanh(source);

        /// <summary>
        /// Element-wise truncation (rounding towards zero).
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> Truncate(NdArray<T> source) => ElementWiseMathFunction<T>.Truncate(source);

        /// <summary>
        /// Calculates the maximum value of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the maximum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> MaxAxis(int axis, NdArray<T> source) => ReductionFunction<T>.MaxAxis(axis, source);

        /// <summary>
        /// Calculates the minimum value of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the minimum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> MinAxis(int axis, NdArray<T> source) => ReductionFunction<T>.MinAxis(axis, source);

        /// <summary>
        /// Calculates the maximum all elements returning a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<T> MaxNdArray(NdArray<T> source) => ReductionFunction<T>.MaxNdArray(source);

        /// <summary>
        /// Calculates the maximum of all elements.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A scalar containing the result of this operation.</returns>
        public static T Max(NdArray<T> source) => MaxNdArray(source).Value;

        /// <summary>
        /// Calculates the minimum all elements returning a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<T> MinNdArray(NdArray<T> source) => ReductionFunction<T>.MinNdArray(source);

        /// <summary>
        /// Calculates the minimum of all elements.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A scalar containing the result of this operation.</returns>
        public static T Min(NdArray<T> source) => MinNdArray(source).Value;

        /// <summary>
        /// Sums the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to sum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> SumAxis(int axis, NdArray<T> source) => ReductionFunction<T>.SumAxis(axis, source);

        /// <summary>
        /// Sums all elements returning a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<T> SumNdArray(NdArray<T> source) => ReductionFunction<T>.SumNdArray(source);

        /// <summary>
        /// Sums all elements.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new scalar the result of this operation.</returns>
        public static T Sum(NdArray<T> source) => SumNdArray(source).Value;

        /// <summary>
        /// Calculates the mean of the elements along the specified axis
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> MeanAxis(int axis, NdArray<T> source) => ReductionFunction<T>.MeanAxis(axis, source);

        /// <summary>
        /// Calculates the mean of the NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>The mean estimate.</returns>
        public static T Mean(NdArray<T> source) => ReductionFunction<T>.Mean(source);

        /// <summary>
        /// Calculates the product of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the product along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> ProductAxis(int axis, NdArray<T> source) => ReductionFunction<T>.ProductAxis(axis, source);

        /// <summary>
        /// Calculates the product all elements returning a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<T> ProductNdArray(NdArray<T> source) => ReductionFunction<T>.ProductNdArray(source);

        /// <summary>
        /// Calculates the product of all elements.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A scalar containing the result of this operation.</returns>
        public static T Product(NdArray<T> source) => ProductNdArray(source).Value;

        /// <summary>
        /// Calculates the variance of the elements along the specified axis.
        /// The default delta degrees of freedom is 0.
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> VarAxis(int axis, NdArray<T> source) => ReductionFunction<T>.VarAxis(axis, source);

        /// <summary>
        /// Calculates the variance of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <param name="ddof">The delta degrees of freedom.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> VarAxis(int axis, NdArray<T> source, T deltaDegreeOfFreedom) => ReductionFunction<T>.VarAxis(axis, source, deltaDegreeOfFreedom);

        /// <summary>
        /// Calculates the variance of the NdArray.
        /// The default delta degrees of freedom is 0.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>The variance estimate.</returns>
        public static T Var(NdArray<T> source) => VarAxis(0, Flattern(source)).Value;

        /// <summary>
        /// Calculates the variance of the NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <param name="ddof">The delta degrees of freedom.</param>
        /// <returns>The variance estimate.</returns>
        public static T Var(NdArray<T> source, T deltaDegreeOfFreedom) => VarAxis(0, Flattern(source), deltaDegreeOfFreedom).Value;

        /// <summary>
        /// Calculates the variance of the elements along the specified axis.
        /// The default delta degrees of freedom is 0.
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> StdAxis(int axis, NdArray<T> source) => ReductionFunction<T>.StdAxis(axis, source);

        /// <summary>
        /// Calculates the variance of the elements along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <param name="ddof">The delta degrees of freedom.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> StdAxis(int axis, NdArray<T> source, T deltaDegreeOfFreedom) => ReductionFunction<T>.StdAxis(axis, source, deltaDegreeOfFreedom);

        /// <summary>
        /// Calculates the variance of the NdArray.
        /// The default delta degrees of freedom is 0.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>The variance estimate.</returns>
        public static T Std(NdArray<T> source) => StdAxis(0, Flattern(source)).Value;

        /// <summary>
        /// Calculates the variance of the NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <param name="ddof">The delta degrees of freedom.</param>
        /// <returns>The variance estimate.</returns>
        public static T Std(NdArray<T> source, T deltaDegreeOfFreedom) => StdAxis(0, Flattern(source), deltaDegreeOfFreedom).Value;

        /// <summary>
        /// Calculates the trace along the specified axes.
        /// </summary>
        /// <param name="axis1">The first axis of the diagonal to compute the trace along.</param>
        /// <param name="axis2">The second axis of the diagonal to compute the trace along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> TraceAxis(int axis1, int axis2, NdArray<T> source) => ReductionFunction<T>.TraceAxis(axis1, axis2, source);

        /// <summary>
        /// Calculates the trace of the matrix.
        /// </summary>
        /// <param name="source">A square matrix.</param>
        /// <returns>The trace of the matrix.</returns>
        public static NdArray<T> Trace(NdArray<T> source) => ReductionFunction<T>.Trace(source);

        /// <summary>
        /// Returns a view of the diagonal along the given axes.
        /// </summary>
        /// <param name="ax1">The first dimension of the diagonal.</param>
        /// <param name="ax2">The seconds dimension of the diagonal.</param>
        /// <param name="array">The NdArray to operate on.</param>
        /// <returns>A NdArray where dimension <paramref name="ax1"/> is the diagonal and dimension
        public static NdArray<T> DiagAxis(int ax1, int ax2, NdArray<T> array) => NdArrayOperator<T>.DiagAxis(ax1, ax2, array);

        /// <summary>
        /// Returns a view of the diagonal of the NdArray.
        /// </summary>
        /// <param name="array">A square NdArray.</param>
        /// <returns>The diagonal NdArray.</returns>
        public static NdArray<T> Diag(NdArray<T> array) => NdArrayOperator<T>.Diag(array);

        /// <summary>
        /// Concatenates NdArrays along an axis.
        /// </summary>
        /// <param name="axis">The concatenation axis.</param>
        /// <param name="source">Sequence of NdArrays to concatenate.</param>
        /// <returns>The concatenated NdArray.</returns>
        public static NdArray<T> Concat(int axis, NdArray<T>[] source) => NdArrayOperator<T>.Concat(axis, source);

        /// <summary>Returns a copy of the NdArray.</summary>
        /// <param name="source">The NdArray to copy.</param>
        /// <param name="order">The memory layout of the copy. (default: row-major)</param>
        /// <returns>A copy of the NdArray.</returns>
        public static NdArray<T> Copy(NdArray<T> source, Order order = Order.RowMajor) => NdArrayOperator<T>.Copy(source, order);

        /// <summary
        /// >Creates a NdArray with the specified diagonal along the given axes.
        /// </summary>
        /// <param name="axis1">The first dimension of the diagonal.</param>
        /// <param name="axis2">The seconds dimension of the diagonal.</param>
        /// <param name="source">The values for the diagonal.</param>
        /// <returns>A NdArray having the values <paramref name="a"/> on the diagonal specified by the axes
        public static NdArray<T> DiagMatAxis(int axis1, int axis2, NdArray<T> source) => NdArrayOperator<T>.DiagMatAxis(axis1, axis2, source);

        /// <summary>
        /// Creates a matrix with the specified diagonal.
        /// </summary>
        /// <param name="source">The vector containing the values for the diagonal.</param>
        /// <returns>A matrix having the values <paramref name="source"/> on its diagonal.</returns>
        public static NdArray<T> DiagMat(NdArray<T> source) => NdArrayOperator<T>.DiagMat(source);

        /// <summary>
        /// Calculates the difference between adjoining elements along the specified axes.
        /// </summary>
        /// <param name="axis">The axis to operate along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>The differences NdArray. It has one element less in dimension <paramref name="axis"/> as the source NdArray.</returns>
        public static NdArray<T> DiffAxis(int axis, NdArray<T> source) => NdArrayOperator<T>.DiffAxis(axis, source);

        /// <summary>
        /// Calculates the difference between adjoining elements of the vector.
        /// </summary>
        /// <param name="source">The vector containing the source values.</param>
        /// <returns>The differences vector. It has one element less than the source NdArray.</returns>
        public static NdArray<T> Diff(NdArray<T> source) => NdArrayOperator<T>.Diff(source);

        /// <summary>
        /// Transpose of a matrix.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The result of this operation.</returns>
        public static NdArray<T> Transpos(NdArray<T> source) => NdArrayOperator<T>.Transpos(source);

        /// <summary>
        /// Pads the NdArray from the left with size-one dimensions until it has at least the specified number of
        /// dimensions.
        /// </summary>
        /// <param name="minNumDim">The minimum number of dimensions.</param>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>A NdArray with at least <paramref name="minNumDim"/> dimensions.</returns>
        public static NdArray<T> AtLeastNd(int minNumDim, NdArray<T> source) => ShapeFunction<T>.AtLeastNd(minNumDim, source);

        /// <summary>
        /// Pads the NdArray from the left with size-one dimensions until it has at least one dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>A NdArray with at least one dimensions.</returns>
        public static NdArray<T> AtLeast1d(NdArray<T> source) => ShapeFunction<T>.AtLeast1d(source);

        /// <summary>
        /// Pads the NdArray from the left with size-two dimensions until it has at least two dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>A NdArray with at least two dimensions.</returns>
        public static NdArray<T> AtLeast2d(NdArray<T> source) => ShapeFunction<T>.AtLeast2d(source);

        /// <summary>
        /// Pads the NdArray from the left with size-three dimensions until it has at least three dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>A NdArray with at least three dimensions.</returns>
        public static NdArray<T> AtLeast3d(NdArray<T> source) => ShapeFunction<T>.AtLeast3d(source);

        /// <summary>
        /// Broadcast a dimension to a specified size.
        /// </summary>
        /// <param name="dim">The size-one dimension to broadcast.</param>
        /// <param name="size">The size to broadcast to.</param>
        /// <param name="a">The NdArray to operate on.</param>
        /// <returns>The resulting NdArray.</returns>
        public static NdArray<T> BroadCastDim(int dim, int size, NdArray<T> source) => ShapeFunction<T>.BroadCastDim(dim, size, source);

        /// <summary>
        /// Broadcasts the specified NdArray to the specified shape.
        /// </summary>
        /// <param name="shp">The target shape.</param>
        /// <param name="frontend">The NdArray to operate on.</param>
        /// <returns>NdArray of shape <paramref name="shp"/>.</returns>
        public static NdArray<TA> BroadCastTo<TA>(int[] shp, IFrontend<TA> frontend) => ShapeFunction<TA>.BroadCastTo(shp, frontend);

        /// <summary>
        /// Broadcasts all specified NdArrays to have the same shape.
        /// </summary>
        /// <param name="src1">The NdArray to operate on.</param>
        /// <param name="src2">The NdArray to operate on.</param>
        /// <returns>A tuple of the resulting NdArrays, all having the same shape.</returns>
        public static (NdArray<T1>, NdArray<T2>) BroadCastToSame<T1, T2>(IFrontend<T1> src1, IFrontend<T2> src2) => ShapeFunction<T>.BroadCastToSame(src1, src2);

        /// <summary>
        /// Broadcasts all specified NdArrays to have the same shape.
        /// </summary>
        /// <param name="src1">The NdArray to operate on.</param>
        /// <param name="src2">The NdArray to operate on.</param>
        /// <param name="src3">The NdArray to operate on.</param>
        /// <returns>A tuple of the resulting NdArrays, all having the same shape.</returns>
        public static (NdArray<T1>, NdArray<T2>, NdArray<T3>) BroadCastToSame<T1, T2, T3>(NdArray<T1> src1, NdArray<T2> src2, NdArray<T3> src3) => ShapeFunction<T>.BroadCastToSame(src1, src2, src3);

        /// <summary>
        /// Broadcasts all specified NdArrays to have the same shape.
        /// </summary>
        /// <param name="src1">The NdArray to operate on.</param>
        /// <param name="src2">The NdArray to operate on.</param>
        /// <param name="src3">The NdArray to operate on.</param>
        /// <returns>A tuple of the resulting NdArrays, all having the same shape.</returns>
        public static NdArray<T>[] BroadCastToSame(NdArray<T>[] src) => ShapeFunction<T>.BroadCastToSame(src);

        /// <summary>
        /// Broadcasts all specified NdArrays to have the same size in the specified dimensions.
        /// </summary>
        /// <param name="dims">A list of dimensions that should be broadcasted to have the same size.</param>
        /// <param name="src1">The NdArray to operate on.</param>
        /// <param name="src2">The NdArray to operate on.</param>
        /// <returns>A tuple of the resulting NdArrays, all having the same size in the specified dimensions.</returns>
        public static (NdArray<T1>, NdArray<T2>) BroadCastToSameInDims<T1, T2>(int[] dims, NdArray<T1> src1, NdArray<T2> src2) => ShapeFunction<T>.BroadCastToSameInDims(dims, src1, src2);

        /// <summary>
        /// Broadcasts all specified NdArrays to have the same size in the specified dimensions.
        /// </summary>
        /// <param name="dims">A list of dimensions that should be broadcasted to have the same size.</param>
        /// <param name="src1">The NdArray to operate on.</param>
        /// <param name="src2">The NdArray to operate on.</param>
        /// <param name="src3">The NdArray to operate on.</param>
        /// <returns>A tuple of the resulting NdArrays, all having the same size in the specified dimensions.</returns>
        public static (NdArray<T1>, NdArray<T2>, NdArray<T3>) BroadCastToSameInDims<T1, T2, T3>(int[] dims, NdArray<T1> src1, NdArray<T2> src2, NdArray<T3> src3) => ShapeFunction<T>.BroadCastToSameInDims(dims, src1, src2, src3);

        /// <summary>
        /// Broadcasts all specified NdArrays to have the same size in the specified dimensions.
        /// </summary>
        /// <param name="dims">A list of dimensions that should be broadcasted to have the same size.</param>
        /// <param name="src">A list of NdArrays to operate on.</param>
        /// <returns>A list of the resulting NdArrays, all having the same size in the specified dimensions.</returns>
        public static NdArray<T>[] BroadCastToSameInDims(int[] dims, NdArray<T>[] src) => ShapeFunction<T>.BroadCastToSameInDims(dims, src);

        /// <summary>
        /// Removes the first dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The resulting NdArray.</returns>
        public static NdArray<T> CutLeft(NdArray<T> source) => ShapeFunction<T>.CutLeft(source);

        /// <summary>
        /// Removes the last dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The resulting NdArray.</returns>
        public static NdArray<T> CutRight(NdArray<T> source) => ShapeFunction<T>.CutRight(source);

        /// <summary>
        /// Flattens the NdArray into a (one-dimensional) vector.
        /// </summary>
        /// <param name="a">The NdArray to operate on.</param>
        /// <returns>A vector.</returns>
        public static NdArray<T> Flatten(NdArray<T> source) => ShapeFunction<T>.Flatten(source);

        /// <summary>
        /// Insert a dimension of size one before the specifed dimension.
        /// </summary>
        /// <param name="axis">The dimension to insert before.</param>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The resulting NdArray.</returns>
        public static NdArray<T> InsertAxis(int axis, NdArray<T> source) => ShapeFunction<T>.InsertAxis(axis, source);

        /// <summary>
        /// Checks if the specified NdArray is broadcasted in at least one dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>true if at least one dimension is broadcasted, otherwise false.</returns>
        public static bool IsBroadcasted(NdArray<T> source) => ShapeFunction<T>.IsBroadcasted(source);

        /// <summary>
        /// Insert a dimension of size one as the first dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The resulting NdArray.</returns>
        public static NdArray<T> PadLeft(NdArray<T> source) => ShapeFunction<T>.PadLeft(source);

        /// <summary>
        /// Append a dimension of size one after the last dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The resulting NdArray.</returns>
        public static NdArray<T> PadRight(NdArray<T> source) => ShapeFunction<T>.PadRight(source);

        /// <summary>
        /// Pads all specified NdArrays to have the same shape.
        /// </summary>
        /// <param name="src1">The NdArray to operate on.</param>
        /// <param name="src2">The NdArray to operate on.</param>
        /// <returns>A tuple of the resulting NdArrays, all having the same shape.</returns>
        public static (NdArray<T1>, NdArray<T2>) PadToSame<T1, T2>(NdArray<T1> src1, NdArray<T2> src2) => ShapeFunction<T>.PadToSame(src1, src2);

        /// <summary>
        /// Pads all specified NdArrays to have the same shape.
        /// </summary>
        /// <param name="src1">The NdArray to operate on.</param>
        /// <param name="src2">The NdArray to operate on.</param>
        /// <param name="src3">The NdArray to operate on.</param>
        /// <returns>A tuple of the resulting NdArrays, all having the same shape.</returns>
        public static (NdArray<T1>, NdArray<T2>, NdArray<T3>) PadToSame<T1, T2, T3>(NdArray<T1> src1, NdArray<T2> src2, NdArray<T3> src3) => ShapeFunction<T>.PadToSame(src1, src2, src3);

        /// <summary>
        /// Pads all specified NdArrays to have the same shape.
        /// </summary>
        /// <param name="src">A list of NdArrays to operate on.</param>
        /// <returns>A list of the resulting NdArrays, all having the same shape.</returns>
        public static NdArray<T>[] PadToSame(NdArray<T>[] src) => ShapeFunction<T>.PadToSame(src);

        /// <summary>
        /// Permutes the axes as specified.
        /// </summary>
        /// <param name="permut">The permutation to apply to the dimensions of NdArray.</param>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The NdArray with the dimensions permuted.</returns>
        public static NdArray<T> PermuteAxes(int[] permut, NdArray<T> source) => StaticMethod.Value.PermuteAxes(permut, source);

        /// <summary>
        /// Reverses the elements in the specified dimension.
        /// </summary>
        /// <param name="axis">The axis to reverse.</param>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The NdArray with the dimensions Reversed.</returns>
        public static NdArray<T> ReverseAxis(int axis, NdArray<T> source) => ShapeFunction<T>.ReverseAxis(axis, source);

        /// <summary>
        /// Swaps the specified dimensions of the NdArray.
        /// </summary>
        /// <param name="axis1">The dimension to swap.</param>
        /// <param name="axis2">The dimension to swap with.</param>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The NdArray with the dimensions swapped.</returns>
        public static NdArray<T> SwapDim(int axis1, int axis2, NdArray<T> source) => ShapeFunction<T>.SwapDim(axis1, axis2, source);

        /// <summary>
        /// Gets a sequence of all indices to enumerate all elements within the NdArray.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>Sequence of indicies.</returns>
        public static int[][] AllIndex(NdArray<T> source) => IndexFunction<T>.AllIndex(source);

        /// <summary>
        /// Gets a sequence of all all elements within the NdArray.</summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>Sequence of elements.</returns>
        public static T[] AllElements(NdArray<T> source) => IndexFunction<T>.AllElements(source);

        /// <summary>
        /// Finds the index of the maximum value along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the maximum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<int> ArgMaxAxis(int axis, NdArray<T> source) => IndexFunction<T>.ArgMaxAxis(axis, source);

        /// <summary>
        /// Finds the indicies of the maximum value of the NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>The indices of the position of the maximum value.</returns>
        public static int[] ArgMax(NdArray<T> source) => IndexFunction<T>.ArgMax(source);

        /// <summary>
        /// Finds the index of the minimum value along the specified axis.
        /// </summary>
        /// <param name="axis">The axis to calculate the minimum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<int> ArgMinAxis(int axis, NdArray<T> source) => IndexFunction<T>.ArgMinAxis(axis, source);

        /// <summary>
        /// Finds the indicies of the minimum value of the NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>The indices of the position of the minimum value.</returns>
        public static int[] ArgMin(NdArray<T> source) => IndexFunction<T>.ArgMin(source);

        /// <summary>
        /// Finds the first occurence of the specfied value along the specified axis and returns its index.
        /// </summary>
        /// <param name="value">The value to find.</param>
        /// <param name="axis">The axis to find the value along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the indices of the first occurence of <paramref name="value"/>.</returns>
        public static NdArray<int> FindAxis(T value, int axis, NdArray<T> source) => IndexFunction<T>.FindAxis(value, axis, source);

        /// <summary>
        /// Finds the first occurence of the specfied value along the specified axis and returns its index.
        /// </summary>
        /// <param name="value">The value to find.</param>
        /// <param name="axis">The axis to find the value along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the indices of the first occurence of <paramref name="value"/>.</returns>
        public static int[] TryFind(T value, NdArray<T> source) => IndexFunction<T>.TryFind(value, source);

        /// <summary>
        /// Finds the first occurence of the specfied value and returns its indices.
        /// </summary>
        /// <param name="value">The value to find.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>The indices of the value.</returns>
        public static int[] Find(T value, NdArray<T> source) => IndexFunction<T>.Find(value, source);

        public void FillUnaryPlus(NdArray<T> source) => ElementWiseOperator<T>.FillUnaryPlus(this, source);

        public void FillUnaryMinum(NdArray<T> source) => ElementWiseOperator<T>.FillUnaryMinus(this, source);

        public void FillAdd(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseOperator<T>.FillAdd(this, lhs, rhs);

        public void FillAdd(NdArray<T> lhs, T rhs) => ElementWiseOperator<T>.FillAdd(this, lhs, ScalarLike(lhs, rhs));

        public void FillAdd(T lhs, NdArray<T> rhs) => ElementWiseOperator<T>.FillAdd(this, ScalarLike(rhs, lhs), rhs);

        public void FillSubtract(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseOperator<T>.FillSubtract(this, lhs, rhs);

        public void FillSubtract(NdArray<T> lhs, T rhs) => ElementWiseOperator<T>.FillSubtract(this, lhs, ScalarLike(lhs, rhs));

        public void FillSubtract(T lhs, NdArray<T> rhs) => ElementWiseOperator<T>.FillSubtract(this, ScalarLike(rhs, lhs), rhs);

        public void FillMultiply(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseOperator<T>.FillMultiply(this, lhs, rhs);

        public void FillMultiply(NdArray<T> lhs, T rhs) => ElementWiseOperator<T>.FillMultiply(this, lhs, ScalarLike(lhs, rhs));

        public void FillMultiply(T lhs, NdArray<T> rhs) => ElementWiseOperator<T>.FillMultiply(this, ScalarLike(rhs, lhs), rhs);

        public void FillDivide(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseOperator<T>.FillDivide(this, lhs, rhs);

        public void FillDivide(NdArray<T> lhs, T rhs) => ElementWiseOperator<T>.FillDivide(this, lhs, ScalarLike(lhs, rhs));

        public void FillDivide(T lhs, NdArray<T> rhs) => ElementWiseOperator<T>.FillDivide(this, ScalarLike(rhs, lhs), rhs);

        public void FillModulo(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseOperator<T>.FillModulo(this, lhs, rhs);

        public void FillModulo(NdArray<T> lhs, T rhs) => ElementWiseOperator<T>.FillModulo(this, lhs, ScalarLike(lhs, rhs));

        public void FillModulo(T lhs, NdArray<T> rhs) => ElementWiseOperator<T>.FillModulo(this, ScalarLike(rhs, lhs), rhs);

        public void FillEqual<T1>(IFrontend<T1> lhs, IFrontend<T1> rhs) => ComparisonFunction.FillEqual(lhs, rhs);

        public void FillEqual(NdArray<T> lhs, T rhs) => ComparisonFunction.FillEqual(lhs, ScalarLike(lhs, rhs));

        public void FillEqual(T lhs, NdArray<T> rhs) => ComparisonFunction.FillEqual(ScalarLike(rhs, lhs), rhs);

        public void FillNotEqual(NdArray<T> lhs, NdArray<T> rhs) => ComparisonFunction.FillNotEqual(lhs, rhs);

        public void FillNotEqual(NdArray<T> lhs, T rhs) => ComparisonFunction.FillNotEqual(lhs, ScalarLike(lhs, rhs));

        public void FillNotEqual(T lhs, NdArray<T> rhs) => ComparisonFunction.FillNotEqual(ScalarLike(rhs, lhs), rhs);

        public void FillLess(NdArray<T> lhs, NdArray<T> rhs) => ComparisonFunction<bool>.FillLess((dynamic)this, lhs, rhs);

        public void FillLess(NdArray<T> lhs, T rhs) => ComparisonFunction<bool>.FillLess((dynamic)this, lhs, ScalarLike(lhs, rhs));

        public void FillLess(T lhs, NdArray<T> rhs) => ComparisonFunction<bool>.FillLess((dynamic)this, ScalarLike(rhs, lhs), rhs);

        public void FillLessOrEqual(NdArray<T> lhs, NdArray<T> rhs) => ComparisonFunction<T>.FillLessOrEqual((dynamic)this, lhs, rhs);

        public void FillLessOrEqual(NdArray<T> lhs, T rhs) => ComparisonFunction<T>.FillLessOrEqual((dynamic)this, lhs, ScalarLike(lhs, rhs));

        public void FillLessOrEqual(T lhs, NdArray<T> rhs) => ComparisonFunction<T>.FillLessOrEqual((dynamic)this, ScalarLike(rhs, lhs), rhs);

        public void FillGreater(NdArray<T> lhs, NdArray<T> rhs) => ComparisonFunction<T>.FillGreater((dynamic)this, lhs, rhs);

        public void FillGreater(NdArray<T> lhs, T rhs) => ComparisonFunction<T>.FillGreater((dynamic)this, lhs, ScalarLike(lhs, rhs));

        public void FillGreater(T lhs, NdArray<T> rhs) => ComparisonFunction<T>.FillGreater((dynamic)this, ScalarLike(rhs, lhs), rhs);

        public void FillGreaterOrEqual(NdArray<T> lhs, NdArray<T> rhs) => ComparisonFunction<T>.FillGreaterOrEqual((dynamic)this, lhs, rhs);

        public void FillGreaterOrEqual(NdArray<T> lhs, T rhs) => ComparisonFunction<T>.FillGreaterOrEqual((dynamic)this, lhs, ScalarLike(lhs, rhs));

        public void FillGreaterOrEqual(T lhs, NdArray<T> rhs) => ComparisonFunction<T>.FillGreaterOrEqual((dynamic)this, ScalarLike(rhs, lhs), rhs);

        /// <summary>
        /// Counts the elements being true along the specified axis and writes the result into this NdArray.
        /// </summary>
        /// <param name="axis">The axis the count along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public void FillCountTrueAxis(int axis, NdArray<bool> source) => LogicalFunction<bool>.FillCountTrueAxis((dynamic)this, axis, source);

        /// <summary>
        /// Fills this NdArray with the element-wise logical negation of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillNot(NdArray<bool> source) => LogicalFunction<bool>.FillNegate((dynamic)this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise logical and of the arguments.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        public void FillAnd(NdArray<bool> lhs, NdArray<bool> rhs) => LogicalFunction<bool>.FillAnd((dynamic)this, lhs, rhs);

        /// <summary>
        /// Fills this NdArray with the element-wise logical or of the arguments.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        public void FillOr(NdArray<bool> lhs, NdArray<bool> rhs) => LogicalFunction<bool>.FillOr((dynamic)this, lhs, rhs);

        /// <summary>
        /// Fills this NdArray with the element-wise logical xor of the arguments.
        /// </summary>
        /// <param name="lhs">The FillXor on the left side of this binary operation.</param>
        /// <param name="rhs">The FillXor on the right side of this binary operation.</param>
        public void FillXor(NdArray<bool> lhs, NdArray<bool> rhs) => LogicalFunction<bool>.FillXor((dynamic)this, lhs, rhs);

        /// <summary>
        /// Checks if all elements along the specified axis are true using this NdArray as target.
        /// </summary>
        /// <param name="axis">The axis to check along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public void FillAllAxis(int axis, NdArray<bool> source) => LogicalFunction<bool>.FillAllAxis((dynamic)this, axis, source);

        /// <summary>
        /// Checks if any element along the specified axis is true using this NdArray as target.
        /// </summary>
        /// <param name="axis">The axis to check along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public void FillAnyAxis(int axis, NdArray<bool> source) => LogicalFunction<bool>.FillAnyAxis((dynamic)this, axis, source);

        /// <summary>
        /// Fills this NdArray with the element-wise finity check (not -Inf, Inf or NaN) of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillIsFinite<TP>(NdArray<TP> source) => ComparisonFunction<bool>.FillIsFinite((dynamic)this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise absolute value of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillAbs(NdArray<T> source) => ElementWiseMathFunction<T>.FillAbs(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise arccosine (inverse cosine) of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillAcos(NdArray<T> source) => ElementWiseMathFunction<T>.FillAcos(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise arcsine (inverse sine) of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillAsin(NdArray<T> source) => ElementWiseMathFunction<T>.FillAsin(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise arctanget (inverse tangent) of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillAtan(NdArray<T> source) => ElementWiseMathFunction<T>.FillAtan(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise ceiling (round towards positive infinity) of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillCeiling(NdArray<T> source) => ElementWiseMathFunction<T>.FillCeiling(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise cosine of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillCos(NdArray<T> source) => ElementWiseMathFunction<T>.FillCos(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise hyperbolic cosine of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillCosh(NdArray<T> source) => ElementWiseMathFunction<T>.FillCosh(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise exponential function of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillExp(NdArray<T> source) => ElementWiseMathFunction<T>.FillExp(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise floor (round towards negative infinity) of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillFloor(NdArray<T> source) => ElementWiseMathFunction<T>.FillFloor(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise natural logarithm of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillLog(NdArray<T> source) => ElementWiseMathFunction<T>.FillLog(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise common logarithm of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillLog10(NdArray<T> source) => ElementWiseMathFunction<T>.FillLog10(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise maximum of the arguments.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        public void FillMaximum(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseMathFunction<T>.FillMaximum(this, lhs, rhs);

        /// <summary>
        /// Fills this NdArray with the element-wise minimum of the arguments.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        public void FillMinimum(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseMathFunction<T>.FillMinimum(this, lhs, rhs);

        /// <summary>
        /// Fills this NdArray with the element-wise exponentiation.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        public void FillPow(NdArray<T> lhs, NdArray<T> rhs) => ElementWiseMathFunction<T>.FillPow(this, lhs, rhs);

        /// <summary>
        /// Fills this NdArray with the element-wise rounding of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillRound(NdArray<T> source) => ElementWiseMathFunction<T>.FillRound(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise sign of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillSign(NdArray<T> source) => ElementWiseMathFunction<T>.FillSign(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise sine of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillSin(NdArray<T> source) => ElementWiseMathFunction<T>.FillSin(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise hyperbolic sine of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillSinh(NdArray<T> source) => ElementWiseMathFunction<T>.FillSinh(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise square root of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillSqrt(NdArray<T> source) => ElementWiseMathFunction<T>.FillSqrt(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise tangent of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillTan(NdArray<T> source) => ElementWiseMathFunction<T>.FillTan(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise hyperbolic tangent of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillTanh(NdArray<T> source) => ElementWiseMathFunction<T>.FillTanh(this, source);

        /// <summary>
        /// Fills this NdArray with the element-wise truncation (rounding towards zero) of the argument.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        public void FillTruncate(NdArray<T> source) => ElementWiseMathFunction<T>.FillTruncate(this, source);

        /// <summary>
        /// Calculates the maximum value of the elements over the specified axis and writes the result into this NdArray.
        /// </summary>
        /// <param name="axis">The axis to calculate the maximum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public void FillMaxAxis(int axis, NdArray<T> source) => ReductionFunction<T>.FillMaxAxis(this, axis, source);

        /// <summary>
        /// Calculates the minimum value of the elements over the specified axis and writes the result into this NdArray.
        /// </summary>
        /// <param name="axis">The axis to calculate the minimum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public void FillMinAxis(int axis, NdArray<T> source) => ReductionFunction<T>.FillMinAxis(this, axis, source);

        /// <summary>
        /// Sums the elements over the specified axis and writes the result into this NdArray.
        /// </summary>
        /// <param name="axis">The axis to sum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public void FillSumAxis(int axis, NdArray<T> source) => ReductionFunction<T>.FillSumAxis(this, axis, source);

        /// <summary>
        /// Calculates the product of the elements over the specified axis and writes the result into this NdArray.
        /// </summary>
        /// <param name="axis">The axis to calculate the product along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public void FillProductAxis(int axis, NdArray<T> source) => ReductionFunction<T>.FillProductAxis(this, axis, source);

        /// <summary>
        /// Finds the index of the maximum value along the specified axis and writes it into this NdArray.
        /// </summary>
        /// <param name="axis">The axis to calculate the maximum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public void FillArgMaxAxis(int axis, NdArray<T> source) => IndexFunction<T>.FillArgMaxAxis((dynamic)this, axis, source);

        /// <summary>
        /// Finds the index of the minimum value along the specified axis and writes it into this NdArray.
        /// </summary>
        /// <param name="axis">The axis to calculate the minimum along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public void FillArgMinAxis(int axis, NdArray<T> source) => IndexFunction<T>.FillArgMinAxis((dynamic)this, axis, source);

        /// <summary>
        /// Finds the first occurence of the specfied value along the specified axis and write its index into this NdArray.
        /// </summary>
        /// <param name="value">The value to find.</param>
        /// <param name="axis">The axis to find the value along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public void FillFindAxis(T value, int axis, NdArray<T> source) => IndexFunction<T>.FillFindAxis((dynamic)this, value, axis, source);

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

        public (IFrontend<T1>, IFrontend<T2>) PrepareElemwiseSources<T1, T2>(IFrontend<T1> arrayA, IFrontend<T2> arrayB)
        {
            // AssertSameStorage [later..]
            var arrA = BroadCastTo(Shape, arrayA);
            var arrB = BroadCastTo(Shape, arrayB);

            return (arrA, arrB);
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
            unchecked
            {
                var hashCode = 397 ^ DataType.GetHashCode();
                hashCode = (hashCode * 397) ^ Layout.GetHashCode();
                hashCode = (hashCode * 397) ^ Storage.GetHashCode();
                return hashCode;
            }
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
        internal static NdArray<TP> ScalarLike<TP>(NdArray<TP> array, TP value) => Constructor<TP>.ScalarLike(array, value);

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

        internal static NdArray<T1> PermuteAxes<T1>(int[] permut, NdArray<T1> src)
        {
            var permutedLayout = Layout.PermuteAxes(permut, src.Layout);

            return src.Relayout(permutedLayout);
        }

        internal static void CheckAxis<TA>(int axis, NdArray<TA> array)
        {
            Layout.CheckAxis(array.Layout, axis);
        }

        internal static void AssertSameShape(NdArray<T> a, NdArray<T> b)
        {
            if (!Enumerable.SequenceEqual(a.Shape, b.Shape))
            {
                var errorMessage = string.Format("NdArrays of shapes {0} and {1} were expected to have same shape", ErrorMessage.ShapeToString(a.Shape), ErrorMessage.ShapeToString(b.Shape));
                throw new ArgumentOutOfRangeException(errorMessage);
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

        internal NdArray<T> AssertBool()
        {
            if (DataType != typeof(bool))
            {
                var errorMessage = string.Format("The operation requires a NdArray<bool> but the data type of the specified NdArray is {0}.", DataType);
                throw new InvalidOperationException(errorMessage);
            }

            return this;
        }

        internal NdArray<T> Copy(Order order = Order.RowMajor)
        {
            return Copy(StaticMethod.Value, order);
        }

        internal NdArray<T> Copy(IStaticMethod staticMethod, Order order)
        {
            var (target, src) = staticMethod.PrepareElemwise<T, T>(this, order);
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
                var errorMessage = string.Format("Cannot reshape NdArray of shape {0} and strides {1} without copying.", ErrorMessage.ShapeToString(Shape), ErrorMessage.ArrayToString(Layout.Stride));
                throw new InvalidOperationException(errorMessage);
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
                var errorMessage = string.Format("This operation requires a scalar (0-dimensional) NdArray, but its shape is {0}", ErrorMessage.ShapeToString(Shape));
                throw new InvalidOperationException(errorMessage);
            }
        }

        internal NdArray<T> AssertInt()
        {
            if (DataType != typeof(int))
            {
                var errorMessage = string.Format("The operation requires a NdArray<bool> but the data type of the specified NdArray is {0}.", DataType);
                throw new InvalidOperationException(errorMessage);
            }

            return this;
        }
    }
}