// <copyright file="RangeArgParserTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArrayNet;
    using System;
    using System.Linq;
    using Xunit;

    public class RangeArgParserTests
    {
        [Fact]
        public void Parse_EmptyArgs_ReturnEmptyRanges()
        {
            // arrange
            var args = new int[] { };

            // action
            var ranges = RangeArgParser.Parse(args.Cast<object>().ToArray());

            // assert
            Assert.Empty(ranges);
        }

        [Fact]
        public void Parse_RangeArgs_ReturnSameRanges()
        {
            // arrange
            var args = new IRange[] { RangeFactory.Elem(9) };

            // action
            var ranges = RangeArgParser.Parse(args);

            // assert
            Assert.Same(args[0], ranges[0]);
        }

        [Fact]
        public void Parse_SignelInteger_ReturnOneElem()
        {
            // arrange
            var args = new[] { 5 };

            // action
            var ranges = RangeArgParser.Parse(args.Cast<object>().ToArray());

            // assert
            Assert.Single(ranges);
            Assert.IsType<Elem>(ranges[0]);
            Assert.Equal(5, (ranges[0] as Elem).Pos);
        }

        [Fact]
        public void Parse_SignelIntegerWithNewAxis_ReturnOneElem()
        {
            // arrange
            var args = new[] { SpecialIdx.NewAxis };

            // action
            var ranges = RangeArgParser.Parse(args.Cast<object>().ToArray());

            // assert
            Assert.Single(ranges);
            Assert.IsType<NewAxis>(ranges[0]);
        }

        [Fact]
        public void Parse_SignelIntegerWithFill_ReturnOneElem()
        {
            // arrange
            var args = new[] { SpecialIdx.Fill };

            // action
            var ranges = RangeArgParser.Parse(args.Cast<object>().ToArray());

            // assert
            Assert.Single(ranges);
            Assert.IsType<AllFill>(ranges[0]);
        }

        [Fact]
        public void Parse_SignelIntegerWithNewAxisAndRanges_ReturnOneElem()
        {
            // arrange
            var args = new[] { SpecialIdx.NewAxis }.Cast<object>().Concat(new IRange[] { RangeFactory.Elem(8) }).ToArray();

            // action
            var ranges = RangeArgParser.Parse(args);

            // assert
            Assert.Equal(2, ranges.Length);
            Assert.IsType<NewAxis>(ranges[0]);
            Assert.IsType<Elem>(ranges[1]);
        }

        [Fact]
        public void Parse_SignelIntegerWithFillAndRanges_ReturnOneElem()
        {
            // arrange
            var args = new[] { SpecialIdx.Fill }.Cast<object>().Concat(new IRange[] { RangeFactory.Elem(8) }).ToArray();

            // action
            var ranges = RangeArgParser.Parse(args);

            // assert
            Assert.Equal(2, ranges.Length);
            Assert.IsType<AllFill>(ranges[0]);
            Assert.IsType<Elem>(ranges[1]);
        }

        [Fact]
        public void Parse_TwoInteger_ReturnOneElem()
        {
            // arrange
            var args = new[] { 5, 9 };

            // action
            var ranges = RangeArgParser.Parse(args.Cast<object>().ToArray());

            // assert
            Assert.Single(ranges);
            Assert.IsType<Range>(ranges[0]);

            var range = ranges[0] as Range;
            Assert.Equal(5, range.Start);
            Assert.Equal(9, range.Stop);
            Assert.Equal(1, range.Step);
        }

        [Fact]
        public void Parse_TwoIntegerStartIsNewAxis_ThrowException()
        {
            // arrange
            var args = new[] { SpecialIdx.NewAxis, 9 };

            // action & assert
            var exception = Assert.Throws<InvalidOperationException>(() => RangeArgParser.Parse(args.Cast<object>().ToArray()));
            Assert.Equal("InvalidArg item Specified items / slices are invalid: [NewAxis, 9].", exception.Message);
        }

        [Fact]
        public void Parse_TwoIntegerStartIsFill_ThrowException()
        {
            // arrange
            var args = new[] { SpecialIdx.Fill, 9 };

            // action & assert
            var exception = Assert.Throws<InvalidOperationException>(() => RangeArgParser.Parse(args.Cast<object>().ToArray()));
            Assert.Equal("InvalidArg item Specified items / slices are invalid: [..., 9].", exception.Message);
        }

        [Fact]
        public void Parse_TwoIntegerStepIsNewAxis_ThrowException()
        {
            // arrange
            var args = new[] { 5, SpecialIdx.NewAxis };

            // action & assert
            var exception = Assert.Throws<InvalidOperationException>(() => RangeArgParser.Parse(args.Cast<object>().ToArray()));
            Assert.Equal("InvalidArg item Specified items / slices are invalid: [5, NewAxis].", exception.Message);
        }

        [Fact]
        public void Parse_TwoIntegerStepIsFill_ThrowException()
        {
            // arrange
            var args = new[] { 5, SpecialIdx.Fill };

            // action & assert
            var exception = Assert.Throws<InvalidOperationException>(() => RangeArgParser.Parse(args.Cast<object>().ToArray()));
            Assert.Equal("InvalidArg item Specified items / slices are invalid: [5, ...].", exception.Message);
        }

        [Fact]
        public void Parse_ThreeInteger_ReturnOneElem()
        {
            // arrange
            var args = new[] { 5, 9, 2 };

            // action
            var ranges = RangeArgParser.Parse(args.Cast<object>().ToArray());

            // assert
            Assert.Single(ranges);
            Assert.IsType<Range>(ranges[0]);

            var range = ranges[0] as Range;
            Assert.Equal(5, range.Start);
            Assert.Equal(9, range.Stop);
            Assert.Equal(2, range.Step);
        }

        [Fact]
        public void Parse_ThreeIntegerStartIsNewAxis_ThrowException()
        {
            // arrange
            var args = new[] { SpecialIdx.NewAxis, 9, 2 };

            // action & assert
            var exception = Assert.Throws<InvalidOperationException>(() => RangeArgParser.Parse(args.Cast<object>().ToArray()));
            Assert.Equal("InvalidArg item Specified items / slices are invalid: [NewAxis, 9, 2].", exception.Message);
        }

        [Fact]
        public void Parse_ThreeIntegerStartIsFill_ThrowException()
        {
            // arrange
            var args = new[] { SpecialIdx.Fill, 9, 2 };

            // action & assert
            var exception = Assert.Throws<InvalidOperationException>(() => RangeArgParser.Parse(args.Cast<object>().ToArray()));
            Assert.Equal("InvalidArg item Specified items / slices are invalid: [..., 9, 2].", exception.Message);
        }

        [Fact]
        public void Parse_ThreeIntegerStopIsNewAxis_ThrowException()
        {
            // arrange
            var args = new[] { 5, SpecialIdx.NewAxis, 2 };

            // action & assert
            var exception = Assert.Throws<InvalidOperationException>(() => RangeArgParser.Parse(args.Cast<object>().ToArray()));
            Assert.Equal("InvalidArg item Specified items / slices are invalid: [5, NewAxis, 2].", exception.Message);
        }

        [Fact]
        public void Parse_ThreeIntegerStopIsFill_ThrowException()
        {
            // arrange
            var args = new[] { 5, SpecialIdx.Fill, 2 };

            // action & assert
            var exception = Assert.Throws<InvalidOperationException>(() => RangeArgParser.Parse(args.Cast<object>().ToArray()));
            Assert.Equal("InvalidArg item Specified items / slices are invalid: [5, ..., 2].", exception.Message);
        }

        [Fact]
        public void Parse_ThreeIntegerStepIsNewAxis_ThrowException()
        {
            // arrange
            var args = new[] { 5, 9, SpecialIdx.NewAxis };

            // action & assert
            var exception = Assert.Throws<InvalidOperationException>(() => RangeArgParser.Parse(args.Cast<object>().ToArray()));
            Assert.Equal("InvalidArg item Specified items / slices are invalid: [5, 9, NewAxis].", exception.Message);
        }

        [Fact]
        public void Parse_ThreeIntegerStepIsFill_ThrowException()
        {
            // arrange
            var args = new[] { 5, 9, SpecialIdx.Fill };

            // action
            // action & assert
            var exception = Assert.Throws<InvalidOperationException>(() => RangeArgParser.Parse(args.Cast<object>().ToArray()));
            Assert.Equal("InvalidArg item Specified items / slices are invalid: [5, 9, ...].", exception.Message);
        }

        [Fact]
        public void Parse_FourInteger_ReturnOneElem()
        {
            // arrange
            var args = new[] { 5, 9, 2, 7 };

            // action
            var ranges = RangeArgParser.Parse(args.Cast<object>().ToArray());

            // assert
            Assert.Equal(2, ranges.Length);
            Assert.IsType<Range>(ranges[0]);

            var range = ranges[0] as Range;
            Assert.Equal(5, range.Start);
            Assert.Equal(9, range.Stop);
            Assert.Equal(2, range.Step);

            Assert.IsType<Elem>(ranges[1]);
            Assert.Equal(7, (ranges[1] as Elem).Pos);
        }

        [Fact]
        public void Parse_InvalidType_ThrowException()
        {
            // arrange
            var args = new[] { "INVALID" };

            // action & assert
            var exception = Assert.Throws<InvalidOperationException>(() => RangeArgParser.Parse(args.Cast<object>().ToArray()));
            Assert.Equal("InvalidArg item Specified items / slices are invalid: [INVALID].", exception.Message);
        }
    }
}