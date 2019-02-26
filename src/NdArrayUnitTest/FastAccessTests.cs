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
    using NdArrayNet;
    using System;
    using Xunit;

    public class FastAccessTests
    {
        [Fact]
        public void Offset()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 9, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var offset = fastAccess.Offset;

            // assert
            Assert.Equal(9, offset);
        }

        [Fact]
        public void NumDiensions()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var numDiensions = fastAccess.NumDiensions;

            // assert
            Assert.Equal(2, numDiensions);
        }

        [Fact]
        public void NumElements()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var numElements = fastAccess.NumElements;

            // assert
            Assert.Equal(12, numElements);
        }

        [Fact]
        public void IsPosValid_WithValidPos_ReturnTrue()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var valid = fastAccess.IsPosValid(new[] { 1, 2 });

            // assert
            Assert.True(valid);
        }

        [Fact]
        public void IsPosValid_WithInvalidPosWrongLength_ReturnFalse()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var valid = fastAccess.IsPosValid(new[] { 1 });

            // assert
            Assert.False(valid);
        }

        [Fact]
        public void IsPosValid_WithInvalidPosOutOfRangeCase1_ReturnFalse()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var valid = fastAccess.IsPosValid(new[] { 3, 2 });

            // assert
            Assert.False(valid);
        }

        [Fact]
        public void IsPosValid_WithInvalidPosOutOfRangeCase2_ReturnFalse()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var valid = fastAccess.IsPosValid(new[] { 1, 4 });

            // assert
            Assert.False(valid);
        }

        [Fact]
        public void IsPosValid_WithNegativeIndex_ReturnFalse()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var valid = fastAccess.IsPosValid(new[] { -1, 2 });

            // assert
            Assert.False(valid);
        }

        [Fact]
        public void Addr_WithInvalidLength_ThrowException()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 0, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => fastAccess.Addr(new[] { 2 }));
            Assert.Equal("Specified argument was out of the range of valid values.\r\nParameter name: Position [2] has wrong dimensionality for NdArray of shape [3,4].", exception.Message);
        }

        [Fact]
        public void Addr_WithNegativeIndex_ThrowException()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 5, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => fastAccess.Addr(new[] { -1, 0 }));
            Assert.Equal("Specified argument was out of the range of valid values.\r\nParameter name: Position [-1,0] is out of range for NdArray of shape [3,4].", exception.Message);
        }

        [Fact]
        public void Addr_OutOfRange_ThrowException()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 5, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => fastAccess.Addr(new[] { 10, 0 }));
            Assert.Equal("Specified argument was out of the range of valid values.\r\nParameter name: Position [10,0] is out of range for NdArray of shape [3,4].", exception.Message);
        }

        [Fact]
        public void Addr_Zero_ReturnOffset()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 5, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var addr = fastAccess.Addr(new[] { 0, 0 });

            // Assert
            Assert.Equal(5, addr);
        }

        [Fact]
        public void Addr_ValidIndex_ReturnOffset()
        {
            // arrange
            var layout = new Layout(new[] { 3, 4 }, 5, new[] { 4, 1 });
            var fastAccess = new FastAccess(layout);

            // action
            var addr = fastAccess.Addr(new[] { 1, 1 });

            // Assert
            Assert.Equal(10, addr);
        }
    }
}