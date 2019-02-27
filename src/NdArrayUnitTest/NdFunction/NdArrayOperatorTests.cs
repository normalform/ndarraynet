// <copyright file="NdArrayOperatorTests.cs" company="NdArrayNet">
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
    using NdArray.NdFunction;
    using NdArrayNet;
    using System;
    using Xunit;

    public class NdArrayOperatorTests
    {
        [Fact]
        public void DiagAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<int>.Zeros(configManager,new[] { 4, 3, 3, 5 });

            // action
            var diag = NdArrayOperator<int>.DiagAxis(1, 2, input);

            // assert
            Assert.Equal(new[] { 4, 3, 5 }, diag.Shape);
        }

        [Fact]
        public void Diag()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<int>.Arange(configManager,0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var diag = NdArrayOperator<int>.Diag(input);

            // assert
            Assert.Equal(0, diag[0].Value);
            Assert.Equal(4, diag[1].Value);
            Assert.Equal(8, diag[2].Value);
        }

        [Fact]
        public void Diag_OneDimensionalArray_ThrowException()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var input = NdArray<int>.Arange(configManager,0, 9, 1);

            // action
            var exception = Assert.Throws<ArgumentException>(() => NdArrayOperator<int>.Diag(input));
            Assert.Contains("Need at least a two dimensional array for diagonal but got shape [9].", exception.Message);
        }

        [Fact]
        public void Concat_EmptyInput_ThrowException()
        {
            // arrange
            const int DummyAxis = 1;
            var emptyInputs = new NdArray<int>[] { };

            // action
            var exception = Assert.Throws<ArgumentException>(() => NdArrayOperator<int>.Concat(DummyAxis, emptyInputs));
            Assert.Contains("Cannot concatenate empty sequence of NdArray.", exception.Message);
        }

        [Fact]
        public void Concat_AxisOutOfrangeCase1_ThrowException()
        {
            // arrange
            const int ConcatAxis = 3;
            var inputs = new NdArray<int>[] { NdArray<int>.Zeros(ConfigManager.Instance, new[] { 1, 1 }) };

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => NdArrayOperator<int>.Concat(ConcatAxis, inputs));
            Assert.Contains("Concatenation axis 3 is out of range for shape [1,1].", exception.Message);
        }

        [Fact]
        public void Concat_AxisOutOfrangeCase2_ThrowException()
        {
            // arrange
            const int ConcatAxis = -1;
            var inputs = new NdArray<int>[] { NdArray<int>.Zeros(ConfigManager.Instance, new[] { 1, 1 }) };

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => NdArrayOperator<int>.Concat(ConcatAxis, inputs));
            Assert.Contains("Concatenation axis -1 is out of range for shape [1,1].", exception.Message);
        }

        [Fact]
        public void Concat_DifferntShapes_ThrowException()
        {
            // arrange
            const int ConcatAxis = 1;
            var inputs = new NdArray<int>[]
            {
                NdArray<int>.Zeros(ConfigManager.Instance, new[] { 1, 1 }),
                NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 1 })
            };

            // action
            var exception = Assert.Throws<ArgumentException>(() => NdArrayOperator<int>.Concat(ConcatAxis, inputs));
            Assert.Contains("Concatentation element with index 1 with shape[2,1] must be equal to shape [1,1] of the first element, except in the concatenation axis 1", exception.Message);
        }

        [Fact]
        public void Concat()
        {
            // arrange
            const int ConcatAxis = 1;
            var inputs = new NdArray<int>[]
            {
                NdArray<int>.Zeros(ConfigManager.Instance, new[] { 4, 28 }),
                NdArray<int>.Zeros(ConfigManager.Instance, new[] { 4, 15 }),
                NdArray<int>.Zeros(ConfigManager.Instance, new[] { 4, 10 })
            };

            // action
            var concat = NdArrayOperator<int>.Concat(ConcatAxis, inputs);

            // assert
            Assert.Equal(new[] { 4, 53 }, concat.Shape);
        }

        [Fact]
        public void Copy()
        {
            // arrange
            var input = NdArray<int>.Arange(ConfigManager.Instance, 0, 10, 1).Reshape(new[] { 2, 5 });

            // action
            var copy = NdArrayOperator<int>.Copy(input);

            // assert
            Assert.Equal(new[] { 2, 5 }, copy.Shape);
            Assert.Equal(new[] { 5, 1 }, copy.Layout.Stride);
        }

        [Fact]
        public void Copy_ColumnMajor()
        {
            // arrange
            var input = NdArray<int>.Arange(ConfigManager.Instance, 0, 10, 1).Reshape(new[] { 2, 5 });

            // action
            var copy = NdArrayOperator<int>.Copy(input, Order.ColumnMajor);

            // assert
            Assert.Equal(new[] { 2, 5 }, copy.Shape);
            Assert.Equal(new[] { 1, 2 }, copy.Layout.Stride);
        }

        [Fact]
        public void DiagMatAxis_SameAxes()
        {
            // arrange
            var axis1 = 1;
            var axis2 = 1;
            var input = NdArray<int>.Arange(ConfigManager.Instance, 0, 10, 1).Reshape(new[] { 2, 5 });

            // action
            var exception = Assert.Throws<ArgumentException>(() => NdArrayOperator<int>.DiagMatAxis(axis1, axis2, input));
            Assert.Contains("Axes [axis1=1, axis2=1] to use for diagonal must be different", exception.Message);
        }

        [Fact]
        public void DiagMatAxis_InvalidAxis()
        {
            // arrange
            var axis1 = 1;
            var axis2 = 3;
            var input = NdArray<int>.Arange(ConfigManager.Instance, 0, 10, 1).Reshape(new[] { 2, 5 });

            // action
            var exception = Assert.Throws<ArgumentException>(() => NdArrayOperator<int>.DiagMatAxis(axis1, axis2, input));
            Assert.Contains("Cannot insert axis at position 3 into array of shape [2,5].", exception.Message);
        }

        [Fact]
        public void DiagMatAxis()
        {
            // arrange
            var axis1 = 0;
            var axis2 = 1;
            var input = NdArray<int>.Arange(ConfigManager.Instance, 0, 12, 1).Reshape(new[] { 4, 3 });

            // action
            var diagMat = NdArrayOperator<int>.DiagMatAxis(axis1, axis2, input);

            // assert
            Assert.Equal(new[] { 4, 4, 3 }, diagMat.Shape);
        }

        [Fact]
        public void DiagMat_Scalar_ThrowException()
        {
            // arrange
            const int DummyValue = 3;
            var input = NdArray<int>.Scalar(ConfigManager.Instance, DummyValue);

            // action
            var exception = Assert.Throws<ArgumentException>(() => NdArrayOperator<int>.DiagMat(input));
            Assert.Contains("Need at leat a one-dimensional array to create a diagonal matrix", exception.Message);
        }

        [Fact]
        public void DiagMat()
        {
            // arrange
            var input = NdArray<int>.Arange(ConfigManager.Instance, 0, 3, 1);

            // action
            var diagMat = NdArrayOperator<int>.DiagMat(input);

            // assert
            Assert.Equal(new[] { 3, 3 }, diagMat.Shape);
        }

        [Fact]
        public void DiffAxis()
        {
            // arrange
            var input = NdArray<int>.Arange(ConfigManager.Instance, 0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var result = NdArrayOperator<int>.DiffAxis(1, input);

            // assert
            Assert.Equal(new[] { 3, 2 }, result.Shape);
        }

        [Fact]
        public void Diff_Scalar_ThrowException()
        {
            // arrange
            const int DummyValue = 3;
            var input = NdArray<int>.Scalar(ConfigManager.Instance, DummyValue);

            // action
            var exception = Assert.Throws<ArgumentException>(() => NdArrayOperator<int>.Diff(input));
            Assert.Contains("Need at least a vector to calculate diff.", exception.Message);
        }

        [Fact]
        public void Diff()
        {
            // arrange
            var input = NdArray<int>.Arange(ConfigManager.Instance, 0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var result = NdArrayOperator<int>.Diff(input);

            // assert
            Assert.Equal(new[] { 3, 2 }, result.Shape);
        }

        [Fact]
        public void Replicate()
        {
            // arrange
            var input = NdArray<int>.Arange(ConfigManager.Instance, 0, 2 * 3, 1).Reshape(new[] { 2, 3 });

            // action
            var result = NdArrayOperator<int>.Replicate(0, 10, input);

            // assert
            Assert.Equal(new[] { 20, 3 }, result.Shape);
        }

        [Fact]
        public void Replicate_NegativeRepeats_ThrowException()
        {
            // arrange
            var input = NdArray<int>.Arange(ConfigManager.Instance, 0, 2 * 3, 1).Reshape(new[] { 2, 3 });

            // action
            var exception = Assert.Throws<ArgumentException>(() => NdArrayOperator<int>.Replicate(0, -10, input));
            Assert.Contains("Number of repetitions cannot be negative.", exception.Message);
        }

        [Fact]
        public void Transpos()
        {
            // arrange
            var input = NdArray<int>.Arange(ConfigManager.Instance, 0, 4, 1).Reshape(new[] { 2, 2 });

            // action
            var result = NdArrayOperator<int>.Transpos(input);

            // assert
            Assert.Equal(0, result[new[] { 0, 0 }]);
            Assert.Equal(1, result[new[] { 1, 0 }]);
            Assert.Equal(2, result[new[] { 0, 1 }]);
            Assert.Equal(3, result[new[] { 1, 1 }]);
        }
    }
}