﻿// <copyright file="List.cs" company="NdArrayNet">
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

    internal static class List
    {
        /// <summary>
        public static int[] Set(int indexToSet, int value, int[] src)
        {
            if (src.Length > 0 && src.Length > indexToSet)
            {
                var newList = new int[src.Length];
                Array.Copy(src, newList, src.Length);
                newList[indexToSet] = value;

                return newList;
            }
            else
            {
                var errorMessage = string.Format("element index {0} out of bounds {1}", indexToSet, src.Length);
                throw new ArgumentOutOfRangeException("indexToSet", errorMessage);
            }
        }

        /// <summary>
        /// Removes element with index elem
        /// </summary>
        public static int[] Without(int indexToRemove, int[] src)
        {
            var srcList = src.ToList();
            srcList.RemoveAt(indexToRemove);

            return srcList.ToArray();
        }

        /// <summary>
        /// insert the specified value at index elem
        /// </summary>
        public static int[] Insert(int indexToInsert, int value, int[] src)
        {
            var srcList = src.ToList();
            srcList.Insert(indexToInsert, value);

            return srcList.ToArray();
        }
    }
}