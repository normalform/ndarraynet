// <copyright file="BaseConstructor.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction.Constructor
{
    using System;
    using NdArrayNet;

    internal abstract class BaseConstructor<T> : INdConstructor<T>
    {
        private IStaticMethod staticMethod;

        protected BaseConstructor(IStaticMethod staticMethod)
        {
            this.staticMethod = staticMethod;
        }

        /// <summary>
        /// Creates a new NdArray filled with equaly spaced values using a specifed increment.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="start">The starting value.</param>
        /// <param name="stop">The end value, which is not included.</param>
        /// <param name="step">The increment between successive element.</param>
        /// <typeparam name="T">The new NdArray.</typeparam>
        public virtual NdArray<T> Arange(IConfigManager configManager, T start, T stop, T step)
        {
            var sp = ScalarPrimitivesRegistry.For<T, T>();
            var spc = ScalarPrimitivesRegistry.For<int, T>();

            var numberOfElementT = sp.Divide(sp.Subtract(stop, start), step);
            var numberOfElementInt = spc.Convert(numberOfElementT);
            var numberOfElement = Math.Max(0, numberOfElementInt);

            var shape = new[] { numberOfElement };

            var newArray = new NdArray<T>(configManager, shape);
            newArray.FillIncrementing(start, step);

            return newArray;
        }

        /// <summary>
        /// Creates a new NdArray filled with the integers from zero to the specified maximum.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="numElements">The number of elements of the new NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public virtual NdArray<T> Counting(IConfigManager configManager, int numElements)
        {
            var newArray = new NdArray<T>(configManager, new[] { numElements });
            newArray.FillIncrementing(Primitives.Zero<T>(), Primitives.One<T>());

            return newArray;
        }

        /// <summary>
        /// Creates a new empty NdArray with the given number of dimensions.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="numDimension">The number of dimensions of the new, empty NdArray.</param>
        /// <returns>The new empty NdArray.</returns>
        public virtual NdArray<T> Empty(IConfigManager configManager, int numDimension)
        {
            return new NdArray<T>(configManager, new int[numDimension]);
        }

        /// <summary>
        /// Creates a new boolean NdArray filled with falses.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="shape">The shape of the new NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public virtual NdArray<bool> Falses(IConfigManager configManager, int[] shape)
        {
            return Filled(configManager, shape, false);
        }

        /// <summary>
        /// Creates a new NdArray filled with the specified value.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="shape">The shape of the new NdArray.</param>
        /// <param name="value">The value to fill the new NdArray with.</param>
        /// <returns>The new NdArray.</returns>
        public virtual NdArray<TF> Filled<TF>(IConfigManager configManager, int[] shape, TF value)
        {
            var newArray = new NdArray<TF>(configManager, shape);
            newArray.FillConst(value);

            return newArray;
        }

        /// <summary>
        /// Creates a new identity matrix.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="size">The size of the square identity matrix.</param>
        /// <returns>The new NdArray.</returns>
        public virtual NdArray<T> Identity(IConfigManager configManager, int size)
        {
            var newArray = NdArray<T>.Zeros(configManager, new[] { size, size });
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
        public virtual NdArray<T> Ones(IConfigManager configManager, int[] shape)
        {
            var newArray = new NdArray<T>(configManager, shape);
            newArray.FillConst(Primitives.One<T>());

            return newArray;
        }

        /// <summary>
        /// Creates a new NdArray filled with ones using the specified NdArray as template.
        /// </summary>
        /// <param name="template">The template NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public virtual NdArray<T> OnesLike(NdArray<T> template)
        {
            return Ones(template.ConfigManager, template.Shape);
        }

        /// <summary>
        /// Creates a new NdArray of given size filled with equaly spaced values.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="start">The starting value.</param>
        /// <param name="stop">The end value, which is not included.</param>
        /// <param name="numElement">The size of the vector.</param>
        /// <returns>The new NdArray.</returns>
        public virtual NdArray<T> Linspace(IConfigManager configManager, T start, T stop, int numElement)
        {
            if (numElement < 2)
            {
                throw new ArgumentException("Linspace requires at least two elements.", "numElement");
            }

            var op = ScalarPrimitivesRegistry.For<T, int>();

            var numElementT = op.Convert(numElement - 1);
            var increment = op.Divide(op.Subtract(stop, start), numElementT);

            var newArray = new NdArray<T>(configManager, new[] { numElement });
            newArray.FillIncrementing(start, increment);

            return newArray;
        }

        /// <summary>
        /// Creates a new zero-dimensional (scalar) NdArray with the specified value.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="value">The value of the new, scalar NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public virtual NdArray<T> Scalar(IConfigManager configManager, T value)
        {
            var newArray = new NdArray<T>(configManager, new int[] { });
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
        public virtual NdArray<T> ScalarLike(IFrontend<T> template, T value)
        {
            return Scalar(template.ConfigManager, value);
        }

        /// <summary>
        /// Creates a new boolean NdArray filled with trues.
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="shape">The shape of the new NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public virtual NdArray<bool> Trues(IConfigManager configManager, int[] shape)
        {
            return Filled(configManager, shape, true);
        }

        /// <summary>
        /// Creates a new NdArray filled with zeros (0).
        /// </summary>
        /// <param name="device">The device to create the NdArray on.</param>
        /// <param name="shape">The shape of the new NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public virtual NdArray<T> Zeros(IConfigManager configManager, int[] shape)
        {
            var newArray = new NdArray<T>(configManager, shape);
            return newArray;
        }

        /// <summary>
        /// Creates a new NdArray filled with zeros using the specified NdArray as template.
        /// </summary>
        /// <param name="template">The template NdArray.</param>
        /// <returns>The new NdArray.</returns>
        public virtual NdArray<T> ZerosLike(NdArray<T> template)
        {
            return Zeros(template.ConfigManager, template.Shape);
        }
    }
}