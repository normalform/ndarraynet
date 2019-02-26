// <copyright file="ErrorMessageTests.cs" company="NdArrayNet">
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