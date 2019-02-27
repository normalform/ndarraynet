// <copyright file="DataAndLayout.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    public class DataAndLayout<T>
    {
        public DataAndLayout(T[] data, FastAccess fastAccess)
        {
            Data = data;
            FastAccess = fastAccess;
        }

        public T[] Data { get; }

        public FastAccess FastAccess { get; }
    }
}