// <copyright file="Permutation.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Permutation
    {
        /// <summary>
        /// true if the given list is a permutation of the numbers 0 to perm.Length-1
        /// </summary>
        /// <param name="perm"></param>
        /// <returns></returns>
        public static bool Is(int[] perm)
        {
            var s0 = new HashSet<int>(perm);
            var s1 = new HashSet<int>(Enumerable.Range(0, perm.Length));

            return s0.SetEquals(s1);
        }
    }
}