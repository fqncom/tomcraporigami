using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using fqn.ItcastOA.IDal;

namespace fqn.ItcastOA.DalFactory
{
    public class AbstractDalFactory
    {
        private static readonly string DalAssemblyName = ConfigurationManager.AppSettings["DalAssemblyName"];
        private static readonly string DalNameSpace = ConfigurationManager.AppSettings["DalNamespace"];

        public static IUserInfoDal GetUserInfoDalInstance()
        {
            string fullClassName = DalNameSpace + ".UserInfoDal";
            return GetINstanceByReflector(fullClassName) as IUserInfoDal;
        }

        private static object GetINstanceByReflector(string fullClassName)
        {
            Assembly ass = Assembly.Load(DalAssemblyName);
            return ass.CreateInstance(fullClassName);
        }

    }
}
