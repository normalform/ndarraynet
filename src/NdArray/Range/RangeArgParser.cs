// <copyright file="RangeArgParser.cs" company="NdArrayNet">
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

                if (typeof(IRange).IsInstanceOfType(args[0]))
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
                        var msg = string.Format("invalidArg item Specified items / slices are invalid: {0}.", allArgs);
                        throw new InvalidOperationException(msg);
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
}