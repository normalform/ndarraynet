// <copyright file="FastAccessTests.cs" company="NdArrayNet">
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
    using NdArrayNet;
    using System;

    [TestClass]
    public class FastAccessTests
    {
        [TestMethod]
        public void Offset()
        {
            // arange
            var layout = new Layout(new[] { 3, 4 }, 9, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var offset = fastAccess.Offset;

            // assert
            Assert.AreEqual(9, offset);
        }

        [TestMethod]
        public void NumDiensions()
        {
            // arange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var numDiensions = fastAccess.NumDiensions;

            // assert
            Assert.AreEqual(2, numDiensions);
        }

        [TestMethod]
        public void NumElements()
        {
            // arange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var numElements = fastAccess.NumElements;

            // assert
            Assert.AreEqual(12, numElements);
        }

        [TestMethod]
        public void IsPosValid_WithValidPos_ReturnTrue()
        {
            // arange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var valid = fastAccess.IsPosValid(new[] { 1, 2 });

            // assert
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void IsPosValid_WithInvalidPosWrongLength_ReturnFalse()
        {
            // arange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var valid = fastAccess.IsPosValid(new[] { 1 });

            // assert
            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void IsPosValid_WithInvalidPosOutOfRangeCase1_ReturnFalse()
        {
            // arange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var valid = fastAccess.IsPosValid(new[] { 3, 2 });

            // assert
            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void IsPosValid_WithInvalidPosOutOfRangeCase2_ReturnFalse()
        {
            // arange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var valid = fastAccess.IsPosValid(new[] { 1, 4 });

            // assert
            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void IsPosValid_WithNegativeIndex_ReturnFalse()
        {
            // arange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var valid = fastAccess.IsPosValid(new[] { -1, 2 });

            // assert
            Assert.IsFalse(valid);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Addr_WithInvalidLength_ThrowException()
        {
            // arange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var _ = fastAccess.Addr(new[] { 2 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Addr_WithNegativeIndex_ThrowException()
        {
            // arange
            var layout = new Layout(new[] { 3, 4 }, 5, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var _ = fastAccess.Addr(new[] { -1, 0 });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Addr_OutOfRange_ThrowException()
        {
            // arange
            var layout = new Layout(new[] { 3, 4 }, 5, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var _ = fastAccess.Addr(new[] { 10, 0 });
        }

        [TestMethod]
        public void Addr_Zero_ReturnOffset()
        {
            // arange
            var layout = new Layout(new[] { 3, 4 }, 5, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var addr = fastAccess.Addr(new[] { 0, 0 });

            // Assert
            Assert.AreEqual(5, addr);
        }

        [TestMethod]
        public void Addr_ValidIndex_ReturnOffset()
        {
            // arange
            var layout = new Layout(new[] { 3, 4 }, 5, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var addr = fastAccess.Addr(new[] { 1, 1 });

            // Assert
            Assert.AreEqual(10, addr);
        }
    }
}