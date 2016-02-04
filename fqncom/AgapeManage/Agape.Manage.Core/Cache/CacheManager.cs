using System;
using System.Collections.Generic;
using System.Text;
using Leopard.Util;
using Leopard.Data;
using Leopard.Cache;
using log4net;
using System.Reflection;
using Agape.Manage.Core.Common;

namespace Agape.Manage.Core.Cache
{
    public class CacheManager
    {
        public static object g_Lock;
        public static DateTime g_LastLoadDateTime;

        static CacheManager()
        {
            g_Lock = new object();
            g_LastLoadDateTime = DateTime.MinValue;
        }

        /// <summary>
        /// 加载状态
        /// </summary>
        /// <returns>返回执行结果</returns>
        public static XReturn LoadCache()
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            if (DateTime.Now.CompareTo(g_LastLoadDateTime.AddMinutes(5)) < 0)
            {
                xReturn.SetError("5分钟内不能重复更新状态");
                return xReturn;
            }

            lock (g_Lock)
            {

                if (DateTime.Now.CompareTo(g_LastLoadDateTime.AddMinutes(5)) < 0)
                {
                    xReturn.SetError("5分钟内不能重复更新状态");
                    return xReturn;
                }

                LeopardLog.Info("载入应用所有全局状态开始");

                xSubReturn = _LoadCache();
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn);
                }

                g_LastLoadDateTime = DateTime.Now;

                LeopardLog.Info("载入应用所有全局状态完成");
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 加载状态
        /// </summary>
        /// <returns>返回执行结果</returns>
        private static XReturn _LoadCache()
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            // 加载Leopard全局状态。
            LeopardConfigs.ApplicationType = EApplicationType.Web;
            xSubReturn = LeopardConfigs.LoadConfigs();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载Leopard全局状态失败");

                return xReturn;
            }
            LeopardLog.Info("加载Leopard全局状态成功");

            // 注册数据库管理器
            xSubReturn = DatabaseFactory.RegisterDatabaseManager();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "注册数据库管理器失败");
            }
            LeopardLog.Info("注册数据库管理器成功");

            // 加载Leopard实体配置。
            string filePath = LeopardConfigs.GetLeopardDataPath("Leopard.Entity.xml");
            xSubReturn = EntityConfigCache.Current.Load(filePath, false);
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载Leopard实体配置失败");
                return xReturn;
            }
            LeopardLog.Info("加载Leopard实体配置成功");

            // 加载类封装信息。
            xSubReturn = AssemblyClassConfig.LoadConfig();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载类封装信息失败");
                return xReturn;
            }
            LeopardLog.Info("加载类封装信息成功");

            // 加载汉字拼音。
            xSubReturn = LeopardFactory.GetWordKeyCache().LoadCache();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载汉字拼音失败");
                return xReturn;
            }
            LeopardLog.Info("加载汉字拼音成功");

            // 加载地区信息
            xSubReturn = LeopardFactory.GetAreaInfoCache().LoadCache();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载区域失败");
                return xReturn;
            }
            LeopardLog.Info("加载区域成功");

            // 加载自增序列。
            xSubReturn = LeopardFactory.GetSequenceCache().LoadCache();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载自增序列失败");
                return xReturn;
            }
            LeopardLog.Info("加载自增序列成功");

            // 加载凭证编号。
            xSubReturn = LeopardFactory.GetVoucherNumberCache().LoadCache();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载凭证编号失败");
                return xReturn;
            }
            LeopardLog.Info("加载凭证编号成功");

            // 加载字典。
            xSubReturn = LeopardFactory.GetDictCache().LoadCache();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载字典失败");
                return xReturn;
            }
            LeopardLog.Info("加载字典成功");

            // 加载树字典。
            xSubReturn = LeopardFactory.GetTreeCache().LoadCache();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载树字典失败");
                return xReturn;
            }
            LeopardLog.Info("加载树字典成功");

            // 加载应用程序实体配置。
            filePath = LeopardConfigs.GetLeopardDataPath("Agape.Manage.Entity.xml");
            xSubReturn = EntityConfigCache.Current.Load(filePath, false);
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载应用程序实体配置失败");
                return xReturn;
            }
            LeopardLog.Info("加载应用程序实体配置成功");

            // 加载爱家贝网站配置
            xSubReturn = AgapeManageConfigs.Current.LoadConfigs();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载爱家贝网站配置");
                return xReturn;
            }

            // 加载商品类型。
            xSubReturn = ProductCategoryCache.Current.LoadCache();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载商品类型失败");
                return xReturn;
            }
            LeopardLog.Info("加载商品类型成功");

            // 加载商品品牌。
            xSubReturn = ProductBrandCache.Current.LoadCache();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载商品品牌失败");
                return xReturn;
            }
            LeopardLog.Info("加载商品品牌成功");

            // 加载商品类型关联品牌。
            xSubReturn = ProductCategoryCache.Current.LoadAssoProductBrand();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载商品类型关联品牌失败");
                return xReturn;
            }
            LeopardLog.Info("加载商品类型关联品牌成功");

            // 加载批次缓存。
            xSubReturn = BatchCache.Current.LoadCache();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "加载批次缓存失败");
                return xReturn;
            }
            LeopardLog.Info("加载批次缓存成功");

            return xReturn.ReturnSuccess();
        }
    }
}
