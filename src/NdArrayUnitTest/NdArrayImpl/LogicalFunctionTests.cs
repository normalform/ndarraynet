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
    }
}