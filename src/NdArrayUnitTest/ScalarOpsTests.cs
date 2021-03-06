﻿// <copyright file="ScalarOpsTests.cs" company="NdArrayNet">
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
    using NdArrayNet;
    using System;
    using System.Linq;

    [TestClass]
    public class ScalarOpsTests
    {
        [TestMethod]
        public void Fill_Scalar()
        {
            // arrange
            var data = new int[1];
            var scalar = new DataAndLayout<int>(data, new FastAccess(new Layout(new int[] { }, 0, new int[] { })));

            // action
            const int FillPattern = 999;
            ScalarOps.Fill(FillPattern, scalar);

            // assert
            Assert.IsTrue(data.All(d => d == FillPattern));
        }

        [TestMethod]
        public void Fill_Vector1D()
        {
            // arrange
            const int BufferSize = 10;
            var data = new int[BufferSize];
            var target = new DataAndLayout<int>(data, new FastAccess(new Layout(new int[] { BufferSize }, 0, new int[] { 1 })));

            const int FillPattern = 999;

            // action
            ScalarOps.Fill(FillPattern, target);

            // assert
            Assert.IsTrue(data.All(d => d == FillPattern));
        }

        [TestMethod]
        public void Fill_Vector2D()
        {
            // arrange
            var data = new int[24];
            var target = new DataAndLayout<int>(data, new FastAccess(new Layout(new int[] { 2, 3, 4 }, 0, new int[] { 12, 4, 1 })));

            const int FillPattern = 999;

            // action
            ScalarOps.Fill(FillPattern, target);

            // assert
            Assert.IsTrue(data.All(d => d == FillPattern));
        }

        [TestMethod]
        public void FillIncrementing()
        {
            // arrange
            const int BufferSize = 10;
            var data = new int[BufferSize];
            var target = new DataAndLayout<int>(data, new FastAccess(new Layout(new int[] { BufferSize }, 0, new int[] { 1 })));

            // action
            ScalarOps.FillIncrementing(0, 2, target);

            // assert
            var expecedData = Enumerable.Range(0, BufferSize).Select(n => n * 2).ToArray();
            Assert.IsTrue(Enumerable.SequenceEqual(expecedData, data));
        }

        [TestMethod]
        public void Copy_Scalar()
        {
            // arrange
            var targetData = new int[1];
            var srcData = new int[1];

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { }, 0, new int[] { })));
            var src = new DataAndLayout<int>(srcData, new FastAccess(new Layout(new int[] { }, 0, new int[] { })));
            const int CopyPattern = 999;
            srcData[0] = CopyPattern;

            // action
            ScalarOps.Copy(target, src);

            // assert
            Assert.AreEqual(CopyPattern, targetData[0]);
        }

        [TestMethod]
        public void Copy_Vector1D()
        {
            // arrange
            const int BufferSize = 10;
            var targetData = new int[BufferSize];
            var srcData = Enumerable.Range(0, BufferSize).ToArray();

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { BufferSize }, 0, new int[] { 1 })));
            var src = new DataAndLayout<int>(srcData, new FastAccess(new Layout(new int[] { BufferSize }, 0, new int[] { 1 })));

            // action
            ScalarOps.Copy(target, src);

            // assert
            Assert.IsTrue(Enumerable.SequenceEqual(targetData, srcData));
        }

        [TestMethod]
        public void Copy_Vector2D()
        {
            // arrange
            const int BufferSize = 24;
            var targetData = new int[BufferSize];
            var srcData = Enumerable.Range(0, BufferSize).ToArray();

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { 2, 3, 4 }, 0, new int[] { 12, 4, 1 })));
            var src = new DataAndLayout<int>(srcData, new FastAccess(new Layout(new int[] { 2, 3, 4 }, 0, new int[] { 12, 4, 1 })));

            // action
            ScalarOps.Copy(target, src);

            // assert
            Assert.IsTrue(Enumerable.SequenceEqual(targetData, srcData));
        }

        [TestMethod]
        public void Multiply_Scalar()
        {
            // arrange
            var targetData = new int[1];
            var src1Data = new int[1];
            var src2Data = new int[1];

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { }, 0, new int[] { })));
            var src1 = new DataAndLayout<int>(src1Data, new FastAccess(new Layout(new int[] { }, 0, new int[] { })));
            var src2 = new DataAndLayout<int>(src2Data, new FastAccess(new Layout(new int[] { }, 0, new int[] { })));

            const int CopyPattern1 = 4;
            src1Data[0] = CopyPattern1;

            const int CopyPattern2 = 5;
            src2Data[0] = CopyPattern2;

            // action
            ScalarOps.Multiply(target, src1, src2);

            // assert
            const int ExpectedValue = 20;
            Assert.AreEqual(ExpectedValue, targetData[0]);
        }

        [TestMethod]
        public void Multiply_Vector1D()
        {
            // arrange
            const int BufferSize = 10;
            var targetData = new int[BufferSize];
            var src1Data = Enumerable.Range(0, BufferSize).ToArray();
            var src2Data = Enumerable.Range(0, BufferSize).ToArray();

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { BufferSize }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<int>(src1Data, new FastAccess(new Layout(new int[] { BufferSize }, 0, new int[] { 1 })));
            var src2 = new DataAndLayout<int>(src2Data, new FastAccess(new Layout(new int[] { BufferSize }, 0, new int[] { 1 })));

            // action
            ScalarOps.Multiply(target, src1, src2);

            // assert
            var expectedData = src1Data.Select(x => x * x).ToArray();
            Assert.IsTrue(Enumerable.SequenceEqual(expectedData, targetData));
        }

        [TestMethod]
        public void Multiply_Vector2D()
        {
            // arrange
            const int BufferSize = 24;
            var targetData = new int[BufferSize];
            var src1Data = Enumerable.Range(0, BufferSize).ToArray();
            var src2Data = Enumerable.Range(0, BufferSize).ToArray();

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { 2, 3, 4 }, 0, new int[] { 12, 4, 1 })));
            var src1 = new DataAndLayout<int>(src1Data, new FastAccess(new Layout(new int[] { 2, 3, 4 }, 0, new int[] { 12, 4, 1 })));
            var src2 = new DataAndLayout<int>(src2Data, new FastAccess(new Layout(new int[] { 2, 3, 4 }, 0, new int[] { 12, 4, 1 })));

            // action
            ScalarOps.Multiply(target, src1, src2);

            // assert
            var expectedData = src1Data.Select(x => x * x).ToArray();
            Assert.IsTrue(Enumerable.SequenceEqual(expectedData, targetData));
        }

        [TestMethod]
        public void Abs()
        {
            // arrange
            var bufferSize = 10;
            var targetData = new int[bufferSize];
            var srcData = Enumerable.Range(-1 * bufferSize / 2, bufferSize).ToArray();

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));
            var src = new DataAndLayout<int>(srcData, new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));

            // action
            ScalarOps.Abs(target, src);

            // assert
            var allPositive = target.Data.All(v => v >= 0);
            Assert.IsTrue(allPositive);
        }

        [TestMethod]
        public void Acos()
        {
            // arrange
            var targetData = new double[3];
            var srcData = new double[] { -1.0, 0, 1.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { 3 }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { 3 }, 0, new int[] { 1 })));

            // action
            ScalarOps.Acos(target, src);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(targetData[0] - Math.PI) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[1] - (Math.PI / 2.0)) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[2] - 0.0) < Epsilon);
        }

        [TestMethod]
        public void Asin()
        {
            // arrange
            var targetData = new double[3];
            var srcData = new double[] { -1.0, 0, 1.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { 3 }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { 3 }, 0, new int[] { 1 })));

            // action
            ScalarOps.Asin(target, src);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(targetData[0] - (-Math.PI / 2.0)) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[1] - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[2] - (Math.PI / 2.0)) < Epsilon);
        }

        [TestMethod]
        public void Atan()
        {
            // arrange
            var targetData = new double[3];
            var srcData = new double[] { -1.0, 0, 1.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { 3 }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { 3 }, 0, new int[] { 1 })));

            // action
            ScalarOps.Atan(target, src);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(targetData[0] - (-Math.PI / 4.0)) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[1] - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[2] - (Math.PI / 4.0)) < Epsilon);
        }

        [TestMethod]
        public void Ceiling()
        {
            // arrange
            const int NumElements = 6;
            var targetData = new double[NumElements];
            var srcData = new[] { -1.0, -0.6, -0.2, 0.2, 0.6, 1.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Ceiling(target, src);

            // assert
            CollectionAssert.AreEqual(new[] { -1.0, 0.0, 0.0, 1.0, 1.0, 1.0 }, targetData);
        }

        [TestMethod]
        public void Cos()
        {
            // arrange
            const int NumElements = 3;
            var targetData = new double[NumElements];
            var srcData = new[] { -Math.PI / 2.0, -0.0, -Math.PI / 2.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Cos(target, src);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(targetData[0] - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[1] - 1.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[2] - 0.0) < Epsilon);
        }

        [TestMethod]
        public void Cosh()
        {
            // arrange
            const int NumElements = 3;
            var targetData = new double[NumElements];
            var srcData = new[] { -Math.PI / 2.0, -0.0, -Math.PI / 2.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Cosh(target, src);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(targetData[0] - 2.5091784786580567) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[1] - 1.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[2] - 2.5091784786580567) < Epsilon);
        }

        [TestMethod]
        public void Exp()
        {
            // arrange
            const int NumElements = 4;
            var targetData = new double[NumElements];
            var srcData = new[] { -1.0, 0.0, 1.0, 10.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Exp(target, src);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(targetData[0] - 0.36787944117144233) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[1] - 1.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[2] - 2.718281828459045) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[3] - 22026.465794806718) < Epsilon);
        }

        [TestMethod]
        public void Floor()
        {
            // arrange
            const int NumElements = 6;
            var targetData = new double[NumElements];
            var srcData = new[] { -1.0, -0.6, -0.2, 0.2, 0.6, 1.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Floor(target, src);

            // assert
            CollectionAssert.AreEqual(new[] { -1.0, -1.0, -1.0, 0.0, 0.0, 1.0 }, targetData);
        }

        [TestMethod]
        public void Log()
        {
            // arrange
            const int NumElements = 3;
            var targetData = new double[NumElements];
            var srcData = new[] { 1.0, Math.E, 4.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Log(target, src);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(targetData[0] - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[1] - 1.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[2] - 1.3862943611198906) < Epsilon);
        }

        [TestMethod]
        public void Log10()
        {
            // arrange
            const int NumElements = 3;
            var targetData = new double[NumElements];
            var srcData = new[] { 1.0, 10.0, 100.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Log10(target, src);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(targetData[0] - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[1] - 1.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[2] - 2.0) < Epsilon);
        }

        [TestMethod]
        public void Maximum()
        {
            // arrange
            var bufferSize = 10;
            var targetData = new int[bufferSize];
            var src1Data = Enumerable.Range(0, bufferSize);
            var src2Data = src1Data.Reverse();

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<int>(src1Data.ToArray(), new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));
            var src2 = new DataAndLayout<int>(src2Data.ToArray(), new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));

            // action
            ScalarOps.Maximum(target, src1, src2);

            // assert
            var expectedData = src1Data.Zip(src2Data, (s1, s2) => Tuple.Create(s1, s2)).Select(t => Math.Max(t.Item1, t.Item2));
            Assert.IsTrue(Enumerable.SequenceEqual(expectedData, targetData));
        }

        [TestMethod]
        public void Minimum()
        {
            // arrange
            var bufferSize = 10;
            var targetData = new int[bufferSize];
            var src1Data = Enumerable.Range(0, bufferSize);
            var src2Data = src1Data.Reverse();

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<int>(src1Data.ToArray(), new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));
            var src2 = new DataAndLayout<int>(src2Data.ToArray(), new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));

            // action
            ScalarOps.Minimum(target, src1, src2);

            // assert
            var expectedData = src1Data.Zip(src2Data, (s1, s2) => Tuple.Create(s1, s2)).Select(t => Math.Min(t.Item1, t.Item2));
            Assert.IsTrue(Enumerable.SequenceEqual(expectedData, targetData));
        }

        [TestMethod]
        public void Pow()
        {
            // arrange
            const int NumElements = 3;
            var targetData = new double[NumElements];
            var lhsData = new[] { 5.0, 6.0, 7.0 };
            var rhsData = new[] { 2.0, 3.0, 4.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var lhs = new DataAndLayout<double>(lhsData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var rhs = new DataAndLayout<double>(rhsData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Pow(target, lhs, rhs);

            // assert
            Assert.AreEqual(25.0, targetData[0]);
            Assert.AreEqual(216.0, targetData[1]);
            Assert.AreEqual(2401.0, targetData[2]);
        }

        [TestMethod]
        public void Round()
        {
            // arrange
            const int NumElements = 6;
            var targetData = new double[NumElements];
            var srcData = new[] { -1.0, -0.6, -0.2, 0.2, 0.6, 1.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Round(target, src);

            // assert
            CollectionAssert.AreEqual(new[] { -1.0, -1.0, 0.0, 0.0, 1.0, 1.0 }, targetData);
        }

        [TestMethod]
        public void Sign()
        {
            // arrange
            const int NumElements = 4;
            var targetData = new double[NumElements];
            var srcData = new[] { -1.0, -2.0, 0.0, 2.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Sign(target, src);

            // assert
            CollectionAssert.AreEqual(new[] { -1.0, -1.0, 0.0, 1.0 }, targetData);
        }

        [TestMethod]
        public void Sin()
        {
            // arrange
            const int NumElements = 3;
            var targetData = new double[NumElements];
            var srcData = new[] { -Math.PI / 2.0, -0.0, Math.PI / 2.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Sin(target, src);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(targetData[0] - -1.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[1] - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[2] - 1.0) < Epsilon);
        }

        [TestMethod]
        public void Sinh()
        {
            // arrange
            const int NumElements = 3;
            var targetData = new double[NumElements];
            var srcData = new[] { -Math.PI / 2.0, -0.0, Math.PI / 2.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Sinh(target, src);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(targetData[0] - -2.3012989023072947) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[1] - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[2] - 2.3012989023072947) < Epsilon);
        }

        [TestMethod]
        public void Sqrt()
        {
            // arrange
            const int NumElements = 3;
            var targetData = new double[NumElements];
            var srcData = new[] { 1.0, 4.0, 16.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Sqrt(target, src);

            // assert
            CollectionAssert.AreEqual(new[] { 1.0, 2.0, 4.0 }, targetData);
        }

        [TestMethod]
        public void Tan()
        {
            // arrange
            const int NumElements = 3;
            var targetData = new double[NumElements];
            var srcData = new[] { -Math.PI / 2.0, -0.0, Math.PI / 2.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Tan(target, src);

            // assert
            const double Epsilon = 1e12;
            Assert.IsTrue(Math.Abs(targetData[0] - -16331778728383844) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[1] - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[2] - 16331778728383844) < Epsilon);
        }

        [TestMethod]
        public void Tanh()
        {
            // arrange
            const int NumElements = 3;
            var targetData = new double[NumElements];
            var srcData = new[] { -Math.PI / 2.0, -0.0, Math.PI / 2.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Tanh(target, src);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(targetData[0] - -0.91715234) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[1] - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(targetData[2] - 0.91715234) < Epsilon);
        }

        [TestMethod]
        public void Truncate()
        {
            // arrange
            const int NumElements = 6;
            var targetData = new double[NumElements];
            var srcData = new[] { -1.0, -0.6, -0.2, 0.2, 0.6, 1.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Truncate(target, src);

            // assert
            CollectionAssert.AreEqual(new[] { -1.0, 0.0, 0.0, 0.0, 0.0, 1.0 }, targetData);
        }

        [TestMethod]
        public void MaxLastAxis()
        {
            // arrange
            const int NumElements = 6;
            var targetData = new double[NumElements];
            var srcData = new[] { -1.0, -0.6, -0.2, 0.2, 0.6, 1.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.MaxLastAxis(target, src);

            // assert
            Assert.AreEqual(1, targetData[0]);
        }

        [TestMethod]
        public void MinLastAxis()
        {
            // arrange
            const int NumElements = 6;
            var targetData = new double[NumElements];
            var srcData = new[] { -1.0, -0.6, -0.2, 0.2, 0.6, 1.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.MinLastAxis(target, src);

            // assert
            Assert.AreEqual(-1, targetData[0]);
        }

        [TestMethod]
        public void SumLastAxis()
        {
            // arrange
            const int NumElements = 6;
            var targetData = new double[NumElements];
            var srcData = new[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.SumLastAxis(target, src);

            // assert
            Assert.AreEqual(6.0, targetData[0]);
        }

        [TestMethod]
        public void ProductLastAxis()
        {
            // arrange
            const int NumElements = 6;
            var targetData = new double[NumElements];
            var srcData = new[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.ProductLastAxis(target, src);

            // assert
            Assert.AreEqual(720.0, targetData[0]);
        }

        [TestMethod]
        public void Negate()
        {
            // arrange
            const int NumElements = 6;
            var targetData = new bool[NumElements];
            var srcData = new[] { true, true, false, false, true, false };

            var target = new DataAndLayout<bool>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<bool>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Negate(target, src);

            // assert
            CollectionAssert.AreEqual(new[] { false, false, true, true, false, true }, targetData);
        }

        [TestMethod]
        public void Equal_bool()
        {
            // arrange
            const int NumElements = 4;
            var targetData = new bool[NumElements];
            var src1Data = new[] { false, false, true, true };
            var src2Data = new[] { false, true, false, true };

            var target = new DataAndLayout<bool>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<bool>(src1Data, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src2 = new DataAndLayout<bool>(src2Data, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Equal(target, src1, src2);

            // assert
            CollectionAssert.AreEqual(new[] { true, false, false, true }, targetData);
        }

        [TestMethod]
        public void Equal_int()
        {
            // arrange
            const int NumElements = 4;
            var targetData = new bool[NumElements];
            var src1Data = new[] { 0, 0, 1, 1 };
            var src2Data = new[] { 0, 1, 0, 1 };

            var target = new DataAndLayout<bool>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<int>(src1Data, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src2 = new DataAndLayout<int>(src2Data, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Equal(target, src1, src2);

            // assert
            CollectionAssert.AreEqual(new[] { true, false, false, true }, targetData);
        }

        [TestMethod]
        public void And()
        {
            // arrange
            const int NumElements = 4;
            var targetData = new bool[NumElements];
            var src1Data = new[] { false, false, true, true };
            var src2Data = new[] { false, true, false, true };

            var target = new DataAndLayout<bool>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<bool>(src1Data, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src2 = new DataAndLayout<bool>(src2Data, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.And(target, src1, src2);

            // assert
            CollectionAssert.AreEqual(new[] { false, false, false, true }, targetData);
        }

        [TestMethod]
        public void Or()
        {
            // arrange
            const int NumElements = 4;
            var targetData = new bool[NumElements];
            var src1Data = new[] { false, false, true, true };
            var src2Data = new[] { false, true, false, true };

            var target = new DataAndLayout<bool>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<bool>(src1Data, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src2 = new DataAndLayout<bool>(src2Data, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Or(target, src1, src2);

            // assert
            CollectionAssert.AreEqual(new[] { false, true, true, true }, targetData);
        }

        [TestMethod]
        public void Xor()
        {
            // arrange
            const int NumElements = 4;
            var targetData = new bool[NumElements];
            var src1Data = new[] { false, false, true, true };
            var src2Data = new[] { false, true, false, true };

            var target = new DataAndLayout<bool>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<bool>(src1Data, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src2 = new DataAndLayout<bool>(src2Data, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.Xor(target, src1, src2);

            // assert
            CollectionAssert.AreEqual(new[] { false, true, true, false }, targetData);
        }

        [TestMethod]
        public void AllLastAxis_AllTrue()
        {
            // arrange
            const int NumElements = 4;
            var targetData = new bool[NumElements];
            var srcData = new[] { true, true, true, true };

            var target = new DataAndLayout<bool>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<bool>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.AllLastAxis(target, src);

            // assert
            CollectionAssert.AreEqual(new[] { true, false, false, false }, targetData);
        }

        [TestMethod]
        public void AllLastAxis_NotAllTrue()
        {
            // arrange
            const int NumElements = 4;
            var targetData = new bool[NumElements];
            var srcData = new[] { false, false, true, true };

            var target = new DataAndLayout<bool>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<bool>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.AllLastAxis(target, src);

            // assert
            CollectionAssert.AreEqual(new[] { false, false, false, false }, targetData);
        }

        [TestMethod]
        public void AnyLastAxis_AllFalse()
        {
            // arrange
            const int NumElements = 4;
            var targetData = new bool[NumElements];
            var srcData = new[] { false, false, false, false };

            var target = new DataAndLayout<bool>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<bool>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.AnyLastAxis(target, src);

            // assert
            CollectionAssert.AreEqual(new[] { false, false, false, false }, targetData);
        }

        [TestMethod]
        public void AnyLastAxis_NotAllFalse()
        {
            // arrange
            const int NumElements = 4;
            var targetData = new bool[NumElements];
            var srcData = new[] { false, false, true, true };

            var target = new DataAndLayout<bool>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<bool>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            ScalarOps.AnyLastAxis(target, src);

            // assert
            CollectionAssert.AreEqual(new[] { true, false, false, false }, targetData);
        }

        [TestMethod]
        public void CountTrueLastAxis()
        {
            // arrange
            var targetData = new int[1];
            var srcData = new[] { false, false, true, true };

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { 1 }, 0, new int[] { 1 })));
            var src = new DataAndLayout<bool>(srcData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));

            // action
            ScalarOps.CountTrueLastAxis(target, src);

            // assert
            Assert.AreEqual(2, targetData[0]);
        }

        [TestMethod]
        public void IfThenElse()
        {
            // arrange
            const int NumElements = 4;
            var conditionData = new[] { false, true, false, true };
            var targetData = new int[NumElements];
            var ifTrueData = new[] { 1, 2, 3, 4 };
            var ifFalseData = new[] { 5, 6, 7, 8 };

            var condition = new DataAndLayout<bool>(conditionData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));
            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));
            var ifTrue = new DataAndLayout<int>(ifTrueData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));
            var ifFalse = new DataAndLayout<int>(ifFalseData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));

            // action
            ScalarOps.IfThenElse(target, condition, ifTrue, ifFalse);

            // assert
            CollectionAssert.AreEqual(new[] { 5, 2, 7, 4 }, targetData);
        }

        [TestMethod]
        public void ArgMaxLastAxis()
        {
            // arrange
            var targetData = new int[4];
            var srcData = new[] { 4, 2, 7, 1 };

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));
            var src = new DataAndLayout<int>(srcData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));

            // action
            ScalarOps.ArgMaxLastAxis(target, src);

            // assert
            Assert.AreEqual(2, targetData[0]);
        }

        [TestMethod]
        public void ArgMinLastAxis()
        {
            // arrange
            var targetData = new int[4];
            var srcData = new[] { 4, 2, 7, 1 };

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));
            var src = new DataAndLayout<int>(srcData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));

            // action
            ScalarOps.ArgMinLastAxis(target, src);

            // assert
            Assert.AreEqual(3, targetData[0]);
        }

        [TestMethod]
        public void FindLastAxis()
        {
            // arrange
            var targetData = new int[4];
            var srcData = new[] { 4, 2, 7, 1 };

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));
            var src = new DataAndLayout<int>(srcData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));

            // action
            ScalarOps.FindLastAxis(2, target, src);

            // assert
            Assert.AreEqual(1, targetData[0]);
        }

        [TestMethod]
        public void FindLastAxis_Notfound()
        {
            // arrange
            var targetData = new int[4];
            var srcData = new[] { 4, 2, 7, 1 };

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));
            var src = new DataAndLayout<int>(srcData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));

            // action
            ScalarOps.FindLastAxis(9, target, src);

            // assert
            Assert.AreEqual(SpecialIdx.NotFound, targetData[0]);
        }

        [TestMethod]
        public void Convert()
        {
            // arrange
            var targetData = new double[4];
            var srcData = new[] { 4, 2, 7, 1 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));
            var source = new DataAndLayout<int>(srcData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));

            // action
            ScalarOps.Convert(target, source);

            // assert
            Assert.IsInstanceOfType(targetData, typeof(double[]));
            CollectionAssert.AreEqual(new[] { 4.0, 2.0, 7.0, 1.0 }, targetData);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Convert_InvalidConvert_ThrowException()
        {
            var targetData = new string[4];
            var srcData = new[] { 4, 2, 7, 1 };

            var target = new DataAndLayout<string>(targetData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));
            var source = new DataAndLayout<int>(srcData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));

            // action
            ScalarOps.Convert(target, source);
        }

        [TestMethod]
        public void TrueIndices()
        {
            // arrange
            var targetData = new int[4];
            var srcData = new bool[] { false, true, false, true };

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));
            var source = new DataAndLayout<bool>(srcData, new FastAccess(new Layout(new int[] { 4 }, 0, new int[] { 1 })));

            // action
            ScalarOps.TrueIndices(target, source);

            // assert
            CollectionAssert.AreEqual(new[] { 1, 3, 1, 3 }, targetData);
        }
    }
}