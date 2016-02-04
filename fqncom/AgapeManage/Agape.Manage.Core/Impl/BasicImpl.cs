using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using Leopard.Util;
using Leopard.Data;
using Leopard.Cache;
using Agape.Manage.Core.Util;
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Cache;

namespace Agape.Manage.Core.Impl
{
    public class BasicImpl
    {
        #region 操作员接口
        /// <summary>
        /// 操作员登录。
        /// </summary>
        /// <param name="LoginName">登录名</param>
        /// <param name="LoginPassword">密码</param>
        /// <param name="OperatorID">返回操作员ID</param>
        /// <returns>返回登录是否成功</returns>
        public static XReturn OperatorLogin(string LoginName, string LoginPassword, out int OperatorID)
        {
            string strSql;
            DbDataReader dr;
            XReturn xReturn = new XReturn();

            OperatorID = 0;
            strSql = string.Format("select OperatorID from LPD_Operator where OperatorNo='{0:S}' and Password='{1:S}'", LoginName, LoginPassword);
            XReturn xSubReturn = DatabaseFactory.GetCurrent().GetReader(strSql, out dr);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            if (dr.Read())
            {
                OperatorID = dr.GetInt32(0);
            }
            dr.Close();

            if (OperatorID == 0)
            {
                return xReturn.ReturnError("用户名或者密码错误");
            }

            return xReturn.ReturnSuccess();
        }
        #endregion

        #region 帮助接口
        /// <summary>
        /// 保存帮助菜单项
        /// </summary>
        /// <param name="HelpItem">帮助菜单项</param>
        /// <returns></returns>
        public static XReturn SaveHelpItem(BSC_HelpItem HelpItem)
        {
            if (HelpItem.x.Exist())
            {
                return HelpItem.x.Update();
            }
            else
            {
                return HelpItem.x.Insert();
            }
        }
        #endregion

        #region 邮件信息服务
        /// <summary>
        /// 查询邮件信息数目。
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Receiver">收件人</param>
        /// <param name="FromDate">开始日期</param>
        /// <param name="ToDate">结束日期</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryEmailMessageCount(string Title, string Receiver, string FromDate, string ToDate)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (Title != String.Empty) strWhere += string.Format(" and a.Title like '%{0:S}%'", Title);
            if (Receiver != String.Empty) strWhere += string.Format(" and a.EmailMessageID in (select b.EmailMessageID from BSC_EmailMessageReceiver b where b.Receiver='{0:S}')", Receiver);
            if (FromDate != String.Empty) strWhere += string.Format(" and a.SendDateTime>='{0:D}'", DateTimeUtil.ConvertStringToDateTime(FromDate));
            if (ToDate != String.Empty) strWhere += string.Format(" and a.SendDateTime<='{0:D}'", DateTimeUtil.ConvertStringToDateTime(ToDate).AddDays(1));

            strSql = "select count(*) from BSC_EmailMessage a where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 查询邮件信息列表。
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Receiver">收件人</param>
        /// <param name="FromDate">开始日期</param>
        /// <param name="ToDate">结束日期</param>
        /// <param name="StartIndex">开始序号</param>
        /// <param name="MaxCount">最大返回数量</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryEmailMessageList(string Title, string Receiver, string FromDate, string ToDate, int StartIndex, int MaxCount, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (Title != String.Empty) strWhere += string.Format(" and a.Title like '%{0:S}%'", Title);
            if (Receiver != String.Empty) strWhere += string.Format(" and a.EmailMessageID in (select b.EmailMessageID from BSC_EmailMessageReceiver b where b.Receiver='{0:S}')", Receiver);
            if (FromDate != String.Empty) strWhere += string.Format(" and a.SendDateTime>='{0:D}'", DateTimeUtil.ConvertStringToDateTime(FromDate));
            if (ToDate != String.Empty) strWhere += string.Format(" and a.SalesPrice<='{0:D}'", DateTimeUtil.ConvertStringToDateTime(ToDate).AddDays(1));

            strSql = "select a.EmailMessageID,a.Title,a.IsBodyHtml,a.Category,a.SendDateTime,a.Status from BSC_EmailMessage a where {0:S} order by a.SendDateTime desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out ReturnTable);
        }
        #endregion

        #region 短信信息服务
        /// <summary>
        /// 查询短信信息数目。
        /// </summary>
        /// <param name="MobilePhone">手机号码</param>
        /// <param name="MessageContent">短信内容</param>
        /// <param name="FromDate">开始日期</param>
        /// <param name="ToDate">结束日期</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryMobileMessageCount(string MobilePhone, string MessageContent, string FromDate, string ToDate)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (MobilePhone != String.Empty) strWhere += string.Format(" and a.MobilePhone = '{0:S}'", MobilePhone);
            if (MessageContent != String.Empty) strWhere += string.Format(" and a.MessageContent like '%{0:S}%'", MessageContent);
            if (FromDate != String.Empty) strWhere += string.Format(" and a.SendDateTime>='{0:D}'", DateTimeUtil.ConvertStringToDateTime(FromDate));
            if (ToDate != String.Empty) strWhere += string.Format(" and a.SendDateTime<='{0:D}'", DateTimeUtil.ConvertStringToDateTime(ToDate).AddDays(1));

            strSql = "select count(*) from BSC_MobileMessage a where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 查询短信信息列表。
        /// </summary>
        /// <param name="MobilePhone">手机号码</param>
        /// <param name="MessageContent">短信内容</param>
        /// <param name="FromDate">开始日期</param>
        /// <param name="ToDate">结束日期</param>
        /// <param name="StartIndex">开始序号</param>
        /// <param name="MaxCount">最大返回数量</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryMobileMessageList(string MobilePhone, string MessageContent, string FromDate, string ToDate, int StartIndex, int MaxCount, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (MobilePhone != String.Empty) strWhere += string.Format(" and a.MobilePhone = '{0:S}'", MobilePhone);
            if (MessageContent != String.Empty) strWhere += string.Format(" and a.MessageContent like '%{0:S}%'", MessageContent);
            if (FromDate != String.Empty) strWhere += string.Format(" and a.SendDateTime>='{0:D}'", DateTimeUtil.ConvertStringToDateTime(FromDate));
            if (ToDate != String.Empty) strWhere += string.Format(" and a.SalesPrice<='{0:D}'", DateTimeUtil.ConvertStringToDateTime(ToDate).AddDays(1));

            strSql = "select a.* from BSC_MobileMessage a where {0:S} order by a.SendDateTime desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out ReturnTable);
        }
        #endregion
    }
}
