using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using Leopard.Data;

namespace Agape.Manage.Core.Session
{
    public class OperatorSession
    {
        static OperatorSession()
        {
        }

        #region 会话保护接口
        /// <summary>
        /// 获取当前会话。
        /// </summary>
        protected static HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        /// <summary>
        /// 清除会话。
        /// </summary>
        protected static void Clear()
        {
            Session.Clear();
        }

        /// <summary>
        /// 判断是否存在会话。
        /// </summary>
        /// <param name="Name">会话名称</param>
        /// <returns></returns>
        protected static bool ExistSession(string Name)
        {
            return Session[Name] != null;
        }
                
        /// <summary>
        /// 获取会话值。
        /// </summary>
        /// <param name="Name">会话名称</param>
        /// <returns></returns>
        protected static object GetValue(string Name)
        {
            return Session[Name];
        }

        /// <summary>
        /// 获取会话布尔值。
        /// </summary>
        /// <param name="Name">会话名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        protected static bool GetBooleanValue(string Name, bool DefaultValue)
        {
            return ExistSession(Name) ? (bool)Session[Name] : DefaultValue;
        }

        /// <summary>
        /// 获取会话整型值。
        /// </summary>
        /// <param name="Name">会话名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        protected static int GetInt32Value(string Name, int DefaultValue)
        {
            return ExistSession(Name) ? (int)Session[Name] : DefaultValue;
        }

        /// <summary>
        /// 获取会话双精度值。
        /// </summary>
        /// <param name="Name">会话名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        protected static double GetDoubleValue(string Name, double DefaultValue)
        {
            return ExistSession(Name) ? (double)Session[Name] : DefaultValue;
        }

        /// <summary>
        /// 获取会话字符串值。
        /// </summary>
        /// <param name="Name">会话名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        protected static string GetStringValue(string Name, string DefaultValue)
        {
            return ExistSession(Name) ? (string)Session[Name] : DefaultValue;
        }

        /// <summary>
        /// 设置会话值。
        /// </summary>
        /// <param name="Name">会话名称</param>
        /// <param name="Value">会员值</param>
        /// <returns></returns>
        protected static void SetValue(string Name, object Value)
        {
            Session[Name] = Value;
        }
        #endregion

        #region 会话基本公共接口
        /// <summary>
        /// 获取是否已经登录。
        /// </summary>
        public static bool IsLogin
        {
            get { return GetBooleanValue("Operator_IsLogin", false); }
        }

        /// <summary>
        /// 获取会员对象。
        /// </summary>
        public static LPD_Operator Operator
        {
            get { return ExistSession("Operator") ? (LPD_Operator)GetValue("Operator") : null; }
        }

        /// <summary>
        /// 登录。
        /// </summary>
        /// <param name="Operator">会员对象</param>
        public static void Login(LPD_Operator Operator)
        {
            SetValue("Operator_IsLogin", true);
            SetValue("Operator", Operator);
        }

        /// <summary>
        /// 注销。
        /// </summary>
        public static void Logout()
        {
            Clear();
        }
        #endregion
    }
}
