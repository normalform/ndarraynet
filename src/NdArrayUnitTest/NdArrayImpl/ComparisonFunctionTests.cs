// <copyright file="ComparisonFunctionTests.cs" company="NdArrayNet">
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
    using Moq;
    using NdArray.NdArrayImpl;
    using NdArrayNet;
    using Xunit;

    public class ComparisonFunctionTests
    {
        [Fact]
        public void FillEqual()
        {
            // arrange
            var mockSrc1 = new Mock<IFrontend<int>>();
            var mockSrc2 = new Mock<IFrontend<int>>();
            var mockBackend = new Mock<IBackend<bool>>();
            var mockFrontend = new Mock<IFrontend<bool>>();
            mockFrontend.Setup(m => m.PrepareElemwiseSources(It.IsAny<IFrontend<int>>(), It.IsAny<IFrontend<int>>())).Returns((mockSrc1.Object, mockSrc2.Object));
            mockFrontend.SetupGet(m => m.Backend).Returns(mockBackend.Object);

            var comparisonFunction = new ComparisonFunction();

            // action
            comparisonFunction.FillEqual(mockFrontend.Object, mockSrc1.Object, mockSrc2.Object);

            // assert
            mockFrontend.Verify(m => m.PrepareElemwiseSources(mockSrc1.Object, mockSrc2.Object));
            mockBackend.Verify(m => m.Equal(mockFrontend.Object, mockSrc1.Object, mockSrc2.Object));
            mockFrontend.VerifyAll();
        }

        [Fact]
        public void Equal()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var sourceA = NdArray<int>.Arange(config, 0, 10, 1);
            var sourceB = NdArray<int>.Arange(config, 0, 10, 1);
            var resultToReturn = NdArray<bool>.Zeros(config, new[] { 10 });

            var mockStaticHelper = new Mock<IStaticMethod>();
            mockStaticHelper.Setup(m => m.PrepareElemwise<bool, int, int>(It.IsAny<NdArray<int>>(), It.IsAny<NdArray<int>>(), Order.RowMajor)).Returns((resultToReturn, sourceA, sourceB));

            // action
            var result = ComparisonFunction.Equal(mockStaticHelper.Object, sourceA, sourceB);

            // assert
            Assert.Equal(new[] { 10 }, result.Shape);
            mockStaticHelper.Verify(m => m.PrepareElemwise<bool, int, int>(sourceA, sourceB, Order.RowMajor));
        }

        [Fact]
        public void FillNotEqual()
        {
            // arrange
            var mockSrc1 = new Mock<IFrontend<int>>();
            var mockSrc2 = new Mock<IFrontend<int>>();
            var mockBackend = new Mock<IBackend<bool>>();
            var mockFrontend = new Mock<IFrontend<bool>>();
            mockFrontend.Setup(m => m.PrepareElemwiseSources(It.IsAny<IFrontend<int>>(), It.IsAny<IFrontend<int>>())).Returns((mockSrc1.Object, mockSrc2.Object));
            mockFrontend.SetupGet(m => m.Backend).Returns(mockBackend.Object);

            var comparisonFunction = new ComparisonFunction();

            // action
            comparisonFunction.FillNotEqual(mockFrontend.Object, mockSrc1.Object, mockSrc2.Object);

            // assert
            mockFrontend.Verify(m => m.PrepareElemwiseSources(mockSrc1.Object, mockSrc2.Object));
            mockBackend.Verify(m => m.NotEqual(mockFrontend.Object, mockSrc1.Object, mockSrc2.Object));
        }

        [Fact]
        public void NotEqual()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var sourceA = NdArray<int>.Arange(config,0, 10, 1);
            var sourceB = NdArray<int>.Arange(config,0, 10, 1);

            // action
            var result = ComparisonFunction.NotEqual(sourceA, sourceB);

            // assert
            Assert.Equal(new[] { 10 }, result.Shape);
        }

        [Fact]
        public void FillLess()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var sourceA = NdArray<int>.Arange(config,0, 10, 1);
            var sourceB = NdArray<int>.Arange(config,0, 10, 1);
            var result = NdArray<bool>.Zeros(config,new[] { 10 });

            // action
            ComparisonFunction.FillLess(result, sourceA, sourceB);

            // assert
            Assert.Equal(new[] { 10 }, result.Shape);
        }

        [Fact]
        public void Less()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var sourceA = NdArray<int>.Arange(config,0, 10, 1);
            var sourceB = NdArray<int>.Arange(config,0, 10, 1);

            // action
            var result = ComparisonFunction.Less(sourceA, sourceB);

            // assert
            Assert.Equal(new[] { 10 }, result.Shape);
        }

        [Fact]
        public void FillLessOrEqual()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var sourceA = NdArray<int>.Arange(config,0, 10, 1);
            var sourceB = NdArray<int>.Arange(config,0, 10, 1);
            var result = NdArray<bool>.Zeros(config,new[] { 10 });

            // action
            ComparisonFunction.FillLessOrEqual(result, sourceA, sourceB);

            // assert
            Assert.Equal(new[] { 10 }, result.Shape);
        }

        [Fact]
        public void LessOrEqual()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var sourceA = NdArray<int>.Arange(config,0, 10, 1);
            var sourceB = NdArray<int>.Arange(config,0, 10, 1);

            // action
            var result = ComparisonFunction.LessOrEqual(sourceA, sourceB);

            // assert
            Assert.Equal(new[] { 10 }, result.Shape);
        }

        [Fact]
        public void FillGreater()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var sourceA = NdArray<int>.Arange(config,0, 10, 1);
            var sourceB = NdArray<int>.Arange(config,0, 10, 1);
            var result = NdArray<bool>.Zeros(config,new[] { 10 });

            // action
            ComparisonFunction.FillGreater(result, sourceA, sourceB);

            // assert
            Assert.Equal(new[] { 10 }, result.Shape);
        }

        [Fact]
        public void Greater()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var sourceA = NdArray<int>.Arange(config,0, 10, 1);
            var sourceB = NdArray<int>.Arange(config,0, 10, 1);

            // action
            var result = ComparisonFunction.Greater(sourceA, sourceB);

            // assert
            Assert.Equal(new[] { 10 }, result.Shape);
        }

        [Fact]
        public void FillGreaterOrEqual()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var sourceA = NdArray<int>.Arange(config,0, 10, 1);
            var sourceB = NdArray<int>.Arange(config,0, 10, 1);
            var result = NdArray<bool>.Zeros(config,new[] { 10 });

            // action
            ComparisonFunction.FillGreaterOrEqual(result, sourceA, sourceB);

            // assert
            Assert.Equal(new[] { 10 }, result.Shape);
        }

        [Fact]
        public void GreaterOrEqual()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var sourceA = NdArray<int>.Arange(config,0, 10, 1);
            var sourceB = NdArray<int>.Arange(config,0, 10, 1);

            // action
            var result = ComparisonFunction.GreaterOrEqual(sourceA, sourceB);

            // assert
            Assert.Equal(new[] { 10 }, result.Shape);
        }

        [Fact]
        public void IsClose_SameIntVectors_ReturnTrues()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var source = NdArray<int>.Arange(config,0, 10, 1);

            // action
            var close = ComparisonFunction.IsClose(source, source);

            // assert
            Assert.Equal(new[] { 10 }, close.Shape);
        }

        [Fact]
        public void IsClose_SameDoubleVectors_ReturnTrues()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var source = NdArray<double>.Arange(config,0, 10, 1);

            // action
            var close = ComparisonFunction.IsClose(source, source);

            // assert
            Assert.Equal(new[] { 10 }, close.Shape);
        }

        [Fact]
        public void IsClose_DifferentDoubleVectors_ReturnFalses()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var source = NdArray<double>.Arange(config,0, 10, 1);

            // action
            var close = ComparisonFunction.IsClose(source, source + 1.0);

            // assert
            Assert.Equal(new[] { 10 }, close.Shape);
        }

        [Fact]
        public void IsClose_DifferentDoubleVectorsWithBigTolerence_ReturnTrue()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var source = NdArray<double>.Arange(config,0, 10, 1);

            // action
            var close = ComparisonFunction.IsClose(source, source + 1.0, 2.0);

            // assert
            Assert.Equal(new[] { 10 }, close.Shape);
        }

        [Fact]
        public void IsClose_CloseDoubleVectors_ReturnTrue()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var sourceA = NdArray<double>.Arange(config,0, 10, 1);
            var sourceB = NdArray<double>.Arange(config,0, 10, 1) + 1e-100;

            // action
            var close = ComparisonFunction.IsClose(sourceA, sourceB);

            // assert
            Assert.Equal(new[] { 10 }, close.Shape);
        }

        [Fact]
        public void AlmostEqual_SameIntVectors_ReturnTrue()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var sourceA = NdArray<int>.Zeros(config,new[] { 2, 3, 4 });
            var sourceB = NdArray<int>.Zeros(config,new[] { 2, 3, 4 });

            // action
            var almostEqual = ComparisonFunction.AlmostEqual(sourceA, sourceB);

            // assert
            Assert.True(almostEqual);
        }

        [Fact]
        public void AlmostEqual_DifferentIntVectors_ReturnFalse()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var sourceA = NdArray<int>.Zeros(config,new[] { 2, 3, 4 });
            var sourceB = NdArray<int>.Zeros(config,new[] { 2, 3, 4 }) + 1;

            // action
            var almostEqual = ComparisonFunction.AlmostEqual(sourceA, sourceB);

            // assert
            Assert.False(almostEqual);
        }

        [Fact]
        public void FillIsFinite()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var source = NdArray<int>.Zeros(config,new[] { 2, 3, 4 });
            var result = NdArray<bool>.Ones(config,new[] { 2, 3, 4 });

            // action
            ComparisonFunction.FillIsFinite(result, source);

            // assert
            Assert.True(NdArray<int>.All(result));
        }

        [Fact]
        public void IsFinite()
        {
            // arrange
            var config = DefaultConfig.Instance;
            var source = NdArray<int>.Zeros(config,new[] { 2, 3, 4 });

            // action
            var result = ComparisonFunction.IsFinite(source);

            // assert
            Assert.True(NdArray<int>.All(result));
        }
    }
}