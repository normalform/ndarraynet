// <copyright file="ErrorMessage.cs" company="NdArrayNet">
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
    using System.Linq;
    using System.Text;

    internal class ErrorMessage
    {
        public static string ArrayToString(int[] array)
        {
            var message = string.Format("[{0}]", string.Join(",", array.Select(a => a.ToString())));
            return message;
        }

        public static string ShapeToString(int[] shape)
        {
            var sb = new StringBuilder();
            var lastIndex = shape.Length - 1;

            for (var index = 0; index < shape.Length; index++)
            {
                var shapeValue = shape[index];
                if (shapeValue == SpecialIdx.Fill)
                {
                    sb.Append("...");
                }
                else if (shapeValue == SpecialIdx.NewAxis)
                {
                    sb.Append("NewAxis");
                }
                else if (shapeValue == SpecialIdx.Remainder)
                {
                    sb.Append("Remainder");
                }
                else
                {
                    sb.AppendFormat("{0}", shapeValue);
                }

                if (index != lastIndex)
                {
                    sb.Append(",");
                }
            }

            return "[" + sb.ToString() + "]";
        }

        public static string RangeArgsToString(object[] args)
        {
            var sb = new StringBuilder();
            var lastItemIndex = args.Length - 1;

            for (var index = 0; index < args.Length; index++)
            {
                var arg = args[index];
                if (arg is int value)
                {
                    if (value == SpecialIdx.Fill)
                    {
                        sb.Append("...");
                    }
                    else if (value == SpecialIdx.NewAxis)
                    {
                        sb.Append("NewAxis");
                    }
                    else
                    {
                        sb.Append(value);
                    }
                }
                else if (arg is Range range)
                {
                    if (range.Step == 1)
                    {
                        sb.AppendFormat("{0}:{1}", range.Start, range.Stop);
                    }
                    else if (range.Start == 0 & range.Stop == 0 & range.Step == 0)
                    {
                        sb.AppendFormat(":");
                    }
                    else
                    {
                        sb.AppendFormat("{0}:{1}:{2}", range.Start, range.Stop, range.Step);
                    }
                }
                else if (arg is Elem elem)
                {
                    sb.AppendFormat("{0}", elem.Pos);
                }
                else if (arg is AllFill)
                {
                    sb.AppendFormat("...");
                }
                else if (arg is NewAxis)
                {
                    sb.AppendFormat("NewAxis");
                }
                else
                {
                    sb.Append(arg.ToString());
                }

                if (index != lastItemIndex)
                {
                    sb.Append(", ");
                }
            }

            return "[" + sb.ToString() + "]";
        }

        public static string ObjectsToString(object[] objects)
        {
            var message = string.Format("[{0}]", string.Join(",", objects.Select(o => o.ToString())));
            return message;
        }
    }
}