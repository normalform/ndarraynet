// <copyright file="RangeTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArrayNet;
    using Xunit;

    public class RangeTests
    {
        [Fact]
        public void Range_ReturnRangeTypeAndStartStopStep()
        {
            // arrange
            var range = new Range(10, 30, 2);

            // action
            var rangeType = range.Type;

            // assert
            Assert.Equal(RangeType.Range, rangeType);
            Assert.Equal(10, range.Start);
            Assert.Equal(30, range.Stop);
            Assert.Equal(2, range.Step);
        }

        [Fact]
        public void Elem_ReturnElemRangeTypeAndPosition()
        {
            // arrange
            var elem = new Elem(100);

            // action
            var rangeType = elem.Type;

            // assert
            Assert.Equal(RangeType.Elem, rangeType);
            Assert.Equal(100, elem.Pos);
        }

        [Fact]
        public void NewAxis_ReturnNewAxisRangeType()
        {
            // arrange
            var newAxis = new NewAxis();

            // action
            var rangeType = newAxis.Type;

            // assert
            Assert.Equal(RangeType.NewAxis, rangeType);
        }

        [Fact]
        public void AllFill_ReturnAllFillRangeType()
        {
            // arrange
            var allFill = new AllFill();

            // action
            var rangeType = allFill.Type;

            // assert
            Assert.Equal(RangeType.AllFill, rangeType);
        }
    }
}