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

        private readonly Comparison.NdComparison comparison;
        private readonly Constructor.NdConstructor constructor;

        public NdFunction(IStaticMethod staticMethod)
        {
            this.staticMethod = staticMethod;
            comparison = new Comparison.NdComparison(staticMethod);
            constructor = new Constructor.NdConstructor(staticMethod);

            Comparison = comparison.Get<T>();
            Constructor = constructor.Get<T>();
        }

        public Comparison.INdComparison<T> Comparison { get; }
        public Constructor.INdConstructor<T> Constructor { get; }
    }
}