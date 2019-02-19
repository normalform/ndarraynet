// <copyright file="ShapeFunctionTests.cs" company="NdArrayNet">
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
    public class ShapeFunctionTests
    {
        [TestMethod]
        public void AtLeastNd_SameDimWithInput_ReturnSameNdArray()
        {
            // arrange
            const int MinNumDim = 2;
            var input = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 2 });

            // action
            var output = ShapeFunction<int>.AtLeastNd(MinNumDim, input);

            // assert
            CollectionAssert.AreEqual(new[] { 2, 2 }, output.Shape);
        }

        [TestMethod]
        public void AtLeastNd_SmallerDimThanInput_ReturnSameNdArray()
        {
            // arrange
            const int MinNumDim = 1;
            var input = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 2 });

            // action
            var output = ShapeFunction<int>.AtLeastNd(MinNumDim, input);

            // assert
            CollectionAssert.AreEqual(new[] { 2, 2 }, output.Shape);
        }

        [TestMethod]
        public void AtLeastNd_GreaterDimThanInput_ReturnNewNdArray()
        {
            // arrange
            const int MinNumDim = 3;
            var input = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 2 });

            // action
            var output = ShapeFunction<int>.AtLeastNd(MinNumDim, input);

            // assert
            CollectionAssert.AreEqual(new[] { 1, 2, 2 }, output.Shape);
        }

        [TestMethod]
        public void AtLeast1d()
        {
            // arrange
            const int DummyValue = 1;
            var input = NdArray<int>.Scalar(HostDevice.Instance, DummyValue);

            // action
            var output = ShapeFunction<int>.AtLeast1d(input);

            // assert
            CollectionAssert.AreEqual(new[] { 1 }, output.Shape);
        }

        [TestMethod]
        public void AtLeast2d()
        {
            // arrange
            const int DummyValue = 1;
            var input = NdArray<int>.Scalar(HostDevice.Instance, DummyValue);

            // action
            var output = ShapeFunction<int>.AtLeast2d(input);

            // assert
            CollectionAssert.AreEqual(new[] { 1, 1}, output.Shape);
        }

        [TestMethod]
        public void AtLeast3d()
        {
            // arrange
            const int DummyValue = 1;
            var input = NdArray<int>.Scalar(HostDevice.Instance, DummyValue);

            // action
            var output = ShapeFunction<int>.AtLeast3d(input);

            // assert
            CollectionAssert.AreEqual(new[] { 1, 1, 1 }, output.Shape);
        }

        [TestMethod]
        public void BroadCastDim()
        {
            // arrange
            var input = NdArray<int>.Zeros(HostDevice.Instance, new[] { 3, 1, 5 });

            // action
            var output = ShapeFunction<int>.BroadCastDim(1, 9, input);

            // assert
            CollectionAssert.AreEqual(new[] { 3, 9, 5 }, output.Shape);
        }

        [TestMethod]
        public void BroadCastTo()
        {
            // arrange
            var input = NdArray<int>.Zeros(HostDevice.Instance, new[] { 1, 7, 1 });

            // action
            var output = ShapeFunction<int>.BroadCastTo(new[] { 2, 7, 3 }, input);

            // assert
            CollectionAssert.AreEqual(new[] { 2, 7, 3 }, output.Shape);
        }

        [TestMethod]
        public void BroadCastToSame_Two()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 4, 5 });
            var input2 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 3, 4, 5 });

            // action
            var (output1, output2) = ShapeFunction<int>.BroadCastToSame(input1, input2);

            // assert
            CollectionAssert.AreEqual(new[] { 3, 4, 5 }, output1.Shape);
            CollectionAssert.AreEqual(new[] { 3, 4, 5 }, output2.Shape);
        }

        [TestMethod]
        public void BroadCastToSame_Three()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 5 });
            var input2 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 4, 5 });
            var input3 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 3, 4, 5 });

            // action
            var (output1, output2, output3) = ShapeFunction<int>.BroadCastToSame(input1, input2, input3);

            // assert
            CollectionAssert.AreEqual(new[] { 3, 4, 5 }, output1.Shape);
            CollectionAssert.AreEqual(new[] { 3, 4, 5 }, output2.Shape);
            CollectionAssert.AreEqual(new[] { 3, 4, 5 }, output3.Shape);
        }

        [TestMethod]
        public void BroadCastToSame_Many()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 5 });
            var input2 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 4, 5 });
            var input3 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 3, 4, 5 });
            var input4 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 3, 4, 5 });

            // action
            var outputs = ShapeFunction<int>.BroadCastToSame(new[] { input1, input2, input3, input4 });

            // assert
            CollectionAssert.AreEqual(new[] { 2, 3, 4, 5 }, outputs[0].Shape);
            CollectionAssert.AreEqual(new[] { 2, 3, 4, 5 }, outputs[1].Shape);
            CollectionAssert.AreEqual(new[] { 2, 3, 4, 5 }, outputs[2].Shape);
            CollectionAssert.AreEqual(new[] { 2, 3, 4, 5 }, outputs[3].Shape);
        }

        [TestMethod]
        public void BroadCastToSameInDims_Two()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 1, 7, 1 });
            var input2 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 3, 4, 5 });

            // action
            var (output1, output2) = ShapeFunction<int>.BroadCastToSameInDims(new[] { 0, 2 }, input1, input2);

            // assert
            CollectionAssert.AreEqual(new[] { 3, 7, 5 }, output1.Shape);
            CollectionAssert.AreEqual(new[] { 3, 4, 5 }, output2.Shape);
        }

        [TestMethod]
        public void BroadCastToSameInDims_Three()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 1, 2, 1 });
            var input2 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 3, 5, 1 });
            var input3 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 3, 4, 5 });

            // action
            var (output1, output2, output3) = ShapeFunction<int>.BroadCastToSameInDims(new[] { 0, 2 }, input1, input2, input3);

            // assert
            CollectionAssert.AreEqual(new[] { 3, 2, 5 }, output1.Shape);
            CollectionAssert.AreEqual(new[] { 3, 5, 5 }, output2.Shape);
            CollectionAssert.AreEqual(new[] { 3, 4, 5 }, output3.Shape);
        }

        [TestMethod]
        public void BroadCastToSameInDims_Many()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 1, 2, 1 });
            var input2 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 3, 5 });
            var input3 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 1, 4, 1 });
            var input4 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 5, 5 });

            // action
            var outputs = ShapeFunction<int>.BroadCastToSameInDims(new[] { 0, 2 }, new[] { input1, input2, input3, input4 });

            // assert
            CollectionAssert.AreEqual(new[] { 2, 2, 5 }, outputs[0].Shape);
            CollectionAssert.AreEqual(new[] { 2, 3, 5 }, outputs[1].Shape);
            CollectionAssert.AreEqual(new[] { 2, 4, 5 }, outputs[2].Shape);
            CollectionAssert.AreEqual(new[] { 2, 5, 5 }, outputs[3].Shape);
        }
    }
}