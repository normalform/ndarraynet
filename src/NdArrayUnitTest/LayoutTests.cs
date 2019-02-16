// <copyright file="LayoutTests.cs" company="NdArrayNet">
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
    public class LayoutTests
    {
        [TestMethod]
        public void OrderedStride_case1()
        {
            // arange
            var shape = new[] { 1, 2, 3, 4, 5 };
            var order = Enumerable.Range(0, shape.Length).Reverse().ToArray();

            // action
            var result = Layout.OrderedStride(shape, order);

            // assert
            var expected = new[] { 120, 60, 20, 5, 1 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OrderedStride_case2()
        {
            // arange
            var shape = new[] { 5, 4, 3, 2, 1 };
            var order = Enumerable.Range(0, shape.Length).Reverse().ToArray();

            // action
            var result = Layout.OrderedStride(shape, order);

            // assert
            var expected = new[] { 24, 6, 2, 1, 1 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void OrderedStride_case3()
        {
            // arange
            var shape = new[] { 0, 1, 2, 3, 4 };
            var order = Enumerable.Range(0, shape.Length).Reverse().ToArray();

            // action
            var result = Layout.OrderedStride(shape, order);

            // assert
            var expected = new[] { 24, 24, 12, 4, 1 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OrderedStride_OrderIsNotPermutation_ThrowException()
        {
            // arange
            var shape = new[] { 0, 1, 2, 3, 4 };
            var order = new[] { 1, 2, 3, 4, 5 };

            // action
            var _ = Layout.OrderedStride(shape, order);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OrderedStride_DifferentSize_ThrowException()
        {
            // arange
            var shape = new[] { 0, 1, 2, 3, 4 };
            var order = new[] { 0, 1 };

            // action
            var _ = Layout.OrderedStride(shape, order);
        }

        [TestMethod]
        public void CStride()
        {
            // arange
            var shape = new[] { 1, 2, 3, 4, 5 };

            // action
            var result = Layout.CStride(shape);

            // assert
            var expected = new[] { 120, 60, 20, 5, 1 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FStride()
        {
            // arange
            var shape = new[] { 1, 2, 3, 4, 5 };

            // action
            var result = Layout.FStride(shape);

            // assert
            var expected = new[] { 1, 1, 2, 6, 24 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void NewC()
        {
            // arange
            var shape = new[] { 1, 2, 3, 4, 5 };

            // action
            var result = Layout.NewC(shape);

            // assert
            var expected = new[] { 120, 60, 20, 5, 1 };
            CollectionAssert.AreEqual(shape, result.Shape);
            CollectionAssert.AreEqual(expected, result.Stride);
        }

        [TestMethod]
        public void NewF()
        {
            // arange
            var shape = new[] { 1, 2, 3, 4, 5 };

            // action
            var result = Layout.NewF(shape);

            // assert
            var expected = new[] { 1, 1, 2, 6, 24 };
            CollectionAssert.AreEqual(shape, result.Shape);
            CollectionAssert.AreEqual(expected, result.Stride);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Swap_NegativeDimensionToSwap_ThrowExceptions()
        {
            // arange
            var shape = new[] { 1, 2, 3, 4, 5 };
            var layout = Layout.NewC(shape);

            // action
            var _ = Layout.Swap(-1, 2, layout);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Swap_NegativeDimensionToSwapWith_ThrowExceptions()
        {
            // arange
            var shape = new[] { 1, 2, 3, 4, 5 };
            var layout = Layout.NewC(shape);

            // action
            var _ = Layout.Swap(2, -1, layout);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Swap_TooBigDimensionToSwap_ThrowExceptions()
        {
            // arange
            var shape = new[] { 1, 2, 3, 4, 5 };
            var layout = Layout.NewC(shape);

            // action
            var _ = Layout.Swap(9, 2, layout);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Swap_TooBigDimensionToSwapWith_ThrowExceptions()
        {
            // arange
            var shape = new[] { 1, 2, 3, 4, 5 };
            var layout = Layout.NewC(shape);

            // action
            var _ = Layout.Swap(2, 9, layout);
        }

        [TestMethod]
        public void PadLeft()
        {
            // arange
            var shape = new[] { 1, 2 };
            var stride = new[] { 2, 1 };
            var srcLayout = new Layout(shape, 8, stride);

            // action
            var paddedLayout = Layout.PadLeft(srcLayout);

            // assert
            var expectedShape = new[] { 1, 1, 2 };
            var expectedStride = new[] { 0, 2, 1 };
            CollectionAssert.AreEqual(expectedShape, paddedLayout.Shape);
            CollectionAssert.AreEqual(expectedStride, paddedLayout.Stride);
            Assert.AreEqual(8, paddedLayout.Offset);
        }

        [TestMethod]
        public void PadRight()
        {
            // arange
            var shape = new[] { 1, 2 };
            var stride = new[] { 2, 1 };
            var srcLayout = new Layout(shape, 8, stride);

            // action
            var paddedLayout = Layout.PadRight(srcLayout);

            // assert
            var expectedShape = new[] { 1, 2, 1 };
            var expectedStride = new[] { 2, 1, 0 };
            CollectionAssert.AreEqual(expectedShape, paddedLayout.Shape);
            CollectionAssert.AreEqual(expectedStride, paddedLayout.Stride);
            Assert.AreEqual(8, paddedLayout.Offset);
        }

        [TestMethod]
        public void StrideEqual_WithSameStrides_ReturnTrue()
        {
            // arange
            var shape = new[] { 1, 2, 3 };
            var strideA = new[] { 6, 2, 1 };
            var strideB = new[] { 6, 2, 1 };

            // action
            var equal = Layout.StrideEqual(shape, strideA, strideB);

            // assert
            Assert.IsTrue(equal);
        }

        [TestMethod]
        public void StrideEqual_WithDifferentStrides_ReturnFalse()
        {
            // arange
            var shape = new[] { 1, 2, 3 };
            var strideA = new[] { 6, 2, 1 };
            var strideB = new[] { 6, 3, 1 };

            // action
            var equal = Layout.StrideEqual(shape, strideA, strideB);

            // assert
            Assert.IsFalse(equal);
        }

        [TestMethod]
        public void StrideEqual_WithDifferentStridesButZeroShape_ReturnTrue()
        {
            // arange
            const int DontCareA = 100;
            const int DontCareB = 200;

            var shape = new[] { 1, 0, 3 };
            var strideA = new[] { 6, DontCareA, 1 };
            var strideB = new[] { 6, DontCareB, 1 };

            // action
            var equal = Layout.StrideEqual(shape, strideA, strideB);

            // assert
            Assert.IsTrue(equal);
        }

        [TestMethod]
        public void IsC_WithC_ReturnTrue()
        {
            // arange
            var layoutStyleC = Layout.NewC(new[] { 1, 2, 3 });

            // action
            var isC = Layout.IsC(layoutStyleC);

            // assert
            Assert.IsTrue(isC);
        }

        [TestMethod]
        public void IsC_WithF_ReturnFalse()
        {
            // arange
            var layoutStyleF = Layout.NewF(new[] { 1, 2, 3 });

            // action
            var isC = Layout.IsC(layoutStyleF);

            // assert
            Assert.IsFalse(isC);
        }

        [TestMethod]
        public void IsF_WithC_ReturnFalse()
        {
            // arange
            var layoutStyleC = Layout.NewC(new[] { 1, 2, 3 });

            // action
            var isF = Layout.IsF(layoutStyleC);

            // assert
            Assert.IsFalse(isF);
        }

        [TestMethod]
        public void IsF_WithF_ReturnTrue()
        {
            // arange
            var layoutStyleF = Layout.NewF(new[] { 1, 2, 3 });

            // action
            var isF = Layout.IsF(layoutStyleF);

            // assert
            Assert.IsTrue(isF);
        }

        [TestMethod]
        public void HasContiguousMemory_WithC_ReturnTrue()
        {
            // arange
            var layoutStyleC = Layout.NewC(new[] { 1, 2, 3 });

            // action
            var continuous = Layout.HasContiguousMemory(layoutStyleC);

            // assert
            Assert.IsTrue(continuous);
        }

        [TestMethod]
        public void HasContiguousMemory_WithF_ReturnTrue()
        {
            // arange
            var layoutStyleF = Layout.NewF(new[] { 1, 2, 3 });

            // action
            var continuous = Layout.HasContiguousMemory(layoutStyleF);

            // assert
            Assert.IsTrue(continuous);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BraodcastDim_NegativeSize_ThrowException()
        {
            // arange
            var layout = Layout.NewC(new[] { 1, 2, 3 });

            // action
            Layout.BraodcastDim(0, -1, layout);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BraodcastDim_InvalidShape_ThrowException()
        {
            // arange
            const int ShapeValue2 = 2;
            const int DimOfTheShapeValue2 = 1;
            var layout = Layout.NewC(new[] { 1, ShapeValue2, 3 });
            const int DummyValue = 9;

            // action
            Layout.BraodcastDim(DimOfTheShapeValue2, DummyValue, layout);
        }

        [TestMethod]
        public void BraodcastDim_Valid_ReturnNewLayout()
        {
            // arange
            const int ShapeValue1 = 1;
            const int DimOfTheShapeValue1 = 0;
            var layout = Layout.NewC(new[] { ShapeValue1, 2, 3 });

            const int NewShapeValue = 9;

            // action
            var newLayout = Layout.BraodcastDim(DimOfTheShapeValue1, NewShapeValue, layout);

            // assert
            var expectedShape = new[] { NewShapeValue, 2, 3 };
            var expectedStride = new[] { 0, 3, 1 };
            CollectionAssert.AreEqual(expectedShape, newLayout.Shape);
            CollectionAssert.AreEqual(expectedStride, newLayout.Stride);
            Assert.AreEqual(0, newLayout.Offset);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BroadcastToShape_LowerBroadcastShapeRank_ThrowException()
        {
            // arange
            var layout = Layout.NewC(new[] { 1, 2, 3 });
            var broadcastShape = new[] { 2, 3 };

            // action
            var _ = Layout.BroadcastToShape(broadcastShape, layout);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BroadcastToShape_InvalidShapeValue_ThrowException()
        {
            // arange
            const int InvalidShapeValue = 9;
            var layout = Layout.NewC(new[] { 2, 3, 4, 5 });
            var broadcastShape = new[] { 2, 3, InvalidShapeValue, 5 };

            // action
            var _ = Layout.BroadcastToShape(broadcastShape, layout);
        }

        [TestMethod]
        public void BroadcastToShape_WithDifferentValidShape_ReturnBroadcastLayout()
        {
            // arange
            var layout = Layout.NewC(new[] { 2, 3 });
            var broadcastShape = new[] { 1, 7, 2, 3 };

            // action
            var broadcastLayout = Layout.BroadcastToShape(broadcastShape, layout);

            // assert
            var expectedShape = new[] { 1, 7, 2, 3 };
            var expectedStride = new[] { 0, 0, 3, 1 };
            CollectionAssert.AreEqual(expectedShape, broadcastLayout.Shape);
            CollectionAssert.AreEqual(expectedStride, broadcastLayout.Stride);
            Assert.AreEqual(0, broadcastLayout.Offset);
        }

        [TestMethod]
        public void BroadcastToShape_WithSameShape_ReturnBroadcastLayout()
        {
            // arange
            var layout = Layout.NewC(new[] { 1, 7, 2, 3 });
            var broadcastShape = new[] { 1, 7, 2, 3 };

            // action
            var broadcastLayout = Layout.BroadcastToShape(broadcastShape, layout);

            // assert
            var expectedShape = new[] { 1, 7, 2, 3 };
            var expectedStride = new[] { 42, 6, 3, 1 };
            CollectionAssert.AreEqual(expectedShape, broadcastLayout.Shape);
            CollectionAssert.AreEqual(expectedStride, broadcastLayout.Stride);
            Assert.AreEqual(0, broadcastLayout.Offset);
        }

        [TestMethod]
        public void TryReshape_WithoutCopyCase1_ReturnNewLayout()
        {
            // arange
            var array = NdArray<int>.Arange(HostDevice.Instance, 0, 10, 1);

            // action
            var newShape = new[] { 10, 1 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            CollectionAssert.AreEqual(newShape, newLayout.Shape);
        }

        [TestMethod]
        public void TryReshape_WithoutCopyCase2_ReturnNewLayout()
        {
            // arange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 10, 1 });

            // action
            var newShape = new[] { 10 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            CollectionAssert.AreEqual(newShape, newLayout.Shape);
        }

        [TestMethod]
        public void TryReshape_WithoutCopyCase3_ReturnNewLayout()
        {
            // arange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 10, 1 });

            // action
            var newShape = new[] { 1, 10 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            CollectionAssert.AreEqual(newShape, newLayout.Shape);
        }

        [TestMethod]
        public void TryReshape_WithoutCopyCase4_ReturnNewLayout()
        {
            // arange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 10, 1 });

            // action
            var newShape = new[] { 10, 1 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            CollectionAssert.AreEqual(newShape, newLayout.Shape);
        }

        [TestMethod]
        public void TryReshape_WithoutCopyCase5_ReturnNewLayout()
        {
            // arange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 10 });

            // action
            var newShape = new[] { 2, 5 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            CollectionAssert.AreEqual(newShape, newLayout.Shape);
        }

        [TestMethod]
        public void TryReshape_WithoutCopyCase6_ReturnNewLayout()
        {
            // arange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 6, 8 });

            // action
            var newShape = new[] { 2, 12, 2 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            CollectionAssert.AreEqual(newShape, newLayout.Shape);
        }

        [TestMethod]
        public void TryReshape_DifferentOrderWithoutCopy_ReturnNewShape()
        {
            // arange
            var array = new NdArray<int>(new[] { 1, 1, 1 }, HostDevice.Instance, Order.ColumnMajor);

            // action
            var newShape = new[] { 1, 1, 1 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            CollectionAssert.AreEqual(newShape, newLayout.Shape);
        }

        [TestMethod]
        public void TryReshape_NeedCopyCase1_ReturnNull()
        {
            // arange
            var array = new NdArray<int>(new[] { 6, 8 }, HostDevice.Instance, Order.ColumnMajor);

            // action
            var newShape = new[] { 8, 6 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            Assert.IsNull(newLayout);
        }

        [TestMethod]
        public void TryReshape_NeedCopyCase2_ReturnNull()
        {
            // arange
            var array = new NdArray<int>(new[] { 6, 8 }, HostDevice.Instance, Order.ColumnMajor);

            // action
            var newShape = new[] { 2, 12, 2 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            Assert.IsNull(newLayout);
        }

        [TestMethod]
        public void TryReshape_WithRemainder_ReturnNewLayout()
        {
            // arange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 2, 3, 4 });

            // action
            var newShape = new[] { 4, SpecialIdx.Remainder, 2 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            CollectionAssert.AreEqual(new[] { 4, 3, 2 }, newLayout.Shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TryReshape_DifferentNumElements_ThrowException()
        {
            // arange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 10 });

            // action
            var newShape = new[] { 3, 2, 5 };
            var newLayout = Layout.TryReshape(newShape, array);
        }
    }
}