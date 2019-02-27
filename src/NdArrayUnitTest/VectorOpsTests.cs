// <copyright file="VectorOpsTests.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet.NdArrayUnitTest
{
    using NdArrayNet;
    using System;
    using System.Linq;
    using System.Numerics;
    using Xunit;

    public class VectorOpsTests
    {
        [Fact]
        public void CanUseSrc_WithNullSrc_ReturnTrue()
        {
            // arrange
            const int DummyDimValue = 0;

            // action
            var canUse = VectorOps.CanUseSrc<int>(DummyDimValue, null);

            // assert
            Assert.True(canUse);
        }

        [Fact]
        public void CanUseSrc_ContinuousStride_ReturnTrue()
        {
            // arrange
            const int BufferSize = 6;
            const int NumDim = 3;
            var data = new int[BufferSize];
            var src = new DataAndLayout<int>(data, new FastAccess(new Layout(new int[] { 1, 2, 3 }, 0, new int[] { 6, 3, 1 })));

            // action
            var canUse = VectorOps.CanUseSrc(NumDim, src);

            // assert
            Assert.True(canUse);
        }

        [Fact]
        public void CanUseSrc_NoStride_ReturnTrue()
        {
            // arrange
            const int BufferSize = 1;
            const int NumDim = 1;
            var data = new int[BufferSize];
            var src = new DataAndLayout<int>(data, new FastAccess(new Layout(new int[] { 1 }, 0, new int[] { 0 })));

            // action
            var canUse = VectorOps.CanUseSrc(NumDim, src);

            // assert
            Assert.True(canUse);
        }

        [Fact]
        public void CanUseSrc_NotContinuousStride_ReturnFalse()
        {
            // arrange
            const int BufferSize = 6;
            const int NumDim = 3;
            var data = new int[BufferSize];
            var src = new DataAndLayout<int>(data, new FastAccess(new Layout(new int[] { 1, 2, 3 }, 0, new int[] { 6, 3, 2 })));

            // action
            var canUse = VectorOps.CanUseSrc(NumDim, src);

            // assert
            Assert.False(canUse);
        }

        [Fact]
        public void CanUse_ScalarTarget_ReturnFalse()
        {
            // arrange
            var target = new DataAndLayout<int>(new int[1], new FastAccess(new Layout(new int[] { }, 0, new int[] { })));

            // action
            var canUse = VectorOps.CanUse(target);

            // assert
            Assert.False(canUse);
        }

        [Fact]
        public void CanUse_UnSupportedType_ReturnFalse()
        {
            // arrange
            var data = new UnSupportedTypeForUnitTestOnly[1];
            var target = new DataAndLayout<UnSupportedTypeForUnitTestOnly>(data, new FastAccess(new Layout(new int[] { }, 0, new int[] { })));

            // action
            var canUse = VectorOps.CanUse(target);

            // assert
            Assert.False(canUse);
        }

        [Fact]
        public void CanUse_TargetIsNotContinuous_ReturnFalse()
        {
            // arrange
            const int NotContinuousStride = 2;
            var target = new DataAndLayout<int>(new int[20], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { NotContinuousStride })));

            // action
            var canUse = VectorOps.CanUse(target);

            // assert
            Assert.False(canUse);
        }

        [Fact]
        public void CanUse_GoodTargetOnly_ReturnTrue()
        {
            // arrange
            var target = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));

            // action
            var canUse = VectorOps.CanUse(target);

            // assert
            Assert.True(canUse);
        }

        [Fact]
        public void CanUse_CanNotUseSrc1_ReturnFalse()
        {
            // arrange
            var target = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));

            const int NotContinuousStride = 2;
            var src1 = new DataAndLayout<int>(new int[20], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { NotContinuousStride })));

            // action
            var canUse = VectorOps.CanUse(target, src1);

            // assert
            Assert.False(canUse);
        }

        [Fact]
        public void CanUse_CanUseSrc1_ReturnTrue()
        {
            // arrange
            var target = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));

            // action
            var canUse = VectorOps.CanUse(target, src1);

            // assert
            Assert.True(canUse);
        }

        [Fact]
        public void CanUse_CanNotUseSrc2Only_ReturnFalse()
        {
            // arrange
            var target = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));

            const int NotContinuousStride = 2;
            var src2 = new DataAndLayout<int>(new int[20], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { NotContinuousStride })));

            // action
            var canUse = VectorOps.CanUse(target, src1, src2);

            // assert
            Assert.False(canUse);
        }

        [Fact]
        public void CanUse_CanNotUseSrc1Only_ReturnFalse()
        {
            // arrange
            var target = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));
            var src2 = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));

            const int NotContinuousStride = 2;
            var src1 = new DataAndLayout<int>(new int[20], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { NotContinuousStride })));

            // action
            var canUse = VectorOps.CanUse(target, src1, src2);

            // assert
            Assert.False(canUse);
        }

        [Fact]
        public void CanUse_CanUseBothSrc1AndSrc2_ReturnTrue()
        {
            // arrange
            var target = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));
            var src2 = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));

            // action
            var canUse = VectorOps.CanUse(target, src1, src2);

            // assert
            Assert.True(canUse);
        }

        [Fact]
        public void Fill()
        {
            // arrange
            var vectorCount = Vector<int>.Count;

            // Itentionally break the alignment for the Vector operation to test more code.
            var bufferSize = 10 + (vectorCount / 2);
            var data = new int[bufferSize];
            var target = new DataAndLayout<int>(data, new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));

            const int FillPattern = 999;

            // action
            VectorOps.Fill(FillPattern, target);

            // assert
            Assert.True(data.All(d => d == FillPattern));
        }

        [Fact]
        public void Copy()
        {
            // arrange
            var vectorCount = Vector<int>.Count;

            // Itentionally break the alignment for the Vector operation to test more code.
            var bufferSize = 10 + (vectorCount / 2);
            var targetData = new int[bufferSize];
            var srcData = Enumerable.Range(0, bufferSize).ToArray();

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));
            var src = new DataAndLayout<int>(srcData, new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));

            // action
            VectorOps.Copy(target, src);

            // assert
            Assert.True(Enumerable.SequenceEqual(targetData, srcData));
        }

        [Fact]
        public void Multiply()
        {
            // arrange
            var vectorCount = Vector<int>.Count;

            // Itentionally break the alignment for the Vector operation to test more code.
            var bufferSize = 10 + (vectorCount / 2);
            var targetData = new int[bufferSize];
            var src1Data = Enumerable.Range(0, bufferSize).ToArray();
            var src2Data = Enumerable.Range(0, bufferSize).ToArray();

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<int>(src1Data, new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));
            var src2 = new DataAndLayout<int>(src2Data, new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));

            // action
            VectorOps.Multiply(target, src1, src2);

            // assert
            var expectedData = src1Data.Select(x => x * x).ToArray();
            Assert.True(Enumerable.SequenceEqual(expectedData, targetData));
        }

        [Fact]
        public void Abs()
        {
            // arrange
            var bufferSize = 10;
            var targetData = new int[bufferSize];
            var srcData = Enumerable.Range(-1 * bufferSize / 2, bufferSize).ToArray();

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));
            var src = new DataAndLayout<int>(srcData, new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));

            // action
            VectorOps.Abs(target, src);

            // assert
            var allPositive = target.Data.All(v => v >= 0);
            Assert.True(allPositive);
        }

        [Fact]
        public void Sqrt()
        {
            // arrange
            const int NumElements = 3;
            var targetData = new double[NumElements];
            var srcData = new[] { 1.0, 4.0, 16.0 };

            var target = new DataAndLayout<double>(targetData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));
            var src = new DataAndLayout<double>(srcData, new FastAccess(new Layout(new int[] { NumElements }, 0, new int[] { 1 })));

            // action
            VectorOps.Sqrt(target, src);

            // assert
            Assert.Equal(new[] { 1.0, 2.0, 4.0 }, targetData);
        }

        [Fact]
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
            VectorOps.Maximum(target, src1, src2);

            // assert
            var expectedData = src1Data.Zip(src2Data, (s1, s2) => Tuple.Create(s1, s2)).Select(t => Math.Max(t.Item1, t.Item2));
            Assert.True(Enumerable.SequenceEqual(expectedData, targetData));
        }

        [Fact]
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
            VectorOps.Minimum(target, src1, src2);

            // assert
            var expectedData = src1Data.Zip(src2Data, (s1, s2) => Tuple.Create(s1, s2)).Select(t => Math.Min(t.Item1, t.Item2));
            Assert.True(Enumerable.SequenceEqual(expectedData, targetData));
        }

        private struct UnSupportedTypeForUnitTestOnly
        {
        }
    }
}