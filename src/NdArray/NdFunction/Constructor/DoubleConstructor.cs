// <copyright file="DoubleConstructor.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction.Constructor
{
    using NdArrayNet;

    internal sealed class DoubleConstructor : BaseConstructor<double>
    {
        public DoubleConstructor(IStaticMethod staticMethod) : base(staticMethod)
        {
        }
    }
}