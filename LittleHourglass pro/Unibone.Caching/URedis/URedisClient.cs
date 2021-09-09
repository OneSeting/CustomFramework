using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unibone.Caching.Utilities;

namespace Unibone.Caching.URedis
{
    public class URedisClient
    {
        private IDatabase database;
        private URedisConfig redisConfig;
        public URedisClient(URedisConfig config)
        {
            this.redisConfig = config.Clone();
        }

        public T Get<T>(string key)
        {
            key = Initialize(key);
            RedisValue redisValue = database.StringGet(key);
            if (redisValue.IsNull)
                return default;
            UCacheObject cacheObject = JsonTool.DeserializeObject<UCacheObject>(redisValue.ToString());
            return (T)cacheObject.CacheValue.Value;
        }
        public void SetOrUpdate<T>(string key, T value, TimeSpan expireTime, bool slide = false)
        {
            if (CheckKey(key))
                return;
            key = Initialize(key);
            UCacheObject cacheObject = new UCacheObject();
            cacheObject.CacheInfo = new UCacheInfo(expireTime);
            cacheObject.CacheValue = new UCacheValue(key, value);
            database.StringSet(key, JsonTool.SersializeObject(cacheObject), expireTime);
        }
        public void SetOrUpdate<T>(List<string> keys, List<T> values, TimeSpan expireTime, bool slide = false)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                SetOrUpdate(keys[i], values[i], expireTime, slide);
            }
        }
        public bool IsSet(string key)
        {
            if (CheckKey(key))
                return false;
            key = Initialize(key);
            return Get<object>(key) != null;
        }

        public bool Remove(string key)
        {
            key = Initialize(key);
            return database.KeyDelete(key);
        }
        public bool Remove(List<string> keys)
        {
            RedisKey[] redisKeys = new RedisKey[keys.Count];
            for (int i = 0; i < keys.Count; i++)
            {
                Remove(keys[i]);
                //redisKeys[i] = keys[i];
            }
            return true;
        }
        public bool RemovePattern(string pattern)
        {
            pattern = Initialize(pattern);
            IServer _server = redisConfig.Connection.GetServer(redisConfig.Connection.GetEndPoints()[0]); //默认一个服务器
            var keys = _server.Keys(database: database.Database, pattern: pattern); //StackExchange.Redis 会根据redis版本决定用keys还是scan(>2.8) 
            database.KeyDelete(keys.ToArray()); //删除一组key
            return true;
        }


        bool CheckKey(string key)
        {
            return string.IsNullOrEmpty(key);
        }
        private string Initialize(string key)
        {
            GetDatabase(key);
            return GetPrefixKey(key);
        }
        private string GetPrefixKey(string key)
        {
            if (!string.IsNullOrEmpty(redisConfig.KeyPrefix))
                return redisConfig.KeyPrefix + key;
            return key;
        }
        private UValueItemConfig GetItemConfig(string key)
        {
            return UCacheGlobalSetting.redisConfigCollection.GetItemConfig(key);
        }
        private List<UValueItemConfig> GetItemConfig(List<string> keys)
        {
            List<UValueItemConfig> configs = new List<UValueItemConfig>();
            for (int i = 0; i < keys.Count; i++)
            {
                configs.Add(GetItemConfig(keys[i]));
            }
            return configs;
        }
        private void GetDatabase(string key)
        {
            UValueItemConfig config = GetItemConfig(key);
            if (config != null)
            {
                redisConfig.DefaultDatabase = config.Database;
                if (config.UseDefaultPrefix == false && !string.IsNullOrEmpty(config.Prefix))
                    redisConfig.KeyPrefix = config.Prefix;
            }
            if (database == null)
                database = redisConfig.Connection.GetDatabase(redisConfig.DefaultDatabase);
        }

    }
}
