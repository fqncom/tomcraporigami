using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace fqn.ItcastOA.Demo
{
    public class MyMvcHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        {
            string url = context.Request.RawUrl;//获取不包含ip地址和端口号的地址，其实拿到的就是控制器名字和方法名

            string[] str = url.TrimStart('/').Split('/');//从/controller/action中分别拿到控制器名和方法名

            string controlName = str[0];
            string actionName = str[1];

            string fullControlName = "fqn.ItcastOA.Demo." + controlName;//根据命名空间名字和控制器名，获取完整的控制器名

            BaseController obj = (BaseController)Assembly.GetExecutingAssembly().CreateInstance(fullControlName);//先获取当前代码的程序集，然后根据之前拿到的命名空间+类名，创建出控制器实例对象

            MethodInfo method = Assembly.GetExecutingAssembly().GetType(fullControlName).GetMethod(actionName);//根据当前程序集中的指定类型名字获取他的类型然后根据方法名反射得到他的方法

            object result = method.Invoke(obj, null);//执行方法，传入实例对象和参数
            context.Response.Write(result);

        }
    }
}
