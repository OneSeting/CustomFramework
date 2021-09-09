using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Unibone.Caching
{
    public abstract class UCacheBase : IUCache//, IUCachePlus
    {

        public abstract string ModuleName { get; }

        public abstract T GetBase<T>(string key);


        //public abstract Dictionary<string, string> GetBase(List<string> keys);

        //public abstract Dictionary<string, T> GetBase<T>(List<string> keys);
        public abstract void SetBase<T>(T t, TimeSpan expireTime, bool slide = false);

        public abstract void SetBase<T>(List<T> ts, TimeSpan expireTime, bool slide = false);


        public abstract void SetBase<T>(string key, T t, TimeSpan expireTime, bool slide = false);

        public abstract void SetBase<T>(List<string> keys, List<T> ts, TimeSpan expireTime, bool slide = false);
        public abstract bool IsSetBase(string key);

        //public abstract Dictionary<string, bool> IsSetBase(List<string> keys);

        public abstract bool RemoveBase(string key);
        public abstract bool RemoveBase(List<string> keys);

        public abstract bool RemovePatternBase(string pattern);

        public abstract bool RemovePatternBase(List<string> patterns);


        public T Get<T>(string key)
        {
            return this.GetBase<T>(key);
        }

        //public Dictionary<string, string> Get(List<string> keys)
        //{
        //    throw new NotImplementedException();
        //}

        //public Dictionary<string, T> Get<T>(List<string> keys)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<T> GetAsync<T>(string key)
        {
            return Task.Run(() =>
            {
                return this.GetBase<T>(key);
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
        public void Set<T>(T t, TimeSpan expireTime, bool slide = false)
        {
            this.SetBase(t, expireTime, slide);
        }

        public void Set<T>(List<T> ts, TimeSpan expireTime, bool slide = false)
        {
            this.SetBase(ts, expireTime, slide);
        }

        public void Set<T>(string key, T t, TimeSpan expireTime, bool slide = false)
        {
            this.SetBase(key, t, expireTime, slide);
        }

        public void Set<T>(List<string> keys, List<T> ts, TimeSpan expireTime, bool slide = false)
        {
            this.SetBase(keys, ts, expireTime, slide);
        }
        public bool IsSet(string key)
        {
            return IsSetBase(key);
        }

        //public Dictionary<string, bool> IsSet(List<string> keys)
        //{
        //    throw new NotImplementedException();
        //}

        public bool Remove(string key)
        {
            return this.RemoveBase(key);
        }

        public bool Remove(List<string> keys)
        {
            return this.RemoveBase(keys);
        }

        public bool RemovePattern(string pattern)
        {
            return this.RemovePatternBase(pattern);
        }

        public bool RemovePattern(List<string> patterns)
        {
            return this.RemovePatternBase(patterns);
        }

    }
}
