using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching.ModuleConfiguration
{
    public class UCachePlusCollection : UModuleCollection
    {
        public IUCachePlus this[string name]
        {
            get
            {
                name = name.ToLower();
                if (string.IsNullOrEmpty(name))
                    return null;
                name = name.ToLower();
                if (collection.ContainsKey(name))
                    return (IUCachePlus)Activator.CreateInstance(collection[name]);
                return null;
            }
        }
        public override bool Add(Type type)
        {
            Type[] interfaces = type.GetInterfaces();
            bool judge = false;
            for (int i = 0; i < interfaces.Length; i++)
            {
                if (interfaces[i] == typeof(IUCachePlus))
                {
                    judge = true;
                    break;
                }
            }
            if (judge)
            {
                IUCachePlus cache = (IUCachePlus)Activator.CreateInstance(type);
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
