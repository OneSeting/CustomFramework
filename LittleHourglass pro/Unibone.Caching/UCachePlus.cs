using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unibone.Caching.ModuleConfiguration;

namespace Unibone.Caching
{
    internal class UCachePlus : IUCachePlus
    {
        public string ModuleName { get; } = UCachePlusSource.UCacheSystemPlus.ToString();
        private IUCache temporaryCache = UModuleContext.cacheCollection[UCacheSource.UCacheSystemTemporary.ToString()];
        private IUCache memoryCache = UModuleContext.cacheCollection[UCacheSource.UCacheSystemMemory.ToString()];
        private IUCache redisCache = UModuleContext.cacheCollection[UCacheSource.UCacheSystemRedis.ToString()];

        public virtual T Get<T>(string key)
        {
            if (temporaryCache.IsSet(key))
                return temporaryCache.Get<T>(key);
            if (memoryCache.IsSet(key))
                return memoryCache.Get<T>(key);
            if (redisCache.IsSet(key))
                return redisCache.Get<T>(key);
            return default;
        }

        //public Dictionary<string, string> Get(List<string> keys)
        //{
        //    return new Dictionary<string, string>();
        //}

        //public Dictionary<string, T> Get<T>(List<string> keys)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual Task<T> GetAsync<T>(string key)
        {
            return Task.Run(() =>
            {
                return this.Get<T>(key);
            });
        }

        //public Task<Dictionary<string, string>> GetAsync(List<string> keys)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Dictionary<string, T>> GetAsync<T>(List<string> keys)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual bool IsSet(string key)
        {
            bool judge = false;
            judge = temporaryCache.IsSet(key);
            judge = judge ? judge : memoryCache.IsSet(key);
            return judge ? judge : redisCache.IsSet(key);
        }

        //public Dictionary<string, bool> IsSet(List<string> keys)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual bool Remove(string key)
        {
            if (temporaryCache.IsSet(key))
                temporaryCache.Remove(key);
            if (memoryCache.IsSet(key))
                memoryCache.Remove(key);
            if (redisCache.IsSet(key))
                redisCache.Remove(key);
            return true;
        }

        public virtual bool Remove(List<string> keys)
        {
            temporaryCache.Remove(keys);
            memoryCache.Remove(keys);
            redisCache.Remove(keys);
            return true;
        }

        public virtual bool RemovePattern(string pattern)
        {
            temporaryCache.RemovePattern(pattern);
            memoryCache.RemovePattern(pattern);
            redisCache.RemovePattern(pattern);
            return true;
        }

        public virtual bool RemovePattern(List<string> patterns)
        {
            temporaryCache.RemovePattern(patterns);
            memoryCache.RemovePattern(patterns);
            redisCache.RemovePattern(patterns);
            return true;
        }

        public virtual void Set<T>(T t, TimeSpan expireTime, bool slide = false)
        {
            temporaryCache.Set(t, expireTime, slide);
            memoryCache.Set(t, expireTime, slide);
            redisCache.Set(t, expireTime, slide);
        }

        public virtual void Set<T>(List<T> ts, TimeSpan expireTime, bool slide = false)
        {
            temporaryCache.Set(ts, expireTime, slide);
            memoryCache.Set(ts, expireTime, slide);
            redisCache.Set(ts, expireTime, slide);
        }

        public void Set<T>(string key, T t, TimeSpan expireTime, bool slide = false)
        {
            temporaryCache.Set(key, t, expireTime, slide);
            memoryCache.Set(key, t, expireTime, slide);
            redisCache.Set(key, t, expireTime, slide);
        }

        public void Set<T>(List<string> keys, List<T> ts, TimeSpan expireTime, bool slide = false)
        {
            temporaryCache.Set(keys, ts, expireTime, slide);
            memoryCache.Set(keys, ts, expireTime, slide);
            redisCache.Set(keys, ts, expireTime, slide);
        }
    }
}
