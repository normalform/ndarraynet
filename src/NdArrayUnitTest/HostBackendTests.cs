// <copyright file="HostBackendTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using Moq;
    using NdArrayNet;
    using System.Linq;
    using Xunit;

    public class HostBackendTests
    {
        [Fact]
        public void GetIndex()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 5, new[] { 4, 1 });
            var hostStorageMock = new Mock<IHostStorage<int>>();
            hostStorageMock.SetupGet(m => m.Data).Returns(Enumerable.Range(0, 12).ToArray());
            var hostBackend = new HostBackend<int>(layout, hostStorageMock.Object);

            // action
            var index = new[] { 1, 2 };
            var val = hostBackend[index];

            // assert
            // 5 + (4 * 1) + 2
            Assert.Equal(11, val);
        }

        [Fact]
        public void SetIndex()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 5, new[] { 4, 1 });
            var hostStorageMock = new Mock<IHostStorage<int>>();
            var memory = new int[12];
            hostStorageMock.SetupGet(m => m.Data).Returns(memory);
            var hostBackend = new HostBackend<int>(layout, hostStorageMock.Object);

            // action
            var index = new[] { 1, 2 };
            hostBackend[index] = 999;

            // assert
            // 5 + (4 * 1) + 2
            Assert.Equal(999, memory[11]);
            Assert.Equal(999, memory.Aggregate(0, (a, b) => a + b));
        }
    }
}