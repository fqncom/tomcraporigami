using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using fqn_AbstractFactoryDemo.IDal;

namespace fqn_AbstractFactoryDemo.Factory
{
    public class UserInfoFactory
    {
        public static readonly string AssemblyName = ConfigurationManager.AppSettings["AssemblyName"];
        public static readonly string NameSpace = ConfigurationManager.AppSettings["NameSpace"];


        public static IUserInfoDal GetUserInfoDal()
        {
            string fullClassName = NameSpace + ".UserInfoDal";
            var assembly = Assembly.Load(AssemblyName);
            return assembly.CreateInstance(fullClassName) as IUserInfoDal;
        }

    }
}
