// <copyright file="PosIter.cs" company="NdArrayNet">
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
    using System.Runtime.CompilerServices;

    internal class PosIter
    {
        public PosIter(
            FastAccess fl,
            int[] startPos = null,
            int fromDim = 0,
            int toDim = int.MinValue)
        {
            if (startPos == null)
            {
                startPos = new int[fl.NumDiensions];
            }

            if (toDim == int.MinValue)
            {
                toDim = fl.NumDiensions - 1;
            }

            Pos = new int[startPos.Length];
            startPos.CopyTo(Pos, 0);

            Shape = fl.Shape;
            Stride = fl.Stride;

            Addr = fl.UnCheckedArray(startPos);
            Active = Enumerable.Range(0, fl.NumDiensions + 1).All(d => !(fromDim <= d && d <= toDim) || (startPos[d] >= 0 && startPos[d] < Shape[d]));
            FromDim = fromDim;
            ToDim = toDim;
        }

        public int[] Pos { get; }

        public int Addr { get; private set; }

        public bool Active { get; private set; }

        public int[] Shape { get; }

        public int[] Stride { get; }

        public int FromDim { get; }

        public int ToDim { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MoveNext()
        {
            // try incrementing starting from last axis
            var increment = true;
            var d = ToDim;

            while (increment && d >= FromDim)
            {
                if (Pos[d] == (Shape[d] - 1))
                {
                    // was last element of that axis
                    Addr = Addr - (Pos[d] * Stride[d]);
                    Pos[d] = 0;
                    d = d - 1;
                }
                else
                {
                    // can increment this axis
                    Addr = Addr + Stride[d];
                    Pos[d] = Pos[d] + 1;
                    increment = false;
                }
            }

            // if we tried to increment past first axis, then iteration finished
            if (d < FromDim)
            {
                Active = false;
            }
        }
    }
}