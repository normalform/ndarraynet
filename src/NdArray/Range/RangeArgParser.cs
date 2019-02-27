// <copyright file="RangeArgParser.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using System;
    using System.Linq;

    public static class RangeArgParser
    {
        /// <summary>
        /// Parses arguments to a .NET Item property or GetSlice, SetSlice method to a IRange list.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IRange[] Parse(object[] allArgs)
        {
            IRange[] toRng(object[] args)
            {
                if (args.Length == 0)
                {
                    return new IRange[] { };
                }

                if (args[0] is IRange)
                {
                    return new[] { args[0] as IRange }.Concat(toRng(args.Skip(1).ToArray())).ToArray();
                }
                else if (args.Length >= 3 && args[0] is int && args[1] is int && args[2] is int)
                {
                    var start = (int)args[0];
                    var stop = (int)args[1];
                    var step = (int)args[2];

                    if (start == SpecialIdx.NewAxis || start == SpecialIdx.Fill ||
                        stop == SpecialIdx.NewAxis || stop == SpecialIdx.Fill ||
                        step == SpecialIdx.NewAxis || step == SpecialIdx.Fill)
                    {
                        var errorMessage = string.Format("InvalidArg item Specified items / slices are invalid: {0}.", ErrorMessage.RangeArgsToString(allArgs));
                        throw new InvalidOperationException(errorMessage);
                    }

                    return new[] { RangeFactory.Range(start, stop, step) }.Concat(toRng(args.Skip(3).ToArray())).ToArray();
                }
                else if (args.Length >= 2 && args[0] is int && args[1] is int)
                {
                    var start = (int)args[0];
                    var stop = (int)args[1];

                    if (start == SpecialIdx.NewAxis || start == SpecialIdx.Fill ||
                        stop == SpecialIdx.NewAxis || stop == SpecialIdx.Fill)
                    {
                        var errorMessage = string.Format("InvalidArg item Specified items / slices are invalid: {0}.", ErrorMessage.RangeArgsToString(allArgs));
                        throw new InvalidOperationException(errorMessage);
                    }

                    return new[] { RangeFactory.Range(start, stop) }.Concat(toRng(args.Skip(2).ToArray())).ToArray();
                }
                else if (args[0] is int idx)
                {
                    if (idx == SpecialIdx.NewAxis)
                    {
                        return new[] { RangeFactory.NewAxis }.Concat(toRng(args.Skip(1).ToArray())).ToArray();
                    }
                    else if (idx == SpecialIdx.Fill)
                    {
                        return new[] { RangeFactory.AllFill }.Concat(toRng(args.Skip(1).ToArray())).ToArray();
                    }
                    else
                    {
                        return new[] { RangeFactory.Elem(idx) }.Concat(toRng(args.Skip(1).ToArray())).ToArray();
                    }
                }
                else
                {
                    var errorMessage = string.Format("InvalidArg item Specified items / slices are invalid: {0}.", ErrorMessage.RangeArgsToString(allArgs));
                    throw new InvalidOperationException(errorMessage);
                }
            }

            return toRng(allArgs);
        }
    }
}