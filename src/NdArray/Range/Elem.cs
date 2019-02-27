// <copyright file="Elem.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed.")]
    public class Elem : RangeBase
    {
        public Elem(int pos) : base(RangeType.Elem)
        {
            Pos = pos;
        }

        public int Pos { get; }
    }
}