// <copyright file="Primitives.cs" company="NdArrayNet">
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

namespace NdArrayNet
{
    using System;
    using System.Collections.Generic;

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
        };

        public static T Zero<T>() => (T)ZeroOf[typeof(T)];

        public static T One<T>() => (T)OneOf[typeof(T)];
    }
}