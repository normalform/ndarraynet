﻿// <copyright file="IntConstructor.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction.Constructor
{
    using NdArrayNet;

    internal sealed class IntConstructor : BaseConstructor<int>
    {
        public IntConstructor(IStaticMethod staticMethod) : base(staticMethod)
        {
        }
    }
}