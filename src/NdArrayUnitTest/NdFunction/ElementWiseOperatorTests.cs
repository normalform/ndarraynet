// <copyright file="ElementWiseOperatorTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArrayNet;
    using System.Numerics;
    using Xunit;

    public class ElementWiseOperatorTests
    {
        [Fact]
        public void UnaryPlus()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<int>.Arange(configManager,0, 10, 1);

            // action
            var output = +input;

            // assert
            Assert.Equal(new[] { 10 }, output.Shape);
        }

        [Fact]
        public void UnaryMinus()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<int>.Arange(configManager,0, 10, 1);

            // action
            var output = -input;

            // assert
            Assert.Equal(new[] { 10 }, output.Shape);
        }

        [Fact]
        public void Add_VectorWithVector()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,0, 10, 1);
            var inputB = NdArray<int>.Arange(configManager,0, 10, 1);

            // action
            var equal = inputA + inputB;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }

        [Fact]
        public void Add_VectorWithScalar()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,0, 10, 1);
            var inputB = NdArray<int>.Scalar(configManager,3);

            // action
            var equal = inputA + inputB;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }

        [Fact]
        public void Add_ScalarWithVector()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,0, 10, 1);
            var inputB = NdArray<int>.Scalar(configManager,3);

            // action
            var equal = inputB + inputA;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }

        [Fact]
        public void Subtract_VectorWithVector()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,0, 10, 1);
            var inputB = NdArray<int>.Arange(configManager,0, 10, 1);

            // action
            var equal = inputA - inputB;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }

        [Fact]
        public void Subtract_VectorWithScalar()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,0, 10, 1);
            var inputB = NdArray<int>.Scalar(configManager,3);

            // action
            var equal = inputA - inputB;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }

        [Fact]
        public void Subtract_ScalarWithVector()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,0, 10, 1);
            var inputB = NdArray<int>.Scalar(configManager,3);

            // action
            var equal = inputB - inputA;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }

        [Fact]
        public void Multiply_VectorWithVector()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,0, 10, 1);
            var inputB = NdArray<int>.Arange(configManager,0, 10, 1);

            // action
            var equal = inputA * inputB;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }

        [Fact]
        public void Multiply_VectorWithScalar()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,0, 10, 1);
            var inputB = NdArray<int>.Scalar(configManager,3);

            // action
            var equal = inputA * inputB;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }

        [Fact]
        public void Multiply_ScalarWithVector()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,0, 10, 1);
            var inputB = NdArray<int>.Scalar(configManager,3);

            // action
            var equal = inputB * inputA;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }

        [Fact]
        public void Divide_VectorWithVector()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,1, 11, 1);
            var inputB = NdArray<int>.Arange(configManager,1, 11, 1);

            // action
            var equal = inputA / inputB;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }

        [Fact]
        public void Divide_VectorWithScalar()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,1, 11, 1);
            var inputB = NdArray<int>.Scalar(configManager,3);

            // action
            var equal = inputA / inputB;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }

        [Fact]
        public void Divide_ScalarWithVector()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,1, 11, 1);
            var inputB = NdArray<int>.Scalar(configManager,3);

            // action
            var equal = inputB / inputA;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }

        [Fact]
        public void Divide_WithAligned()
        {
            // arrange
            var vectorCount = Vector<int>.Count;
            var numElements = vectorCount * 2;

            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,1, numElements + 1, 1);
            var inputB = NdArray<int>.Arange(configManager,1, numElements + 1, 1);

            // action
            var equal = inputB / inputA;

            // assert
            Assert.Equal(new[] { numElements }, equal.Shape);
        }

        [Fact]
        public void Divide_WithUnAligned()
        {
            // arrange
            var vectorCount = Vector<int>.Count;
            var numElements = vectorCount / 2;

            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,1, numElements + 1, 1);
            var inputB = NdArray<int>.Arange(configManager,1, numElements + 1, 1);

            // action
            var equal = inputB / inputA;

            // assert
            Assert.Equal(new[] { numElements }, equal.Shape);
        }

        [Fact]
        public void Modulo_VectorWithVector()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,1, 11, 1);
            var inputB = NdArray<int>.Arange(configManager,1, 11, 1);

            // action
            var equal = inputA % inputB;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }

        [Fact]
        public void Modulo_VectorWithScalar()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,1, 11, 1);
            var inputB = NdArray<int>.Scalar(configManager,3);

            // action
            var equal = inputA % inputB;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }

        [Fact]
        public void Modulo_ScalarWithVector()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Arange(configManager,1, 11, 1);
            var inputB = NdArray<int>.Scalar(configManager,3);

            // action
            var equal = inputB % inputA;

            // assert
            Assert.Equal(new[] { 10 }, equal.Shape);
        }
    }
}