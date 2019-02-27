// <copyright file="DataFunctionTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArray.NdFunction;
    using NdArrayNet;
    using System;
    using Xunit;

    public class DataFunctionTests
    {
        [Fact]
        public void Convert()
        {
            // arrange
            var source = NdArray<int>.Scalar(ConfigManager.Instance, 2);

            // action
            var result = DataFunction<double>.Convert(source).Value;

            // assert
            Assert.IsType<double>(result);
            Assert.Equal(2.0, result);
        }

        [Fact]
        public void Convert_InvalidConvert_ThrowException()
        {
            // arrange
            var source = NdArray<int>.Scalar(ConfigManager.Instance, 2);

            // action
            var exception = Assert.Throws<ArgumentException>(() => DataFunction<string>.Convert(source));
            Assert.Equal("Type 'System.String' cannot be marshaled as an unmanaged structure; no meaningful size or offset can be computed.", exception.Message);
        }
    }
}