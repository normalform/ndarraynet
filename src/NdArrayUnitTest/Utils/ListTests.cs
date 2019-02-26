// <copyright file="ListTests.cs" company="NdArrayNet">
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

    public class ListTests
    {
        [Fact]
        public void Set_EmptyList_ThrowException()
        {
            // arrange
            var emptyList = new int[] { };

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => List.Set(1, 2, emptyList));
            Assert.Equal("Element index 1 out of bounds 0\r\nParameter name: indexToSet", exception.Message);
        }

        [Fact]
        public void Set_TooBigIndex_ThrowException()
        {
            // arrange
            var emptyList = new int[] { 1 };
            const int BigIndex = 1;
            const int DummyValue = 2;

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => List.Set(BigIndex, DummyValue, emptyList));
            Assert.Equal("Element index 1 out of bounds 1\r\nParameter name: indexToSet", exception.Message);
        }

        [Fact]
        public void Set_ZeroElement()
        {
            // arrange
            var stride = new int[] { 10, 1 };
            const int Elem = 0;
            const int Value = 2;

            // action
            var list = List.Set(Elem, Value, stride);

            // assert
            Assert.Equal(new[] { Value, 1 }, list);
        }

        [Fact]
        public void Set_NoneZeroElement()
        {
            // arrange
            var src = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            const int Index = 2;
            const int Value = 2;

            // action
            var list = List.Set(Index, Value, src);

            // assert
            Assert.Equal(new[] { 9, 8, Value, 6, 5, 4, 3, 2, 1 }, list);
        }

        [Fact]
        public void Without()
        {
            // arrange
            var src = new int[] { 1, 2, 3 };

            // action
            var list = List.Without(1, src);

            // assert
            Assert.Equal(new[] { 1, 3 }, list);
        }

        [Fact]
        public void Insert()
        {
            // arrange
            var src = new int[] { 1, 2, 3 };

            // action
            var list = List.Insert(2, 9, src);

            // assert
            Assert.Equal(new[] { 1, 2, 9, 3 }, list);
        }
    }
}