// <copyright file="ElementWiseOperatorTests.cs" company="NdArrayNet">
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