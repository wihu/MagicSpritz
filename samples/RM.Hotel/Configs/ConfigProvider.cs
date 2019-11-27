using System;
using System.Collections.Generic;

namespace RM.Hotel
{
    public interface IConfig
    {
    }

    public class ConfigProvider
    {

        private Dictionary<string, IConfig> _configs = new Dictionary<string, IConfig>();

        public void Add<T>(string name, T config) where T : class, IConfig
        {
            _configs.Add(name, config);
        }

        public T Get<T>(string name) where T : class, IConfig
        {
            _configs.TryGetValue(name, out var config);
            return config as T;
        }
    }
}
