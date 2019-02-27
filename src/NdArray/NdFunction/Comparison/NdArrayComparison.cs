// <copyright file="NdArrayComparison.cs" company="NdArrayNet">
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

namespace NdArray.NdFunction.Comparison
{
    using System;
    using System.Collections.Generic;
    using NdArrayNet;

    internal static class NdArrayComparison<T>
    {
        public static INdArrayComparison<T> Instance(NdArrayComparison comparison) => comparison.GetComparison<T>();
    }

    internal class NdArrayComparison
    {
        private readonly object mapLock = new object();

        private readonly IStaticMethod StaticMethod;

        private readonly Dictionary<Type, object> comparisonMap = new Dictionary<Type, object>();

        public NdArrayComparison(IStaticMethod staticMethod)
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

        public INdArrayComparison<T> GetComparison<T>()
        {
            var type = typeof(T);
            lock(mapLock)
            {
                if (comparisonMap.ContainsKey(type))
                {
                    return comparisonMap[type] as INdArrayComparison<T>;
                }
            }

            return null;
        }
    }
}
