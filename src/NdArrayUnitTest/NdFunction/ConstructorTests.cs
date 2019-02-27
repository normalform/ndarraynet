// <copyright file="ConstructorTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArray.NdFunction;
    using NdArrayNet;
    using System;
    using Xunit;

    public class ConstructorTests
    {
        [Fact]
        public void Arange_Start0()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<int>.Arange(configManager, 0, 10, 1);

            // assert
            var expectedLayout = new Layout(new[] { 10 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(0, array[0].Value);
        }

        [Fact]
        public void Arange_Start1()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<int>.Arange(configManager, 1, 10, 1);

            // assert
            var expectedLayout = new Layout(new[] { 9 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(1, array[0].Value);
        }

        [Fact]
        public void Arange_Step1()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<int>.Arange(configManager, 0, 10, 1);

            // assert
            var expectedLayout = new Layout(new[] { 10 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(1, array[1].Value);
        }

        [Fact]
        public void Arange_Step2()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<int>.Arange(configManager, 0, 10, 2);

            // assert
            var expectedLayout = new Layout(new[] { 5 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(2, array[1].Value);
        }

        [Fact]
        public void Arange_Stop10()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<int>.Arange(configManager, 0, 10, 1);

            // assert
            var expectedLayout = new Layout(new[] { 10 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(9, array[9].Value);
        }

        [Fact]
        public void Arange_Stop11()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<int>.Arange(configManager,0, 11, 1);

            // assert
            var expectedLayout = new Layout(new[] { 11 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(10, array[10].Value);
        }

        [Fact]
        public void Counting_Int()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<int>.Counting(configManager,10);

            // assert
            var expectedLayout = new Layout(new[] { 10 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(0, array[0].Value);
            Assert.Equal(9, array[9].Value);
        }

        [Fact]
        public void Counting_Double()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<double>.Counting(configManager,10);

            // assert
            var expectedLayout = new Layout(new[] { 10 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(0.0, array[0].Value);
            Assert.Equal(9.0, array[9].Value);
        }

        [Fact]
        public void Empty()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<int>.Empty(configManager,3);

            // assert
            var expectedLayout = new Layout(new[] { 0, 0, 0 }, 0, new[] { 0, 0, 1 });
            Assert.Equal(expectedLayout, array.Layout);
        }

        [Fact]
        public void Falses()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<bool>.Falses(configManager,new[] { 1, 2, 3 });

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.False(array[new[] { 0, 0, 0 }]);
            Assert.False(array[new[] { 0, 1, 2 }]);
        }

        [Fact]
        public void Filled()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<int>.Filled(configManager,new[] { 1, 2, 3 }, 55);

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(55, array[new[] { 0, 0, 0 }]);
            Assert.Equal(55, array[new[] { 0, 1, 2 }]);
        }

        [Fact]
        public void Identity_3by3()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<int>.Identity(configManager,3);

            // assert
            var expectedLayout = new Layout(new[] { 3, 3 }, 0, new[] { 3, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(1, array[new[] { 0, 0 }]);
            Assert.Equal(0, array[new[] { 0, 1 }]);
            Assert.Equal(0, array[new[] { 0, 2 }]);

            Assert.Equal(0, array[new[] { 1, 0 }]);
            Assert.Equal(1, array[new[] { 1, 1 }]);
            Assert.Equal(0, array[new[] { 1, 2 }]);

            Assert.Equal(0, array[new[] { 2, 0 }]);
            Assert.Equal(0, array[new[] { 2, 1 }]);
            Assert.Equal(1, array[new[] { 2, 2 }]);
        }

        [Fact]
        public void Ones()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<int>.Ones(configManager,new[] { 2, 2 });

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(1, array[new[] { 0, 0 }]);
            Assert.Equal(1, array[new[] { 0, 1 }]);
            Assert.Equal(1, array[new[] { 1, 0 }]);
            Assert.Equal(1, array[new[] { 1, 1 }]);
        }

        [Fact]
        public void OnesLike()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var zeros = NdArray<int>.Zeros(configManager,new[] { 2, 2 });

            // action
            var array = Constructor<int>.OnesLike(zeros);

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(1, array[new[] { 0, 0 }]);
            Assert.Equal(1, array[new[] { 0, 1 }]);
            Assert.Equal(1, array[new[] { 1, 0 }]);
            Assert.Equal(1, array[new[] { 1, 1 }]);
        }

        [Fact]
        public void Linspace_Int()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<int>.Linspace(configManager,0, 5, 5);

            // assert
            var expectedLayout = new Layout(new[] { 5 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(0, array[new[] { 0 }]);
            Assert.Equal(1, array[new[] { 1 }]);
            Assert.Equal(2, array[new[] { 2 }]);
            Assert.Equal(3, array[new[] { 3 }]);
            Assert.Equal(4, array[new[] { 4 }]);
        }

        [Fact]
        public void Linspace_Double()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<double>.Linspace(configManager,1.0, 2.0, 5);

            // assert
            var expectedLayout = new Layout(new[] { 5 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(1.0, array[new[] { 0 }]);
            Assert.Equal(1.25, array[new[] { 1 }]);
            Assert.Equal(1.5, array[new[] { 2 }]);
            Assert.Equal(1.75, array[new[] { 3 }]);
            Assert.Equal(2.0, array[new[] { 4 }]);
        }

        [Fact]
        public void Linspace_InvalidNumElements_ThrowException()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var exception = Assert.Throws<ArgumentException>(() => Constructor<int>.Linspace(configManager,0, 5, 1));
            Assert.Contains("Linspace requires at least two elements.", exception.Message);
        }

        [Fact]
        public void Scalar()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            const int Value = 9;

            // action
            var array = Constructor<int>.Scalar(configManager,Value);

            // asssert
            Assert.Equal(Value, array.Value);
        }

        [Fact]
        public void ScalarLike()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var referenceArray = NdArray<int>.Arange(configManager,0, 24, 1).Reshape(new[] { 2, 3, 4 });
            const int Value = 9;

            // action
            var array = Constructor<int>.ScalarLike(referenceArray, Value);

            // asssert
            Assert.Equal(Value, array.Value);
        }

        [Fact]
        public void Trues()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<bool>.Trues(configManager,new[] { 1, 2, 3 });

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.True(array[new[] { 0, 0, 0 }]);
            Assert.True(array[new[] { 0, 1, 2 }]);
        }

        [Fact]
        public void Zeros()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = Constructor<int>.Zeros(configManager,new[] { 2, 2 });

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(0, array[new[] { 0, 0 }]);
            Assert.Equal(0, array[new[] { 0, 1 }]);
            Assert.Equal(0, array[new[] { 1, 0 }]);
            Assert.Equal(0, array[new[] { 1, 1 }]);
        }

        [Fact]
        public void ZerosLike()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var ones = NdArray<int>.Ones(configManager,new[] { 2, 2 });

            // action
            var array = Constructor<int>.ZerosLike(ones);

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(0, array[new[] { 0, 0 }]);
            Assert.Equal(0, array[new[] { 0, 1 }]);
            Assert.Equal(0, array[new[] { 1, 0 }]);
            Assert.Equal(0, array[new[] { 1, 1 }]);
        }
    }
}