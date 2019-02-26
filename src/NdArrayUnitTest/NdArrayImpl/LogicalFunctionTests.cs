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
    using NdArray.NdArrayImpl;
    using NdArrayNet;
    using Xunit;

    public class LogicalFunctionTests
    {
        [Fact]
        public void Negate()
        {
            // arrange
            var input = NdArray<bool>.Zeros(DefaultConfig.Instance, new int[] { 10 });

            // action
            var output = LogicalFunction<bool>.Negate(input);

            // assert
            Assert.True(NdArray<bool>.All(output));
        }

        [Fact]
        public void And()
        {
            // arrange
            var input1 = NdArray<bool>.Zeros(DefaultConfig.Instance, new int[] { 4 });
            var input2 = NdArray<bool>.Ones(DefaultConfig.Instance, new int[] { 4 });

            // action
            var output = LogicalFunction<bool>.And(input1, input2);

            // assert
            var expected = NdArray<bool>.Zeros(DefaultConfig.Instance, new int[] { 4 });
            var result = expected == output;
            Assert.True(NdArray<bool>.All(result));
        }

        [Fact]
        public void Or()
        {
            // arrange
            var input1 = NdArray<bool>.Zeros(DefaultConfig.Instance, new int[] { 4 });
            var input2 = NdArray<bool>.Ones(DefaultConfig.Instance, new int[] { 4 });

            // action
            var output = LogicalFunction<bool>.Or(input1, input2);

            // assert
            var expected = NdArray<bool>.Ones(DefaultConfig.Instance, new int[] { 4 });
            var result = expected == output;
            Assert.True(NdArray<bool>.All(result));
        }

        [Fact]
        public void Xor()
        {
            // arrange
            var input1 = NdArray<bool>.Zeros(DefaultConfig.Instance, new int[] { 2 });
            var input2 = NdArray<bool>.Ones(DefaultConfig.Instance, new int[] { 2 });
            input2[0].Value = false;

            // action
            var output = LogicalFunction<bool>.Xor(input1, input2);

            // assert
            Assert.False(output[0].Value);
            Assert.True(output[1].Value);
        }

        [Fact]
        public void AllAxis()
        {
            // arrange
            var input = NdArray<bool>.Ones(DefaultConfig.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = false;

            // action
            var output0 = LogicalFunction<bool>.AllAxis(0, input);
            var output1 = LogicalFunction<bool>.AllAxis(1, input);

            // assert
            Assert.True(output0[0].Value);
            Assert.True(output1[1].Value);
        }

        [Fact]
        public void AllNdArray()
        {
            // arrange
            var input = NdArray<bool>.Ones(DefaultConfig.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = false;

            // action
            var output = LogicalFunction<bool>.AllNdArray(input);

            // assert
            Assert.False(output[0].Value);
        }

        [Fact]
        public void All()
        {
            // arrange
            var input = NdArray<bool>.Ones(DefaultConfig.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = false;

            // action
            var output = LogicalFunction<bool>.All(input);

            // assert
            Assert.False(output);
        }

        [Fact]
        public void AnyAxis()
        {
            // arrange
            var input = NdArray<bool>.Zeros(DefaultConfig.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = true;

            // action
            var output0 = LogicalFunction<bool>.AnyAxis(0, input);
            var output1 = LogicalFunction<bool>.AnyAxis(1, input);

            // assert
            Assert.True(output0[1].Value);
            Assert.False(output1[1].Value);
        }

        [Fact]
        public void AnyNdArray()
        {
            // arrange
            var input = NdArray<bool>.Zeros(DefaultConfig.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = true;

            // action
            var output = LogicalFunction<bool>.AnyNdArray(input);

            // assert
            Assert.True(output[0].Value);
        }

        [Fact]
        public void Any()
        {
            // arrange
            var input = NdArray<bool>.Zeros(DefaultConfig.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = true;

            // action
            var output = LogicalFunction<bool>.Any(input);

            // assert
            Assert.True(output);
        }

        [Fact]
        public void CountTrueAxis()
        {
            // arrange
            var input = NdArray<bool>.Ones(DefaultConfig.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = false;
            input[new[] { 1, 0 }] = false;
            input[new[] { 1, 3 }] = false;

            // action
            var output0 = LogicalFunction<bool>.CountTrueAxis(0, input);
            var output1 = LogicalFunction<bool>.CountTrueAxis(1, input);

            // assert
            Assert.Equal(1, output0[0].Value);
            Assert.Equal(2, output1[1].Value);
        }

        [Fact]
        public void CountTrueNdArray()
        {
            // arrange
            var input = NdArray<bool>.Ones(DefaultConfig.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = false;
            input[new[] { 1, 0 }] = false;
            input[new[] { 1, 3 }] = false;

            // action
            var output = LogicalFunction<bool>.CountTrueNdArray(input);

            // assert
            Assert.Equal(5, output[0].Value);
        }

        [Fact]
        public void CountTrue()
        {
            // arrange
            var input = NdArray<bool>.Ones(DefaultConfig.Instance, new int[] { 2, 4 });
            input[new[] { 0, 1 }] = false;
            input[new[] { 1, 0 }] = false;
            input[new[] { 1, 3 }] = false;

            // action
            var output = LogicalFunction<bool>.CountTrue(input);

            // assert
            Assert.Equal(5, output);
        }

        [Fact]
        public void IfThenElse()
        {
            // arrange
            var condition = NdArray<bool>.Ones(DefaultConfig.Instance, new int[] { 4 });
            var ifTrue = NdArray<int>.Ones(DefaultConfig.Instance, new int[] { 4 });
            var ifFalse = NdArray<int>.Zeros(DefaultConfig.Instance, new int[] { 4 });

            condition[new[] { 0 }] = false;
            condition[new[] { 2 }] = false;

            // action
            var output = LogicalFunction<int>.IfThenElse(condition, ifTrue, ifFalse);

            // assert
            Assert.Equal(0, output[0].Value);
            Assert.Equal(1, output[1].Value);
            Assert.Equal(0, output[2].Value);
            Assert.Equal(1, output[3].Value);
        }
    }
}