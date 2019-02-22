// <copyright file="IBackend.cs" company="NdArrayNet">
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

namespace NdArrayNet
{
    internal interface IBackend<T>
    {
        DataAndLayout<T> DataLayout { get; }

        T this[int[] index] { get; set; }

        void FillIncrementing(T start, T step, IFrontend<T> trgt);

        void FillConst(T value, IFrontend<T> trgt);

        void Copy(IFrontend<T> trgt, IFrontend<T> src);

        void UnaryPlus(IFrontend<T> trgt, IFrontend<T> src);

        void UnaryMinus(IFrontend<T> trgt, IFrontend<T> src);

        void Equal<TP>(IFrontend<bool> trgt, IFrontend<TP> src1, IFrontend<TP> src2);

        void NotEqual<TP>(IFrontend<bool> trgt, IFrontend<TP> src1, IFrontend<TP> src2);

        void Less<TP>(IFrontend<bool> trgt, IFrontend<TP> src1, IFrontend<TP> src2);

        void LessOrEqual<TP>(IFrontend<bool> trgt, IFrontend<TP> src1, IFrontend<TP> src2);

        void Greater<TP>(IFrontend<bool> trgt, IFrontend<TP> src1, IFrontend<TP> src2);

        void GreaterOrEqual<TP>(IFrontend<bool> trgt, IFrontend<TP> src1, IFrontend<TP> src2);

        void Add(IFrontend<T> trgt, IFrontend<T> src1, IFrontend<T> src2);

        void Subtract(IFrontend<T> trgt, IFrontend<T> src1, IFrontend<T> src2);

        void Multiply(IFrontend<T> trgt, IFrontend<T> a, IFrontend<T> b);

        void Divide(IFrontend<T> trgt, IFrontend<T> a, IFrontend<T> b);

        void Modulo(IFrontend<T> trgt, IFrontend<T> a, IFrontend<T> b);

        void Abs(IFrontend<T> trgt, IFrontend<T> src);

        void Acos(IFrontend<T> trgt, IFrontend<T> src);

        void Asin(IFrontend<T> trgt, IFrontend<T> src);

        void Atan(IFrontend<T> trgt, IFrontend<T> src);

        void Ceiling(IFrontend<T> trgt, IFrontend<T> src);

        void Cos(IFrontend<T> trgt, IFrontend<T> src);

        void Cosh(IFrontend<T> trgt, IFrontend<T> src);

        void Exp(IFrontend<T> trgt, IFrontend<T> src);

        void Floor(IFrontend<T> trgt, IFrontend<T> src);

        void Log(IFrontend<T> trgt, IFrontend<T> src);

        void Log10(IFrontend<T> trgt, IFrontend<T> src);

        void Maximum(IFrontend<T> trgt, IFrontend<T> lhs, IFrontend<T> rhs);

        void Minimum(IFrontend<T> trgt, IFrontend<T> lhs, IFrontend<T> rhs);

        void Pow(IFrontend<T> trgt, IFrontend<T> lhs, IFrontend<T> rhs);

        void Round(IFrontend<T> trgt, IFrontend<T> src);

        void Sign(IFrontend<T> trgt, IFrontend<T> src);

        void Sin(IFrontend<T> trgt, IFrontend<T> src);

        void Sinh(IFrontend<T> trgt, IFrontend<T> src);

        void Sqrt(IFrontend<T> trgt, IFrontend<T> src);

        void Tan(IFrontend<T> trgt, IFrontend<T> src);

        void Tanh(IFrontend<T> trgt, IFrontend<T> src);

        void Truncate(IFrontend<T> trgt, IFrontend<T> src);

        void AllLastAxis(IFrontend<bool> trgt, IFrontend<bool> src);

        void AnyLastAxis(IFrontend<bool> trgt, IFrontend<bool> src);

        void IsFinite<TP>(IFrontend<bool> trgt, IFrontend<TP> src);

        void MaxLastAxis(IFrontend<T> trgt, IFrontend<T> src);

        void MinLastAxis(IFrontend<T> trgt, IFrontend<T> src);

        void SumLastAxis(IFrontend<T> trgt, IFrontend<T> src);

        void ProductLastAxis(IFrontend<T> trgt, IFrontend<T> src);

        void Negate(IFrontend<bool> trgt, IFrontend<bool> src);

        void And(IFrontend<bool> trgt, IFrontend<bool> lhs, IFrontend<bool> rhs);

        void Or(IFrontend<bool> trgt, IFrontend<bool> lhs, IFrontend<bool> rhs);

        void Xor(IFrontend<bool> trgt, IFrontend<bool> lhs, IFrontend<bool> rhs);

        void CountTrueLastAxis(IFrontend<int> trgt, IFrontend<bool> src);

        void IfThenElse(IFrontend<T> trgt, IFrontend<bool> condition, IFrontend<T> ifTrue, IFrontend<T> ifFalse);

        void ArgMaxLastAxis<T1>(IFrontend<int> trgt, IFrontend<T1> src);

        void ArgMinLastAxis<T1>(IFrontend<int> trgt, IFrontend<T1> src);

        void FindLastAxis<T1>(T1 value, IFrontend<int> trgt, IFrontend<T1> src);

        void Convert<TC>(IFrontend<T> trgt, IFrontend<TC> src);
    }
}