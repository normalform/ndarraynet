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
    using System;
    using System.Linq;

    /// <summary>
    /// Layout (shape, offset, stride) of a NdArray.
    /// </summary>
    public class Layout
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="shape">Shape.</param>
        /// <param name="offset">Offset (to first element) in elements.</param>
        /// <param name="stride">Stride in elements.</param>
        public Layout(int[] shape, int offset, int[] stride)
        {
            this.Shape = shape;
            this.Offset = offset;
            this.Stride = stride;

            this.NumDimensions = shape.Length;
            this.NumElements = shape.Aggregate((a, b) => a * b);
        }

        public static Layout NewC(int[] shape)
        {
            return new Layout(shape, 0, CStride(shape));
        }

        public static Layout NewF(int[] shape)
        {
            return new Layout(shape, 0, FStride(shape));
        }

        /// <summary>
        /// Gets the shape.
        /// </summary>
        public int[] Shape { get; }

        /// <summary>
        /// Gets the offset (to first element) in elements.
        /// </summary>
        public int Offset { get; }

        /// <summary>
        /// Gets the stride in elements.
        /// </summary>
        public int[] Stride { get; }

        /// <summary>
        /// Number of dimensions.
        /// </summary>
        public int NumDimensions { get; }

        /// <summary>
        /// Number of elements.
        /// </summary>
        public int NumElements { get; }

        public static int[] OrderedStride(int[] shape, int[] order)
        {
            if (!Utils.Permutation.Is(order))
            {
                var msg = string.Format("The stride order {0} is not a permutation", order);
                throw new ArgumentException(msg);
            }
            if (order.Length != shape.Length)
            {
                var msg = string.Format("The stride order {0} is incompatible with the shape {1}", order, shape);
                throw new ArgumentException(msg);
            }

            var cumElems = 1;
            var orderedStride = new int[order.Length];
            for (var index = 0; index < order.Length; index++)
            {
                orderedStride[index] = cumElems;
                var orderedShape = shape[order[index]];
                cumElems = cumElems * orderedShape;
            }

            return order.Select(o => orderedStride[o]).ToArray();
        }

        public static int[] CStride(int[] shape)
        {
            var order = Enumerable.Range(0, shape.Length).Reverse().ToArray();

            return OrderedStride(shape, order);
        }

        public static int[] FStride(int[] shape)
        {
            var order = Enumerable.Range(0, shape.Length).ToArray();
            return OrderedStride(shape, order);
        }

        /// <summary>
        /// Swaps the specified dimensions of the NdArray.
        /// </summary>
        /// <param name="ax1">The dimension to swap.</param>
        /// <param name="ax2">The dimension to swap with.</param>
        /// <param name="a">The NdArray to operate on.</param>
        /// <returns>The NdArray with the dimensions swapped.</returns>
        public static Layout Swap(int ax1, int ax2, Layout a)
        {
            if(!(0 <= ax1 && ax1 < a.NumDimensions && 0 <= ax2 && ax2 < a.NumDimensions))
            {
                var msg = string.Format("Cannot swap dimension {0} with {1} of for shape {2}.", ax1, ax2, a.Shape);
                throw new ArgumentException(msg);
            }

            var newShp = a.Shape.Select(x => x).ToArray();
            var newStr = a.Stride.Select(x => x).ToArray();

            newShp[ax1] = a.Shape[ax2];
            newShp[ax2] = a.Shape[ax1];

            newStr[ax1] = a.Stride[ax2];
            newStr[ax2] = a.Stride[ax1];

            return new Layout(newShp, a.Offset, newStr);
        }
    }
}