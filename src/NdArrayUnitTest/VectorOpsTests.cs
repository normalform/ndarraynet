// <copyright file="VectorOpsTests.cs" company="NdArrayNet">
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
    using System.Linq;
    using System.Numerics;

    [TestClass]
    public class VectorOpsTests
    {
        [TestMethod]
        public void CanUseSrc_WithNullSrc_ReturnTrue()
        {
            // arange
            const int DummyDimValue = 0;

            // action
            var canUse = VectorOps.CanUseSrc<int>(DummyDimValue, null);

            // assert
            Assert.IsTrue(canUse);
        }

        [TestMethod]
        public void CanUseSrc_ContinuousStride_ReturnTrue()
        {
            // arange
            const int BufferSize = 6;
            const int NumDim = 3;
            var data = new int[BufferSize];
            var src = new DataAndLayout<int>(data, new FastAccess(new Layout(new int[] { 1, 2, 3 }, 0, new int[] { 6, 3, 1 })));

            // action
            var canUse = VectorOps.CanUseSrc(NumDim, src);

            // assert
            Assert.IsTrue(canUse);
        }

        [TestMethod]
        public void CanUseSrc_NoStride_ReturnTrue()
        {
            // arange
            const int BufferSize = 1;
            const int NumDim = 1;
            var data = new int[BufferSize];
            var src = new DataAndLayout<int>(data, new FastAccess(new Layout(new int[] { 1 }, 0, new int[] { 0 })));

            // action
            var canUse = VectorOps.CanUseSrc(NumDim, src);

            // assert
            Assert.IsTrue(canUse);
        }

        [TestMethod]
        public void CanUseSrc_NotContinuousStride_ReturnFalse()
        {
            // arange
            const int BufferSize = 6;
            const int NumDim = 3;
            var data = new int[BufferSize];
            var src = new DataAndLayout<int>(data, new FastAccess(new Layout(new int[] { 1, 2, 3 }, 0, new int[] { 6, 3, 2 })));

            // action
            var canUse = VectorOps.CanUseSrc(NumDim, src);

            // assert
            Assert.IsFalse(canUse);
        }

        [TestMethod]
        public void CanUse_ScalarTarget_ReturnFalse()
        {
            // arange
            var target = new DataAndLayout<int>(new int[1], new FastAccess(new Layout(new int[] { }, 0, new int[] { })));

            // action
            var canUse = VectorOps.CanUse(target);

            // assert
            Assert.IsFalse(canUse);
        }

        [TestMethod]
        public void CanUse_UnSupportedType_ReturnFalse()
        {
            // arange
            var data = new UnSupportedType[1];
            var target = new DataAndLayout<UnSupportedType>(data, new FastAccess(new Layout(new int[] { }, 0, new int[] { })));

            // action
            var canUse = VectorOps.CanUse(target);

            // assert
            Assert.IsFalse(canUse);
        }

        [TestMethod]
        public void CanUse_TargetIsNotContinuous_ReturnFalse()
        {
            // arange
            const int NotContinuousStride = 2;
            var target = new DataAndLayout<int>(new int[20], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { NotContinuousStride })));

            // action
            var canUse = VectorOps.CanUse(target);

            // assert
            Assert.IsFalse(canUse);
        }

        [TestMethod]
        public void CanUse_GoodTargetOnly_ReturnTrue()
        {
            // arange
            var target = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));

            // action
            var canUse = VectorOps.CanUse(target);

            // assert
            Assert.IsTrue(canUse);
        }

        [TestMethod]
        public void CanUse_CanNotUseSrc1_ReturnFalse()
        {
            // arange
            var target = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));

            const int NotContinuousStride = 2;
            var src1 = new DataAndLayout<int>(new int[20], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { NotContinuousStride })));

            // action
            var canUse = VectorOps.CanUse(target, src1);

            // assert
            Assert.IsFalse(canUse);
        }

        [TestMethod]
        public void CanUse_CanUseSrc1_ReturnTrue()
        {
            // arange
            var target = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));

            // action
            var canUse = VectorOps.CanUse(target, src1);

            // assert
            Assert.IsTrue(canUse);
        }

        [TestMethod]
        public void CanUse_CanNotUseSrc2Only_ReturnFalse()
        {
            // arange
            var target = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));

            const int NotContinuousStride = 2;
            var src2 = new DataAndLayout<int>(new int[20], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { NotContinuousStride })));

            // action
            var canUse = VectorOps.CanUse(target, src1, src2);

            // assert
            Assert.IsFalse(canUse);
        }

        [TestMethod]
        public void CanUse_CanNotUseSrc1Only_ReturnFalse()
        {
            // arange
            var target = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));
            var src2 = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));

            const int NotContinuousStride = 2;
            var src1 = new DataAndLayout<int>(new int[20], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { NotContinuousStride })));

            // action
            var canUse = VectorOps.CanUse(target, src1, src2);

            // assert
            Assert.IsFalse(canUse);
        }

        [TestMethod]
        public void CanUse_CanUseBothSrc1AndSrc2_ReturnTrue()
        {
            // arange
            var target = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));
            var src1 = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));
            var src2 = new DataAndLayout<int>(new int[10], new FastAccess(new Layout(new int[] { 10 }, 0, new int[] { 1 })));

            // action
            var canUse = VectorOps.CanUse(target, src1, src2);

            // assert
            Assert.IsTrue(canUse);
        }

        [TestMethod]
        public void Fill()
        {
            // arange
            var vectorCount = Vector<int>.Count;

            // Itentionally break the alignment for the Vector operation to test more code.
            var bufferSize = 10 + (vectorCount / 2);
            var data = new int[bufferSize];
            var target = new DataAndLayout<int>(data, new FastAccess(new Layout(new int[] { bufferSize }, 0, new int[] { 1 })));

            const int FillPattern = 999;

            // action
            VectorOps.Fill(FillPattern, target);

            // assert
            Assert.IsTrue(data.All(d => d == FillPattern));
        }

        [TestMethod]
        public void Copy()
        {
            // arange
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
            Assert.IsTrue(Enumerable.SequenceEqual(targetData, srcData));
        }

        private struct UnSupportedType
        {
        }
    }
}