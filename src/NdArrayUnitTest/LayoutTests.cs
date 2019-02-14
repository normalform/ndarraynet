// <copyright file="LayoutTests.cs" company="NdArrayNet">
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
    using System;
    using System.Linq;

    [TestClass]
    public class LayoutTests
    {
        [TestMethod]
        public void OrderedStride_case1()
        {
            // arange
            var shape = new[] { 1, 2, 3, 4, 5 };
            var order = Enumerable.Range(0, shape.Length).Reverse().ToArray();

            // action
            var result = Layout.OrderedStride(shape, order);

            // assert
            var expected = new[] { 120, 60, 20, 5, 1 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OrderedStride_case2()
        {
            // arange
            var shape = new[] { 5, 4, 3, 2, 1 };
            var order = Enumerable.Range(0, shape.Length).Reverse().ToArray();

            // action
            var result = Layout.OrderedStride(shape, order);

            // assert
            var expected = new[] { 24, 6, 2, 1, 1 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OrderedStride_case3()
        {
            // arange
            var shape = new[] { 0, 1, 2, 3, 4 };
            var order = Enumerable.Range(0, shape.Length).Reverse().ToArray();

            // action
            var result = Layout.OrderedStride(shape, order);

            // assert
            var expected = new[] { 24, 24, 12, 4, 1 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OrderedStride_OrderIsNotPermutation_ThrowException()
        {
            // arange
            var shape = new[] { 0, 1, 2, 3, 4 };
            var order = new[] { 1, 2, 3, 4, 5 };

            // action
            var _ = Layout.OrderedStride(shape, order);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OrderedStride_DifferentSize_ThrowException()
        {
            // arange
            var shape = new[] { 0, 1, 2, 3, 4 };
            var order = new[] { 0, 1 };

            // action
            var _ = Layout.OrderedStride(shape, order);
        }

        [TestMethod]
        public void CStride()
        {
            // arange
            var shape = new[] { 1, 2, 3, 4, 5 };

            // action
            var result = Layout.CStride(shape);

            // assert
            var expected = new[] { 120, 60, 20, 5, 1 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FStride()
        {
            // arange
            var shape = new[] { 1, 2, 3, 4, 5 };

            // action
            var result = Layout.FStride(shape);

            // assert
            var expected = new[] { 1, 1, 2, 6, 24 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void NewC()
        {
            // arange
            var shape = new[] { 1, 2, 3, 4, 5 };

            // action
            var result = Layout.NewC(shape);

            // assert
            var expected = new[] { 120, 60, 20, 5, 1 };
            CollectionAssert.AreEqual(shape, result.Shape);
            CollectionAssert.AreEqual(expected, result.Stride);
        }

        [TestMethod]
        public void NewF()
        {
            // arange
            var shape = new[] { 1, 2, 3, 4, 5 };

            // action
            var result = Layout.NewF(shape);

            // assert
            var expected = new[] { 1, 1, 2, 6, 24 };
            CollectionAssert.AreEqual(shape, result.Shape);
            CollectionAssert.AreEqual(expected, result.Stride);
        }
    }
}