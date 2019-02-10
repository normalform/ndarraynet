//Copyright(c) 2019, Jaeho Kim
//All rights reserved.

//Redistribution and use in source and binary forms, with or without
//modification, are permitted provided that the following conditions are met:

//1. Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
//2. Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.

//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
//ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

//The views and conclusions contained in the software and documentation are those
//of the authors and should not be interpreted as representing official policies,
//either expressed or implied, of the NdArrayNet project.

namespace ScalarPrimitivesUnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NdArrayNet;

    [TestClass]
    public class ScalarPrimitivesTests
    {
        [TestMethod]
        public void Add_int()
        {
            var sp = new ScalarPrimitives<int, int>();
            var left = 3;
            var right = 4;

            var result = sp.Add(left, right);

            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Add_double()
        {
            var sp = new ScalarPrimitives<double, double>();
            var left = 3.0;
            var right = 4.0;

            var result = sp.Add(left, right);

            Assert.AreEqual(7.0, result);
        }

        [TestMethod]
        public void Subtract_int()
        {
            var sp = new ScalarPrimitives<int, int>();
            var left = 3;
            var right = 4;

            var result = sp.Subtract(left, right);

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void Subtract_double()
        {
            var sp = new ScalarPrimitives<double, double>();
            var left = 3.0;
            var right = 4.0;

            var result = sp.Subtract(left, right);

            Assert.AreEqual(-1.0, result);
        }

        [TestMethod]
        public void Multiply_int()
        {
            var sp = new ScalarPrimitives<int, int>();
            var left = 3;
            var right = 4;

            var result = sp.Multiply(left, right);

            Assert.AreEqual(3 * 4, result);
        }

        [TestMethod]
        public void Multiply_double()
        {
            var sp = new ScalarPrimitives<double, double>();
            var left = 3.0;
            var right = 4.0;

            var result = sp.Multiply(left, right);

            Assert.AreEqual(3.0 * 4.0, result);
        }

        [TestMethod]
        public void Divide_int()
        {
            var sp = new ScalarPrimitives<int, int>();
            var left = 3;
            var right = 4;

            var result = sp.Divide(left, right);

            Assert.AreEqual(3/4, result);
        }

        [TestMethod]
        public void Divide_double()
        {
            var sp = new ScalarPrimitives<double, double>();
            var left = 3.0;
            var right = 4.0;

            var result = sp.Divide(left, right);

            Assert.AreEqual(3.0/4.0, result);
        }

        [TestMethod]
        public void Convert_IntToLong()
        {
            var sp = new ScalarPrimitives<long, int>();
            var result = sp.Convert(100);

            Assert.AreEqual(100L, result);
        }

        [TestMethod]
        public void Convert_DoubleToLong()
        {
            var sp = new ScalarPrimitives<long, double>();
            var result = sp.Convert(100.0);

            Assert.AreEqual(100L, result);
        }

        [TestMethod]
        public void For()
        {
            var op = ScalarPrimitives.For<int, int>();

            Assert.IsInstanceOfType(op, typeof(ScalarPrimitives<int, int>));
        }
    }
}