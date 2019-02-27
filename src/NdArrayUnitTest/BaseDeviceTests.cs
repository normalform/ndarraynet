// <copyright file="BaseDeviceTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArrayNet;
    using System;
    using Xunit;

    public class BaseDeviceTests
    {
        [Fact]
        public void Id_WithDummy_ThrowException()
        {
            // arrange
            var dummy = new DummyBaseDevice();

            // action
            Assert.Throws<NotImplementedException>(() => dummy.Id);
        }

        [Fact]
        public void Zeroed_WithDummy_ThrowException()
        {
            // arrange
            var dummy = new DummyBaseDevice();

            // action
            Assert.Throws<NotImplementedException>(() => dummy.Zeroed);
        }

        [Fact]
        public void Create_WithDummy_ThrowException()
        {
            // arrange
            var dummy = new DummyBaseDevice();

            // action
            Assert.Throws<NotImplementedException>(() => dummy.Create<int>(3));
        }

        internal class DummyBaseDevice : BaseDevice
        {
        }
    }
}