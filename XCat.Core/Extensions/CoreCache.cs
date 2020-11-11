//using Microsoft.Extensions.Caching.Memory;
//using System;
//using System.Collections.Generic;
//using System.Text;

//public static class CoreCache
//{
//    public static T Get<T>(this IMemoryCache cache, string key, Func<T> func, DateTime? expires = null)
//    {
//        var res = cache.Get<T>(key);
//        if (res == null)
//        {
//            res = func();
//            if(res !=null)
//            {
//                cache.Set(key, res, new DateTimeOffset(expires == null ? Util.GetTomorrow() : expires.Value));
//            }
//        }
//        return res;
//    }
//    public static void Remove<T>(this IMemoryCache cache, string key)
//    {
//        cache.Remove(key);
//    }
//}