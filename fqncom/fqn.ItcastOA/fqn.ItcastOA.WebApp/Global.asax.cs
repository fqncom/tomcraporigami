using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common.Logging;
using fqn.ItcastOA.WebApp.Models;
using log4net.Core;
using Spring.Web.Mvc;
using RouteDebug;

namespace fqn.ItcastOA.WebApp
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : SpringMvcApplication//System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            SearchQueueManager.GetInstance().StartThread();//开启线程写字典数据
            log4net.Config.XmlConfigurator.Configure();//读取Log4Net配置信息

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);//在这里初始化了记录错误的队列
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
            #region 日志记录（使用log4net）
            string filePath = Server.MapPath("App_Data/");//定义写日志文件的路径
            ThreadPool.QueueUserWorkItem((path) =>
            {
                while (true)
                {
                    if (SimpleErrorFilter.ExQueue.Count > 0)
                    {
                        Exception ex = SimpleErrorFilter.ExQueue.Dequeue();
                        if (ex != null)
                        {
                            //File.AppendAllText(path + DateTime.Now.ToString("yy-MM-dd") + ".txt", ex.ToString(), Encoding.UTF8);
                            ILog log = LogManager.GetLogger("fqn_Error");
                            log.Error(ex.ToString());
                        }
                        else
                        {
                            Thread.Sleep(5000);
                        }
                    }
                    else
                    {
                        Thread.Sleep(5000);
                    }
                }
            }, filePath);
            #endregion

            ////使用RouteDebug进行路由验证
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }

        protected override void ConfigureApplicationContext()
        {
            base.ConfigureApplicationContext();//读取配置文件进行
        }


        //protected void Application_Error()
        //{

        //}
    }
}