// <copyright file="ComparisonTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using Moq;
    using NdArray.NdFunction.Comparison;
    using NdArrayNet;
    using Xunit;
    using System;
    using System.Reflection;

    public class ComparisonTests
    {
        [Theory]
        [InlineData(typeof(int))]
        public void FillEqual(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(FillEqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }
        
        private void FillEqualTestHelper<T>()
        {
            // arrange
            var backendMock = new Mock<IBackend<bool>>();
            var resultMock = new Mock<IFrontend<bool>>();
            resultMock.SetupGet(m => m.Backend).Returns(backendMock.Object);
            var lhsMock = new Mock<IFrontend<T>>();
            var rhsMock = new Mock<IFrontend<T>>();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object)).Returns((lhsMock.Object, rhsMock.Object));
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            comparison.FillEqual(resultMock.Object, lhsMock.Object, rhsMock.Object);

            // assert
            staticMethodMock.Verify(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
            backendMock.Verify(m => m.Equal(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void Equal(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(EqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        private void EqualTestHelper<T>()
        {
            // arrange
            var backendMock = new Mock<IBackend<bool>>();
            var resultMock = new Mock<IFrontend<bool>>();
            resultMock.SetupGet(m => m.Backend).Returns(backendMock.Object);
            var lhsMock = new Mock<IFrontend<T>>();
            var rhsMock = new Mock<IFrontend<T>>();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.PrepareElemwise<bool, T, T>(lhsMock.Object, rhsMock.Object, Order.RowMajor)).Returns((resultMock.Object, lhsMock.Object, rhsMock.Object));
            staticMethodMock.Setup(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object)).Returns((lhsMock.Object, rhsMock.Object));
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            comparison.Equal(lhsMock.Object, rhsMock.Object);

            // assert
            staticMethodMock.Verify(m => m.PrepareElemwise<bool, T, T>(lhsMock.Object, rhsMock.Object, Order.RowMajor), Times.Once);
            staticMethodMock.Verify(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
            staticMethodMock.Verify(m => m.AssertBool(resultMock.Object));
            backendMock.Verify(m => m.Equal(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void FillNotEqual(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(FillNotEqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        private void FillNotEqualTestHelper<T>()
        {
            // arrange
            var backendMock = new Mock<IBackend<bool>>();
            var resultMock = new Mock<IFrontend<bool>>();
            resultMock.SetupGet(m => m.Backend).Returns(backendMock.Object);
            var lhsMock = new Mock<IFrontend<T>>();
            var rhsMock = new Mock<IFrontend<T>>();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object)).Returns((lhsMock.Object, rhsMock.Object));
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            comparison.FillNotEqual(resultMock.Object, lhsMock.Object, rhsMock.Object);

            // assert
            staticMethodMock.Verify(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
            backendMock.Verify(m => m.NotEqual(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void NotEqual(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(NotEqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        private void NotEqualTestHelper<T>()
        {
            // arrange
            var backendMock = new Mock<IBackend<bool>>();
            var resultMock = new Mock<IFrontend<bool>>();
            resultMock.SetupGet(m => m.Backend).Returns(backendMock.Object);
            var lhsMock = new Mock<IFrontend<T>>();
            var rhsMock = new Mock<IFrontend<T>>();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.PrepareElemwise<bool, T, T>(lhsMock.Object, rhsMock.Object, Order.RowMajor)).Returns((resultMock.Object, lhsMock.Object, rhsMock.Object));
            staticMethodMock.Setup(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object)).Returns((lhsMock.Object, rhsMock.Object));
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            comparison.NotEqual(lhsMock.Object, rhsMock.Object);

            // assert
            staticMethodMock.Verify(m => m.PrepareElemwise<bool, T, T>(lhsMock.Object, rhsMock.Object, Order.RowMajor), Times.Once);
            staticMethodMock.Verify(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
            staticMethodMock.Verify(m => m.AssertBool(resultMock.Object));
            backendMock.Verify(m => m.NotEqual(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void FillLess(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(FillLessTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        private void FillLessTestHelper<T>()
        {
            // arrange
            var backendMock = new Mock<IBackend<bool>>();
            var resultMock = new Mock<IFrontend<bool>>();
            resultMock.SetupGet(m => m.Backend).Returns(backendMock.Object);
            var lhsMock = new Mock<IFrontend<T>>();
            var rhsMock = new Mock<IFrontend<T>>();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object)).Returns((lhsMock.Object, rhsMock.Object));
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            comparison.FillLess(resultMock.Object, lhsMock.Object, rhsMock.Object);

            // assert
            staticMethodMock.Verify(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
            backendMock.Verify(m => m.Less(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void Less(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(LessTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        private void LessTestHelper<T>()
        {
            // arrange
            var backendMock = new Mock<IBackend<bool>>();
            var resultMock = new Mock<IFrontend<bool>>();
            resultMock.SetupGet(m => m.Backend).Returns(backendMock.Object);
            var lhsMock = new Mock<IFrontend<T>>();
            var rhsMock = new Mock<IFrontend<T>>();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.PrepareElemwise<bool, T, T>(lhsMock.Object, rhsMock.Object, Order.RowMajor)).Returns((resultMock.Object, lhsMock.Object, rhsMock.Object));
            staticMethodMock.Setup(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object)).Returns((lhsMock.Object, rhsMock.Object));
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            comparison.Less(lhsMock.Object, rhsMock.Object);

            // assert
            staticMethodMock.Verify(m => m.PrepareElemwise<bool, T, T>(lhsMock.Object, rhsMock.Object, Order.RowMajor), Times.Once);
            staticMethodMock.Verify(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
            staticMethodMock.Verify(m => m.AssertBool(resultMock.Object));
            backendMock.Verify(m => m.Less(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void FillLessOrEqual(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(FillLessOrEqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        private void FillLessOrEqualTestHelper<T>()
        {
            // arrange
            var backendMock = new Mock<IBackend<bool>>();
            var resultMock = new Mock<IFrontend<bool>>();
            resultMock.SetupGet(m => m.Backend).Returns(backendMock.Object);
            var lhsMock = new Mock<IFrontend<T>>();
            var rhsMock = new Mock<IFrontend<T>>();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object)).Returns((lhsMock.Object, rhsMock.Object));
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            comparison.FillLessOrEqual(resultMock.Object, lhsMock.Object, rhsMock.Object);

            // assert
            staticMethodMock.Verify(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
            backendMock.Verify(m => m.LessOrEqual(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void LessOrEqual(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(LessOrEqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        private void LessOrEqualTestHelper<T>()
        {
            // arrange
            var backendMock = new Mock<IBackend<bool>>();
            var resultMock = new Mock<IFrontend<bool>>();
            resultMock.SetupGet(m => m.Backend).Returns(backendMock.Object);
            var lhsMock = new Mock<IFrontend<T>>();
            var rhsMock = new Mock<IFrontend<T>>();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.PrepareElemwise<bool, T, T>(lhsMock.Object, rhsMock.Object, Order.RowMajor)).Returns((resultMock.Object, lhsMock.Object, rhsMock.Object));
            staticMethodMock.Setup(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object)).Returns((lhsMock.Object, rhsMock.Object));
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            comparison.LessOrEqual(lhsMock.Object, rhsMock.Object);

            // assert
            staticMethodMock.Verify(m => m.PrepareElemwise<bool, T, T>(lhsMock.Object, rhsMock.Object, Order.RowMajor), Times.Once);
            staticMethodMock.Verify(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
            staticMethodMock.Verify(m => m.AssertBool(resultMock.Object));
            backendMock.Verify(m => m.LessOrEqual(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void FillGreater(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(FillGreaterTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        private void FillGreaterTestHelper<T>()
        {
            // arrange
            var backendMock = new Mock<IBackend<bool>>();
            var resultMock = new Mock<IFrontend<bool>>();
            resultMock.SetupGet(m => m.Backend).Returns(backendMock.Object);
            var lhsMock = new Mock<IFrontend<T>>();
            var rhsMock = new Mock<IFrontend<T>>();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object)).Returns((lhsMock.Object, rhsMock.Object));
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            comparison.FillGreater(resultMock.Object, lhsMock.Object, rhsMock.Object);

            // assert
            staticMethodMock.Verify(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
            backendMock.Verify(m => m.Greater(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void Greater(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(GreaterTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        private void GreaterTestHelper<T>()
        {
            // arrange
            var backendMock = new Mock<IBackend<bool>>();
            var resultMock = new Mock<IFrontend<bool>>();
            resultMock.SetupGet(m => m.Backend).Returns(backendMock.Object);
            var lhsMock = new Mock<IFrontend<T>>();
            var rhsMock = new Mock<IFrontend<T>>();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.PrepareElemwise<bool, T, T>(lhsMock.Object, rhsMock.Object, Order.RowMajor)).Returns((resultMock.Object, lhsMock.Object, rhsMock.Object));
            staticMethodMock.Setup(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object)).Returns((lhsMock.Object, rhsMock.Object));
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            comparison.Greater(lhsMock.Object, rhsMock.Object);

            // assert
            staticMethodMock.Verify(m => m.PrepareElemwise<bool, T, T>(lhsMock.Object, rhsMock.Object, Order.RowMajor), Times.Once);
            staticMethodMock.Verify(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
            staticMethodMock.Verify(m => m.AssertBool(resultMock.Object));
            backendMock.Verify(m => m.Greater(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void FillGreaterOrEqual(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(FillGreaterOrEqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        private void FillGreaterOrEqualTestHelper<T>()
        {
            // arrange
            var backendMock = new Mock<IBackend<bool>>();
            var resultMock = new Mock<IFrontend<bool>>();
            resultMock.SetupGet(m => m.Backend).Returns(backendMock.Object);
            var lhsMock = new Mock<IFrontend<T>>();
            var rhsMock = new Mock<IFrontend<T>>();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object)).Returns((lhsMock.Object, rhsMock.Object));
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            comparison.FillGreaterOrEqual(resultMock.Object, lhsMock.Object, rhsMock.Object);

            // assert
            staticMethodMock.Verify(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
            backendMock.Verify(m => m.GreaterOrEqual(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void GreaterOrEqual(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(GreaterOrEqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        private void GreaterOrEqualTestHelper<T>()
        {
            // arrange
            var backendMock = new Mock<IBackend<bool>>();
            var resultMock = new Mock<IFrontend<bool>>();
            resultMock.SetupGet(m => m.Backend).Returns(backendMock.Object);
            var lhsMock = new Mock<IFrontend<T>>();
            var rhsMock = new Mock<IFrontend<T>>();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.PrepareElemwise<bool, T, T>(lhsMock.Object, rhsMock.Object, Order.RowMajor)).Returns((resultMock.Object, lhsMock.Object, rhsMock.Object));
            staticMethodMock.Setup(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object)).Returns((lhsMock.Object, rhsMock.Object));
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            comparison.GreaterOrEqual(lhsMock.Object, rhsMock.Object);

            // assert
            staticMethodMock.Verify(m => m.PrepareElemwise<bool, T, T>(lhsMock.Object, rhsMock.Object, Order.RowMajor), Times.Once);
            staticMethodMock.Verify(m => m.PrepareElemwiseSources(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
            staticMethodMock.Verify(m => m.AssertBool(resultMock.Object));
            backendMock.Verify(m => m.GreaterOrEqual(resultMock.Object, lhsMock.Object, rhsMock.Object), Times.Once);
        }

        //[Fact]
        //public void IsClose_SameIntVectors_ReturnTrues()
        //{
        //    // arrange
        //    var config = DefaultConfig.Instance;
        //    var source = NdArray<int>.Arange(ConfigManager, 0, 10, 1);

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