// <copyright file="RangeFactoryTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArrayNet;
    using Xunit;

    public class RangeFactoryTests
    {
        [Fact]
        public void NewAxis_ReturnNewAxis()
        {
            // arrange & action
            var rng = RangeFactory.NewAxis;

            // assert
            Assert.IsType<NewAxis>(rng);
        }

        [Fact]
        public void AllFill_ReturnAllFill()
        {
            // arrange & action
            var rng = RangeFactory.AllFill;

            // assert
            Assert.IsType<AllFill>(rng);
        }

        [Fact]
        public void Range_ReturnRange()
        {
            // arrange & action
            var rng = RangeFactory.Range(10, 30, 2) as Range;

            // assert
            Assert.IsType<Range>(rng);
            Assert.Equal(10, rng.Start);
            Assert.Equal(30, rng.Stop);
            Assert.Equal(2, rng.Step);
        }

        [Fact]
        public void All_ReturnRangeWithAllZeros()
        {
            // arrange & action
            var rng = RangeFactory.All as Range;

            // assert
            Assert.IsType<Range>(rng);
            Assert.Equal(0, rng.Start);
            Assert.Equal(0, rng.Stop);
            Assert.Equal(0, rng.Step);
        }

        [Fact]
        public void Elem_ReturnElem()
        {
            // arrange & action
            var rng = RangeFactory.Elem(100) as Elem;

            // assert
            Assert.IsType<Elem>(rng);
            Assert.Equal(100, rng.Pos);
        }
    }
}