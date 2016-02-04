using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using fqn_WebMVC.IBll;

namespace fqn_WebMVC.FactoryBll
{
   public partial class AbstractFactory
   {
       private static readonly string BllAssemblyName =
           ConfigurationManager.AppSettings["BllAssemblyName"];

       private static readonly string BllNameSpace = ConfigurationManager.AppSettings["BllNameSpace"];

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns></returns>
        public static IBooksBll CreateBooksInstance()
        {
            string fullAssemblyName = BllNameSpace + ".BooksBll";
            var asembly = Assembly.Load(BllAssemblyName);
            return asembly.CreateInstance(fullAssemblyName) as IBooksBll;
        }

        public static IUsersBll CreateUsersInstance()
        {
            string fullAssemblyName = BllNameSpace + ".UsersBll";
            var asembly = Assembly.Load(BllAssemblyName);
            return asembly.CreateInstance(fullAssemblyName) as IUsersBll;
        }
    }
}
