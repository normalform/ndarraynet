// <copyright file="LogicalFunctionTests.cs" company="NdArrayNet">
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
    public class LogicalFunctionTests
    {
        [TestMethod]
        public void FillNegate()
        {
            // arrange
            var input = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 10 });

            // action
            var output = LogicalFunction<bool>.FillNegate(input);

            // assert
            Assert.IsTrue(NdArray<bool>.All(output));
        }

        [TestMethod]
        public void FillAnd()
        {
            // arrange
            var input1 = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 4 });
            var input2 = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 4 });

            // action
            var output = LogicalFunction<bool>.FillAnd(input1, input2);

            // assert
            var expected = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 4 });
            var result = expected == output;
            Assert.IsTrue(NdArray<bool>.All(result));
        }

        [TestMethod]
        public void FillOr()
        {
            // arrange
            var input1 = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 4 });
            var input2 = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 4 });

            // action
            var output = LogicalFunction<bool>.FillOr(input1, input2);

            // assert
            var expected = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 4 });
            var result = expected == output;
            Assert.IsTrue(NdArray<bool>.All(result));
        }

        [TestMethod]
        public void FillXor()
        {
            // arrange
            var input1 = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 2 });
            var input2 = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 2 });
            input2[0].Value = false;

            // action
            var output = LogicalFunction<bool>.FillXor(input1, input2);

            // assert
            Assert.AreEqual(false, output[0].Value);
            Assert.AreEqual(true, output[1].Value);
        }

        [TestMethod]
        public void AllAxis()
        {
            // arrange
            var input = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = false;

            // action
            var output0 = LogicalFunction<bool>.AllAxis(0, input);
            var output1 = LogicalFunction<bool>.AllAxis(1, input);

            // assert
            Assert.AreEqual(true, output0[0].Value);
            Assert.AreEqual(true, output1[1].Value);
        }

        [TestMethod]
        public void AllNdArray()
        {
            // arrange
            var input = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = false;

            // action
            var output = LogicalFunction<bool>.AllNdArray(input);

            // assert
            Assert.AreEqual(false, output[0].Value);
        }

        [TestMethod]
        public void All()
        {
            // arrange
            var input = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = false;

            // action
            var output = LogicalFunction<bool>.All(input);

            // assert
            Assert.AreEqual(false, output);
        }

        [TestMethod]
        public void AnyAxis()
        {
            // arrange
            var input = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = true;

            // action
            var output0 = LogicalFunction<bool>.AnyAxis(0, input);
            var output1 = LogicalFunction<bool>.AnyAxis(1, input);

            // assert
            Assert.AreEqual(true, output0[1].Value);
            Assert.AreEqual(false, output1[1].Value);
        }

        [TestMethod]
        public void AnyNdArray()
        {
            // arrange
            var input = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = true;

            // action
            var output = LogicalFunction<bool>.AnyNdArray(input);

            // assert
            Assert.AreEqual(true, output[0].Value);
        }

        [TestMethod]
        public void Any()
        {
            // arrange
            var input = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = true;

            // action
            var output = LogicalFunction<bool>.Any(input);

            // assert
            Assert.AreEqual(true, output);
        }

        [TestMethod]
        public void FillCountTrueAxis()
        {
            // arrange
            var input = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = false;
            input[new[] { 1, 0 }] = false;
            input[new[] { 1, 3 }] = false;

            // action
            var output0 = LogicalFunction<bool>.FillCountTrueAxis(0, input);
            var output1 = LogicalFunction<bool>.FillCountTrueAxis(1, input);

            // assert
            Assert.AreEqual(1, output0[0].Value);
            Assert.AreEqual(2, output1[1].Value);
        }

        [TestMethod]
        public void CountTrueNdArray()
        {
            // arrange
            var input = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = false;
            input[new[] { 1, 0 }] = false;
            input[new[] { 1, 3 }] = false;

            // action
            var output = LogicalFunction<bool>.CountTrueNdArray(input);

            // assert
            Assert.AreEqual(5, output[0].Value);
        }

        [TestMethod]
        public void CountTrue()
        {
            // arrange
            var input = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = false;
            input[new[] { 1, 0 }] = false;
            input[new[] { 1, 3 }] = false;

            // action
            var output = LogicalFunction<bool>.CountTrue(input);

            // assert
            Assert.AreEqual(5, output);
        }

        [TestMethod]
        public void IfThenElse()
        {
            // arrange
            var condition = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 4 });
            var ifTrue = NdArray<int>.Ones(HostDevice.Instance, new int[] { 4 });
            var ifFalse = NdArray<int>.Zeros(HostDevice.Instance, new int[] { 4 });

            condition[new[] { 0 }] = false;
            condition[new[] { 2 }] = false;

            // action
            var output = LogicalFunction<int>.IfThenElse(condition, ifTrue, ifFalse);

            // assert
            Assert.AreEqual(0, output[0].Value);
            Assert.AreEqual(1, output[1].Value);
            Assert.AreEqual(0, output[2].Value);
            Assert.AreEqual(1, output[3].Value);
        }
    }
}