// <copyright file="NdArrayOperatorTests.cs" company="NdArrayNet">
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
    public class NdArrayOperatorTests
    {
        [TestMethod]
        public void DiagAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<int>.Zeros(device, new[] { 4, 3, 3, 5 });

            // action
            var diag = NdArrayOperator<int>.DiagAxis(1, 2, input);

            // assert
            CollectionAssert.AreEqual(new[] { 4, 3, 5 }, diag.Shape);
        }

        [TestMethod]
        public void Diag()
        {
            // arrange
            var device = HostDevice.Instance;
            var input = NdArray<int>.Arange(device, 0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var diag = NdArrayOperator<int>.Diag(input);

            // assert
            Assert.AreEqual(0, diag[0].Value);
            Assert.AreEqual(4, diag[1].Value);
            Assert.AreEqual(8, diag[2].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Concat_EmptyInput_ThrowException()
        {
            // arrange
            const int DummyAxis = 1;
            var emptyInputs = new NdArray<int>[] { };

            // action
            NdArrayOperator<int>.Concat(DummyAxis, emptyInputs);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Concat_AxisOutOfrangeCase1_ThrowException()
        {
            // arrange
            const int ConcatAxis = 3;
            var inputs = new NdArray<int>[] { NdArray<int>.Zeros(HostDevice.Instance, new[] { 1, 1 }) };

            // action
            NdArrayOperator<int>.Concat(ConcatAxis, inputs);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Concat_AxisOutOfrangeCase2_ThrowException()
        {
            // arrange
            const int ConcatAxis = -1;
            var inputs = new NdArray<int>[] { NdArray<int>.Zeros(HostDevice.Instance, new[] { 1, 1 }) };

            // action
            NdArrayOperator<int>.Concat(ConcatAxis, inputs);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Concat_DifferntShapes_ThrowException()
        {
            // arrange
            const int ConcatAxis = 1;
            var inputs = new NdArray<int>[]
            {
                NdArray<int>.Zeros(HostDevice.Instance, new[] { 1, 1 }),
                NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 1 })
            };

            // action
            NdArrayOperator<int>.Concat(ConcatAxis, inputs);
        }

        [TestMethod]
        public void Concat()
        {
            // arrange
            const int ConcatAxis = 1;
            var inputs = new NdArray<int>[]
            {
                NdArray<int>.Zeros(HostDevice.Instance, new[] { 4, 28 }),
                NdArray<int>.Zeros(HostDevice.Instance, new[] { 4, 15 }),
                NdArray<int>.Zeros(HostDevice.Instance, new[] { 4, 10 })
            };

            // action
            var concat = NdArrayOperator<int>.Concat(ConcatAxis, inputs);

            // assert
            CollectionAssert.AreEqual(new[] { 4, 53 }, concat.Shape);
        }

        [TestMethod]
        public void Copy()
        {
            // arrange
            var input = NdArray<int>.Arange(HostDevice.Instance, 0, 10, 1).Reshape(new[] { 2, 5 });

            // action
            var copy = NdArrayOperator<int>.Copy(input);

            // assert
            CollectionAssert.AreEqual(new[] { 2, 5 }, copy.Shape);
            CollectionAssert.AreEqual(new[] { 5, 1 }, copy.Layout.Stride);
        }

        [TestMethod]
        public void Copy_ColumnMajor()
        {
            // arrange
            var input = NdArray<int>.Arange(HostDevice.Instance, 0, 10, 1).Reshape(new[] { 2, 5 });

            // action
            var copy = NdArrayOperator<int>.Copy(input, Order.ColumnMajor);

            // assert
            CollectionAssert.AreEqual(new[] { 2, 5 }, copy.Shape);
            CollectionAssert.AreEqual(new[] { 1, 2 }, copy.Layout.Stride);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DiagMatAxis_SameAxes()
        {
            // arrange
            var axis1 = 1;
            var axis2 = 1;
            var input = NdArray<int>.Arange(HostDevice.Instance, 0, 10, 1).Reshape(new[] { 2, 5 });

            // action
            var diagMat = NdArrayOperator<int>.DiagMatAxis(axis1, axis2, input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DiagMatAxis_InvalidAxis()
        {
            // arrange
            var axis1 = 1;
            var axis2 = 3;
            var input = NdArray<int>.Arange(HostDevice.Instance, 0, 10, 1).Reshape(new[] { 2, 5 });

            // action
            var diagMat = NdArrayOperator<int>.DiagMatAxis(axis1, axis2, input);
        }

        [TestMethod]
        public void DiagMatAxis()
        {
            // arrange
            var axis1 = 0;
            var axis2 = 1;
            var input = NdArray<int>.Arange(HostDevice.Instance, 0, 12, 1).Reshape(new[] { 4, 3 });

            // action
            var diagMat = NdArrayOperator<int>.DiagMatAxis(axis1, axis2, input);

            // assert
            CollectionAssert.AreEqual(new[] { 4, 4, 3 }, diagMat.Shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DiagMat_Scalar_ThrowException()
        {
            // arrange
            const int DummyValue = 3;
            var input = NdArray<int>.Scalar(HostDevice.Instance, DummyValue);

            // action
            var diagMat = NdArrayOperator<int>.DiagMat(input);
        }

        [TestMethod]
        public void DiagMat()
        {
            // arrange
            var input = NdArray<int>.Arange(HostDevice.Instance, 0, 3, 1);

            // action
            var diagMat = NdArrayOperator<int>.DiagMat(input);

            // assert
            CollectionAssert.AreEqual(new[] { 3, 3 }, diagMat.Shape);
        }

        [TestMethod]
        public void DiffAxis()
        {
            // arrange
            var input = NdArray<int>.Arange(HostDevice.Instance, 0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var result = NdArrayOperator<int>.DiffAxis(1, input);

            // assert
            CollectionAssert.AreEqual(new[] { 3, 2 }, result.Shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Diff_Scalar_ThrowException()
        {
            // arrange
            const int DummyValue = 3;
            var input = NdArray<int>.Scalar(HostDevice.Instance, DummyValue);

            // action
            var diagMat = NdArrayOperator<int>.Diff(input);
        }

        [TestMethod]
        public void Diff()
        {
            // arrange
            var input = NdArray<int>.Arange(HostDevice.Instance, 0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var result = NdArrayOperator<int>.Diff(input);

            // assert
            CollectionAssert.AreEqual(new[] { 3, 2 }, result.Shape);
        }

        [TestMethod]
        public void Replicate()
        {
            // arrange
            var input = NdArray<int>.Arange(HostDevice.Instance, 0, 2 * 3, 1).Reshape(new[] { 2, 3 });

            // action
            var result = NdArrayOperator<int>.Replicate(0, 10, input);

            // assert
            CollectionAssert.AreEqual(new[] { 20, 3 }, result.Shape);
        }

        [TestMethod]
        public void Transpos()
        {
            // arrange
            var input = NdArray<int>.Arange(HostDevice.Instance, 0, 4, 1).Reshape(new[] { 2, 2 });

            // action
            var result = NdArrayOperator<int>.Transpos(input);

            // assert
            Assert.AreEqual(0, result[new[] { 0, 0 }]);
            Assert.AreEqual(1, result[new[] { 1, 0 }]);
            Assert.AreEqual(2, result[new[] { 0, 1 }]);
            Assert.AreEqual(3, result[new[] { 1, 1 }]);
        }
    }
}