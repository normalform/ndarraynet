// <copyright file="ConstructorTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved.
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using Moq;
    using NdArray.NdFunction.Constructor;
    using NdArrayNet;
    using System;
    using System.Reflection;
    using Xunit;

    public class ConstructorTests
    {
        [Theory]
        [InlineData(0, 10, 1, 10)]
        [InlineData(0U, 10U, 1U, 10)]
        [InlineData(0L, 10L, 1L, 10)]
        [InlineData(0UL, 10UL, 1UL, 10)]
        [InlineData((short)0, (short)10, (short)1, 10)]
        [InlineData((ushort)0, (ushort)10, (ushort)1, 10)]
        [InlineData(0.0, 10.0, 1.0, 10)]
        [InlineData(0.0f, 10.0f, 1.0f, 10)]
        public void Arange_ValidInput<T>(T start, T stop, T step, int numElements)
        {
            ArangeTestHelper(start, stop, step, numElements);
        }

        [Fact]
        public void Arange_Decimal()
        {
            ArangeTestHelper(0M, 10M, 1M, 10);
        }

        [Theory]
        [InlineData(false, true, true, 1)]
        [InlineData((byte)0, (byte)10, (byte)1, 10)]
        [InlineData((sbyte)0, (sbyte)10, (sbyte)1, 10)]
        [InlineData((char)0, (char)10, (char)1, 10)]
        public void Arange_InvalidInput_ThrowException<T>(T start, T stop, T step, int numElements)
        {
            Assert.Throws<InvalidOperationException>(() => ArangeTestHelper(start, stop, step, numElements));
        }

        [Theory]
        [InlineData(10, 0, 1)]
        [InlineData(10, 0U, 1U)]
        [InlineData(10, 0UL, 1UL)]
        [InlineData(10, (short)0, (short)1)]
        [InlineData(10, (ushort)0, (ushort)1)]
        [InlineData(10, 0.0, 1.0)]
        [InlineData(10, 0.0f, 1.0f)]
        [InlineData(1, false, true)]
        [InlineData(10, (byte)0, (byte)1)]
        [InlineData(10, (sbyte)0, (sbyte)1)]
        public void Count_ValidInput<T>(int numElements, T expedtedStart, T expectedStep)
        {
            CountTestHelper(numElements, expedtedStart, expectedStep);
        }

        [Fact]
        public void Count_Decimal()
        {
            CountTestHelper(10, 0M, 1M);
        }

        [Theory]
        [InlineData(10, (char)0, (char)1)]
        public void Count_InvalidInput_ThrowException<T>(int numElements, T expedtedStart, T expectedStep)
        {
            Assert.Throws<InvalidOperationException>(() => CountTestHelper(numElements, expedtedStart, expectedStep));
        }

        [Theory]
        [InlineData(typeof(int))]
        [InlineData(typeof(uint))]
        [InlineData(typeof(long))]
        [InlineData(typeof(ulong))]
        [InlineData(typeof(short))]
        [InlineData(typeof(ushort))]
        [InlineData(typeof(double))]
        [InlineData(typeof(float))]
        [InlineData(typeof(bool))]
        [InlineData(typeof(byte))]
        [InlineData(typeof(sbyte))]
        [InlineData(typeof(char))]
        public void Empty(Type type)
        {
            var testHelper = typeof(ConstructorTests).GetMethod("EmptyTestHelper", BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            genericHelper.Invoke(this, null);
        }

        [Fact]
        public void Falses()
        {
            FalsesTestHelper(false);
        }

        [Theory]
        [InlineData(typeof(int), 0)]
        [InlineData(typeof(uint), 0U)]
        [InlineData(typeof(long), 0L)]
        [InlineData(typeof(ulong), 0UL)]
        [InlineData(typeof(short), (short)0)]
        [InlineData(typeof(ushort), (ushort)0)]
        [InlineData(typeof(double), 0.0)]
        [InlineData(typeof(float), 0.0f)]
        [InlineData(typeof(byte), (byte)0)]
        [InlineData(typeof(sbyte), (sbyte)0)]
        [InlineData(typeof(char), (char)0)]
        public void Falses_UnsupportedType_ThrowException(Type type, object value)
        {
            var testHelper = typeof(ConstructorTests).GetMethod("FalsesTestHelper", BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            Assert.Throws<TargetInvocationException>(() => genericHelper.Invoke(this, new[] { value }));
        }

        [Fact]
        public void Falses_WithDecimal_ThrowException()
        {
            // TODO It should return different exception
            Assert.Throws<NullReferenceException>(() => FalsesTestHelper((decimal)0));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0U)]
        [InlineData(0L)]
        [InlineData(0UL)]
        [InlineData((short)0)]
        [InlineData((ushort)0)]
        [InlineData(0.0)]
        [InlineData(0.0f)]
        [InlineData(false)]
        [InlineData((byte)0)]
        [InlineData((sbyte)0)]
        [InlineData((char)0)]
        public void Filled<T>(T value)
        {
            FilledTestHelper(value);
        }

        [Fact]
        public void Filled_WithDecimal()
        {
            FilledTestHelper((decimal)0);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1U)]
        [InlineData(1L)]
        [InlineData(1UL)]
        [InlineData((short)1)]
        [InlineData((ushort)1)]
        [InlineData(1.0)]
        [InlineData(1.0f)]
        [InlineData(true)]
        [InlineData((byte)1)]
        [InlineData((sbyte)1)]
        public void Identity<T>(T one)
        {
            IdentityTestHelper(one);
        }

        [Fact]
        public void Identity_WithDecimal()
        {
            IdentityTestHelper((decimal)1);
        }

        [Fact]
        public void Identity_WithChar_ThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => IdentityTestHelper((char)1));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1U)]
        [InlineData(1L)]
        [InlineData(1UL)]
        [InlineData((short)1)]
        [InlineData((ushort)1)]
        [InlineData(1.0)]
        [InlineData(1.0f)]
        [InlineData(true)]
        [InlineData((byte)1)]
        [InlineData((sbyte)1)]
        public void Ones<T>(T one)
        {
            OnesTestHelper(one);
        }

        [Fact]
        public void Ones_WithDecimal()
        {
            OnesTestHelper((decimal)1);
        }

        [Fact]
        public void Ones_WithChar_ThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => OnesTestHelper((char)1));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1U)]
        [InlineData(1L)]
        [InlineData(1UL)]
        [InlineData((short)1)]
        [InlineData((ushort)1)]
        [InlineData(1.0)]
        [InlineData(1.0f)]
        [InlineData(true)]
        [InlineData((byte)1)]
        [InlineData((sbyte)1)]
        public void OnesLike<T>(T one)
        {
            OnesLikeTestHelper(one);
        }

        [Fact]
        public void OnesLike_WithDecimal()
        {
            OnesLikeTestHelper((decimal)1);
        }

        [Fact]
        public void OnesLike_WithChar_ThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => OnesLikeTestHelper((char)1));
        }

        [Theory]
        [InlineData(0, 5, 6, 1)]
        [InlineData(0U, 5U, 6, 1U)]
        [InlineData(0L, 5L, 6, 1L)]
        [InlineData(0UL, 5UL, 6, 1UL)]
        [InlineData((short)0, (short)5, 6, (short)1)]
        [InlineData((ushort)0, (ushort)5, 6, (ushort)1)]
        [InlineData(0.0, 5.0, 6.0, 1.0)]
        [InlineData(0.0f, 5.0f, 6.0f, 1.0f)]
        public void Linspace_WithValidInput<T>(T start, T stop, int numElements, T expectedStep)
        {
            LinspaceTestHelper(start, stop, numElements, expectedStep);
        }

        [Fact]
        public void Linspace_WithDecimal()
        {
            LinspaceTestHelper<decimal>(0, 5, 6, 1);
        }

        [Theory]
        [InlineData((byte)0, (byte)5, 5, (byte)1)]
        [InlineData((sbyte)0, (sbyte)5, 5, (sbyte)1)]
        [InlineData((bool)false, true, 5, true)]
        public void Linspace_WithInvalidInput_ThrowException<T>(T start, T stop, int numElements, T expectedStep)
        {
            Assert.Throws<InvalidOperationException>(() => LinspaceTestHelper(start, stop, numElements, expectedStep));
        }

        [Fact]
        public void Linspace_InvalidNumElements_ThrowException()
        {
            const int InvalidNumElements = 1;
            const int Dummy = 0;

            var exception = Assert.Throws<ArgumentException>(() => LinspaceTestHelper(Dummy, Dummy + 5, InvalidNumElements, Dummy + 1));
            Assert.Contains("Linspace requires at least two elements.", exception.Message);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(11U)]
        [InlineData(11L)]
        [InlineData(11UL)]
        [InlineData((short)11)]
        [InlineData((ushort)11)]
        [InlineData(11.0)]
        [InlineData(11.0f)]
        [InlineData(true)]
        [InlineData((byte)11)]
        [InlineData((sbyte)11)]
        public void Scalar<T>(T value)
        {
            ScalarTestHelper(value);
        }

        [Fact]
        public void Scalar_WithDecimal()
        {
            ScalarTestHelper((decimal)11);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(11U)]
        [InlineData(11L)]
        [InlineData(11UL)]
        [InlineData((short)11)]
        [InlineData((ushort)11)]
        [InlineData(11.0)]
        [InlineData(11.0f)]
        [InlineData(true)]
        [InlineData((byte)11)]
        [InlineData((sbyte)11)]
        public void ScalarLike<T>(T value)
        {
            ScalarLikeTestHelper(value);
        }

        [Fact]
        public void ScalarLike_WithDecimal()
        {
            ScalarLikeTestHelper((decimal)11);
        }

        [Fact]
        public void Trues()
        {
            TruesTestHelper(true);
        }

        [Theory]
        [InlineData(typeof(int), 1)]
        [InlineData(typeof(uint), 1U)]
        [InlineData(typeof(long), 1L)]
        [InlineData(typeof(ulong), 1UL)]
        [InlineData(typeof(short), (short)1)]
        [InlineData(typeof(ushort), (ushort)1)]
        [InlineData(typeof(double), 1.0)]
        [InlineData(typeof(float), 1.0f)]
        [InlineData(typeof(byte), (byte)1)]
        [InlineData(typeof(sbyte), (sbyte)1)]
        [InlineData(typeof(char), (char)1)]
        public void Trues_UnsupportedType_ThrowException(Type type, object value)
        {
            var testHelper = typeof(ConstructorTests).GetMethod("TruesTestHelper", BindingFlags.NonPublic | BindingFlags.Instance);
            var genericHelper = testHelper.MakeGenericMethod(type);
            Assert.Throws<TargetInvocationException>(() => genericHelper.Invoke(this, new[] { value }));
        }

        [Fact]
        public void Trues_WithDecimal_ThrowException()
        {
            // TODO It should return different exception
            Assert.Throws<NullReferenceException>(() => TruesTestHelper((decimal)1));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0U)]
        [InlineData(0L)]
        [InlineData(0UL)]
        [InlineData((short)0)]
        [InlineData((ushort)0)]
        [InlineData(0.0)]
        [InlineData(0.0f)]
        [InlineData(false)]
        [InlineData((byte)0)]
        [InlineData((sbyte)0)]
        [InlineData((char)0)]
        public void Zeros<T>(T zero)
        {
            ZerosTestHelper(zero);
        }

        [Fact]
        public void Zeros_WithDecimal()
        {
            ZerosTestHelper((decimal)0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0U)]
        [InlineData(0L)]
        [InlineData(0UL)]
        [InlineData((short)0)]
        [InlineData((ushort)0)]
        [InlineData(0.0)]
        [InlineData(0.0f)]
        [InlineData(false)]
        [InlineData((byte)0)]
        [InlineData((sbyte)0)]
        public void ZerosLike<T>(T zero)
        {
            ZerosLikeTestHelper(zero);
        }

        [Fact]
        public void ZerosLike_WithDecimal()
        {
            ZerosLikeTestHelper((decimal)0);
        }

        [Fact]
        public void ZerosLike_WithChar_ThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => ZerosLikeTestHelper((char)0));
        }

        private void ArangeTestHelper<T>(T start, T stop, T step, int expectedNumElements)
        {
            // arrange
            Layout savedLayout = null;
            var configManagerMock = new Mock<IConfigManager>();
            var backendMock = new Mock<IBackend<T>>();
            configManagerMock.Setup(m => m.GetConfig<T>().Create(It.IsAny<Layout>()).Backend(It.IsAny<Layout>()))
                .Callback<Layout>(l => savedLayout = l)
                .Returns(backendMock.Object);

            var staticMethodMock = new Mock<IStaticMethod>();
            var constructor = new NdConstructor(staticMethodMock.Object).Get<T>();

            // action
            constructor.Arange(configManagerMock.Object, start, stop, step);

            // assert
            backendMock.Verify(m => m.FillIncrementing(start, step, It.IsAny<IFrontend<T>>()), Times.Once);
            Assert.Equal(new Layout(new[] { expectedNumElements }, 0, new[] { 1 }), savedLayout);
        }

        private void CountTestHelper<T>(int numElements, T expedtedStart, T expectedStep)
        {
            // arrange
            Layout savedLayout = null;
            var configManagerMock = new Mock<IConfigManager>();
            var backendMock = new Mock<IBackend<T>>();
            configManagerMock.Setup(m => m.GetConfig<T>().Create(It.IsAny<Layout>()).Backend(It.IsAny<Layout>()))
                .Callback<Layout>(l => savedLayout = l)
                .Returns(backendMock.Object);

            var staticMethodMock = new Mock<IStaticMethod>();
            var constructor = new NdConstructor(staticMethodMock.Object).Get<T>();

            // action
            constructor.Counting(configManagerMock.Object, numElements);

            // assert
            backendMock.Verify(m => m.FillIncrementing(expedtedStart, expectedStep, It.IsAny<IFrontend<T>>()), Times.Once);
            Assert.Equal(new Layout(new[] { numElements }, 0, new[] { 1 }), savedLayout);
        }

        private void EmptyTestHelper<T>()
        {
            // arrange
            Layout savedLayout = null;
            var configManagerMock = new Mock<IConfigManager>();
            configManagerMock.Setup(m => m.GetConfig<T>().Create(It.IsAny<Layout>()))
                .Callback<Layout>(s => savedLayout = s);

            var staticMethodMock = new Mock<IStaticMethod>();
            var constructor = new NdConstructor(staticMethodMock.Object).Get<T>();

            // action
            constructor.Empty(configManagerMock.Object, 3);

            // assert
            var expectedLayout = new Layout(new[] { 0, 0, 0 }, 0, new[] { 0, 0, 1 });
            Assert.Equal(expectedLayout, savedLayout);
            configManagerMock.Verify(m => m.GetConfig<T>().Create(It.IsAny<Layout>()), Times.Once);
        }

        private void FalsesTestHelper<T>(T value)
        {
            // arrange
            Layout savedLayout = null;
            var configManagerMock = new Mock<IConfigManager>();
            var backendMock = new Mock<IBackend<T>>();
            backendMock.Setup(m => m.FillConst(value, It.IsAny<IFrontend<T>>()))
                .Callback<T, IFrontend<T>>((p, f) => savedLayout = f.Layout);
            configManagerMock.Setup(m => m.GetConfig<T>().Create(It.IsAny<Layout>()).Backend(It.IsAny<Layout>()))
                .Returns(backendMock.Object);

            var staticMethodMock = new Mock<IStaticMethod>();
            var constructor = new NdConstructor(staticMethodMock.Object).Get<T>();

            // action
            constructor.Falses(configManagerMock.Object, new[] { 1, 2, 3 });

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
            Assert.Equal(expectedLayout, savedLayout);
            backendMock.Verify(m => m.FillConst(value, It.IsAny<IFrontend<T>>()), Times.Once);
        }

        private void FilledTestHelper<T>(T value)
        {
            // arrange
            Layout savedLayout = null;
            var configManagerMock = new Mock<IConfigManager>();
            var backendMock = new Mock<IBackend<T>>();
            backendMock.Setup(m => m.FillConst(value, It.IsAny<IFrontend<T>>()))
                .Callback<T, IFrontend<T>>((p, f) => savedLayout = f.Layout);
            configManagerMock.Setup(m => m.GetConfig<T>().Create(It.IsAny<Layout>()).Backend(It.IsAny<Layout>()))
                .Returns(backendMock.Object);

            var staticMethodMock = new Mock<IStaticMethod>();
            var constructor = new NdConstructor(staticMethodMock.Object).Get<T>();

            // action
            constructor.Filled(configManagerMock.Object, new[] { 1, 2, 3 }, value);

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
            Assert.Equal(expectedLayout, savedLayout);
            backendMock.Verify(m => m.FillConst(value, It.IsAny<IFrontend<T>>()), Times.Once);
        }

        private void IdentityTestHelper<T>(T one)
        {
            // arrange
            var layout3by3 = new Layout(new[] { 3, 3 }, 0, new[] { 3, 1 });
            var layoutDiag = new Layout(new[] { 3 }, 0, new[] { 4 });
            Layout savedDiagLayout = null;
            var configManagerMock = new Mock<IConfigManager>();
            var backendMock = new Mock<IBackend<T>>();
            backendMock.Setup(m => m.FillConst(one, It.IsAny<IFrontend<T>>()))
                .Callback<T, IFrontend<T>>((p, f) => savedDiagLayout = f.Layout);
            configManagerMock.Setup(m => m.GetConfig<T>().Create(layoutDiag).Backend(layoutDiag))
                .Returns(backendMock.Object);

            var staticMethodMock = new Mock<IStaticMethod>();
            var constructor = new NdConstructor(staticMethodMock.Object).Get<T>();

            // action
            constructor.Identity(configManagerMock.Object, 3);

            // assert
            configManagerMock.Verify(m => m.GetConfig<T>().Create(layout3by3), Times.Once);
            configManagerMock.Verify(m => m.GetConfig<T>().Create(layoutDiag).Backend(layoutDiag), Times.Once);
            backendMock.Verify(m => m.FillConst(one, It.IsAny<IFrontend<T>>()), Times.Once);
        }

        private void OnesTestHelper<T>(T one)
        {
            // arrange
            Layout savedLayout = null;
            var configManagerMock = new Mock<IConfigManager>();
            var backendMock = new Mock<IBackend<T>>();
            backendMock.Setup(m => m.FillConst(one, It.IsAny<IFrontend<T>>()))
                .Callback<T, IFrontend<T>>((p, f) => savedLayout = f.Layout);
            configManagerMock.Setup(m => m.GetConfig<T>().Create(It.IsAny<Layout>()).Backend(It.IsAny<Layout>()))
                .Returns(backendMock.Object);

            var staticMethodMock = new Mock<IStaticMethod>();
            var constructor = new NdConstructor(staticMethodMock.Object).Get<T>();

            // action
            constructor.Ones(configManagerMock.Object, new[] { 2, 2 });

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            Assert.Equal(expectedLayout, savedLayout);
            backendMock.Verify(m => m.FillConst(one, It.IsAny<IFrontend<T>>()), Times.Once);
        }

        private void OnesLikeTestHelper<T>(T one)
        {
            // arrange
            var layout2by2 = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            var configManagerMock = new Mock<IConfigManager>();
            var backendMock = new Mock<IBackend<T>>();
            configManagerMock.Setup(m => m.GetConfig<T>().Create(layout2by2).Backend(layout2by2))
                .Returns(backendMock.Object);

            var staticMethodMock = new Mock<IStaticMethod>();
            var constructor = new NdConstructor(staticMethodMock.Object).Get<T>();

            // action
            var zeros = constructor.Zeros(configManagerMock.Object, new[] { 2, 2 });
            constructor.OnesLike(zeros);

            // assert
            configManagerMock.Verify(m => m.GetConfig<T>().Create(layout2by2), Times.Exactly(2));
            configManagerMock.Verify(m => m.GetConfig<T>().Create(layout2by2).Backend(layout2by2), Times.Once);
            backendMock.Verify(m => m.FillConst(one, It.IsAny<IFrontend<T>>()), Times.Once);
        }

        private void LinspaceTestHelper<T>(T start, T stop, int numElements, T expectedStep)
        {
            // arrange
            Layout savedLayout = null;
            var configManagerMock = new Mock<IConfigManager>();
            var backendMock = new Mock<IBackend<T>>();
            backendMock.Setup(m => m.FillIncrementing(It.IsAny<T>(), It.IsAny<T>(), It.IsAny<IFrontend<T>>()))
                .Callback<T, T, IFrontend<T>>((x, y, z) => savedLayout = z.Layout);
            configManagerMock.Setup(m => m.GetConfig<T>().Create(It.IsAny<Layout>()).Backend(It.IsAny<Layout>()))
                .Returns(backendMock.Object);

            var staticMethodMock = new Mock<IStaticMethod>();
            var constructor = new NdConstructor(staticMethodMock.Object).Get<T>();

            // action
            constructor.Linspace(configManagerMock.Object, start, stop, numElements);

            // assert
            var expectedLayout = new Layout(new[] { numElements }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, savedLayout);
            backendMock.Verify(m => m.FillIncrementing(start, expectedStep, It.IsAny<IFrontend<T>>()), Times.Once);
        }

        private void ScalarTestHelper<T>(T value)
        {
            // arrange
            Layout savedLayout = null;
            var backendMock = new Mock<IBackend<T>>();
            var configManagerMock = new Mock<IConfigManager>();
            configManagerMock.Setup(m => m.GetConfig<T>().Create(It.IsAny<Layout>()).Backend(It.IsAny<Layout>()))
                .Callback<Layout>(s => savedLayout = s)
                .Returns(backendMock.Object);

            var staticMethodMock = new Mock<IStaticMethod>();
            var constructor = new NdConstructor(staticMethodMock.Object).Get<T>();

            // action
            var scalar = constructor.Scalar(configManagerMock.Object, value);

            // assert
            var expectedLayout = new Layout(new int[] { }, 0, new int[] { });
            Assert.Equal(expectedLayout, savedLayout);
            configManagerMock.Verify(m => m.GetConfig<T>().Create(It.IsAny<Layout>()), Times.Once);
            backendMock.VerifySet(m => m[new int[] { }] = value, Times.Once);
        }

        private void ScalarLikeTestHelper<T>(T value)
        {
            // arrange
            Layout savedLayout = null;
            var backendMock = new Mock<IBackend<T>>();
            var configManagerMock = new Mock<IConfigManager>();
            configManagerMock.Setup(m => m.GetConfig<T>().Create(It.IsAny<Layout>()).Backend(It.IsAny<Layout>()))
                .Callback<Layout>(s => savedLayout = s)
                .Returns(backendMock.Object);

            var staticMethodMock = new Mock<IStaticMethod>();
            var constructor = new NdConstructor(staticMethodMock.Object).Get<T>();

            var referenceArray = constructor.Zeros(configManagerMock.Object, new[] { 2, 3, 4 });

            // action
            var scalar = constructor.ScalarLike(referenceArray, value);

            // assert
            var expectedLayout = new Layout(new int[] { }, 0, new int[] { });
            Assert.Equal(expectedLayout, savedLayout);
            configManagerMock.Verify(m => m.GetConfig<T>().Create(expectedLayout), Times.Once);
            backendMock.VerifySet(m => m[new int[] { }] = value, Times.Once);
        }

        private void TruesTestHelper<T>(T value)
        {
            // arrange
            Layout savedLayout = null;
            var configManagerMock = new Mock<IConfigManager>();
            var backendMock = new Mock<IBackend<T>>();
            backendMock.Setup(m => m.FillConst(value, It.IsAny<IFrontend<T>>()))
                .Callback<T, IFrontend<T>>((p, f) => savedLayout = f.Layout);
            configManagerMock.Setup(m => m.GetConfig<T>().Create(It.IsAny<Layout>()).Backend(It.IsAny<Layout>()))
                .Returns(backendMock.Object);

            var staticMethodMock = new Mock<IStaticMethod>();
            var constructor = new NdConstructor(staticMethodMock.Object).Get<T>();

            // action
            constructor.Trues(configManagerMock.Object, new[] { 1, 2, 3 });

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
            Assert.Equal(expectedLayout, savedLayout);
            backendMock.Verify(m => m.FillConst(value, It.IsAny<IFrontend<T>>()), Times.Once);
        }

        private void ZerosTestHelper<T>(T zero)
        {
            // arrange
            var configManagerMock = new Mock<IConfigManager>();
            configManagerMock.Setup(m => m.GetConfig<T>().Create(It.IsAny<Layout>()));

            var staticMethodMock = new Mock<IStaticMethod>();
            var constructor = new NdConstructor(staticMethodMock.Object).Get<T>();

            // action
            constructor.Zeros(configManagerMock.Object, new[] { 2, 2 });

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            configManagerMock.Verify(m => m.GetConfig<T>().Create(expectedLayout), Times.Once);
        }

        private void ZerosLikeTestHelper<T>(T zero)
        {
            // arrange
            var layout2by2 = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            var configManagerMock = new Mock<IConfigManager>();
            var backendMock = new Mock<IBackend<T>>();
            configManagerMock.Setup(m => m.GetConfig<T>().Create(layout2by2).Backend(layout2by2))
                .Returns(backendMock.Object);

            var staticMethodMock = new Mock<IStaticMethod>();
            var constructor = new NdConstructor(staticMethodMock.Object).Get<T>();

            // action
            var ones = constructor.Ones(configManagerMock.Object, new[] { 2, 2 });
            constructor.ZerosLike(ones);

            // assert
            configManagerMock.Verify(m => m.GetConfig<T>().Create(layout2by2), Times.Exactly(2));
            configManagerMock.Verify(m => m.GetConfig<T>().Create(layout2by2).Backend(layout2by2), Times.Once);
            backendMock.Verify(m => m.FillConst(It.IsAny<T>(), It.IsAny<IFrontend<T>>()), Times.Once);
        }
    }
}