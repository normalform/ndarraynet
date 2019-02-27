// <copyright file="HostStorageTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArrayNet;
    using System;
    using Xunit;

    public class HostStorageTests
    {
        [Fact]
        public void HostStorage_WithMaximumSize_ThrowException()
        {
            // arrange
            var size = int.MaxValue + 1L;

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new HostStorage<int>(size));
            Assert.Contains("Cannot create host NdArray storage for 2147483648 elements, the current limit is 2147483647 elements.", exception.Message);
        }

        [Fact]
        public void Backend()
        {
            // arrange
            var data = new int[100];
            var hostStorage = new HostStorage<int>(data);
            var layout = new Layout(new[] { 10, 10 }, 0, new[] { 10, 1 });

            // action
            var hostBackend = hostStorage.Backend(layout);

            // assert
            Assert.IsType<HostBackend<int>>(hostBackend);
        }

        [Fact]
        public void Pin()
        {
            // arrange
            var data = new int[100];
            var hostStorage = new HostStorage<int>(data);

            // action
            using (var memory = hostStorage.Pin())
            {
                // assert
                Assert.IsType<PinnedMemory>(memory);
            }
        }
    }
}