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

        private static void Stride11InnerLoop<T, T1>(
            Func<Vector<T1>, Vector<T>> vectorOp,
            int[] shape,
            int lastDimensionIndex,
            DataAndLayout<T> target,
            DataAndLayout<T1> source,
            int vectorCount,
            int targetAddressBase,
            int sourceAddressBase)
                where T : struct
                where T1 : struct
        {
            var targetAddress = targetAddressBase;
            var sourceAddress = sourceAddressBase;
            var sourceBuffer = new T1[vectorCount];

            var vectorIters = shape[lastDimensionIndex] / vectorCount;
            for (var vecIter = 0; vecIter < vectorIters; vecIter++)
            {
                var targetVec = vectorOp(new Vector<T1>(source.Data, sourceAddress));
                targetVec.CopyTo(target.Data, targetAddress);
                targetAddress += vectorCount;
                sourceAddress += vectorCount;
            }

            var restElems = shape[lastDimensionIndex] % vectorCount;
            for (var restPos = 0; restPos < restElems; restPos++)
            {
                sourceBuffer[restPos] = source.Data[sourceAddress];
                sourceAddress++;
            }

            var restTrgtVec = vectorOp(new Vector<T1>(sourceBuffer));
            for (var restPos = 0; restPos < restElems; restPos++)
            {
                target.Data[targetAddress] = restTrgtVec[restPos];
                targetAddress++;
            }
        }

        private static void Stride10InnerLoop<T, T1>(
            Func<Vector<T1>, Vector<T>> vectorOp,
            int[] shape,
            int lastDimensionIndex,
            DataAndLayout<T> target,
            DataAndLayout<T1> source,
            int vectorCount,
            int targetAddressBase,
            int sourceAddressBase)
                where T : struct
                where T1 : struct
        {
            var targetAddress = targetAddressBase;
            var vectorIters = shape[lastDimensionIndex] / vectorCount;
            var sourceVector = new Vector<T1>(source.Data[sourceAddressBase]);
            var targetVector = vectorOp(sourceVector);

            for (var vecIter = 0; vecIter < vectorIters; vecIter++)
            {
                targetVector.CopyTo(target.Data, targetAddress);
                targetAddress += vectorCount;
            }

            var restElems = shape[lastDimensionIndex] % vectorCount;
            for (var restPos = 0; restPos < restElems; restPos++)
            {
                target.Data[targetAddress] = targetVector[restPos];
                targetAddress++;
            }
        }

        private static void ApplyUnary<T, T1>(Func<Vector<T1>, Vector<T>> vectorOp, DataAndLayout<T> target, DataAndLayout<T1> source)
            where T : struct
            where T1 : struct
        {
            Debug.Assert(Vector<T>.Count == Vector<T1>.Count, "Vector sizes should be matched");

            var lastDimensionIndex = target.FastAccess.NumDiensions - 1;
            var shape = target.FastAccess.Shape;
            var vectorCount = Vector<T>.Count;

            var targetIter = new PosIter(target.FastAccess, toDim: lastDimensionIndex - 1);
            var srcIter = new PosIter(source.FastAccess, toDim: lastDimensionIndex - 1);

            while (targetIter.Active)
            {
                if (target.FastAccess.Stride[lastDimensionIndex] == 1 && source.FastAccess.Stride[lastDimensionIndex] == 1)
                {
                    Stride11InnerLoop(vectorOp, shape, lastDimensionIndex, target, source, vectorCount, targetIter.Addr, srcIter.Addr);
                }
                else if (target.FastAccess.Stride[lastDimensionIndex] == 1 && source.FastAccess.Stride[lastDimensionIndex] == 0)
                {
                    Stride10InnerLoop(vectorOp, shape, lastDimensionIndex, target, source, vectorCount, targetIter.Addr, srcIter.Addr);
                }
                else
                {
                    throw new InvalidOperationException("vector operation not applicable to the given NdArray");
                }

                targetIter.MoveNext();
                srcIter.MoveNext();
            }
        }

        private static void Stride111InnerLoop<T, T1, T2>(
            Func<Vector<T1>, Vector<T2>, Vector<T>> vectorOp,
            int[] shape,
            int lastDimensionIndex,
            DataAndLayout<T> target,
            DataAndLayout<T1> source1,
            DataAndLayout<T2> source2,
            int vectorCount,
            int targetAddressBase,
            int sourceAddressBase1,
            int sourceAddressBase2)
                where T : struct
                where T1 : struct
                where T2 : struct
        {
            var targetAddress = targetAddressBase;
            var sourceAddress1 = sourceAddressBase1;
            var sourceAddress2 = sourceAddressBase2;
            var sourceBuffer1 = new T1[vectorCount];
            var sourceBuffer2 = new T2[vectorCount];

            var vectorIters = shape[lastDimensionIndex] / vectorCount;
            for (var vecIter = 0; vecIter < vectorIters; vecIter++)
            {
                var targetVec = vectorOp(new Vector<T1>(source1.Data, sourceAddress1), new Vector<T2>(source2.Data, sourceAddress2));
                targetVec.CopyTo(target.Data, targetAddress);
                targetAddress += vectorCount;
                sourceAddress1 += vectorCount;
                sourceAddress2 += vectorCount;
            }

            var restElements = shape[lastDimensionIndex] % vectorCount;
            for (var restPos = 0; restPos < restElements; restPos++)
            {
                sourceBuffer1[restPos] = source1.Data[sourceAddress1];
                sourceBuffer2[restPos] = source2.Data[sourceAddress2];
                sourceAddress1++;
                sourceAddress2++;
            }

            var restTrgtVec = vectorOp(new Vector<T1>(sourceBuffer1), new Vector<T2>(sourceBuffer2));
            for (var restPos = 0; restPos < restElements; restPos++)
            {
                target.Data[targetAddress] = restTrgtVec[restPos];
                targetAddress++;
            }
        }

        private static void Stride110InnerLoop<T, T1, T2>(
            Func<Vector<T1>, Vector<T2>, Vector<T>> vectorOp,
            int[] shape,
            int lastDimensionIndex,
            DataAndLayout<T> target,
            DataAndLayout<T1> source1,
            DataAndLayout<T2> source2,
            int vectorCount,
            int targetAddressBase,
            int sourceAddressBase1,
            int sourceAddressBase2)
                where T : struct
                where T1 : struct
                where T2 : struct
        {
            var targetAddress = targetAddressBase;
            var sourceAddress1 = sourceAddressBase1;
            var sourceBuffer1 = new T1[vectorCount];
            var sourceVector2 = new Vector<T2>(source2.Data[sourceAddressBase2]);

            var vectorIters = shape[lastDimensionIndex] / vectorCount;

            for (var vecIter = 0; vecIter < vectorIters; vecIter++)
            {
                var targetVector = vectorOp(new Vector<T1>(source1.Data, sourceAddress1), sourceVector2);
                targetVector.CopyTo(target.Data, targetAddress);
                targetAddress += vectorCount;
                sourceAddress1 += vectorCount;
            }

            var restElems = shape[lastDimensionIndex] % vectorCount;
            for (var restPos = 0; restPos < restElems; restPos++)
            {
                sourceBuffer1[restPos] = source1.Data[sourceAddress1];
                sourceAddress1++;
            }

            var trgtVec2 = vectorOp(new Vector<T1>(sourceBuffer1), sourceVector2);
            for (var restPos = 0; restPos < restElems; restPos++)
            {
                target.Data[targetAddress] = trgtVec2[restPos];
                targetAddress++;
            }
        }

        private static void Stride101InnerLoop<T, T1, T2>(
            Func<Vector<T1>, Vector<T2>, Vector<T>> vectorOp,
            int[] shape,
            int lastDimensionIndex,
            DataAndLayout<T> target,
            DataAndLayout<T1> source1,
            DataAndLayout<T2> source2,
            int vectorCount,
            int targetAddressBase,
            int sourceAddressBase1,
            int sourceAddressBase2)
                where T : struct
                where T1 : struct
                where T2 : struct
        {
            var targetAddr = targetAddressBase;
            var sourceAddress2 = sourceAddressBase2;

            var sourceVector1 = new Vector<T1>(source1.Data[sourceAddressBase1]);
            var sourceBuffer2 = new T2[vectorCount];
            var vectorIters = shape[lastDimensionIndex] / vectorCount;

            for (var vecIter = 0; vecIter < vectorIters; vecIter++)
            {
                var targetVector = vectorOp(sourceVector1, new Vector<T2>(source2.Data, sourceAddress2));
                targetVector.CopyTo(target.Data, targetAddr);
                targetAddr += vectorCount;
                sourceAddress2 += vectorCount;
            }

            var restElements = shape[lastDimensionIndex] % vectorCount;
            for (var restPos = 0; restPos < restElements; restPos++)
            {
                sourceBuffer2[restPos] = source2.Data[sourceAddress2];
                sourceAddress2 = sourceAddress2 + 1;
            }

            var trgtVec2 = vectorOp(sourceVector1, new Vector<T2>(sourceBuffer2));
            for (var restPos = 0; restPos < restElements; restPos++)
            {
                target.Data[targetAddr] = trgtVec2[restPos];
                targetAddr = targetAddr + 1;
            }
        }

        private static void Stride100InnerLoop<T, T1, T2>(
            Func<Vector<T1>, Vector<T2>, Vector<T>> vectorOp,
            int[] shape,
            int lastDimensionIndex,
            DataAndLayout<T> target,
            DataAndLayout<T1> source1,
            DataAndLayout<T2> source2,
            int vectorCount,
            int targetAddressBase,
            int sourceAddressBase1,
            int sourceAddressBase2)
                where T : struct
                where T1 : struct
                where T2 : struct
        {
            var targetAddress = targetAddressBase;
            var vectorIters = shape[lastDimensionIndex] / vectorCount;
            var targetVector = vectorOp(new Vector<T1>(source1.Data[sourceAddressBase1]), new Vector<T2>(source2.Data[sourceAddressBase2]));

            for (var vecIter = 0; vecIter < vectorIters; vecIter++)
            {
                targetVector.CopyTo(target.Data, targetAddress);
                targetAddress += vectorCount;
            }

            var resetElements = shape[lastDimensionIndex] % vectorCount;
            for (var restPos = 0; restPos < resetElements; restPos++)
            {
                target.Data[targetAddress] = targetVector[restPos];
                targetAddress++;
            }
        }

        private static void ApplyBinary<T, T1, T2>(
            Func<Vector<T1>, Vector<T2>, Vector<T>> vectorOp,
            DataAndLayout<T> target,
            DataAndLayout<T1> source1,
            DataAndLayout<T2> source2)
                where T : struct
                where T1 : struct
                where T2 : struct
        {
            Debug.Assert(Vector<T>.Count == Vector<T1>.Count && Vector<T1>.Count == Vector<T2>.Count, "Vector sizes should be matched");

            var lastDimensionIndex = target.FastAccess.NumDiensions - 1;
            var shape = target.FastAccess.Shape;
            var vectorCount = Vector<T>.Count;
            var sourceBuffer1 = new T1[vectorCount];
            var sourceBuffer2 = new T2[vectorCount];

            var targetIter = new PosIter(target.FastAccess, toDim: lastDimensionIndex - 1);
            var sourceIter1 = new PosIter(source1.FastAccess, toDim: lastDimensionIndex - 1);
            var sourceIter2 = new PosIter(source2.FastAccess, toDim: lastDimensionIndex - 1);

            while (targetIter.Active)
            {
                var targetStride = target.FastAccess.Stride[lastDimensionIndex];
                var sourceStride1 = source1.FastAccess.Stride[lastDimensionIndex];
                var sourceStride2 = source2.FastAccess.Stride[lastDimensionIndex];

                if (targetStride == 1 && sourceStride1 == 1 && sourceStride2 == 1)
                {
                    Stride111InnerLoop(vectorOp, shape, lastDimensionIndex, target, source1, source2, vectorCount, targetIter.Addr, sourceIter1.Addr, sourceIter2.Addr);
                }
                else if (targetStride == 1 && sourceStride1 == 1 && sourceStride2 == 0)
                {
                    Stride110InnerLoop(vectorOp, shape, lastDimensionIndex, target, source1, source2, vectorCount, targetIter.Addr, sourceIter1.Addr, sourceIter2.Addr);
                }
                else if (targetStride == 1 && sourceStride1 == 0 && sourceStride2 == 1)
                {
                    Stride101InnerLoop(vectorOp, shape, lastDimensionIndex, target, source1, source2, vectorCount, targetIter.Addr, sourceIter1.Addr, sourceIter2.Addr);
                }
                else if (targetStride == 1 && sourceStride1 == 0 && sourceStride2 == 0)
                {
                    Stride100InnerLoop(vectorOp, shape, lastDimensionIndex, target, source1, source2, vectorCount, targetIter.Addr, sourceIter1.Addr, sourceIter2.Addr);
                }
                else
                {
                    throw new InvalidOperationException("vector operation not applicable to the given NdArray");
                }

                targetIter.MoveNext();
                sourceIter1.MoveNext();
                sourceIter2.MoveNext();
            }
        }

        private static void FillImpl<T>(T value, DataAndLayout<T> trgt)
            where T : struct
        {
            var nd = trgt.FastAccess.NumDiensions;
            var shape = trgt.FastAccess.Shape;
            var vectorCount = Vector<T>.Count;

            void vectorInnerLoop(int addr)
            {
                var trgtAddr = addr;
                var trgtVec = new Vector<T>(value);
                var vecIters = shape[nd - 1] / vectorCount;
                for (var vecIter = 0; vecIter < vecIters; vecIter++)
                {
                    trgtVec.CopyTo(trgt.Data, trgtAddr);
                    trgtAddr += vectorCount;
                }

                var restElems = shape[nd - 1] % vectorCount;
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