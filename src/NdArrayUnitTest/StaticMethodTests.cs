// <copyright file="StaticMethodTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArrayNet;
    using System;
    using Xunit;

    public class StaticMethodTests
    {
        [Fact]
        public void PrepareAxisReduceSources_WrongShape_ThrowException()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 0, 8, 1).Reshape(new[] { 2, 4 });
            var target = NdArray<int>.Arange(ConfigManager.Instance, 0, 4, 1).Reshape(new[] { 2, 2 });

            var staticMethod = new StaticMethod();

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => staticMethod.PrepareAxisReduceSources(target, 1, source, null, Order.RowMajor));
            Assert.Equal("Reduction of NdArray [2,4] along axis 1 gives shape [2] but target has shape [2,2].", exception.Message);
        }
    }
}