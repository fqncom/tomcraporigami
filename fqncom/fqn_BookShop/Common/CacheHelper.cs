using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.UI.WebControls;

namespace MyBookShop.Common
{
    public class CacheHelper
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCache(string key, object value)
        {
            SetCache(key, value, DateTime.MaxValue);
        }

        /// <summary>
        /// 重载设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        public static void SetCache(string key, object value, DateTime time)
        {
            Cache cache = HttpRuntime.Cache;
            cache.Add(key, value, null, time, TimeSpan.Zero, CacheItemPriority.Normal, null);
        }

        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            Cache cache = HttpRuntime.Cache;
            return cache[key];
        }

        /// <summary>
        /// 删除一条缓存
        /// </summary>
        /// <param name="key"></param>
        public static void DeleteCache(string key)
        {
            Cache cache = HttpRuntime.Cache;
            if (cache != null)
            {
                cache.Remove(key);
            }
        }

    }
}
