// <copyright file="PosIter.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
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