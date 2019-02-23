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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NdArrayNet;
    using System;
    using System.Linq;

    [TestClass]
    public class NdArrayTests
    {
        [TestMethod]
        public void NdArray_RowMajor()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = new NdArray<int>(new int[] { 1, 2, 3, 4, 5 }, device, Order.RowMajor);

            // assert
            var expected = new[] { 120, 60, 20, 5, 1 };
            CollectionAssert.AreEqual(expected, array.Layout.Stride);
        }

        [TestMethod]
        public void NdArray_ColumnMajor()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = new NdArray<int>(new int[] { 1, 2, 3, 4, 5 }, device, Order.ColumnMajor);

            // assert
            var expected = new[] { 1, 1, 2, 6, 24 };
            CollectionAssert.AreEqual(expected, array.Layout.Stride);
        }

        [TestMethod]
        public void NumElements()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = new NdArray<int>(new int[] { 1, 2, 3, 4, 5 }, device, Order.ColumnMajor);

            // assert
            var expected = 120;
            Assert.AreEqual(expected, array.NumElements);
        }

        [TestMethod]
        public void NumDimensions()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = new NdArray<int>(new int[] { 1, 2, 3, 4, 5 }, device, Order.ColumnMajor);

            // assert
            var expected = 5;
            Assert.AreEqual(expected, array.NumDimensions);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FillIncrementing_WithScalar_ThrowException()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Ones(device, new int[] { });

            // action
            array.FillIncrementing(0, 1);
        }

        [TestMethod]
        public void GetValue_ScalarArray_ReturnValue()
        {
            // arrange
            var device = HostDevice.Instance;
            var scalarArray = NdArray<int>.Ones(device, new int[] { });

            // action
            var value = scalarArray.Value;

            // assert
            Assert.AreEqual(1, value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetValue_Vector_ThrowException()
        {
            // arrange
            var device = HostDevice.Instance;
            var scalarArray = NdArray<int>.Ones(device, new int[] { 1 });

            // action
            var _ = scalarArray.Value;
        }

        [TestMethod]
        public void SetValue_ScalarArray_ReturnValue()
        {
            // arrange
            var device = HostDevice.Instance;
            var scalarArray = NdArray<int>.Ones(device, new int[] { });

            // action
            scalarArray.Value = 100;

            // assert
            Assert.AreEqual(100, scalarArray.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetValue_Vector_ThrowException()
        {
            // arrange
            var device = HostDevice.Instance;
            var scalarArray = NdArray<int>.Ones(device, new int[] { 1 });

            // action
            scalarArray.Value = 100;
        }

        [TestMethod]
        public void Get_SingleIndex()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Arange(device, 0, 3, 1);

            // action
            var scalarArray = array[2];

            // assert
            Assert.AreEqual(2, scalarArray.Value);
        }

        [TestMethod]
        public void Set_SingleIndex()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Arange(device, 0, 3, 1);

            // action
            array[2] = NdArray<int>.Ones(device, new int[] { });
            var scalarArray = array[2];

            // assert
            Assert.AreEqual(1, scalarArray.Value);
        }

        [TestMethod]
        public void AssertSameShape_WithSameShape_Pass()
        {
            // arrange
            // arrange
            var device = HostDevice.Instance;
            var arrayA = NdArray<int>.Arange(device, 1, 3, 1);
            var arrayB = NdArray<int>.Arange(device, 1, 3, 1);

            // action
            NdArray<int>.AssertSameShape(arrayA, arrayB);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AssertSameShape_DifferentShape_ThrowException()
        {
            // arrange
            var device = HostDevice.Instance;
            var arrayA = NdArray<int>.Arange(device, 0, 3, 1);
            var arrayB = NdArray<int>.Arange(device, 1, 3, 1);

            // action
            NdArray<int>.AssertSameShape(arrayA, arrayB);
        }

        [TestMethod]
        public void AssertSameStorage()
        {
            // arrange
            // arrange
            var device = HostDevice.Instance;
            var arrays = new[]
            {
                NdArray<int>.Arange(device, 1, 3, 1),
                NdArray<int>.Arange(device, 1, 3, 1)
            };

            // action
            NdArray<int>.AssertSameStorage(arrays);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AssertScalar_WithVector_ThrowException()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Arange(device, 0, 3, 1);

            // action
            array.AssertScalar();
        }

        [TestMethod]
        public void AssertScalar_WithScalar_Pass()
        {
            // arrange
            var device = HostDevice.Instance;
            var emptyShapeForScalar = new int[] { };
            var array = NdArray<int>.Zeros(device, emptyShapeForScalar);

            // action
            array.AssertScalar();
        }

        [TestMethod]
        public void SetAndGetWithArrayAndPos()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Ones(device, new[] { 2, 3, 4 });

            // action
            NdArray<int>.Set(array, new[] { 0, 1, 2 }, 999);
            var value = NdArray<int>.Get(array, new[] { 0, 1, 2 });

            // assert
            Assert.AreEqual(999, value);
        }

        [TestMethod]
        public void GetView_WithRangeSpecifiers()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Ones(device, new[] { 2, 3 });

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
            Assert.AreEqual(2, scalarView.Value);
            Assert.AreEqual(1, arrayView1[0].Value);
            Assert.AreEqual(2, arrayView1[1].Value);
            Assert.AreEqual(3, arrayView1[2].Value);
            Assert.AreEqual(4, arrayView2[0].Value);
            Assert.AreEqual(5, arrayView2[1].Value);
            Assert.AreEqual(4, arrayView3[new[] { 0, 0 }]);
            Assert.AreEqual(5, arrayView3[new[] { 0, 1 }]);
        }

        [TestMethod]
        public void SetView_WithRangeSpecifiers()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Ones(device, new[] { 2, 3 });

            // action
            array[new[] { RangeFactory.Elem(0), RangeFactory.All }] = NdArray<int>.Arange(device, 7, 10, 1);

            // assert
            Assert.AreEqual(7, NdArray<int>.Get(array, new[] { 0, 0 }));
            Assert.AreEqual(8, NdArray<int>.Get(array, new[] { 0, 1 }));
            Assert.AreEqual(9, NdArray<int>.Get(array, new[] { 0, 2 }));
            Assert.AreEqual(1, NdArray<int>.Get(array, new[] { 1, 0 }));
            Assert.AreEqual(1, NdArray<int>.Get(array, new[] { 1, 1 }));
            Assert.AreEqual(1, NdArray<int>.Get(array, new[] { 1, 2 }));
        }

        [TestMethod]
        public void ScalarString()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var strInt = NdArray<int>.ScalarString(NdArray<int>.Ones(device, new int[] { }));
            var strLong = NdArray<long>.ScalarString(NdArray<long>.Ones(device, new int[] { }));
            var strFloat = NdArray<float>.ScalarString(NdArray<float>.Ones(device, new int[] { }));
            var strDouble = NdArray<double>.ScalarString(NdArray<double>.Ones(device, new int[] { }));
            var strBool = NdArray<bool>.ScalarString(NdArray<bool>.Ones(device, new int[] { }));
            var strByte = NdArray<byte>.ScalarString(NdArray<byte>.Ones(device, new int[] { }));
            var strUnkown = NdArray<UnKownValueTypeForTestOnly>.ScalarString(NdArray<UnKownValueTypeForTestOnly>.Zeros(device, new int[] { }));

            // assert
            Assert.AreEqual("   1", strInt);
            Assert.AreEqual("   1", strLong);
            Assert.AreEqual("   1.0000", strFloat);
            Assert.AreEqual("   1.0000", strDouble);
            Assert.AreEqual("true", strBool);
            Assert.AreEqual("  1", strByte);
            Assert.AreEqual("UnKownType", strUnkown);
        }

        [TestMethod]
        public void PrettyDim()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Ones(device, new[] { 1, 2, 3 });

            // action
            var str = NdArray<int>.PrettyDim(10, " ", array);

            // assert
            Assert.AreEqual("[[[   1    1    1]\n  [   1    1    1]]]", str);
        }

        [TestMethod]
        public void ArrayToString()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Ones(device, new[] { 1, 2, 3 });

            // action
            var str = array.ToString();

            // assert
            Assert.AreEqual("[[[   1    1    1]\n  [   1    1    1]]]", str);
        }

        [TestMethod]
        public void TryReshapeView_WitoutCopy_ReturnReshapedArray()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Ones(device, new[] { 10 });

            // action
            var newView = array.TryReshapeView(new[] { 1, 10 });

            // assert
            CollectionAssert.AreEqual(new[] { 1, 10 }, newView.Shape);
        }

        [TestMethod]
        public void TryReshapeView_NeedCopy_ReturnNull()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = new NdArray<int>(new[] { 2, 5 }, HostDevice.Instance, Order.ColumnMajor);

            // action
            var newView = array.TryReshapeView(new[] { 2, 5 });

            // assert
            Assert.IsNull(newView);
        }

        [TestMethod]
        public void ReshapeView_WitoutCopy_ReturnReshapedArray()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Ones(device, new[] { 10 });

            // action
            var newView = array.ReshapeView(new[] { 1, 10 });

            // assert
            CollectionAssert.AreEqual(new[] { 1, 10 }, newView.Shape);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReshapeView_NeedCopy_ReturnNull()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = new NdArray<int>(new[] { 2, 5 }, HostDevice.Instance, Order.ColumnMajor);

            // action
            var newView = array.ReshapeView(new[] { 2, 5 });
        }

        [TestMethod]
        public void Reshape_WithoutCopy()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = NdArray<int>.Arange(device, 0, 2 * 3 * 4 * 5, 1);

            // action
            var newView = array.Reshape(new[] { 2, 3, 4, 5 });

            // assert
            CollectionAssert.AreEqual(new[] { 2, 3, 4, 5 }, newView.Shape);
        }

        [TestMethod]
        public void Reshape_NeedCopy()
        {
            // arrange
            var device = HostDevice.Instance;
            var array = new NdArray<int>(new[] { 2, 5 }, HostDevice.Instance, Order.ColumnMajor);

            // action
            var newView = array.Reshape(new[] { 5, 2 });

            // assert
            CollectionAssert.AreEqual(new[] { 5, 2 }, newView.Shape);
        }

        [TestMethod]
        public void Broadcasting_VectorsWithSameShape()
        {
            // arrange
            var device = HostDevice.Instance;
            var arrayA = NdArray<int>.Arange(device, 0, 24, 1).Reshape(new[] { 2, 3, 4 });
            var arrayB = NdArray<int>.Arange(device, 0, 24, 1).Reshape(new[] { 2, 3, 4 });

            // action
            var result = arrayA * arrayB;

            // assert
            CollectionAssert.AreEqual(new[] { 2, 3, 4 }, result.Shape);
        }

        [TestMethod]
        public void Broadcasting_VectorsWithDiffernetShape()
        {
            // arrange
            var device = HostDevice.Instance;
            var arrayA = NdArray<int>.Arange(device, 0, 24, 1).Reshape(new[] { 2, 3, 4 });
            var arrayB = NdArray<int>.Arange(device, 0, 12, 1).Reshape(new[] { 3, 4 });

            // action
            var result = arrayA * arrayB;

            // assert
            CollectionAssert.AreEqual(new[] { 2, 3, 4 }, result.Shape);
        }

        [TestMethod]
        public void Broadcasting_VectorsWithScalar()
        {
            // arrange
            var device = HostDevice.Instance;
            var arrayA = NdArray<int>.Arange(device, 0, 24, 1).Reshape(new[] { 2, 3, 4 });
            var arrayB = NdArray<int>.Ones(device, new int[] { });

            // action
            var result = arrayA * arrayB;

            // assert
            CollectionAssert.AreEqual(new[] { 2, 3, 4 }, result.Shape);
        }

        [TestMethod]
        public void Broadcasting_ScalarWithScalar()
        {
            // arrange
            var device = HostDevice.Instance;
            var arrayA = NdArray<int>.Ones(device, new int[] { });
            var arrayB = NdArray<int>.Ones(device, new int[] { });

            // action
            var result = arrayA * arrayB;

            // assert
            CollectionAssert.AreEqual(new int[] { }, result.Shape);
        }

        [TestMethod]
        public void Broadcasting_ScalarWithVectors()
        {
            // arrange
            var device = HostDevice.Instance;
            var arrayA = NdArray<int>.Arange(device, 0, 24, 1).Reshape(new[] { 2, 3, 4 });
            var arrayB = NdArray<int>.Ones(device, new int[] { });

            // action
            var result = arrayB * arrayA;

            // assert
            CollectionAssert.AreEqual(new[] { 2, 3, 4 }, result.Shape);
        }

        [TestMethod]
        public void Scalar()
        {
            // arrange
            var device = HostDevice.Instance;
            const int Value = 9;

            // action
            var array = NdArray<int>.Scalar(device, Value);

            // asssert
            Assert.AreEqual(Value, array.Value);
        }

        [TestMethod]
        public void ScalarLike()
        {
            // arrange
            var device = HostDevice.Instance;
            var referenceArray = NdArray<int>.Arange(device, 0, 24, 1).Reshape(new[] { 2, 3, 4 });
            const int Value = 9;

            // action
            var array = NdArray<int>.ScalarLike(referenceArray, Value);

            // asssert
            Assert.AreEqual(Value, array.Value);
        }

        [TestMethod]
        public void Counting()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = NdArray<int>.Counting(device, 10);

            // assert
            var expectedLayout = new Layout(new[] { 10 }, 0, new[] { 1 });
            Assert.AreEqual(expectedLayout, array.Layout);
        }

        [TestMethod]
        public void Empty()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = NdArray<int>.Empty(device, 3);

            // assert
            var expectedLayout = new Layout(new[] { 0, 0, 0 }, 0, new[] { 0 });
        }

        [TestMethod]
        public void Falses()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = NdArray<bool>.Falses(device, new[] { 1, 2, 3 });

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 1 });
        }

        [TestMethod]
        public void Filled()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = NdArray<int>.Filled(device, new[] { 1, 2, 3 }, 55);

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 1 });
        }

        [TestMethod]
        public void Identity_3by3()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = NdArray<int>.Identity(device, 3);

            // assert
            var expectedLayout = new Layout(new[] { 3, 3 }, 0, new[] { 1 });
        }

        [TestMethod]
        public void Ones()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = NdArray<int>.Ones(device, new[] { 2, 2 });

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 1 });
        }

        [TestMethod]
        public void OnesLike()
        {
            // arrange
            var device = HostDevice.Instance;
            var zeros = NdArray<int>.Zeros(device, new[] { 2, 2 });

            // action
            var array = NdArray<int>.OnesLike(zeros);

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 1 });
        }

        [TestMethod]
        public void Linspace()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = NdArray<int>.Linspace(device, 0, 5, 5);

            // assert
            var expectedLayout = new Layout(new[] { 5 }, 0, new[] { 1 });
        }

        [TestMethod]
        public void Trues()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = NdArray<bool>.Trues(device, new[] { 1, 2, 3 });

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 1 });
        }

        [TestMethod]
        public void Zeros()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = NdArray<int>.Zeros(device, new[] { 2, 2 });

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 1 });
        }

        [TestMethod]
        public void ZerosLike()
        {
            // arrange
            var device = HostDevice.Instance;
            var ones = NdArray<int>.Ones(device, new[] { 2, 2 });

            // action
            var array = NdArray<int>.ZerosLike(ones);

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 1 });
        }

        [TestMethod]
        public void Abs()
        {
            // arrange
            var srcArray = NdArray<int>.Linspace(HostDevice.Instance, -4, 4, 8);

            // action
            var newArray = NdArray<int>.Abs(srcArray);

            // assert
            Assert.IsTrue(newArray[0].Value > 0);
        }

        [TestMethod]
        public void Acos()
        {
            // arrange
            var srcArray = NdArray<double>.Linspace(HostDevice.Instance, -1, 1, 3);

            // action
            var newArray = NdArray<double>.Acos(srcArray);

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
            var newArray = NdArray<double>.Asin(srcArray);

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
            var newArray = NdArray<double>.Atan(srcArray);

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
            var newArray = NdArray<double>.Ceiling(srcArray);

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
            var newArray = NdArray<double>.Cos(srcArray);

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
            var newArray = NdArray<double>.Cosh(srcArray);

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
            var newArray = NdArray<double>.Exp(srcArray);

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
            var newArray = NdArray<double>.Floor(srcArray);

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
            var newArray = NdArray<double>.Log(srcArray);

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
            var newArray = NdArray<double>.Log10(srcArray);

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
            var newArray = NdArray<double>.Maximum(srcArray1, srcArray2);

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
            var newArray = NdArray<double>.Minimum(srcArray1, srcArray2);

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
            var newArray = NdArray<double>.Pow(lhs, rhs);

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
            var newArray = NdArray<double>.Round(srcArray);

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
            var newArray = NdArray<double>.Sign(src);

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
            var newArray = NdArray<double>.Sin(srcArray);

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
            var newArray = NdArray<double>.Sinh(srcArray);

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
            var newArray = NdArray<double>.Sqrt(srcArray);

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
            var newArray = NdArray<double>.Tan(srcArray);

            // assert
            const double Epsilon = 1e10;
            Assert.IsTrue(Math.Abs(newArray[0].Value - -16331778728383844) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[1].Value - 0.0) < Epsilon);
            Assert.IsTrue(Math.Abs(newArray[2].Value - 16331778728383844) < Epsilon);
        }

        [TestMethod]
        public void Perf()
        {
            var a0 = NdArray<double>.Zeros(HostDevice.Instance, new[] { 2, 3, 4, 2 });
            var a1 = NdArray<double>.Zeros(HostDevice.Instance, new[] { 3, 4, 2 });
            var c = NdArray<double>.Zeros(HostDevice.Instance, new[] { 2, 3, 4, 2 });
            for (var i = 0; i < 10000; i++)
            {
                c = a0 * a1;
            }
            Assert.IsTrue(true);
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
            var newArray = NdArray<double>.Tanh(srcArray);

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
            var newArray = NdArray<double>.Truncate(srcArray);

            // assert
            Assert.AreEqual(-1.0, newArray[0].Value);
            Assert.AreEqual(0.0, newArray[1].Value);
            Assert.AreEqual(0.0, newArray[2].Value);
            Assert.AreEqual(0.0, newArray[3].Value);
            Assert.AreEqual(0.0, newArray[4].Value);
            Assert.AreEqual(1.0, newArray[5].Value);
        }

        [TestMethod]
        public void Flattern()
        {
            // arrange
            var srcArray = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 3, 4 });

            // action
            var newArray = NdArray<int>.Flattern(srcArray);

            // assert
            var expectedLayout = new Layout(new[] { 24 }, 0, new[] { 1 });
            Assert.AreEqual(expectedLayout, newArray.Layout);
        }

        [TestMethod]
        public void IsFinite()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Zeros(device, new[] { 2, 3, 4 });

            // action
            var finite = NdArray<int>.IsFinite(inputA);

            // assert
            Assert.IsTrue(NdArray<int>.All(finite));
        }

        [TestMethod]
        public void AllFinite()
        {
            // arrange
            var device = HostDevice.Instance;
            var inputA = NdArray<int>.Zeros(device, new[] { 2, 3, 4 });

            // action
            var finite = NdArray<int>.AllFinite(inputA);

            // assert
            Assert.IsTrue(finite);
        }

        [TestMethod]
        public void MaxAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var max = NdArray<int>.MaxAxis(0, source);

            // assert
            Assert.AreEqual(9, max.Value);
        }

        [TestMethod]
        public void MaxNdArray()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var max = NdArray<int>.MaxNdArray(source);

            // assert
            Assert.AreEqual(9, max.Value);
        }

        [TestMethod]
        public void Max()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var max = NdArray<int>.Max(source);

            // assert
            Assert.AreEqual(9, max);
        }

        [TestMethod]
        public void MinAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var min = NdArray<int>.MinAxis(0, source);

            // assert
            Assert.AreEqual(0, min.Value);
        }

        [TestMethod]
        public void MinNdArray()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var min = NdArray<int>.MinNdArray(source);

            // assert
            Assert.AreEqual(0, min.Value);
        }

        [TestMethod]
        public void Min()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var min = NdArray<int>.Min(source);

            // assert
            Assert.AreEqual(0, min);
        }

        [TestMethod]
        public void SumAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var sum = NdArray<int>.SumAxis(0, source);

            // assert
            Assert.AreEqual(45, sum.Value);
        }

        [TestMethod]
        public void SumNdArray()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var sum = NdArray<int>.SumNdArray(source);

            // assert
            Assert.AreEqual(45, sum.Value);
        }

        [TestMethod]
        public void Sum()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<int>.Arange(device, 0, 10, 1);

            // action
            var sum = NdArray<int>.Sum(source);

            // assert
            Assert.AreEqual(45, sum);
        }

        [TestMethod]
        public void MeanAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var mean = NdArray<double>.MeanAxis(1, source);

            // assert
            Assert.AreEqual(2.5, mean[0].Value);
        }

        [TestMethod]
        public void Mean()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var sum = NdArray<double>.Mean(source);

            // assert
            Assert.AreEqual(4.5, sum);
        }

        [TestMethod]
        public void ProductAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var product = NdArray<double>.ProductAxis(1, source);

            // assert
            Assert.AreEqual(24.0, product[0].Value);
        }

        [TestMethod]
        public void ProductNdArray()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var product = NdArray<double>.ProductNdArray(source);

            // assert
            Assert.AreEqual(40320.0, product[0].Value);
        }

        [TestMethod]
        public void Product()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var product = NdArray<double>.Product(source);

            // assert
            Assert.AreEqual(40320.0, product);
        }

        [TestMethod]
        public void VarAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var var = NdArray<double>.VarAxis(1, source);

            // assert
            Assert.AreEqual(1.25, var[0].Value);
        }

        [TestMethod]
        public void VarAxis_Ddof1()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var var = NdArray<double>.VarAxis(1, source, 1.0);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(1.666666666 - var[0].Value) < Epsilon);
        }

        [TestMethod]
        public void Var()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var var = NdArray<double>.Var(source);

            // assert
            Assert.AreEqual(5.25, var);
        }

        [TestMethod]
        public void Var_Ddof1()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var var = NdArray<double>.Var(source, 1.0);

            // assert
            Assert.AreEqual(6.0, var);
        }

        [TestMethod]
        public void StdAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var std = NdArray<double>.StdAxis(1, source);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(1.11803399 - std[0].Value) < Epsilon);
        }

        [TestMethod]
        public void StdAxis_Ddof1()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var std = NdArray<double>.StdAxis(1, source, 1.0);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(1.29099445 - std[0].Value) < Epsilon);
        }

        [TestMethod]
        public void Std()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var std = NdArray<double>.Std(source);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(2.29128784747792 - std) < Epsilon);
        }

        [TestMethod]
        public void Std_Ddof1()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<double>.Arange(device, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var std = NdArray<double>.Std(source, 1.0);

            // assert
            const double Epsilon = 1e-8;
            Assert.IsTrue(Math.Abs(2.449489742783178 - std) < Epsilon);
        }

        [TestMethod]
        public void TraceAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<int>.Arange(device, 0, 27, 1).Reshape(new[] { 3, 3, 3 });

            // action
            var trace = NdArray<int>.TraceAxis(0, 1, source);

            // assert
            Assert.AreEqual(36, trace[0].Value);
            Assert.AreEqual(39, trace[1].Value);
            Assert.AreEqual(42, trace[2].Value);
        }

        [TestMethod]
        public void Trace()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<int>.Arange(device, 0, 27, 1).Reshape(new[] { 3, 3, 3 });

            // action
            var trace = NdArray<int>.Trace(source);

            // assert
            Assert.AreEqual(12, trace[0].Value);
            Assert.AreEqual(39, trace[1].Value);
            Assert.AreEqual(66, trace[2].Value);
        }

        [TestMethod]
        public void DiagAxis()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<int>.Zeros(device, new[] { 4, 3, 3, 5 });

            // action
            var diag = NdArray<int>.DiagAxis(1, 2, source);

            // assert
            CollectionAssert.AreEqual(new[] { 4, 3, 5 }, diag.Shape);
        }

        [TestMethod]
        public void Diag()
        {
            // arrange
            var device = HostDevice.Instance;
            var source = NdArray<int>.Arange(device, 0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var diag = NdArray<int>.Diag(source);

            // assert
            Assert.AreEqual(0, diag[0].Value);
            Assert.AreEqual(4, diag[1].Value);
            Assert.AreEqual(8, diag[2].Value);
        }

        [TestMethod]
        public void Concat()
        {
            // arrange
            const int ConcatAxis = 1;
            var inputs = new NdArray<int>[]
            {
                NdArray<int>.Zeros(HostDevice.Instance, new[] { 4, 28 }),
                NdArray<int>.Zeros(HostDevice.Instance, new[] { 4, 15 }),
                NdArray<int>.Zeros(HostDevice.Instance, new[] { 4, 10 })
            };

            // action
            var concat = NdArray<int>.Concat(ConcatAxis, inputs);

            // assert
            CollectionAssert.AreEqual(new[] { 4, 53 }, concat.Shape);
        }

        [TestMethod]
        public void Copy_ColumnMajor()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 0, 10, 1).Reshape(new[] { 2, 5 });

            // action
            var copy = NdArray<int>.Copy(source, Order.ColumnMajor);

            // assert
            CollectionAssert.AreEqual(new[] { 2, 5 }, copy.Shape);
            CollectionAssert.AreEqual(new[] { 1, 2 }, copy.Layout.Stride);
        }

        [TestMethod]
        public void DiagMatAxis()
        {
            // arrange
            var axis1 = 0;
            var axis2 = 1;
            var source = NdArray<int>.Arange(HostDevice.Instance, 0, 12, 1).Reshape(new[] { 4, 3 });

            // action
            var diagMat = NdArray<int>.DiagMatAxis(axis1, axis2, source);

            // assert
            CollectionAssert.AreEqual(new[] { 4, 4, 3 }, diagMat.Shape);
        }

        [TestMethod]
        public void DiagMat()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 0, 3, 1);

            // action
            var diagMat = NdArray<int>.DiagMat(source);

            // assert
            CollectionAssert.AreEqual(new[] { 3, 3 }, diagMat.Shape);
        }

        [TestMethod]
        public void DiffAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var result = NdArray<int>.DiffAxis(1, source);

            // assert
            CollectionAssert.AreEqual(new[] { 3, 2 }, result.Shape);
        }

        [TestMethod]
        public void Transpos()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 0, 4, 1).Reshape(new[] { 2, 2 });

            // action
            var result = NdArray<int>.Transpos(source);

            // assert
            Assert.AreEqual(0, result[new[] { 0, 0 }]);
            Assert.AreEqual(1, result[new[] { 1, 0 }]);
            Assert.AreEqual(2, result[new[] { 0, 1 }]);
            Assert.AreEqual(3, result[new[] { 1, 1 }]);
        }

        [TestMethod]
        public void AtLeastNd()
        {
            // arrange
            const int MinNumDim = 3;
            var source = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 2 });

            // action
            var output = NdArray<int>.AtLeastNd(MinNumDim, source);

            // assert
            CollectionAssert.AreEqual(new[] { 1, 2, 2 }, output.Shape);
        }

        [TestMethod]
        public void AtLeast1d()
        {
            // arrange
            const int DummyValue = 1;
            var source = NdArray<int>.Scalar(HostDevice.Instance, DummyValue);

            // action
            var output = NdArray<int>.AtLeast1d(source);

            // assert
            CollectionAssert.AreEqual(new[] { 1 }, output.Shape);
        }

        [TestMethod]
        public void AtLeast2d()
        {
            // arrange
            const int DummyValue = 1;
            var source = NdArray<int>.Scalar(HostDevice.Instance, DummyValue);

            // action
            var output = NdArray<int>.AtLeast2d(source);

            // assert
            CollectionAssert.AreEqual(new[] { 1, 1 }, output.Shape);
        }

        [TestMethod]
        public void AtLeast3d()
        {
            // arrange
            const int DummyValue = 1;
            var source = NdArray<int>.Scalar(HostDevice.Instance, DummyValue);

            // action
            var output = NdArray<int>.AtLeast3d(source);

            // assert
            CollectionAssert.AreEqual(new[] { 1, 1, 1 }, output.Shape);
        }

        [TestMethod]
        public void BroadCastDim()
        {
            // arrange
            var source = NdArray<int>.Zeros(HostDevice.Instance, new[] { 3, 1, 5 });

            // action
            var output = NdArray<int>.BroadCastDim(1, 9, source);

            // assert
            CollectionAssert.AreEqual(new[] { 3, 9, 5 }, output.Shape);
        }

        [TestMethod]
        public void BroadCastTo()
        {
            // arrange
            var source = NdArray<int>.Zeros(HostDevice.Instance, new[] { 1, 7, 1 });

            // action
            var output = NdArray<int>.BroadCastTo(new[] { 2, 7, 3 }, source);

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
            var (output1, output2) = NdArray<int>.BroadCastToSame(input1, input2);

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
            var (output1, output2, output3) = NdArray<int>.BroadCastToSame(input1, input2, input3);

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
            var outputs = NdArray<int>.BroadCastToSame(new[] { input1, input2, input3, input4 });

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
            var (output1, output2) = NdArray<int>.BroadCastToSameInDims(new[] { 0, 2 }, input1, input2);

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
            var (output1, output2, output3) = NdArray<int>.BroadCastToSameInDims(new[] { 0, 2 }, input1, input2, input3);

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
            var outputs = NdArray<int>.BroadCastToSameInDims(new[] { 0, 2 }, new[] { input1, input2, input3, input4 });

            // assert
            CollectionAssert.AreEqual(new[] { 2, 2, 5 }, outputs[0].Shape);
            CollectionAssert.AreEqual(new[] { 2, 3, 5 }, outputs[1].Shape);
            CollectionAssert.AreEqual(new[] { 2, 4, 5 }, outputs[2].Shape);
            CollectionAssert.AreEqual(new[] { 2, 5, 5 }, outputs[3].Shape);
        }

        [TestMethod]
        public void CutLeft()
        {
            // arrange
            var source = NdArray<int>.Zeros(HostDevice.Instance, new[] { 1, 2, 3 });

            // action
            var output = NdArray<int>.CutLeft(source);

            // assert
            CollectionAssert.AreEqual(new[] { 2, 3 }, output.Shape);
        }

        [TestMethod]
        public void CutRight()
        {
            // arrange
            var source = NdArray<int>.Zeros(HostDevice.Instance, new[] { 1, 2, 3 });

            // action
            var output = NdArray<int>.CutRight(source);

            // assert
            CollectionAssert.AreEqual(new[] { 1, 2 }, output.Shape);
        }

        [TestMethod]
        public void Flatten()
        {
            // arrange
            var source = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 3, 4 });

            // action
            var output = NdArray<int>.Flatten(source);

            // assert
            CollectionAssert.AreEqual(new[] { 24 }, output.Shape);
        }

        [TestMethod]
        public void InsertAxis()
        {
            // arrange
            var source = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 3, 4 });

            // action
            var output = NdArray<int>.InsertAxis(1, source);

            // assert
            CollectionAssert.AreEqual(new[] { 2, 1, 3, 4 }, output.Shape);
        }

        [TestMethod]
        public void IsBroadcasted_WithBroadCastedNdArray_ReturnTrue()
        {
            // arrange
            var source = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 1, 4 });
            var broadCasted = NdArray<int>.BroadCastDim(1, 2, source);

            // action
            var output = NdArray<int>.IsBroadcasted(broadCasted);

            // assert
            Assert.IsTrue(output);
        }

        [TestMethod]
        public void PadLeft()
        {
            // arrange
            var source = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 3, 4 });

            // action
            var output = NdArray<int>.PadLeft(source);

            // assert
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4 }, output.Shape);
        }

        [TestMethod]
        public void PadRight()
        {
            // arrange
            var source = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 3, 4 });

            // action
            var output = NdArray<int>.PadRight(source);

            // assert
            CollectionAssert.AreEqual(new[] { 2, 3, 4, 1 }, output.Shape);
        }

        [TestMethod]
        public void PadToSame_Two()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 4, 5 });
            var input2 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 3, 4, 5 });

            // action
            var (output1, output2) = NdArray<int>.PadToSame(input1, input2);

            // assert
            CollectionAssert.AreEqual(new[] { 1, 4, 5 }, output1.Shape);
            CollectionAssert.AreEqual(new[] { 3, 4, 5 }, output2.Shape);
        }

        [TestMethod]
        public void PadToSamee_Three()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 5 });
            var input2 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 4, 5 });
            var input3 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 3, 4, 5 });

            // action
            var (output1, output2, output3) = NdArray<int>.PadToSame(input1, input2, input3);

            // assert
            CollectionAssert.AreEqual(new[] { 1, 1, 5 }, output1.Shape);
            CollectionAssert.AreEqual(new[] { 1, 4, 5 }, output2.Shape);
            CollectionAssert.AreEqual(new[] { 3, 4, 5 }, output3.Shape);
        }

        [TestMethod]
        public void PadToSame_Many()
        {
            // arrange
            var input1 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 5 });
            var input2 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 4, 5 });
            var input3 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 3, 4, 5 });
            var input4 = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 3, 4, 5 });

            // action
            var outputs = NdArray<int>.PadToSame(new[] { input1, input2, input3, input4 });

            // assert
            CollectionAssert.AreEqual(new[] { 1, 1, 1, 5 }, outputs[0].Shape);
            CollectionAssert.AreEqual(new[] { 1, 1, 4, 5 }, outputs[1].Shape);
            CollectionAssert.AreEqual(new[] { 1, 3, 4, 5 }, outputs[2].Shape);
            CollectionAssert.AreEqual(new[] { 2, 3, 4, 5 }, outputs[3].Shape);
        }

        [TestMethod]
        public void PermuteAxes()
        {
            // arrange
            var source = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 3, 4, 5 });

            // action
            var output = NdArray<int>.PermuteAxes(new[] { 1, 0, 3, 2 }, source);

            // assert
            CollectionAssert.AreEqual(new[] { 3, 2, 5, 4 }, output.Shape);
        }

        [TestMethod]
        public void ReverseAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 0, 4, 1);

            // action
            var output = NdArray<int>.ReverseAxis(0, source);

            var s = output.ToString();

            // assert
            Assert.AreEqual(4, output.Shape[0]);
            Assert.AreEqual(-1, output.Layout.Stride[0]);
        }

        [TestMethod]
        public void SwapDim()
        {
            // arrange
            var source = NdArray<int>.Zeros(HostDevice.Instance, new[] { 2, 3, 4 });

            // action
            var output = NdArray<int>.SwapDim(0, 2, source);

            // assert
            CollectionAssert.AreEqual(new[] { 4, 3, 2 }, output.Shape);
        }

        [TestMethod]
        public void LogicalNegate()
        {
            // arrange
            var source = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 10 });

            // action
            var output = NdArray<bool>.Not(source);

            // assert
            Assert.IsTrue(NdArray<bool>.All(output));
        }

        [TestMethod]
        public void LogicalAnd()
        {
            // arrange
            var input1 = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 4 });
            var input2 = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 4 });

            // action
            var output = NdArray<bool>.And(input1, input2);

            // assert
            var expected = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 4 });
            var result = expected == output;
            Assert.IsTrue(NdArray<bool>.All(result));
        }

        [TestMethod]
        public void LogicalOr()
        {
            // arrange
            var input1 = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 4 });
            var input2 = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 4 });

            // action
            var output = NdArray<bool>.Or(input1, input2);

            // assert
            var expected = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 4 });
            var result = expected == output;
            Assert.IsTrue(NdArray<bool>.All(result));
        }

        [TestMethod]
        public void LogicalXor()
        {
            // arrange
            var input1 = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 2 });
            var input2 = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 2 });
            input2[0].Value = false;

            // action
            var output = NdArray<bool>.Xor(input1, input2);

            // assert
            Assert.AreEqual(false, output[0].Value);
            Assert.AreEqual(true, output[1].Value);
        }

        [TestMethod]
        public void AllIndex()
        {
            // arrange
            var array = NdArray<int>.Arange(HostDevice.Instance, 0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var output = NdArray<int>.AllIndex(array);

            // assert
            CollectionAssert.AreEqual(new[] { 0, 0 }, output[0]);
            CollectionAssert.AreEqual(new[] { 0, 1 }, output[1]);
            CollectionAssert.AreEqual(new[] { 0, 2 }, output[2]);
            CollectionAssert.AreEqual(new[] { 1, 0 }, output[3]);
            CollectionAssert.AreEqual(new[] { 1, 1 }, output[4]);
            CollectionAssert.AreEqual(new[] { 1, 2 }, output[5]);
            CollectionAssert.AreEqual(new[] { 2, 0 }, output[6]);
            CollectionAssert.AreEqual(new[] { 2, 1 }, output[7]);
            CollectionAssert.AreEqual(new[] { 2, 2 }, output[8]);
        }

        [TestMethod]
        public void AllElements()
        {
            // arrange
            var array = NdArray<int>.Arange(HostDevice.Instance, 0, 9, 1).Reshape(new[] { 3, 3 });

            // action
            var output = NdArray<int>.AllElements(array);

            // assert
            CollectionAssert.AreEqual(Enumerable.Range(0, 9).ToArray(), output);
        }

        [TestMethod]
        public void AllAxis()
        {
            // arrange
            var source = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = false;

            // action
            var output0 = NdArray<bool>.AllAxis(0, source);
            var output1 = NdArray<bool>.AllAxis(1, source);

            // assert
            Assert.AreEqual(true, output0[0].Value);
            Assert.AreEqual(true, output1[1].Value);
        }

        [TestMethod]
        public void AllNdArray()
        {
            // arrange
            var source = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = false;

            // action
            var output = NdArray<bool>.AllNdArray(source);

            // assert
            Assert.AreEqual(false, output[0].Value);
        }

        [TestMethod]
        public void All()
        {
            // arrange
            var source = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = false;

            // action
            var output = NdArray<bool>.All(source);

            // assert
            Assert.AreEqual(false, output);
        }

        [TestMethod]
        public void AnyAxis()
        {
            // arrange
            var source = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = true;

            // action
            var output0 = NdArray<bool>.AnyAxis(0, source);
            var output1 = NdArray<bool>.AnyAxis(1, source);

            // assert
            Assert.AreEqual(true, output0[1].Value);
            Assert.AreEqual(false, output1[1].Value);
        }

        [TestMethod]
        public void AnyNdArray()
        {
            // arrange
            var source = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = true;

            // action
            var output = NdArray<bool>.AnyNdArray(source);

            // assert
            Assert.AreEqual(true, output[0].Value);
        }

        [TestMethod]
        public void Any()
        {
            // arrange
            var source = NdArray<bool>.Zeros(HostDevice.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = true;

            // action
            var output = NdArray<bool>.Any(source);

            // assert
            Assert.AreEqual(true, output);
        }

        [TestMethod]
        public void CountTrueAxis()
        {
            // arrange
            var source = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = false;
            source[new[] { 1, 0 }] = false;
            source[new[] { 1, 3 }] = false;

            // action
            var output0 = NdArray<bool>.CountTrueAxis(0, source);
            var output1 = NdArray<bool>.CountTrueAxis(1, source);

            // assert
            Assert.AreEqual(1, output0[0].Value);
            Assert.AreEqual(2, output1[1].Value);
        }

        [TestMethod]
        public void CountTrueNdArray()
        {
            // arrange
            var source = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = false;
            source[new[] { 1, 0 }] = false;
            source[new[] { 1, 3 }] = false;

            // action
            var output = NdArray<bool>.CountTrueNdArray(source);

            // assert
            Assert.AreEqual(5, output[0].Value);
        }

        [TestMethod]
        public void CountTrue()
        {
            // arrange
            var source = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 2, 4 });
            source[new[] { 0, 1 }] = false;
            source[new[] { 1, 0 }] = false;
            source[new[] { 1, 3 }] = false;

            // action
            var output = NdArray<bool>.CountTrue(source);

            // assert
            Assert.AreEqual(5, output);
        }

        [TestMethod]
        public void IfThenElse()
        {
            // arrange
            var condition = NdArray<bool>.Ones(HostDevice.Instance, new int[] { 4 });
            var ifTrue = NdArray<int>.Ones(HostDevice.Instance, new int[] { 4 });
            var ifFalse = NdArray<int>.Zeros(HostDevice.Instance, new int[] { 4 });

            condition[new[] { 0 }] = false;
            condition[new[] { 2 }] = false;

            // action
            var output = NdArray<int>.IfThenElse(condition, ifTrue, ifFalse);

            // assert
            Assert.AreEqual(0, output[0].Value);
            Assert.AreEqual(1, output[1].Value);
            Assert.AreEqual(0, output[2].Value);
            Assert.AreEqual(1, output[3].Value);
        }

        [TestMethod]
        public void ArgMaxAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = NdArray<int>.ArgMaxAxis(1, source);

            // assert
            Assert.AreEqual(3, output[0].Value);
            Assert.AreEqual(3, output[1].Value);
        }

        [TestMethod]
        public void ArgMax()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = NdArray<int>.ArgMax(source);

            // assert
            CollectionAssert.AreEqual(new[] { 1, 3 }, output);
        }

        [TestMethod]
        public void ArgMinAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = NdArray<int>.ArgMinAxis(1, source);

            // assert
            Assert.AreEqual(0, output[0].Value);
            Assert.AreEqual(0, output[1].Value);
        }

        [TestMethod]
        public void ArgMin()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = NdArray<int>.ArgMin(source);

            // assert
            CollectionAssert.AreEqual(new[] { 0, 0 }, output);
        }

        [TestMethod]
        public void FindAxis()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 0, 8, 1).Reshape(new[] { 2, 4 });

            // action
            var output = NdArray<int>.FindAxis(2, 1, source);

            // assert
            Assert.AreEqual(2, output[0].Value);
            Assert.AreEqual(SpecialIdx.NotFound, output[1].Value);
        }

        [TestMethod]
        public void TryFind()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });
            source[new[] { 1, 3 }] = 2;

            // action
            var output = NdArray<int>.TryFind(2, source);

            // assert
            CollectionAssert.AreEqual(new[] { 0, 1 }, output);
        }

        [TestMethod]
        public void TryFind_NotFound()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = NdArray<int>.TryFind(10, source);

            // assert
            Assert.IsTrue(output.Length == 0);
        }

        [TestMethod]
        public void Find()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });
            source[new[] { 1, 3 }] = 2;

            // action
            var output = NdArray<int>.Find(2, source);

            // assert
            CollectionAssert.AreEqual(new[] { 0, 1 }, output);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Find_NotFound_ThrowException()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 1, 9, 1).Reshape(new[] { 2, 4 });

            // action
            var output = NdArray<int>.Find(10, source);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PrepareAxisReduceSources_WrongShape_ThrowException()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 0, 8, 1).Reshape(new[] { 2, 4 });
            var target = NdArray<int>.Arange(HostDevice.Instance, 0, 4, 1).Reshape(new[] { 2, 2 });

            // action
            var output = NdArray<int>.PrepareAxisReduceSources(target, 1, source);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AssertBool_NotBool_ThrowException()
        {
            // arrange
            var source = NdArray<int>.Arange(HostDevice.Instance, 0, 8, 1).Reshape(new[] { 2, 4 });

            // action
            var _ = source.AssertBool();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AssertInt_NotInt_ThrowException()
        {
            // arrange
            var source = NdArray<uint>.Arange(HostDevice.Instance, 0, 8, 1).Reshape(new[] { 2, 4 });

            // action
            var _ = source.AssertBool();
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