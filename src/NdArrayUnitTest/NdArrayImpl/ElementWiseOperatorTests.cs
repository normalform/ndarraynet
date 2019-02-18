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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NdArrayNet;
    using System.Numerics;

    [TestClass]
    public class ElementWiseOperatorTests
    {
        [TestMethod]
        public void UnaryPlus()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var output = +input;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, output.Shape);
        }

        [TestMethod]
        public void UnaryMinus()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var output = -input;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, output.Shape);
        }

        [TestMethod]
        public void Add_VectorWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var equal = inputA + inputB;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Add_VectorWithScalar()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = inputA + inputB;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Add_ScalarWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = inputB + inputA;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Subtract_VectorWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var equal = inputA - inputB;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Subtract_VectorWithScalar()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = inputA + inputB;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Subtract_ScalarWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = inputB - inputA;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Multiply_VectorWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var equal = inputA * inputB;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Multiply_VectorWithScalar()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = inputA * inputB;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Multiply_ScalarWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = inputB * inputA;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Divide_VectorWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 1, 11, 1);
            var inputB = NdArray<int>.Arange(device, 1, 11, 1);

            // action
            var equal = inputA / inputB;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Divide_VectorWithScalar()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 1, 11, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = inputA / inputB;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Divide_ScalarWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 1, 11, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = inputB / inputA;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Divide_WithAligned()
        {
            // arrange
            var vectorCount = Vector<int>.Count;
            var numElements = vectorCount * 2;

            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 1, numElements + 1, 1);
            var inputB = NdArray<int>.Arange(device, 1, numElements + 1, 1);

            // action
            var equal = inputB / inputA;

            // assert
            CollectionAssert.AreEqual(new[] { numElements }, equal.Shape);
        }

        [TestMethod]
        public void Divide_WithUnAligned()
        {
            // arrange
            var vectorCount = Vector<int>.Count;
            var numElements = vectorCount / 2;

            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 1, numElements + 1, 1);
            var inputB = NdArray<int>.Arange(device, 1, numElements + 1, 1);

            // action
            var equal = inputB / inputA;

            // assert
            CollectionAssert.AreEqual(new[] { numElements }, equal.Shape);
        }

        [TestMethod]
        public void Modulo_VectorWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 1, 11, 1);
            var inputB = NdArray<int>.Arange(device, 1, 11, 1);

            // action
            var equal = inputA % inputB;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Modulo_VectorWithScalar()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 1, 11, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = inputA % inputB;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Modulo_ScalarWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 1, 11, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = inputB % inputA;

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }
    }
}