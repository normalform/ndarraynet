// <copyright file="NumCs.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    public static class NumCs
    {

        public static NdArray<int> Arange(int stop)
        {
            var configManager = ConfigManager.Instance;
            return NdArray<int>.Arange(configManager, 0, stop, 1);
        }

        public static NdArray<int> Arange(int start, int stop, int step)
        {
            var configManager = ConfigManager.Instance;
            return NdArray<int>.Arange(configManager, start, stop, step);
        }

        public static NdArray<double> Arange(double stop)
        {
            var configManager = ConfigManager.Instance;
            return NdArray<double>.Arange(configManager, 0.0, stop, 1.0);
        }

        public static NdArray<double> Arange(double start, double stop, double step)
        {
            var configManager = ConfigManager.Instance;
            return NdArray<double>.Arange(configManager, start, stop, step);
        }

        public static NdArray<T> Ones<T>(int[] shape)
        {
            var configManager = ConfigManager.Instance;
            return NdArray<T>.Ones(configManager, shape);
        }

        public static NdArray<T> OnesLike<T>(NdArray<T> template)
        {
            return NdArray<T>.Ones(template.ConfigManager, template.Shape);
        }

        public static NdArray<T> Zeros<T>(int[] shape)
        {
            var configManager = ConfigManager.Instance;
            return NdArray<T>.Zeros(configManager, shape);
        }

        public static NdArray<T> ZerosLike<T>(NdArray<T> template)
        {
            return NdArray<T>.Zeros(template.ConfigManager, template.Shape);
        }
    }
}