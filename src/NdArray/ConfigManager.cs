// <copyright file="ConfigManager.cs" company="NdArrayNet">
// Copyright(c) 2019, Jaeho Kim
// All rights reserved. 
// Licensed under the BSD 2-Clause License; See the LICENSE file.
// </copyright>

namespace NdArrayNet
{
    using System;
    using System.Collections.Generic;

    internal sealed class ConfigManager : IConfigManager
    {
        private readonly object configLock = new object();

        private static IConfigManager instance;

        private Dictionary<Type, object> configs;

        public static IConfigManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigManager();
                }

                return instance;
            }
        }

        private ConfigManager()
        {
            configs = new Dictionary<Type, object>();
        }

        public IConfig<T> GetConfig<T>()
        {
            var type = typeof(T);

            lock(configLock)
            {
                if (!configs.ContainsKey(type))
                {
                    configs.Add(type, DefaultConfig<T>.Instance);
                }
            }

            return configs[type] as IConfig<T>;
        }
    }
}