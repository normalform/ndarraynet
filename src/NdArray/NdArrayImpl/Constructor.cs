// <copyright file="Constructor.cs" company="NdArrayNet">
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

namespace NdArray.NdArrayImpl
{
    using System;
    using NdArrayNet;

    internal static class Constructor<T>
    {
        /// <summary>
        /// Creates a new NdArray filled with equaly spaced values using a specifed increment.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="start">The starting value.</param>
        /// <param name="stop">The end value, which is not included.</param>
        /// <param name="step">The increment between successive element.</param>
        /// <typeparam name="T">The new NdArray.</typeparam>
        public static NdArray<T> Arange(IDevice device, T start, T stop, T step)
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

        /// <summary>
        /// Creates a new NdArray filled with the integers from zero to the specified maximum.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="numElements">The number of elements of the new NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public static NdArray<T> Counting(IDevice device, int numElements)
        {
            var newArray = new NdArray<T>(new[] { numElements }, device);
            newArray.FillIncrementing(Primitives.Zero<T>(), Primitives.One<T>());

            return newArray;
        }

        /// <summary>
        /// Creates a new empty NdArray with the given number of dimensions.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="numDimension">The number of dimensions of the new, empty NdArray.</param>
        /// <returns>The new empty NdArray.</returns>
        public static NdArray<T> Empty(IDevice device, int numDimension)
        {
            return new NdArray<T>(new int[numDimension], device);
        }

        /// <summary>
        /// Creates a new boolean NdArray filled with falses.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="shape">The shape of the new NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public static NdArray<bool> Falses(IDevice device, int[] shape)
        {
            return Filled(device, shape, false);
        }

        /// <summary>
        /// Creates a new NdArray filled with the specified value.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="shape">The shape of the new NdArray.</param>
        /// <param name="value">The value to fill the new NdArray with.</param>
        /// <returns>The new NdArray.</returns>
        public static NdArray<TF> Filled<TF>(IDevice device, int[] shape, TF value)
        {
            var newArray = new NdArray<TF>(shape, device);
            newArray.FillConst(value);

            return newArray;
        }

        /// <summary>
        /// Creates a new identity matrix.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="size">The size of the square identity matrix.</param>
        /// <returns>The new NdArray.</returns>
        public static NdArray<T> Identity(IDevice device, int size)
        {
            var newArray = NdArray<T>.Zeros(device, new[] { size, size });
            var diagView = NdArrayOperator<T>.Diag(newArray);
            diagView.FillConst(Primitives.One<T>());

            return newArray;
        }

        /// <summary>
        /// Creates a new NdArray filled with ones (1).
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="shape">The shape of the new NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public static NdArray<T> Ones(IDevice device, int[] shape)
        {
            var newArray = new NdArray<T>(shape, device);
            newArray.FillConst(Primitives.One<T>());

            return newArray;
        }

        /// <summary>
        /// Creates a new NdArray filled with ones using the specified NdArray as template.
        /// </summary>
        /// <param name="template">The template NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public static NdArray<T> OnesLike(NdArray<T> template)
        {
            return Ones(template.Storage.Device, template.Shape);
        }

        /// <summary>
        /// Creates a new NdArray of given size filled with equaly spaced values.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="start">The starting value.</param>
        /// <param name="stop">The end value, which is not included.</param>
        /// <param name="numElement">The size of the vector.</param>
        /// <returns>The new NdArray.</returns>
        public static NdArray<T> Linspace(IDevice device, T start, T stop, int numElement)
        {
            if (numElement < 2)
            {
                throw new ArgumentException("linspace requires at least two elements.", "numElement");
            }

            var op = ScalarPrimitives.For<T, int>();

            var numElementT = op.Convert(numElement - 1);
            var increment = op.Divide(op.Subtract(stop, start), numElementT);

            var newArray = new NdArray<T>(new[] { numElement }, device);
            newArray.FillIncrementing(start, increment);

            return newArray;
        }

        /// <summary>
        /// Creates a new zero-dimensional (scalar) NdArray with the specified value.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="value">The value of the new, scalar NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public static NdArray<T> Scalar(IDevice device, T value)
        {
            var newArray = new NdArray<T>(new int[] { }, device);
            newArray.Value = value;

            return newArray;
        }

        /// <summary>
        /// Creates a new zero-dimensional (scalar) NdArray using the specified NdArray as template and with
        /// the specified value.
        /// </summary>
        /// <param name="tmpl">template template NdArray.</param>
        /// <param name="value">The value of the new, scalar NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public static NdArray<T> ScalarLike(NdArray<T> template, T value)
        {
            return Scalar(template.Storage.Device, value);
        }

        /// <summary>
        /// Creates a new boolean NdArray filled with trues.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="shape">The shape of the new NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public static NdArray<bool> Trues(IDevice device, int[] shape)
        {
            return Filled(device, shape, true);
        }

        /// <summary>
        /// Creates a new NdArray filled with zeros (0).
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="shape">The shape of the new NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public static NdArray<T> Zeros(IDevice device, int[] shape)
        {
            var newArray = new NdArray<T>(shape, device);
            return newArray;
        }

        /// <summary>
        /// Creates a new NdArray filled with zeros using the specified NdArray as template.
        /// </summary>
        /// <param name="template">The template NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public static NdArray<T> ZerosLike(NdArray<T> template)
        {
            return Zeros(template.Storage.Device, template.Shape);
        }
    }
}