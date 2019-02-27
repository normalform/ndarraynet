// <copyright file="NdArrayBase.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using System;

    public class NdArrayBase
    {
        internal static readonly Lazy<IStaticMethod> StaticMethod = new Lazy<IStaticMethod>(() => new StaticMethod());

        protected NdArrayBase()
        {
        }
    }
}