﻿// <copyright file="HostBackendTests.cs" company="NdArrayNet">
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
    using Moq;
    using NdArrayNet;
    using System.Linq;

    [TestClass]
    public class HostBackendTests
    {
        [TestMethod]
        public void GetIndex()
        {
            // arange
            var layout = new Layout(new[] { 3, 4 }, 5, new[] { 4, 1 });
            var hostStorageMock = new Mock<IHostStorage<int>>();
            hostStorageMock.SetupGet(m => m.Data).Returns(Enumerable.Range(0, 12).ToArray());
            var hostBackend = new HostBackend<int>(layout, hostStorageMock.Object);

            // action
            var index = new[] { 1, 2 };
            var val = hostBackend[index];

            // assert
            // 5 + (4 * 1) + 2
            Assert.AreEqual(11, val);
        }

        [TestMethod]
        public void SetIndex()
        {
            // arange
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
            Assert.AreEqual(999, memory[11]);
            Assert.AreEqual(999, memory.Aggregate(0, (a, b) => a + b));
        }
    }
}