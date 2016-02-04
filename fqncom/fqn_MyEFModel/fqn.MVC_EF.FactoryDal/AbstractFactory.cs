using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using fqn.MVC_EF.IDal;
using System.Reflection;

namespace fqn.MVC_EF.FactoryDal
{
    public class AbstractFactory
    {
        private static readonly string AssemblyName = ConfigurationManager.AppSettings["DalAssemblyName"];
        private static readonly string NameSpace = ConfigurationManager.AppSettings["DalNameSpace"];

        /// <summary>
        /// 返回省得接口类型对象
        /// </summary>
        /// <returns></returns>
        public static IProvinceDal CreateProvinceDalInstance()
        {
            string fullAssembly = NameSpace + ".S_ProvinceDal";
            Assembly ass = Assembly.Load(AssemblyName);
            return ass.CreateInstance(fullAssembly) as IProvinceDal;
        }
        /// <summary>
        /// 返回省得接口类型对象
        /// </summary>
        /// <returns></returns>
        public static ICityDal CreateCityDalInstance()
        {
            string fullAssembly = NameSpace + ".S_CityDal";
            Assembly ass = Assembly.Load(AssemblyName);
            return ass.CreateInstance(fullAssembly) as ICityDal;
        }
        /// <summary>
        /// 返回省得接口类型对象
        /// </summary>
        /// <returns></returns>
        public static IDistrictDal CreateDistrictDalInstance()
        {
            string fullAssembly = NameSpace + ".S_DistrictDal";
            Assembly ass = Assembly.Load(AssemblyName);
            return ass.CreateInstance(fullAssembly) as IDistrictDal;
        }

        public static IUserInfoDal CreateUserInfoDalInstance()
        {
            string fullAssembly = NameSpace + ".UserInfoDal";
            Assembly ass = Assembly.Load(AssemblyName);
            return ass.CreateInstance(fullAssembly) as IUserInfoDal;
        }


    }
}
