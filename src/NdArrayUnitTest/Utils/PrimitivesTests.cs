// <copyright file="PrimitivesTests.cs" company="NdArrayNet">
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

    using NdArrayNet.Utils;

    [TestClass]
    public class PrimitivesTests
    {
        [TestMethod]
        public void Zero_Byte_ReturnByteZero()
        {
            // arrange & action
            var zero = Primitives.Zero<byte>();

            // assert
            Assert.AreEqual(0U, zero);
            Assert.IsInstanceOfType(zero, typeof(byte));
        }

        [TestMethod]
        public void One_Byte_ReturnByteOne()
        {
            // arrange & action
            var one = Primitives.One<byte>();

            // assert
            Assert.AreEqual(1U, one);
            Assert.IsInstanceOfType(one, typeof(byte));
        }

        [TestMethod]
        public void Zero_Sbyte_ReturnSbyteZero()
        {
            // arrange & action
            var zero = Primitives.Zero<sbyte>();

            // assert
            Assert.AreEqual(0, zero);
            Assert.IsInstanceOfType(zero, typeof(sbyte));
        }

        [TestMethod]
        public void One_Sbyte_ReturnSbyteOne()
        {
            // arrange & action
            var one = Primitives.One<sbyte>();

            // assert
            Assert.AreEqual(1, one);
            Assert.IsInstanceOfType(one, typeof(sbyte));
        }

        [TestMethod]
        public void Zero_Short_ReturnShortZero()
        {
            // arrange & action
            var zero = Primitives.Zero<short>();

            // assert
            Assert.AreEqual(0, zero);
            Assert.IsInstanceOfType(zero, typeof(short));
        }

        [TestMethod]
        public void One_Short_ReturnShortOne()
        {
            // arrange & action
            var one = Primitives.One<short>();

            // assert
            Assert.AreEqual(1, one);
            Assert.IsInstanceOfType(one, typeof(short));
        }

        [TestMethod]
        public void Zero_Ushort_ReturnUshortZero()
        {
            // arrange & action
            var zero = Primitives.Zero<ushort>();

            // assert
            Assert.AreEqual(0, zero);
            Assert.IsInstanceOfType(zero, typeof(ushort));
        }

        [TestMethod]
        public void One_Ushort_ReturnUshortOne()
        {
            // arrange & action
            var one = Primitives.One<ushort>();

            // assert
            Assert.AreEqual(1, one);
            Assert.IsInstanceOfType(one, typeof(ushort));
        }

        [TestMethod]
        public void Zero_Int_ReturnIntOne()
        {
            // arrange & action
            var zero = Primitives.Zero<int>();

            // assert
            Assert.AreEqual(0, zero);
            Assert.IsInstanceOfType(zero, typeof(int));
        }

        [TestMethod]
        public void One_Int_ReturnIntOne()
        {
            // arrange & action
            var one = Primitives.One<int>();

            // assert
            Assert.AreEqual(1, one);
            Assert.IsInstanceOfType(one, typeof(int));
        }

        [TestMethod]
        public void Zero_Uint_ReturnUintZero()
        {
            // arrange & action
            var zero = Primitives.Zero<uint>();

            // assert
            Assert.AreEqual(0U, zero);
            Assert.IsInstanceOfType(zero, typeof(uint));
        }

        [TestMethod]
        public void One_Uint_ReturnUintOne()
        {
            // arrange & action
            var one = Primitives.One<uint>();

            // assert
            Assert.AreEqual(1U, one);
            Assert.IsInstanceOfType(one, typeof(uint));
        }

        [TestMethod]
        public void Zero_Long_ReturnLongZero()
        {
            // arrange & action
            var zero = Primitives.Zero<long>();

            // assert
            Assert.AreEqual(0L, zero);
            Assert.IsInstanceOfType(zero, typeof(long));
        }

        [TestMethod]
        public void One_Long_ReturnLongOne()
        {
            // arrange & action
            var one = Primitives.One<long>();

            // assert
            Assert.AreEqual(1L, one);
            Assert.IsInstanceOfType(one, typeof(long));
        }

        [TestMethod]
        public void Zero_Ulong_ReturnUlongZero()
        {
            // arrange & action
            var zero = Primitives.Zero<ulong>();

            // assert
            Assert.AreEqual(0UL, zero);
            Assert.IsInstanceOfType(zero, typeof(ulong));
        }

        [TestMethod]
        public void One_Ulong_ReturnUlongOne()
        {
            // arrange & action
            var one = Primitives.One<ulong>();

            // assert
            Assert.AreEqual(1UL, one);
            Assert.IsInstanceOfType(one, typeof(ulong));
        }

        [TestMethod]
        public void Zero_Float_ReturnFloatZero()
        {
            // arrange & action
            var zero = Primitives.Zero<float>();

            // assert
            Assert.AreEqual(0.0F, zero);
            Assert.IsInstanceOfType(zero, typeof(float));
        }

        [TestMethod]
        public void One_Float_ReturnFloatOne()
        {
            // arrange & action
            var one = Primitives.One<float>();

            // assert
            Assert.AreEqual(1.0F, one);
            Assert.IsInstanceOfType(one, typeof(float));
        }

        [TestMethod]
        public void Zero_Double_ReturnDoubleZero()
        {
            // arrange & action
            var zero = Primitives.Zero<double>();

            // assert
            Assert.AreEqual(0.0, zero);
            Assert.IsInstanceOfType(zero, typeof(double));
        }

        [TestMethod]
        public void One_Double_ReturnDoubleOne()
        {
            // arrange & action
            var one = Primitives.One<double>();

            // assert
            Assert.AreEqual(1.0, one);
            Assert.IsInstanceOfType(one, typeof(double));
        }

        [TestMethod]
        public void Zero_Decimal_ReturnDecimalZero()
        {
            // arrange & action
            var zero = Primitives.Zero<decimal>();

            // assert
            Assert.AreEqual(decimal.Zero, zero);
            Assert.IsInstanceOfType(zero, typeof(decimal));
        }

        [TestMethod]
        public void One_Decimal_ReturnDecimalOne()
        {
            // arrange & action
            var one = Primitives.One<decimal>();

            // assert
            Assert.AreEqual(decimal.One, one);
            Assert.IsInstanceOfType(one, typeof(decimal));
        }
    }
}