<%@ Application Language="C#" %>
<%@ Import Namespace="Agape.Manage.Core.Cache" %>
<%@ Import Namespace="Agape.Manage.Core.Manager" %>
<%@ Import Namespace="log4net" %>
<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="Leopard.Util" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        //在应用程序启动时运行的代码

        CacheManager.LoadCache();

        TimeThreadManager.Current.StartManager();

        LeopardLog.Info("WEB应用启动完成");
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

        TimeThreadManager.Current.EndManager();

        LeopardLog.Info("WEB应用结束完成");
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        //在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e) 
    {
        //在新会话启动时运行的代码
    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }
       
</script>
