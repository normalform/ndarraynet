// <copyright file="ConstructorTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArrayNet;
    using System;
    using Xunit;
    using NdArray.NdFunction.Constructor;
    using Moq;
    using System.Collections.Generic;

    public class ConstructorTests
    {
        [Theory]
        [MemberData(nameof(ArangeValidTestInputs))]
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
        [MemberData(nameof(ArangeInValidTestInputs))]
        public void Arange_InvalidInput_ThrowException<T>(T start, T stop, T step, int numElements)
        {
            Assert.Throws<InvalidOperationException>(() => ArangeTestHelper(start, stop, step, numElements));
        }

        public static IEnumerable<object[]> ArangeValidTestInputs()
        {
            yield return new object[] { 0, 10, 1, 10 };
            yield return new object[] { 0U, 10U, 1U, 10 };
            yield return new object[] { 0L, 10L, 1L, 10 };
            yield return new object[] { 0UL, 10UL, 1UL, 10 };
            yield return new object[] { (short)0, (short)10, (short)1, 10 };
            yield return new object[] { (ushort)0, (ushort)10, (ushort)1, 10 };
            yield return new object[] { 0.0, 10.0, 1.0, 10 };
            yield return new object[] { 0.0f, 10.0f, 1.0f, 10 };
        }

        public static IEnumerable<object[]> ArangeInValidTestInputs()
        {
            yield return new object[] { false, true, true, 1 };
            yield return new object[] { (byte)0, (byte)10, (byte)1, 10 };
            yield return new object[] { (sbyte)0, (sbyte)10, (sbyte)1, 10 };
            yield return new object[] { (char)0, (char)10, (char)1, 10 };
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
            var array = constructor.Arange(configManagerMock.Object, start, stop, step);

            // assert
            backendMock.Verify(m => m.FillIncrementing(start, step, It.IsAny<IFrontend<T>>()));
            Assert.Equal(new Layout(new[] { expectedNumElements }, 0, new[] { 1 }), savedLayout);
        }

        [Theory]
        [MemberData(nameof(CountValidTestInputs))]
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
        [MemberData(nameof(ArangeInValidTestInputs))]
        public void Count_InvalidInput_ThrowException<T>(T start, T stop, T step, int numElements)
        {
            Assert.Throws<InvalidOperationException>(() => ArangeTestHelper(start, stop, step, numElements));
        }

        public static IEnumerable<object[]> CountValidTestInputs()
        {
            yield return new object[] { 10, 0, 1 };
            yield return new object[] { 10, 0U, 1U };
            yield return new object[] { 10, 0L, 1L };
            yield return new object[] { 10, 0UL, 1UL };
            yield return new object[] { 10, (short)0, (short)1 };
            yield return new object[] { 10, (ushort)0, (ushort)1 };
            yield return new object[] { 10, 0.0, 1.0 };
            yield return new object[] { 10, 0.0f, 1.0f};
        }

        public static IEnumerable<object[]> CountInValidTestInputs()
        {
            yield return new object[] { 1, false, true };
            yield return new object[] { 10, (byte)0, (byte)1 };
            yield return new object[] { 10, (sbyte)0, (sbyte)1 };
            yield return new object[] { 10, (char)0, (char)1 };
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
            var array = constructor.Counting(configManagerMock.Object, numElements);

            // assert
            backendMock.Verify(m => m.FillIncrementing(expedtedStart, expectedStep, It.IsAny<IFrontend<T>>()));
            Assert.Equal(new Layout(new[] { numElements }, 0, new[] { 1 }), savedLayout);
        }

        //[Fact]
        //public void Empty()
        //{
        //    // arrange
        //    var configManager = ConfigManager.Instance;

        //    // action
        //    var array = BaseConstructor<int>.Empty(configManager,3);

        //    // assert
        //    var expectedLayout = new Layout(new[] { 0, 0, 0 }, 0, new[] { 0, 0, 1 });
        //    Assert.Equal(expectedLayout, array.Layout);
        //}

        //[Fact]
        //public void Falses()
        //{
        //    // arrange
        //    var configManager = ConfigManager.Instance;

        //    // action
        //    var array = BaseConstructor<bool>.Falses(configManager,new[] { 1, 2, 3 });

        //    // assert
        //    var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
        //    Assert.Equal(expectedLayout, array.Layout);
        //    Assert.False(array[new[] { 0, 0, 0 }]);
        //    Assert.False(array[new[] { 0, 1, 2 }]);
        //}

        //[Fact]
        //public void Filled()
        //{
        //    // arrange
        //    var configManager = ConfigManager.Instance;

        //    // action
        //    var array = BaseConstructor<int>.Filled(configManager,new[] { 1, 2, 3 }, 55);

        //    // assert
        //    var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
        //    Assert.Equal(expectedLayout, array.Layout);
        //    Assert.Equal(55, array[new[] { 0, 0, 0 }]);
        //    Assert.Equal(55, array[new[] { 0, 1, 2 }]);
        //}

        //[Fact]
        //public void Identity_3by3()
        //{
        //    // arrange
        //    var configManager = ConfigManager.Instance;

        //    // action
        //    var array = BaseConstructor<int>.Identity(configManager,3);

        //    // assert
        //    var expectedLayout = new Layout(new[] { 3, 3 }, 0, new[] { 3, 1 });
        //    Assert.Equal(expectedLayout, array.Layout);
        //    Assert.Equal(1, array[new[] { 0, 0 }]);
        //    Assert.Equal(0, array[new[] { 0, 1 }]);
        //    Assert.Equal(0, array[new[] { 0, 2 }]);

        //    Assert.Equal(0, array[new[] { 1, 0 }]);
        //    Assert.Equal(1, array[new[] { 1, 1 }]);
        //    Assert.Equal(0, array[new[] { 1, 2 }]);

        //    Assert.Equal(0, array[new[] { 2, 0 }]);
        //    Assert.Equal(0, array[new[] { 2, 1 }]);
        //    Assert.Equal(1, array[new[] { 2, 2 }]);
        //}

        //[Fact]
        //public void Ones()
        //{
        //    // arrange
        //    var configManager = ConfigManager.Instance;

        //    // action
        //    var array = BaseConstructor<int>.Ones(configManager,new[] { 2, 2 });

        //    // assert
        //    var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
        //    Assert.Equal(expectedLayout, array.Layout);
        //    Assert.Equal(1, array[new[] { 0, 0 }]);
        //    Assert.Equal(1, array[new[] { 0, 1 }]);
        //    Assert.Equal(1, array[new[] { 1, 0 }]);
        //    Assert.Equal(1, array[new[] { 1, 1 }]);
        //}

        //[Fact]
        //public void OnesLike()
        //{
        //    // arrange
        //    var configManager = ConfigManager.Instance;
        //    var zeros = NdArray<int>.Zeros(configManager,new[] { 2, 2 });

        //    // action
        //    var array = BaseConstructor<int>.OnesLike(zeros);

        //    // assert
        //    var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
        //    Assert.Equal(expectedLayout, array.Layout);
        //    Assert.Equal(1, array[new[] { 0, 0 }]);
        //    Assert.Equal(1, array[new[] { 0, 1 }]);
        //    Assert.Equal(1, array[new[] { 1, 0 }]);
        //    Assert.Equal(1, array[new[] { 1, 1 }]);
        //}

        //[Fact]
        //public void Linspace_Int()
        //{
        //    // arrange
        //    var configManager = ConfigManager.Instance;

        //    // action
        //    var array = BaseConstructor<int>.Linspace(configManager,0, 5, 5);

        //    // assert
        //    var expectedLayout = new Layout(new[] { 5 }, 0, new[] { 1 });
        //    Assert.Equal(expectedLayout, array.Layout);
        //    Assert.Equal(0, array[new[] { 0 }]);
        //    Assert.Equal(1, array[new[] { 1 }]);
        //    Assert.Equal(2, array[new[] { 2 }]);
        //    Assert.Equal(3, array[new[] { 3 }]);
        //    Assert.Equal(4, array[new[] { 4 }]);
        //}

        //[Fact]
        //public void Linspace_Double()
        //{
        //    // arrange
        //    var configManager = ConfigManager.Instance;

        //    // action
        //    var array = BaseConstructor<double>.Linspace(configManager,1.0, 2.0, 5);

        //    // assert
        //    var expectedLayout = new Layout(new[] { 5 }, 0, new[] { 1 });
        //    Assert.Equal(expectedLayout, array.Layout);
        //    Assert.Equal(1.0, array[new[] { 0 }]);
        //    Assert.Equal(1.25, array[new[] { 1 }]);
        //    Assert.Equal(1.5, array[new[] { 2 }]);
        //    Assert.Equal(1.75, array[new[] { 3 }]);
        //    Assert.Equal(2.0, array[new[] { 4 }]);
        //}

        //[Fact]
        //public void Linspace_InvalidNumElements_ThrowException()
        //{
        //    // arrange
        //    var configManager = ConfigManager.Instance;

        //    // action
        //    var exception = Assert.Throws<ArgumentException>(() => BaseConstructor<int>.Linspace(configManager,0, 5, 1));
        //    Assert.Contains("Linspace requires at least two elements.", exception.Message);
        //}

        //[Fact]
        //public void Scalar()
        //{
        //    // arrange
        //    var configManager = ConfigManager.Instance;
        //    const int Value = 9;

        //    // action
        //    var array = BaseConstructor<int>.Scalar(configManager,Value);

        //    // asssert
        //    Assert.Equal(Value, array.Value);
        //}

        //[Fact]
        //public void ScalarLike()
        //{
        //    // arrange
        //    var configManager = ConfigManager.Instance;
        //    var referenceArray = NdArray<int>.Arange(configManager,0, 24, 1).Reshape(new[] { 2, 3, 4 });
        //    const int Value = 9;

        //    // action
        //    var array = BaseConstructor<int>.ScalarLike(referenceArray, Value);

        //    // asssert
        //    Assert.Equal(Value, array.Value);
        //}

        //[Fact]
        //public void Trues()
        //{
        //    // arrange
        //    var configManager = ConfigManager.Instance;

        //    // action
        //    var array = BaseConstructor<bool>.Trues(configManager,new[] { 1, 2, 3 });

        //    // assert
        //    var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
        //    Assert.Equal(expectedLayout, array.Layout);
        //    Assert.True(array[new[] { 0, 0, 0 }]);
        //    Assert.True(array[new[] { 0, 1, 2 }]);
        //}

        //[Fact]
        //public void Zeros()
        //{
        //    // arrange
        //    var configManager = ConfigManager.Instance;

        //    // action
        //    var array = BaseConstructor<int>.Zeros(configManager,new[] { 2, 2 });

        //    // assert
        //    var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
        //    Assert.Equal(expectedLayout, array.Layout);
        //    Assert.Equal(0, array[new[] { 0, 0 }]);
        //    Assert.Equal(0, array[new[] { 0, 1 }]);
        //    Assert.Equal(0, array[new[] { 1, 0 }]);
        //    Assert.Equal(0, array[new[] { 1, 1 }]);
        //}

        //[Fact]
        //public void ZerosLike()
        //{
        //    // arrange
        //    var configManager = ConfigManager.Instance;
        //    var ones = NdArray<int>.Ones(configManager,new[] { 2, 2 });

        //    // action
        //    var array = BaseConstructor<int>.ZerosLike(ones);

        //    // assert
        //    var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
        //    Assert.Equal(expectedLayout, array.Layout);
        //    Assert.Equal(0, array[new[] { 0, 0 }]);
        //    Assert.Equal(0, array[new[] { 0, 1 }]);
        //    Assert.Equal(0, array[new[] { 1, 0 }]);
        //    Assert.Equal(0, array[new[] { 1, 1 }]);
        //}
    }
}