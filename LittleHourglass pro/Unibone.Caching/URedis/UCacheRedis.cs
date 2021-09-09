using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Unibone.Caching.URedis
{
    internal class UCacheRedis : UCacheBase, IUCache
    {
        public UCacheRedis()
        {
            this.redisConfig = UCacheGlobalSetting.redisConfigCollection[UCacheSource.UCacheSystemRedis.ToString()];
            this.redisClient = new URedisClient(redisConfig);

        }
        private URedisConfig redisConfig;
        private URedisClient redisClient;
        public override string ModuleName { get; } = UCacheSource.UCacheSystemRedis.ToString();

        public override T GetBase<T>(string key)
        {
            return redisClient.Get<T>(key);
        }
        public override void SetBase<T>(T t, TimeSpan expireTime, bool slide = false)
        {

        }

        public override void SetBase<T>(List<T> ts, TimeSpan expireTime, bool slide = false)
        {
            throw new NotImplementedException();
        }

        public override void SetBase<T>(string key, T t, TimeSpan expireTime, bool slide = false)
        {
            redisClient.SetOrUpdate(key, t, expireTime, slide);
        }

        public override void SetBase<T>(List<string> keys, List<T> ts, TimeSpan expireTime, bool slide = false)
        {
            redisClient.SetOrUpdate(keys, ts, expireTime, slide);
        }
        public override bool IsSetBase(string key)
        {
            return redisClient.IsSet(key);
        }

        public override bool RemoveBase(string key)
        {
            return redisClient.Remove(key);
        }

        public override bool RemoveBase(List<string> keys)
        {
            return redisClient.Remove(keys);
        }

        public override bool RemovePatternBase(string pattern)
        {
            return redisClient.RemovePattern(pattern);
        }

        public override bool RemovePatternBase(List<string> patterns)
        {
            return true;
        }

    }
}
