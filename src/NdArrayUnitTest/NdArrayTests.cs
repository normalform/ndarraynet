// <copyright file="NdArrayTests.cs" company="NdArrayNet">
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
    using Moq;
    using NdArray.NdFunction.Comparison;
    using NdArrayNet;
    using System;
    using System.Linq;
    using Xunit;
    using System.Collections.Generic;

    public class NdArrayTests
    {
        [Fact]
        public void NdArray_RowMajor()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = new NdArray<int>(configManager, new int[] { 1, 2, 3, 4, 5 }, Order.RowMajor);

            // assert
            var expected = new[] { 120, 60, 20, 5, 1 };
            Assert.Equal(expected, array.Layout.Stride);
        }

        [Fact]
        public void NdArray_ColumnMajor()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = new NdArray<int>(configManager, new int[] { 1, 2, 3, 4, 5 }, Order.ColumnMajor);

            // assert
            var expected = new[] { 1, 1, 2, 6, 24 };
            Assert.Equal(expected, array.Layout.Stride);
        }

        [Fact]
        public void NumElements()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = new NdArray<int>(configManager, new int[] { 1, 2, 3, 4, 5 }, Order.ColumnMajor);

            // assert
            var expected = 120;
            Assert.Equal(expected, array.NumElements);
        }

        [Fact]
        public void NumDimensions()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = new NdArray<int>(configManager, new int[] { 1, 2, 3, 4, 5 }, Order.ColumnMajor);

            // assert
            var expected = 5;
            Assert.Equal(expected, array.NumDimensions);
        }

        [Fact]
        public void FillIncrementing_WithScalar_ThrowException()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var array = NdArray<int>.Ones(configManager, new int[] { });

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => array.FillIncrementing(0, 1));
            Assert.Equal("FillIncrementing requires a vector.", exception.Message);
        }

        [Fact]
        public void GetValue_ScalarArray_ReturnValue()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var scalarArray = NdArray<int>.Ones(configManager, new int[] { });

            // action
            var value = scalarArray.Value;

            // assert
            Assert.Equal(1, value);
        }

        [Fact]
        public void GetValue_Vector_ThrowException()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var scalarArray = NdArray<int>.Ones(configManager,new int[] { 1 });

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => scalarArray.Value);
            Assert.Equal("This operation requires a scalar (0-dimensional) NdArray, but its shape is [1]", exception.Message);
        }

        [Fact]
        public void SetValue_ScalarArray_ReturnValue()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var scalarArray = NdArray<int>.Ones(configManager,new int[] { });

            // action
            scalarArray.Value = 100;

            // assert
            Assert.Equal(100, scalarArray.Value);
        }

        [Fact]
        public void SetValue_Vector_ThrowException()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var scalarArray = NdArray<int>.Ones(configManager,new int[] { 1 });

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => scalarArray.Value = 100);
            Assert.Equal("This operation requires a scalar (0-dimensional) NdArray, but its shape is [1]", exception.Message);
        }

        [Fact]
        public void Get_SingleIndex()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var array = NdArray<int>.Arange(configManager,0, 3, 1);

            // action
            var scalarArray = array[2];

            // assert
            Assert.Equal(2, scalarArray.Value);
        }

        [Fact]
        public void Set_SingleIndex()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var array = NdArray<int>.Arange(configManager,0, 3, 1);

            // action
            array[2] = NdArray<int>.Ones(configManager,new int[] { });
            var scalarArray = array[2];

            // assert
            Assert.Equal(1, scalarArray.Value);
        }

        [Fact]
        public void AssertSameShape_WithSameShape_Pass()
        {
            // arrange
            // arrange
            var configManager = ConfigManager.Instance;
            var arrayA = NdArray<int>.Arange(configManager,1, 3, 1);
            var arrayB = NdArray<int>.Arange(configManager,1, 3, 1);

            // action
            NdArray<int>.AssertSameShape(arrayA, arrayB);
        }

        [Fact]
        public void AssertSameShape_DifferentShape_ThrowException()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var arrayA = NdArray<int>.Arange(configManager,0, 3, 1);
            var arrayB = NdArray<int>.Arange(configManager,1, 3, 1);

            // action
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => NdArray<int>.AssertSameShape(arrayA, arrayB));
            Assert.Contains("NdArrays of shapes [3] and [2] were expected to have same shape", exception.Message);
        }

        [Fact]
        public void AssertSameStorage()
        {
            // arrange
            // arrange
            var configManager = ConfigManager.Instance;
            var arrays = new[]
            {
                NdArray<int>.Arange(configManager,1, 3, 1),
                NdArray<int>.Arange(configManager,1, 3, 1)
            };

            // action
            NdArray<int>.AssertSameStorage(arrays);
        }

        [Fact]
        public void AssertScalar_WithVector_ThrowException()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var array = NdArray<int>.Arange(configManager,0, 3, 1);

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => array.AssertScalar());
            Assert.Equal("This operation requires a scalar (0-dimensional) NdArray, but its shape is [3]", exception.Message);
        }

        [Fact]
        public void AssertScalar_WithScalar_Pass()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var emptyShapeForScalar = new int[] { };
            var array = NdArray<int>.Zeros(configManager,emptyShapeForScalar);

            // action
            array.AssertScalar();
        }

        [Fact]
        public void SetAndGetWithArrayAndPos()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var array = NdArray<int>.Ones(configManager,new[] { 2, 3, 4 });

            // action
            NdArray<int>.Set(array, new[] { 0, 1, 2 }, 999);
            var value = NdArray<int>.Get(array, new[] { 0, 1, 2 });

            // assert
            Assert.Equal(999, value);
        }

        [Fact]
        public void GetView_WithRangeSpecifiers()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var array = NdArray<int>.Ones(configManager,new[] { 2, 3 });

            NdArray<int>.Set(array, new[] { 0, 0 }, 1);
            NdArray<int>.Set(array, new[] { 0, 1 }, 2);
            NdArray<int>.Set(array, new[] { 0, 2 }, 3);
            NdArray<int>.Set(array, new[] { 1, 0 }, 4);
            NdArray<int>.Set(array, new[] { 1, 1 }, 5);
            NdArray<int>.Set(array, new[] { 1, 2 }, 6);

            // action
            var scalarView = array[new[] { RangeFactory.Elem(0), RangeFactory.Elem(1) }];
            var arrayView1 = array[new[] { RangeFactory.Elem(0), RangeFactory.All }];
            var arrayView2 = array[new[] { RangeFactory.Elem(1), RangeFactory.Range(0, 1) }];
            var arrayView3 = array[new[] { RangeFactory.Range(1, 1), RangeFactory.Range(0, 1) }];

            // assert
            Assert.Equal(2, scalarView.Value);
            Assert.Equal(1, arrayView1[0].Value);
            Assert.Equal(2, arrayView1[1].Value);
            Assert.Equal(3, arrayView1[2].Value);
            Assert.Equal(4, arrayView2[0].Value);
            Assert.Equal(5, arrayView2[1].Value);
            Assert.Equal(4, arrayView3[new[] { 0, 0 }]);
            Assert.Equal(5, arrayView3[new[] { 0, 1 }]);
        }

        [Fact]
        public void SetView_WithRangeSpecifiers()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var array = NdArray<int>.Ones(configManager,new[] { 2, 3 });

            // action
            array[new[] { RangeFactory.Elem(0), RangeFactory.All }] = NdArray<int>.Arange(configManager,7, 10, 1);

            // assert
            Assert.Equal(7, NdArray<int>.Get(array, new[] { 0, 0 }));
            Assert.Equal(8, NdArray<int>.Get(array, new[] { 0, 1 }));
            Assert.Equal(9, NdArray<int>.Get(array, new[] { 0, 2 }));
            Assert.Equal(1, NdArray<int>.Get(array, new[] { 1, 0 }));
            Assert.Equal(1, NdArray<int>.Get(array, new[] { 1, 1 }));
            Assert.Equal(1, NdArray<int>.Get(array, new[] { 1, 2 }));
        }

        [Fact]
        public void ScalarString()
        {
            // arrange
            var configInt = ConfigManager.Instance;
            var configLong = ConfigManager.Instance;
            var configFloat = ConfigManager.Instance;
            var configDouble = ConfigManager.Instance;
            var configBool = ConfigManager.Instance;
            var configByte = ConfigManager.Instance;
            var configUnkown = ConfigManager.Instance;

            // action
            var strInt = NdArray<int>.ScalarString(NdArray<int>.Ones(configInt,new int[] { }));
            var strLong = NdArray<long>.ScalarString(NdArray<long>.Ones(configLong, new int[] { }));
            var strFloat = NdArray<float>.ScalarString(NdArray<float>.Ones(configFloat, new int[] { }));
            var strDouble = NdArray<double>.ScalarString(NdArray<double>.Ones(configDouble, new int[] { }));
            var strBool = NdArray<bool>.ScalarString(NdArray<bool>.Ones(configBool, new int[] { }));
            var strByte = NdArray<byte>.ScalarString(NdArray<byte>.Ones(configByte, new int[] { }));
            var strUnkown = NdArray<UnKownValueTypeForTestOnly>.ScalarString(NdArray<UnKownValueTypeForTestOnly>.Zeros(configUnkown, new int[] { }));

            // assert
            Assert.Equal("   1", strInt);
            Assert.Equal("   1", strLong);
            Assert.Equal("   1.0000", strFloat);
            Assert.Equal("   1.0000", strDouble);
            Assert.Equal("true", strBool);
            Assert.Equal("  1", strByte);
            Assert.Equal("UnKownType", strUnkown);
        }

        [Fact]
        public void PrettyDim()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var array = NdArray<int>.Ones(configManager,new[] { 1, 2, 3 });

            // action
            var str = NdArray<int>.PrettyDim(10, " ", array);

            // assert
            Assert.Equal("[[[   1    1    1]\n  [   1    1    1]]]", str);
        }

        [Fact]
        public void ArrayToString()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var array = NdArray<int>.Ones(configManager,new[] { 1, 2, 3 });

            // action
            var str = array.ToString();

            // assert
            Assert.Equal("[[[   1    1    1]\n  [   1    1    1]]]", str);
        }

        [Fact]
        public void TryReshapeView_WitoutCopy_ReturnReshapedArray()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var array = NdArray<int>.Ones(configManager,new[] { 10 });

            // action
            var newView = array.TryReshapeView(new[] { 1, 10 });

            // assert
            Assert.Equal(new[] { 1, 10 }, newView.Shape);
        }

        [Fact]
        public void TryReshapeView_NeedCopy_ReturnNull()
        {
            // arrange
            var array = new NdArray<int>(ConfigManager.Instance, new[] { 2, 5 }, Order.ColumnMajor);

            // action
            var newView = array.TryReshapeView(new[] { 2, 5 });

            // assert
            Assert.Null(newView);
        }

        [Fact]
        public void ReshapeView_WitoutCopy_ReturnReshapedArray()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var array = NdArray<int>.Ones(configManager,new[] { 10 });

            // action
            var newView = array.ReshapeView(new[] { 1, 10 });

            // assert
            Assert.Equal(new[] { 1, 10 }, newView.Shape);
        }

        [Fact]
        public void ReshapeView_NeedCopy_ReturnNull()
        {
            // arrange
            var array = new NdArray<int>(ConfigManager.Instance, new[] { 2, 5 }, Order.ColumnMajor);

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => array.ReshapeView(new[] { 2, 5 }));
            Assert.Equal("Cannot reshape NdArray of shape [2,5] and strides [1,2] without copying.", exception.Message);
        }

        [Fact]
        public void Reshape_WithoutCopy()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var array = NdArray<int>.Arange(configManager,0, 2 * 3 * 4 * 5, 1);

            // action
            var newView = array.Reshape(new[] { 2, 3, 4, 5 });

            // assert
            Assert.Equal(new[] { 2, 3, 4, 5 }, newView.Shape);
        }

        [Fact]
        public void Reshape_NeedCopy()
        {
            // arrange
            var array = new NdArray<int>(ConfigManager.Instance, new[] { 2, 5 }, Order.ColumnMajor);

            // action
            var newView = array.Reshape(new[] { 5, 2 });

            // assert
            Assert.Equal(new[] { 5, 2 }, newView.Shape);
        }

        [Fact]
        public void Broadcasting_VectorsWithSameShape()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var arrayA = NdArray<int>.Arange(configManager,0, 24, 1).Reshape(new[] { 2, 3, 4 });
            var arrayB = NdArray<int>.Arange(configManager,0, 24, 1).Reshape(new[] { 2, 3, 4 });

            // action
            var result = arrayA * arrayB;

            // assert
            Assert.Equal(new[] { 2, 3, 4 }, result.Shape);
        }

        [Fact]
        public void Broadcasting_VectorsWithDiffernetShape()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var arrayA = NdArray<int>.Arange(configManager,0, 24, 1).Reshape(new[] { 2, 3, 4 });
            var arrayB = NdArray<int>.Arange(configManager,0, 12, 1).Reshape(new[] { 3, 4 });

            // action
            var result = arrayA * arrayB;

            // assert
            Assert.Equal(new[] { 2, 3, 4 }, result.Shape);
        }

        [Fact]
        public void Broadcasting_VectorsWithScalar()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var arrayA = NdArray<int>.Arange(configManager,0, 24, 1).Reshape(new[] { 2, 3, 4 });
            var arrayB = NdArray<int>.Ones(configManager,new int[] { });

            // action
            var result = arrayA * arrayB;

            // assert
            Assert.Equal(new[] { 2, 3, 4 }, result.Shape);
        }

        [Fact]
        public void Broadcasting_ScalarWithScalar()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var arrayA = NdArray<int>.Ones(configManager,new int[] { });
            var arrayB = NdArray<int>.Ones(configManager,new int[] { });

            // action
            var result = arrayA * arrayB;

            // assert
            Assert.Equal(new int[] { }, result.Shape);
        }

        [Fact]
        public void Broadcasting_ScalarWithVectors()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var arrayA = NdArray<int>.Arange(configManager,0, 24, 1).Reshape(new[] { 2, 3, 4 });
            var arrayB = NdArray<int>.Ones(configManager,new int[] { });

            // action
            var result = arrayB * arrayA;

            // assert
            Assert.Equal(new[] { 2, 3, 4 }, result.Shape);
        }

        [Fact]
        public void Scalar()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            const int Value = 9;

            // action
            var array = NdArray<int>.Scalar(configManager,Value);

            // asssert
            Assert.Equal(Value, array.Value);
        }

        [Fact]
        public void ScalarLike()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var referenceArray = NdArray<int>.Arange(configManager,0, 24, 1).Reshape(new[] { 2, 3, 4 });
            const int Value = 9;

            // action
            var array = NdArray<int>.ScalarLike(referenceArray, Value);

            // asssert
            Assert.Equal(Value, array.Value);
        }

        [Fact]
        public void Counting()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = NdArray<int>.Counting(configManager,10);

            // assert
            var expectedLayout = new Layout(new[] { 10 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
        }

        [Fact]
        public void Empty()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = NdArray<int>.Empty(configManager,3);

            // assert
            var expectedLayout = new Layout(new[] { 0, 0, 0 }, 0, new[] { 0, 0, 1 });
            Assert.Equal(expectedLayout, array.Layout);
        }

        [Fact]
        public void Falses()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = NdArray<bool>.Falses(configManager,new[] { 1, 2, 3 });

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
            Assert.Equal(expectedLayout, array.Layout);
        }

        [Fact]
        public void Filled()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = NdArray<int>.Filled(configManager,new[] { 1, 2, 3 }, 55);

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
            Assert.Equal(expectedLayout, array.Layout);
        }

        [Fact]
        public void Identity_3by3()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = NdArray<int>.Identity(configManager,3);

            // assert
            var expectedLayout = new Layout(new[] { 3, 3 }, 0, new[] { 3, 1 });
            Assert.Equal(expectedLayout, array.Layout);
        }

        [Fact]
        public void Ones()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = NdArray<int>.Ones(configManager,new[] { 2, 2 });

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            Assert.Equal(expectedLayout, array.Layout);
        }

        [Fact]
        public void OnesLike()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var zeros = NdArray<int>.Zeros(configManager,new[] { 2, 2 });

            // action
            var array = NdArray<int>.OnesLike(zeros);

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            Assert.Equal(expectedLayout, array.Layout);
        }

        [Fact]
        public void Linspace()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = NdArray<int>.Linspace(configManager,0, 5, 5);

            // assert
            var expectedLayout = new Layout(new[] { 5 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
        }

        [Fact]
        public void Trues()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = NdArray<bool>.Trues(configManager,new[] { 1, 2, 3 });

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
            Assert.Equal(expectedLayout, array.Layout);
        }

        [Fact]
        public void Zeros()
        {
            // arrange
            var configManager = ConfigManager.Instance;

            // action
            var array = NdArray<int>.Zeros(configManager,new[] { 2, 2 });

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            Assert.Equal(expectedLayout, array.Layout);
        }

        [Fact]
        public void ZerosLike()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var ones = NdArray<int>.Ones(configManager,new[] { 2, 2 });

            // action
            var array = NdArray<int>.ZerosLike(ones);

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            Assert.Equal(expectedLayout, array.Layout);
        }

        [Fact]
        public void Abs()
        {
            // arrange
            var srcArray = NdArray<int>.Linspace(ConfigManager.Instance, -4, 4, 8);

            // action
            var newArray = NdArray<int>.Abs(srcArray);

            // assert
            Assert.True(newArray[0].Value > 0);
        }

        [Fact]
        public void Acos()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(ConfigManager.Instance, -1, 1, 3);

            // action
            var newArray = NdArray<double>.Acos(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - Math.PI) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - (Math.PI / 2.0)) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 0.0) < Epsilon);
        }

        [Fact]
        public void Asin()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(ConfigManager.Instance, -1, 1, 3);

            // action
            var newArray = NdArray<double>.Asin(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - (-Math.PI / 2.0)) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - (Math.PI / 2.0)) < Epsilon);
        }

        [Fact]
        public void Atan()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(ConfigManager.Instance, -1, 1, 3);

            // action
            var newArray = NdArray<double>.Atan(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - (-Math.PI / 4.0)) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - (Math.PI / 4.0)) < Epsilon);
        }

        [Fact]
        public void Ceiling()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(ConfigManager.Instance, -1, 1, 6);

            // action
            var newArray = NdArray<double>.Ceiling(srcArray);

            // assert
            Assert.Equal(-1.0, newArray[0].Value);
            Assert.Equal(0.0, newArray[1].Value);
            Assert.Equal(1.0, newArray[4].Value);
        }

        [Fact]
        public void Cos()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = NdArray<double>.Cos(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 0.0) < Epsilon);
        }

        [Fact]
        public void Cosh()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = NdArray<double>.Cosh(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - 2.5091784786580567) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 2.5091784786580567) < Epsilon);
        }

        [Fact]
        public void Exp()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 4 });
            srcArray[0].Value = -1.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = 1.0;
            srcArray[3].Value = 10.0;

            // action
            var newArray = NdArray<double>.Exp(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - 0.36787944117144233) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 2.718281828459045) < Epsilon);
            Assert.True(Math.Abs(newArray[3].Value - 22026.465794806718) < Epsilon);
        }

        [Fact]
        public void Floor()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(ConfigManager.Instance, -1, 1, 6);

            // action
            var newArray = NdArray<double>.Floor(srcArray);

            // assert
            Assert.Equal(-1.0, newArray[0].Value);
            Assert.Equal(-1.0, newArray[1].Value);
            Assert.Equal(-1.0, newArray[2].Value);
            Assert.Equal(0.0, newArray[3].Value);
            Assert.Equal(0.0, newArray[4].Value);
            Assert.Equal(1.0, newArray[5].Value);
        }

        [Fact]
        public void Log()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = 1.00;
            srcArray[1].Value = Math.E;
            srcArray[2].Value = 4.0;

            // action
            var newArray = NdArray<double>.Log(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 1.3862943611198906) < Epsilon);
        }

        [Fact]
        public void Log10()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = 1.00;
            srcArray[1].Value = 10.0;
            srcArray[2].Value = 100.0;

            // action
            var newArray = NdArray<double>.Log10(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 1.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 2.0) < Epsilon);
        }

        [Fact]
        public void Maximum()
        {
            // arrange
            var srcArray1 = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            var srcArray2 = NdArray<double>.Ones(ConfigManager.Instance, new[] { 3 });

            // action
            var newArray = NdArray<double>.Maximum(srcArray1, srcArray2);

            // assert
            Assert.Equal(1.0, newArray[0].Value);
            Assert.Equal(1.0, newArray[1].Value);
            Assert.Equal(1.0, newArray[2].Value);
        }

        [Fact]
        public void Minimum()
        {
            // arrange
            var srcArray1 = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            var srcArray2 = NdArray<double>.Ones(ConfigManager.Instance, new[] { 3 });

            // action
            var newArray = NdArray<double>.Minimum(srcArray1, srcArray2);

            // assert
            Assert.Equal(0.0, newArray[0].Value);
            Assert.Equal(0.0, newArray[1].Value);
            Assert.Equal(0.0, newArray[2].Value);
        }

        [Fact]
        public void Pow()
        {
            // arrange
            var lhs = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            lhs[0].Value = 5.0;
            lhs[1].Value = 6.0;
            lhs[2].Value = 7.0;

            var rhs = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            rhs[0].Value = 2.00;
            rhs[1].Value = 3.0;
            rhs[2].Value = 4.0;

            // action
            var newArray = NdArray<double>.Pow(lhs, rhs);

            // assert
            Assert.Equal(25.0, newArray[0].Value);
            Assert.Equal(216.0, newArray[1].Value);
            Assert.Equal(2401.0, newArray[2].Value);
        }

        [Fact]
        public void Round()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(ConfigManager.Instance, -1, 1, 6);

            // action
            var newArray = NdArray<double>.Round(srcArray);

            // assert
            Assert.Equal(-1.0, newArray[0].Value);
            Assert.Equal(-1.0, newArray[1].Value);
            Assert.Equal(0.0, newArray[2].Value);
            Assert.Equal(0.0, newArray[3].Value);
            Assert.Equal(1.0, newArray[4].Value);
            Assert.Equal(1.0, newArray[5].Value);
        }

        [Fact]
        public void Sign()
        {
            // arrange
            var src = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 4 });
            src[0].Value = -2.0;
            src[1].Value = -1.0;
            src[2].Value = 0.0;
            src[3].Value = 1.0;

            // action
            var newArray = NdArray<double>.Sign(src);

            // assert
            Assert.Equal(-1.0, newArray[0].Value);
            Assert.Equal(-1.0, newArray[1].Value);
            Assert.Equal(0.0, newArray[2].Value);
            Assert.Equal(1.0, newArray[3].Value);
        }

        [Fact]
        public void Sin()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = NdArray<double>.Sin(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - -1.0) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 1.0) < Epsilon);
        }

        [Fact]
        public void Sinh()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = NdArray<double>.Sinh(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - -2.3012989023072947) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 2.3012989023072947) < Epsilon);
        }

        [Fact]
        public void Sqrt()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = 1.0;
            srcArray[1].Value = 4.0;
            srcArray[2].Value = 16.0;

            // action
            var newArray = NdArray<double>.Sqrt(srcArray);

            // assert
            Assert.Equal(1.0, newArray[0].Value);
            Assert.Equal(2.0, newArray[1].Value);
            Assert.Equal(4.0, newArray[2].Value);
        }

        [Fact]
        public void Tan()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = NdArray<double>.Tan(srcArray);

            // assert
            const double Epsilon = 1e12;
            Assert.True(Math.Abs(newArray[0].Value - -16331778728383844) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 16331778728383844) < Epsilon);
        }

        [Fact]
        public void Tanh()
        {
            // arrange
            var srcArray = NdArray<double>.Zeros(ConfigManager.Instance, new[] { 3 });
            srcArray[0].Value = -Math.PI / 2.0;
            srcArray[1].Value = 0.0;
            srcArray[2].Value = Math.PI / 2.0;

            // action
            var newArray = NdArray<double>.Tanh(srcArray);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(newArray[0].Value - -0.91715234) < Epsilon);
            Assert.True(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.True(Math.Abs(newArray[2].Value - 0.91715234) < Epsilon);
        }

        [Fact]
        public void Truncate()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(ConfigManager.Instance, -1, 1, 6);

            // action
            var newArray = NdArray<double>.Truncate(srcArray);

            // assert
            Assert.Equal(-1.0, newArray[0].Value);
            Assert.Equal(0.0, newArray[1].Value);
            Assert.Equal(0.0, newArray[2].Value);
            Assert.Equal(0.0, newArray[3].Value);
            Assert.Equal(0.0, newArray[4].Value);
            Assert.Equal(1.0, newArray[5].Value);
        }

        [Fact]
        public void Flattern()
        {
            // arrange
            var srcArray = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 3, 4 });

            // action
            var newArray = NdArray<int>.Flattern(srcArray);

            // assert
            var expectedLayout = new Layout(new[] { 24 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, newArray.Layout);
        }

        [Fact]
        public void IsFinite()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Zeros(configManager,new[] { 2, 3, 4 });

            // action
            var finite = NdArray<int>.IsFinite(inputA);

            // assert
            Assert.True(NdArray<int>.All(finite));
        }

        [Fact]
        public void AllFinite()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var inputA = NdArray<int>.Zeros(configManager,new[] { 2, 3, 4 });

            // action
            var finite = NdArray<int>.AllFinite(inputA);

            // assert
            Assert.True(finite);
        }

        [Fact]
        public void MaxAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<int>.Arange(configManager,0, 10, 1);

            // action
            var max = NdArray<int>.MaxAxis(0, source);

            // assert
            Assert.Equal(9, max.Value);
        }

        [Fact]
        public void MaxNdArray()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<int>.Arange(configManager,0, 10, 1);

            // action
            var max = NdArray<int>.MaxNdArray(source);

            // assert
            Assert.Equal(9, max.Value);
        }

        [Fact]
        public void Max()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<int>.Arange(configManager,0, 10, 1);

            // action
            var max = NdArray<int>.Max(source);

            // assert
            Assert.Equal(9, max);
        }

        [Fact]
        public void MinAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<int>.Arange(configManager,0, 10, 1);

            // action
            var min = NdArray<int>.MinAxis(0, source);

            // assert
            Assert.Equal(0, min.Value);
        }

        [Fact]
        public void MinNdArray()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<int>.Arange(configManager,0, 10, 1);

            // action
            var min = NdArray<int>.MinNdArray(source);

            // assert
            Assert.Equal(0, min.Value);
        }

        [Fact]
        public void Min()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<int>.Arange(configManager,0, 10, 1);

            // action
            var min = NdArray<int>.Min(source);

            // assert
            Assert.Equal(0, min);
        }

        [Fact]
        public void SumAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<int>.Arange(configManager,0, 10, 1);

            // action
            var sum = NdArray<int>.SumAxis(0, source);

            // assert
            Assert.Equal(45, sum.Value);
        }

        [Fact]
        public void SumNdArray()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<int>.Arange(configManager,0, 10, 1);

            // action
            var sum = NdArray<int>.SumNdArray(source);

            // assert
            Assert.Equal(45, sum.Value);
        }

        [Fact]
        public void Sum()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<int>.Arange(configManager,0, 10, 1);

            // action
            var sum = NdArray<int>.Sum(source);

            // assert
            Assert.Equal(45, sum);
        }

        [Fact]
        public void MeanAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var mean = NdArray<double>.MeanAxis(1, source);

            // assert
            Assert.Equal(2.5, mean[0].Value);
        }

        [Fact]
        public void Mean()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var sum = NdArray<double>.Mean(source);

            // assert
            Assert.Equal(4.5, sum);
        }

        [Fact]
        public void ProductAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var product = NdArray<double>.ProductAxis(1, source);

            // assert
            Assert.Equal(24.0, product[0].Value);
        }

        [Fact]
        public void ProductNdArray()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var product = NdArray<double>.ProductNdArray(source);

            // assert
            Assert.Equal(40320.0, product[0].Value);
        }

        [Fact]
        public void Product()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var product = NdArray<double>.Product(source);

            // assert
            Assert.Equal(40320.0, product);
        }

        [Fact]
        public void VarAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var var = NdArray<double>.VarAxis(1, source);

            // assert
            Assert.Equal(1.25, var[0].Value);
        }

        [Fact]
        public void VarAxis_Ddof1()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var var = NdArray<double>.VarAxis(1, source, 1.0);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(1.666666666 - var[0].Value) < Epsilon);
        }

        [Fact]
        public void Var()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var var = NdArray<double>.Var(source);

            // assert
            Assert.Equal(5.25, var);
        }

        [Fact]
        public void Var_Ddof1()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var var = NdArray<double>.Var(source, 1.0);

            // assert
            Assert.Equal(6.0, var);
        }

        [Fact]
        public void StdAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var std = NdArray<double>.StdAxis(1, source);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(1.11803399 - std[0].Value) < Epsilon);
        }

        [Fact]
        public void StdAxis_Ddof1()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var std = NdArray<double>.StdAxis(1, source, 1.0);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(1.29099445 - std[0].Value) < Epsilon);
        }

        [Fact]
        public void Std()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var std = NdArray<double>.Std(source);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(2.29128784747792 - std) < Epsilon);
        }

        [Fact]
        public void Std_Ddof1()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<double>.Arange(configManager,1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var std = NdArray<double>.Std(source, 1.0);

            // assert
            const double Epsilon = 1e-8;
            Assert.True(Math.Abs(2.449489742783178 - std) < Epsilon);
        }

        [Fact]
        public void TraceAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<int>.Arange(configManager,0, 27, 1).Reshape(new[] { 3, 3, 3 });

            // action
            var trace = NdArray<int>.TraceAxis(0, 1, source);

            // assert
            Assert.Equal(36, trace[0].Value);
            Assert.Equal(39, trace[1].Value);
            Assert.Equal(42, trace[2].Value);
        }

        [Fact]
        public void Trace()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<int>.Arange(configManager,0, 27, 1).Reshape(new[] { 3, 3, 3 });

            // action
            var trace = NdArray<int>.Trace(source);

            // assert
            Assert.Equal(12, trace[0].Value);
            Assert.Equal(39, trace[1].Value);
            Assert.Equal(66, trace[2].Value);
        }

        [Fact]
        public void DiagAxis()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<int>.Zeros(configManager,new[] { 4, 3, 3, 5 });

            // action
            var diag = NdArray<int>.DiagAxis(1, 2, source);

            // assert
            Assert.Equal(new[] { 4, 3, 5 }, diag.Shape);
        }

        [Fact]
        public void Diag()
        {
            // arrange
            var configManager = ConfigManager.Instance;
            var source = NdArray<int>.Arange(configManager,0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var diag = NdArray<int>.Diag(source);

            // assert
            Assert.Equal(0, diag[0].Value);
            Assert.Equal(4, diag[1].Value);
            Assert.Equal(8, diag[2].Value);
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
            var concat = NdArray<int>.Concat(ConcatAxis, inputs);

            // assert
            Assert.Equal(new[] { 4, 53 }, concat.Shape);
        }

        [Fact]
        public void Copy_ColumnMajor()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 0, 10, 1).Reshape(new[] { 2, 5 });

            // action
            var copy = NdArray<int>.Copy(source, Order.ColumnMajor);

            // assert
            Assert.Equal(new[] { 2, 5 }, copy.Shape);
            Assert.Equal(new[] { 1, 2 }, copy.Layout.Stride);
        }

        [Fact]
        public void DiagMatAxis()
        {
            // arrange
            var axis1 = 0;
            var axis2 = 1;
            var source = NdArray<int>.Arange(ConfigManager.Instance, 0, 12, 1).Reshape(new[] { 4, 3 });

            // action
            var diagMat = NdArray<int>.DiagMatAxis(axis1, axis2, source);

            // assert
            Assert.Equal(new[] { 4, 4, 3 }, diagMat.Shape);
        }

        [Fact]
        public void DiagMat()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 0, 3, 1);

            // action
            var diagMat = NdArray<int>.DiagMat(source);

            // assert
            Assert.Equal(new[] { 3, 3 }, diagMat.Shape);
        }

        [Fact]
        public void DiffAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var result = NdArray<int>.DiffAxis(1, source);

            // assert
            Assert.Equal(new[] { 3, 2 }, result.Shape);
        }

        [Fact]
        public void Transpos()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 0, 4, 1).Reshape(new[] { 2, 2 });

            // action
            var result = NdArray<int>.Transpos(source);

            // assert
            Assert.Equal(0, result[new[] { 0, 0 }]);
            Assert.Equal(1, result[new[] { 1, 0 }]);
            Assert.Equal(2, result[new[] { 0, 1 }]);
            Assert.Equal(3, result[new[] { 1, 1 }]);
        }

        [Fact]
        public void AtLeastNd()
        {
            // arrange
            const int MinNumDim = 3;
            var source = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 2 });

            // action
            var output = NdArray<int>.AtLeastNd(MinNumDim, source);

            // assert
            Assert.Equal(new[] { 1, 2, 2 }, output.Shape);
        }

        [Fact]
        public void AtLeast1d()
        {
            // arrange
            const int DummyValue = 1;
            var source = NdArray<int>.Scalar(ConfigManager.Instance, DummyValue);

            // action
            var output = NdArray<int>.AtLeast1d(source);

            // assert
            Assert.Equal(new[] { 1 }, output.Shape);
        }

        [Fact]
        public void AtLeast2d()
        {
            // arrange
            const int DummyValue = 1;
            var source = NdArray<int>.Scalar(ConfigManager.Instance, DummyValue);

            // action
            var output = NdArray<int>.AtLeast2d(source);

            // assert
            Assert.Equal(new[] { 1, 1 }, output.Shape);
        }

        [Fact]
        public void AtLeast3d()
        {
            // arrange
            const int DummyValue = 1;
            var source = NdArray<int>.Scalar(ConfigManager.Instance, DummyValue);

            // action
            var output = NdArray<int>.AtLeast3d(source);

            // assert
            Assert.Equal(new[] { 1, 1, 1 }, output.Shape);
        }

        [Fact]
        public void BroadCastDim()
        {
            // arrange
            var source = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 3, 1, 5 });

            // action
            var output = NdArray<int>.BroadCastDim(1, 9, source);

            // assert
            Assert.Equal(new[] { 3, 9, 5 }, output.Shape);
        }

        [Fact]
        public void BroadCastTo()
        {
            // arrange
            var source = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 1, 7, 1 });

            // action
            var output = NdArray<int>.BroadCastTo(new[] { 2, 7, 3 }, source);

            // assert
            Assert.Equal(new[] { 2, 7, 3 }, output.Shape);
        }

        [Fact]
        public void BroadCastToSame_Two()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 4, 5 });
            var input2 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 3, 4, 5 });

            // action
            var (output1, output2) = NdArray<int>.BroadCastToSame(input1, input2);

            // assert
            Assert.Equal(new[] { 3, 4, 5 }, output1.Shape);
            Assert.Equal(new[] { 3, 4, 5 }, output2.Shape);
        }

        [Fact]
        public void BroadCastToSame_Three()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 5 });
            var input2 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 4, 5 });
            var input3 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 3, 4, 5 });

            // action
            var (output1, output2, output3) = NdArray<int>.BroadCastToSame(input1, input2, input3);

            // assert
            Assert.Equal(new[] { 3, 4, 5 }, output1.Shape);
            Assert.Equal(new[] { 3, 4, 5 }, output2.Shape);
            Assert.Equal(new[] { 3, 4, 5 }, output3.Shape);
        }

        [Fact]
        public void BroadCastToSame_Many()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 5 });
            var input2 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 4, 5 });
            var input3 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 3, 4, 5 });
            var input4 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 3, 4, 5 });

            // action
            var outputs = NdArray<int>.BroadCastToSame(new[] { input1, input2, input3, input4 });

            // assert
            Assert.Equal(new[] { 2, 3, 4, 5 }, outputs[0].Shape);
            Assert.Equal(new[] { 2, 3, 4, 5 }, outputs[1].Shape);
            Assert.Equal(new[] { 2, 3, 4, 5 }, outputs[2].Shape);
            Assert.Equal(new[] { 2, 3, 4, 5 }, outputs[3].Shape);
        }

        [Fact]
        public void BroadCastToSameInDims_Two()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 1, 7, 1 });
            var input2 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 3, 4, 5 });

            // action
            var (output1, output2) = NdArray<int>.BroadCastToSameInDims(new[] { 0, 2 }, input1, input2);

            // assert
            Assert.Equal(new[] { 3, 7, 5 }, output1.Shape);
            Assert.Equal(new[] { 3, 4, 5 }, output2.Shape);
        }

        [Fact]
        public void BroadCastToSameInDims_Three()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 1, 2, 1 });
            var input2 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 3, 5, 1 });
            var input3 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 3, 4, 5 });

            // action
            var (output1, output2, output3) = NdArray<int>.BroadCastToSameInDims(new[] { 0, 2 }, input1, input2, input3);

            // assert
            Assert.Equal(new[] { 3, 2, 5 }, output1.Shape);
            Assert.Equal(new[] { 3, 5, 5 }, output2.Shape);
            Assert.Equal(new[] { 3, 4, 5 }, output3.Shape);
        }

        [Fact]
        public void BroadCastToSameInDims_Many()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 1, 2, 1 });
            var input2 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 3, 5 });
            var input3 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 1, 4, 1 });
            var input4 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 5, 5 });

            // action
            var outputs = NdArray<int>.BroadCastToSameInDims(new[] { 0, 2 }, new[] { input1, input2, input3, input4 });

            // assert
            Assert.Equal(new[] { 2, 2, 5 }, outputs[0].Shape);
            Assert.Equal(new[] { 2, 3, 5 }, outputs[1].Shape);
            Assert.Equal(new[] { 2, 4, 5 }, outputs[2].Shape);
            Assert.Equal(new[] { 2, 5, 5 }, outputs[3].Shape);
        }

        [Fact]
        public void CutLeft()
        {
            // arrange
            var source = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 1, 2, 3 });

            // action
            var output = NdArray<int>.CutLeft(source);

            // assert
            Assert.Equal(new[] { 2, 3 }, output.Shape);
        }

        [Fact]
        public void CutRight()
        {
            // arrange
            var source = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 1, 2, 3 });

            // action
            var output = NdArray<int>.CutRight(source);

            // assert
            Assert.Equal(new[] { 1, 2 }, output.Shape);
        }

        [Fact]
        public void Flatten()
        {
            // arrange
            var source = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 3, 4 });

            // action
            var output = NdArray<int>.Flatten(source);

            // assert
            Assert.Equal(new[] { 24 }, output.Shape);
        }

        [Fact]
        public void InsertAxis()
        {
            // arrange
            var source = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 3, 4 });

            // action
            var output = NdArray<int>.InsertAxis(1, source);

            // assert
            Assert.Equal(new[] { 2, 1, 3, 4 }, output.Shape);
        }

        [Fact]
        public void IsBroadcasted_WithBroadCastedNdArray_ReturnTrue()
        {
            // arrange
            var source = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 1, 4 });
            var broadCasted = NdArray<int>.BroadCastDim(1, 2, source);

            // action
            var output = NdArray<int>.IsBroadcasted(broadCasted);

            // assert
            Assert.True(output);
        }

        [Fact]
        public void PadLeft()
        {
            // arrange
            var source = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 3, 4 });

            // action
            var output = NdArray<int>.PadLeft(source);

            // assert
            Assert.Equal(new[] { 1, 2, 3, 4 }, output.Shape);
        }

        [Fact]
        public void PadRight()
        {
            // arrange
            var source = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 3, 4 });

            // action
            var output = NdArray<int>.PadRight(source);

            // assert
            Assert.Equal(new[] { 2, 3, 4, 1 }, output.Shape);
        }

        [Fact]
        public void PadToSame_Two()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 4, 5 });
            var input2 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 3, 4, 5 });

            // action
            var (output1, output2) = NdArray<int>.PadToSame(input1, input2);

            // assert
            Assert.Equal(new[] { 1, 4, 5 }, output1.Shape);
            Assert.Equal(new[] { 3, 4, 5 }, output2.Shape);
        }

        [Fact]
        public void PadToSamee_Three()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 5 });
            var input2 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 4, 5 });
            var input3 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 3, 4, 5 });

            // action
            var (output1, output2, output3) = NdArray<int>.PadToSame(input1, input2, input3);

            // assert
            Assert.Equal(new[] { 1, 1, 5 }, output1.Shape);
            Assert.Equal(new[] { 1, 4, 5 }, output2.Shape);
            Assert.Equal(new[] { 3, 4, 5 }, output3.Shape);
        }

        [Fact]
        public void PadToSame_Many()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 5 });
            var input2 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 4, 5 });
            var input3 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 3, 4, 5 });
            var input4 = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 3, 4, 5 });

            // action
            var outputs = NdArray<int>.PadToSame(new[] { input1, input2, input3, input4 });

            // assert
            Assert.Equal(new[] { 1, 1, 1, 5 }, outputs[0].Shape);
            Assert.Equal(new[] { 1, 1, 4, 5 }, outputs[1].Shape);
            Assert.Equal(new[] { 1, 3, 4, 5 }, outputs[2].Shape);
            Assert.Equal(new[] { 2, 3, 4, 5 }, outputs[3].Shape);
        }

        [Fact]
        public void PermuteAxes()
        {
            // arrange
            var source = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 3, 4, 5 });

            // action
            var output = NdArray<int>.PermuteAxes(new[] { 1, 0, 3, 2 }, source);

            // assert
            Assert.Equal(new[] { 3, 2, 5, 4 }, output.Shape);
        }

        [Fact]
        public void ReverseAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 0, 4, 1);

            // action
            var output = NdArray<int>.ReverseAxis(0, source);

            // assert
            Assert.Equal(4, output.Shape[0]);
            Assert.Equal(-1, output.Layout.Stride[0]);
        }

        [Fact]
        public void SwapDim()
        {
            // arrange
            var source = NdArray<int>.Zeros(ConfigManager.Instance, new[] { 2, 3, 4 });

            // action
            var output = NdArray<int>.SwapDim(0, 2, source);

            // assert
            Assert.Equal(new[] { 4, 3, 2 }, output.Shape);
        }

        [Fact]
        public void LogicalNegate()
        {
            // arrange
            var source = NdArray<bool>.Zeros(ConfigManager.Instance, new int[] { 10 });

            // action
            var output = NdArray<bool>.Not(source);

            // assert
            Assert.True(NdArray<bool>.All(output));
        }

        [Fact]
        public void LogicalAnd()
        {
            // arrange
            var input1 = NdArray<bool>.Zeros(ConfigManager.Instance, new int[] { 4 });
            var input2 = NdArray<bool>.Ones(ConfigManager.Instance, new int[] { 4 });

            // action
            var output = NdArray<bool>.And(input1, input2);

            // assert
            var expected = NdArray<bool>.Zeros(ConfigManager.Instance, new int[] { 4 });
            var result = expected == output;
            Assert.True(NdArray<bool>.All(result));
        }

        [Fact]
        public void LogicalOr()
        {
            // arrange
            var input1 = NdArray<bool>.Zeros(ConfigManager.Instance, new int[] { 4 });
            var input2 = NdArray<bool>.Ones(ConfigManager.Instance, new int[] { 4 });

            // action
            var output = NdArray<bool>.Or(input1, input2);

            // assert
            var expected = NdArray<bool>.Ones(ConfigManager.Instance, new int[] { 4 });
            var result = expected == output;
            Assert.True(NdArray<bool>.All(result));
        }

        [Fact]
        public void LogicalXor()
        {
            // arrange
            var input1 = NdArray<bool>.Zeros(ConfigManager.Instance, new int[] { 2 });
            var input2 = NdArray<bool>.Ones(ConfigManager.Instance, new int[] { 2 });
            input2[0].Value = false;

            // action
            var output = NdArray<bool>.Xor(input1, input2);

            // assert
            Assert.False(output[0].Value);
            Assert.True(output[1].Value);
        }

        [Fact]
        public void AllIndex()
        {
            // arrange
            var array = NdArray<int>.Arange(ConfigManager.Instance, 0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var output = NdArray<int>.AllIndex(array);

            // assert
            Assert.Equal(new[] { 0, 0 }, output[0]);
            Assert.Equal(new[] { 0, 1 }, output[1]);
            Assert.Equal(new[] { 0, 2 }, output[2]);
            Assert.Equal(new[] { 1, 0 }, output[3]);
            Assert.Equal(new[] { 1, 1 }, output[4]);
            Assert.Equal(new[] { 1, 2 }, output[5]);
            Assert.Equal(new[] { 2, 0 }, output[6]);
            Assert.Equal(new[] { 2, 1 }, output[7]);
            Assert.Equal(new[] { 2, 2 }, output[8]);
        }

        [Fact]
        public void AllElements()
        {
            // arrange
            var array = NdArray<int>.Arange(ConfigManager.Instance, 0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var output = NdArray<int>.AllElements(array);

            // assert
            Assert.Equal(Enumerable.Range(0, 9).ToArray(), output);
        }

        [Fact]
        public void AllAxis()
        {
            // arrange
            var source = NdArray<bool>.Ones(ConfigManager.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = false;

            // action
            var output0 = NdArray<bool>.AllAxis(0, source);
            var output1 = NdArray<bool>.AllAxis(1, source);

            // assert
            Assert.True(output0[0].Value);
            Assert.True(output1[1].Value);
        }

        [Fact]
        public void AllNdArray()
        {
            // arrange
            var source = NdArray<bool>.Ones(ConfigManager.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = false;

            // action
            var output = NdArray<bool>.AllNdArray(source);

            // assert
            Assert.False(output[0].Value);
        }

        [Fact]
        public void All()
        {
            // arrange
            var source = NdArray<bool>.Ones(ConfigManager.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = false;

            // action
            var output = NdArray<bool>.All(source);

            // assert
            Assert.False(output);
        }

        [Fact]
        public void AnyAxis()
        {
            // arrange
            var source = NdArray<bool>.Zeros(ConfigManager.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = true;

            // action
            var output0 = NdArray<bool>.AnyAxis(0, source);
            var output1 = NdArray<bool>.AnyAxis(1, source);

            // assert
            Assert.True(output0[1].Value);
            Assert.False(output1[1].Value);
        }

        [Fact]
        public void AnyNdArray()
        {
            // arrange
            var source = NdArray<bool>.Zeros(ConfigManager.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = true;

            // action
            var output = NdArray<bool>.AnyNdArray(source);

            // assert
            Assert.True(output[0].Value);
        }

        [Fact]
        public void Any()
        {
            // arrange
            var source = NdArray<bool>.Zeros(ConfigManager.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = true;

            // action
            var output = NdArray<bool>.Any(source);

            // assert
            Assert.True(output);
        }

        [Fact]
        public void CountTrueAxis()
        {
            // arrange
            var source = NdArray<bool>.Ones(ConfigManager.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = false;
            source[new[] { 1, 0 }] = false;
            source[new[] { 1, 3 }] = false;

            // action
            var output0 = NdArray<bool>.CountTrueAxis(0, source);
            var output1 = NdArray<bool>.CountTrueAxis(1, source);

            // assert
            Assert.Equal(1, output0[0].Value);
            Assert.Equal(2, output1[1].Value);
        }

        [Fact]
        public void CountTrueNdArray()
        {
            // arrange
            var source = NdArray<bool>.Ones(ConfigManager.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = false;
            source[new[] { 1, 0 }] = false;
            source[new[] { 1, 3 }] = false;

            // action
            var output = NdArray<bool>.CountTrueNdArray(source);

            // assert
            Assert.Equal(5, output[0].Value);
        }

        [Fact]
        public void CountTrue()
        {
            // arrange
            var source = NdArray<bool>.Ones(ConfigManager.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = false;
            source[new[] { 1, 0 }] = false;
            source[new[] { 1, 3 }] = false;

            // action
            var output = NdArray<bool>.CountTrue(source);

            // assert
            Assert.Equal(5, output);
        }

        [Fact]
        public void IfThenElse()
        {
            // arrange
            var condition = NdArray<bool>.Ones(ConfigManager.Instance, new int[] { 4 });
            var ifTrue = NdArray<int>.Ones(ConfigManager.Instance, new int[] { 4 });
            var ifFalse = NdArray<int>.Zeros(ConfigManager.Instance, new int[] { 4 });

            condition[new[] { 0 }] = false;
            condition[new[] { 2 }] = false;

            // action
            var output = NdArray<int>.IfThenElse(condition, ifTrue, ifFalse);

            // assert
            Assert.Equal(0, output[0].Value);
            Assert.Equal(1, output[1].Value);
            Assert.Equal(0, output[2].Value);
            Assert.Equal(1, output[3].Value);
        }

        [Fact]
        public void ArgMaxAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = NdArray<int>.ArgMaxAxis(1, source);

            // assert
            Assert.Equal(3, output[0].Value);
            Assert.Equal(3, output[1].Value);
        }

        [Fact]
        public void ArgMax()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = NdArray<int>.ArgMax(source);

            // assert
            Assert.Equal(new[] { 1, 3 }, output);
        }

        [Fact]
        public void ArgMinAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = NdArray<int>.ArgMinAxis(1, source);

            // assert
            Assert.Equal(0, output[0].Value);
            Assert.Equal(0, output[1].Value);
        }

        [Fact]
        public void ArgMin()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = NdArray<int>.ArgMin(source);

            // assert
            Assert.Equal(new[] { 0, 0 }, output);
        }

        [Fact]
        public void FindAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 0, 8, 1).Reshape(new[] { 2, 4 });

            // action
            var output = NdArray<int>.FindAxis(2, 1, source);

            // assert
            Assert.Equal(2, output[0].Value);
            Assert.Equal(SpecialIdx.NotFound, output[1].Value);
        }

        [Fact]
        public void TryFind()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });
            source[new[] { 1, 3 }] = 2;

            // action
            var output = NdArray<int>.TryFind(2, source);

            // assert
            Assert.Equal(new[] { 0, 1 }, output);
        }

        [Fact]
        public void TryFind_NotFound()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = NdArray<int>.TryFind(10, source);

            // assert
            Assert.True(output.Length == 0);
        }

        [Fact]
        public void Find()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });
            source[new[] { 1, 3 }] = 2;

            // action
            var output = NdArray<int>.Find(2, source);

            // assert
            Assert.Equal(new[] { 0, 1 }, output);
        }

        [Fact]
        public void Find_NotFound_ThrowException()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => NdArray<int>.Find(10, source));
            Assert.Equal("Value 10 was not found in specifed NdArray.", exception.Message);
        }

        [Fact]
        public void AssertBool_NotBool_ThrowException()
        {
            // arrange
            var source = NdArray<int>.Arange(ConfigManager.Instance, 0, 8, 1).Reshape(new[] { 2, 4 });

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => source.AssertBool());
            Assert.Equal("The operation requires a NdArray<bool> but the data type of the specified NdArray is System.Int32.", exception.Message);
        }

        [Fact]
        public void AssertInt_NotInt_ThrowException()
        {
            // arrange
            var source = NdArray<uint>.Arange(ConfigManager.Instance, 0, 8, 1).Reshape(new[] { 2, 4 });

            // action
            var exception = Assert.Throws<InvalidOperationException>(() => source.AssertInt());
            Assert.Equal("The operation requires a NdArray<System.Int32> but the data type of the specified NdArray is System.UInt32.", exception.Message);
        }

        public static IEnumerable<object[]> FillBinaryOperatorTestInputs()
        {
            yield return new object[] { 1 };    // Int
            yield return new object[] { 1.0 };  // Double
        }

        [Theory]
        [MemberData(nameof(FillBinaryOperatorTestInputs))]
        public static void BinaryFill_ArrayWithArray<T>(T value) where T : struct
        {
            Generic_FillArrayWithArray(value, typeof(T).ToString());
            //Generic_FillScalarWithArray(value, typeof(T).ToString());
            //Generic_FillArrayWithScalar(value, typeof(T).ToString());
        }

        private static void Generic_FillArrayWithArray<T>(T value, string typeName) where T : struct
        {
            // arrange
            var source0Mock = new Mock<IFrontend<T>>();
            var source1Mock = new Mock<IFrontend<T>>();
            var resultMock = new Mock<IFrontend<bool>>();
            var comparisonFuncMock = new Mock<INdArrayComparison<T>>();
            source0Mock.SetupGet(m => m.Comparison).Returns(comparisonFuncMock.Object);

            // action
            NdArray<T>.FillEqual(resultMock.Object, source0Mock.Object, source1Mock.Object);

            // assert
            comparisonFuncMock.Verify(m => m.FillEqual(resultMock.Object, source0Mock.Object, source1Mock.Object), typeName);
        }

        private static void Generic_FillScalarWithArray<T>(T value, string typeName) where T : struct
        {
            // arrange
            var source1Mock = new Mock<IFrontend<T>>();
            var resultMock = new Mock<IFrontend<bool>>();
            var comparisonFuncMock = new Mock<INdArrayComparison<T>>();
            source1Mock.SetupGet(m => m.Comparison).Returns(comparisonFuncMock.Object);

            // action
            NdArray<T>.FillEqual(resultMock.Object, value, source1Mock.Object);

            // assert
            comparisonFuncMock.Verify(m => m.FillEqual(resultMock.Object, It.IsAny<NdArray<T>>(), source1Mock.Object), typeName);
        }

        private static void Generic_FillArrayWithScalar<T>(T value, string typeName) where T : struct
        {
            // arrange
            var source0Mock = new Mock<IFrontend<T>>();
            var resultMock = new Mock<IFrontend<bool>>();
            var comparisonFuncMock = new Mock<INdArrayComparison<T>>();
            source0Mock.SetupGet(m => m.Comparison).Returns(comparisonFuncMock.Object);

            // action
            NdArray<T>.FillEqual(resultMock.Object, source0Mock.Object, value);

            // assert
            comparisonFuncMock.Verify(m => m.FillEqual(resultMock.Object, source0Mock.Object, It.IsAny<NdArray<T>>()), typeName);
        }

        private struct UnKownValueTypeForTestOnly
        {
            public override string ToString()
            {
                return "UnKownType";
            }
        }
    }
}