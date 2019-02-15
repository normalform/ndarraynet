// <copyright file="ScalarOps.cs" company="NdArrayNet">
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
    }
}