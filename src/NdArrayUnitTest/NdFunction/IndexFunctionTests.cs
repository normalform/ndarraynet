// <copyright file="IndexFunctionTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
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