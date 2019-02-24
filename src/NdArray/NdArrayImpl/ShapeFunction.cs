// <copyright file="ShapeFunction.cs" company="NdArrayNet">
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

namespace NdArray.NdArrayImpl
{
    using System;
    using System.Linq;
    using NdArrayNet;

    internal static class ShapeFunction<T>
    {
        /// <summary>
        /// Pads the NdArray from the left with size-one dimensions until it has at least the specified number of
        /// dimensions.
        /// </summary>
        /// <param name="minNumDim">The minimum number of dimensions.</param>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>A NdArray with at least <paramref name="minNumDim"/> dimensions.</returns>
        public static NdArray<T> AtLeastNd(int minNumDim, NdArray<T> source)
        {
            if (source.NumDimensions >= minNumDim)
            {
                return source;
            }

            var newShape = Enumerable.Repeat(1, minNumDim - source.NumDimensions).Concat(source.Shape).ToArray();

            return source.Reshape(newShape);
        }

        /// <summary>
        /// Pads the NdArray from the left with size-one dimensions until it has at least one dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>A NdArray with at least one dimensions.</returns>
        public static NdArray<T> AtLeast1d(NdArray<T> source)
        {
            return AtLeastNd(1, source);
        }

        /// <summary>
        /// Pads the NdArray from the left with size-two dimensions until it has at least two dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>A NdArray with at least two dimensions.</returns>
        public static NdArray<T> AtLeast2d(NdArray<T> source)
        {
            return AtLeastNd(2, source);
        }

        /// <summary>
        /// Pads the NdArray from the left with size-three dimensions until it has at least three dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>A NdArray with at least three dimensions.</returns>
        public static NdArray<T> AtLeast3d(NdArray<T> source)
        {
            return AtLeastNd(3, source);
        }

        /// <summary>
        /// Broadcast a dimension to a specified size.
        /// </summary>
        /// <param name="dim">The size-one dimension to broadcast.</param>
        /// <param name="size">The size to broadcast to.</param>
        /// <param name="a">The NdArray to operate on.</param>
        /// <returns>The resulting NdArray.</returns>
        public static NdArray<T> BroadCastDim(int dim, int size, NdArray<T> source)
        {
            return source.Relayout(Layout.BroadcastDim(dim, size, source.Layout));
        }

        /// <summary>
        /// Broadcasts the specified NdArray to the specified shape.
        /// </summary>
        /// <param name="shp">The target shape.</param>
        /// <param name="frontend">The NdArray to operate on.</param>
        /// <returns>NdArray of shape <paramref name="shp"/>.</returns>
        public static NdArray<T> BroadCastTo(int[] shp, IFrontend<T> frontend)
        {
            var layout = Layout.BroadcastToShape(shp, frontend.Layout);
            return frontend.Relayout(layout);
        }

        /// <summary>
        /// Broadcasts all specified NdArrays to have the same shape.
        /// </summary>
        /// <param name="src1">The NdArray to operate on.</param>
        /// <param name="src2">The NdArray to operate on.</param>
        /// <returns>A tuple of the resulting NdArrays, all having the same shape.</returns>
        public static (NdArray<T1>, NdArray<T2>) BroadCastToSame<T1, T2>(IFrontend<T1> src1, IFrontend<T2> src2)
        {
            return ApplyLayoutFn(Layout.BroadcastToSameMany, src1, src2);
        }

        /// <summary>
        /// Broadcasts all specified NdArrays to have the same shape.
        /// </summary>
        /// <param name="src1">The NdArray to operate on.</param>
        /// <param name="src2">The NdArray to operate on.</param>
        /// <param name="src3">The NdArray to operate on.</param>
        /// <returns>A tuple of the resulting NdArrays, all having the same shape.</returns>
        public static (NdArray<T1>, NdArray<T2>, NdArray<T3>) BroadCastToSame<T1, T2, T3>(IFrontend<T1> src1, IFrontend<T2> src2, IFrontend<T3> src3)
        {
            return ApplyLayoutFn(Layout.BroadcastToSameMany, src1, src2, src3);
        }

        /// <summary>
        /// Broadcasts all specified NdArrays to have the same shape.
        /// </summary>
        /// <param name="sources">A list of NdArrays to operate on.</param>
        /// <returns>A list of the resulting NdArrays, all having the same shape.</returns>
        public static NdArray<T>[] BroadCastToSame(NdArray<T>[] sources)
        {
            return ApplyLayoutFn(Layout.BroadcastToSameMany, sources);
        }

        /// <summary>
        /// Broadcasts all specified NdArrays to have the same size in the specified dimensions.
        /// </summary>
        /// <param name="dims">A list of dimensions that should be broadcasted to have the same size.</param>
        /// <param name="src1">The NdArray to operate on.</param>
        /// <param name="src2">The NdArray to operate on.</param>
        /// <returns>A tuple of the resulting NdArrays, all having the same size in the specified dimensions.</returns>
        public static (NdArray<T1>, NdArray<T2>) BroadCastToSameInDims<T1, T2>(int[] dims, NdArray<T1> src1, NdArray<T2> src2)
        {
            return ApplyLayoutFn(Layout.BroadcastToSameInDimsMany, dims, src1, src2);
        }

        /// <summary>
        /// Broadcasts all specified NdArrays to have the same size in the specified dimensions.
        /// </summary>
        /// <param name="dims">A list of dimensions that should be broadcasted to have the same size.</param>
        /// <param name="src1">The NdArray to operate on.</param>
        /// <param name="src2">The NdArray to operate on.</param>
        /// <param name="src3">The NdArray to operate on.</param>
        /// <returns>A tuple of the resulting NdArrays, all having the same size in the specified dimensions.</returns>
        public static (NdArray<T1>, NdArray<T2>, NdArray<T3>) BroadCastToSameInDims<T1, T2, T3>(int[] dims, NdArray<T1> src1, NdArray<T2> src2, NdArray<T3> src3)
        {
            return ApplyLayoutFn(Layout.BroadcastToSameInDimsMany, dims, src1, src2, src3);
        }

        /// <summary>
        /// Broadcasts all specified NdArrays to have the same size in the specified dimensions.
        /// </summary>
        /// <param name="dims">A list of dimensions that should be broadcasted to have the same size.</param>
        /// <param name="sources">A list of NdArrays to operate on.</param>
        /// <returns>A list of the resulting NdArrays, all having the same size in the specified dimensions.</returns>
        public static NdArray<T>[] BroadCastToSameInDims(int[] dims, NdArray<T>[] sources)
        {
            return ApplyLayoutFn(Layout.BroadcastToSameInDimsMany, dims, sources);
        }

        /// <summary>
        /// Removes the first dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The resulting NdArray.</returns>
        public static NdArray<T> CutLeft(NdArray<T> source)
        {
            return source.Relayout(Layout.CutLeft(source.Layout));
        }

        /// <summary>
        /// Removes the last dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The resulting NdArray.</returns>
        public static NdArray<T> CutRight(NdArray<T> source)
        {
            return source.Relayout(Layout.CutRight(source.Layout));
        }

        /// <summary>
        /// Flattens the NdArray into a (one-dimensional) vector.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>A vector.</returns>
        public static NdArray<T> Flatten(NdArray<T> source)
        {
            return source.Reshape(new[] { SpecialIdx.Remainder });
        }

        /// <summary>
        /// Insert a dimension of size one before the specifed dimension.
        /// </summary>
        /// <param name="axis">The dimension to insert before.</param>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The resulting NdArray.</returns>
        public static NdArray<T> InsertAxis(int axis, NdArray<T> source)
        {
            return source.Relayout(Layout.InsertAxis(axis, source.Layout));
        }

        /// <summary>
        /// Checks if the specified NdArray is broadcasted in at least one dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>true if at least one dimension is broadcasted, otherwise false.</returns>
        public static bool IsBroadcasted(NdArray<T> source)
        {
            return Layout.IsBroadcasted(source.Layout);
        }

        /// <summary>
        /// Insert a dimension of size one as the first dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The resulting NdArray.</returns>
        public static NdArray<T> PadLeft(NdArray<T> source)
        {
            return source.Relayout(Layout.PadLeft(source.Layout));
        }

        /// <summary>
        /// Append a dimension of size one after the last dimension.
        /// </summary>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The resulting NdArray.</returns>
        public static NdArray<T> PadRight(NdArray<T> source)
        {
            return source.Relayout(Layout.PadRight(source.Layout));
        }

        /// <summary>
        /// Pads all specified NdArrays to have the same shape.
        /// </summary>
        /// <param name="src1">The NdArray to operate on.</param>
        /// <param name="src2">The NdArray to operate on.</param>
        /// <returns>A tuple of the resulting NdArrays, all having the same shape.</returns>
        public static (NdArray<T1>, NdArray<T2>) PadToSame<T1, T2>(NdArray<T1> src1, NdArray<T2> src2)
        {
            return ApplyLayoutFn(Layout.PadToSameMany, src1, src2);
        }

        /// <summary>
        /// Pads all specified NdArrays to have the same shape.
        /// </summary>
        /// <param name="src1">The NdArray to operate on.</param>
        /// <param name="src2">The NdArray to operate on.</param>
        /// <param name="src3">The NdArray to operate on.</param>
        /// <returns>A tuple of the resulting NdArrays, all having the same shape.</returns>
        public static (NdArray<T1>, NdArray<T2>, NdArray<T3>) PadToSame<T1, T2, T3>(NdArray<T1> src1, NdArray<T2> src2, NdArray<T3> src3)
        {
            return ApplyLayoutFn(Layout.PadToSameMany, src1, src2, src3);
        }

        /// <summary>
        /// Pads all specified NdArrays to have the same shape.
        /// </summary>
        /// <param name="sources">A list of NdArrays to operate on.</param>
        /// <returns>A list of the resulting NdArrays, all having the same shape.</returns>
        public static NdArray<T>[] PadToSame(NdArray<T>[] sources)
        {
            return ApplyLayoutFn(Layout.PadToSameMany, sources);
        }

        /// <summary>
        /// Permutes the axes as specified.
        /// </summary>
        /// <param name="permut">The permutation to apply to the dimensions of NdArray.</param>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The NdArray with the dimensions permuted.</returns>
        public static NdArray<T> PermuteAxes(int[] permut, NdArray<T> source)
        {
            return source.Relayout(Layout.PermuteAxes(permut, source.Layout));
        }

        /// <summary>
        /// Reverses the elements in the specified dimension.
        /// </summary>
        /// <param name="axis">The axis to reverse.</param>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The NdArray with the dimensions Reversed.</returns>
        public static NdArray<T> ReverseAxis(int axis, NdArray<T> source)
        {
            return source.Relayout(Layout.ReverseAxis(axis, source.Layout));
        }

        /// <summary>
        /// Swaps the specified dimensions of the NdArray.
        /// </summary>
        /// <param name="axis1">The dimension to swap.</param>
        /// <param name="axis2">The dimension to swap with.</param>
        /// <param name="source">The NdArray to operate on.</param>
        /// <returns>The NdArray with the dimensions swapped.</returns>
        public static NdArray<T> SwapDim(int axis1, int axis2, NdArray<T> source)
        {
            return source.Relayout(Layout.SwapDim(axis1, axis2, source.Layout));
        }

        internal static (NdArray<T1>, NdArray<T2>) ApplyLayoutFn<T1, T2>(Func<Layout[], Layout[]> fn, IFrontend<T1> src1, IFrontend<T2> src2)
        {
            var layouts = new[] { src1.Layout, src2.Layout };
            var newLayouts = fn(layouts);
            if (newLayouts.Length == 2 && newLayouts[0] != null && newLayouts[1] != null)
            {
                return (src1.Relayout(newLayouts[0]), src2.Relayout(newLayouts[1]));
            }

            throw new InvalidOperationException("unexpected layout function result");
        }

        internal static (NdArray<T1>, NdArray<T2>, NdArray<T3>) ApplyLayoutFn<T1, T2, T3>(Func<Layout[], Layout[]> fn, IFrontend<T1> src1, IFrontend<T2> src2, IFrontend<T3> src3)
        {
            var layouts = new[] { src1.Layout, src2.Layout, src3.Layout };
            var newLayouts = fn(layouts);
            if (newLayouts.Length == 3 && newLayouts[0] != null && newLayouts[1] != null && newLayouts[2] != null)
            {
                return (src1.Relayout(newLayouts[0]), src2.Relayout(newLayouts[1]), src3.Relayout(newLayouts[2]));
            }

            throw new InvalidOperationException("unexpected layout function result");
        }

        internal static NdArray<T>[] ApplyLayoutFn(Func<Layout[], Layout[]> fn, NdArray<T>[] sources)
        {
            var layouts = sources.Select(l => l.Layout).ToArray();
            var newLayouts = fn(layouts);

            return sources.Select((s, idx) => s.Relayout(newLayouts[idx])).ToArray();
        }

        internal static (NdArray<T1>, NdArray<T2>) ApplyLayoutFn<T1, T2>(Func<int[], Layout[], Layout[]> fn, int[] dims, NdArray<T1> src1, NdArray<T2> src2)
        {
            var layouts = new[] { src1.Layout, src2.Layout };
            var newLayouts = fn(dims, layouts);
            if (newLayouts.Length == 2 && newLayouts[0] != null && newLayouts[1] != null)
            {
                return (src1.Relayout(newLayouts[0]), src2.Relayout(newLayouts[1]));
            }

            throw new InvalidOperationException("unexpected layout function result");
        }

        internal static (NdArray<T1>, NdArray<T2>, NdArray<T3>) ApplyLayoutFn<T1, T2, T3>(Func<int[], Layout[], Layout[]> fn, int[] dims, NdArray<T1> src1, NdArray<T2> src2, NdArray<T3> src3)
        {
            var layouts = new[] { src1.Layout, src2.Layout, src3.Layout };
            var newLayouts = fn(dims, layouts);
            if (newLayouts.Length == 3 && newLayouts[0] != null && newLayouts[1] != null && newLayouts[2] != null)
            {
                return (src1.Relayout(newLayouts[0]), src2.Relayout(newLayouts[1]), src3.Relayout(newLayouts[2]));
            }

            throw new InvalidOperationException("unexpected layout function result");
        }

        internal static NdArray<T>[] ApplyLayoutFn(Func<int[], Layout[], Layout[]> fn, int[] dims, NdArray<T>[] sources)
        {
            var layouts = sources.Select(l => l.Layout).ToArray();
            var newLayouts = fn(dims, layouts);

            return sources.Select((s, idx) => s.Relayout(newLayouts[idx])).ToArray();
        }
    }
}