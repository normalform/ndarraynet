// <copyright file="NumPyConstructTests.cs" company="NdArrayNet">
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

namespace NdArrayNetUnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NdArrayNet;

    [TestClass]
    public class NumPyConstructTests
    {
        [TestMethod]
        public void Arange_IntTypeFullArgs_ReturnIntegerTypeNdArray()
        {
            // arrange & action
            var array = NumPy.Arange(0, 10, 1);

            // assert
            Assert.IsInstanceOfType(array, typeof(NdArray<int>));
        }

        [TestMethod]
        public void Arange_IntTypeStopArgOnly_ReturnIntegerTypeNdArray()
        {
            // arrange & action
            var array = NumPy.Arange(10);

            // assert
            Assert.IsInstanceOfType(array, typeof(NdArray<int>));
        }

        [TestMethod]
        public void Ones_IntTypeVector_ReturnIntegerTypeNdArray()
        {
            // arrange & action
            var array = NumPy.Ones<int>(new[] { 2, 3, 4, 5 });

            // assert
            Assert.IsInstanceOfType(array, typeof(NdArray<int>));
        }

        [TestMethod]
        public void OnesLike_IntTypeVector_ReturnIntegerTypeNdArray()
        {
            // arrange
            var template = NumPy.Ones<int>(new[] { 2, 3, 4, 5 });

            // action
            var array = NumPy.OnesLike(template);

            // assert
            Assert.IsInstanceOfType(array, typeof(NdArray<int>));
        }

        [TestMethod]
        public void Zeros_IntTypeVector_ReturnIntegerTypeNdArray()
        {
            // arrange & action
            var array = NumPy.Zeros<int>(new[] { 2, 3, 4, 5 });

            // assert
            Assert.IsInstanceOfType(array, typeof(NdArray<int>));
        }

        [TestMethod]
        public void ZerosLike_IntTypeVector_ReturnIntegerTypeNdArray()
        {
            // arrange
            var template = NumPy.Zeros<int>(new[] { 2, 3, 4, 5 });

            // action
            var array = NumPy.ZerosLike(template);

            // assert
            Assert.IsInstanceOfType(array, typeof(NdArray<int>));
        }

        [TestMethod]
        public void Arange_DoubleTypeFullArgs_ReturnDoubleTypeNdArray()
        {
            // arrange & action
            var array = NumPy.Arange(0.0, 10.0, 1.0);

            // assert
            Assert.IsInstanceOfType(array, typeof(NdArray<double>));
        }

        [TestMethod]
        public void Arange_DoubleTypeStopArgOnly_ReturnDoubleTypeNdArray()
        {
            // arrange & action
            var array = NumPy.Arange(10.0);

            // assert
            Assert.IsInstanceOfType(array, typeof(NdArray<double>));
        }

        [TestMethod]
        public void Ones_DoubleTypeVector_ReturnDoubleTypeNdArray()
        {
            // arrange & action
            var array = NumPy.Ones<double>(new[] { 2, 3, 4, 5 });

            // assert
            Assert.IsInstanceOfType(array, typeof(NdArray<double>));
        }

        [TestMethod]
        public void OnesLike_DoubleTypeVector_ReturnDoubleTypeNdArray()
        {
            // arrange
            var template = NumPy.Ones<double>(new[] { 2, 3, 4, 5 });

            // action
            var array = NumPy.OnesLike(template);

            // assert
            Assert.IsInstanceOfType(array, typeof(NdArray<double>));
        }

        [TestMethod]
        public void Zeros_DoubleTypeVector_ReturnDoubleTypeNdArray()
        {
            // arrange & action
            var array = NumPy.Zeros<double>(new[] { 2, 3, 4, 5 });

            // assert
            Assert.IsInstanceOfType(array, typeof(NdArray<double>));
        }

        [TestMethod]
        public void ZerosLike_DoubleTypeVector_ReturnDoubleTypeNdArray()
        {
            // arrange
            var template = NumPy.Zeros<double>(new[] { 2, 3, 4, 5 });

            // action
            var array = NumPy.ZerosLike(template);

            // assert
            Assert.IsInstanceOfType(array, typeof(NdArray<double>));
        }
    }
}