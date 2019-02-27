// <copyright file="ScalarPrimitivesRegistry.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using System;
    using System.Collections.Generic;

    internal static class ScalarPrimitivesRegistry
    {
        private static Dictionary<Tuple<Type, Type>, object> instances = new Dictionary<Tuple<Type, Type>, object>();

        public static ScalarPrimitives<T, TC> For<T, TC>()
        {
            lock (instances)
            {
                var types = Tuple.Create(typeof(T), typeof(TC));
                if (instances.ContainsKey(types))
                {
                    return instances[types] as ScalarPrimitives<T, TC>;
                }

                var sp = new ScalarPrimitives<T, TC>();
                instances.Add(types, sp);
                return sp;
            }
        }
    }
}