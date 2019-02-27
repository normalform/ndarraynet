// <copyright file="ElementWiseMathFunctionTests.cs" company="NdArrayNet">
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

    public class ElementWiseMathFunctionTests
    {
        [Fact]
        public void Abs()
        {
            // arrange
            var srcArray = NdArray<int>.Linspace(ConfigManager.Instance, -4, 4, 8);

            // action
            var newArray = ElementWiseMathFunction<int>.Abs(srcArray);

            // assert
            Assert.True(newArray[0].Value > 0);
        }

        [Fact]
        public void Acos()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(ConfigManager.Instance, -1, 1, 3);

            // action
            var newArray = ElementWiseMathFunction<double>.Acos(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - Math.PI) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - (Math.PI / 2.0)) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 0.0) < Epsilon);
        }

        [Fact]
        public void Asin()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(ConfigManager.Instance, -1, 1, 3);

            // action
            var newArray = ElementWiseMathFunction<double>.Asin(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - (-Math.PI / 2.0)) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - (Math.PI / 2.0)) < Epsilon);
        }

        [Fact]
        public void Atan()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(ConfigManager.Instance, -1, 1, 3);

            // action
            var newArray = ElementWiseMathFunction<double>.Atan(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - (-Math.PI / 4.0)) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - (Math.PI / 4.0)) < Epsilon);
        }

        [Fact]
        public void Ceiling()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(ConfigManager.Instance, -1, 1, 6);

            // action
            var newArray = ElementWiseMathFunction<double>.Ceiling(srcArray);

            // assert
            Assert.Equal(-1.0, newArray[0].Value);
            Assert.Equal(0.0, newArray[1].Value);
            Assert.Equal(1.0, newArray[4].Value);
        }

        [Fact]
        public void Cos()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Cos(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 0.0) < Epsilon);
        }

        [Fact]
        public void Cosh()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Cosh(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - 2.5091784786580567) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 2.5091784786580567) < Epsilon);
        }

        [Fact]
        public void Exp()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 4 });
            srcArray[0].Value = -1.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = 1.0;
            srcArray[3].Value = 10.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Exp(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - 0.36787944117144233) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 2.718281828459045) < Epsilon);
            Assert.True(Math.Abs(newArray[3].Value - 22026.465794806718) < Epsilon);
        }

        [Fact]
        public void Floor()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(ConfigManager.Instance, -1, 1, 6);

            // action
            var newArray = ElementWiseMathFunction<double>.Floor(srcArray);

            // assert
            Assert.Equal(-1.0, newArray[0].Value);
            Assert.Equal(-1.0, newArray[1].Value);
            Assert.Equal(-1.0, newArray[2].Value);
            Assert.Equal(0.0, newArray[3].Value);
            Assert.Equal(0.0, newArray[4].Value);
            Assert.Equal(1.0, newArray[5].Value);
        }

        [Fact]
        public void Log()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = 1.00;
            srcArray[1].Value = Math.E;
            srcArray[2].Value = 4.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Log(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 1.3862943611198906) < Epsilon);
        }

        [Fact]
        public void Log10()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = 1.00;
            srcArray[1].Value = 10.0;
            srcArray[2].Value = 100.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Log10(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 2.0) < Epsilon);
        }

        [Fact]
        public void Maximum()
        {
            // arrange
            var srcArray1 = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            var srcArray2 = NdArray<double>.Ones(ConfigManager.Instance, new[] { 3 });

            // action
            var newArray = ElementWiseMathFunction<double>.Maximum(srcArray1, srcArray2);

            // assert
            Assert.Equal(1.0, newArray[0].Value);
            Assert.Equal(1.0, newArray[1].Value);
            Assert.Equal(1.0, newArray[2].Value);
        }

        [Fact]
        public void Minimum()
        {
            // arrange
            var srcArray1 = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            var srcArray2 = NdArray<double>.Ones(ConfigManager.Instance, new[] { 3 });

            // action
            var newArray = ElementWiseMathFunction<double>.Minimum(srcArray1, srcArray2);

            // assert
            Assert.Equal(0.0, newArray[0].Value);
            Assert.Equal(0.0, newArray[1].Value);
            Assert.Equal(0.0, newArray[2].Value);
        }

        [Fact]
        public void Pow()
        {
            // arrange
            var lhs = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            lhs[0].Value = 5.0;
            lhs[1].Value = 6.0;
            lhs[2].Value = 7.0;

            var rhs = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            rhs[0].Value = 2.00;
            rhs[1].Value = 3.0;
            rhs[2].Value = 4.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Pow(lhs, rhs);

            // assert
            Assert.Equal(25.0, newArray[0].Value);
            Assert.Equal(216.0, newArray[1].Value);
            Assert.Equal(2401.0, newArray[2].Value);
        }

        [Fact]
        public void Round()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(ConfigManager.Instance, -1, 1, 6);

            // action
            var newArray = ElementWiseMathFunction<double>.Round(srcArray);

            // assert
            Assert.Equal(-1.0, newArray[0].Value);
            Assert.Equal(-1.0, newArray[1].Value);
            Assert.Equal(0.0, newArray[2].Value);
            Assert.Equal(0.0, newArray[3].Value);
            Assert.Equal(1.0, newArray[4].Value);
            Assert.Equal(1.0, newArray[5].Value);
        }

        [Fact]
        public void Sign()
        {
            // arrange
            var src = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 4 });
            src[0].Value = -2.0;
            src[1].Value = -1.0;
            src[2].Value = 0.0;
            src[3].Value = 1.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Sign(src);

            // assert
            Assert.Equal(-1.0, newArray[0].Value);
            Assert.Equal(-1.0, newArray[1].Value);
            Assert.Equal(0.0, newArray[2].Value);
            Assert.Equal(1.0, newArray[3].Value);
        }

        [Fact]
        public void Sin()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Sin(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - -1.0) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 1.0) < Epsilon);
        }

        [Fact]
        public void Sinh()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Sinh(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - -2.3012989023072947) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 2.3012989023072947) < Epsilon);
        }

        [Fact]
        public void Sqrt()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = 1.0;
            srcArray[1].Value = 4.0;
            srcArray[2].Value = 16.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Sqrt(srcArray);

            // assert
            Assert.Equal(1.0, newArray[0].Value);
            Assert.Equal(2.0, newArray[1].Value);
            Assert.Equal(4.0, newArray[2].Value);
        }

        [Fact]
        public void Tan()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Tan(srcArray);

            // assert
            const double Epsilon = 1e12;
            Assert.True(Math.Abs(newArray[0].Value - -16331778728383844) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 16331778728383844) < Epsilon);
        }

        [Fact]
        public void Tanh()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Tanh(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - -0.91715234) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 0.91715234) < Epsilon);
        }

        [Fact]
        public void Truncate()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(ConfigManager.Instance, -1, 1, 6);

            // action
            var newArray = ElementWiseMathFunction<double>.Truncate(srcArray);

            // assert
            Assert.Equal(-1.0, newArray[0].Value);
            Assert.Equal(0.0, newArray[1].Value);
            Assert.Equal(0.0, newArray[2].Value);
            Assert.Equal(0.0, newArray[3].Value);
            Assert.Equal(0.0, newArray[4].Value);
            Assert.Equal(1.0, newArray[5].Value);
        }
    }
}