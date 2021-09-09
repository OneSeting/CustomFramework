using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching.ModuleConfiguration
{
    public class UCacheCollection : UModuleCollection
    {
        public IUCache this[string name]
        {
            get
            {
                name = name.ToLower();
                if (string.IsNullOrEmpty(name))
                    return null;
                name = name.ToLower();
                if (collection.ContainsKey(name))
                    return (IUCache)Activator.CreateInstance(collection[name]);
                return null;
            }
        }
        public override bool Add(Type type)
        {
            Type[] interfaces = type.GetInterfaces();
            bool judge = false;
            for (int i = 0; i < interfaces.Length; i++)
            {
                if (interfaces[i] == typeof(IUCache))
                {
                    judge = true;
                    break;
                }
            }
            if (type.BaseType == typeof(UCacheBase) || judge)
            {
                IUCache cache = (IUCache)Activator.CreateInstance(type);
                if (string.IsNullOrEmpty(cache.ModuleName))
                    return false;
                if (collection.ContainsKey(cache.ModuleName.ToLower()))
                    return false;
                collection.Add(cache.ModuleName.ToLower(), type);
                return true;
            }
            return false;
        }
        public override bool Add<T>()
        {
            Type type = typeof(T);
            return this.Add(type);
        }
    }
}
