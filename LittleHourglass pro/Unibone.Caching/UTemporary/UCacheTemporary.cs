using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching.UTemporary
{
    public class UCacheTemporary : UCacheBase, IUCache
    {
        public override string ModuleName { get; }

        public override T GetBase<T>(string key)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override void SetBase<T>(List<string> keys, List<T> ts, TimeSpan expireTime, bool slide = false)
        {
            throw new NotImplementedException();
        }
        public override bool IsSetBase(string key)
        {
            throw new NotImplementedException();
        }

        public override bool RemoveBase(string key)
        {
            throw new NotImplementedException();
        }

        public override bool RemoveBase(List<string> keys)
        {
            throw new NotImplementedException();
        }

        public override bool RemovePatternBase(string pattern)
        {
            throw new NotImplementedException();
        }

        public override bool RemovePatternBase(List<string> patterns)
        {
            throw new NotImplementedException();
        }

      
    }
}
