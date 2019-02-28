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
            constructorMap.Add(typeof(bool), new BoolConstructor(staticMethod));
            constructorMap.Add(typeof(byte), new ByteConstructor(staticMethod));
            constructorMap.Add(typeof(char), new CharConstructor(staticMethod));
            constructorMap.Add(typeof(decimal), new DecimalConstructor(staticMethod));
            constructorMap.Add(typeof(double), new DoubleConstructor(staticMethod));
            constructorMap.Add(typeof(float), new FloatConstructor(staticMethod));
            constructorMap.Add(typeof(int), new IntConstructor(staticMethod));
            constructorMap.Add(typeof(long), new LongConstructor(staticMethod));
            constructorMap.Add(typeof(sbyte), new SByteConstructor(staticMethod));
            constructorMap.Add(typeof(short), new ShortConstructor(staticMethod));
            constructorMap.Add(typeof(uint), new UIntConstructor(staticMethod));
            constructorMap.Add(typeof(ulong), new ULongConstructor(staticMethod));
            constructorMap.Add(typeof(ushort), new UShortConstructor(staticMethod));
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