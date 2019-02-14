// <copyright file="Range.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//
// 1. Redistributions of source code must retain the above copyright notice, this
//    list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
// The views and conclusions contained in the software and documentation are those
// of the authors and should not be interpreted as representing official policies,
// either expressed or implied, of the NdArrayNet project.
// </copyright>

namespace NdArrayNet
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public enum RangeType
    {
        Range,
        Elem,
        NewAxis,
        AllFill,
    }

    public interface IRange
    {
        RangeType Type { get; }
    }

    public static class RangeFactory
    {
        public static IRange NewAxis => new NewAxis();

        public static IRange AllFill => new AllFill();

        public static IRange All => new Range(0, 0, 0);

        public static IRange Range(int start, int stop, int step = 1) => new Range(start, stop, step);

        public static IRange Elem(int pos) => new Elem(pos);
    }

    public static class RangeArgParser
    {
        /// <summary>
        /// Converts arguments to a .NET Item property or GetSlice, SetSlice method to a IRange list.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IRange[] OfItemOrSliceArgs(object[] allArgs)
        {
            IRange[] toRng(object[] args)
            {
                if (args.Length == 0)
                {
                    return new IRange[] { };
                }

                if (args.GetType() == typeof(IRange[]))
                {
                    return args as IRange[];
                }
                else if (args.Length > 3 && args[0] is int && args[1] is int && args[2] is int)
                {
                    var start = (int)args[0];
                    var stop = (int)args[1];
                    var step = (int)args[2];

                    if (start == SpecialIdx.NewAxis || start == SpecialIdx.Fill ||
                        stop == SpecialIdx.NewAxis || stop == SpecialIdx.Fill ||
                        step == SpecialIdx.NewAxis || step == SpecialIdx.Fill)
                    {
                        var msg = string.Format("invalidArg item Specified items / slices are invalid: {0}.", allArgs);
                        throw new InvalidOperationException(msg);
                    }

                    return new[] { RangeFactory.Range(start, stop, step) }.Concat(toRng(args.Skip(3).ToArray())).ToArray();
                }
                else if (args.Length > 2 && args[0] is int && args[1] is int)
                {
                    var start = (int)args[0];
                    var stop = (int)args[1];

                    if (start == SpecialIdx.NewAxis || start == SpecialIdx.Fill ||
                        stop == SpecialIdx.NewAxis || stop == SpecialIdx.Fill)
                    {
                        var msg = string.Format("invalidArg item Specified items / slices are invalid: {0}.", allArgs);
                        throw new InvalidOperationException(msg);
                    }

                    return new[] { RangeFactory.Range(start, stop) }.Concat(toRng(args.Skip(2).ToArray())).ToArray();
                }
                else if (args[0] is int)
                {
                    var idx = (int)args[0];
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
                    var msg = string.Format("invalidArg item Specified items / slices are invalid: {0}.", allArgs);
                    throw new InvalidOperationException(msg);
                }
            }

            return toRng(allArgs);
        }
    }

    public abstract class RangeBase : IRange
    {
        protected RangeBase(RangeType type)
        {
            Type = type;
        }

        public RangeType Type { get; }
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

    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed.")]
    public class Elem : RangeBase
    {
        public Elem(int pos) : base(RangeType.Elem)
        {
            Pos = pos;
        }

        public int Pos { get; }
    }

    public class NewAxis : RangeBase
    {
        public NewAxis() : base(RangeType.NewAxis)
        {
        }
    }

    public class AllFill : RangeBase
    {
        public AllFill() : base(RangeType.AllFill)
        {
        }
    }

    internal static class SpecialIdx
    {
        /// <summary>
        /// For slicing: inserts a new axis of size one.
        /// </summary>
        public static readonly int NewAxis = int.MinValue + 1;

        /// <summary>
        /// For slicing: fills all remaining axes with size one.
        /// Cannot be used together with NewAxis.
        /// </summary>
        public static readonly int Fill = int.MinValue + 2;

        /// <summary>
        /// For reshape: remainder, so that number of elements stays constant.
        /// </summary>
        public static readonly int Remainder = int.MinValue + 3;

        /// <summary>
        /// For search: value was not found.
        /// </summary>
        public static readonly int NotFound = int.MinValue + 4;
    }
}