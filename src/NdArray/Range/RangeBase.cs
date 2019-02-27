// <copyright file="RangeBase.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    public abstract class RangeBase : IRange
    {
        protected RangeBase(RangeType type)
        {
            Type = type;
        }

        public RangeType Type { get; }
    }
}