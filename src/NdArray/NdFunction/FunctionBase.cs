// <copyright file="FunctionBase.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction
{
    using System;
    using NdArrayNet;

    internal class FunctionBase
    {
        internal static readonly Lazy<IStaticMethod> StaticMethod = new Lazy<IStaticMethod>(() => new StaticMethod());

        protected FunctionBase()
        {
        }
    }
}