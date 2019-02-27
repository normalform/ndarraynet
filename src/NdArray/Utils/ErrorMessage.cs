// <copyright file="ErrorMessage.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using System.Linq;
    using System.Text;

    internal static class ErrorMessage
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
                    else if (range.Start == 0 && range.Stop == 0 && range.Step == 0)
                    {
                        sb.Append(":");
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
                    sb.Append("...");
                }
                else if (arg is NewAxis)
                {
                    sb.Append("NewAxis");
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