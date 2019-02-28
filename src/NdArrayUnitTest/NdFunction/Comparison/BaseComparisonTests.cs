// <copyright file="BaseComparisonTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using Moq;
    using NdArray.NdFunction;
    using NdArrayNet;
    using Xunit;

    public class BaseComparisonTests
    {
        //[Fact]
        //public void FillEqual()
        //{
        //    // arrange
        //    var mockSrc1 = new Mock<IFrontend<int>>();
        //    var mockSrc2 = new Mock<IFrontend<int>>();
        //    var mockBackend = new Mock<IBackend<bool>>();
        //    var mockFrontend = new Mock<IFrontend<bool>>();
        //    mockFrontend.Setup(m => m.PrepareElemwiseSources(It.IsAny<IFrontend<int>>(), It.IsAny<IFrontend<int>>())).Returns((mockSrc1.Object, mockSrc2.Object));
        //    mockFrontend.SetupGet(m => m.Backend).Returns(mockBackend.Object);

        //    var comparisonFunction = new ComparisonFunction();

        //    // action
        //    comparisonFunction.FillEqual(mockFrontend.Object, mockSrc1.Object, mockSrc2.Object);

        //    // assert
        //    mockFrontend.Verify(m => m.PrepareElemwiseSources(mockSrc1.Object, mockSrc2.Object));
        //    mockBackend.Verify(m => m.Equal(mockFrontend.Object, mockSrc1.Object, mockSrc2.Object));
        //    mockFrontend.VerifyAll();
        //}

        //[Fact]
        //public void Equal()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var sourceA = NdArray<int>.Arange(ConfigManager, 0, 10, 1);
        //    var sourceB = NdArray<int>.Arange(ConfigManager, 0, 10, 1);
        //    var resultToReturn = NdArray<bool>.Zeros(ConfigManager, new[] { 10 });

        //    var mockStaticHelper = new Mock<IStaticMethod>();
        //    mockStaticHelper.Setup(m => m.PrepareElemwise<bool, int, int>(It.IsAny<NdArray<int>>(), It.IsAny<NdArray<int>>(), Order.RowMajor)).Returns((resultToReturn, sourceA, sourceB));

        //    // action
        //    var result = ComparisonFunction.Equal(mockStaticHelper.Object, sourceA, sourceB);

        //    // assert
        //    Assert.Equal(new[] { 10 }, result.Shape);
        //    mockStaticHelper.Verify(m => m.PrepareElemwise<bool, int, int>(sourceA, sourceB, Order.RowMajor));
        //}

        //[Fact]
        //public void FillNotEqual()
        //{
        //    // arrange
        //    var mockSrc1 = new Mock<IFrontend<int>>();
        //    var mockSrc2 = new Mock<IFrontend<int>>();
        //    var mockBackend = new Mock<IBackend<bool>>();
        //    var mockFrontend = new Mock<IFrontend<bool>>();
        //    mockFrontend.Setup(m => m.PrepareElemwiseSources(It.IsAny<IFrontend<int>>(), It.IsAny<IFrontend<int>>())).Returns((mockSrc1.Object, mockSrc2.Object));
        //    mockFrontend.SetupGet(m => m.Backend).Returns(mockBackend.Object);

        //    var comparisonFunction = new ComparisonFunction();

        //    // action
        //    comparisonFunction.FillNotEqual(mockFrontend.Object, mockSrc1.Object, mockSrc2.Object);

        //    // assert
        //    mockFrontend.Verify(m => m.PrepareElemwiseSources(mockSrc1.Object, mockSrc2.Object));
        //    mockBackend.Verify(m => m.NotEqual(mockFrontend.Object, mockSrc1.Object, mockSrc2.Object));
        //}

        //[Fact]
        //public void NotEqual()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var sourceA = NdArray<int>.Arange(ConfigManager,0, 10, 1);
        //    var sourceB = NdArray<int>.Arange(ConfigManager,0, 10, 1);

        //    // action
        //    var result = ComparisonFunction.NotEqual(sourceA, sourceB);

        //    // assert
        //    Assert.Equal(new[] { 10 }, result.Shape);
        //}

        //[Fact]
        //public void FillLess()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var sourceA = NdArray<int>.Arange(ConfigManager,0, 10, 1);
        //    var sourceB = NdArray<int>.Arange(ConfigManager,0, 10, 1);
        //    var result = NdArray<bool>.Zeros(ConfigManager,new[] { 10 });

        //    // action
        //    ComparisonFunction.FillLess(result, sourceA, sourceB);

        //    // assert
        //    Assert.Equal(new[] { 10 }, result.Shape);
        //}

        //[Fact]
        //public void Less()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var sourceA = NdArray<int>.Arange(ConfigManager,0, 10, 1);
        //    var sourceB = NdArray<int>.Arange(ConfigManager,0, 10, 1);

        //    // action
        //    var result = ComparisonFunction.Less(sourceA, sourceB);

        //    // assert
        //    Assert.Equal(new[] { 10 }, result.Shape);
        //}

        //[Fact]
        //public void FillLessOrEqual()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var sourceA = NdArray<int>.Arange(ConfigManager,0, 10, 1);
        //    var sourceB = NdArray<int>.Arange(ConfigManager,0, 10, 1);
        //    var result = NdArray<bool>.Zeros(ConfigManager,new[] { 10 });

        //    // action
        //    ComparisonFunction.FillLessOrEqual(result, sourceA, sourceB);

        //    // assert
        //    Assert.Equal(new[] { 10 }, result.Shape);
        //}

        //[Fact]
        //public void LessOrEqual()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var sourceA = NdArray<int>.Arange(ConfigManager,0, 10, 1);
        //    var sourceB = NdArray<int>.Arange(ConfigManager,0, 10, 1);

        //    // action
        //    var result = ComparisonFunction.LessOrEqual(sourceA, sourceB);

        //    // assert
        //    Assert.Equal(new[] { 10 }, result.Shape);
        //}

        //[Fact]
        //public void FillGreater()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var sourceA = NdArray<int>.Arange(ConfigManager,0, 10, 1);
        //    var sourceB = NdArray<int>.Arange(ConfigManager,0, 10, 1);
        //    var result = NdArray<bool>.Zeros(ConfigManager,new[] { 10 });

        //    // action
        //    ComparisonFunction.FillGreater(result, sourceA, sourceB);

        //    // assert
        //    Assert.Equal(new[] { 10 }, result.Shape);
        //}

        //[Fact]
        //public void Greater()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var sourceA = NdArray<int>.Arange(ConfigManager,0, 10, 1);
        //    var sourceB = NdArray<int>.Arange(ConfigManager,0, 10, 1);

        //    // action
        //    var result = ComparisonFunction.Greater(sourceA, sourceB);

        //    // assert
        //    Assert.Equal(new[] { 10 }, result.Shape);
        //}

        //[Fact]
        //public void FillGreaterOrEqual()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var sourceA = NdArray<int>.Arange(ConfigManager,0, 10, 1);
        //    var sourceB = NdArray<int>.Arange(ConfigManager,0, 10, 1);
        //    var result = NdArray<bool>.Zeros(ConfigManager,new[] { 10 });

        //    // action
        //    ComparisonFunction.FillGreaterOrEqual(result, sourceA, sourceB);

        //    // assert
        //    Assert.Equal(new[] { 10 }, result.Shape);
        //}

        //[Fact]
        //public void GreaterOrEqual()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var sourceA = NdArray<int>.Arange(ConfigManager,0, 10, 1);
        //    var sourceB = NdArray<int>.Arange(ConfigManager,0, 10, 1);

        //    // action
        //    var result = ComparisonFunction.GreaterOrEqual(sourceA, sourceB);

        //    // assert
        //    Assert.Equal(new[] { 10 }, result.Shape);
        //}

        //[Fact]
        //public void IsClose_SameIntVectors_ReturnTrues()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var source = NdArray<int>.Arange(ConfigManager,0, 10, 1);

        //    // action
        //    var close = ComparisonFunction.IsClose(source, source);

        //    // assert
        //    Assert.Equal(new[] { 10 }, close.Shape);
        //}

        //[Fact]
        //public void IsClose_SameDoubleVectors_ReturnTrues()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var source = NdArray<double>.Arange(ConfigManager,0, 10, 1);

        //    // action
        //    var close = ComparisonFunction.IsClose(source, source);

        //    // assert
        //    Assert.Equal(new[] { 10 }, close.Shape);
        //}

        //[Fact]
        //public void IsClose_DifferentDoubleVectors_ReturnFalses()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var source = NdArray<double>.Arange(ConfigManager,0, 10, 1);

        //    // action
        //    var close = ComparisonFunction.IsClose(source, source + 1.0);

        //    // assert
        //    Assert.Equal(new[] { 10 }, close.Shape);
        //}

        //[Fact]
        //public void IsClose_DifferentDoubleVectorsWithBigTolerence_ReturnTrue()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var source = NdArray<double>.Arange(ConfigManager,0, 10, 1);

        //    // action
        //    var close = ComparisonFunction.IsClose(source, source + 1.0, 2.0);

        //    // assert
        //    Assert.Equal(new[] { 10 }, close.Shape);
        //}

        //[Fact]
        //public void IsClose_CloseDoubleVectors_ReturnTrue()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var sourceA = NdArray<double>.Arange(ConfigManager,0, 10, 1);
        //    var sourceB = NdArray<double>.Arange(ConfigManager,0, 10, 1) + 1e-100;

        //    // action
        //    var close = ComparisonFunction.IsClose(sourceA, sourceB);

        //    // assert
        //    Assert.Equal(new[] { 10 }, close.Shape);
        //}

        //[Fact]
        //public void AlmostEqual_SameIntVectors_ReturnTrue()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var sourceA = NdArray<int>.Zeros(ConfigManager,new[] { 2, 3, 4 });
        //    var sourceB = NdArray<int>.Zeros(ConfigManager,new[] { 2, 3, 4 });

        //    // action
        //    var almostEqual = ComparisonFunction.AlmostEqual(sourceA, sourceB);

        //    // assert
        //    Assert.True(almostEqual);
        //}

        //[Fact]
        //public void AlmostEqual_DifferentIntVectors_ReturnFalse()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var sourceA = NdArray<int>.Zeros(ConfigManager,new[] { 2, 3, 4 });
        //    var sourceB = NdArray<int>.Zeros(ConfigManager,new[] { 2, 3, 4 }) + 1;

        //    // action
        //    var almostEqual = ComparisonFunction.AlmostEqual(sourceA, sourceB);

        //    // assert
        //    Assert.False(almostEqual);
        //}

        //[Fact]
        //public void FillIsFinite()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var source = NdArray<int>.Zeros(ConfigManager,new[] { 2, 3, 4 });
        //    var result = NdArray<bool>.Ones(ConfigManager,new[] { 2, 3, 4 });

        //    // action
        //    ComparisonFunction.FillIsFinite(result, source);

        //    // assert
        //    Assert.True(NdArray<int>.All(result));
        //}

        //[Fact]
        //public void IsFinite()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var source = NdArray<int>.Zeros(ConfigManager,new[] { 2, 3, 4 });

        //    // action
        //    var result = ComparisonFunction.IsFinite(source);

        //    // assert
        //    Assert.True(NdArray<int>.All(result));
        //}
    }
}