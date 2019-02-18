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

        public static (DataAndLayout<T1>, DataAndLayout<T2>) ElemwiseDataAndLayout<T1, T2>(IFrontend<T1> trgt, IFrontend<T2> src)
        {
            var (tl, ls) = ElemwiseLayouts(trgt.Layout, new Layout[] { src.Layout });
            return (trgt.Relayout(tl).Backend.DataLayout, src.Relayout(ls[0]).Backend.DataLayout);
        }

        public static (DataAndLayout<T>, DataAndLayout<TA>, DataAndLayout<TB>) ElemwiseDataAndLayout<TA, TB>(IFrontend<T> trgt, IFrontend<TA> a, IFrontend<TB> b)
        {
            var (tl, ls) = ElemwiseLayouts(trgt.Layout, new Layout[] { a.Layout, b.Layout });
            return (trgt.Relayout(tl).Backend.DataLayout, a.Relayout(ls[0]).Backend.DataLayout, b.Relayout(ls[1]).Backend.DataLayout);
        }

        public static (DataAndLayout<TR>, DataAndLayout<TA>, DataAndLayout<TB>) ElemwiseDataAndLayout<TR, TA, TB>(IFrontend<TR> trgt, IFrontend<TA> a, IFrontend<TB> b)
        {
            var (tl, ls) = ElemwiseLayouts(trgt.Layout, new Layout[] { a.Layout, b.Layout });
            return (trgt.Relayout(tl).Backend.DataLayout, a.Relayout(ls[0]).Backend.DataLayout, b.Relayout(ls[1]).Backend.DataLayout);
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

        public void UnaryPlus(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (t, s) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.UnaryPlus(t, s);
        }

        public void UnaryMinus(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (t, s) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.UnaryMinus(t, s);
        }

        public void Equal<TP>(IFrontend<bool> trgt, IFrontend<TP> src1, IFrontend<TP> src2)
        {
            var (dataLayoutTrgt, dataLayout1, dataLayout2) = ElemwiseDataAndLayout(trgt, src1, src2);
            ScalarOps.Equal(dataLayoutTrgt, dataLayout1, dataLayout2);
        }

        public void NotEqual<TP>(IFrontend<bool> trgt, IFrontend<TP> src1, IFrontend<TP> src2)
        {
            var (dataLayoutTrgt, dataLayout1, dataLayout2) = ElemwiseDataAndLayout(trgt, src1, src2);
            ScalarOps.NotEqual(dataLayoutTrgt, dataLayout1, dataLayout2);
        }

        public void Less<TP>(IFrontend<bool> trgt, IFrontend<TP> src1, IFrontend<TP> src2)
        {
            var (dataLayoutTrgt, dataLayout1, dataLayout2) = ElemwiseDataAndLayout(trgt, src1, src2);
            ScalarOps.Less(dataLayoutTrgt, dataLayout1, dataLayout2);
        }

        public void LessOrEqual<TP>(IFrontend<bool> trgt, IFrontend<TP> src1, IFrontend<TP> src2)
        {
            var (dataLayoutTrgt, dataLayout1, dataLayout2) = ElemwiseDataAndLayout(trgt, src1, src2);
            ScalarOps.LessOrEqual(dataLayoutTrgt, dataLayout1, dataLayout2);
        }

        public void Greater<TP>(IFrontend<bool> trgt, IFrontend<TP> src1, IFrontend<TP> src2)
        {
            var (dataLayoutTrgt, dataLayout1, dataLayout2) = ElemwiseDataAndLayout(trgt, src1, src2);
            ScalarOps.Greater(dataLayoutTrgt, dataLayout1, dataLayout2);
        }

        public void GreaterOrEqual<TP>(IFrontend<bool> trgt, IFrontend<TP> src1, IFrontend<TP> src2)
        {
            var (dataLayoutTrgt, dataLayout1, dataLayout2) = ElemwiseDataAndLayout(trgt, src1, src2);
            ScalarOps.GreaterOrEqual(dataLayoutTrgt, dataLayout1, dataLayout2);
        }

        public void Add(IFrontend<T> trgt, IFrontend<T> src1, IFrontend<T> src2)
        {
            var (dataLayoutTrgt, dataLayoutA, dataLayoutB) = ElemwiseDataAndLayout<T, T, T>(trgt, src1, src2);
            if (VectorOps.CanUse(dataLayoutTrgt, dataLayoutA, dataLayoutB))
            {
                VectorOps.Add(dataLayoutTrgt, dataLayoutA, dataLayoutB);
            }
            else
            {
                ScalarOps.Add(dataLayoutTrgt, dataLayoutA, dataLayoutB);
            }
        }

        public void Subtract(IFrontend<T> trgt, IFrontend<T> src1, IFrontend<T> src2)
        {
            var (dataLayoutTrgt, dataLayoutA, dataLayoutB) = ElemwiseDataAndLayout<T, T, T>(trgt, src1, src2);
            if (VectorOps.CanUse(dataLayoutTrgt, dataLayoutA, dataLayoutB))
            {
                VectorOps.Subtract(dataLayoutTrgt, dataLayoutA, dataLayoutB);
            }
            else
            {
                ScalarOps.Subtract(dataLayoutTrgt, dataLayoutA, dataLayoutB);
            }
        }

        public void Multiply(IFrontend<T> trgt, IFrontend<T> a, IFrontend<T> b)
        {
            var (dataLayoutTrgt, dataLayoutA, dataLayoutB) = ElemwiseDataAndLayout<T, T, T>(trgt, a, b);
            if (VectorOps.CanUse(dataLayoutTrgt, dataLayoutA, dataLayoutB))
            {
                VectorOps.Multiply(dataLayoutTrgt, dataLayoutA, dataLayoutB);
            }
            else
            {
                ScalarOps.Multiply(dataLayoutTrgt, dataLayoutA, dataLayoutB);
            }
        }

        public void Divide(IFrontend<T> trgt, IFrontend<T> a, IFrontend<T> b)
        {
            var (dataLayoutTrgt, dataLayoutA, dataLayoutB) = ElemwiseDataAndLayout<T, T, T>(trgt, a, b);

            if (VectorOps.CanUse(dataLayoutTrgt, dataLayoutA, dataLayoutB) && VectorOps.AlignedWith(dataLayoutA, dataLayoutB))
            {
                VectorOps.Divide(dataLayoutTrgt, dataLayoutA, dataLayoutB);
            }
            else
            {
                ScalarOps.Divide(dataLayoutTrgt, dataLayoutA, dataLayoutB);
            }
        }

        public void Modulo(IFrontend<T> trgt, IFrontend<T> a, IFrontend<T> b)
        {
            var (dataLayoutTrgt, dataLayoutA, dataLayoutB) = ElemwiseDataAndLayout<T, T, T>(trgt, a, b);
            ScalarOps.Modulo(dataLayoutTrgt, dataLayoutA, dataLayoutB);
        }

        public void Abs(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            if (VectorOps.CanUse(dataLayoutTrgt, dataLayout))
            {
                VectorOps.Abs(dataLayoutTrgt, dataLayout);
            }
            else
            {
                ScalarOps.Abs(dataLayoutTrgt, dataLayout);
            }
        }

        public void Acos(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Acos(dataLayoutTrgt, dataLayout);
        }

        public void Asin(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Asin(dataLayoutTrgt, dataLayout);
        }

        public void Atan(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Atan(dataLayoutTrgt, dataLayout);
        }

        public void Ceiling(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Ceiling(dataLayoutTrgt, dataLayout);
        }

        public void Cos(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Cos(dataLayoutTrgt, dataLayout);
        }

        public void Cosh(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Cosh(dataLayoutTrgt, dataLayout);
        }

        public void Exp(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Exp(dataLayoutTrgt, dataLayout);
        }

        public void Floor(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Floor(dataLayoutTrgt, dataLayout);
        }

        public void Log(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Log(dataLayoutTrgt, dataLayout);
        }

        public void Log10(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Log10(dataLayoutTrgt, dataLayout);
        }

        public void Maximum(IFrontend<T> trgt, IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (dataLayoutTrgt, dataLayoutA, dataLayoutB) = ElemwiseDataAndLayout<T, T, T>(trgt, lhs, rhs);

            if (VectorOps.CanUse(dataLayoutTrgt, dataLayoutA, dataLayoutB))
            {
                VectorOps.Maximum(dataLayoutTrgt, dataLayoutA, dataLayoutB);
            }
            else
            {
                ScalarOps.Maximum(dataLayoutTrgt, dataLayoutA, dataLayoutB);
            }
        }

        public void Minimum(IFrontend<T> trgt, IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (dataLayoutTrgt, dataLayoutA, dataLayoutB) = ElemwiseDataAndLayout<T, T, T>(trgt, lhs, rhs);

            if (VectorOps.CanUse(dataLayoutTrgt, dataLayoutA, dataLayoutB))
            {
                VectorOps.Minimum(dataLayoutTrgt, dataLayoutA, dataLayoutB);
            }
            else
            {
                ScalarOps.Minimum(dataLayoutTrgt, dataLayoutA, dataLayoutB);
            }
        }

        public void Pow(IFrontend<T> trgt, IFrontend<T> lhs, IFrontend<T> rhs)
        {
            var (dataLayoutTrgt, dataLayout1, dataLayout2) = ElemwiseDataAndLayout<T, T, T>(trgt, lhs, rhs);
            ScalarOps.Pow(dataLayoutTrgt, dataLayout1, dataLayout2);
        }

        public void Round(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Round(dataLayoutTrgt, dataLayout);
        }

        public void Sign(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Sign(dataLayoutTrgt, dataLayout);
        }

        public void Sin(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Sin(dataLayoutTrgt, dataLayout);
        }

        public void Sinh(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Sinh(dataLayoutTrgt, dataLayout);
        }

        public void Sqrt(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            if (VectorOps.CanUse(dataLayoutTrgt, dataLayout))
            {
                VectorOps.Sqrt(dataLayoutTrgt, dataLayout);
            }
            else
            {
                ScalarOps.Sqrt(dataLayoutTrgt, dataLayout);
            }
        }

        public void Tan(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Tan(dataLayoutTrgt, dataLayout);
        }

        public void Tanh(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Tanh(dataLayoutTrgt, dataLayout);
        }

        public void Truncate(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.Truncate(dataLayoutTrgt, dataLayout);
        }

        public (DataAndLayout<T1>, DataAndLayout<T2>) GetDataAndLayout<T1, T2>(IFrontend<T2> trgt, IFrontend<T2> src)
        {
            return (((NdArray<T1>)trgt).Backend.DataLayout, ((NdArray<T2>)src).Backend.DataLayout);
        }

        public void AllLastAxis(IFrontend<bool> trgt, IFrontend<bool> src)
        {
            var (dataLayoutTrgt, dataLayout) = GetDataAndLayout<bool, bool>(trgt, src);
            ScalarOps.AllLastAxis(dataLayoutTrgt, dataLayout);
        }

        public void AnyLastAxis(IFrontend<bool> trgt, IFrontend<bool> src)
        {
            var (dataLayoutTrgt, dataLayout) = GetDataAndLayout<bool, bool>(trgt, src);
            ScalarOps.AnyLastAxis(dataLayoutTrgt, dataLayout);
        }

        public void IsFinite<TP>(IFrontend<bool> trgt, IFrontend<TP> src)
        {
            var (dataLayoutTrgt, dataLayout) = ElemwiseDataAndLayout(trgt, src);
            ScalarOps.IsFinite<TP>(dataLayoutTrgt, dataLayout);
        }

        public void MaxLastAxis(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = GetDataAndLayout<T, T>(trgt, src);
            ScalarOps.MaxLastAxis(dataLayoutTrgt, dataLayout);
        }

        public void MinLastAxis(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = GetDataAndLayout<T, T>(trgt, src);
            ScalarOps.MinLastAxis(dataLayoutTrgt, dataLayout);
        }

        public void SumLastAxis(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = GetDataAndLayout<T, T>(trgt, src);
            ScalarOps.SumLastAxis(dataLayoutTrgt, dataLayout);
        }

        public void ProductLastAxis(IFrontend<T> trgt, IFrontend<T> src)
        {
            var (dataLayoutTrgt, dataLayout) = GetDataAndLayout<T, T>(trgt, src);
            ScalarOps.ProductLastAxis(dataLayoutTrgt, dataLayout);
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