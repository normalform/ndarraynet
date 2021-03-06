﻿// <copyright file="ElementWiseMathFunctionTests.cs" company="NdArrayNet">
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

namespace NdArrayNet.NdArrayUnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NdArray.NdArrayImpl;
    using NdArrayNet;
    using System;

    [TestClass]
    public class ElementWiseMathFunctionTests
    {
        [TestMethod]
        public void Abs()
        {
            // arrange
            var srcArray = NdArray<int>.Linspace(HostDevice.Instance, -4, 4, 8);

            // action
            var newArray = ElementWiseMathFunction<int>.Abs(srcArray);

            // assert
            Assert.IsTrue(newArray[0].Value > 0);
        }

        [TestMethod]
        public void Acos()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(HostDevice.Instance, -1, 1, 3);

            // action
            var newArray = ElementWiseMathFunction<double>.Acos(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(newArray[0].Value - Math.PI) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[1].Value - (Math.PI / 2.0)) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[2].Value - 0.0) < Epsilon);
        }

        [TestMethod]
        public void Asin()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(HostDevice.Instance, -1, 1, 3);

            // action
            var newArray = ElementWiseMathFunction<double>.Asin(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(newArray[0].Value - (-Math.PI / 2.0)) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[2].Value - (Math.PI / 2.0)) < Epsilon);
        }

        [TestMethod]
        public void Atan()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(HostDevice.Instance, -1, 1, 3);

            // action
            var newArray = ElementWiseMathFunction<double>.Atan(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(newArray[0].Value - (-Math.PI / 4.0)) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[2].Value - (Math.PI / 4.0)) < Epsilon);
        }

        [TestMethod]
        public void Ceiling()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(HostDevice.Instance, -1, 1, 6);

            // action
            var newArray = ElementWiseMathFunction<double>.Ceiling(srcArray);

            // assert
            Assert.AreEqual(-1.0, newArray[0].Value);
            Assert.AreEqual(0.0, newArray[1].Value);
            Assert.AreEqual(1.0, newArray[4].Value);
        }

        [TestMethod]
        public void Cos()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(HostDevice.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Cos(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(newArray[0].Value - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[2].Value - 0.0) < Epsilon);
        }

        [TestMethod]
        public void Cosh()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(HostDevice.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Cosh(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(newArray[0].Value - 2.5091784786580567) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[2].Value - 2.5091784786580567) < Epsilon);
        }

        [TestMethod]
        public void Exp()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(HostDevice.Instance, new[] { 4 });
            srcArray[0].Value = -1.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = 1.0;
            srcArray[3].Value = 10.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Exp(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(newArray[0].Value - 0.36787944117144233) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[2].Value - 2.718281828459045) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[3].Value - 22026.465794806718) < Epsilon);
        }

        [TestMethod]
        public void Floor()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(HostDevice.Instance, -1, 1, 6);

            // action
            var newArray = ElementWiseMathFunction<double>.Floor(srcArray);

            // assert
            Assert.AreEqual(-1.0, newArray[0].Value);
            Assert.AreEqual(-1.0, newArray[1].Value);
            Assert.AreEqual(-1.0, newArray[2].Value);
            Assert.AreEqual(0.0, newArray[3].Value);
            Assert.AreEqual(0.0, newArray[4].Value);
            Assert.AreEqual(1.0, newArray[5].Value);
        }

        [TestMethod]
        public void Log()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(HostDevice.Instance, new[] { 3 });
            srcArray[0].Value = 1.00;
            srcArray[1].Value = Math.E;
            srcArray[2].Value = 4.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Log(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(newArray[0].Value - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[2].Value - 1.3862943611198906) < Epsilon);
        }

        [TestMethod]
        public void Log10()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(HostDevice.Instance, new[] { 3 });
            srcArray[0].Value = 1.00;
            srcArray[1].Value = 10.0;
            srcArray[2].Value = 100.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Log10(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(newArray[0].Value - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[2].Value - 2.0) < Epsilon);
        }

        [TestMethod]
        public void Maximum()
        {
            // arrange
            var srcArray1 = NdArray<double>.Zeros(HostDevice.Instance, new[] { 3 });
            var srcArray2 = NdArray<double>.Ones(HostDevice.Instance, new[] { 3 });

            // action
            var newArray = ElementWiseMathFunction<double>.Maximum(srcArray1, srcArray2);

            // assert
            Assert.AreEqual(1.0, newArray[0].Value);
            Assert.AreEqual(1.0, newArray[1].Value);
            Assert.AreEqual(1.0, newArray[2].Value);
        }

        [TestMethod]
        public void Minimum()
        {
            // arrange
            var srcArray1 = NdArray<double>.Zeros(HostDevice.Instance, new[] { 3 });
            var srcArray2 = NdArray<double>.Ones(HostDevice.Instance, new[] { 3 });

            // action
            var newArray = ElementWiseMathFunction<double>.Minimum(srcArray1, srcArray2);

            // assert
            Assert.AreEqual(0.0, newArray[0].Value);
            Assert.AreEqual(0.0, newArray[1].Value);
            Assert.AreEqual(0.0, newArray[2].Value);
        }

        [TestMethod]
        public void Pow()
        {
            // arrange
            var lhs = NdArray<double>.Zeros(HostDevice.Instance, new[] { 3 });
            lhs[0].Value = 5.0;
            lhs[1].Value = 6.0;
            lhs[2].Value = 7.0;

            var rhs = NdArray<double>.Zeros(HostDevice.Instance, new[] { 3 });
            rhs[0].Value = 2.00;
            rhs[1].Value = 3.0;
            rhs[2].Value = 4.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Pow(lhs, rhs);

            // assert
            Assert.AreEqual(25.0, newArray[0].Value);
            Assert.AreEqual(216.0, newArray[1].Value);
            Assert.AreEqual(2401.0, newArray[2].Value);
        }

        [TestMethod]
        public void Round()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(HostDevice.Instance, -1, 1, 6);

            // action
            var newArray = ElementWiseMathFunction<double>.Round(srcArray);

            // assert
            Assert.AreEqual(-1.0, newArray[0].Value);
            Assert.AreEqual(-1.0, newArray[1].Value);
            Assert.AreEqual(0.0, newArray[2].Value);
            Assert.AreEqual(0.0, newArray[3].Value);
            Assert.AreEqual(1.0, newArray[4].Value);
            Assert.AreEqual(1.0, newArray[5].Value);
        }

        [TestMethod]
        public void Sign()
        {
            // arrange
            var src = NdArray<double>.Zeros(HostDevice.Instance, new[] { 4 });
            src[0].Value = -2.0;
            src[1].Value = -1.0;
            src[2].Value = 0.0;
            src[3].Value = 1.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Sign(src);

            // assert
            Assert.AreEqual(-1.0, newArray[0].Value);
            Assert.AreEqual(-1.0, newArray[1].Value);
            Assert.AreEqual(0.0, newArray[2].Value);
            Assert.AreEqual(1.0, newArray[3].Value);
        }

        [TestMethod]
        public void Sin()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(HostDevice.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Sin(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(newArray[0].Value - -1.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[2].Value - 1.0) < Epsilon);
        }

        [TestMethod]
        public void Sinh()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(HostDevice.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Sinh(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(newArray[0].Value - -2.3012989023072947) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[2].Value - 2.3012989023072947) < Epsilon);
        }

        [TestMethod]
        public void Sqrt()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(HostDevice.Instance, new[] { 3 });
            srcArray[0].Value = 1.0;
            srcArray[1].Value = 4.0;
            srcArray[2].Value = 16.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Sqrt(srcArray);

            // assert
            Assert.AreEqual(1.0, newArray[0].Value);
            Assert.AreEqual(2.0, newArray[1].Value);
            Assert.AreEqual(4.0, newArray[2].Value);
        }

        [TestMethod]
        public void Tan()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(HostDevice.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Tan(srcArray);

            // assert
            const double Epsilon = 1e12;
            Assert.IsTrue(Math.Abs(newArray[0].Value - -16331778728383844) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[2].Value - 16331778728383844) < Epsilon);
        }

        [TestMethod]
        public void Tanh()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(HostDevice.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = ElementWiseMathFunction<double>.Tanh(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(newArray[0].Value - -0.91715234) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[2].Value - 0.91715234) < Epsilon);
        }

        [TestMethod]
        public void Truncate()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(HostDevice.Instance, -1, 1, 6);

            // action
            var newArray = ElementWiseMathFunction<double>.Truncate(srcArray);

            // assert
            Assert.AreEqual(-1.0, newArray[0].Value);
            Assert.AreEqual(0.0, newArray[1].Value);
            Assert.AreEqual(0.0, newArray[2].Value);
            Assert.AreEqual(0.0, newArray[3].Value);
            Assert.AreEqual(0.0, newArray[4].Value);
            Assert.AreEqual(1.0, newArray[5].Value);
        }
    }
}