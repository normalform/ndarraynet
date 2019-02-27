// <copyright file="List.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
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
                var errorMessage = string.Format("Element index {0} out of bounds {1}", indexToSet, src.Length);
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