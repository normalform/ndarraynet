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
    using NdArray.NdFunction;
    using NdArrayNet;
    using System;
    using System.Linq;
    using Xunit;

    public class IndexFunctionTests
    {
        [Fact]
        public void AllIndex()
        {
            // arrange
            var source = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 1, 3 });

            // action
            var output = IndexFunction<int>.AllIndex(source);

            // assert
            Assert.Equal(new[] { 0, 0, 0 }, output[0]);
            Assert.Equal(new[] { 0, 0, 1 }, output[1]);
            Assert.Equal(new[] { 0, 0, 2 }, output[2]);
            Assert.Equal(new[] { 1, 0, 0 }, output[3]);
            Assert.Equal(new[] { 1, 0, 1 }, output[4]);
            Assert.Equal(new[] { 1, 0, 2 }, output[5]);
        }

        [Fact]
        public void AllElements()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var output = IndexFunction<int>.AllElements(source);

            // assert
            Assert.Equal(Enumerable.Range(0, 9).ToArray(), output);
        }

        [Fact]
        public void ArgMaxAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = IndexFunction<int>.ArgMaxAxis(1, source);

            // assert
            Assert.Equal(3, output[0].Value);
            Assert.Equal(3, output[1].Value);
        }

        [Fact]
        public void ArgMax()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = IndexFunction<int>.ArgMax(source);

            // assert
            Assert.Equal(new[] { 1, 3 }, output);
        }

        [Fact]
        public void ArgMinAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = IndexFunction<int>.ArgMinAxis(1, source);

            // assert
            Assert.Equal(0, output[0].Value);
            Assert.Equal(0, output[1].Value);
        }

        [Fact]
        public void ArgMin()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = IndexFunction<int>.ArgMin(source);

            // assert
            Assert.Equal(new[] { 0, 0 }, output);
        }

        [Fact]
        public void FindAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 0, 8, 1).Reshape(new[] { 2, 4 });

            // action
            var output = IndexFunction<int>.FindAxis(2, 1, source);

            // assert
            Assert.Equal(2, output[0].Value);
            Assert.Equal(SpecialIdx.NotFound, output[1].Value);
        }

        [Fact]
        public void TryFind()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });
            source[new[] { 1, 3 }] = 2;

            // action
            var output = IndexFunction<int>.TryFind(2, source);

            // assert
            Assert.Equal(new[] { 0, 1 }, output);
        }

        [Fact]
        public void TryFind_NotFound()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = IndexFunction<int>.TryFind(10, source);

            // assert
            Assert.True(output.Length == 0);
        }

        [Fact]
        public void Find()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });
            source[new[] { 1, 3 }] = 2;

            // action
            var output = IndexFunction<int>.Find(2, source);

            // assert
            Assert.Equal(new[] { 0, 1 }, output);
        }

        [Fact]
        public void Find_NotFound_ThrowException()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => IndexFunction<int>.Find(10, source));
            Assert.Equal("Value 10 was not found in specifed NdArray.", exception.Message);
        }

        [Fact]
        public void CountTrueAxis()
        {
            // arrange
            var source = NdArray<bool>.Zeros(ConfigManager.Instance, new[] { 2, 4 });
            source[new[] { 0, 0 }] = true;
            source[new[] { 0, 2 }] = true;
            source[new[] { 1, 1 }] = true;
            source[new[] { 1, 2 }] = true;

            // action
            var output = IndexFunction<bool>.CountTrueAxis(1, source);

            // assert
            Assert.Equal(2, output[0].Value);
            Assert.Equal(2, output[1].Value);
        }

        [Fact]
        public void CountTrueNdArray()
        {
            // arrange
            var source = NdArray<bool>.Zeros(ConfigManager.Instance, new[] { 2, 4 });
            source[new[] { 0, 0 }] = true;
            source[new[] { 0, 2 }] = true;
            source[new[] { 1, 1 }] = true;
            source[new[] { 1, 2 }] = true;

            // action
            var output = IndexFunction<bool>.CountTrueNdArray(source);

            // assert
            Assert.Equal(4, output[0].Value);
        }

        [Fact]
        public void CountTrue()
        {
            // arrange
            var source = NdArray<bool>.Zeros(ConfigManager.Instance, new[] { 2, 4 });
            source[new[] { 0, 0 }] = true;
            source[new[] { 0, 2 }] = true;
            source[new[] { 1, 1 }] = true;
            source[new[] { 1, 2 }] = true;

            // action
            var output = IndexFunction<bool>.CountTrue(source);

            // assert
            Assert.Equal(4, output);
        }

        [Fact]
        public void TrueIndices()
        {
            // arrange
            var source = NdArray<bool>.Zeros(ConfigManager.Instance, new[] { 2, 4 });
            source[new[] { 0, 0 }] = true;
            source[new[] { 0, 2 }] = true;
            source[new[] { 1, 1 }] = true;
            source[new[] { 1, 2 }] = true;

            // action
            var output = IndexFunction<bool>.TrueIndices(source);

            // assert
            Assert.Equal(0, output[new[] { 0, 0 }]);
            Assert.Equal(0, output[new[] { 0, 1 }]);
            Assert.Equal(0, output[new[] { 1, 0 }]);
            Assert.Equal(2, output[new[] { 1, 1 }]);
            Assert.Equal(1, output[new[] { 2, 0 }]);
            Assert.Equal(1, output[new[] { 2, 1 }]);
            Assert.Equal(1, output[new[] { 3, 0 }]);
            Assert.Equal(2, output[new[] { 3, 1 }]);
        }
    }
}