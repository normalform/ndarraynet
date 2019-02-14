// <copyright file="RangeFactoryTests.cs" company="NdArrayNet">
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
    public class RangeFactoryTests
    {
        [TestMethod]
        public void NewAxis_ReturnNewAxis()
        {
            // arange & action
            var rng = RangeFactory.NewAxis;

            // assert
            Assert.IsInstanceOfType(rng, typeof(NewAxis));
        }

        [TestMethod]
        public void AllFill_ReturnAllFill()
        {
            // arange & action
            var rng = RangeFactory.AllFill;

            // assert
            Assert.IsInstanceOfType(rng, typeof(AllFill));
        }

        [TestMethod]
        public void Range_ReturnRange()
        {
            // arange & action
            var rng = RangeFactory.Range(10, 30, 2) as Range;

            // assert
            Assert.IsInstanceOfType(rng, typeof(Range));
            Assert.AreEqual(10, rng.Start);
            Assert.AreEqual(30, rng.Stop);
            Assert.AreEqual(2, rng.Step);
        }

        [TestMethod]
        public void All_ReturnRangeWithAllZeros()
        {
            // arange & action
            var rng = RangeFactory.All as Range;

            // assert
            Assert.IsInstanceOfType(rng, typeof(Range));
            Assert.AreEqual(0, rng.Start);
            Assert.AreEqual(0, rng.Stop);
            Assert.AreEqual(0, rng.Step);
        }

        [TestMethod]
        public void Elem_ReturnElem()
        {
            // arange & action
            var rng = RangeFactory.Elem(100) as Elem;

            // assert
            Assert.IsInstanceOfType(rng, typeof(Elem));
            Assert.AreEqual(100, rng.Pos);
        }
   }
}