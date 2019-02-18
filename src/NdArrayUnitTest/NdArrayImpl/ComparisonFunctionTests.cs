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
        public void Equal_VectorWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var equal = ComparisonFunction<int>.FillEqual(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Equal_VectorWithScalar()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = ComparisonFunction<int>.FillEqual(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Equal_ScalarWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = ComparisonFunction<int>.FillEqual(inputB, inputA);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void NotEqual_VectorWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var equal = ComparisonFunction<int>.FillNotEqual(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void NotEqual_VectorWithScalar()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = ComparisonFunction<int>.FillNotEqual(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void NotEqual_ScalarWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = ComparisonFunction<int>.FillNotEqual(inputB, inputA);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Less_VectorWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var equal = ComparisonFunction<int>.FillLess(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Less_VectorWithScalar()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = ComparisonFunction<int>.FillLess(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Less_ScalarWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = ComparisonFunction<int>.FillLess(inputB, inputA);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void LessOrEqual_VectorWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var equal = ComparisonFunction<int>.FillLessOrEqual(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void LessOrEqual_VectorWithScalar()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = ComparisonFunction<int>.FillLessOrEqual(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void LessOrEqual_ScalarWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = ComparisonFunction<int>.FillLessOrEqual(inputB, inputA);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Greater_VectorWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var equal = ComparisonFunction<int>.FillGreater(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Greater_VectorWithScalar()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = ComparisonFunction<int>.FillGreater(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void Greater_ScalarWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = ComparisonFunction<int>.FillGreater(inputB, inputA);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void GreaterOrEqual_VectorWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var equal = ComparisonFunction<int>.FillGreaterOrEqual(inputA,  inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void GreaterOrEqual_VectorWithScalar()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = ComparisonFunction<int>.FillGreaterOrEqual(inputA, inputB);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
        }

        [TestMethod]
        public void GreaterOrEqual_ScalarWithVector()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Arange(device, 0, 10, 1);
            var inputB = NdArray<int>.Scalar(device, 3);

            // action
            var equal = ComparisonFunction<int>.FillGreaterOrEqual(inputB, inputA);

            // assert
            CollectionAssert.AreEqual(new[] { 10 }, equal.Shape);
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
    }
}