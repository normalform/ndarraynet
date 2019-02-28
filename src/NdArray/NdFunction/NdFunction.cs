// <copyright file="NdFunction.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction
{
    using NdArrayNet;

    internal class NdFunction<T> : INdFunction<T>
    {
        private readonly IStaticMethod staticMethod;

        private readonly Comparison.NdArrayComparison comparison;

        public NdFunction(IStaticMethod staticMethod)
        {
            this.staticMethod = staticMethod;
            comparison = new Comparison.NdArrayComparison(staticMethod);

            Comparison = comparison.GetComparison<T>();
        }

        public Comparison.INdArrayComparison<T> Comparison { get; }
    }
}