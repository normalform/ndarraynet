// <copyright file="NdComparison.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction.Comparison
{
    using System;
    using System.Collections.Generic;
    using NdArrayNet;

    internal class NdComparison
    {
        private readonly object mapLock = new object();

        private readonly IStaticMethod StaticMethod;

        private readonly Dictionary<Type, object> comparisonMap = new Dictionary<Type, object>();

        public NdComparison(IStaticMethod staticMethod)
        {
            StaticMethod = staticMethod;
            comparisonMap.Add(typeof(bool), new BoolComparison(staticMethod));
            comparisonMap.Add(typeof(byte), new ByteComparison(staticMethod));
            comparisonMap.Add(typeof(char), new CharComparison(staticMethod));
            comparisonMap.Add(typeof(decimal), new DecimalComparison(staticMethod));
            comparisonMap.Add(typeof(double), new DoubleComparison(staticMethod));
            comparisonMap.Add(typeof(float), new FloatComparison(staticMethod));
            comparisonMap.Add(typeof(int), new IntComparison(staticMethod));
            comparisonMap.Add(typeof(long), new LongComparison(staticMethod));
            comparisonMap.Add(typeof(sbyte), new SByteComparison(staticMethod));
            comparisonMap.Add(typeof(short), new ShortComparison(staticMethod));
            comparisonMap.Add(typeof(uint), new UIntComparison(staticMethod));
            comparisonMap.Add(typeof(ulong), new ULongComparison(staticMethod));
            comparisonMap.Add(typeof(ushort), new UShortComparison(staticMethod));
        }

        public INdComparison<T> Get<T>()
        {
            var type = typeof(T);
            lock(mapLock)
            {
                if (comparisonMap.ContainsKey(type))
                {
                    return comparisonMap[type] as INdComparison<T>;
                }
            }

            return null;
        }
    }
}
