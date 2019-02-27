// <copyright file="IBackend.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    public interface IBackend<T>
    {
        DataAndLayout<T> DataAndLayout { get; }

        T this[int[] index] { get; set; }

        void FillIncrementing(T start, T step, IFrontend<T> trgt);

        void FillConst(T value, IFrontend<T> trgt);

        void Copy(IFrontend<T> trgt, IFrontend<T> src);

        void UnaryPlus(IFrontend<T> trgt, IFrontend<T> src);

        void UnaryMinus(IFrontend<T> trgt, IFrontend<T> src);

        void Equal<T1>(IFrontend<bool> trgt, IFrontend<T1> src1, IFrontend<T1> src2);

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

        void TrueIndices(IFrontend<int> trgt, IFrontend<bool> src);

        void Convert<TC>(IFrontend<T> trgt, IFrontend<TC> src);
    }
}