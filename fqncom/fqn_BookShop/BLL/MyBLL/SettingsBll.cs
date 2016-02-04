using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyBookShop.BLL
{
    public partial class SettingsBll
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MyBookShop.Model.Settings GetModel(string key)
        {
            //从缓存中拿对象
            object cacheObj = Common.CacheHelper.GetCache("settings_" + key);
            if (cacheObj == null)//如果没有，则创建
            {
                Model.Settings settingObj = dal.GetModel(key);
                Common.CacheHelper.SetCache("settings_" + key, settingObj);
                return settingObj;
            }
            return cacheObj as Model.Settings;
        }

        /// <summary>
        /// 更新一条数据，并且清除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void UpdateModel(string key, string value)
        {
            Model.Settings settingObj = dal.GetModel(key);
            settingObj.Value = value;
            if (dal.Update(settingObj))
            {
                Common.CacheHelper.DeleteCache("settings_" + key);
            }
        }

    }
}
