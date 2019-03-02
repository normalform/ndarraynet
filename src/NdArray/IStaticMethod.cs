// <copyright file="IStaticMethod.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    internal interface IStaticMethod
    {
        bool All(NdArray<bool> source);

        void AssertBool<T>(IFrontend<T> source);

        (NdArray<T1>, NdArray<T2>) BroadCastToSame<T1, T2>(IFrontend<T1> src1, IFrontend<T2> src2);

        IFrontend<TA> BroadCastTo<TA>(int[] shp, IFrontend<TA> frontend);

        (NdArray<TR>, NdArray<TA>) PrepareElemwise<TR, TA>(NdArray<TA> array, Order order);

        (IFrontend<TR>, IFrontend<TA>, IFrontend<TB>) PrepareElemwise<TR, TA, TB>(IFrontend<TA> arrayA, IFrontend<TB> arrayB, Order order);

        (NdArray<TR>, NdArray<TA>, NdArray<TB>, NdArray<TC>) PrepareElemwise<TR, TA, TB, TC>(NdArray<TA> arrayA, NdArray<TB> arrayB, NdArray<TC> arrayC, Order order);

        IFrontend<TA> PrepareElemwiseSources<TR, TA>(IFrontend<TR> target, IFrontend<TA> array);

        (IFrontend<TA>, IFrontend<TB>) PrepareElemwiseSources<TR, TA, TB>(IFrontend<TR> target, IFrontend<TA> arrayA, IFrontend<TB> arrayB);

        (IFrontend<TA>, IFrontend<TB>, IFrontend<TC>) PrepareElemwiseSources<TR, TA, TB, TC>(NdArray<TR> target, NdArray<TA> arrayA, NdArray<TB> arrayB, NdArray<TC> arrayC);

        void CheckAxis<TA>(int axis, NdArray<TA> array);

        void AssertSameStorage<T1>(NdArray<T1>[] arrays);

        NdArray<T> PermuteAxes<T>(int[] permut, NdArray<T> source);

        (NdArray<TA>, NdArray<TR>) PrepareAxisReduceSources<TR, TA>(NdArray<TR> target, int axis, NdArray<TA> array, NdArray<TR> initial, Order order);

        (NdArray<TR>, NdArray<TA>) PrepareAxisReduceTarget<TR, TA>(int axis, NdArray<TA> array, Order order);
    }
}