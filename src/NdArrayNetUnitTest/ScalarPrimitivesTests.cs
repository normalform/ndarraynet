// <copyright file="ScalarPrimitivesTests.cs" company="NdArrayNet">
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

namespace ScalarPrimitivesUnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NdArrayNet;
    using System;
    using System.Linq.Expressions;

    [TestClass]
    public class ScalarPrimitivesTests
    {
        [TestMethod]
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
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CompileAny_NoExpressions_InvalidOperationException()
        {
            // arrange
            var errExpr = new Expression<Func<int, int, int>>[] { };

            // action
            var fun = ScalarPrimitives<int, int>.CompileAny(errExpr);
        }

        [TestMethod]
        public void Add_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Add(3, 4);

            // assert
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Add_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Add(3.0, 4.0);

            // assert
            Assert.AreEqual(7.0, result);
        }

        [TestMethod]
        public void Subtract_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Subtract(3, 4);

            // assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Subtract_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Subtract(3, 4);

            // assert
            Assert.AreEqual(-1.0, result);
        }

        [TestMethod]
        public void Multiply_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Multiply(3, 4);

            // assert
            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void Multiply_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Multiply(3.0, 4.0);

            // assert
            Assert.AreEqual(12.0, result);
        }

        [TestMethod]
        public void Divide_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Divide(3, 4);

            // assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divide_IntDiviceByZero_DividecByZeroException()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Divide(3, 0);
        }

        [TestMethod]
        public void Divide_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Divide(3.0, 4.0);

            // assert
            Assert.AreEqual(0.75, result);
        }

        [TestMethod]
        public void Divide_PostiveDoubleDivideByZero_ReturnPositiveInfinity()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Divide(3.0, 0.0);

            // assert
            Assert.AreEqual(double.PositiveInfinity, result);
        }

        [TestMethod]
        public void Divide_NegativeDoubleDivideByZero_ReturnNegativeInfinity()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Divide(-3.0, 0.0);

            // assert
            Assert.AreEqual(double.NegativeInfinity, result);
        }

        [TestMethod]
        public void Divide_DoubleDivide_ReturnNan()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Divide(0.0, 0.0);

            // assert
            Assert.AreEqual(double.NaN, result);
        }

        [TestMethod]
        public void Modulo_IntCase1_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Modulo(3, 4);

            // assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Modulo_IntCase2_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Modulo(4, 3);

            // assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Modulo_IntCase3_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Modulo(4, -3);

            // assert
            // NOTE: The Python returns -2
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Modulo_DoubleCase1_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Modulo(3.0, 4.0);

            // assert
            Assert.AreEqual(3.0, result);
        }

        [TestMethod]
        public void Modulo_DoubleCase2_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Modulo(4.0, 3.0);

            // assert
            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        public void Modulo_DoubleByZero_ReturnNan()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Modulo(4.0, 0.0);

            // assert
            Assert.AreEqual(double.NaN, result);
        }

        [TestMethod]
        public void Power_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Power(3, 4);

            // assert
            Assert.AreEqual(81, result);
        }

        [TestMethod]
        public void Power_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Power(3.0, 4.0);

            // assert
            Assert.AreEqual(81.0, result);
        }

        [TestMethod]
        public void Abs_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Abs(-3);

            // assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Abs_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Abs(-3.0);

            // assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Sign_IntPositive_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Sign(3);

            // assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Sign_DoublePositive_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Sign(3.0);

            // assert
            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        public void Sign_IntNegative_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Sign(-3);

            // assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Sign_DoubleNegative_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Sign(-3.0);

            // assert
            Assert.AreEqual(-1.0, result);
        }

        [TestMethod]
        public void Log_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Log(1);

            // assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Log_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Log(Math.E);

            // assert
            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        public void Log_Int0_ReturnIntMin()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Log(0);

            // assert
            Assert.AreEqual(int.MinValue, result);
        }

        [TestMethod]
        public void Log_Double0_ReturnNegativeInfinity()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Log(0.0);

            // assert
            Assert.AreEqual(double.NegativeInfinity, result);
        }

        [TestMethod]
        public void Log_IntNegative_ReturnIntMin()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Log(-1);

            // assert
            Assert.AreEqual(int.MinValue, result);
        }

        [TestMethod]
        public void Log_DoubleNegative_ReturnNan()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Log(-1.0);

            // assert
            Assert.AreEqual(double.NaN, result);
        }

        [TestMethod]
        public void Log10_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Log10(10);

            // assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Log10_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Log10(10);

            // assert
            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        public void Exp_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Exp(0);

            // assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Exp_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Exp(1);

            // assert
            Assert.AreEqual(Math.E, result);
        }

        [TestMethod]
        public void Sin_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Sin(0);

            // assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Sin_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Sin(Math.PI / 2.0);

            // assert
            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        public void Cos_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Cos(0);

            // assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Cos_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Cos(Math.PI);

            // assert
            Assert.AreEqual(-1.0, result);
        }

        [TestMethod]
        public void Tan_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Tan(2);

            // assert
            Assert.AreEqual(-2, result);
        }

        [TestMethod]
        public void Tan_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Tan(Math.PI / 4.0);

            // assert
            const double Epsilon = 1e-10;
            Assert.IsTrue(Math.Abs(1.0 - result) < Epsilon);
        }

        [TestMethod]
        public void Asin_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Asin(1);

            // assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Asin_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Asin(1.0);

            // assert
            const double Epsilon = 1e-10;
            Assert.IsTrue(Math.Abs(1.5707963267949 - result) < Epsilon);
        }

        [TestMethod]
        public void Acos_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Acos(-1);

            // assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Acos_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Acos(0);

            // assert
            const double Epsilon = 1e-10;
            Assert.IsTrue(Math.Abs(1.5707963267949 - result) < Epsilon);
        }

        [TestMethod]
        public void Atan_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Atan(-2);

            // assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Atan_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Atan(1);

            // assert
            const double Epsilon = 1e-10;
            Assert.IsTrue(Math.Abs(0.785398163397448 - result) < Epsilon);
        }

        [TestMethod]
        public void Sinh_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Sinh(1);

            // assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Sinh_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Sinh(1);

            // assert
            const double Epsilon = 1e-10;
            Assert.IsTrue(Math.Abs(1.1752011936438 - result) < Epsilon);
        }

        [TestMethod]
        public void Cosh_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Cosh(1);

            // assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Cosh_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Cosh(1);

            // assert
            const double Epsilon = 1e-10;
            Assert.IsTrue(Math.Abs(1.54308063481524 - result) < Epsilon);
        }

        [TestMethod]
        public void Tanh_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Tanh(1);

            // assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Tanh_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Tanh(1);

            // assert
            const double Epsilon = 1e-10;
            Assert.IsTrue(Math.Abs(0.761594155955765 - result) < Epsilon);
        }

        [TestMethod]
        public void Sqrt_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Sqrt(9);

            // assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Sqrt_Double_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Sqrt(9);

            // assert
            Assert.AreEqual(3.0, result);
        }

        [TestMethod]
        public void Ceiling_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Ceiling(9);

            // assert
            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void Ceiling_DoublePositive_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Ceiling(1.01);

            // assert
            Assert.AreEqual(2.0, result);
        }

        [TestMethod]
        public void Ceiling_DoubleNegative_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Ceiling(-1.01);

            // assert
            Assert.AreEqual(-1.0, result);
        }

        [TestMethod]
        public void Floor_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Floor(9);

            // assert
            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void Floor_DoublePositive_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Floor(1.9);

            // assert
            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        public void Floor_DoubleNegative_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Floor(-1.9);

            // assert
            Assert.AreEqual(-2.0, result);
        }

        [TestMethod]
        public void Round_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Round(9);

            // assert
            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void Round_DoublePositive_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Round(1.5);

            // assert
            Assert.AreEqual(2.0, result);
        }

        [TestMethod]
        public void Round_DoubleNegative_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Round(-1.5);

            // assert
            Assert.AreEqual(-2.0, result);
        }

        [TestMethod]
        public void Truncate_Int_ReturnInt()
        {
            // arrange
            var sp = new ScalarPrimitives<int, int>();

            // action
            var result = sp.Truncate(9);

            // assert
            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void Truncate_DoublePositive_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Truncate(1.5);

            // assert
            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        public void Truncate_DoubleNegative_ReturnDouble()
        {
            // arrange
            var sp = new ScalarPrimitives<double, double>();

            // action
            var result = sp.Truncate(-1.5);

            // assert
            Assert.AreEqual(-1.0, result);
        }

        [TestMethod]
        public void Convert_IntToLong_ReturnLong()
        {
            // arrange
            var sp = new ScalarPrimitives<long, int>();

            // action
            var result = sp.Convert(100);

            // assert
            Assert.AreEqual(100L, result);
        }

        [TestMethod]
        public void Convert_DoubleToLong_ReturnLong()
        {
            // arrange
            var sp = new ScalarPrimitives<long, double>();

            // action
            var result = sp.Convert(100.0);

            // assert
            Assert.AreEqual(100L, result);
        }

        [TestMethod]
        public void For_IntInt_ReturnScalarPrimitivesIntInt()
        {
            // arrange & action
            var op = ScalarPrimitives.For<int, int>();

            // assert
            Assert.IsInstanceOfType(op, typeof(ScalarPrimitives<int, int>));
        }

        [TestMethod]
        public void For_DoubleInt_ReturnScalarPrimitivesDoubleInt()
        {
            // arrange & action
            var op = ScalarPrimitives.For<double, int>();

            // assert
            Assert.IsInstanceOfType(op, typeof(ScalarPrimitives<double, int>));
        }
    }
}