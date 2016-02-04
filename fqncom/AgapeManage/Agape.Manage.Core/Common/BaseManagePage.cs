using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Leopard.Util;
using Leopard.Data;
using Agape.Manage.Core.Impl;
using Agape.Manage.Core.Session;

namespace Agape.Manage.Core.Common
{
    public class BaseManagePage : System.Web.UI.Page
    {
        protected Encoding m_ClientEncoding;

        public Encoding ClientEncoding
        {
            get { return m_ClientEncoding; }
        }

        /// <summary>
        /// 获取页面名称
        /// </summary>
        /// <returns></returns>
        public virtual string GetPageName()
        {
            return "管理页面";
        }

        /// <summary>
        /// 是否检查登录情况
        /// </summary>
        /// <returns></returns>
        public virtual bool IsCheckLogin()
        {
            return true;
        }

        public void Page_PreLoad(object sender, EventArgs e)
        {
            m_ClientEncoding = Encoding.GetEncoding(Response.Charset);

            if (IsCheckLogin() && !OperatorSession.IsLogin)
            {
                RedirectParent("Login.aspx");
            }
        }

        public void Page_LoadComplete(object sender, EventArgs e)
        {
            SavePageHit();
        }

        /// <summary>
        /// 重定向错误页面。
        /// </summary>
        /// <param name="ErrorMessage">错误信息</param>
        protected void RedirectError(string ErrorMessage)
        {
            string url = string.Format("Error.aspx?PageName={0:S}&&Message={1:S}", GetPageName(), ErrorMessage);
            Response.Redirect(url, true);
        }

        /// <summary>
        /// 重定向指定页面。
        /// </summary>
        /// <param name="url">页面地址</param>
        protected void Redirect(string url)
        {
            Response.Redirect(url, true);
        }

        /// <summary>
        /// 重定向指向父页面。
        /// </summary>
        /// <param name="url"></param>
        protected void RedirectParent(string url)
        {
            Response.Clear();
            Response.Write("<script>window.parent.location.href='" + url + "'</script>");
            Response.End();
        }

        /// <summary>
        /// 保存页面点击
        /// </summary>
        protected XReturn SavePageHit()
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            STAT_PageHit PageHit = new STAT_PageHit();
            PageHit.HitDate = DateTimeUtil.GetShortDateString();
            PageHit.HitTime = DateTimeUtil.GetShortTimeString();
            PageHit.PageCode = this.GetType().Name;
            PageHit.IP = Request.UserHostAddress;

            xSubReturn = SavePageHintExtra(PageHit);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "保存页面点击记录额外处理失败");
            }

            xSubReturn = StatImpl.SavePageHit(PageHit);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "保存页面点击记录失败");
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 保存页面点击额外处理
        /// </summary>
        /// <param name="PageHit">页面点击</param>
        protected virtual XReturn SavePageHintExtra(STAT_PageHit PageHit)
        {
            return XReturn.ReturnNewSccess();
        }

        #region 参数处理
        public bool ExistParameter(string ParameterName)
        {
            return Request[ParameterName] != null;
        }

        protected string GetParameter(string ParameterName)
        {
            return Request[ParameterName];
        }

        /// <summary>
        /// 获取整型参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public int GetIntParameter(string ParameterName, int DefaultValue)
        {
            int TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                try
                {
                    TempValue = Convert.ToInt32(GetParameter(ParameterName));
                }
                catch (Exception e)
                {
                }
            }

            return TempValue;
        }

        /// <summary>
        /// 获取布尔参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public bool GetBooleanParameter(string ParameterName, bool DefaultValue)
        {
            bool TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                try
                {
                    TempValue = Convert.ToBoolean(GetParameter(ParameterName));
                }
                catch (Exception e)
                {
                }
            }

            return TempValue;
        }

        /// <summary>
        /// 获取双精度参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public double GetDoubleParameter(string ParameterName, int DefaultValue)
        {
            double TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                try
                {
                    TempValue = Convert.ToDouble(GetParameter(ParameterName));
                }
                catch (Exception e)
                {
                }
            }

            return TempValue;
        }

        /// <summary>
        /// 获取字符串参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public string GetStringParameter(string ParameterName, string DefaultValue)
        {
            string TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                TempValue = GetParameter(ParameterName);
            }

            return TempValue;
        }

        /// <summary>
        /// 获取整型参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <param name="ParameterValue">返回参数值</param>
        /// <returns></returns>
        public bool GetIntParameter(string ParameterName, int DefaultValue, out int ParameterValue)
        {
            bool bReturn = false;
            int TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                try
                {
                    TempValue = Convert.ToInt32(GetParameter(ParameterName));
                    bReturn = true;
                }
                catch (Exception e)
                {
                }
            }

            ParameterValue = TempValue;
            return bReturn;
        }

        /// <summary>
        /// 获取布尔参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <param name="ParameterValue">返回参数值</param>
        /// <returns></returns>
        public bool GetBooleanParameter(string ParameterName, bool DefaultValue, out bool ParameterValue)
        {
            bool bReturn = false;
            bool TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                try
                {
                    TempValue = Convert.ToBoolean(GetParameter(ParameterName));
                    bReturn = true;
                }
                catch (Exception e)
                {
                }
            }

            ParameterValue = TempValue;
            return bReturn;
        }

        /// <summary>
        /// 获取双精度参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public bool GetDoubleParameter(string ParameterName, int DefaultValue, out double ParameterValue)
        {
            bool bReturn = false;
            double TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                try
                {
                    TempValue = Convert.ToDouble(GetParameter(ParameterName));
                    bReturn = true;
                }
                catch (Exception e)
                {
                }
            }

            ParameterValue = TempValue;
            return bReturn;
        }

        /// <summary>
        /// 获取字符串参数值。
        /// </summary>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DefaultValue">默认值</param>
        /// <param name="ParameterValue">返回参数值</param>
        /// <returns></returns>
        public bool GetStringParameter(string ParameterName, string DefaultValue, out string ParameterValue)
        {
            bool bReturn = false;
            string TempValue = DefaultValue;

            if (ExistParameter(ParameterName))
            {
                TempValue = GetParameter(ParameterName);
                bReturn = true;
            }

            ParameterValue = TempValue;
            return bReturn;
        }
        #endregion
    }
}
