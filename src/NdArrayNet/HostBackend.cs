//Copyright(c) 2019, Jaeho Kim
//All rights reserved.

//Redistribution and use in source and binary forms, with or without
//modification, are permitted provided that the following conditions are met:

//1. Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
//2. Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.

//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
//ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

//The views and conclusions contained in the software and documentation are those
//of the authors and should not be interpreted as representing official policies,
//either expressed or implied, of the NdArrayNet project.

namespace NdArrayNet
{
    using System.Linq;

    public class HostBackend<T> : IBackend<T>
    {
        private T[] Data;

        public HostBackend(Layout layout, HostStorage<T> storage)
        {
            this.FastLayout = new FastLayout(layout);
            this.Data = storage.Data;
        }

        public FastLayout FastLayout { get; }

        public T this[int[] index]
        {
            get
            {
                var addr = this.FastLayout.Addr(index);
                return (T)this.Data[addr];
            }

            set
            {
                var addr = this.FastLayout.Addr(index);
                this.Data[addr] = value;
            }
        }

        public DataAndLayout<T> DataLayout => new DataAndLayout<T>(this.Data, this.FastLayout);

        /// <summary>
        /// gets layouts for specified targets and sources, optimized for an element-wise operation
        /// </summary>
        /// <param name="trgt"></param>
        /// <param name="src"></param>
        public static (Layout, Layout[]) ElemwiseLayouts(Layout trgt, Layout[] srcs)
        {
            var dimGood = Enumerable.Range(0, trgt.NumDimensions)
                                    .Select(idx => trgt.Stride[idx] == 1 && srcs.All(src => src.Stride[idx] == 1 || src.Stride[idx] == 0));

            if(dimGood.Any(d => d == true))
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

        public void FillIncrementing(T start, T step, IFrontend<T> trgt)
        {
            var dataAndLayout = ElemwiseDataAndLayout(trgt);
            ScalarOps.FillIncrementing(start, step, dataAndLayout);
        }
    }
}
