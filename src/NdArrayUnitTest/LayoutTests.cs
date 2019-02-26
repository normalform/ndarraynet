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
    using NdArrayNet;
    using System;
    using System.Linq;
    using Xunit;

    public class LayoutTests
    {
        [Fact]
        public void OrderedStride_case1()
        {
            // arrange
            var shape = new[] { 1, 2, 3, 4, 5 };
            var order = Enumerable.Range(0, shape.Length).Reverse().ToArray();

            // action
            var result = Layout.OrderedStride(shape, order);

            // assert
            var expected = new[] { 120, 60, 20, 5, 1 };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void OrderedStride_case2()
        {
            // arrange
            var shape = new[] { 5, 4, 3, 2, 1 };
            var order = Enumerable.Range(0, shape.Length).Reverse().ToArray();

            // action
            var result = Layout.OrderedStride(shape, order);

            // assert
            var expected = new[] { 24, 6, 2, 1, 1 };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void OrderedStride_case3()
        {
            // arrange
            var shape = new[] { 0, 1, 2, 3, 4 };
            var order = Enumerable.Range(0, shape.Length).Reverse().ToArray();

            // action
            var result = Layout.OrderedStride(shape, order);

            // assert
            var expected = new[] { 24, 24, 12, 4, 1 };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void OrderedStride_OrderIsNotPermutation_ThrowException()
        {
            // arrange
            var shape = new[] { 0, 1, 2, 3, 4 };
            var order = new[] { 1, 2, 3, 4, 5 };

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.OrderedStride(shape, order));
            Assert.Equal("The stride order [1,2,3,4,5] is not a permutation", exception.Message);
        }

        [Fact]
        public void OrderedStride_DifferentSize_ThrowException()
        {
            // arrange
            var shape = new[] { 0, 1, 2, 3, 4 };
            var order = new[] { 0, 1 };

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.OrderedStride(shape, order));
            Assert.Equal("The stride order [0,1] is incompatible with the shape [0,1,2,3,4]", exception.Message);
        }

        [Fact]
        public void CStride()
        {
            // arrange
            var shape = new[] { 1, 2, 3, 4, 5 };

            // action
            var result = Layout.CStride(shape);

            // assert
            var expected = new[] { 120, 60, 20, 5, 1 };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FStride()
        {
            // arrange
            var shape = new[] { 1, 2, 3, 4, 5 };

            // action
            var result = Layout.FStride(shape);

            // assert
            var expected = new[] { 1, 1, 2, 6, 24 };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void NewC()
        {
            // arrange
            var shape = new[] { 1, 2, 3, 4, 5 };

            // action
            var result = Layout.NewC(shape);

            // assert
            var expected = new[] { 120, 60, 20, 5, 1 };
            Assert.Equal(shape, result.Shape);
            Assert.Equal(expected, result.Stride);
        }

        [Fact]
        public void NewF()
        {
            // arrange
            var shape = new[] { 1, 2, 3, 4, 5 };

            // action
            var result = Layout.NewF(shape);

            // assert
            var expected = new[] { 1, 1, 2, 6, 24 };
            Assert.Equal(shape, result.Shape);
            Assert.Equal(expected, result.Stride);
        }

        [Fact]
        public void SwapDim_NegativeDimensionToSwap_ThrowExceptions()
        {
            // arrange
            var shape = new[] { 1, 2, 3, 4, 5 };
            var layout = Layout.NewC(shape);

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.SwapDim(-1, 2, layout));
            Assert.Equal("Cannot swap dimension -1 with 2 of for shape [1,2,3,4,5].", exception.Message);
        }

        [Fact]
        public void SwapDim_NegativeDimensionToSwapWith_ThrowExceptions()
        {
            // arrange
            var shape = new[] { 1, 2, 3, 4, 5 };
            var layout = Layout.NewC(shape);

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.SwapDim(2, -1, layout));
            Assert.Equal("Cannot swap dimension 2 with -1 of for shape [1,2,3,4,5].", exception.Message);
        }

        [Fact]
        public void SwapDim_TooBigDimensionToSwap_ThrowExceptions()
        {
            // arrange
            var shape = new[] { 1, 2, 3, 4, 5 };
            var layout = Layout.NewC(shape);

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.SwapDim(9, 2, layout));
            Assert.Equal("Cannot swap dimension 9 with 2 of for shape [1,2,3,4,5].", exception.Message);
        }

        [Fact]
        public void SwapDim_TooBigDimensionToSwapWith_ThrowExceptions()
        {
            // arrange
            var shape = new[] { 1, 2, 3, 4, 5 };
            var layout = Layout.NewC(shape);

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.SwapDim(2, 9, layout));
            Assert.Equal("Cannot swap dimension 2 with 9 of for shape [1,2,3,4,5].", exception.Message);
        }

        [Fact]
        public void PadLeft()
        {
            // arrange
            var shape = new[] { 1, 2 };
            var stride = new[] { 2, 1 };
            var srcLayout = new Layout(shape, 8, stride);

            // action
            var paddedLayout = Layout.PadLeft(srcLayout);

            // assert
            var expectedShape = new[] { 1, 1, 2 };
            var expectedStride = new[] { 0, 2, 1 };
            Assert.Equal(expectedShape, paddedLayout.Shape);
            Assert.Equal(expectedStride, paddedLayout.Stride);
            Assert.Equal(8, paddedLayout.Offset);
        }

        [Fact]
        public void PadRight()
        {
            // arrange
            var shape = new[] { 1, 2 };
            var stride = new[] { 2, 1 };
            var srcLayout = new Layout(shape, 8, stride);

            // action
            var paddedLayout = Layout.PadRight(srcLayout);

            // assert
            var expectedShape = new[] { 1, 2, 1 };
            var expectedStride = new[] { 2, 1, 0 };
            Assert.Equal(expectedShape, paddedLayout.Shape);
            Assert.Equal(expectedStride, paddedLayout.Stride);
            Assert.Equal(8, paddedLayout.Offset);
        }

        [Fact]
        public void StrideEqual_WithSameStrides_ReturnTrue()
        {
            // arrange
            var shape = new[] { 1, 2, 3 };
            var strideA = new[] { 6, 2, 1 };
            var strideB = new[] { 6, 2, 1 };

            // action
            var equal = Layout.StrideEqual(shape, strideA, strideB);

            // assert
            Assert.True(equal);
        }

        [Fact]
        public void StrideEqual_WithDifferentStrides_ReturnFalse()
        {
            // arrange
            var shape = new[] { 1, 2, 3 };
            var strideA = new[] { 6, 2, 1 };
            var strideB = new[] { 6, 3, 1 };

            // action
            var equal = Layout.StrideEqual(shape, strideA, strideB);

            // assert
            Assert.False(equal);
        }

        [Fact]
        public void StrideEqual_WithDifferentStridesButZeroShape_ReturnTrue()
        {
            // arrange
            const int DontCareA = 100;
            const int DontCareB = 200;

            var shape = new[] { 1, 0, 3 };
            var strideA = new[] { 6, DontCareA, 1 };
            var strideB = new[] { 6, DontCareB, 1 };

            // action
            var equal = Layout.StrideEqual(shape, strideA, strideB);

            // assert
            Assert.True(equal);
        }

        [Fact]
        public void IsC_WithC_ReturnTrue()
        {
            // arrange
            var layoutStyleC = Layout.NewC(new[] { 1, 2, 3 });

            // action
            var isC = Layout.IsC(layoutStyleC);

            // assert
            Assert.True(isC);
        }

        [Fact]
        public void IsC_WithF_ReturnFalse()
        {
            // arrange
            var layoutStyleF = Layout.NewF(new[] { 1, 2, 3 });

            // action
            var isC = Layout.IsC(layoutStyleF);

            // assert
            Assert.False(isC);
        }

        [Fact]
        public void IsF_WithC_ReturnFalse()
        {
            // arrange
            var layoutStyleC = Layout.NewC(new[] { 1, 2, 3 });

            // action
            var isF = Layout.IsF(layoutStyleC);

            // assert
            Assert.False(isF);
        }

        [Fact]
        public void IsF_WithF_ReturnTrue()
        {
            // arrange
            var layoutStyleF = Layout.NewF(new[] { 1, 2, 3 });

            // action
            var isF = Layout.IsF(layoutStyleF);

            // assert
            Assert.True(isF);
        }

        [Fact]
        public void HasContiguousMemory_WithC_ReturnTrue()
        {
            // arrange
            var layoutStyleC = Layout.NewC(new[] { 1, 2, 3 });

            // action
            var continuous = Layout.HasContiguousMemory(layoutStyleC);

            // assert
            Assert.True(continuous);
        }

        [Fact]
        public void HasContiguousMemory_WithF_ReturnTrue()
        {
            // arrange
            var layoutStyleF = Layout.NewF(new[] { 1, 2, 3 });

            // action
            var continuous = Layout.HasContiguousMemory(layoutStyleF);

            // assert
            Assert.True(continuous);
        }

        [Fact]
        public void BraodcastDim_NegativeSize_ThrowException()
        {
            // arrange
            var layout = Layout.NewC(new[] { 1, 2, 3 });

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Layout.BraodcastDim(0, -1, layout));
            Assert.Equal("Size must be positive\r\nParameter name: size", exception.Message);
        }

        [Fact]
        public void BraodcastDim_InvalidShape_ThrowException()
        {
            // arrange
            const int ShapeValue2 = 2;
            const int DimOfTheShapeValue2 = 1;
            var layout = Layout.NewC(new[] { 1, ShapeValue2, 3 });
            const int DummyValue = 9;

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Layout.BraodcastDim(DimOfTheShapeValue2, DummyValue, layout));
            Assert.Equal("Specified argument was out of the range of valid values.\r\nParameter name: Dimension 1 of shape [1,2,3] must be of size 1 to broadcast.", exception.Message);
        }

        [Fact]
        public void BraodcastDim_Valid_ReturnNewLayout()
        {
            // arrange
            const int ShapeValue1 = 1;
            const int DimOfTheShapeValue1 = 0;
            var layout = Layout.NewC(new[] { ShapeValue1, 2, 3 });

            const int NewShapeValue = 9;

            // action
            var newLayout = Layout.BraodcastDim(DimOfTheShapeValue1, NewShapeValue, layout);

            // assert
            var expectedShape = new[] { NewShapeValue, 2, 3 };
            var expectedStride = new[] { 0, 3, 1 };
            Assert.Equal(expectedShape, newLayout.Shape);
            Assert.Equal(expectedStride, newLayout.Stride);
            Assert.Equal(0, newLayout.Offset);
        }

        [Fact]
        public void BroadcastToShape_LowerBroadcastShapeRank_ThrowException()
        {
            // arrange
            var layout = Layout.NewC(new[] { 1, 2, 3 });
            var broadcastShape = new[] { 2, 3 };

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => Layout.BroadcastToShape(broadcastShape, layout));
            Assert.Equal("Cannot broadcast to shape [2,3] from shape [1,2,3] of higher rank.", exception.Message);
        }

        [Fact]
        public void BroadcastToShape_InvalidShapeValue_ThrowException()
        {
            // arrange
            const int InvalidShapeValue = 9;
            var layout = Layout.NewC(new[] { 2, 3, 4, 5 });
            var broadcastShape = new[] { 2, 3, InvalidShapeValue, 5 };

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => Layout.BroadcastToShape(broadcastShape, layout));
            Assert.Equal("Cannot broadcast shape [2,3,4,5] to shape [2,3,9,5].", exception.Message);
        }

        [Fact]
        public void BroadcastToShape_WithDifferentValidShape_ReturnBroadcastLayout()
        {
            // arrange
            var layout = Layout.NewC(new[] { 2, 3 });
            var broadcastShape = new[] { 1, 7, 2, 3 };

            // action
            var broadcastLayout = Layout.BroadcastToShape(broadcastShape, layout);

            // assert
            var expectedShape = new[] { 1, 7, 2, 3 };
            var expectedStride = new[] { 0, 0, 3, 1 };
            Assert.Equal(expectedShape, broadcastLayout.Shape);
            Assert.Equal(expectedStride, broadcastLayout.Stride);
            Assert.Equal(0, broadcastLayout.Offset);
        }

        [Fact]
        public void BroadcastToShape_WithSameShape_ReturnBroadcastLayout()
        {
            // arrange
            var layout = Layout.NewC(new[] { 1, 7, 2, 3 });
            var broadcastShape = new[] { 1, 7, 2, 3 };

            // action
            var broadcastLayout = Layout.BroadcastToShape(broadcastShape, layout);

            // assert
            var expectedShape = new[] { 1, 7, 2, 3 };
            var expectedStride = new[] { 42, 6, 3, 1 };
            Assert.Equal(expectedShape, broadcastLayout.Shape);
            Assert.Equal(expectedStride, broadcastLayout.Stride);
            Assert.Equal(0, broadcastLayout.Offset);
        }

        [Fact]
        public void TryReshape_WithoutCopyCase1_ReturnNewLayout()
        {
            // arrange
            var array = NdArray<int>.Arange(HostDevice.Instance, 0, 10, 1);

            // action
            var newShape = new[] { 10, 1 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            Assert.Equal(newShape, newLayout.Shape);
        }

        [Fact]
        public void TryReshape_WithoutCopyCase2_ReturnNewLayout()
        {
            // arrange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 10, 1 });

            // action
            var newShape = new[] { 10 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            Assert.Equal(newShape, newLayout.Shape);
        }

        [Fact]
        public void TryReshape_WithoutCopyCase3_ReturnNewLayout()
        {
            // arrange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 10, 1 });

            // action
            var newShape = new[] { 1, 10 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            Assert.Equal(newShape, newLayout.Shape);
        }

        [Fact]
        public void TryReshape_WithoutCopyCase4_ReturnNewLayout()
        {
            // arrange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 10, 1 });

            // action
            var newShape = new[] { 10, 1 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            Assert.Equal(newShape, newLayout.Shape);
        }

        [Fact]
        public void TryReshape_WithoutCopyCase5_ReturnNewLayout()
        {
            // arrange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 10 });

            // action
            var newShape = new[] { 2, 5 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            Assert.Equal(newShape, newLayout.Shape);
        }

        [Fact]
        public void TryReshape_WithoutCopyCase6_ReturnNewLayout()
        {
            // arrange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 6, 8 });

            // action
            var newShape = new[] { 2, 12, 2 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            Assert.Equal(newShape, newLayout.Shape);
        }

        [Fact]
        public void TryReshape_DifferentOrderWithoutCopy_ReturnNewShape()
        {
            // arrange
            var array = new NdArray<int>(new[] { 1, 1, 1 }, HostDevice.Instance, Order.ColumnMajor);

            // action
            var newShape = new[] { 1, 1, 1 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            Assert.Equal(newShape, newLayout.Shape);
        }

        [Fact]
        public void TryReshape_NeedCopyCase1_ReturnNull()
        {
            // arrange
            var array = new NdArray<int>(new[] { 6, 8 }, HostDevice.Instance, Order.ColumnMajor);

            // action
            var newShape = new[] { 8, 6 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            Assert.Null(newLayout);
        }

        [Fact]
        public void TryReshape_NeedCopyCase2_ReturnNull()
        {
            // arrange
            var array = new NdArray<int>(new[] { 6, 8 }, HostDevice.Instance, Order.ColumnMajor);

            // action
            var newShape = new[] { 2, 12, 2 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            Assert.Null(newLayout);
        }

        [Fact]
        public void TryReshape_WithRemainder_ReturnNewLayout()
        {
            // arrange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 2, 3, 4 });

            // action
            var newShape = new[] { 4, SpecialIdx.Remainder, 2 };
            var newLayout = Layout.TryReshape(newShape, array);

            // assert
            Assert.Equal(new[] { 4, 3, 2 }, newLayout.Shape);
        }

        [Fact]
        public void TryReshape_DifferentNumElements_ThrowException()
        {
            // arrange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 10 });

            // action
            var newShape = new[] { 3, 2, 5 };
            var exception = Assert.Throws<ArgumentException>(() => Layout.TryReshape(newShape, array));
            Assert.Equal("Cannot reshape from shape [10] (with 10 elements) to shape [3,2,5] (with 30 elements)\r\nParameter name: shape", exception.Message);
        }

        [Fact]
        public void TryReshape_WithRemainderAndInvalidNewShape_ThrowException()
        {
            // arrange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 2, 3, 4 });

            // action
            var newShape = new[] { 3, SpecialIdx.Remainder, 3 };
            var exception = Assert.Throws<ArgumentException>(() => Layout.TryReshape(newShape, array));
            Assert.Equal("Cannot reshape from [2,3,4] to [3,Remainder,3] because 24 / 9 is not an integer\r\nParameter name: shape", exception.Message);
        }

        [Fact]
        public void TryReshape_WithRemainderAndInvalidNewShape_ThrowException1()
        {
            // arrange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 2, 3, 4 });

            // action
            var newShape = new[] { 3, SpecialIdx.Remainder, 3 };
            var exception = Assert.Throws<ArgumentException>(() => Layout.TryReshape(newShape, array));
            Assert.Equal("Cannot reshape from [2,3,4] to [3,Remainder,3] because 24 / 9 is not an integer\r\nParameter name: shape", exception.Message);
        }

        [Fact]
        public void TryReshape_TwoRemainders_ThrowException()
        {
            // arrange
            var array = NdArray<int>.Ones(HostDevice.Instance, new[] { 2, 3, 4 });

            // action
            var newShape = new[] { 3, SpecialIdx.Remainder, SpecialIdx.Remainder };
            var exception = Assert.Throws<ArgumentException>(() => Layout.TryReshape(newShape, array));
            Assert.Equal("Only the size of one dimension can be determined automatically, but shape was [3,Remainder,Remainder]\r\nParameter name: shape", exception.Message);
        }

        [Fact]
        public void Equal_WithSameObject_ReturnTrue()
        {
            // arrange
            var layout = new Layout(new int[] { }, 0, new int[] { });

            // action
            var equal = layout.Equals(layout);

            // assert
            Assert.True(equal);
        }

        [Fact]
        public void Equal_WithNullObject_ReturnFalse()
        {
            // arrange
            var layout = new Layout(new int[] { }, 0, new int[] { });

            // action
            var equal = layout.Equals(null);

            // assert
            Assert.False(equal);
        }

        [Fact]
        public void Equal_WithSameLayout_ReturnTrue()
        {
            // arrange
            var layout1 = new Layout(new int[] { }, 0, new int[] { });
            var layout2 = new Layout(new int[] { }, 0, new int[] { });

            // action
            var equal = layout1.Equals(layout2);

            // assert
            Assert.True(equal);
        }

        [Fact]
        public void Equal_WithDifferentShape_ReturnFase()
        {
            // arrange
            var layout1 = new Layout(new int[] { 1 }, 0, new int[] { });
            var layout2 = new Layout(new int[] { }, 0, new int[] { });

            // action
            var equal = layout1.Equals(layout2);

            // assert
            Assert.False(equal);
        }

        [Fact]
        public void Equal_WithDifferentOffset_ReturnFase()
        {
            // arrange
            var layout1 = new Layout(new int[] { }, 1, new int[] { });
            var layout2 = new Layout(new int[] { }, 0, new int[] { });

            // action
            var equal = layout1.Equals(layout2);

            // assert
            Assert.False(equal);
        }

        [Fact]
        public void Equal_WithDifferentStride_ReturnFase()
        {
            // arrange
            var layout1 = new Layout(new int[] { }, 0, new int[] { 1 });
            var layout2 = new Layout(new int[] { }, 0, new int[] { });

            // action
            var equal = layout1.Equals(layout2);

            // assert
            Assert.False(equal);
        }

        [Fact]
        public void Transpos_OneD_ThrowException()
        {
            // arrange
            var layout = new Layout(new int[] { 10 }, 0, new int[] { 1 });

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.Transpos(layout));
            Assert.Equal("Cannot transpose non-matrix of shape [10]\r\nParameter name: source", exception.Message);
        }

        [Fact]
        public void Transpos_Scalar_ThrowException()
        {
            // arrange
            var layout = new Layout(new int[] { }, 0, new int[] { 0 });

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.Transpos(layout));
            Assert.Equal("Cannot transpose non-matrix of shape []\r\nParameter name: source", exception.Message);
        }

        [Fact]
        public void Transpos()
        {
            // arrange
            var layout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });

            // action
            var output = Layout.Transpos(layout);

            // assert
            var expectedLayout = new Layout(new[] { 1, 3, 2 }, 0, new[] { 6, 1, 3 });
            Assert.Equal(expectedLayout, output);
        }

        [Fact]
        public void CutLeft_Scalar_ThrowException()
        {
            // arrange
            var layout = new Layout(new int[] { }, 0, new int[] { 0 });

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.CutLeft(layout));
            Assert.Equal("Cannot remove dimensions from scalar\r\nParameter name: source", exception.Message);
        }

        [Fact]
        public void CutLeft()
        {
            // arrange
            var layout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });

            // action
            var output = Layout.CutLeft(layout);

            // assert
            var expectedLayout = new Layout(new[] { 2, 3 }, 0, new[] { 3, 1 });
            Assert.Equal(expectedLayout, output);
        }

        [Fact]
        public void CutRight_Scalar_ThrowException()
        {
            // arrange
            var layout = new Layout(new int[] { }, 0, new int[] { 0 });

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.CutRight(layout));
            Assert.Equal("Cannot remove dimensions from scalar\r\nParameter name: source", exception.Message);
        }

        [Fact]
        public void CutRight()
        {
            // arrange
            var layout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });

            // action
            var output = Layout.CutRight(layout);

            // assert
            var expectedLayout = new Layout(new[] { 1, 2 }, 0, new[] { 6, 3 });
            Assert.Equal(expectedLayout, output);
        }

        [Fact]
        public void IsBroadcasted_WithBroadCastedLayout_ReturnTrue()
        {
            // arrange
            var layout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 3, 0, 1 });

            // action
            var output = Layout.IsBroadcasted(layout);

            // assert
            Assert.True(output);
        }

        [Fact]
        public void IsBroadcasted_WithoutBroadCastedLayout_ReturnFalse()
        {
            // arrange
            var layout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });

            // action
            var output = Layout.IsBroadcasted(layout);

            // assert
            Assert.False(output);
        }

        [Fact]
        public void ReverseAxis()
        {
            // arrange
            var layout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });

            // action
            var output = Layout.ReverseAxis(1, layout);

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 3, new[] { 6, -3, 1 });
            Assert.Equal(expectedLayout, output);
        }

        [Fact]
        public void AllIndex()
        {
            // arrange
            var layout = new Layout(new[] { 2, 1, 3 }, 0, new[] { 3, 3, 1 });

            // action
            var output = Layout.AllIndex(layout);

            // assert
            Assert.Equal(new[] { 0, 0, 0 }, output[0]);
            Assert.Equal(new[] { 0, 0, 1 }, output[1]);
            Assert.Equal(new[] { 0, 0, 2 }, output[2]);
            Assert.Equal(new[] { 1, 0, 0 }, output[3]);
            Assert.Equal(new[] { 1, 0, 1 }, output[4]);
            Assert.Equal(new[] { 1, 0, 2 }, output[5]);
        }

        [Fact]
        public void AllIndexOfShape()
        {
            // arrange
            var shape = new int[] { 2, 1, 3 };

            // action
            var output = Layout.AllIndexOfShape(shape).ToArray();

            // assert
            Assert.Equal(new[] { 0, 0, 0 }, output[0]);
            Assert.Equal(new[] { 0, 0, 1 }, output[1]);
            Assert.Equal(new[] { 0, 0, 2 }, output[2]);
            Assert.Equal(new[] { 1, 0, 0 }, output[3]);
            Assert.Equal(new[] { 1, 0, 1 }, output[4]);
            Assert.Equal(new[] { 1, 0, 2 }, output[5]);
        }

        [Fact]
        public void LinearToIndex()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));

            // action
            var output = Layout.LinearToIndex(layout, 73);

            // assert
            Assert.Equal(new[] { 0, 1, 0, 2, 3 }, output);
        }

        [Fact]
        public void Check_InvalidShapeAndStrideLenth_ThrowException()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, new int[] { });

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.Check(layout));
            Assert.Equal("shape [1,2,3,4,5] and stride [] must have same number of entries", exception.Message);
        }

        [Fact]
        public void Check_NegativeShape_ThrowException()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));
            shape[2] = -1;

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.Check(layout));
            Assert.Equal("shape [1,2,-1,4,5] cannot have negative entires", exception.Message);
        }

        [Fact]
        public void DiagAxis_SameAxes_ThrowException()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));
            shape[2] = -1;

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.DiagAxis(1, 1, layout));
            Assert.Equal("Axes to use for diagonal must be different.\r\nParameter name: ax1", exception.Message);
        }

        [Fact]
        public void DiagAxis_DifferntShapes_ThrowException()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.DiagAxis(1, 2, layout));
            Assert.Equal("Array must have same dimensions along axis 1 and 2 to extract diagonal but it has shape [1,2,3,4,5]\r\nParameter name: layout", exception.Message);
        }

        [Fact]
        public void BroadcastToSameInDimsMany_BigDim_ThrowException()
        {
            // arrange
            var dims = new int[] { 2, 8, 1 };
            var array = NdArray<int>.Zeros(HostDevice.Instance, new[] { 1, 2, 3 });
            var layouts = new Layout[] { array.Layout, array.Layout, array.Layout };

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => Layout.BroadcastToSameInDimsMany(dims, layouts));
            Assert.Equal("Cannot broadcast shapes [1,2,3],[1,2,3],[1,2,3] to same size in non - existant dimension 8.", exception.Message);
        }

        [Fact]
        public void BroadcastToSameInDimsMany_NotCompatible_ThrowException()
        {
            // arrange
            var dims = new int[] { 0 };
            var array1 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 1, 3, 4 });
            var array2 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 3, 3, 4 });
            var array3 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 3, 4 });
            var layouts = new Layout[] { array1.Layout, array2.Layout, array3.Layout };

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => Layout.BroadcastToSameInDimsMany(dims, layouts));
            Assert.Equal("Cannot broadcast shapes [1,3,4],[3,3,4],[2,3,4] to same size in dimension 0 because they do not agree in the target size.", exception.Message);
        }

        [Fact]
        public void BroadcastToSameInDimsMany_UnableToBroadCast_ThrowException()
        {
            // arrange
            var dims = new int[] { 0 };
            var array1 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 3, 4 });
            var array2 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 3, 3, 4 });
            var layouts = new Layout[] { array2.Layout, array1.Layout, array2.Layout };

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => Layout.BroadcastToSameInDimsMany(dims, layouts));
            Assert.Equal("Non-broadcast dimension 0 of shapes [3,3,4],[2,3,4],[3,3,4] does not agree.", exception.Message);
        }

        [Fact]
        public void InsertAxis_NegativeAxis_ThrowException()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.InsertAxis(-1, layout));
            Assert.Equal("Axis -1 out of range for NdArray with shape [1,2,3,4,5]", exception.Message);
        }

        [Fact]
        public void InsertAxis_BigAxis_ThrowException()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.InsertAxis(100, layout));
            Assert.Equal("Axis 100 out of range for NdArray with shape [1,2,3,4,5]", exception.Message);
        }

        [Fact]
        public void PermuteAxes()
        {
            // arrange
            var shape = new int[] { 2, 3, 4 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));

            // action
            var output = Layout.PermuteAxes(new[] { 2, 1, 0 }, layout);

            // assert
            var expectedShape = new int[] { 4, 3, 2 };
            Assert.Equal(expectedShape, output.Shape);
        }

        [Fact]
        public void PermuteAxes_InvalidPermut_ThrowException()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.PermuteAxes(new[] { 2, 3, 4 }, layout));
            Assert.Equal("Permutation [2,3,4] must have same rank as shape [1,2,3,4,5].\r\nParameter name: permut", exception.Message);
        }

        [Fact]
        public void View_ThrowException()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.View(new[] { new InvalidRangeTypeForTest() }, layout));
            Assert.Equal("Slice [NdArrayNet.NdArrayUnitTest.LayoutTests+InvalidRangeTypeForTest] is incompatible with shape [1,2,3,4,5].\r\nParameter name: ranges", exception.Message);
        }

        [Fact]
        public void BroadcastDim_NegativeSize_ThrowException()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Layout.BroadcastDim(0, -1, layout));
            Assert.Equal("Size must be positive\r\nParameter name: size", exception.Message);
        }

        [Fact]
        public void BroadcastDim_UnableToBroadCast_ThrowException1()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));

            // action
            var exception = Assert.Throws<ArgumentException>(() => Layout.BroadcastDim(1, 2, layout));
            Assert.Equal("Dimension 1 of shape [1,2,3,4,5] must be of size 1 to broadcast.\r\nParameter name: dim", exception.Message);
        }

        [Fact]
        public void CheckAxis_NegativeAxis_ThrowException()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Layout.CheckAxis(layout, -1));
            Assert.Equal("Specified argument was out of the range of valid values.\r\nParameter name: axis -1 out of range for NdArray with shape [1,2,3,4,5]", exception.Message);
        }

        [Fact]
        public void CheckAxis_TooBigAxis_ThrowException()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Layout.CheckAxis(layout, 100));
            Assert.Equal("Specified argument was out of the range of valid values.\r\nParameter name: axis 100 out of range for NdArray with shape [1,2,3,4,5]", exception.Message);
        }

        [Fact]
        public void CheckElementRange_NegativeIndex_ThrowException()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Layout.CheckElementRange(false, 10, -1, new[] { RangeFactory.Elem(0) }, layout.Shape));
            Assert.Equal("Specified argument was out of the range of valid values.\r\nParameter name: Index -1 out of range in slice [0] for shape [1,2,3,4,5].", exception.Message);
        }

        [Fact]
        public void CheckElementRange_TooBigIndex_ThrowException()
        {
            // arrange
            var shape = new int[] { 1, 2, 3, 4, 5 };
            var layout = new Layout(shape, 0, Layout.CStride(shape));

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Layout.CheckElementRange(false, 10, 100, new[] { RangeFactory.Elem(0) }, layout.Shape));
            Assert.Equal("Specified argument was out of the range of valid values.\r\nParameter name: Index 100 out of range in slice [0] for shape [1,2,3,4,5].", exception.Message);
        }

        private class InvalidRangeTypeForTest : RangeBase
        {
            public InvalidRangeTypeForTest() : base((RangeType)9)
            {
            }
        }
    }
}