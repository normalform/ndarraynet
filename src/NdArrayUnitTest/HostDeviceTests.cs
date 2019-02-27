// <copyright file="HostDeviceTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArrayNet;
    using Xunit;

    public class HostDeviceTests
    {
        [Fact]
        public void Instance_CreateHostDevice()
        {
            // arrange & action
            var configManager = ConfigManager.Instance;

            // assert
            Assert.IsType<HostDevice>(configManager.GetConfig<int>().Device);
        }

        [Fact]
        public void Id()
        {
            // arrange & action
            var configManager = ConfigManager.Instance;

            // assert
            Assert.Equal("Host", configManager.GetConfig<int>().Device.Id);
        }

        [Fact]
        public void Zeroed()
        {
            // arrange & action
            var configManager = ConfigManager.Instance;

            // assert
            Assert.True(configManager.GetConfig<int>().Device.Zeroed);
        }

        [Fact]
        public void Create()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var storage = configManager.GetConfig<int>().Device.Create<int>(10);

            // assert
            Assert.IsType<HostStorage<int>>(storage);
        }
    }
}