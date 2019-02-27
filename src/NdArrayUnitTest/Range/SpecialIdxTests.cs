// <copyright file="SpecialIdxTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArrayNet;
    using Xunit;

    public class SpecialIdxTests
    {
        [Fact]
        public void NewAxis()
        {
            // arrange & action
            var idx = SpecialIdx.NewAxis;

            // assert
            Assert.Equal(int.MinValue + 1, idx);
        }

        [Fact]
        public void Fill()
        {
            // arrange & action
            var idx = SpecialIdx.Fill;

            // assert
            Assert.Equal(int.MinValue + 2, idx);
        }

        [Fact]
        public void Remainder()
        {
            // arrange & action
            var idx = SpecialIdx.Remainder;

            // assert
            Assert.Equal(int.MinValue + 3, idx);
        }

        [Fact]
        public void NotFound()
        {
            // arrange & action
            var idx = SpecialIdx.NotFound;

            // assert
            Assert.Equal(int.MinValue + 4, idx);
        }
    }
}