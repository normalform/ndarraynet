// <copyright file="FastAccess.cs" company="NdArrayNet">
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

    public class FastLayout
    {
        public FastLayout(Layout layout)
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
                addr = addr + pos[d] * Stride[d];
            }

            return addr;
        }

        public int Addr(int[] pos)
        {
            if (pos.Length != NumDiensions)
            {
                var msg = string.Format("Position {0} has wrong dimensionality for NdArray of shape {1}.", pos, Shape);
                throw new IndexOutOfRangeException(msg);
            }

            int addr = Offset;
            for (var d = 0; d < NumDiensions; d++)
            {
                var p = pos[d];
                if (0 <= p && p < Shape[d])
                {
                    addr = addr + p * Stride[d];
                }
                else
                {
                    var msg = string.Format("Position {0} is out of range for NdArray of shape {1}.", pos, Shape);
                    throw new IndexOutOfRangeException(msg);
                }
            }

            return addr;
        }
    }
}