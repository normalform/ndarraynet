// <copyright file="Primitives.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal static class Primitives
    {
        private static readonly Dictionary<Type, object> ZeroOf = new Dictionary<Type, object>
        {
            { typeof(byte), (byte)0 },
            { typeof(sbyte), (sbyte)0 },
            { typeof(short), (short)0 },        // Int16
            { typeof(ushort), (ushort)0 },      // UInt16
            { typeof(int), 0 },                 // Int32
            { typeof(uint), 0U },               // UInt32
            { typeof(long), 0L },               // Int64
            { typeof(ulong), 0UL },             // UInt64
            { typeof(float), 0.0F },            // Single
            { typeof(double), 0.0 },
            { typeof(decimal), decimal.Zero },
            { typeof(bool), false },
        };

        private static readonly Dictionary<Type, object> OneOf = new Dictionary<Type, object>
        {
            { typeof(byte), (byte)1 },
            { typeof(sbyte), (sbyte)1 },
            { typeof(short), (short)1 },        // Int16
            { typeof(ushort), (ushort)1 },      // UInt16
            { typeof(int), 1 },                 // Int32
            { typeof(uint), 1U },               // UInt32
            { typeof(long), 1L },               // Int64
            { typeof(ulong), 1UL },             // Uint64
            { typeof(float), 1.0F },            // Single
            { typeof(double), 1.0 },
            { typeof(decimal), decimal.One },
            { typeof(bool), true },
        };

        private static readonly Dictionary<Type, object> MinValueOf = new Dictionary<Type, object>
        {
            { typeof(byte), byte.MinValue },
            { typeof(sbyte), sbyte.MinValue },
            { typeof(short), short.MinValue },        // Int16
            { typeof(ushort), ushort.MinValue },      // UInt16
            { typeof(int), int.MinValue },                 // Int32
            { typeof(uint), uint.MinValue },               // UInt32
            { typeof(long), long.MinValue },               // Int64
            { typeof(ulong), ulong.MinValue },             // Uint64
            { typeof(float), float.MinValue },            // Single
            { typeof(double), double.MinValue },
            { typeof(decimal), decimal.MinValue },
            { typeof(bool), false },
        };

        private static readonly Dictionary<Type, object> MaxValueOf = new Dictionary<Type, object>
        {
            { typeof(byte), byte.MaxValue },
            { typeof(sbyte), sbyte.MaxValue },
            { typeof(short), short.MaxValue },        // Int16
            { typeof(ushort), ushort.MaxValue },      // UInt16
            { typeof(int), int.MaxValue },                 // Int32
            { typeof(uint), uint.MaxValue },               // UInt32
            { typeof(long), long.MaxValue },               // Int64
            { typeof(ulong), ulong.MaxValue },             // Uint64
            { typeof(float), float.MaxValue },            // Single
            { typeof(double), double.MaxValue },
            { typeof(decimal), decimal.MaxValue },
            { typeof(bool), true },
        };

        public static T Zero<T>()
        {
            var type = typeof(T);

            if (ZeroOf.ContainsKey(type))
            {
                return (T)ZeroOf[type];
            }

            return (T)GetStaticProperty(typeof(T), "Zero");
        }

        public static T One<T>()
        {
            var type = typeof(T);

            if (OneOf.ContainsKey(type))
            {
                return (T)OneOf[type];
            }

            return (T)GetStaticProperty(typeof(T), "One");
        }

        public static T MaxValue<T>()
        {
            var type = typeof(T);

            if (OneOf.ContainsKey(type))
            {
                return (T)MaxValueOf[type];
            }

            return (T)GetStaticProperty(typeof(T), "MaxValue");
        }

        public static T MinValue<T>()
        {
            var type = typeof(T);

            if (OneOf.ContainsKey(type))
            {
                return (T)MinValueOf[type];
            }

            return (T)GetStaticProperty(typeof(T), "MinValue");
        }

        internal static object GetStaticProperty(Type type, string name)
        {
            var property = type.GetProperty(name, BindingFlags.Public | BindingFlags.Static, null, type, new Type[] { }, new ParameterModifier[] { });
            if (property == null)
            {
                var errorMessage = string.Format("The type {0} must implement the static property {1}", type.Name, name);
                throw new InvalidOperationException(errorMessage);
            }

            return property.GetValue(null);
        }
    }
}