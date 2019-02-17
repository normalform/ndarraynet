﻿// <copyright file="ScalarOps.cs" company="NdArrayNet">
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
    using System.Threading.Tasks;

    /// <summary>
    /// Scalar operations on host NdArray.
    /// </summary>
    internal static class ScalarOps
    {
        public static void ApplyNoaryOp<T>(Func<int[], T> op, DataAndLayout<T> trgt, bool isIndexed, bool useThreads)
        {
            var nd = trgt.FastAccess.NumDiensions;
            var shape = trgt.FastAccess.Shape;

            Action<bool, int> loops = (bool dim0Fixed, int dim0Pos) =>
            {
                var fromDim = dim0Fixed ? 1 : 0;
                var startPos = new int[nd];
                if (dim0Fixed)
                {
                    startPos[0] = dim0Pos;
                }

                var targetPosItr = new PosIter(trgt.FastAccess, startPos, fromDim: fromDim, toDim: nd - 2);
                var pos = new int[targetPosItr.Pos.Length];

                while (targetPosItr.Active)
                {
                    var targetAddr = targetPosItr.Addr;
                    if (nd == 0)
                    {
                        trgt.Data[targetPosItr.Addr] = op(null);
                    }
                    else if (isIndexed)
                    {
                        for (var d = 0; d < nd; d++)
                        {
                            pos[d] = targetPosItr.Pos[d];
                        }

                        for (var i = 0; i < shape[nd - 1]; i++)
                        {
                            trgt.Data[targetAddr] = op(pos);
                            targetAddr = targetAddr + trgt.FastAccess.Stride[nd - 1];
                            pos[nd - 1] = pos[nd - 1] + 1;
                        }
                    }
                    else
                    {
                        for (var i = 0; i < shape[nd - 1]; i++)
                        {
                            trgt.Data[targetAddr] = op(null);
                            targetAddr = targetAddr + trgt.FastAccess.Stride[nd - 1];
                        }
                    }

                    targetPosItr.MoveNext();
                }
            };

            if (useThreads && nd > 1)
            {
                Parallel.For(0, shape[0], index => loops(true, index));
            }
            else
            {
                loops(false, 0);
            }
        }

        public static void ApplyUnaryOp<T, T1>(Func<int[], T1, T> op, DataAndLayout<T> trgt, DataAndLayout<T1> src, bool isIndexed, bool useThreads)
        {
            var nd = trgt.FastAccess.NumDiensions;
            var shape = trgt.FastAccess.Shape;

            Action<bool, int> loops = (bool dim0Fixed, int dim0Pos) =>
            {
                var fromDim = dim0Fixed ? 1 : 0;
                var startPos = new int[nd];
                if (dim0Fixed)
                {
                    startPos[0] = dim0Pos;
                }

                var targetPosItr = new PosIter(trgt.FastAccess, startPos, fromDim: fromDim, toDim: nd - 2);
                var srcPosItr = new PosIter(src.FastAccess, startPos, fromDim: fromDim, toDim: nd - 2);
                var pos = new int[targetPosItr.Pos.Length];

                while (targetPosItr.Active)
                {
                    var targetAddr = targetPosItr.Addr;
                    var srcAddr = srcPosItr.Addr;

                    if (nd == 0)
                    {
                        trgt.Data[targetPosItr.Addr] = op(null, src.Data[srcAddr]);
                    }
                    else if (isIndexed)
                    {
                        for (var d = 0; d < nd; d++)
                        {
                            pos[d] = targetPosItr.Pos[d];
                        }

                        for (var i = 0; i < shape[nd - 1]; i++)
                        {
                            trgt.Data[targetAddr] = op(pos, src.Data[srcAddr]);
                            targetAddr = targetAddr + trgt.FastAccess.Stride[nd - 1];
                            srcAddr = srcAddr + src.FastAccess.Stride[nd - 1];
                            pos[nd - 1] = pos[nd - 1] + 1;
                        }
                    }
                    else
                    {
                        for (var i = 0; i < shape[nd - 1]; i++)
                        {
                            trgt.Data[targetAddr] = op(null, src.Data[srcAddr]);
                            targetAddr = targetAddr + trgt.FastAccess.Stride[nd - 1];
                            srcAddr = srcAddr + src.FastAccess.Stride[nd - 1];
                        }
                    }

                    targetPosItr.MoveNext();
                    srcPosItr.MoveNext();
                }
            };

            if (useThreads && nd > 1)
            {
                Parallel.For(0, shape[0], index => loops(true, index));
            }
            else
            {
                loops(false, 0);
            }
        }

        public static void ApplyBinaryOp<T, T1, T2>(Func<int[], T1, T2, T> op, DataAndLayout<T> trgt, DataAndLayout<T1> src1, DataAndLayout<T2> src2, bool isIndexed, bool useThreads)
        {
            var nd = trgt.FastAccess.NumDiensions;
            var shape = trgt.FastAccess.Shape;

            Action<bool, int> loops = (bool dim0Fixed, int dim0Pos) =>
            {
                var fromDim = dim0Fixed ? 1 : 0;
                var startPos = new int[nd];
                if (dim0Fixed)
                {
                    startPos[0] = dim0Pos;
                }

                var targetPosItr = new PosIter(trgt.FastAccess, startPos, fromDim: fromDim, toDim: nd - 2);
                var src1PosItr = new PosIter(src1.FastAccess, startPos, fromDim: fromDim, toDim: nd - 2);
                var src2PosItr = new PosIter(src2.FastAccess, startPos, fromDim: fromDim, toDim: nd - 2);
                var pos = new int[targetPosItr.Pos.Length];

                while (targetPosItr.Active)
                {
                    var targetAddr = targetPosItr.Addr;
                    var src1Addr = src1PosItr.Addr;
                    var src2Addr = src2PosItr.Addr;

                    if (nd == 0)
                    {
                        trgt.Data[targetPosItr.Addr] = op(null, src1.Data[src1Addr], src2.Data[src2Addr]);
                    }
                    else if (isIndexed)
                    {
                        for (var d = 0; d < nd; d++)
                        {
                            pos[d] = targetPosItr.Pos[d];
                        }

                        for (var i = 0; i < shape[nd - 1]; i++)
                        {
                            trgt.Data[targetAddr] = op(pos, src1.Data[src1Addr], src2.Data[src2Addr]);
                            targetAddr = targetAddr + trgt.FastAccess.Stride[nd - 1];
                            src1Addr = src1Addr + src1.FastAccess.Stride[nd - 1];
                            src2Addr = src2Addr + src2.FastAccess.Stride[nd - 1];
                            pos[nd - 1] = pos[nd - 1] + 1;
                        }
                    }
                    else
                    {
                        for (var i = 0; i < shape[nd - 1]; i++)
                        {
                            trgt.Data[targetAddr] = op(null, src1.Data[src1Addr], src2.Data[src2Addr]);
                            targetAddr = targetAddr + trgt.FastAccess.Stride[nd - 1];
                            src1Addr = src1Addr + src1.FastAccess.Stride[nd - 1];
                            src2Addr = src2Addr + src2.FastAccess.Stride[nd - 1];
                        }
                    }

                    targetPosItr.MoveNext();
                    src1PosItr.MoveNext();
                    src2PosItr.MoveNext();
                }
            };

            if (useThreads && nd > 1)
            {
                Parallel.For(0, shape[0], index => loops(true, index));
            }
            else
            {
                loops(false, 0);
            }
        }

        public static void FillIncrementing<T>(T start, T step, DataAndLayout<T> trgt)
        {
            var p = ScalarPrimitives.For<T, int>();
            T op(int[] pos) => p.Add(start, p.Multiply(step, p.Convert(pos[0])));
            ApplyNoaryOp(op, trgt, isIndexed: true, useThreads: true);
        }

        public static void Fill<T>(T value, DataAndLayout<T> trgt)
        {
            T op(int[] pos) => value;
            ApplyNoaryOp(op, trgt, isIndexed: false, useThreads: true);
        }

        public static void Copy<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            T op(int[] pos, T value) => value;
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Add<T>(DataAndLayout<T> trgt, DataAndLayout<T> src1, DataAndLayout<T> src2)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a, T b) => p.Add(a, b);

            ApplyBinaryOp(op, trgt, src1, src2, isIndexed: false, useThreads: true);
        }

        public static void Subtract<T>(DataAndLayout<T> trgt, DataAndLayout<T> src1, DataAndLayout<T> src2)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a, T b) => p.Subtract(a, b);

            ApplyBinaryOp(op, trgt, src1, src2, isIndexed: false, useThreads: true);
        }

        public static void Multiply<T>(DataAndLayout<T> trgt, DataAndLayout<T> src1, DataAndLayout<T> src2)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a, T b) => p.Multiply(a, b);

            ApplyBinaryOp(op, trgt, src1, src2, isIndexed: false, useThreads: true);
        }

        public static void Divide<T>(DataAndLayout<T> trgt, DataAndLayout<T> src1, DataAndLayout<T> src2)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a, T b) => p.Divide(a, b);

            ApplyBinaryOp(op, trgt, src1, src2, isIndexed: false, useThreads: true);
        }

        public static void Modulo<T>(DataAndLayout<T> trgt, DataAndLayout<T> src1, DataAndLayout<T> src2)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a, T b) => p.Modulo(a, b);

            ApplyBinaryOp(op, trgt, src1, src2, isIndexed: false, useThreads: true);
        }

        public static void Equal<TP>(DataAndLayout<bool> trgt, DataAndLayout<TP> src1, DataAndLayout<TP> src2)
        {
            var p = ScalarPrimitives.For<TP, TP>();
            bool op(int[] pos, TP a, TP b) => p.Equal(a, b);

            ApplyBinaryOp(op, trgt, src1, src2, isIndexed: false, useThreads: true);
        }

        public static void NotEqual<TP>(DataAndLayout<bool> trgt, DataAndLayout<TP> src1, DataAndLayout<TP> src2)
        {
            var p = ScalarPrimitives.For<TP, TP>();
            bool op(int[] pos, TP a, TP b) => p.NotEqual(a, b);

            ApplyBinaryOp(op, trgt, src1, src2, isIndexed: false, useThreads: true);
        }

        public static void Less<TP>(DataAndLayout<bool> trgt, DataAndLayout<TP> src1, DataAndLayout<TP> src2)
        {
            var p = ScalarPrimitives.For<TP, TP>();
            bool op(int[] pos, TP a, TP b) => p.Less(a, b);

            ApplyBinaryOp(op, trgt, src1, src2, isIndexed: false, useThreads: true);
        }

        public static void LessOrEqual<TP>(DataAndLayout<bool> trgt, DataAndLayout<TP> src1, DataAndLayout<TP> src2)
        {
            var p = ScalarPrimitives.For<TP, TP>();
            bool op(int[] pos, TP a, TP b) => p.LessOrEqual(a, b);

            ApplyBinaryOp(op, trgt, src1, src2, isIndexed: false, useThreads: true);
        }

        public static void Greater<TP>(DataAndLayout<bool> trgt, DataAndLayout<TP> src1, DataAndLayout<TP> src2)
        {
            var p = ScalarPrimitives.For<TP, TP>();
            bool op(int[] pos, TP a, TP b) => p.Greater(a, b);

            ApplyBinaryOp(op, trgt, src1, src2, isIndexed: false, useThreads: true);
        }

        public static void GreaterOrEqual<TP>(DataAndLayout<bool> trgt, DataAndLayout<TP> src1, DataAndLayout<TP> src2)
        {
            var p = ScalarPrimitives.For<TP, TP>();
            bool op(int[] pos, TP a, TP b) => p.GreaterOrEqual(a, b);

            ApplyBinaryOp(op, trgt, src1, src2, isIndexed: false, useThreads: true);
        }

        public static void UnaryPlus<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.UnaryPlus(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void UnaryMinus<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.UnaryMinus(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Abs<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Abs(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Acos<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Acos(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Asin<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Asin(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Atan<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Atan(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Ceiling<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Ceiling(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Cos<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Cos(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Cosh<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Cosh(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Exp<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Exp(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Floor<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Floor(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Log<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Log(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Log10<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Log10(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Pow<T>(DataAndLayout<T> trgt, DataAndLayout<T> lhs, DataAndLayout<T> rhs)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T l, T t) => p.Power(l, t);
            ApplyBinaryOp(op, trgt, lhs, rhs, isIndexed: false, useThreads: true);
        }

        public static void Round<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Round(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Sign<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Sign(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Sin<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Sin(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Sinh<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Sinh(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Sqrt<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Sqrt(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Tan<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Tan(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Tanh<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Tanh(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }

        public static void Truncate<T>(DataAndLayout<T> trgt, DataAndLayout<T> src)
        {
            var p = ScalarPrimitives.For<T, T>();
            T op(int[] pos, T a) => p.Truncate(a);
            ApplyUnaryOp(op, trgt, src, isIndexed: false, useThreads: true);
        }
    }
}