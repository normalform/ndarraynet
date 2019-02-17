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

        private delegate void UnaryDelegate<T>(DataAndLayout<T> trgt, DataAndLayout<T> src);

        private delegate void BinaryDelegate<T>(DataAndLayout<T> trgt, DataAndLayout<T> src1, DataAndLayout<T> src2);

        private delegate int VectorCountDelegate<T>();

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

        public static bool AlignedWith<T>(DataAndLayout<T> src1, DataAndLayout<T> src2)
        {
            var vectorCount = VectorCount<T>();

            // Need to compute data alignment to avoid unwanted divied by zero with un-alinged data with the vector-SIMD- divided operation.
            var lastShape = Math.Min(src1.FastAccess.Shape.Last(), src2.FastAccess.Shape.Last());

            if ((lastShape >= vectorCount) && (lastShape % vectorCount) == 0)
            {
                return true;
            }

            return false;
        }

        public static void Fill<T>(T value, DataAndLayout<T> trgt)
        {
            Method<FillDelegate<T>>("FillImpl").Invoke(value, trgt);
        }

        public static void Copy<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            Method<CopyDelegate<T>>("CopyImpl").Invoke(trgt, src);
        }

        public static void Add<T>(DataAndLayout<T> trgt, DataAndLayout<T> src1, DataAndLayout<T> src2)
        {
            Method<BinaryDelegate<T>>("AddImpl").Invoke(trgt, src1, src2);
        }

        public static void Subtract<T>(DataAndLayout<T> trgt, DataAndLayout<T> src1, DataAndLayout<T> src2)
        {
            Method<BinaryDelegate<T>>("SubtractImpl").Invoke(trgt, src1, src2);
        }

        public static void Multiply<T>(DataAndLayout<T> trgt, DataAndLayout<T> src1, DataAndLayout<T> src2)
        {
            Method<BinaryDelegate<T>>("MultiplyImpl").Invoke(trgt, src1, src2);
        }

        public static void Divide<T>(DataAndLayout<T> trgt, DataAndLayout<T> src1, DataAndLayout<T> src2)
        {
            Method<BinaryDelegate<T>>("DivideImpl").Invoke(trgt, src1, src2);
        }

        public static void Abs<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            Method<UnaryDelegate<T>>("AbsImpl").Invoke(trgt, src);
        }

        public static void Sqrt<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            Method<UnaryDelegate<T>>("SqrtImpl").Invoke(trgt, src);
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

        private static int VectorCount<T>()
        {
            return Method<VectorCountDelegate<T>>("VectorCountImpl").Invoke();
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
            UnaryDataAndLayouts<T, T1> unaryDataAndLayouts,
            int vectorCount,
            UnaryAddessBases unaryAddessBases)
                where T : struct
                where T1 : struct
        {
            var targetAddress = unaryAddessBases.TargetAddress;
            var sourceAddress = unaryAddessBases.SourceAddress;
            var sourceBuffer = new T1[vectorCount];

            var vectorIters = shape[lastDimensionIndex] / vectorCount;
            for (var vecIter = 0; vecIter < vectorIters; vecIter++)
            {
                var targetVec = vectorOp(new Vector<T1>(unaryDataAndLayouts.Source.Data, sourceAddress));
                targetVec.CopyTo(unaryDataAndLayouts.Target.Data, targetAddress);
                targetAddress += vectorCount;
                sourceAddress += vectorCount;
            }

            var restElems = shape[lastDimensionIndex] % vectorCount;
            for (var restPos = 0; restPos < restElems; restPos++)
            {
                sourceBuffer[restPos] = unaryDataAndLayouts.Source.Data[sourceAddress];
                sourceAddress++;
            }

            var restTrgtVec = vectorOp(new Vector<T1>(sourceBuffer));
            for (var restPos = 0; restPos < restElems; restPos++)
            {
                unaryDataAndLayouts.Target.Data[targetAddress] = restTrgtVec[restPos];
                targetAddress++;
            }
        }

        private static void Stride10InnerLoop<T, T1>(
            Func<Vector<T1>, Vector<T>> vectorOp,
            int[] shape,
            int lastDimensionIndex,
            UnaryDataAndLayouts<T, T1> unaryDataAndLayouts,
            int vectorCount,
            UnaryAddessBases unaryAddessBases)
                where T : struct
                where T1 : struct
        {
            var targetAddress = unaryAddessBases.TargetAddress;
            var vectorIters = shape[lastDimensionIndex] / vectorCount;
            var sourceVector = new Vector<T1>(unaryDataAndLayouts.Source.Data[unaryAddessBases.SourceAddress]);
            var targetVector = vectorOp(sourceVector);

            for (var vecIter = 0; vecIter < vectorIters; vecIter++)
            {
                targetVector.CopyTo(unaryDataAndLayouts.Target.Data, targetAddress);
                targetAddress += vectorCount;
            }

            var restElems = shape[lastDimensionIndex] % vectorCount;
            for (var restPos = 0; restPos < restElems; restPos++)
            {
                unaryDataAndLayouts.Target.Data[targetAddress] = targetVector[restPos];
                targetAddress++;
            }
        }

        private static void ApplyUnary<T, T1>(Func<Vector<T1>, Vector<T>> vectorOp, UnaryDataAndLayouts<T, T1> unaryDataAndLayouts)
            where T : struct
            where T1 : struct
        {
            Debug.Assert(Vector<T>.Count == Vector<T1>.Count, "Vector sizes should be matched");

            var lastDimensionIndex = unaryDataAndLayouts.Target.FastAccess.NumDiensions - 1;
            var shape = unaryDataAndLayouts.Target.FastAccess.Shape;
            var vectorCount = Vector<T>.Count;

            var targetIter = new PosIter(unaryDataAndLayouts.Target.FastAccess, toDim: lastDimensionIndex - 1);
            var srcIter = new PosIter(unaryDataAndLayouts.Source.FastAccess, toDim: lastDimensionIndex - 1);

            while (targetIter.Active)
            {
                var targetStride = unaryDataAndLayouts.Target.FastAccess.Stride[lastDimensionIndex];
                var sourceStride = unaryDataAndLayouts.Source.FastAccess.Stride[lastDimensionIndex];

                var unaryAddessBases = new UnaryAddessBases(targetIter.Addr, srcIter.Addr);
                if (targetStride == 1 && sourceStride == 1)
                {
                    Stride11InnerLoop(vectorOp, shape, lastDimensionIndex, unaryDataAndLayouts, vectorCount, unaryAddessBases);
                }
                else if (targetStride == 1 && sourceStride == 0)
                {
                    Stride10InnerLoop(vectorOp, shape, lastDimensionIndex, unaryDataAndLayouts, vectorCount, unaryAddessBases);
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
            BinaryDataAndLayouts<T, T1, T2> binaryDataLaouts,
            int vectorCount,
            BinaryAddessBases binaryAddessBases)
                where T : struct
                where T1 : struct
                where T2 : struct
        {
            var targetAddress = binaryAddessBases.TargetAddress;
            var sourceAddress1 = binaryAddessBases.SourceAddress1;
            var sourceAddress2 = binaryAddessBases.SourceAddress2;
            var sourceBuffer1 = new T1[vectorCount];
            var sourceBuffer2 = new T2[vectorCount];

            var vectorIters = shape[lastDimensionIndex] / vectorCount;
            for (var vecIter = 0; vecIter < vectorIters; vecIter++)
            {
                var targetVec = vectorOp(new Vector<T1>(binaryDataLaouts.Source1.Data, sourceAddress1), new Vector<T2>(binaryDataLaouts.Source2.Data, sourceAddress2));
                targetVec.CopyTo(binaryDataLaouts.Target.Data, targetAddress);
                targetAddress += vectorCount;
                sourceAddress1 += vectorCount;
                sourceAddress2 += vectorCount;
            }

            var restElements = shape[lastDimensionIndex] % vectorCount;
            for (var restPos = 0; restPos < restElements; restPos++)
            {
                sourceBuffer1[restPos] = binaryDataLaouts.Source1.Data[sourceAddress1];
                sourceBuffer2[restPos] = binaryDataLaouts.Source2.Data[sourceAddress2];
                sourceAddress1++;
                sourceAddress2++;
            }

            if (restElements > 0)
            {
                var restTrgtVec = vectorOp(new Vector<T1>(sourceBuffer1), new Vector<T2>(sourceBuffer2));
                for (var restPos = 0; restPos < restElements; restPos++)
                {
                    binaryDataLaouts.Target.Data[targetAddress] = restTrgtVec[restPos];
                    targetAddress++;
                }
            }
        }

        private static void Stride110InnerLoop<T, T1, T2>(
            Func<Vector<T1>, Vector<T2>, Vector<T>> vectorOp,
            int[] shape,
            int lastDimensionIndex,
            BinaryDataAndLayouts<T, T1, T2> binaryDataLaouts,
            int vectorCount,
            BinaryAddessBases binaryAddessBases)
                where T : struct
                where T1 : struct
                where T2 : struct
        {
            var targetAddress = binaryAddessBases.TargetAddress;
            var sourceAddress1 = binaryAddessBases.SourceAddress1;
            var sourceBuffer1 = new T1[vectorCount];
            var sourceVector2 = new Vector<T2>(binaryDataLaouts.Source2.Data[binaryAddessBases.SourceAddress2]);

            var vectorIters = shape[lastDimensionIndex] / vectorCount;

            for (var vecIter = 0; vecIter < vectorIters; vecIter++)
            {
                var targetVector = vectorOp(new Vector<T1>(binaryDataLaouts.Source1.Data, sourceAddress1), sourceVector2);
                targetVector.CopyTo(binaryDataLaouts.Target.Data, targetAddress);
                targetAddress += vectorCount;
                sourceAddress1 += vectorCount;
            }

            var restElems = shape[lastDimensionIndex] % vectorCount;
            for (var restPos = 0; restPos < restElems; restPos++)
            {
                sourceBuffer1[restPos] = binaryDataLaouts.Source1.Data[sourceAddress1];
                sourceAddress1++;
            }

            var trgtVec2 = vectorOp(new Vector<T1>(sourceBuffer1), sourceVector2);
            for (var restPos = 0; restPos < restElems; restPos++)
            {
                binaryDataLaouts.Target.Data[targetAddress] = trgtVec2[restPos];
                targetAddress++;
            }
        }

        private static void Stride101InnerLoop<T, T1, T2>(
            Func<Vector<T1>, Vector<T2>, Vector<T>> vectorOp,
            int[] shape,
            int lastDimensionIndex,
            BinaryDataAndLayouts<T, T1, T2> binaryDataLaouts,
            int vectorCount,
            BinaryAddessBases binaryAddessBases)
                where T : struct
                where T1 : struct
                where T2 : struct
        {
            var targetAddr = binaryAddessBases.TargetAddress;
            var sourceAddress2 = binaryAddessBases.SourceAddress2;

            var sourceVector1 = new Vector<T1>(binaryDataLaouts.Source1.Data[binaryAddessBases.SourceAddress1]);
            var sourceBuffer2 = new T2[vectorCount];
            var vectorIters = shape[lastDimensionIndex] / vectorCount;

            for (var vecIter = 0; vecIter < vectorIters; vecIter++)
            {
                var targetVector = vectorOp(sourceVector1, new Vector<T2>(binaryDataLaouts.Source2.Data, sourceAddress2));
                targetVector.CopyTo(binaryDataLaouts.Target.Data, targetAddr);
                targetAddr += vectorCount;
                sourceAddress2 += vectorCount;
            }

            var restElements = shape[lastDimensionIndex] % vectorCount;
            for (var restPos = 0; restPos < restElements; restPos++)
            {
                sourceBuffer2[restPos] = binaryDataLaouts.Source2.Data[sourceAddress2];
                sourceAddress2 = sourceAddress2 + 1;
            }

            var trgtVec2 = vectorOp(sourceVector1, new Vector<T2>(sourceBuffer2));
            for (var restPos = 0; restPos < restElements; restPos++)
            {
                binaryDataLaouts.Target.Data[targetAddr] = trgtVec2[restPos];
                targetAddr = targetAddr + 1;
            }
        }

        private static void Stride100InnerLoop<T, T1, T2>(
            Func<Vector<T1>, Vector<T2>, Vector<T>> vectorOp,
            int[] shape,
            int lastDimensionIndex,
            BinaryDataAndLayouts<T, T1, T2> binaryDataLaouts,
            int vectorCount,
            BinaryAddessBases binaryAddessBases)
                where T : struct
                where T1 : struct
                where T2 : struct
        {
            var targetAddress = binaryAddessBases.TargetAddress;
            var vectorIters = shape[lastDimensionIndex] / vectorCount;
            var targetVector = vectorOp(new Vector<T1>(binaryDataLaouts.Source1.Data[binaryAddessBases.SourceAddress1]), new Vector<T2>(binaryDataLaouts.Source2.Data[binaryAddessBases.SourceAddress2]));

            for (var vecIter = 0; vecIter < vectorIters; vecIter++)
            {
                targetVector.CopyTo(binaryDataLaouts.Target.Data, targetAddress);
                targetAddress += vectorCount;
            }

            var resetElements = shape[lastDimensionIndex] % vectorCount;
            for (var restPos = 0; restPos < resetElements; restPos++)
            {
                binaryDataLaouts.Target.Data[targetAddress] = targetVector[restPos];
                targetAddress++;
            }
        }

        private static void ApplyBinary<T, T1, T2>(
            Func<Vector<T1>, Vector<T2>, Vector<T>> vectorOp,
            BinaryDataAndLayouts<T, T1, T2> binaryDataLaouts)
                where T : struct
                where T1 : struct
                where T2 : struct
        {
            Debug.Assert(Vector<T>.Count == Vector<T1>.Count && Vector<T1>.Count == Vector<T2>.Count, "Vector sizes should be matched");

            var lastDimensionIndex = binaryDataLaouts.Target.FastAccess.NumDiensions - 1;
            var shape = binaryDataLaouts.Target.FastAccess.Shape;
            var vectorCount = Vector<T>.Count;

            var targetIter = new PosIter(binaryDataLaouts.Target.FastAccess, toDim: lastDimensionIndex - 1);
            var sourceIter1 = new PosIter(binaryDataLaouts.Source1.FastAccess, toDim: lastDimensionIndex - 1);
            var sourceIter2 = new PosIter(binaryDataLaouts.Source2.FastAccess, toDim: lastDimensionIndex - 1);

            while (targetIter.Active)
            {
                var targetStride = binaryDataLaouts.Target.FastAccess.Stride[lastDimensionIndex];
                var sourceStride1 = binaryDataLaouts.Source1.FastAccess.Stride[lastDimensionIndex];
                var sourceStride2 = binaryDataLaouts.Source2.FastAccess.Stride[lastDimensionIndex];
                var binaryAddessBases = new BinaryAddessBases(targetIter.Addr, sourceIter1.Addr, sourceIter2.Addr);

                if (targetStride == 1 && sourceStride1 == 1 && sourceStride2 == 1)
                {
                    Stride111InnerLoop(vectorOp, shape, lastDimensionIndex, binaryDataLaouts, vectorCount, binaryAddessBases);
                }
                else if (targetStride == 1 && sourceStride1 == 1 && sourceStride2 == 0)
                {
                    Stride110InnerLoop(vectorOp, shape, lastDimensionIndex, binaryDataLaouts, vectorCount, binaryAddessBases);
                }
                else if (targetStride == 1 && sourceStride1 == 0 && sourceStride2 == 1)
                {
                    Stride101InnerLoop(vectorOp, shape, lastDimensionIndex, binaryDataLaouts, vectorCount, binaryAddessBases);
                }
                else if (targetStride == 1 && sourceStride1 == 0 && sourceStride2 == 0)
                {
                    Stride100InnerLoop(vectorOp, shape, lastDimensionIndex, binaryDataLaouts, vectorCount, binaryAddessBases);
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
            ApplyUnary(op, new UnaryDataAndLayouts<T, T>(trgt, src));
        }

        private static void AddImpl<T>(DataAndLayout<T> target, DataAndLayout<T> source1, DataAndLayout<T> source2) where T : struct
        {
            Vector<T> op(Vector<T> a, Vector<T> b) => Vector.Add(a, b);

            ApplyBinary(op, new BinaryDataAndLayouts<T, T, T>(target, source1, source2));
        }

        private static void SubtractImpl<T>(DataAndLayout<T> target, DataAndLayout<T> source1, DataAndLayout<T> source2) where T : struct
        {
            Vector<T> op(Vector<T> a, Vector<T> b) => Vector.Subtract(a, b);

            ApplyBinary(op, new BinaryDataAndLayouts<T, T, T>(target, source1, source2));
        }

        private static void MultiplyImpl<T>(DataAndLayout<T> target, DataAndLayout<T> source1, DataAndLayout<T> source2) where T : struct
        {
            Vector<T> op(Vector<T> a, Vector<T> b) => Vector.Multiply(a, b);

            ApplyBinary(op, new BinaryDataAndLayouts<T, T, T>(target, source1, source2));
        }

        private static void DivideImpl<T>(DataAndLayout<T> target, DataAndLayout<T> source1, DataAndLayout<T> source2) where T : struct
        {
            Vector<T> op(Vector<T> a, Vector<T> b) => Vector.Divide(a, b);

            ApplyBinary(op, new BinaryDataAndLayouts<T, T, T>(target, source1, source2));
        }

        private static void AbsImpl<T>(DataAndLayout<T> target, DataAndLayout<T> source) where T : struct
        {
            Vector<T> op(Vector<T> a) => Vector.Abs(a);

            ApplyUnary(op, new UnaryDataAndLayouts<T, T>(target, source));
        }

        private static void SqrtImpl<T>(DataAndLayout<T> target, DataAndLayout<T> source) where T : struct
        {
            Vector<T> op(Vector<T> a) => Vector.SquareRoot(a);

            ApplyUnary(op, new UnaryDataAndLayouts<T, T>(target, source));
        }

        private static int VectorCountImpl<T>() where T : struct
        {
            return Vector<T>.Count;
        }

        private class UnaryDataAndLayouts<T, T1>
        {
            public UnaryDataAndLayouts(DataAndLayout<T> target, DataAndLayout<T1> source)
            {
                Target = target;
                Source = source;
            }

            public DataAndLayout<T> Target { get; }

            public DataAndLayout<T1> Source { get; }
        }

        private class UnaryAddessBases
        {
            public UnaryAddessBases(int targetAddress, int sourceAddress)
            {
                TargetAddress = targetAddress;
                SourceAddress = sourceAddress;
            }

            public int TargetAddress { get; }

            public int SourceAddress { get; }
        }

        private class BinaryDataAndLayouts<T, T1, T2>
        {
            public BinaryDataAndLayouts(DataAndLayout<T> target, DataAndLayout<T1> source1, DataAndLayout<T2> source2)
            {
                Target = target;
                Source1 = source1;
                Source2 = source2;
            }

            public DataAndLayout<T> Target { get; }

            public DataAndLayout<T1> Source1 { get; }

            public DataAndLayout<T2> Source2 { get; }
        }

        private class BinaryAddessBases
        {
            public BinaryAddessBases(int targetAddress, int sourceAddress1, int sourceAddress2)
            {
                TargetAddress = targetAddress;
                SourceAddress1 = sourceAddress1;
                SourceAddress2 = sourceAddress2;
            }

            public int TargetAddress { get; }

            public int SourceAddress1 { get; }

            public int SourceAddress2 { get; }
        }
    }
}