using System;
using System.Collections.Generic;
using System.Text;

namespace Unibone.Caching
{
    public static class UCacheExtension
    {
        public static void Set<T>(this IUCache cache, T t, int expireMinutes = 60, bool slide = false)
        {
            cache.Set(t, new TimeSpan(0, expireMinutes, 0), slide);
        }
        public static void Set<T>(this IUCache cache, List<T> ts, int expireMinutes = 60, bool slide = false)
        {
            cache.Set(ts, new TimeSpan(0, expireMinutes, 0), slide);
        }

        public static void Set<T>(this IUCache cache, string key, T t, int expireMinutes = 60, bool slide = false)
        {
            cache.Set(key, t, new TimeSpan(0, expireMinutes, 0), slide);
        }
        public static void Set<T>(this IUCache cache, List<string> keys, List<T> ts, int expireMinutes = 60, bool slide = false)
        {
            cache.Set(keys, ts, new TimeSpan(0, expireMinutes, 0), slide);
        }

        public static void Set<T>(this IUCacheControl cache, T t, int expireMinutes = 60, bool slide = false, string cacheSource = "Default")
        {
            cache.Set(t, new TimeSpan(0, expireMinutes, 0), slide, cacheSource);
        }
        public static void Set<T>(this IUCacheControl cache, List<T> ts, int expireMinutes = 60, bool slide = false, string cacheSource = "Default")
        {
            cache.Set(ts, new TimeSpan(0, expireMinutes, 0), slide, cacheSource);
        }

        public static void Set<T>(this IUCacheControl cache, string key, T t, int expireMinutes = 60, bool slide = false, string cacheSource = "Default")
        {
            cache.Set(key, t, new TimeSpan(0, expireMinutes, 0), slide, cacheSource);
        }
        public static void Set<T>(this IUCacheControl cache, List<string> keys, List<T> ts, int expireMinutes = 60, bool slide = false, string cacheSource = "Default")
        {
            cache.Set(keys, ts, new TimeSpan(0, expireMinutes, 0), slide, cacheSource);
        }


    }
}
