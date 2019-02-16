// <copyright file="RangeTests.cs" company="NdArrayNet">
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

    [TestClass]
    public class RangeTests
    {
        [TestMethod]
        public void Range_ReturnRangeTypeAndStartStopStep()
        {
            // arrange
            var range = new Range(10, 30, 2);

            // action
            var rangeType = range.Type;

            // assert
            Assert.AreEqual(RangeType.Range, rangeType);
            Assert.AreEqual(10, range.Start);
            Assert.AreEqual(30, range.Stop);
            Assert.AreEqual(2, range.Step);
        }

        [TestMethod]
        public void Elem_ReturnElemRangeTypeAndPosition()
        {
            // arrange
            var elem = new Elem(100);

            // action
            var rangeType = elem.Type;

            // assert
            Assert.AreEqual(RangeType.Elem, rangeType);
            Assert.AreEqual(100, elem.Pos);
        }

        [TestMethod]
        public void NewAxis_ReturnNewAxisRangeType()
        {
            // arrange
            var newAxis = new NewAxis();

            // action
            var rangeType = newAxis.Type;

            // assert
            Assert.AreEqual(RangeType.NewAxis, rangeType);
        }

        [TestMethod]
        public void AllFill_ReturnAllFillRangeType()
        {
            // arrange
            var allFill = new AllFill();

            // action
            var rangeType = allFill.Type;

            // assert
            Assert.AreEqual(RangeType.AllFill, rangeType);
        }
    }
}