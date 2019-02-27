// <copyright file="Range.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using System.Diagnostics.CodeAnalysis;

    public enum RangeType
    {
        Range,
        Elem,
        NewAxis,
        AllFill,
    }

    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed.")]
    public class Range : RangeBase
    {
        public Range(int start, int stop, int step) : base(RangeType.Range)
        {
            Start = start;
            Stop = stop;
            Step = step;
        }

        public int Start { get; }

        public int Stop { get; }

        public int Step { get; }
    }
}