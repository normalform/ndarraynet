// <copyright file="ComparisonTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved.
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using Moq;
    using NdArray.NdFunction;
    using NdArray.NdFunction.Comparison;
    using NdArrayNet;
    using System;
    using System.Reflection;
    using Xunit;

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

        [Theory]
        [InlineData(typeof(int))]
        public void Equal(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(EqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void FillNotEqual(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(FillNotEqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void NotEqual(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(NotEqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void FillLess(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(FillLessTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void Less(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(LessTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void FillLessOrEqual(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(FillLessOrEqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void LessOrEqual(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(LessOrEqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void FillGreater(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(FillGreaterTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void Greater(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(GreaterTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void FillGreaterOrEqual(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(FillGreaterOrEqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void GreaterOrEqual(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(GreaterOrEqualTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void FillIsFinite(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(FillIsFiniteTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        [Theory]
        [InlineData(typeof(int))]
        public void IsFinite(Type type)
        {
            var testHelper = typeof(ComparisonTests).GetMethod(nameof(IsFiniteTestHelper), BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        [Theory]
        [InlineData(0.1, 0.2)]
        [InlineData(0.1f, 0.2f)]
        public void IsClose<T>(T absoluteTolerence, T relativeTolerence)
        {
            IsCloseTestHelper(absoluteTolerence, relativeTolerence);
        }

        [Theory]
        [InlineData(0.1, 0.2)]
        [InlineData(0.1f, 0.2f)]
        public void AlmostEqual_WithSameShape<T>(T absoluteTolerence, T relativeTolerence)
        {
            var sameShape = new[] { 1, 2, 3 };
            AlmostEqualTestHelper(sameShape, sameShape, absoluteTolerence, relativeTolerence, true, 1);
        }

        [Theory]
        [InlineData(0.1, 0.2)]
        public void AlmostEqual_WithDifferentShape<T>(T absoluteTolerence, T relativeTolerence)
        {
            var lhsShape = new[] { 1, 2, 3 };
            var rhsShape = new[] { 1, 2 };
            AlmostEqualTestHelper(lhsShape, rhsShape, absoluteTolerence, relativeTolerence, false, 0);
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

        private void FillIsFiniteTestHelper<T>()
        {
            // arrange
            var backendMock = new Mock<IBackend<bool>>();
            var resultMock = new Mock<IFrontend<bool>>();
            resultMock.SetupGet(m => m.Backend).Returns(backendMock.Object);
            var sourceMock = new Mock<IFrontend<T>>();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.PrepareElemwiseSources(resultMock.Object, sourceMock.Object)).Returns(sourceMock.Object);
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            comparison.FillIsFinite(resultMock.Object, sourceMock.Object);

            // assert
            staticMethodMock.Verify(m => m.PrepareElemwiseSources(resultMock.Object, sourceMock.Object), Times.Once);
            backendMock.Verify(m => m.IsFinite(resultMock.Object, sourceMock.Object), Times.Once);
        }

        private void IsFiniteTestHelper<T>()
        {
            // arrange
            var backendMock = new Mock<IBackend<bool>>();
            var resultMock = new Mock<IFrontend<bool>>();
            resultMock.SetupGet(m => m.Backend).Returns(backendMock.Object);
            var sourceMock = new Mock<IFrontend<T>>();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.PrepareElemwise<bool, T>(sourceMock.Object, Order.RowMajor)).Returns((resultMock.Object, sourceMock.Object));
            staticMethodMock.Setup(m => m.PrepareElemwiseSources(resultMock.Object, sourceMock.Object)).Returns(sourceMock.Object);
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            comparison.IsFinite(sourceMock.Object);

            // assert
            staticMethodMock.Verify(m => m.PrepareElemwise<bool, T>(sourceMock.Object, Order.RowMajor), Times.Once);
            staticMethodMock.Verify(m => m.PrepareElemwiseSources(resultMock.Object, sourceMock.Object), Times.Once);
            staticMethodMock.Verify(m => m.AssertBool(resultMock.Object));
            backendMock.Verify(m => m.IsFinite(resultMock.Object, sourceMock.Object), Times.Once);
        }

        private void IsCloseTestHelper<T>(T absoluteTolerence, T relativeTolerence)
        {
            // arrange
            var lhsMock = new Mock<IFrontend<T>>();
            var rhsMock = new Mock<IFrontend<T>>();

            var expectedResult = CreateTestResult();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.IsCloseWithTolerence(lhsMock.Object, rhsMock.Object, absoluteTolerence, relativeTolerence)).Returns(expectedResult);
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            var result = comparison.IsClose(lhsMock.Object, rhsMock.Object, absoluteTolerence, relativeTolerence);

            // assert
            Assert.Equal(expectedResult, result);
            staticMethodMock.Verify(m => m.IsCloseWithTolerence(lhsMock.Object, rhsMock.Object, absoluteTolerence, relativeTolerence), Times.Once);
        }

        private void AlmostEqualTestHelper<T>(int[] lhsShape, int[] rhsShape, T absoluteTolerence, T relativeTolerence, bool expectedResult, int verifyTimes)
        {
            // arrange
            var lhsMock = new Mock<IFrontend<T>>();
            var rhsMock = new Mock<IFrontend<T>>();
            lhsMock.SetupGet(m => m.Shape).Returns(lhsShape);
            rhsMock.SetupGet(m => m.Shape).Returns(rhsShape);
            var dummyResult = CreateTestResult();

            var staticMethodMock = new Mock<IStaticMethod>();
            staticMethodMock.Setup(m => m.IsCloseWithTolerence(lhsMock.Object, rhsMock.Object, absoluteTolerence, relativeTolerence)).Returns(dummyResult);
            staticMethodMock.Setup(m => m.All(dummyResult)).Returns(expectedResult);
            var comparison = new NdComparison(staticMethodMock.Object).Get<T>();

            // action
            var result = comparison.AlmostEqual(lhsMock.Object, rhsMock.Object, absoluteTolerence, relativeTolerence);

            // assert
            Assert.Equal(expectedResult, result);
            staticMethodMock.Verify(m => m.IsCloseWithTolerence(lhsMock.Object, rhsMock.Object, absoluteTolerence, relativeTolerence), Times.Exactly(verifyTimes));
        }

        private NdArray<bool> CreateTestResult()
        {
            var configMock = new Mock<IConfig<bool>>();
            var functionMock = new Mock<INdFunction<bool>>();
            var configManagerMock = new Mock<IConfigManager>();
            configManagerMock.Setup(m => m.GetConfig<bool>()).Returns(configMock.Object);
            configManagerMock.SetupGet(m => m.GetConfig<bool>().NdFunction).Returns(functionMock.Object);
            var expectedResult = new NdArray<bool>(configManagerMock.Object, new[] { 1 });

            return expectedResult;
        }
    }
}