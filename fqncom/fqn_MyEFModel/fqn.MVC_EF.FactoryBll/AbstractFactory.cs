using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using fqn.MVC_EF.IBll;


namespace fqn.MVC_EF.FactoryBll
{
    public class AbstractFactory
    {
        private static readonly string AssemblyName = ConfigurationManager.AppSettings["BllAssemblyName"];
        private static readonly string NameSpace = ConfigurationManager.AppSettings["BllNameSpace"];

        public static IProvinceBll CreateProvinceBllInstance()
        {
            string fullName = NameSpace + ".S_ProvinceBll";
            Assembly ass = Assembly.Load(AssemblyName);
            return ass.CreateInstance(fullName) as IProvinceBll;
        }

        public static ICityBll CreateCityBllInstance()
        {
            string fullName = NameSpace + ".S_CityBll";
            Assembly ass = Assembly.Load(AssemblyName);
            return ass.CreateInstance(fullName) as ICityBll;
        }

        public static IDistrictBll CreateDistrictBllInstance()
        {
            string fullName = NameSpace + ".S_DistrictBll";
            Assembly ass = Assembly.Load(AssemblyName);
            return ass.CreateInstance(fullName) as IDistrictBll;
        }

        public static IUserInfoBll CreateUserInfoBllInstance()
        {
            string fullName = NameSpace + ".UserInfoBll";
            Assembly ass = Assembly.Load(AssemblyName);
            return ass.CreateInstance(fullName) as IUserInfoBll;
        }


    }
}
