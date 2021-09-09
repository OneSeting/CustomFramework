using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching.UMemory
{
    public class UCacheMemory : UCacheBase, IUCache
    {
        public override string ModuleName { get; } = UCacheSource.UCacheSystemMemory.ToString();
        private UMemoryCaching MemoryCaching = UMemoryCaching.Default;
        public override T GetBase<T>(string key)
        {
            return MemoryCaching.Get<T>(key);
        }

        public override bool IsSetBase(string key)
        {
            return MemoryCaching.IsSet(key);
        }
        public override void SetBase<T>(T t, TimeSpan expireTime, bool slide = false)
        {
            throw new NotImplementedException();
        }

        public override void SetBase<T>(List<T> ts, TimeSpan expireTime, bool slide = false)
        {

            throw new NotImplementedException();
        }

        public override void SetBase<T>(string key, T t, TimeSpan expireTime, bool slide = false)
        {
            MemoryCaching.SetOrUpdate(key, t, expireTime, slide);
        }

        public override void SetBase<T>(List<string> keys, List<T> ts, TimeSpan expireTime, bool slide = false)
        {
            MemoryCaching.SetOrUpdate(keys, ts, expireTime, slide);
        }
        public override bool RemoveBase(string key)
        {
            return MemoryCaching.Remove(key);
        }

        public override bool RemoveBase(List<string> keys)
        {
            for (int i = 0; i < keys.Count; i++)
                MemoryCaching.Remove(keys[i]);
            return true;
        }

        public override bool RemovePatternBase(string pattern)
        {
            return MemoryCaching.RemovePattern(pattern);
        }

        public override bool RemovePatternBase(List<string> patterns)
        {
            for (int i = 0; i < patterns.Count; i++)
                MemoryCaching.RemovePattern(patterns[i]);
            return true;
        }


    }
}
