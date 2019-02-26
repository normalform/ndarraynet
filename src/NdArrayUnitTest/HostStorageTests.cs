// <copyright file="HostStorageTests.cs" company="NdArrayNet">
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
            Assert.Equal("Specified argument was out of the range of valid values.\r\nParameter name: Cannot create host NdArray storage for 2147483648 elements, the current limit is 2147483647 elements.", exception.Message);
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
            var layout = new Layout(new[] { 10, 10 }, 0, new[] { 10, 1 });

            // action
            using (var memory = hostStorage.Pin())
            {
                // assert
                Assert.IsType<PinnedMemory>(memory);
            }
        }
    }
}