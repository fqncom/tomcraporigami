using System;
using System.Collections;
using System.Data;
using System.Text;
using Leopard.Util;
using Leopard.Cache;
using Leopard.Data;
using Agape.Manage.Core.Util;
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Impl;
using Agape.Manage.Core.Cache;
using Agape.Manage.Core.Session;

public partial class SalesService : BaseServicePage
{
    #region 订单服务
    /// <summary>
    /// 查询订单列表包含明细。
    /// </summary>
    public XReturn QueryOrderList()
    {
        DataTable dtOrders = null;
        DataTable dtOrderItems = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string OrderNo, FromDate, ToDate;
        int Status, PageIndex, PageCount = 0, RowCount = 0, PageSize, StartIndex;
        bool QueryItems;

        if (!OperatorSession.IsLogin)
        {
            return xReturn.ReturnError("您还未登录");
        }

        GetStringParameter("OrderNo", String.Empty, out OrderNo);
        GetStringParameter("FromDate", String.Empty, out FromDate);
        GetStringParameter("ToDate", String.Empty, out ToDate);
        GetIntParameter("Status", 0, out Status);
        GetIntParameter("PageIndex", 1, out PageIndex);
        GetIntParameter("PageSize", 10, out PageSize);
        GetBooleanParameter("QueryItems", false, out QueryItems);

        xSubReturn = SalesImpl.QueryOrderCount(0, OrderNo, FromDate, ToDate, Status);
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

            xSubReturn = SalesImpl.QueryOrderList(0, OrderNo, FromDate, ToDate, Status, StartIndex, PageSize, out dtOrders);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询订单列表失败");
            }
        }

        if (QueryItems)
        {
            foreach (DataRow dr in dtOrders.Rows)
            {
                m_XmlTextWriter.WriteStartElement("Order");

                ExportXmlAsAttribute(m_XmlTextWriter, dr, null);

                int OrderID = Convert.ToInt32(dr["OrderID"]);
                xSubReturn = SalesImpl.QueryOrderItems(OrderID, out dtOrderItems);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, "查询订单明细失败");
                }

                ExportXmlAsAttribute(m_XmlTextWriter, dtOrderItems, "OrderItem");

                m_XmlTextWriter.WriteEndElement();
            }
        }
        else
        {
            ExportXmlAsAttribute(m_XmlTextWriter, dtOrders, "Order");
        }

        xReturn.SetValue("RowCount", RowCount);
        xReturn.SetValue("PageCount", PageCount);

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询订单和订单明细。
    /// </summary>
    public XReturn QueryOrderAndItems()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int OrderID;
        string OrderNo;
        DataTable OrderTable, OrderItemTable;

        GetIntParameter("OrderID", 0, out OrderID);
        GetStringParameter("OrderNo", String.Empty, out OrderNo);

        xSubReturn = SalesImpl.QueryOrderAndItems(OrderID, OrderNo, out OrderTable, out OrderItemTable);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        ExportXmlAsNode(m_XmlTextWriter, OrderTable, "Order");
        ExportXmlAsNode(m_XmlTextWriter, OrderItemTable, "OrderItem");

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 会员咨询服务
    /// <summary>
    /// 答复会员咨询
    /// </summary>
    /// <returns></returns>
    public XReturn AnswerMemberConsultation()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int MemberConsultationID;
        string Answer;

        if (!OperatorSession.IsLogin)
        {
            return xReturn.ReturnError("您还未登录");
        }

        GetIntParameter("MemberConsultationID", 0, out MemberConsultationID);
        GetStringParameter("Answer", String.Empty, out Answer);

        xSubReturn = MemberImpl.AnswerMemberConsultation(MemberConsultationID, Answer);
        if (xSubReturn.IsUnSuccess())
        {
            xReturn.SetError(xSubReturn, "提交答复会员咨询失败");
            return xReturn.ReturnSuccess();
        }

        string strLog = string.Format("答复会员咨询[{0:D}][{1:S}]成功", MemberConsultationID, Answer);
        LeopardLog.Error(strLog);

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 删除会员咨询
    /// </summary>
    /// <returns></returns>
    public XReturn DeleteMemberConsultation()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int MemberConsultationID;

        if (!OperatorSession.IsLogin)
        {
            return xReturn.ReturnError("您还未登录");
        }

        GetIntParameter("MemberConsultationID", 0, out MemberConsultationID);

        xSubReturn = MemberImpl.DeleteMemberConsultation(MemberConsultationID);
        if (xSubReturn.IsUnSuccess())
        {
            xReturn.SetError(xSubReturn, "提交删除会员咨询失败");
            return xReturn.ReturnSuccess();
        }

        string strLog = string.Format("删除会员咨询[{0:D}]成功", MemberConsultationID);
        LeopardLog.Error(strLog);

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 会员维修服务
    /// <summary>
    /// 查询返修列表。
    /// </summary>
    public XReturn QueryRepairList()
    {
        DataTable dt;
        XReturn xReturn = new XReturn();

        string RepairNo, FromDate, ToDate;
        int MemberID, Status;

        GetIntParameter("MemberID", 0, out MemberID);
        if (!OperatorSession.IsLogin)
        {
            return xReturn.ReturnError("您还未登录");
        }

        GetStringParameter("RepairNo", String.Empty, out RepairNo);
        GetStringParameter("FromDate", String.Empty, out FromDate);
        GetStringParameter("ToDate", String.Empty, out ToDate);
        GetIntParameter("Status", 0, out Status);

        XReturn xSubReturn = SalesImpl.QueryRepairList(MemberID, RepairNo, FromDate, ToDate, Status, out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询返修列表失败");
        }

        ExportXmlAsAttribute(m_XmlTextWriter, dt, "Repair");

        return xReturn.ReturnSuccess();
    }
    #endregion
}