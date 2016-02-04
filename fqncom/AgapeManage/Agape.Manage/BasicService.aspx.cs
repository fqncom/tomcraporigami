using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.IO;
using System.Text;
using Leopard.Util;
using Leopard.Cache;
using Leopard.Data;
using Agape.Manage.Core.Util;
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Impl;
using Agape.Manage.Core.Cache;
using Agape.Manage.Core.Session;

public partial class BasicService : BaseServicePage
{
    #region 区域服务
    /// <summary>
    /// 查询区域列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryAreaList()
    {
        DataTable dt;
        XReturn xReturn = new XReturn();
        int AreaLevel;
        string AreaCode, ParentCode;

        GetIntParameter("AreaLevel", 0, out AreaLevel);
        GetStringParameter("AreaCode", String.Empty, out AreaCode);
        GetStringParameter("ParentCode", String.Empty, out ParentCode);

        XReturn xSubReturn = LeopardFactory.GetLeopardImpl().QueryAreaList(AreaCode, ParentCode, AreaLevel, out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        foreach (DataRow dr in dt.Rows)
        {
            m_XmlTextWriter.WriteStartElement("Area");

            m_XmlTextWriter.WriteAttributeString("AreaCode", dr["AreaCode"].ToString());
            m_XmlTextWriter.WriteAttributeString("AreaName", dr["AreaName"].ToString());
            m_XmlTextWriter.WriteAttributeString("AreaLevel", dr["AreaLevel"].ToString());
            m_XmlTextWriter.WriteAttributeString("ParentCode", dr["ParentCode"].ToString());

            m_XmlTextWriter.WriteEndElement();
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 字典服务
    /// <summary>
    /// 查询区域列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryDictItemList()
    {
        XReturn xReturn = new XReturn();
        string DictCodeList;
        string[] DictCodeArray;

        GetStringParameter("DictCodeList", String.Empty, out DictCodeList);
        DictCodeArray = DictCodeList.Split(',');

        m_XmlTextWriter.WriteStartElement("DictList");

        foreach (string DictCode in DictCodeArray)
        {
            ArrayList DictItemList = LeopardFactory.GetDictCache().GetDictItemList(DictCode);
            if (DictItemList != null && DictItemList.Count > 0)
            {
                m_XmlTextWriter.WriteStartElement("Dict");
                m_XmlTextWriter.WriteAttributeString("DictCode", DictCode);

                foreach (LPD_DictItem DictItem in DictItemList)
                {
                    ExportXmlAsAttribute(m_XmlTextWriter, DictItem, "DictItem");
                }
                m_XmlTextWriter.WriteEndElement();
            }
        }

        m_XmlTextWriter.WriteEndElement();

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 操作员服务
    /// <summary>
    /// 操作员登录。
    /// </summary>
    public XReturn OperatorLogin()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int OperatorID;
        string LoginName, LoginPassword;

        GetStringParameter("LoginName", String.Empty, out LoginName);
        if (String.IsNullOrEmpty(LoginName))
        {
            return xReturn.ReturnError("登录名不能为空");
        }

        GetStringParameter("LoginPassword", String.Empty, out LoginPassword);
        if (String.IsNullOrEmpty(LoginPassword))
        {
            return xReturn.ReturnError("登录密码不能为空");
        }

        string LoginPasswordDigest = MD5Util.MD5Encrypt(LoginPassword);
        xSubReturn = BasicImpl.OperatorLogin(LoginName, LoginPasswordDigest, out OperatorID);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "操作员登录失败");
        }

        LPD_Operator Operator = new LPD_Operator();
        Operator.OperatorID = OperatorID;
        xSubReturn = Operator.x.SelectByIdentity();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        OperatorSession.Login(Operator);

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询操作员数量
    /// </summary>
    /// <returns></returns>
    public XReturn QueryOperatorCount()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string OperatorNo, OperatorName;

        GetStringParameter("OperatorNo", string.Empty, out OperatorNo);
        GetStringParameter("OperatorName", string.Empty, out OperatorName);

        xSubReturn = LeopardFactory.GetLeopardImpl().QueryOperatorCount(String.Empty, String.Empty);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询操作员数量失败");
        }
        xReturn.SetValue("RowCount", xSubReturn.ReturnValue);
        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询操作员列表
    /// </summary>
    /// <returns></returns>
    public XReturn QueryOperatorList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string OperatorNo, OperatorName;
        int PageIndex, PageCount = 0, RowCount = 0, PageSize, StartIndex;

        GetStringParameter("OperatorNo", String.Empty, out OperatorNo);
        GetStringParameter("OperatorName", String.Empty, out OperatorName);
        GetIntParameter("PageIndex", 1, out PageIndex);
        GetIntParameter("PageSize", 10, out PageSize);

        xSubReturn = LeopardFactory.GetLeopardImpl().QueryOperatorCount(OperatorNo, OperatorName);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询操作员数量失败");
        }

        RowCount = xSubReturn.GetIntValue();

        if (RowCount > 0)
        {
            PageCount = GenericUtil.CalculatePageCount(RowCount, PageSize);
            if (PageIndex > PageCount) PageIndex = PageCount;
            StartIndex = (PageIndex - 1) * PageSize;

            xSubReturn = LeopardFactory.GetLeopardImpl().QueryOperators(OperatorNo, OperatorName, StartIndex, PageSize, out dt);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询操作员列表失败");
            }

            ExportXmlAsAttribute(m_XmlTextWriter, dt, "Operator");
        }

        xReturn.SetValue("RowCount", RowCount);
        xReturn.SetValue("PageCount", PageCount);

        return xReturn.ReturnSuccess();
    }

    public XReturn QueryOperator()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        LPD_Operator Operator = new LPD_Operator();

        Operator.OperatorID = GetIntParameter("OperatorID", 0);
        if (Operator.OperatorID == 0)
        {
            return xReturn.ReturnError("操作员ID参数不存在");
        }

        xSubReturn = Operator.x.SelectByIdentity();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询操作员失败");
        }

        ExportXmlAsNode(m_XmlTextWriter, Operator, "Operator");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存管理员。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveOperator()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        LPD_Operator Operator = new LPD_Operator();

        Operator.OperatorID = GetIntParameter("OperatorID", 0);
        if (Operator.OperatorID > 0)
        {
            xSubReturn = Operator.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询操作员失败");
            }
        }
        Operator.OperatorNo = GetStringParameter("OperatorNo", String.Empty);
        Operator.OperatorName = GetStringParameter("OperatorName", String.Empty);

        string LoginPassword = GetStringParameter("Password", String.Empty);
        Operator.Password = MD5Util.MD5Encrypt(LoginPassword);

        xSubReturn = LeopardFactory.GetLeopardImpl().SaveOperator((Operator.OperatorID == 0 ? EEditMode.New : EEditMode.Edit), Operator);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "保存操作员失败");
        }

        xReturn.SetValue("OperatorID", Operator.OperatorID);
        return xReturn.ReturnSuccess();
    }

    public XReturn DeleteOperator()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        LPD_Operator Operator = new LPD_Operator();

        int OperatorID = GetIntParameter("OperatorID", 0);

        xSubReturn = LeopardFactory.GetLeopardImpl().DeleteOperator(OperatorID);
        if (xSubReturn.IsUnSuccess())
        {
            xReturn.ReturnError(xSubReturn);
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询角色数量
    /// </summary>
    /// <returns></returns>
    public XReturn QueryRoleCount()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string RoleCode, RoleName;

        GetStringParameter("RoleCode", string.Empty, out RoleCode);
        GetStringParameter("RoleName", string.Empty, out RoleName);

        xSubReturn = LeopardFactory.GetLeopardImpl().QueryRoles(RoleName, out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询角色数量失败");
        }
        xReturn.SetValue("RowCount", xSubReturn.ReturnValue);
        return xReturn.ReturnSuccess();
    }


    public XReturn QueryRoleList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string RoleCode, RoleName;
        int PageIndex, PageCount = 0, RowCount = 0, PageSize, StartIndex;

        GetStringParameter("RoleCode", String.Empty, out RoleCode);
        GetStringParameter("RoleName", String.Empty, out RoleName);
        GetIntParameter("PageIndex", 1, out PageIndex);
        GetIntParameter("PageSize", 10, out PageSize);

        xSubReturn = LeopardFactory.GetLeopardImpl().QueryRoles(RoleName, out dt);

        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询角色失败");
        }
        RowCount = xSubReturn.GetIntValue();

        if (RowCount > 0)
        {
            PageCount = GenericUtil.CalculatePageCount(RowCount, PageSize);
            if (PageIndex > PageCount) PageIndex = PageCount;
            StartIndex = (PageIndex - 1) * PageSize;

            xSubReturn = LeopardFactory.GetLeopardImpl().QueryRoles(RoleName, out dt);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询角色列表失败");
            }

            ExportXmlAsAttribute(m_XmlTextWriter, dt, "Role");
        }

        xReturn.SetValue("RowCount", RowCount);
        xReturn.SetValue("PageCount", PageCount);

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询角色
    /// </summary>
    /// <returns></returns>
    public XReturn QueryRole()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        LPD_Role Role = new LPD_Role();
        string RoleCode;

        GetStringParameter("RoleCode", String.Empty, out RoleCode);

        if (String.IsNullOrEmpty(RoleCode))
        {
            return xReturn.ReturnError("角色代码为空");
        }

        Role.RoleCode = RoleCode;
        xSubReturn = Role.x.Select();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询角色记录失败");
        }

        ExportXmlAsNode(m_XmlTextWriter, Role, "Role");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存角色。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveRole()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        LPD_Role Role = new LPD_Role();

        if (Role.RoleCode == null)
        {
            return xReturn.ReturnError("查询角色失败");
        }

        Role.RoleCode = GetStringParameter("RoleCode", String.Empty);
        Role.RoleName = GetStringParameter("RoleName", String.Empty);
        Role.Status = GetStringParameter("Status", String.Empty);
        Role.Remark = GetStringParameter("Remark", String.Empty);
        xSubReturn = LeopardFactory.GetLeopardImpl().SaveRole((Role.RoleCode != null ? EEditMode.New : EEditMode.Edit), Role);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "保存角色失败");
        }
        xReturn.SetValue("RoleCode", Role.RoleCode);
        return xReturn.ReturnSuccess();
    }

    public XReturn DeleteRole()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        LPD_Role Role = new LPD_Role();
        string RoleCode;

        GetStringParameter("RoleCode", String.Empty, out RoleCode);
        xSubReturn = LeopardFactory.GetLeopardImpl().DeleteRole(RoleCode);
        if (xSubReturn.IsUnSuccess())
        {
            xReturn.ReturnError(xSubReturn);
        }
        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 帮助菜单服务
    /// <summary>
    /// 查询帮助菜单项。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryHelpItem()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_HelpItem HelpItem = new BSC_HelpItem();

        HelpItem.HelpCode = GetStringParameter("HelpCode", String.Empty);
        if (HelpItem.x.Exist())
        {
            xSubReturn = HelpItem.x.Select();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询帮助菜单项失败");
            }
        }

        ExportXmlAsNode(m_XmlTextWriter, HelpItem, "HelpItem");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存帮助菜单项。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveHelpItem()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_HelpItem HelpItem = new BSC_HelpItem();

        HelpItem.HelpCode = GetStringParameter("HelpCode", String.Empty);
        string strTemp = GetStringParameter("HelpContent", String.Empty);
        HelpItem.HelpContent = HttpUtility.UrlDecode(strTemp, ClientEncoding);

        xSubReturn = BasicImpl.SaveHelpItem(HelpItem);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "保存帮助菜单项失败");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 邮件服务
    /// <summary>
    /// 查询商品列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryEmailMessageList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string Title, Receiver, FromDate, ToDate;
        int PageIndex, PageCount = 0, RowCount = 0, PageSize, StartIndex;

        GetStringParameter("Title", String.Empty, out Title);
        GetStringParameter("Receiver", String.Empty, out Receiver);
        GetStringParameter("FromDate", String.Empty, out FromDate);
        GetStringParameter("ToDate", String.Empty, out ToDate);
        GetIntParameter("PageIndex", 1, out PageIndex);
        GetIntParameter("PageSize", 10, out PageSize);

        xSubReturn = BasicImpl.QueryEmailMessageCount(Title, Receiver, FromDate, ToDate);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        RowCount = xSubReturn.GetIntValue();

        if (RowCount > 0)
        {
            PageCount = GenericUtil.CalculatePageCount(RowCount, PageSize);
            if (PageIndex > PageCount) PageIndex = PageCount;
            StartIndex = (PageIndex - 1) * PageSize;

            xSubReturn = BasicImpl.QueryEmailMessageList(Title, Receiver, FromDate, ToDate, StartIndex, PageSize, out dt);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            ExportXmlAsAttribute(m_XmlTextWriter, dt, "EmailMessage");
        }

        xReturn.SetValue("RowCount", RowCount);
        xReturn.SetValue("PageCount", PageCount);

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 操作员登录。
    /// </summary>
    public XReturn SubmitEmailMessage()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string Title, ReceiverList, Body;
        int IsBodyHtml;

        GetStringParameter("Title", String.Empty, out Title);
        if (String.IsNullOrEmpty(Title))
        {
            return xReturn.ReturnError("标题不能为空");
        }

        GetStringParameter("ReceiverList", String.Empty, out ReceiverList);
        if (String.IsNullOrEmpty(ReceiverList))
        {
            return xReturn.ReturnError("收件人不能为空");
        }

        GetIntParameter("IsBodyHtml", (int)EYesNo.No, out IsBodyHtml);

        GetStringParameter("Body", String.Empty, out Body);
        if (String.IsNullOrEmpty(Body))
        {
            return xReturn.ReturnError("主题不能为空");
        }
        Body = HttpUtility.UrlDecode(Body, ClientEncoding);

        //xSubReturn = TaskThreadManager.Default.AddEmailTask(Title, ReceiverList, Body, IsBodyHtml);
        //if (xSubReturn.IsUnSuccess())
        //{
        //    return xReturn.ReturnError(xSubReturn, "提交邮件信息失败");
        //}

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 短信服务
    /// <summary>
    /// 查询商品列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryMobileMessageList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string MobilePhone, MessageContent, FromDate, ToDate;
        int PageIndex, PageCount = 0, RowCount = 0, PageSize, StartIndex;

        GetStringParameter("MobilePhone", String.Empty, out MobilePhone);
        GetStringParameter("MessageContent", String.Empty, out MessageContent);
        GetStringParameter("FromDate", String.Empty, out FromDate);
        GetStringParameter("ToDate", String.Empty, out ToDate);
        GetIntParameter("PageIndex", 1, out PageIndex);
        GetIntParameter("PageSize", 10, out PageSize);

        xSubReturn = BasicImpl.QueryMobileMessageCount(MobilePhone, MessageContent, FromDate, ToDate);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        RowCount = xSubReturn.GetIntValue();

        if (RowCount > 0)
        {
            PageCount = GenericUtil.CalculatePageCount(RowCount, PageSize);
            if (PageIndex > PageCount) PageIndex = PageCount;
            StartIndex = (PageIndex - 1) * PageSize;

            xSubReturn = BasicImpl.QueryMobileMessageList(MobilePhone, MessageContent, FromDate, ToDate, StartIndex, PageSize, out dt);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            ExportXmlAsAttribute(m_XmlTextWriter, dt, "MobileMessage");
        }

        xReturn.SetValue("RowCount", RowCount);
        xReturn.SetValue("PageCount", PageCount);

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 操作员登录。
    /// </summary>
    public XReturn SubmitMobileMessage()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string ReceiverList, MessageContent;

        GetStringParameter("ReceiverList", String.Empty, out ReceiverList);
        if (String.IsNullOrEmpty(ReceiverList))
        {
            return xReturn.ReturnError("收件人不能为空");
        }

        GetStringParameter("MessageContent", String.Empty, out MessageContent);
        if (String.IsNullOrEmpty(MessageContent))
        {
            return xReturn.ReturnError("短信内容不能为空");
        }
        MessageContent = HttpUtility.UrlDecode(MessageContent, ClientEncoding);

        //xSubReturn = TaskThreadManager.Default.AddMobileMessageTask(ReceiverList, MessageContent);
        //if (xSubReturn.IsUnSuccess())
        //{
        //    return xReturn.ReturnError(xSubReturn, "提交短信信息失败");
        //}

        return xReturn.ReturnSuccess();
    }
    #endregion
}






