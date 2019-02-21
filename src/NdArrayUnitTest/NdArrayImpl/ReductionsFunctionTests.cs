// <copyright file="ReductionsFunctionTests.cs" company="NdArrayNet">
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

    [TestClass]
    public class ReductionsFunctionTests
    {
        [TestMethod]
        public void MaxAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var max = ReductionFunction<double>.MaxAxis(1, input);

            // assert
            Assert.AreEqual(4.0, max[0].Value);
        }

        [TestMethod]
        public void MinAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var min = ReductionFunction<double>.MinAxis(1, input);

            // assert
            Assert.AreEqual(1.0, min[0].Value);
        }

        [TestMethod]
        public void MaxNdArray()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var max = ReductionFunction<double>.MaxNdArray(input);

            // assert
            Assert.AreEqual(8.0, max[0].Value);
        }

        [TestMethod]
        public void MinNdArray()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var min = ReductionFunction<double>.MinNdArray(input);

            // assert
            Assert.AreEqual(1.0, min[0].Value);
        }

        [TestMethod]
        public void SumAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var sum = ReductionFunction<double>.SumAxis(1, input);

            // assert
            Assert.AreEqual(10.0, sum[0].Value);
        }

        [TestMethod]
        public void SumNdArray()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var sum = ReductionFunction<double>.SumNdArray(input);

            // assert
            Assert.AreEqual(36.0, sum[0].Value);
        }

        [TestMethod]
        public void MeanAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var mean = ReductionFunction<double>.MeanAxis(1, input);

            // assert
            Assert.AreEqual(2.5, mean[0].Value);
        }

        [TestMethod]
        public void Mean()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var sum = ReductionFunction<double>.Mean(input);

            // assert
            Assert.AreEqual(4.5, sum);
        }

        [TestMethod]
        public void ProductAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var product = ReductionFunction<double>.ProductAxis(1, input);

            // assert
            Assert.AreEqual(24.0, product[0].Value);
        }

        [TestMethod]
        public void ProductNdArray()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var product = ReductionFunction<double>.ProductNdArray(input);

            // assert
            Assert.AreEqual(40320.0, product[0].Value);
        }

        [TestMethod]
        public void VarAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var var = ReductionFunction<double>.VarAxis(1, input);

            // assert
            Assert.AreEqual(1.25, var[0].Value);
        }

        [TestMethod]
        public void VarAxis_Ddof1()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var var = ReductionFunction<double>.VarAxis(1, input, 1.0);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(1.666666666 - var[0].Value) < Epsilon);
        }

        [TestMethod]
        public void StdAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var std = ReductionFunction<double>.StdAxis(1, input);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(1.11803399 - std[0].Value) < Epsilon);
        }

        [TestMethod]
        public void StdAxis_Ddof1()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var std = ReductionFunction<double>.StdAxis(1, input, 1.0);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(1.29099445 - std[0].Value) < Epsilon);
        }

        [TestMethod]
        public void TraceAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<int>.Arange(device, 0, 27, 1).Reshape(new[] { 3, 3, 3 });

            // action
            var trace = ReductionFunction<int>.TraceAxis(0, 1, input);

            // assert
            Assert.AreEqual(36, trace[0].Value);
            Assert.AreEqual(39, trace[1].Value);
            Assert.AreEqual(42, trace[2].Value);
        }

        [TestMethod]
        public void Trace()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<int>.Arange(device, 0, 27, 1).Reshape(new[] { 3, 3, 3 });

            // action
            var trace = ReductionFunction<int>.Trace(input);

            // assert
            Assert.AreEqual(12, trace[0].Value);
            Assert.AreEqual(39, trace[1].Value);
            Assert.AreEqual(66, trace[2].Value);
        }
    }
}