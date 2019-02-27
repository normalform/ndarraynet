// <copyright file="FastAccess.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using System;
    using System.Linq;

    public class FastAccess
    {
        public FastAccess(Layout layout)
        {
            NumDiensions = layout.NumDimensions;
            NumElements = layout.NumElements;
            Offset = layout.Offset;
            Shape = layout.Shape;
            Stride = layout.Stride;
        }

        public int NumDiensions { get; }

        public int NumElements { get; }

        public int Offset { get; }

        public int[] Shape { get; }

        public int[] Stride { get; }

        public bool IsPosValid(int[] pos)
        {
            if (pos.Length == NumDiensions)
            {
                var posAndShape = pos.Zip(Shape, (p, s) => new { p, s });
                return posAndShape.All(ps => ps.p >= 0 && ps.p < ps.s);
            }
            else
            {
                return false;
            }
        }

        public int UnCheckedArray(int[] pos)
        {
            int addr = Offset;

            for (var d = 0; d < NumDiensions; d++)
            {
                addr = addr + (pos[d] * Stride[d]);
            }

            return addr;
        }

        public int Addr(int[] pos)
        {
            if (pos.Length != NumDiensions)
            {
                var errorMessage = string.Format("Position {0} has wrong dimensionality for NdArray of shape {1}.", ErrorMessage.ArrayToString(pos), ErrorMessage.ShapeToString(Shape));
                throw new ArgumentOutOfRangeException(errorMessage);
            }

            int addr = Offset;
            for (var d = 0; d < NumDiensions; d++)
            {
                var p = pos[d];
                if (p >= 0 && p < Shape[d])
                {
                    addr = addr + (p * Stride[d]);
                }
                else
                {
                    var errorMessage = string.Format("Position {0} is out of range for NdArray of shape {1}.", ErrorMessage.ArrayToString(pos), ErrorMessage.ShapeToString(Shape));
                    throw new ArgumentOutOfRangeException(errorMessage);
                }
            }

            return addr;
        }
    }
}