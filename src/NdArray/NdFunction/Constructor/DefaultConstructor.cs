// <copyright file="DefaultConstructor.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction.Constructor
{
    using NdArrayNet;

    internal sealed class DefaultConstructor<T> : BaseConstructor<T>
    {
        public DefaultConstructor(IStaticMethod staticMethod) : base(staticMethod)
        {
        }
    }
}