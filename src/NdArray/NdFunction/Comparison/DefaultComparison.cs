// <copyright file="DefaultComparison.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction.Comparison
{
    using NdArrayNet;

    internal sealed class DefaultComparison<T> : BaseComparison<T>
    {
        public DefaultComparison(IStaticMethod staticMethod) : base(staticMethod)
        {
        }
    }
}