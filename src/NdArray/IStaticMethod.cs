// <copyright file="IStaticMethod.cs" company="NdArrayNet">
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
    internal interface IStaticMethod
    {
        bool All(NdArray<bool> source);

        (NdArray<T1>, NdArray<T2>) BroadCastToSame<T1, T2>(NdArray<T1> src1, NdArray<T2> src2);

        IFrontend<TA> BroadCastTo<TA>(int[] shp, IFrontend<TA> frontend);

        (NdArray<TR>, NdArray<TA>) PrepareElemwise<TR, TA>(NdArray<TA> array, Order order);

        (NdArray<TR>, NdArray<TA>, NdArray<TB>) PrepareElemwise<TR, TA, TB>(NdArray<TA> arrayA, NdArray<TB> arrayB, Order order);

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