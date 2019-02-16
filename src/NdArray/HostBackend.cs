// <copyright file="HostBackend.cs" company="NdArrayNet">
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

    /// <summary>
    /// Backend for host <see cref="NdArray{T}"/>.
    /// </summary>
    /// <typeparam name="T">The generic type parameter.</typeparam>
    internal class HostBackend<T> : IBackend<T>
    {
        private readonly T[] data;

        public HostBackend(Layout layout, IHostStorage<T> storage)
        {
            FastAccess = new FastAccess(layout);
            data = storage.Data;
        }

        public FastAccess FastAccess { get; }

        public DataAndLayout<T> DataLayout => new DataAndLayout<T>(data, FastAccess);

        public T this[int[] index]
        {
            get
            {
                var addr = FastAccess.Addr(index);
                return data[addr];
            }

            set
            {
                var addr = FastAccess.Addr(index);
                data[addr] = value;
            }
        }

        /// <summary>
        /// gets layouts for specified targets and sources, optimized for an element-wise operation
        /// </summary>
        /// <param name="trgt"></param>
        /// <param name="src"></param>
        public static (Layout, Layout[]) ElemwiseLayouts(Layout trgt, Layout[] srcs)
        {
            var dimGood = Enumerable.Range(0, trgt.NumDimensions)
                                    .Select(idx => trgt.Stride[idx] == 1 && srcs.All(src => src.Stride[idx] == 1 || src.Stride[idx] == 0));

            if (dimGood.Any())
            {
                var dims = Enumerable.Range(0, trgt.NumDimensions)
                                       .Select(d => dimGood.ElementAt(d) ? trgt.Shape[d] : -1);
                var bestLastDim = dims.ToList().IndexOf(dims.Max());

                var swapedTrgt = Layout.Swap(bestLastDim, trgt.NumDimensions - 1, trgt);
                var swapedSrcs = srcs.Select(src => Layout.Swap(bestLastDim, trgt.NumDimensions - 1, src));

                return (swapedTrgt, swapedSrcs.ToArray());
            }

            return (trgt, srcs);
        }

        public static DataAndLayout<T> ElemwiseDataAndLayout(IFrontend<T> trgt)
        {
            var (tl, ls) = ElemwiseLayouts(trgt.Layout, new Layout[] { });
            return trgt.Relayout(tl).Backend.DataLayout;
        }

        public static (DataAndLayout<T>, DataAndLayout<TA>) ElemwiseDataAndLayout<TA>(IFrontend<T> trgt, IFrontend<TA> src)
        {
            var (tl, ls) = ElemwiseLayouts(trgt.Layout, new Layout[] { src.Layout });
            return (trgt.Relayout(tl).Backend.DataLayout, src.Relayout(ls[0]).Backend.DataLayout);
        }

        public static (DataAndLayout<T>, DataAndLayout<TA>, DataAndLayout<TB>) ElemwiseDataAndLayout<TA, TB>(IFrontend<T> trgt, IFrontend<TA> a, IFrontend<TB> b)
        {
            var (tl, ls) = ElemwiseLayouts(trgt.Layout, new Layout[] { a.Layout, b.Layout });
            return (trgt.Relayout(tl).Backend.DataLayout, a.Relayout(ls[0]).Backend.DataLayout, b.Relayout(ls[1]).Backend.DataLayout);
        }

        public void Multiply(IFrontend<T> trgt, IFrontend<T> a, IFrontend<T> b)
        {
            var (dataLayoutTrgt, dataLayoutA, dataLayoutB) = ElemwiseDataAndLayout(trgt, a, b);
            if (VectorOps.CanUse(dataLayoutTrgt, dataLayoutA, dataLayoutB))
            {
                VectorOps.Multiply(dataLayoutTrgt, dataLayoutA, dataLayoutB);
            }
            else
            {
                ScalarOps.Multiply(dataLayoutTrgt, dataLayoutA, dataLayoutB);
            }
        }

        public void FillIncrementing(T start, T step, IFrontend<T> trgt)
        {
            var dataAndLayout = ElemwiseDataAndLayout(trgt);
            ScalarOps.FillIncrementing(start, step, dataAndLayout);
        }

        public void FillConst(T value, IFrontend<T> trgt)
        {
            var dataAndLayout = ElemwiseDataAndLayout(trgt);
            if (VectorOps.CanUse(dataAndLayout))
            {
                VectorOps.Fill(value, dataAndLayout);
            }
            else
            {
                ScalarOps.Fill(value, dataAndLayout);
            }
        }

        public void Copy(IFrontend<T> trgt, IFrontend<T> src)
        {
            if (Layout.HasContiguousMemory(trgt.Layout) &&
                Layout.HasContiguousMemory(src.Layout) &&
                Enumerable.SequenceEqual(trgt.Layout.Stride, src.Layout.Stride))
            {
                // use array block copy for contiguous memory block
                var (t, s) = ElemwiseDataAndLayout(trgt, src);
                if (t.FastAccess.NumElements > 0)
                {
                    Array.Copy(s.Data, s.FastAccess.Offset, t.Data, t.FastAccess.Offset, t.FastAccess.NumElements);
                }
            }
            else
            {
                var (t, s) = ElemwiseDataAndLayout(trgt, src);
                if (VectorOps.CanUse(t, s))
                {
                    VectorOps.Copy(t, s);
                }
                else
                {
                    ScalarOps.Copy(t, s);
                }
            }
        }
    }
}