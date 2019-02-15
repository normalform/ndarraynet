// <copyright file="Layout.cs" company="NdArrayNet">
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
            Shape = shape;
            Offset = offset;
            Stride = stride;

            NumDimensions = shape.Length;
            NumElements = shape.Aggregate(1, (a, b) => a * b);
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

        public static Layout NewC(int[] shape)
        {
            return new Layout(shape, 0, CStride(shape));
        }

        public static Layout NewF(int[] shape)
        {
            return new Layout(shape, 0, FStride(shape));
        }

        public static int[] OrderedStride(int[] shape, int[] order)
        {
            if (!Permutation.Is(order))
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
            if (!(ax1 >= 0 && ax1 < a.NumDimensions && ax2 >= 0 && ax2 < a.NumDimensions))
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

        public static Layout PadLeft(Layout input)
        {
            var newShape = new[] { 1 }.Concat(input.Shape).ToArray();
            var newStride = new[] { 0 }.Concat(input.Stride).ToArray();

            return new Layout(newShape, input.Offset, newStride);
        }

        public static Layout PadRight(Layout input)
        {
            var newShape = input.Shape.Concat(new[] { 1 }).ToArray();
            var newStride = input.Stride.Concat(new[] { 0 }).ToArray();

            return new Layout(newShape, input.Offset, newStride);
        }

        public static bool StrideEqual(int[] shp, int[] strA, int[] strB)
        {
            var zip = shp.Zip(strA, (sp, sa) => new { Shape = sp, StrideA = sa })
                .Zip(strB, (spAndSa, sb) => new { spAndSa.Shape, StribeA = spAndSa.StrideA, StribeB = sb });

            return zip.All(z => (z.Shape == 0) || (z.StribeA == z.StribeB));
        }

        public static bool IsC(Layout input)
        {
            return StrideEqual(input.Shape, input.Stride, CStride(input.Shape));
        }

        public static bool IsF(Layout input)
        {
            return StrideEqual(input.Shape, input.Stride, FStride(input.Shape));
        }

        public static bool HasContiguousMemory(Layout input)
        {
            return IsC(input) || IsF(input);
        }

        public static Layout BraodcastDim(int dim, int size, Layout layout)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException("size", "size must be positive");
            }

            if (layout.Shape[dim] == 1)
            {
                return new Layout(List.Set(dim, size, layout.Shape), layout.Offset, List.Set(dim, 0, layout.Stride));
            }

            var msg = string.Format("Dimension {0} of shape {1} must be of size 1 to broadcast.", dim, layout.Shape);
            throw new ArgumentOutOfRangeException(msg);
        }

        public static Layout BroadcastToShape(int[] broadcastShape, Layout input)
        {
            var broadcastDim = broadcastShape.Length;
            if (broadcastDim < input.NumDimensions)
            {
                var msg = string.Format("Cannot broadcast to shape {0} from shape {1} of higher rank.", broadcastShape, input.Shape);
                throw new InvalidOperationException(msg);
            }

            var broadcastLayout = new Layout(input.Shape, input.Offset, input.Stride);

            while (broadcastLayout.NumDimensions < broadcastDim)
            {
                broadcastLayout = PadLeft(broadcastLayout);
            }

            for (var dimIndex = 0; dimIndex < broadcastDim; dimIndex++)
            {
                var targetShapeValue = broadcastLayout.Shape[dimIndex];
                var inputShapeValue = broadcastShape[dimIndex];
                if (targetShapeValue == inputShapeValue)
                {
                    continue;
                }

                if (targetShapeValue == 1)
                {
                    broadcastLayout = BraodcastDim(dimIndex, inputShapeValue, broadcastLayout);
                }
                else
                {
                    var msg = string.Format("Cannot broadcast shape {0} to shape {1}.", input.Shape, broadcastShape);
                    throw new InvalidOperationException(msg);
                }
            }

            return broadcastLayout;
        }

        public Layout View(IRange[] ranges, Layout layout)
        {
            Layout SubView(IRange[] subRanges, Layout subLayout)
            {
                if (subRanges.Length == 0 || subLayout.Stride.Length == 0)
                {
                    return subLayout;
                }

                var currentRangeType = subRanges[0].Type;
                if (currentRangeType == RangeType.AllFill)
                {
                    var restRanges = subRanges.Skip(1).ToArray();
                    var restShps = subLayout.Shape.Skip(1).ToArray();
                    if (restShps.Length > restRanges.Length)
                    {
                        var newRange = new[] { RangeFactory.All, RangeFactory.AllFill }.Concat(restRanges).ToArray();
                        return SubView(newRange, subLayout);
                    }
                    else if (restShps.Length == restRanges.Length)
                    {
                        var newRange = new[] { RangeFactory.All }.Concat(restRanges).ToArray();
                        return SubView(newRange, subLayout);
                    }
                    else
                    {
                        return SubView(restRanges, subLayout);
                    }
                }
                else if (currentRangeType == RangeType.Range || currentRangeType == RangeType.Elem)
                {
                    var restRanges = subRanges.Skip(1).ToArray();
                    var shape = subLayout.Shape.Length > 0 ? subLayout.Shape[0] : 0;
                    var stride = subLayout.Stride.Length > 0 ? subLayout.Stride[0] : 0;

                    var restShape = subLayout.Shape.Skip(1).ToArray();
                    var restStride = subLayout.Stride.Skip(1).ToArray();
                    var restLayout = SubView(restRanges, new Layout(restShape, subLayout.Offset, restStride));

                    if (currentRangeType == RangeType.Elem)
                    {
                        var position = ((Elem)subRanges[0]).Pos;
                        CheckElementRange(false, shape, position, ranges, subLayout.Shape);
                        return new Layout(restLayout.Shape, restLayout.Offset + (position * stride), restLayout.Stride);
                    }
                    else if (currentRangeType == RangeType.Range)
                    {
                        var start = ((Range)subRanges[0]).Start;
                        var stop = ((Range)subRanges[0]).Stop;
                        var step = ((Range)subRanges[0]).Step;

                        // This indicates for the 'All' range specifier.
                        if (start == 0 && stop == 0 && step == 0)
                        {
                            stop = shape - 1;
                        }

                        if (stop >= start)
                        {
                            // non-empy slice
                            CheckElementRange(false, shape, start, ranges, subLayout.Shape);
                            CheckElementRange(true, shape, stop, ranges, subLayout.Shape);
                            return new Layout(
                                new[] { stop + 1 - start }.Concat(restLayout.Shape).ToArray(),
                                restLayout.Offset + (start * stride),
                                new[] { stride }.Concat(restLayout.Stride).ToArray());
                        }
                        else
                        {
                            // empty slice
                            // We allow start and stop to be out of range in this case.
                            return new Layout(
                                new[] { 0 }.Concat(restLayout.Shape).ToArray(),
                                restLayout.Offset,
                                new[] { stride }.Concat(restLayout.Stride).ToArray());
                        }
                    }
                }
                else if (currentRangeType == RangeType.NewAxis)
                {
                    var restRanges = subRanges.Skip(1).ToArray();
                    var restLayout = SubView(restRanges, subLayout);

                    return new Layout(
                        new[] { 1 }.Concat(restLayout.Shape).ToArray(),
                        restLayout.Offset,
                        new[] { 0 }.Concat(restLayout.Stride).ToArray());
                }

                var msg = string.Format("Slice {0} is incompatible with shape {1}.", ranges, subLayout);
                throw new ArgumentOutOfRangeException(msg);
            }

            return SubView(ranges, layout);
        }

        private static void CheckElementRange(bool isEnd, int numElements, int index, IRange[] ranges, int[] shp)
        {
            var numEl = isEnd ? numElements + 1 : numElements;
            if (!(index >= 0 && index < numEl))
            {
                var msg = string.Format("Index {0} out of range in slice {1} for shape {2}.", index, ranges, shp);
                throw new ArgumentOutOfRangeException(msg);
            }
        }
    }
}