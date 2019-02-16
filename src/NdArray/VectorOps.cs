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
    using System.Diagnostics;
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

        private delegate void CopyDelegate<T>(DataAndLayout<T> trgt, DataAndLayout<T> src);

        private delegate void BinaryDelegate<T>(DataAndLayout<T> trgt, DataAndLayout<T> src1, DataAndLayout<T> src2);

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

        public static void Copy<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            Method<CopyDelegate<T>>("CopyImpl").Invoke(trgt, src);
        }

        public static void Multiply<T>(DataAndLayout<T> trgt, DataAndLayout<T> src1, DataAndLayout<T> src2)
        {
            Method<BinaryDelegate<T>>("MultiplyImpl").Invoke(trgt, src1, src2);
        }

        internal static bool CanUseSrc<T>(int numDim, DataAndLayout<T> src)
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

        private static void ApplyUnary<T, T1>(Func<Vector<T1>, Vector<T>> vectorOp, DataAndLayout<T> trgt, DataAndLayout<T1> src)
            where T : struct
            where T1 : struct
        {
            Debug.Assert(Vector<T>.Count == Vector<T1>.Count, "Vector sizes should be matched");

            var nd = trgt.FastAccess.NumDiensions;
            var shape = trgt.FastAccess.Shape;
            var srcBuf = new T1[Vector<T>.Count];

            void stride11InnerLoop(int tAddr, int sAddr)
            {
                var targetAddr = tAddr;
                var srcAddr = sAddr;

                var vecIters = shape[nd - 1] / Vector<T>.Count;
                for (var vecIter = 0; vecIter < vecIters; vecIter++)
                {
                    var trgtVec = vectorOp(new Vector<T1>(src.Data, srcAddr));
                    trgtVec.CopyTo(trgt.Data, targetAddr);
                    targetAddr = targetAddr + Vector<T>.Count;
                    srcAddr = srcAddr + Vector<T>.Count;
                }

                var restElems = shape[nd - 1] % Vector<T>.Count;
                for (var restPos = 0; restPos < restElems; restPos++)
                {
                    srcBuf[restPos] = src.Data[srcAddr];
                    srcAddr = srcAddr + 1;
                }

                var restTrgtVec = vectorOp(new Vector<T1>(srcBuf));
                for (var restPos = 0; restPos < restElems; restPos++)
                {
                    trgt.Data[targetAddr] = restTrgtVec[restPos];
                    targetAddr = targetAddr + 1;
                }
            }

            void stride10InnerLoop(int tAddr, int srcAddr)
            {
                var targetAddr = tAddr;
                var vecIters = shape[nd - 1] / Vector<T>.Count;
                var srcVec = new Vector<T1>(src.Data[srcAddr]);
                var trgtVec = vectorOp(srcVec);

                for (var vecIter = 0; vecIter < vecIters; vecIter++)
                {
                    trgtVec.CopyTo(trgt.Data, targetAddr);
                    targetAddr = targetAddr + Vector<T>.Count;
                }

                var restElems = shape[nd - 1] % Vector<T>.Count;
                for (var restPos = 0; restPos < restElems; restPos++)
                {
                    trgt.Data[targetAddr] = trgtVec[restPos];
                    targetAddr = targetAddr + 1;
                }
            }

            var targetPosItr = new PosIter(trgt.FastAccess, toDim: nd - 2);
            var srcPosItr = new PosIter(src.FastAccess, toDim: nd - 2);

            while (targetPosItr.Active)
            {
                if (trgt.FastAccess.Stride[nd - 1] == 1 && src.FastAccess.Stride[nd - 1] == 1)
                {
                    stride11InnerLoop(targetPosItr.Addr, srcPosItr.Addr);
                }
                else if (trgt.FastAccess.Stride[nd - 1] == 1 && src.FastAccess.Stride[nd - 1] == 0)
                {
                    stride10InnerLoop(targetPosItr.Addr, srcPosItr.Addr);
                }
                else
                {
                    throw new InvalidOperationException("vector operation not applicable to the given NdArray");
                }

                targetPosItr.MoveNext();
                srcPosItr.MoveNext();
            }
        }

        private static void ApplyBinary<T, T1, T2>(Func<Vector<T1>, Vector<T2>, Vector<T>> vectorOp, DataAndLayout<T> trgt, DataAndLayout<T1> src1, DataAndLayout<T2> src2)
            where T : struct
            where T1 : struct
            where T2 : struct
        {
            Debug.Assert(Vector<T>.Count == Vector<T1>.Count && Vector<T1>.Count == Vector<T2>.Count, "Vector sizes should be matched");

            var nd = trgt.FastAccess.NumDiensions;
            var shape = trgt.FastAccess.Shape;
            var vectorCount = Vector<T>.Count;
            var src1Buf = new T1[vectorCount];
            var src2Buf = new T2[vectorCount];

            void stride111InnerLoop(int tAddr, int s1Addr, int s2Addr)
            {
                var targetAddr = tAddr;
                var src1Addr = s1Addr;
                var src2Addr = s2Addr;

                var vecIters = shape[nd - 1] / vectorCount;
                for (var vecIter = 0; vecIter < vecIters; vecIter++)
                {
                    var trgtVec = vectorOp(new Vector<T1>(src1.Data, src1Addr), new Vector<T2>(src2.Data, src2Addr));
                    trgtVec.CopyTo(trgt.Data, targetAddr);
                    targetAddr = targetAddr + Vector<T>.Count;
                    src1Addr = src1Addr + Vector<T>.Count;
                    src2Addr = src2Addr + Vector<T>.Count;
                }

                var restElems = shape[nd - 1] % vectorCount;
                for (var restPos = 0; restPos < restElems; restPos++)
                {
                    src1Buf[restPos] = src1.Data[src1Addr];
                    src2Buf[restPos] = src2.Data[src2Addr];
                    src1Addr = src1Addr + 1;
                    src2Addr = src2Addr + 1;
                }

                var restTrgtVec = vectorOp(new Vector<T1>(src1Buf), new Vector<T2>(src2Buf));
                for (var restPos = 0; restPos < restElems; restPos++)
                {
                    trgt.Data[targetAddr] = restTrgtVec[restPos];
                    targetAddr = targetAddr + 1;
                }
            }

            void stride110InnerLoop(int tAddr, int s1Addr, int src2Addr)
            {
                var targetAddr = tAddr;
                var src1Addr = s1Addr;
                var vecIters = shape[nd - 1] / Vector<T>.Count;
                var src2Vec = new Vector<T2>(src2.Data[src2Addr]);

                for (var vecIter = 0; vecIter < vecIters; vecIter++)
                {
                    var trgtVec = vectorOp(new Vector<T1>(src1.Data, src1Addr), src2Vec);
                    trgtVec.CopyTo(trgt.Data, targetAddr);
                    targetAddr = targetAddr + Vector<T>.Count;
                    src1Addr = src1Addr + Vector<T>.Count;
                }

                var restElems = shape[nd - 1] % Vector<T>.Count;
                for (var restPos = 0; restPos < restElems; restPos++)
                {
                    src1Buf[restPos] = src1.Data[src1Addr];
                    src1Addr = src1Addr + 1;
                }

                var trgtVec2 = vectorOp(new Vector<T1>(src1Buf), src2Vec);
                for (var restPos = 0; restPos < restElems; restPos++)
                {
                    trgt.Data[targetAddr] = trgtVec2[restPos];
                    targetAddr = targetAddr + 1;
                }
            }

            void stride101InnerLoop(int tAddr, int src1Addr, int s2Addr)
            {
                var targetAddr = tAddr;
                var src2Addr = s2Addr;
                var vecIters = shape[nd - 1] / Vector<T>.Count;
                var src1Vec = new Vector<T1>(src1.Data[src1Addr]);

                for (var vecIter = 0; vecIter < vecIters; vecIter++)
                {
                    var trgtVec = vectorOp(src1Vec, new Vector<T2>(src2.Data, src2Addr));
                    trgtVec.CopyTo(trgt.Data, targetAddr);
                    targetAddr = targetAddr + Vector<T>.Count;
                    src2Addr = src2Addr + Vector<T>.Count;
                }

                var restElems = shape[nd - 1] % Vector<T>.Count;
                for (var restPos = 0; restPos < restElems; restPos++)
                {
                    src2Buf[restPos] = src2.Data[src2Addr];
                    src2Addr = src2Addr + 1;
                }

                var trgtVec2 = vectorOp(src1Vec, new Vector<T2>(src2Buf));
                for (var restPos = 0; restPos < restElems; restPos++)
                {
                    trgt.Data[targetAddr] = trgtVec2[restPos];
                    targetAddr = targetAddr + 1;
                }
            }

            void stride100InnerLoop(int tAddr, int src1Addr, int src2Addr)
            {
                var targetAddr = tAddr;
                var vecIters = shape[nd - 1] / Vector<T>.Count;
                var trgtVec = vectorOp(new Vector<T1>(src1.Data[src1Addr]), new Vector<T2>(src2.Data[src2Addr]));

                for (var vecIter = 0; vecIter < vecIters; vecIter++)
                {
                    trgtVec.CopyTo(trgt.Data, targetAddr);
                    targetAddr = targetAddr + Vector<T>.Count;
                }

                var restElems = shape[nd - 1] % Vector<T>.Count;
                for (var restPos = 0; restPos < restElems; restPos++)
                {
                    trgt.Data[targetAddr] = trgtVec[restPos];
                    targetAddr = targetAddr + 1;
                }
            }

            var targetPosItr = new PosIter(trgt.FastAccess, toDim: nd - 2);
            var src1PosItr = new PosIter(src1.FastAccess, toDim: nd - 2);
            var src2PosItr = new PosIter(src2.FastAccess, toDim: nd - 2);

            while (targetPosItr.Active)
            {
                if (trgt.FastAccess.Stride[nd - 1] == 1 && src1.FastAccess.Stride[nd - 1] == 1 && src2.FastAccess.Stride[nd - 1] == 1)
                {
                    stride111InnerLoop(targetPosItr.Addr, src1PosItr.Addr, src2PosItr.Addr);
                }
                else if (trgt.FastAccess.Stride[nd - 1] == 1 && src1.FastAccess.Stride[nd - 1] == 1 && src2.FastAccess.Stride[nd - 1] == 0)
                {
                    stride110InnerLoop(targetPosItr.Addr, src1PosItr.Addr, src2PosItr.Addr);
                }
                else if (trgt.FastAccess.Stride[nd - 1] == 1 && src1.FastAccess.Stride[nd - 1] == 0 && src2.FastAccess.Stride[nd - 1] == 1)
                {
                    stride101InnerLoop(targetPosItr.Addr, src1PosItr.Addr, src2PosItr.Addr);
                }
                else if (trgt.FastAccess.Stride[nd - 1] == 1 && src1.FastAccess.Stride[nd - 1] == 0 && src2.FastAccess.Stride[nd - 1] == 0)
                {
                    stride100InnerLoop(targetPosItr.Addr, src1PosItr.Addr, src2PosItr.Addr);
                }
                else
                {
                    throw new InvalidOperationException("vector operation not applicable to the given NdArray");
                }

                targetPosItr.MoveNext();
                src1PosItr.MoveNext();
                src2PosItr.MoveNext();
            }
        }

        private static void FillImpl<T>(T value, DataAndLayout<T> trgt)
            where T : struct
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

        private static void CopyImpl<T>(DataAndLayout<T> trgt, DataAndLayout<T> src) where T : struct
        {
            Vector<T> op(Vector<T> v) => v;
            ApplyUnary(op, trgt, src);
        }

        private static void MultiplyImpl<T>(DataAndLayout<T> trgt, DataAndLayout<T> src1, DataAndLayout<T> src2) where T : struct
        {
            Vector<T> op(Vector<T> a, Vector<T> b) => Vector.Multiply(a, b);
            ApplyBinary(op, trgt, src1, src2);
        }
    }
}