// <copyright file="RangeArgParserTests.cs" company="NdArrayNet">
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
    using System.Linq;

    [TestClass]
    public class RangeArgParserTests
    {
        [TestMethod]
        public void Parse_EmptyArgs_ReturnEmptyRanges()
        {
            // arrange
            var args = new int[] { };

            // action
            var ranges = RangeArgParser.Parse(args.Cast<object>().ToArray());

            // assert
            Assert.AreEqual(0, ranges.Length);
        }

        [TestMethod]
        public void Parse_RangeArgs_ReturnSameRanges()
        {
            // arrange
            var args = new IRange[] { RangeFactory.Elem(9) };

            // action
            var ranges = RangeArgParser.Parse(args);

            // assert
            Assert.AreSame(args[0], ranges[0]);
        }

        [TestMethod]
        public void Parse_SignelInteger_ReturnOneElem()
        {
            // arrange
            var args = new[] { 5 };

            // action
            var ranges = RangeArgParser.Parse(args.Cast<object>().ToArray());

            // assert
            Assert.AreEqual(1, ranges.Length);
            Assert.IsInstanceOfType(ranges[0], typeof(Elem));
            Assert.AreEqual(5, (ranges[0] as Elem).Pos);
        }

        [TestMethod]
        public void Parse_SignelIntegerWithNewAxis_ReturnOneElem()
        {
            // arrange
            var args = new[] { SpecialIdx.NewAxis };

            // action
            var ranges = RangeArgParser.Parse(args.Cast<object>().ToArray());

            // assert
            Assert.AreEqual(1, ranges.Length);
            Assert.IsInstanceOfType(ranges[0], typeof(NewAxis));
        }

        [TestMethod]
        public void Parse_SignelIntegerWithFill_ReturnOneElem()
        {
            // arrange
            var args = new[] { SpecialIdx.Fill };

            // action
            var ranges = RangeArgParser.Parse(args.Cast<object>().ToArray());

            // assert
            Assert.AreEqual(1, ranges.Length);
            Assert.IsInstanceOfType(ranges[0], typeof(AllFill));
        }

        [TestMethod]
        public void Parse_SignelIntegerWithNewAxisAndRanges_ReturnOneElem()
        {
            // arrange
            var args = new[] { SpecialIdx.NewAxis }.Cast<object>().Concat(new IRange[] { RangeFactory.Elem(8) }).ToArray();

            // action
            var ranges = RangeArgParser.Parse(args);

            // assert
            Assert.AreEqual(2, ranges.Length);
            Assert.IsInstanceOfType(ranges[0], typeof(NewAxis));
            Assert.IsInstanceOfType(ranges[1], typeof(Elem));
        }

        [TestMethod]
        public void Parse_SignelIntegerWithFillAndRanges_ReturnOneElem()
        {
            // arrange
            var args = new[] { SpecialIdx.Fill }.Cast<object>().Concat(new IRange[] { RangeFactory.Elem(8) }).ToArray();

            // action
            var ranges = RangeArgParser.Parse(args);

            // assert
            Assert.AreEqual(2, ranges.Length);
            Assert.IsInstanceOfType(ranges[0], typeof(AllFill));
            Assert.IsInstanceOfType(ranges[1], typeof(Elem));
        }

        [TestMethod]
        public void Parse_TwoInteger_ReturnOneElem()
        {
            // arrange
            var args = new[] { 5, 9 };

            // action
            var ranges = RangeArgParser.Parse(args.Cast<object>().ToArray());

            // assert
            Assert.AreEqual(1, ranges.Length);
            Assert.IsInstanceOfType(ranges[0], typeof(Range));

            var range = ranges[0] as Range;
            Assert.AreEqual(5, range.Start);
            Assert.AreEqual(9, range.Stop);
            Assert.AreEqual(1, range.Step);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_TwoIntegerStartIsNewAxis_ThrowException()
        {
            // arrange
            var args = new[] { SpecialIdx.NewAxis, 9 };

            // action
            var _ = RangeArgParser.Parse(args.Cast<object>().ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_TwoIntegerStartIsFill_ThrowException()
        {
            // arrange
            var args = new[] { SpecialIdx.Fill, 9 };

            // action
            var _ = RangeArgParser.Parse(args.Cast<object>().ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_TwoIntegerStepIsNewAxis_ThrowException()
        {
            // arrange
            var args = new[] { 5, SpecialIdx.NewAxis };

            // action
            var _ = RangeArgParser.Parse(args.Cast<object>().ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_TwoIntegerStepIsFill_ThrowException()
        {
            // arrange
            var args = new[] { 5, SpecialIdx.Fill };

            // action
            var _ = RangeArgParser.Parse(args.Cast<object>().ToArray());
        }

        [TestMethod]
        public void Parse_ThreeInteger_ReturnOneElem()
        {
            // arrange
            var args = new[] { 5, 9, 2 };

            // action
            var ranges = RangeArgParser.Parse(args.Cast<object>().ToArray());

            // assert
            Assert.AreEqual(1, ranges.Length);
            Assert.IsInstanceOfType(ranges[0], typeof(Range));

            var range = ranges[0] as Range;
            Assert.AreEqual(5, range.Start);
            Assert.AreEqual(9, range.Stop);
            Assert.AreEqual(2, range.Step);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_ThreeIntegerStartIsNewAxis_ThrowException()
        {
            // arrange
            var args = new[] { SpecialIdx.NewAxis, 9, 2 };

            // action
            var _ = RangeArgParser.Parse(args.Cast<object>().ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_ThreeIntegerStartIsFill_ThrowException()
        {
            // arrange
            var args = new[] { SpecialIdx.Fill, 9, 2 };

            // action
            var _ = RangeArgParser.Parse(args.Cast<object>().ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_ThreeIntegerStopIsNewAxis_ThrowException()
        {
            // arrange
            var args = new[] { 5, SpecialIdx.NewAxis, 2 };

            // action
            var _ = RangeArgParser.Parse(args.Cast<object>().ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_ThreeIntegerStopIsFill_ThrowException()
        {
            // arrange
            var args = new[] { 5, SpecialIdx.Fill, 2 };

            // action
            var _ = RangeArgParser.Parse(args.Cast<object>().ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_ThreeIntegerStepIsNewAxis_ThrowException()
        {
            // arrange
            var args = new[] { 5, 9, SpecialIdx.NewAxis };

            // action
            var _ = RangeArgParser.Parse(args.Cast<object>().ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_ThreeIntegerStepIsFill_ThrowException()
        {
            // arrange
            var args = new[] { 5, 9, SpecialIdx.Fill };

            // action
            var _ = RangeArgParser.Parse(args.Cast<object>().ToArray());
        }

        [TestMethod]
        public void Parse_FourInteger_ReturnOneElem()
        {
            // arrange
            var args = new[] { 5, 9, 2, 7 };

            // action
            var ranges = RangeArgParser.Parse(args.Cast<object>().ToArray());

            // assert
            Assert.AreEqual(2, ranges.Length);
            Assert.IsInstanceOfType(ranges[0], typeof(Range));

            var range = ranges[0] as Range;
            Assert.AreEqual(5, range.Start);
            Assert.AreEqual(9, range.Stop);
            Assert.AreEqual(2, range.Step);

            Assert.IsInstanceOfType(ranges[1], typeof(Elem));
            Assert.AreEqual(7, (ranges[1] as Elem).Pos);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Parse_InvalidType_ThrowException()
        {
            // arrange
            var args = new[] { "INVALID" };

            // action
            var _ = RangeArgParser.Parse(args.Cast<object>().ToArray());
        }
    }
}