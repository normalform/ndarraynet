// <copyright file="NdArrayTests.cs" company="NdArrayNet">
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

    [TestClass]
    public class NdArrayTests
    {
        [TestMethod]
        public void NdArray_RowMajor()
        {
            // arange
            var device = HostDevice.Instance;

            // action
            var array = new NdArray<int>(new int[] { 1, 2, 3, 4, 5 }, device, Order.RowMajor);

            // assert
            var expected = new[] { 120, 60, 20, 5, 1 };
            CollectionAssert.AreEqual(expected, array.Layout.Stride);
        }

        [TestMethod]
        public void NdArray_ColumnMajor()
        {
            // arange
            var device = HostDevice.Instance;

            // action
            var array = new NdArray<int>(new int[] { 1, 2, 3, 4, 5 }, device, Order.ColumnMajor);

            // assert
            var expected = new[] { 1, 1, 2, 6, 24 };
            CollectionAssert.AreEqual(expected, array.Layout.Stride);
        }

        [TestMethod]
        public void NumElements()
        {
            // arange
            var device = HostDevice.Instance;

            // action
            var array = new NdArray<int>(new int[] { 1, 2, 3, 4, 5 }, device, Order.ColumnMajor);

            // assert
            var expected = 120;
            Assert.AreEqual(expected, array.NumElements);
        }

        [TestMethod]
        public void NumDimensions()
        {
            // arange
            var device = HostDevice.Instance;

            // action
            var array = new NdArray<int>(new int[] { 1, 2, 3, 4, 5 }, device, Order.ColumnMajor);

            // assert
            var expected = 5;
            Assert.AreEqual(expected, array.NumDimensions);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FillIncrementing_WithScalar_ThrowException()
        {
            // arange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Ones(device, new int[] { });

            // action
            array.FillIncrementing(0, 1);
        }

        [TestMethod]
        public void GetValue_ScalarArray_ReturnValue()
        {
            // arange
            var device = HostDevice.Instance;
            var scalarArray = NdArray<int>.Ones(device, new int[] { });

            // action
            var value = scalarArray.Value;

            // assert
            Assert.AreEqual(1, value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetValue_Vector_ThrowException()
        {
            // arange
            var device = HostDevice.Instance;
            var scalarArray = NdArray<int>.Ones(device, new int[] { 1 });

            // action
            var _ = scalarArray.Value;
        }

        [TestMethod]
        public void SetValue_ScalarArray_ReturnValue()
        {
            // arange
            var device = HostDevice.Instance;
            var scalarArray = NdArray<int>.Ones(device, new int[] { });

            // action
            scalarArray.Value = 100;

            // assert
            Assert.AreEqual(100, scalarArray.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetValue_Vector_ThrowException()
        {
            // arange
            var device = HostDevice.Instance;
            var scalarArray = NdArray<int>.Ones(device, new int[] { 1 });

            // action
            scalarArray.Value = 100;
        }

        [TestMethod]
        public void Get_SingleIndex()
        {
            // arange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Arange(device, 0, 3, 1);

            // action
            var scalarArray = array[2];

            // assert
            Assert.AreEqual(2, scalarArray.Value);
        }

        [TestMethod]
        public void Set_SingleIndex()
        {
            // arange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Arange(device, 0, 3, 1);

            // action
            array[2] = NdArray<int>.Ones(device, new int[] { });
            var scalarArray = array[2];

            // assert
            Assert.AreEqual(1, scalarArray.Value);
        }
    }
}