// <copyright file="IndexFunctionTests.cs" company="NdArrayNet">
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
    using NdArray.NdArrayImpl;
    using NdArrayNet;
    using System;
    using System.Linq;

    [TestClass]
    public class IndexFunctionTests
    {
        [TestMethod]
        public void AllIndex()
        {
            // arrange
            var source = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 1, 3 });

            // action
            var output = IndexFunction<int>.AllIndex(source);

            // assert
            CollectionAssert.AreEqual(new[] { 0, 0, 0 }, output[0]);
            CollectionAssert.AreEqual(new[] { 0, 0, 1 }, output[1]);
            CollectionAssert.AreEqual(new[] { 0, 0, 2 }, output[2]);
            CollectionAssert.AreEqual(new[] { 1, 0, 0 }, output[3]);
            CollectionAssert.AreEqual(new[] { 1, 0, 1 }, output[4]);
            CollectionAssert.AreEqual(new[] { 1, 0, 2 }, output[5]);
        }

        [TestMethod]
        public void AllElems()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var output = IndexFunction<int>.AllElems(source);

            // assert
            CollectionAssert.AreEqual(Enumerable.Range(0, 9).ToArray(), output);
        }

        [TestMethod]
        public void ArgMaxAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = IndexFunction<int>.ArgMaxAxis(1, source);

            // assert
            Assert.AreEqual(3, output[0].Value);
            Assert.AreEqual(3, output[1].Value);
        }

        [TestMethod]
        public void ArgMax()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = IndexFunction<int>.ArgMax(source);

            // assert
            CollectionAssert.AreEqual(new[] { 1, 3 }, output);
        }

        [TestMethod]
        public void ArgMinAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = IndexFunction<int>.ArgMinAxis(1, source);

            // assert
            Assert.AreEqual(0, output[0].Value);
            Assert.AreEqual(0, output[1].Value);
        }

        [TestMethod]
        public void ArgMin()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = IndexFunction<int>.ArgMin(source);

            // assert
            CollectionAssert.AreEqual(new[] { 0, 0 }, output);
        }

        [TestMethod]
        public void FindAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 0, 8, 1).Reshape(new[] { 2, 4 });

            // action
            var output = IndexFunction<int>.FindAxis(2, 1, source);

            // assert
            Assert.AreEqual(2, output[0].Value);
            Assert.AreEqual(SpecialIdx.NotFound, output[1].Value);
        }

        [TestMethod]
        public void TryFind()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });
            source[new[] { 1, 3 }] = 2;

            // action
            var output = IndexFunction<int>.TryFind(2, source);

            // assert
            CollectionAssert.AreEqual(new[] { 0, 1 }, output);
        }

        [TestMethod]
        public void TryFind_NotFound()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = IndexFunction<int>.TryFind(10, source);

            // assert
            Assert.IsTrue(output.Length == 0);
        }

        [TestMethod]
        public void Find()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });
            source[new[] { 1, 3 }] = 2;

            // action
            var output = IndexFunction<int>.Find(2, source);

            // assert
            CollectionAssert.AreEqual(new[] { 0, 1 }, output);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Find_NotFound_ThrowException()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = IndexFunction<int>.Find(10, source);
        }
    }
}