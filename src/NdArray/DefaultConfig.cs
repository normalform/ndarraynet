// <copyright file="DefaultConfig.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using NdArray;
    using NdArray.NdFunction.Comparison;

    internal sealed class DefaultConfig<T> : IConfig<T>
    {
        private static DefaultConfig<T> instance;

        private readonly NdArrayComparison ndArrayComparison;

        public IDevice Device => HostDevice.Instance;

        public INdArrayComparison<T> ComparisonFunction { get; }

        private DefaultConfig(IStaticMethod staticMethod = null)
        {
            IStaticMethod newStaticMethod;
            if (staticMethod == null)
            {
                newStaticMethod = new StaticMethod();
            }
            else
            {
                newStaticMethod = staticMethod;
            }

            ndArrayComparison = new NdArrayComparison(newStaticMethod);
            ComparisonFunction = ndArrayComparison.GetComparison<T>();
        }

        public static DefaultConfig<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DefaultConfig<T>();
                }

                return instance;
            }
        }

        public IStorage<T> Create(Layout layout)
        {
            var storage = Device.Create<T>(layout.NumElements);
            var backend = storage.Backend(layout);

            return storage;
        }
    }
}