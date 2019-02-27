// <copyright file="RangeFactory.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    public static class RangeFactory
    {
        public static IRange NewAxis => new NewAxis();

        public static IRange AllFill => new AllFill();

        public static IRange All => new Range(0, 0, 0);

        public static IRange Range(int start, int stop, int step = 1) => new Range(start, stop, step);

        public static IRange Elem(int pos) => new Elem(pos);
    }
}