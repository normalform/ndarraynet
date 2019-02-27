// <copyright file="StaticMethod.cs" company="NdArrayNet">
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
    using System;
    using System.Linq;
    using NdArray.NdFunction;

    internal class StaticMethod : IStaticMethod
    {
        public bool All(NdArray<bool> source)
        {
            return LogicalFunction<bool>.All(source);
        }

        public (NdArray<T1>, NdArray<T2>) BroadCastToSame<T1, T2>(NdArray<T1> src1, NdArray<T2> src2)
        {
            return ShapeFunction<T1>.BroadCastToSame(src1, src2);
        }

        public (NdArray<T1>, NdArray<T2>, NdArray<T3>) BroadCastToSame<T1, T2, T3>(NdArray<T1> src1, NdArray<T2> src2, NdArray<T3> src3)
        {
            return ShapeFunction<T1>.BroadCastToSame(src1, src2, src3);
        }

        public IFrontend<TA> BroadCastTo<TA>(int[] shp, IFrontend<TA> frontend)
        {
            return ShapeFunction<TA>.BroadCastTo(shp, frontend);
        }

        public (NdArray<TR>, NdArray<TA>) PrepareElemwise<TR, TA>(NdArray<TA> array, Order order)
        {
            var target = new NdArray<TR>(array.ConfigManager, array.Shape, order);
            return (target, array);
        }

        public (NdArray<TR>, NdArray<T1>, NdArray<T2>) PrepareElemwise<TR, T1, T2>(NdArray<T1> arrayA, NdArray<T2> arrayB, Order order)
        {
            // AssertSameStorage [later..]
            var (arrA, arrB) = BroadCastToSame(arrayA, arrayB);
            var target = new NdArray<TR>(arrA.ConfigManager, arrA.Shape, order);

            return (target, arrA, arrB);
        }

        public (NdArray<TR>, NdArray<TA>, NdArray<TB>, NdArray<TC>) PrepareElemwise<TR, TA, TB, TC>(NdArray<TA> arrayA, NdArray<TB> arrayB, NdArray<TC> arrayC, Order order)
        {
            // AssertSameStorage [later..]
            var (arrA, arrB, arrC) = BroadCastToSame(arrayA, arrayB, arrayC);
            var target = new NdArray<TR>(arrA.ConfigManager, arrA.Shape, order);

            return (target, arrA, arrB, arrC);
        }

        public IFrontend<TA> PrepareElemwiseSources<TR, TA>(IFrontend<TR> target, IFrontend<TA> array)
        {
            // AssertSameStorage [later..]
            return BroadCastTo(target.Shape, array);
        }

        public (IFrontend<TA>, IFrontend<TB>) PrepareElemwiseSources<TR, TA, TB>(IFrontend<TR> target, IFrontend<TA> arrayA, IFrontend<TB> arrayB)
        {
            // AssertSameStorage [later..]
            var arrA = BroadCastTo(target.Shape, arrayA);
            var arrB = BroadCastTo(target.Shape, arrayB);

            return (arrA, arrB);
        }

        public (IFrontend<TA>, IFrontend<TB>, IFrontend<TC>) PrepareElemwiseSources<TR, TA, TB, TC>(NdArray<TR> target, NdArray<TA> arrayA, NdArray<TB> arrayB, NdArray<TC> arrayC)
        {
            // AssertSameStorage [later..]
            var arrA = BroadCastTo(target.Shape, arrayA);
            var arrB = BroadCastTo(target.Shape, arrayB);
            var arrC = BroadCastTo(target.Shape, arrayC);

            return (arrA, arrB, arrC);
        }

        public void CheckAxis<TA>(int axis, NdArray<TA> array)
        {
            Layout.CheckAxis(array.Layout, axis);
        }

        public void AssertSameStorage<T1>(NdArray<T1>[] arrays)
        {
            // skip this for now because of it supports only one storage type for now.
        }

        public NdArray<T> PermuteAxes<T>(int[] permut, NdArray<T> source)
        {
            return ShapeFunction<T>.PermuteAxes(permut, source);
        }

        public (NdArray<TA>, NdArray<TR>) PrepareAxisReduceSources<TR, TA>(NdArray<TR> target, int axis, NdArray<TA> array, NdArray<TR> initial, Order order)
        {
            // AssertSameStorage. Later. Note might need to support the different TR and TA types.
            CheckAxis(axis, array);

            var reducedShaped = List.Without(axis, array.Shape);
            if (!Enumerable.SequenceEqual(target.Shape, reducedShaped))
            {
                var errorMessage = string.Format("Reduction of NdArray {0} along axis {1} gives shape {2} but target has shape {3}.", ErrorMessage.ShapeToString(array.Shape), axis, ErrorMessage.ShapeToString(reducedShaped), ErrorMessage.ShapeToString(target.Shape));
                throw new InvalidOperationException(errorMessage);
            }

            if (!(initial is null))
            {
                AssertSameStorage(new[] { target, initial });
                BroadCastTo(reducedShaped, initial);
            }

            var axisToLastTemp = Enumerable.Range(0, array.NumDimensions).ToList();
            axisToLastTemp.RemoveAt(axis);
            axisToLastTemp.Add(axis);

            var axisToLast = axisToLastTemp.ToArray();
            var newArray = PermuteAxes(axisToLast, array);
            if (!Enumerable.SequenceEqual(target.Shape, newArray.Shape.Take(newArray.NumDimensions - 1)))
            {
                throw new InvalidOperationException("Internal axis reduce shape computation error.");
            }

            return (newArray, initial);
        }

        public (NdArray<TR>, NdArray<TA>) PrepareAxisReduceTarget<TR, TA>(int axis, NdArray<TA> array, Order order)
        {
            CheckAxis(axis, array);
            var reducedShaped = List.Without(axis, array.Shape);
            var target = new NdArray<TR>(array.ConfigManager, reducedShaped, order);

            return (target, array);
        }
    }
}