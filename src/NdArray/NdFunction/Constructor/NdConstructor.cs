// <copyright file="NdConstructor.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction.Constructor
{
    using System;
    using System.Collections.Generic;
    using NdArrayNet;

    internal class NdConstructor
    {
        private readonly object mapLock = new object();

        private readonly IStaticMethod StaticMethod;

        private readonly Dictionary<Type, object> constructorMap = new Dictionary<Type, object>();

        public NdConstructor(IStaticMethod staticMethod)
        {
            StaticMethod = staticMethod;
            constructorMap.Add(typeof(bool), new DefaultConstructor<bool>(staticMethod));
            constructorMap.Add(typeof(byte), new DefaultConstructor<byte>(staticMethod));
            constructorMap.Add(typeof(char), new DefaultConstructor<char>(staticMethod));
            constructorMap.Add(typeof(decimal), new DefaultConstructor<decimal>(staticMethod));
            constructorMap.Add(typeof(double), new DefaultConstructor<double>(staticMethod));
            constructorMap.Add(typeof(float), new DefaultConstructor<float>(staticMethod));
            constructorMap.Add(typeof(int), new DefaultConstructor<int>(staticMethod));
            constructorMap.Add(typeof(long), new DefaultConstructor<long>(staticMethod));
            constructorMap.Add(typeof(sbyte), new DefaultConstructor<sbyte>(staticMethod));
            constructorMap.Add(typeof(short), new DefaultConstructor<short>(staticMethod));
            constructorMap.Add(typeof(uint), new DefaultConstructor<uint>(staticMethod));
            constructorMap.Add(typeof(ulong), new DefaultConstructor<ulong>(staticMethod));
            constructorMap.Add(typeof(ushort), new DefaultConstructor<ushort>(staticMethod));
        }

        public INdConstructor<T> Get<T>()
        {
            var type = typeof(T);
            lock (mapLock)
            {
                if (constructorMap.ContainsKey(type))
                {
                    return constructorMap[type] as INdConstructor<T>;
                }
            }

            return null;
        }
    }
}