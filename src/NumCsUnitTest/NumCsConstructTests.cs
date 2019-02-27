// <copyright file="NumCsConstructTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NumCsUnitTest
{
    using NdArrayNet;
    using Xunit;

    public class NumCsConstructTests
    {
        [Fact]
        public void Arange_IntTypeFullArgs_ReturnIntegerTypeNdArray()
        {
            // arrange & action
            var array = NumCs.Arange(0, 10, 1);

            // assert
            Assert.IsType<NdArray<int>>(array);
        }

        [Fact]
        public void Arange_IntOneElement_ReturnIntegerTypeNdArray()
        {
            // arrange & action
            var array = NumCs.Arange(1);

            // assert
            Assert.IsType<NdArray<int>>(array);
        }

        [Fact]
        public void Arange_IntTypeStopArgOnly_ReturnIntegerTypeNdArray()
        {
            // arrange & action
            var array = NumCs.Arange(10);

            // assert
            Assert.IsType<NdArray<int>>(array);
        }

        [Fact]
        public void Ones_IntTypeScalar_ReturnIntegerTypeNdArray()
        {
            // arrange & action
            var array = NumCs.Ones<int>(new int[] { });

            // assert
            Assert.IsType<NdArray<int>>(array);
        }

        [Fact]
        public void Ones_IntTypeVector_ReturnIntegerTypeNdArray()
        {
            // arrange & action
            var array = NumCs.Ones<int>(new[] { 2, 3, 4, 5 });

            // assert
            Assert.IsType<NdArray<int>>(array);
        }

        [Fact]
        public void OnesLike_IntTypeVector_ReturnIntegerTypeNdArray()
        {
            // arrange
            var template = NumCs.Ones<int>(new[] { 2, 3, 4, 5 });

            // action
            var array = NumCs.OnesLike(template);

            // assert
            Assert.IsType<NdArray<int>>(array);
        }

        [Fact]
        public void Zeros_IntTypeVector_ReturnIntegerTypeNdArray()
        {
            // arrange & action
            var array = NumCs.Zeros<int>(new[] { 2, 3, 4, 5 });

            // assert
            Assert.IsType<NdArray<int>>(array);
        }

        [Fact]
        public void ZerosLike_IntTypeVector_ReturnIntegerTypeNdArray()
        {
            // arrange
            var template = NumCs.Zeros<int>(new[] { 2, 3, 4, 5 });

            // action
            var array = NumCs.ZerosLike(template);

            // assert
            Assert.IsType<NdArray<int>>(array);
        }

        [Fact]
        public void Arange_DoubleTypeFullArgs_ReturnDoubleTypeNdArray()
        {
            // arrange & action
            var array = NumCs.Arange(0.0, 10.0, 1.0);

            // assert
            Assert.IsType<NdArray<double>>(array);
        }

        [Fact]
        public void Arange_DoubleTypeStopArgOnly_ReturnDoubleTypeNdArray()
        {
            // arrange & action
            var array = NumCs.Arange(10.0);

            // assert
            Assert.IsType<NdArray<double>>(array);
        }

        [Fact]
        public void Ones_DoubleTypeVector_ReturnDoubleTypeNdArray()
        {
            // arrange & action
            var array = NumCs.Ones<double>(new[] { 2, 3, 4, 5 });

            // assert
            Assert.IsType<NdArray<double>>(array);
        }

        [Fact]
        public void OnesLike_DoubleTypeVector_ReturnDoubleTypeNdArray()
        {
            // arrange
            var template = NumCs.Ones<double>(new[] { 2, 3, 4, 5 });

            // action
            var array = NumCs.OnesLike(template);

            // assert
            Assert.IsType<NdArray<double>>(array);
        }

        [Fact]
        public void Zeros_DoubleTypeVector_ReturnDoubleTypeNdArray()
        {
            // arrange & action
            var array = NumCs.Zeros<double>(new[] { 2, 3, 4, 5 });

            // assert
            Assert.IsType<NdArray<double>>(array);
        }

        [Fact]
        public void ZerosLike_DoubleTypeVector_ReturnDoubleTypeNdArray()
        {
            // arrange
            var template = NumCs.Zeros<double>(new[] { 2, 3, 4, 5 });

            // action
            var array = NumCs.ZerosLike(template);

            // assert
            Assert.IsType<NdArray<double>>(array);
        }
    }
}