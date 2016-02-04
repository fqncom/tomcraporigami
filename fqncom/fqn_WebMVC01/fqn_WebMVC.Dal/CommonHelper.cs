using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using fqn_WebMVC.Model;

namespace fqn_WebMVC.Dal
{
    public class CommonHelper
    {
        /// <summary>
        /// 使用全局上下文对象的属性Items将数据访问对象Entity保存起来，使对象在线程内唯一。防止出现脏数据。
        /// </summary>
        /// <returns></returns>
        public static Book_ShopEntities CheckEntitiesExistOrCreate()
        {
            Book_ShopEntities bse = HttpContext.Current.Items["DbEntity"] as Book_ShopEntities;
            if (bse == null)
            {
                bse = new Book_ShopEntities();
                HttpContext.Current.Items.Add("DbEntity", bse);
            }
            return bse;
        }
    }
}
