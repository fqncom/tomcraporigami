using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memcached.ClientLibrary;

namespace fqn.ItcastOA.Common
{
    public class MemCacheHelper
    {
        private static readonly MemcachedClient Mc = null;
        static MemCacheHelper()
        {
            string[] serverlist = { "127.0.0.1:11211", "10.0.0.132:11211" };

            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(serverlist);

            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;

            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;

            pool.MaintenanceSleep = 30;
            pool.Failover = true;

            pool.Nagle = false;
            pool.Initialize();

            // 获得客户端实例
            Mc = new MemcachedClient();
            Mc.EnableCompression = false;
        }

        public static object Get(string key)
        {
            return Mc.Get(key);
        }

        public static bool Set(string key, object value)
        {
            return Set(key, value, DateTime.MaxValue);
        }
        public static bool Set(string key, object value, DateTime time)
        
        {
            if (Mc.KeyExists(key))
            {
                Mc.Delete(key);
            }
            return Mc.Set(key, value, time);
        }

        public static bool Delete(string key)
        {
            if (Mc.KeyExists(key))
            {
                Mc.Delete(key);
            }
            return true;
        }

    }
}
