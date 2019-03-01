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
            comparisonMap.Add(typeof(bool), new DefaultComparison<bool>(staticMethod));
            comparisonMap.Add(typeof(byte), new DefaultComparison<byte>(staticMethod));
            comparisonMap.Add(typeof(char), new DefaultComparison<char>(staticMethod));
            comparisonMap.Add(typeof(decimal), new DefaultComparison<decimal>(staticMethod));
            comparisonMap.Add(typeof(double), new DefaultComparison<double>(staticMethod));
            comparisonMap.Add(typeof(float), new DefaultComparison<float>(staticMethod));
            comparisonMap.Add(typeof(int), new DefaultComparison<int>(staticMethod));
            comparisonMap.Add(typeof(long), new DefaultComparison<long>(staticMethod));
            comparisonMap.Add(typeof(sbyte), new DefaultComparison<sbyte>(staticMethod));
            comparisonMap.Add(typeof(short), new DefaultComparison<short>(staticMethod));
            comparisonMap.Add(typeof(uint), new DefaultComparison<uint>(staticMethod));
            comparisonMap.Add(typeof(ulong), new DefaultComparison<ulong>(staticMethod));
            comparisonMap.Add(typeof(ushort), new DefaultComparison<ushort>(staticMethod));
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
