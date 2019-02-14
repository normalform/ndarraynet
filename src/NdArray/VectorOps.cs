// <copyright file="VectorOps.cs" company="NdArrayNet">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Reflection;

    /// <summary>
    /// Scalar operations on host NdArray.
    /// </summary>
    internal class VectorOps
    {
        private static readonly List<Type> VecTypes = new List<Type>
        {
            typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int),
            typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal)
        };

        private static Dictionary<(string, List<Type>), Delegate> methodDelegates = new Dictionary<(string, List<Type>), Delegate>();

        private delegate void FillDelegate<T>(T value, DataAndLayout<T> dataAndLayout);

        public static bool CanUse<T>(DataAndLayout<T> trgt, DataAndLayout<T> src1 = null, DataAndLayout<T> src2 = null)
        {
            var nd = trgt.FastAccess.NumDiensions;
            if (nd == 0)
            {
                return false;
            }

            var canUseType = VecTypes.Contains(typeof(T));
            var canUseTrgt = trgt.FastAccess.Stride[nd - 1] == 1;

            return canUseType && canUseTrgt && CanUseSrc(nd, src1) && CanUseSrc(nd, src2);
        }

        public static void Fill<T>(T value, DataAndLayout<T> trgt)
        {
            Method<FillDelegate<T>>("FillImpl").Invoke(value, trgt);
        }

        private static bool CanUseSrc<T>(int numDim, DataAndLayout<T> src)
        {
            if (src == null)
            {
                return true;
            }

            var lastStride = src.FastAccess.Stride[numDim - 1];
            return lastStride == 1 || lastStride == 0;
        }

        private static D Method<D>(string name) where D : Delegate
        {
            var dt = typeof(D).GenericTypeArguments;
            var dtl = dt.ToList();
            methodDelegates.TryGetValue((name, dtl), out Delegate del);
            if (del == null)
            {
                var mi = typeof(VectorOps).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(dt);
                var newDel = mi.CreateDelegate(typeof(D));
                methodDelegates[(name, dtl)] = newDel;
                return (D)newDel;
            }

            return (D)del;
        }

        private static void FillImpl<T>(T value, DataAndLayout<T> trgt) where T : struct
        {
            var nd = trgt.FastAccess.NumDiensions;
            var shape = trgt.FastAccess.Shape;

            void vectorInnerLoop(int addr)
            {
                var trgtAddr = addr;
                var trgtVec = new Vector<T>(value);
                var vecIters = shape[nd - 1] / Vector<T>.Count;
                for (var vecIter = 0; vecIter < vecIters; vecIter++)
                {
                    trgtVec.CopyTo(trgt.Data, trgtAddr);
                    trgtAddr += Vector<T>.Count;
                }

                var restElems = shape[nd - 1] % Vector<T>.Count;
                for (var restPos = 0; restPos < restElems; restPos++)
                {
                    trgt.Data[trgtAddr] = trgtVec[restPos];
                    trgtAddr++;
                }
            }

            var targetPosItr = new PosIter(trgt.FastAccess, toDim: nd - 2);
            while (targetPosItr.Active)
            {
                if (trgt.FastAccess.Stride[nd - 1] == 1)
                {
                    vectorInnerLoop(targetPosItr.Addr);
                }
                else
                {
                    throw new InvalidOperationException("vector operation not applicable to the given NdArray");
                }

                targetPosItr.MoveNext();
            }
        }
    }
}