// <copyright file="ShortConstructor.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction.Constructor
{
    using NdArrayNet;

    internal sealed class ShortConstructor : BaseConstructor<short>
    {
        public ShortConstructor(IStaticMethod staticMethod) : base(staticMethod)
        {
        }
    }
}