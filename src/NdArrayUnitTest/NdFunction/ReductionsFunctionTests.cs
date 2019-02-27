// <copyright file="ReductionsFunctionTests.cs" company="NdArrayNet">
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

    public class ReductionsFunctionTests
    {
        [Fact]
        public void MaxAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var max = ReductionFunction<double>.MaxAxis(1, input);

            // assert
            Assert.Equal(4.0, max[0].Value);
        }

        [Fact]
        public void MinAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var min = ReductionFunction<double>.MinAxis(1, input);

            // assert
            Assert.Equal(1.0, min[0].Value);
        }

        [Fact]
        public void MaxNdArray()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var max = ReductionFunction<double>.MaxNdArray(input);

            // assert
            Assert.Equal(8.0, max[0].Value);
        }

        [Fact]
        public void MinNdArray()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var min = ReductionFunction<double>.MinNdArray(input);

            // assert
            Assert.Equal(1.0, min[0].Value);
        }

        [Fact]
        public void SumAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var sum = ReductionFunction<double>.SumAxis(1, input);

            // assert
            Assert.Equal(10.0, sum[0].Value);
        }

        [Fact]
        public void SumNdArray()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var sum = ReductionFunction<double>.SumNdArray(input);

            // assert
            Assert.Equal(36.0, sum[0].Value);
        }

        [Fact]
        public void MeanAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var mean = ReductionFunction<double>.MeanAxis(1, input);

            // assert
            Assert.Equal(2.5, mean[0].Value);
        }

        [Fact]
        public void Mean()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var sum = ReductionFunction<double>.Mean(input);

            // assert
            Assert.Equal(4.5, sum);
        }

        [Fact]
        public void ProductAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var product = ReductionFunction<double>.ProductAxis(1, input);

            // assert
            Assert.Equal(24.0, product[0].Value);
        }

        [Fact]
        public void ProductNdArray()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var product = ReductionFunction<double>.ProductNdArray(input);

            // assert
            Assert.Equal(40320.0, product[0].Value);
        }

        [Fact]
        public void VarAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var var = ReductionFunction<double>.VarAxis(1, input);

            // assert
            Assert.Equal(1.25, var[0].Value);
        }

        [Fact]
        public void VarAxis_Ddof1()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var var = ReductionFunction<double>.VarAxis(1, input, 1.0);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(1.666666666 - var[0].Value) < Epsilon);
        }

        [Fact]
        public void StdAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var std = ReductionFunction<double>.StdAxis(1, input);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(1.11803399 - std[0].Value) < Epsilon);
        }

        [Fact]
        public void StdAxis_Ddof1()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var std = ReductionFunction<double>.StdAxis(1, input, 1.0);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(1.29099445 - std[0].Value) < Epsilon);
        }

        [Fact]
        public void TraceAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<int>.Arange(configManager,0, 27, 1).Reshape(new[] { 3, 3, 3 });

            // action
            var trace = ReductionFunction<int>.TraceAxis(0, 1, input);

            // assert
            Assert.Equal(36, trace[0].Value);
            Assert.Equal(39, trace[1].Value);
            Assert.Equal(42, trace[2].Value);
        }

        [Fact]
        public void Trace()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<int>.Arange(configManager,0, 27, 1).Reshape(new[] { 3, 3, 3 });

            // action
            var trace = ReductionFunction<int>.Trace(input);

            // assert
            Assert.Equal(12, trace[0].Value);
            Assert.Equal(39, trace[1].Value);
            Assert.Equal(66, trace[2].Value);
        }

        [Fact]
        public void Trace_WithInvalidDimension_ThrowException()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<int>.Arange(configManager,0, 27, 1);

            // action
            var exception = Assert.Throws<ArgumentException>(() => ReductionFunction<int>.Trace(input));
            Assert.Contains("Need at least a two dimensional array for trace but got 1 dimensional and it's shape is [27].", exception.Message);
        }
    }
}