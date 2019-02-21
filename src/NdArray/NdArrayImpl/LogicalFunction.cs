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

    internal static class LogicalFunction<T>
    {
        /// <summary>
        /// Element-wise logical negation.
        /// </summary>
        /// <param name="input">The NdArray to apply this operation to.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> FillNegate(NdArray<bool> input)
        {
            var (target, src) = NdArray<bool>.PrepareElemwise<bool, bool>(input);
            target.AssertBool();

            var src2 = NdArray<bool>.PrepareElemwiseSources(target, src);
            target.Backend.Negate(target, src2);

            return target;
        }

        /// <summary>
        /// Element-wise loigcal and.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> FillAnd(NdArray<bool> lhs, NdArray<bool> rhs)
        {
            var (target, lhs1, rhs1) = NdArray<bool>.PrepareElemwise<bool, bool, bool>(lhs, rhs);
            target.AssertBool();

            var (lhs2, rhs2) = NdArray<bool>.PrepareElemwiseSources(target, lhs1, rhs1);
            target.Backend.And(target, lhs2, rhs2);

            return target;
        }

        /// <summary>
        /// Element-wise loigcal or.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> FillOr(NdArray<bool> lhs, NdArray<bool> rhs)
        {
            var (target, lhs1, rhs1) = NdArray<bool>.PrepareElemwise<bool, bool, bool>(lhs, rhs);
            target.AssertBool();

            var (lhs2, rhs2) = NdArray<bool>.PrepareElemwiseSources(target, lhs1, rhs1);
            target.Backend.Or(target, lhs2, rhs2);

            return target;
        }

        /// <summary>
        /// Element-wise loigcal xor.
        /// </summary>
        /// <param name="lhs">The NdArray on the left side of this binary operation.</param>
        /// <param name="rhs">The NdArray on the right side of this binary operation.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> FillXor(NdArray<bool> lhs, NdArray<bool> rhs)
        {
            var (target, lhs1, rhs1) = NdArray<bool>.PrepareElemwise<bool, bool, bool>(lhs, rhs);
            target.AssertBool();

            var (lhs2, rhs2) = NdArray<bool>.PrepareElemwiseSources(target, lhs1, rhs1);
            target.Backend.Xor(target, lhs2, rhs2);

            return target;
        }

        /// <summary>
        /// Checks if all elements of the NdArray are true.
        /// </summary>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A scalar containing the result of this operation.</returns>
        public static bool All(NdArray<bool> input)
        {
            return AllNdArray(input).Value;
        }

        /// <summary>
        /// Checks if all elements of the NdArray are true returning the result as a NdArray.
        /// </summary>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> AllNdArray(NdArray<bool> input)
        {
            var flattendArray = NdArray<bool>.Flattern(input);
            return AllAxis(0, flattendArray);
        }

        /// <summary>
        /// Checks if all elements along the specified axis are true.
        /// </summary>
        /// <param name="axis">The axis to check along.</param>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> AllAxis(int axis, NdArray<bool> input)
        {
            var (targt, src) = NdArray<bool>.PrepareAxisReduceTarget<bool, bool>(axis, input);
            NdArray<bool>.FillAllAxis(targt, axis, src);
            return targt;
        }

        /// <summary>
        /// Checks if any elements of the NdArray are true.
        /// </summary>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A scalar containing the result of this operation.</returns>
        public static bool Any(NdArray<bool> input)
        {
            return AnyNdArray(input).Value;
        }

        /// <summary>
        /// Checks if any element of the NdArray is true returning the result as a NdArray.
        /// </summary>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> AnyNdArray(NdArray<bool> input)
        {
            var flattendArray = NdArray<bool>.Flattern(input);
            return AnyAxis(0, flattendArray);
        }

        /// <summary>
        /// Checks if any element along the specified axis is true.
        /// </summary>
        /// <param name="axis">The axis to check along.</param>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new NdArray containing the result of this operation.</returns>
        public static NdArray<bool> AnyAxis(int axis, NdArray<bool> input)
        {
            var (target, src1) = NdArray<bool>.PrepareAxisReduceTarget<bool, bool>(axis, input);
            var (newSrc, _) = NdArray<bool>.PrepareAxisReduceSources(target, axis, input, null);
            target.Backend.AnyLastAxis(target, newSrc);

            return target;
        }

        /// <summary>
        /// Counts the elements being true along the specified axis.
        /// </summary>
        /// <param name="axis">The axis the count along.</param>
        /// <param name="input">The NdArray containing the source values.</param>
        public static NdArray<int> FillCountTrueAxis(int axis, NdArray<bool> input)
        {
            var (target, src1) = NdArray<int>.PrepareAxisReduceTarget<int, bool>(axis, input);
            target.AssertInt();

            var (newSrc, _) = NdArray<int>.PrepareAxisReduceSources(target, axis, input, null);
            target.Backend.CountTrueLastAxis(target, newSrc);

            return target;
        }

        /// <summary>
        /// Counts the elements being true returning the result as a NdArray.
        /// </summary>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A new scalar NdArray containing the result of this operation.</returns>
        public static NdArray<int> CountTrueNdArray(NdArray<bool> input)
        {
            return FillCountTrueAxis(0, NdArray<bool>.Flatten(input));
        }

        /// <summary>Counts the elements being true.</summary>
        /// <param name="input">The NdArray containing the source values.</param>
        /// <returns>A scalar containing the result of this operation.</returns>
        public static int CountTrue(NdArray<bool> input)
        {
            return CountTrueNdArray(input).Value;
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
            var (target, cond, ift, iff) = NdArray<T>.PrepareElemwise<T, bool, T, T>(condition, ifTrue, ifFalse);

            var (cond2, ift2, iff2) = NdArray<T>.PrepareElemwiseSources(target, cond, ift, iff);
            target.Backend.IfThenElse(target, cond2, ift2, iff2);

            return target;
        }
    }
}