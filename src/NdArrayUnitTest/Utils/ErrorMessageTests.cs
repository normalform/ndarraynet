// <copyright file="ErrorMessageTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArrayNet;
    using System.Linq;
    using Xunit;

    public class ErrorMessageTests
    {
        [Fact]
        public void ArrayToString()
        {
            // arrange
            var array = new int[] { 1, 2, 3 };

            // action
            var message = ErrorMessage.ArrayToString(array);

            // assert
            Assert.Equal("[1,2,3]", message);
        }

        [Fact]
        public void ShapeToString()
        {
            // arrange
            var array = new int[] { 1, 2, 3, SpecialIdx.Fill, SpecialIdx.NewAxis, SpecialIdx.Remainder };

            // action
            var message = ErrorMessage.ShapeToString(array);

            // assert
            Assert.Equal("[1,2,3,...,NewAxis,Remainder]", message);
        }

        [Fact]
        public void RangeArgsToString_RangeDefault()
        {
            // arrange
            var objects = new[] { RangeFactory.Range(0, 2) };

            // action
            var message = ErrorMessage.RangeArgsToString(objects);

            // assert
            Assert.Equal("[0:2]", message);
        }

        [Fact]
        public void RangeArgsToString_RangeWithStep()
        {
            // arrange
            var objects = new[] { RangeFactory.Range(0, 4, 2) };

            // action
            var message = ErrorMessage.RangeArgsToString(objects);

            // assert
            Assert.Equal("[0:4:2]", message);
        }

        [Fact]
        public void RangeArgsToString_TwoRangesWithStep()
        {
            // arrange
            var objects = new[] { RangeFactory.Range(0, 4, 2), RangeFactory.Range(0, 3) };

            // action
            var message = ErrorMessage.RangeArgsToString(objects);

            // assert
            Assert.Equal("[0:4:2, 0:3]", message);
        }

        [Fact]
        public void RangeArgsToString_Elem()
        {
            // arrange
            var objects = new[] { RangeFactory.Elem(8) };

            // action
            var message = ErrorMessage.RangeArgsToString(objects);

            // assert
            Assert.Equal("[8]", message);
        }

        [Fact]
        public void RangeArgsToString_RangeAll()
        {
            // arrange
            var objects = new[] { RangeFactory.All };

            // action
            var message = ErrorMessage.RangeArgsToString(objects);

            // assert
            Assert.Equal("[:]", message);
        }

        [Fact]
        public void RangeArgsToString_AllFill()
        {
            // arrange
            var objects = new[] { RangeFactory.AllFill };

            // action
            var message = ErrorMessage.RangeArgsToString(objects);

            // assert
            Assert.Equal("[...]", message);
        }

        [Fact]
        public void RangeArgsToString_FillIndex()
        {
            // arrange
            var objects = new int[] { SpecialIdx.Fill }.Cast<object>().ToArray();

            // action
            var message = ErrorMessage.RangeArgsToString(objects);

            // assert
            Assert.Equal("[...]", message);
        }

        [Fact]
        public void RangeArgsToString_NewAxis()
        {
            // arrange
            var objects = new[] { RangeFactory.NewAxis };

            // action
            var message = ErrorMessage.RangeArgsToString(objects);

            // assert
            Assert.Equal("[NewAxis]", message);
        }

        [Fact]
        public void RangeArgsToString_NewAxisIndex()
        {
            // arrange
            var objects = new int[] { SpecialIdx.NewAxis }.Cast<object>().ToArray();

            // action
            var message = ErrorMessage.RangeArgsToString(objects);

            // assert
            Assert.Equal("[NewAxis]", message);
        }

        [Fact]
        public void ObjectsToString()
        {
            // arrange
            var objects = new int[] { 1, 2, 3 }.Cast<object>().ToArray();

            // action
            var message = ErrorMessage.ObjectsToString(objects);

            // assert
            Assert.Equal("[1,2,3]", message);
        }
    }
}