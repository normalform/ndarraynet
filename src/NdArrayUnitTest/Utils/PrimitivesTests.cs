// <copyright file="PrimitivesTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArrayNet;
    using System;
    using Xunit;

    public class PrimitivesTests
    {
        [Fact]
        public void Zero_Byte_ReturnByteZero()
        {
            // arrange & action
            var zero = Primitives.Zero<byte>();

            // assert
            Assert.Equal(0U, zero);
            Assert.IsType<byte>(zero);
        }

        [Fact]
        public void One_Byte_ReturnByteOne()
        {
            // arrange & action
            var one = Primitives.One<byte>();

            // assert
            Assert.Equal(1U, one);
            Assert.IsType<byte>(one);
        }

        [Fact]
        public void Zero_Sbyte_ReturnSbyteZero()
        {
            // arrange & action
            var zero = Primitives.Zero<sbyte>();

            // assert
            Assert.Equal(0, zero);
            Assert.IsType<sbyte>(zero);
        }

        [Fact]
        public void One_Sbyte_ReturnSbyteOne()
        {
            // arrange & action
            var one = Primitives.One<sbyte>();

            // assert
            Assert.Equal(1, one);
            Assert.IsType<sbyte>(one);
        }

        [Fact]
        public void Zero_Short_ReturnShortZero()
        {
            // arrange & action
            var zero = Primitives.Zero<short>();

            // assert
            Assert.Equal(0, zero);
            Assert.IsType<short>(zero);
        }

        [Fact]
        public void One_Short_ReturnShortOne()
        {
            // arrange & action
            var one = Primitives.One<short>();

            // assert
            Assert.Equal(1, one);
            Assert.IsType<short>(one);
        }

        [Fact]
        public void Zero_Ushort_ReturnUshortZero()
        {
            // arrange & action
            var zero = Primitives.Zero<ushort>();

            // assert
            Assert.Equal(0, zero);
            Assert.IsType<ushort>(zero);
        }

        [Fact]
        public void One_Ushort_ReturnUshortOne()
        {
            // arrange & action
            var one = Primitives.One<ushort>();

            // assert
            Assert.Equal(1, one);
            Assert.IsType<ushort>(one);
        }

        [Fact]
        public void Zero_Int_ReturnIntOne()
        {
            // arrange & action
            var zero = Primitives.Zero<int>();

            // assert
            Assert.Equal(0, zero);
            Assert.IsType<int>(zero);
        }

        [Fact]
        public void One_Int_ReturnIntOne()
        {
            // arrange & action
            var one = Primitives.One<int>();

            // assert
            Assert.Equal(1, one);
            Assert.IsType<int>(one);
        }

        [Fact]
        public void Zero_Uint_ReturnUintZero()
        {
            // arrange & action
            var zero = Primitives.Zero<uint>();

            // assert
            Assert.Equal(0U, zero);
            Assert.IsType<uint>(zero);
        }

        [Fact]
        public void One_Uint_ReturnUintOne()
        {
            // arrange & action
            var one = Primitives.One<uint>();

            // assert
            Assert.Equal(1U, one);
            Assert.IsType<uint>(one);
        }

        [Fact]
        public void Zero_Long_ReturnLongZero()
        {
            // arrange & action
            var zero = Primitives.Zero<long>();

            // assert
            Assert.Equal(0L, zero);
            Assert.IsType<long>(zero);
        }

        [Fact]
        public void One_Long_ReturnLongOne()
        {
            // arrange & action
            var one = Primitives.One<long>();

            // assert
            Assert.Equal(1L, one);
            Assert.IsType<long>(one);
        }

        [Fact]
        public void Zero_Ulong_ReturnUlongZero()
        {
            // arrange & action
            var zero = Primitives.Zero<ulong>();

            // assert
            Assert.Equal(0UL, zero);
            Assert.IsType<ulong>(zero);
        }

        [Fact]
        public void One_Ulong_ReturnUlongOne()
        {
            // arrange & action
            var one = Primitives.One<ulong>();

            // assert
            Assert.Equal(1UL, one);
            Assert.IsType<ulong>(one);
        }

        [Fact]
        public void Zero_Float_ReturnFloatZero()
        {
            // arrange & action
            var zero = Primitives.Zero<float>();

            // assert
            Assert.Equal(0.0F, zero);
            Assert.IsType<float>(zero);
        }

        [Fact]
        public void One_Float_ReturnFloatOne()
        {
            // arrange & action
            var one = Primitives.One<float>();

            // assert
            Assert.Equal(1.0F, one);
            Assert.IsType<float>(one);
        }

        [Fact]
        public void Zero_Double_ReturnDoubleZero()
        {
            // arrange & action
            var zero = Primitives.Zero<double>();

            // assert
            Assert.Equal(0.0, zero);
            Assert.IsType<double>(zero);
        }

        [Fact]
        public void One_Double_ReturnDoubleOne()
        {
            // arrange & action
            var one = Primitives.One<double>();

            // assert
            Assert.Equal(1.0, one);
            Assert.IsType<double>(one);
        }

        [Fact]
        public void Zero_Decimal_ReturnDecimalZero()
        {
            // arrange & action
            var zero = Primitives.Zero<decimal>();

            // assert
            Assert.Equal(decimal.Zero, zero);
            Assert.IsType<decimal>(zero);
        }

        [Fact]
        public void One_Decimal_ReturnDecimalOne()
        {
            // arrange & action
            var one = Primitives.One<decimal>();

            // assert
            Assert.Equal(decimal.One, one);
            Assert.IsType<decimal>(one);
        }

        [Fact]
        public void GetStaticProperty_InvalidMethod_ThrowException()
        {
            // action & assert
            var ex = Assert.Throws<InvalidOperationException>(() => Primitives.GetStaticProperty(typeof(int), "INVALID_METHOD"));
            Assert.Equal("The type Int32 must implement the static property INVALID_METHOD", ex.Message);
        }
    }
}