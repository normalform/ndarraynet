// <copyright file="ListTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
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
            Assert.Contains("Element index 1 out of bounds 0", exception.Message);
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
            Assert.Contains("Element index 1 out of bounds 1", exception.Message);
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