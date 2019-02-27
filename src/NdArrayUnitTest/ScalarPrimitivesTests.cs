// <copyright file="ScalarPrimitivesTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArrayNet;
    using System;
    using System.Linq.Expressions;
    using Xunit;

    public class ScalarPrimitivesTests
    {
        [Fact]
        public void CompileAny_UsingAdd_ReturnFunctionToAdd()
        {
            // arrange
            var paramA = Expression.Parameter(typeof(int), "a");
            var paramB = Expression.Parameter(typeof(int), "b");

            var exps = new[] { Expression.Lambda<Func<int, int, int>>(Expression.Add(paramA, paramB), paramA, paramB) };

            // action
            var fun = ScalarPrimitives<int, int>.CompileAny(exps);
            var result = fun(1, 2);

            // assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void CompileAny_NoExpressions_InvalidOperationException()
        {
            // arrange
            var errExpr = new Expression<Func<int, int, int>>[] { };

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => ScalarPrimitives<int, int>.CompileAny(errExpr));
            Assert.Equal("Cannot compile scalar primitive for type Int32", exception.Message);
        }

        [Fact]
        public void Add_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Add(3, 4);

            // assert
            Assert.Equal(7, result);
        }

        [Fact]
        public void Add_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Add(3.0, 4.0);

            // assert
            Assert.Equal(7.0, result);
        }

        [Fact]
        public void Subtract_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Subtract(3, 4);

            // assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void Subtract_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Subtract(3, 4);

            // assert
            Assert.Equal(-1.0, result);
        }

        [Fact]
        public void Multiply_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Multiply(3, 4);

            // assert
            Assert.Equal(12, result);
        }

        [Fact]
        public void Multiply_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Multiply(3.0, 4.0);

            // assert
            Assert.Equal(12.0, result);
        }

        [Fact]
        public void Divide_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Divide(3, 4);

            // assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Divide_IntDiviceByZero_DividecByZeroException()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var exception = Assert.Throws<DivideByZeroException>(() => sp.Divide(3, 0));
            Assert.Equal("Attempted to divide by zero.", exception.Message);
        }

        [Fact]
        public void Divide_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Divide(3.0, 4.0);

            // assert
            Assert.Equal(0.75, result);
        }

        [Fact]
        public void Divide_PostiveDoubleDivideByZero_ReturnPositiveInfinity()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Divide(3.0, 0.0);

            // assert
            Assert.Equal(double.PositiveInfinity, result);
        }

        [Fact]
        public void Divide_NegativeDoubleDivideByZero_ReturnNegativeInfinity()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Divide(-3.0, 0.0);

            // assert
            Assert.Equal(double.NegativeInfinity, result);
        }

        [Fact]
        public void Divide_DoubleDivide_ReturnNan()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Divide(0.0, 0.0);

            // assert
            Assert.Equal(double.NaN, result);
        }

        [Fact]
        public void Modulo_IntCase1_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Modulo(3, 4);

            // assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Modulo_IntCase2_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Modulo(4, 3);

            // assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Modulo_IntCase3_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Modulo(4, -3);

            // assert
            // NOTE: The Python returns -2
            Assert.Equal(1, result);
        }

        [Fact]
        public void Modulo_DoubleCase1_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Modulo(3.0, 4.0);

            // assert
            Assert.Equal(3.0, result);
        }

        [Fact]
        public void Modulo_DoubleCase2_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Modulo(4.0, 3.0);

            // assert
            Assert.Equal(1.0, result);
        }

        [Fact]
        public void Modulo_DoubleByZero_ReturnNan()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Modulo(4.0, 0.0);

            // assert
            Assert.Equal(double.NaN, result);
        }

        [Fact]
        public void Power_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Power(3, 4);

            // assert
            Assert.Equal(81, result);
        }

        [Fact]
        public void Power_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Power(3.0, 4.0);

            // assert
            Assert.Equal(81.0, result);
        }

        [Fact]
        public void Abs_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Abs(-3);

            // assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Abs_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Abs(-3.0);

            // assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Sign_IntPositive_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Sign(3);

            // assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Sign_DoublePositive_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Sign(3.0);

            // assert
            Assert.Equal(1.0, result);
        }

        [Fact]
        public void Sign_IntNegative_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Sign(-3);

            // assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void Sign_DoubleNegative_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Sign(-3.0);

            // assert
            Assert.Equal(-1.0, result);
        }

        [Fact]
        public void Log_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Log(1);

            // assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Log_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Log(Math.E);

            // assert
            Assert.Equal(1.0, result);
        }

        [Fact]
        public void Log_Int0_ReturnIntMin()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Log(0);

            // assert
            Assert.Equal(int.MinValue, result);
        }

        [Fact]
        public void Log_Double0_ReturnNegativeInfinity()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Log(0.0);

            // assert
            Assert.Equal(double.NegativeInfinity, result);
        }

        [Fact]
        public void Log_IntNegative_ReturnIntMin()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Log(-1);

            // assert
            Assert.Equal(int.MinValue, result);
        }

        [Fact]
        public void Log_DoubleNegative_ReturnNan()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Log(-1.0);

            // assert
            Assert.Equal(double.NaN, result);
        }

        [Fact]
        public void Log10_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Log10(10);

            // assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Log10_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Log10(10);

            // assert
            Assert.Equal(1.0, result);
        }

        [Fact]
        public void Exp_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Exp(0);

            // assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Exp_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Exp(1);

            // assert
            Assert.Equal(Math.E, result);
        }

        [Fact]
        public void Sin_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Sin(0);

            // assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Sin_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Sin(Math.PI / 2.0);

            // assert
            Assert.Equal(1.0, result);
        }

        [Fact]
        public void Cos_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Cos(0);

            // assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Cos_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Cos(Math.PI);

            // assert
            Assert.Equal(-1.0, result);
        }

        [Fact]
        public void Tan_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Tan(2);

            // assert
            Assert.Equal(-2, result);
        }

        [Fact]
        public void Tan_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Tan(Math.PI / 4.0);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(1.0 - result) < Epsilon);
        }

        [Fact]
        public void Asin_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Asin(1);

            // assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Asin_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Asin(1.0);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(1.5707963267949 - result) < Epsilon);
        }

        [Fact]
        public void Acos_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Acos(-1);

            // assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Acos_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Acos(0);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(1.5707963267949 - result) < Epsilon);
        }

        [Fact]
        public void Atan_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Atan(-2);

            // assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void Atan_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Atan(1);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(0.785398163397448 - result) < Epsilon);
        }

        [Fact]
        public void Sinh_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Sinh(1);

            // assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Sinh_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Sinh(1);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(1.1752011936438 - result) < Epsilon);
        }

        [Fact]
        public void Cosh_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Cosh(1);

            // assert
            Assert.Equal(1, result);
        }

        [Fact]
        public void Cosh_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Cosh(1);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(1.54308063481524 - result) < Epsilon);
        }

        [Fact]
        public void Tanh_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Tanh(1);

            // assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Tanh_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Tanh(1);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(0.761594155955765 - result) < Epsilon);
        }

        [Fact]
        public void Sqrt_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Sqrt(9);

            // assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Sqrt_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Sqrt(9);

            // assert
            Assert.Equal(3.0, result);
        }

        [Fact]
        public void Ceiling_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Ceiling(9);

            // assert
            Assert.Equal(9, result);
        }

        [Fact]
        public void Ceiling_DoublePositive_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Ceiling(1.01);

            // assert
            Assert.Equal(2.0, result);
        }

        [Fact]
        public void Ceiling_DoubleNegative_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Ceiling(-1.01);

            // assert
            Assert.Equal(-1.0, result);
        }

        [Fact]
        public void Floor_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Floor(9);

            // assert
            Assert.Equal(9, result);
        }

        [Fact]
        public void Floor_DoublePositive_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Floor(1.9);

            // assert
            Assert.Equal(1.0, result);
        }

        [Fact]
        public void Floor_DoubleNegative_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Floor(-1.9);

            // assert
            Assert.Equal(-2.0, result);
        }

        [Fact]
        public void Round_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Round(9);

            // assert
            Assert.Equal(9, result);
        }

        [Fact]
        public void Round_DoublePositive_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Round(1.5);

            // assert
            Assert.Equal(2.0, result);
        }

        [Fact]
        public void Round_DoubleNegative_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Round(-1.5);

            // assert
            Assert.Equal(-2.0, result);
        }

        [Fact]
        public void Truncate_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Truncate(9);

            // assert
            Assert.Equal(9, result);
        }

        [Fact]
        public void Truncate_DoublePositive_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Truncate(1.5);

            // assert
            Assert.Equal(1.0, result);
        }

        [Fact]
        public void Truncate_DoubleNegative_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Truncate(-1.5);

            // assert
            Assert.Equal(-1.0, result);
        }

        [Fact]
        public void Convert_IntToLong_ReturnLong()
        {
            // arrange
            var sp = new ScalarPrimitives<long, int>();

            // action
            var result = sp.Convert(100);

            // assert
            Assert.Equal(100L, result);
        }

        [Fact]
        public void Convert_DoubleToLong_ReturnLong()
        {
            // arrange
            var sp = new ScalarPrimitives<long, double>();

            // action
            var result = sp.Convert(100.0);

            // assert
            Assert.Equal(100L, result);
        }

        [Fact]
        public void For_IntInt_ReturnScalarPrimitivesIntInt()
        {
            // arrange & action
            var op = ScalarPrimitivesRegistry.For<int, int>();

            // assert
            Assert.IsType<ScalarPrimitives<int, int>>(op);
        }

        [Fact]
        public void For_DoubleInt_ReturnScalarPrimitivesDoubleInt()
        {
            // arrange & action
            var op = ScalarPrimitivesRegistry.For<double, int>();

            // assert
            Assert.IsType<ScalarPrimitives<double, int>>(op);
        }

        [Fact]
        public void IsFinite_WithIntValue_ReturnTrue()
        {
            // arrange
            const int Value = 1;

            // action
            var isFinite = ScalarPrimitives<int, int>.IsFiniteFunc(Value);

            // assert
            Assert.True(isFinite);
        }

        [Fact]
        public void IsFinite_WithDoubleValue_ReturnTrue()
        {
            // arrange
            const double Value = 1.0;

            // action
            var isFinite = ScalarPrimitives<double, double>.IsFiniteFunc(Value);

            // assert
            Assert.True(isFinite);
        }

        [Fact]
        public void IsFinite_WithDoubleNan_ReturnFalse()
        {
            // arrange
            const double Value = double.NaN;

            // action
            var isFinite = ScalarPrimitives<double, double>.IsFiniteFunc(Value);

            // assert
            Assert.False(isFinite);
        }

        [Fact]
        public void IsFinite_WithDoublePositiveInfinity_ReturnFalse()
        {
            // arrange
            const double Value = double.PositiveInfinity;

            // action
            var isFinite = ScalarPrimitives<double, double>.IsFiniteFunc(Value);

            // assert
            Assert.False(isFinite);
        }

        [Fact]
        public void IsFinite_WithDoubleNegativeInfinity_ReturnFalse()
        {
            // arrange
            const double Value = double.NegativeInfinity;

            // action
            var isFinite = ScalarPrimitives<double, double>.IsFiniteFunc(Value);

            // assert
            Assert.False(isFinite);
        }

        [Fact]
        public void IsFinite_WithFloatValue_ReturnTrue()
        {
            // arrange
            const float Value = 1.0f;

            // action
            var isFinite = ScalarPrimitives<float, float>.IsFiniteFunc(Value);

            // assert
            Assert.True(isFinite);
        }

        [Fact]
        public void IsFinite_WithFloatNan_ReturnFalse()
        {
            // arrange
            const float Value = float.NaN;

            // action
            var isFinite = ScalarPrimitives<float, float>.IsFiniteFunc(Value);

            // assert
            Assert.False(isFinite);
        }

        [Fact]
        public void IsFinite_WithFloatPositiveInfinity_ReturnFalse()
        {
            // arrange
            const float Value = float.PositiveInfinity;

            // action
            var isFinite = ScalarPrimitives<float, float>.IsFiniteFunc(Value);

            // assert
            Assert.False(isFinite);
        }

        [Fact]
        public void IsFinite_WithFloatNegativeInfinity_ReturnFalse()
        {
            // arrange
            const float Value = float.NegativeInfinity;

            // action
            var isFinite = ScalarPrimitives<float, float>.IsFiniteFunc(Value);

            // assert
            Assert.False(isFinite);
        }
    }
}