// <copyright file="LogicalFunction.cs" company="NdArrayNet">
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
    using NdArrayNet;

    internal class LogicalFunction<T> : FunctionBase
    {
        public static void FillNegate(NdArray<bool> target, NdArray<bool> source)
        {
            FillNegate(StaticMethod.Value, target, source);
        }

        /// <summary>
        /// Element-wise logical negation.
        /// </summary>
        /// <param name="source">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> Negate(NdArray<bool> source)
        {
            return Negate(StaticMethod.Value, source);
        }

        public static void FillAnd(NdArray<bool> target, NdArray<bool> lhs, NdArray<bool> rhs)
        {
            FillAnd(StaticMethod.Value, target, lhs, rhs);
        }

        /// <summary>
        /// Element-wise loigcal and.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> And(NdArray<bool> lhs, NdArray<bool> rhs)
        {
            return And(StaticMethod.Value, lhs, rhs);
        }

        public static void FillOr(NdArray<bool> target, NdArray<bool> lhs, NdArray<bool> rhs)
        {
            FillOr(StaticMethod.Value, target, lhs, rhs);
        }

        /// <summary>
        /// Element-wise loigcal or.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> Or(NdArray<bool> lhs, NdArray<bool> rhs)
        {
            return Or(StaticMethod.Value, lhs, rhs);
        }

        public static void FillXor(NdArray<bool> target, NdArray<bool> lhs, NdArray<bool> rhs)
        {
            FillXor(StaticMethod.Value, target, lhs, rhs);
        }

        /// <summary>
        /// Element-wise loigcal xor.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> Xor(NdArray<bool> lhs, NdArray<bool> rhs)
        {
            return Xor(StaticMethod.Value, lhs, rhs);
        }

        public static void FillAllAxis(NdArray<bool> target, int axis, NdArray<bool> source)
        {
            FillAllAxis(StaticMethod.Value, target, axis, source);
        }

        /// <summary>
        /// Checks if all elements along the specified axis are true.
        /// </summary>
        /// <param name="axis">The axis to check along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> AllAxis(int axis, NdArray<bool> source)
        {
            return AllAxis(StaticMethod.Value, axis, source);
        }

        /// <summary>
        /// Checks if all elements of the NdArray are true returning the result as a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> AllNdArray(NdArray<bool> source)
        {
            var flattendArray = NdArray<bool>.Flattern(source);
            return AllAxis(0, flattendArray);
        }

        /// <summary>
        /// Checks if all elements of the NdArray are true.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A scalar containing the result of this operation.</returns>
        public static bool All(NdArray<bool> source)
        {
            return AllNdArray(source).Value;
        }

        public static void FillAnyAxis(NdArray<bool> target, int axis, NdArray<bool> source)
        {
            FillAnyAxis(StaticMethod.Value, target, axis, source);
        }

        /// <summary>
        /// Checks if any element along the specified axis is true.
        /// </summary>
        /// <param name="axis">The axis to check along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> AnyAxis(int axis, NdArray<bool> source)
        {
            return AnyAxis(StaticMethod.Value, axis, source);
        }

        /// <summary>
        /// Checks if any element of the NdArray is true returning the result as a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> AnyNdArray(NdArray<bool> source)
        {
            var flattendArray = NdArray<bool>.Flattern(source);
            return AnyAxis(0, flattendArray);
        }

        /// <summary>
        /// Checks if any elements of the NdArray are true.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A scalar containing the result of this operation.</returns>
        public static bool Any(NdArray<bool> source)
        {
            return AnyNdArray(source).Value;
        }

        public static void FillCountTrueAxis(NdArray<int> target, int axis, NdArray<bool> source)
        {
            FillCountTrueAxis(StaticMethod.Value, target, axis, source);
        }

        /// <summary>
        /// Counts the elements being true along the specified axis.
        /// </summary>
        /// <param name="axis">The axis the count along.</param>
        /// <param name="source">The NdArray containing the source values.</param>
        public static NdArray<int> CountTrueAxis(int axis, NdArray<bool> source)
        {
            return CountTrueAxis(StaticMethod.Value, axis, source);
        }

        /// <summary>
        /// Counts the elements being true returning the result as a NdArray.
        /// </summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<int> CountTrueNdArray(NdArray<bool> source)
        {
            return CountTrueAxis(0, NdArray<bool>.Flatten(source));
        }

        /// <summary>Counts the elements being true.</summary>
        /// <param name="source">The NdArray containing the source values.</param>
        /// <returns>A scalar containing the result of this operation.</returns>
        public static int CountTrue(NdArray<bool> source)
        {
            return CountTrueNdArray(source).Value;
        }

        /// <summary>
        /// Element-wise choice between two sources depending on a condition.
        /// </summary>
        /// <param name="cond">The condition NdArray.</param>
        /// <param name="ifTrue">The NdArray containing the values to use for when an element of the condition is true.</param>
        /// <param name="ifFalse">The NdArray containing the values to use for when an element of the condition is false.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<T> IfThenElse(NdArray<bool> condition, NdArray<T> ifTrue, NdArray<T> ifFalse)
        {
            return IfThenElse(StaticMethod.Value, condition, ifTrue, ifFalse);
        }

        internal static void FillNegate(IStaticMethod staticMethod, NdArray<bool> target, NdArray<bool> source)
        {
            var preparedSource = staticMethod.PrepareElemwiseSources(target, source);
            target.Backend.Negate(target, preparedSource);
        }

        internal static NdArray<bool> Negate(IStaticMethod staticMethod, NdArray<bool> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareElemwise<bool, bool>(source, Order.RowMajor);
            preparedTarget.AssertBool();

            FillNegate(preparedTarget, preparedSource);

            return preparedTarget;
        }

        internal static void FillAnd(IStaticMethod staticMethod, NdArray<bool> target, NdArray<bool> lhs, NdArray<bool> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.And(target, preparedLhs, preparedRhs);
        }

        internal static NdArray<bool> And(IStaticMethod staticMethod, NdArray<bool> lhs, NdArray<bool> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, bool, bool>(lhs, rhs, Order.RowMajor);
            preparedTarget.AssertBool();

            FillAnd(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        internal static void FillOr(IStaticMethod staticMethod, NdArray<bool> target, NdArray<bool> lhs, NdArray<bool> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Or(target, preparedLhs, preparedRhs);
        }

        internal static NdArray<bool> Or(IStaticMethod staticMethod, NdArray<bool> lhs, NdArray<bool> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, bool, bool>(lhs, rhs, Order.RowMajor);
            preparedTarget.AssertBool();

            FillOr(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        internal static void FillXor(IStaticMethod staticMethod, NdArray<bool> target, NdArray<bool> lhs, NdArray<bool> rhs)
        {
            var (preparedLhs, preparedRhs) = staticMethod.PrepareElemwiseSources(target, lhs, rhs);
            target.Backend.Xor(target, preparedLhs, preparedRhs);
        }

        internal static NdArray<bool> Xor(IStaticMethod staticMethod, NdArray<bool> lhs, NdArray<bool> rhs)
        {
            var (preparedTarget, preparedLhs, preparedRhs) = staticMethod.PrepareElemwise<bool, bool, bool>(lhs, rhs, Order.RowMajor);
            preparedTarget.AssertBool();

            FillXor(preparedTarget, preparedLhs, preparedRhs);

            return preparedTarget;
        }

        internal static void FillAllAxis(IStaticMethod staticMethod, NdArray<bool> target, int axis, NdArray<bool> source)
        {
            var (preparedSource, _) = staticMethod.PrepareAxisReduceSources(target, axis, source, null, Order.RowMajor);
            target.Backend.AllLastAxis(target, preparedSource);
        }

        internal static NdArray<bool> AllAxis(IStaticMethod staticMethod, int axis, NdArray<bool> source)
        {
            var (preparedTargt, preparedSource) = staticMethod.PrepareAxisReduceTarget<bool, bool>(axis, source, Order.RowMajor);
            FillAllAxis(preparedTargt, axis, preparedSource);
            return preparedTargt;
        }

        internal static void FillAnyAxis(IStaticMethod staticMethod, NdArray<bool> target, int axis, NdArray<bool> source)
        {
            var (preparedSource, _) = staticMethod.PrepareAxisReduceSources(target, axis, source, null, Order.RowMajor);
            target.Backend.AnyLastAxis(target, preparedSource);
        }

        internal static NdArray<bool> AnyAxis(IStaticMethod staticMethod, int axis, NdArray<bool> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareAxisReduceTarget<bool, bool>(axis, source, Order.RowMajor);
            FillAnyAxis(preparedTarget, axis, preparedSource);

            return preparedTarget;
        }

        internal static void FillCountTrueAxis(IStaticMethod staticMethod, NdArray<int> target, int axis, NdArray<bool> source)
        {
            var (preparedSource, _) = staticMethod.PrepareAxisReduceSources(target, axis, source, null, Order.RowMajor);
            target.Backend.CountTrueLastAxis(target, preparedSource);
        }

        internal static NdArray<int> CountTrueAxis(IStaticMethod staticMethod, int axis, NdArray<bool> source)
        {
            var (preparedTarget, preparedSource) = staticMethod.PrepareAxisReduceTarget<int, bool>(axis, source, Order.RowMajor);
            preparedTarget.AssertInt();

            FillCountTrueAxis(preparedTarget, axis, preparedSource);

            return preparedTarget;
        }

        internal static NdArray<T> IfThenElse(IStaticMethod staticMethod, NdArray<bool> condition, NdArray<T> ifTrue, NdArray<T> ifFalse)
        {
            var (target, cond, ift, iff) = staticMethod.PrepareElemwise<T, bool, T, T>(condition, ifTrue, ifFalse, Order.RowMajor);

            var (cond2, ift2, iff2) = staticMethod.PrepareElemwiseSources(target, cond, ift, iff);
            target.Backend.IfThenElse(target, cond2, ift2, iff2);

            return target;
        }
    }
}