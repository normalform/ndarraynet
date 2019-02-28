// <copyright file="INdConstructor.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArray.NdFunction.Constructor
{
    using NdArrayNet;

    public interface INdConstructor<T>
    {
        NdArray<T> Arange(IConfigManager configManager, T start, T stop, T step);
        NdArray<T> Counting(IConfigManager configManager, int numElements);
        NdArray<T> Empty(IConfigManager configManager, int numDimension);
        NdArray<bool> Falses(IConfigManager configManager, int[] shape);
        NdArray<T> Filled<T>(IConfigManager configManager, int[] shape, T value);
        NdArray<T> Identity(IConfigManager configManager, int size);
        NdArray<T> Ones(IConfigManager configManager, int[] shape);
        NdArray<T> OnesLike(NdArray<T> template);
        NdArray<T> Linspace(IConfigManager configManager, T start, T stop, int numElement);
        NdArray<T> Scalar(IConfigManager configManager, T value);
        NdArray<T> ScalarLike(IFrontend<T> template, T value);
        NdArray<bool> Trues(IConfigManager configManager, int[] shape);
        NdArray<T> Zeros(IConfigManager configManager, int[] shape);
        NdArray<T> ZerosLike(NdArray<T> template);
    }
}