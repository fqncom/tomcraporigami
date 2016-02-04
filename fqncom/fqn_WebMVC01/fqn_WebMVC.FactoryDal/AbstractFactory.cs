using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using fqn_WebMVC.IDal;

namespace fqn_WebMVC.FactoryDal
{
    public partial class AbstractFactory
    {
        private static readonly string DalAssemblyName =
            ConfigurationManager.AppSettings["DalAssemblyName"];

        private static readonly string DalNameSpace = ConfigurationManager.AppSettings["DalNameSpace"];

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <returns></returns>
        public static IBooksDal CreateBooksInstance()
        {
            string fullAssemblyName = DalNameSpace + ".BooksDal";
            var asembly = Assembly.Load(DalAssemblyName);
            return asembly.CreateInstance(fullAssemblyName) as IBooksDal;
        }

        public static IUsersDal CreateUsersInstance()
        {
            string fullAssemblyName = DalNameSpace + ".UsersDal";
            var asembly = Assembly.Load(DalAssemblyName);
            return asembly.CreateInstance(fullAssemblyName) as IUsersDal;
        }
    }
}
