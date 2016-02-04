using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using fqn.MVC_EF.IDal;

namespace fqn.MVC_EF.CommonDal
{
    public class CommonHelper
    {

        /// <summary>
        /// 创建线程内唯一的ef数据模型。减少内存消耗的同时，使操作数据的ef对象唯一，防止更新数据库时出现脏数据
        /// </summary>
        /// <returns></returns>
        public static EF_Model CheckDbModelAndGet()
        {
            EF_Model db = HttpContext.Current.Items["EF_Model"] as EF_Model;
            if (HttpContext.Current.Items["EF_Model"] == null)
            {
                return db = new EF_Model();
            }
            return db;
        }

        public static fqnModelContainer CheckDbModel2AndGet()
        {
            fqnModelContainer db = HttpContext.Current.Items["EF_Model2"] as fqnModelContainer;
            if (HttpContext.Current.Items["EF_Model2"] == null)
            {
                return db = new fqnModelContainer();
            }
            return db;
        }

    }
}
