// <copyright file="ComparisonFunctionTests.cs" company="NdArrayNet">
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

    [TestClass]
    public class ComparisonFunctionTests
    {
        [TestMethod]
        public void FillEqual()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);
            var result = NdArray<bool>.Zeros(device, new[] { 10 });

            // action
            ComparisonFunction<bool>.FillEqual(result, inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, result.Shape);
        }

        [TestMethod]
        public void Equal()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var result = ComparisonFunction<int>.Equal(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, result.Shape);
        }

        [TestMethod]
        public void FillNotEqual()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);
            var result = NdArray<bool>.Zeros(device, new[] { 10 });

            // action
            ComparisonFunction<bool>.FillNotEqual(result, inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, result.Shape);
        }

        [TestMethod]
        public void NotEqual()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var result = ComparisonFunction<int>.NotEqual(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, result.Shape);
        }

        [TestMethod]
        public void FillLess()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);
            var result = NdArray<bool>.Zeros(device, new[] { 10 });

            // action
            ComparisonFunction<bool>.FillLess(result, inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, result.Shape);
        }

        [TestMethod]
        public void Less()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var result = ComparisonFunction<int>.Less(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, result.Shape);
        }

        [TestMethod]
        public void FillLessOrEqual()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);
            var result = NdArray<bool>.Zeros(device, new[] { 10 });

            // action
            ComparisonFunction<bool>.FillLessOrEqual(result, inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, result.Shape);
        }

        [TestMethod]
        public void LessOrEqual()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var result = ComparisonFunction<int>.LessOrEqual(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, result.Shape);
        }

        [TestMethod]
        public void FillGreater()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);
            var result = NdArray<bool>.Zeros(device, new[] { 10 });

            // action
            ComparisonFunction<bool>.FillGreater(result, inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, result.Shape);
        }

        [TestMethod]
        public void Greater()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var result = ComparisonFunction<int>.Greater(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, result.Shape);
        }

        [TestMethod]
        public void FillGreaterOrEqual()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);
            var result = NdArray<bool>.Zeros(device, new[] { 10 });

            // action
            ComparisonFunction<bool>.FillGreaterOrEqual(result, inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, result.Shape);
        }

        [TestMethod]
        public void GreaterOrEqual()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var result = ComparisonFunction<int>.GreaterOrEqual(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, result.Shape);
        }

        [TestMethod]
        public void IsClose_SameIntVectors_ReturnTrues()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var close = ComparisonFunction<int>.IsClose(inputA, inputA);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, close.Shape);
        }

        [TestMethod]
        public void IsClose_SameDoubleVectors_ReturnTrues()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<double>.Arange(device, 0, 10, 1);

            // action
            var close = ComparisonFunction<double>.IsClose(inputA, inputA);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, close.Shape);
        }

        [TestMethod]
        public void IsClose_DifferentDoubleVectors_ReturnFalses()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<double>.Arange(device, 0, 10, 1);

            // action
            var close = ComparisonFunction<double>.IsClose(inputA, inputA + 1.0);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, close.Shape);
        }

        [TestMethod]
        public void IsClose_DifferentDoubleVectorsWithBigTolerence_ReturnTrue()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<double>.Arange(device, 0, 10, 1);

            // action
            var close = ComparisonFunction<double>.IsClose(inputA, inputA + 1.0, 2.0);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, close.Shape);
        }

        [TestMethod]
        public void IsClose_CloseDoubleVectors_ReturnTrue()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<double>.Arange(device, 0, 10, 1);
            var inputB = NdArray<double>.Arange(device, 0, 10, 1) + 1e-100;

            // action
            var close = ComparisonFunction<double>.IsClose(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, close.Shape);
        }

        [TestMethod]
        public void AlmostEqual_SameIntVectors_ReturnTrue()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Zeros(device, new[] { 2, 3, 4 });
            var inputB = NdArray<int>.Zeros(device, new[] { 2, 3, 4 });

            // action
            var almostEqual = ComparisonFunction<int>.AlmostEqual(inputA, inputB);

            // assert
            Assert.IsTrue(almostEqual);
        }

        [TestMethod]
        public void AlmostEqual_DifferentIntVectors_ReturnFalse()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Zeros(device, new[] { 2, 3, 4 });
            var inputB = NdArray<int>.Zeros(device, new[] { 2, 3, 4 }) + 1;

            // action
            var almostEqual = ComparisonFunction<int>.AlmostEqual(inputA, inputB);

            // assert
            Assert.IsFalse(almostEqual);
        }

        [TestMethod]
        public void FillIsFinite()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Zeros(device, new[] { 2, 3, 4 });
            var result = NdArray<bool>.Ones(device, new[] { 2, 3, 4 });

            // action
            ComparisonFunction<int>.FillIsFinite(result, inputA);

            // assert
            Assert.IsTrue(NdArray<int>.All(result));
        }

        [TestMethod]
        public void IsFinite()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Zeros(device, new[] { 2, 3, 4 });

            // action
            var result = ComparisonFunction<int>.IsFinite(inputA);

            // assert
            Assert.IsTrue(NdArray<int>.All(result));
        }
    }
}