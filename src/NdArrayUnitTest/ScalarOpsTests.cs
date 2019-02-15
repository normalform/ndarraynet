// <copyright file="ScalarOpsTests.cs" company="NdArrayNet">
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
            // arange
            var data = new int[1];
            var scalar = new DataAndLayout<int>(data, new FastAccess(new Layout(new int[] {}, 0, new int[] { })));

            // action
            const int FillPattern = 999;
            ScalarOps.Fill(FillPattern, scalar);

            // assert
            Assert.IsTrue(data.All(d => d == FillPattern));
        }

        [TestMethod]
        public void Fill_Vector1D()
        {
            // arange
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
            // arange
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
            // arange
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
            // arange
            var targetData = new int[1];
            var srcData = new int[1];

            var target = new DataAndLayout<int>(targetData, new FastAccess(new Layout(new int[] {}, 0, new int[] {})));
            var src = new DataAndLayout<int>(srcData, new FastAccess(new Layout(new int[] {}, 0, new int[] {})));
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
            // arange
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
            // arange
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
    }
}