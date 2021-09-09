using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching.URedis
{
    public class URedisConfigCollection
    {
        internal Dictionary<string, URedisConfig> redisConfig = new Dictionary<string, URedisConfig>();
        internal Dictionary<string, UValueItemConfig> genericConfig = new Dictionary<string, UValueItemConfig>();
        /// <summary>
        /// 获取不到就会使用默认的127.0.0.1:6379
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public URedisConfig this[string moduleName]
        {
            get
            {
                moduleName = moduleName.ToLower();
                if (redisConfig.ContainsKey(moduleName))
                    return redisConfig[moduleName];
                return new URedisConfig();
            }
        }
        public UValueItemConfig GetItemConfig(string key)
        {
            key = key.ToLower();
            if (genericConfig.ContainsKey(key))
                return genericConfig[key];
            foreach (var item in genericConfig)
            {
                if (key.Contains(item.Key))
                {
                    return item.Value;
                }
            }
            return null;
        }
        /// <summary>
        /// 添加空的会抛出异常，请不要添加空的配置对象
        /// </summary>
        /// <param name="config"></param>
        public void Add(URedisConfig config)
        {
            if (config == null)
                throw new ArgumentNullException();
            if (redisConfig.ContainsKey(config.ModuleName.ToLower()))
                redisConfig[config.ModuleName.ToLower()] = config.Clone();
            else
                redisConfig.Add(config.ModuleName.ToLower(), config.Clone());
        }

        /// <summary>
        /// 配置具体的类型缓存在哪里db 如果没有配置那么缓存在默认的<see cref="URedisConfig"/>配置里面
        /// </summary>
        /// <param name="config"></param>
        public void Add(UValueItemConfig config)
        {
            if (config == null)
                throw new ArgumentNullException();
            if (genericConfig.ContainsKey(config.Key))
                genericConfig[config.Key] = config.Clone();
            else
                genericConfig.Add(config.Key, config.Clone());
        }
    }
}
