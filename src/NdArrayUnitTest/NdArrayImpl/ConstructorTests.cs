// <copyright file="ConstructorTests.cs" company="NdArrayNet">
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
    using NdArray.NdArrayImpl;
    using NdArrayNet;
    using System;
    using Xunit;

    public class ConstructorTests
    {
        [Fact]
        public void Arange_Start0()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<int>.Arange(device, 0, 10, 1);

            // assert
            var expectedLayout = new Layout(new[] { 10 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(0, array[0].Value);
        }

        [Fact]
        public void Arange_Start1()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<int>.Arange(device, 1, 10, 1);

            // assert
            var expectedLayout = new Layout(new[] { 9 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(1, array[0].Value);
        }

        [Fact]
        public void Arange_Step1()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<int>.Arange(device, 0, 10, 1);

            // assert
            var expectedLayout = new Layout(new[] { 10 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(1, array[1].Value);
        }

        [Fact]
        public void Arange_Step2()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<int>.Arange(device, 0, 10, 2);

            // assert
            var expectedLayout = new Layout(new[] { 5 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(2, array[1].Value);
        }

        [Fact]
        public void Arange_Stop10()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<int>.Arange(device, 0, 10, 1);

            // assert
            var expectedLayout = new Layout(new[] { 10 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(9, array[9].Value);
        }

        [Fact]
        public void Arange_Stop11()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<int>.Arange(device, 0, 11, 1);

            // assert
            var expectedLayout = new Layout(new[] { 11 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(10, array[10].Value);
        }

        [Fact]
        public void Counting_Int()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<int>.Counting(device, 10);

            // assert
            var expectedLayout = new Layout(new[] { 10 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(0, array[0].Value);
            Assert.Equal(9, array[9].Value);
        }

        [Fact]
        public void Counting_Double()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<double>.Counting(device, 10);

            // assert
            var expectedLayout = new Layout(new[] { 10 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(0.0, array[0].Value);
            Assert.Equal(9.0, array[9].Value);
        }

        [Fact]
        public void Empty()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<int>.Empty(device, 3);

            // assert
            var expectedLayout = new Layout(new[] { 0, 0, 0 }, 0, new[] { 0, 0, 1 });
            Assert.Equal(expectedLayout, array.Layout);
        }

        [Fact]
        public void Falses()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<bool>.Falses(device, new[] { 1, 2, 3 });

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.False(array[new[] { 0, 0, 0 }]);
            Assert.False(array[new[] { 0, 1, 2 }]);
        }

        [Fact]
        public void Filled()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<int>.Filled(device, new[] { 1, 2, 3 }, 55);

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(55, array[new[] { 0, 0, 0 }]);
            Assert.Equal(55, array[new[] { 0, 1, 2 }]);
        }

        [Fact]
        public void Identity_3by3()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<int>.Identity(device, 3);

            // assert
            var expectedLayout = new Layout(new[] { 3, 3 }, 0, new[] { 3, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(1, array[new[] { 0, 0 }]);
            Assert.Equal(0, array[new[] { 0, 1 }]);
            Assert.Equal(0, array[new[] { 0, 2 }]);

            Assert.Equal(0, array[new[] { 1, 0 }]);
            Assert.Equal(1, array[new[] { 1, 1 }]);
            Assert.Equal(0, array[new[] { 1, 2 }]);

            Assert.Equal(0, array[new[] { 2, 0 }]);
            Assert.Equal(0, array[new[] { 2, 1 }]);
            Assert.Equal(1, array[new[] { 2, 2 }]);
        }

        [Fact]
        public void Ones()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<int>.Ones(device, new[] { 2, 2 });

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(1, array[new[] { 0, 0 }]);
            Assert.Equal(1, array[new[] { 0, 1 }]);
            Assert.Equal(1, array[new[] { 1, 0 }]);
            Assert.Equal(1, array[new[] { 1, 1 }]);
        }

        [Fact]
        public void OnesLike()
        {
            // arrange
            var device = HostDevice.Instance;
            var zeros = NdArray<int>.Zeros(device, new[] { 2, 2 });

            // action
            var array = Constructor<int>.OnesLike(zeros);

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(1, array[new[] { 0, 0 }]);
            Assert.Equal(1, array[new[] { 0, 1 }]);
            Assert.Equal(1, array[new[] { 1, 0 }]);
            Assert.Equal(1, array[new[] { 1, 1 }]);
        }

        [Fact]
        public void Linspace_Int()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<int>.Linspace(device, 0, 5, 5);

            // assert
            var expectedLayout = new Layout(new[] { 5 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(0, array[new[] { 0 }]);
            Assert.Equal(1, array[new[] { 1 }]);
            Assert.Equal(2, array[new[] { 2 }]);
            Assert.Equal(3, array[new[] { 3 }]);
            Assert.Equal(4, array[new[] { 4 }]);
        }

        [Fact]
        public void Linspace_Double()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<double>.Linspace(device, 1.0, 2.0, 5);

            // assert
            var expectedLayout = new Layout(new[] { 5 }, 0, new[] { 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(1.0, array[new[] { 0 }]);
            Assert.Equal(1.25, array[new[] { 1 }]);
            Assert.Equal(1.5, array[new[] { 2 }]);
            Assert.Equal(1.75, array[new[] { 3 }]);
            Assert.Equal(2.0, array[new[] { 4 }]);
        }

        [Fact]
        public void Linspace_InvalidNumElements_ThrowException()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var exception = Assert.Throws<ArgumentException>(() => Constructor<int>.Linspace(device, 0, 5, 1));
            Assert.Contains("Linspace requires at least two elements.", exception.Message);
        }

        [Fact]
        public void Scalar()
        {
            // arrange
            var device = HostDevice.Instance;
            const int Value = 9;

            // action
            var array = Constructor<int>.Scalar(device, Value);

            // asssert
            Assert.Equal(Value, array.Value);
        }

        [Fact]
        public void ScalarLike()
        {
            // arrange
            var device = HostDevice.Instance;
            var referenceArray = NdArray<int>.Arange(device, 0, 24, 1).Reshape(new[] { 2, 3, 4 });
            const int Value = 9;

            // action
            var array = Constructor<int>.ScalarLike(referenceArray, Value);

            // asssert
            Assert.Equal(Value, array.Value);
        }

        [Fact]
        public void Trues()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<bool>.Trues(device, new[] { 1, 2, 3 });

            // assert
            var expectedLayout = new Layout(new[] { 1, 2, 3 }, 0, new[] { 6, 3, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.True(array[new[] { 0, 0, 0 }]);
            Assert.True(array[new[] { 0, 1, 2 }]);
        }

        [Fact]
        public void Zeros()
        {
            // arrange
            var device = HostDevice.Instance;

            // action
            var array = Constructor<int>.Zeros(device, new[] { 2, 2 });

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(0, array[new[] { 0, 0 }]);
            Assert.Equal(0, array[new[] { 0, 1 }]);
            Assert.Equal(0, array[new[] { 1, 0 }]);
            Assert.Equal(0, array[new[] { 1, 1 }]);
        }

        [Fact]
        public void ZerosLike()
        {
            // arrange
            var device = HostDevice.Instance;
            var ones = NdArray<int>.Ones(device, new[] { 2, 2 });

            // action
            var array = Constructor<int>.ZerosLike(ones);

            // assert
            var expectedLayout = new Layout(new[] { 2, 2 }, 0, new[] { 2, 1 });
            Assert.Equal(expectedLayout, array.Layout);
            Assert.Equal(0, array[new[] { 0, 0 }]);
            Assert.Equal(0, array[new[] { 0, 1 }]);
            Assert.Equal(0, array[new[] { 1, 0 }]);
            Assert.Equal(0, array[new[] { 1, 1 }]);
        }
    }
}