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

public partial class MemberService : BaseServicePage
{
    #region 会员基本服务
    /// <summary>
    /// 查询会员数量。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryMemberCount()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int MemberID;
        string MemberNo, MemberName;

        GetIntParameter("MemberID", 0, out MemberID);
        GetStringParameter("MemberNo", String.Empty, out MemberNo);
        GetStringParameter("MemberName", String.Empty, out MemberName);

        xSubReturn = MemberImpl.QueryMemberCount(MemberID, MemberNo, MemberName);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        xReturn.SetValue("RowCount", xSubReturn.ReturnValue);
        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询会员列表。
    /// </summary>
    public XReturn QueryMemberList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int MemberID, PageIndex, PageCount = 0, RowCount = 0, PageSize, StartIndex;
        string MemberNo, MemberName;

        GetIntParameter("MemberID", 0, out MemberID);
        GetStringParameter("MemberNo", String.Empty, out MemberNo);
        GetStringParameter("MemberName", String.Empty, out MemberName);
        GetIntParameter("PageIndex", 1, out PageIndex);
        GetIntParameter("PageSize", 10, out PageSize);

        xSubReturn = MemberImpl.QueryMemberCount(MemberID, MemberNo, MemberName);
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

            xSubReturn = MemberImpl.QueryMemberList(MemberID, MemberNo, MemberName, StartIndex, PageSize, out dt);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            ExportXmlAsAttribute(m_XmlTextWriter, dt, "Member");
        }

        xReturn.SetValue("RowCount", RowCount);
        xReturn.SetValue("PageCount", PageCount);

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 会员评论服务
    /// <summary>
    /// 查询会员评论列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryMemberReviewList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int ProductID, PageIndex, PageCount = 0, RowCount = 0, PageSize, StartIndex;

        GetIntParameter("ProductID", 1, out ProductID);
        GetIntParameter("PageIndex", 1, out PageIndex);
        GetIntParameter("PageSize", 10, out PageSize);

        xSubReturn = MemberImpl.QueryMemberReviewCount(0, ProductID);
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

            xSubReturn = MemberImpl.QueryMemberReviewList(0, ProductID, StartIndex, PageSize, out dt);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            ExportXmlAsAttribute(m_XmlTextWriter, dt, "MemberReview");
        }

        xReturn.SetValue("RowCount", RowCount);
        xReturn.SetValue("PageCount", PageCount);

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 会员咨询服务
    /// <summary>
    /// 查询会员咨询列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryMemberConsultationList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string MemberName, RealName, ProductNo, ProductName;
        int ProductID, MemberID, Status, PageIndex, PageCount = 0, RowCount = 0, PageSize, StartIndex;

        GetIntParameter("ProductID", 0, out ProductID);
        GetIntParameter("MemberID", 0, out MemberID);
        GetIntParameter("Status", 0, out Status);
        GetStringParameter("MemberName", String.Empty, out MemberName);
        GetStringParameter("RealName", String.Empty, out RealName);
        GetStringParameter("ProductNo", String.Empty, out ProductNo);
        GetStringParameter("ProductName", String.Empty, out ProductName);
        GetIntParameter("PageIndex", 1, out PageIndex);
        GetIntParameter("PageSize", 10, out PageSize);

        xSubReturn = MemberImpl.QueryMemberConsultationCount(MemberID, MemberName, RealName, ProductID, ProductNo, ProductName, Status);
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

            xSubReturn = MemberImpl.QueryMemberConsultationList(MemberID, MemberName, RealName, ProductID, ProductNo, ProductName, Status, StartIndex, PageSize, out dt);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            ExportXmlAsAttribute(m_XmlTextWriter, dt, "MemberConsultation");
        }

        xReturn.SetValue("RowCount", RowCount);
        xReturn.SetValue("PageCount", PageCount);

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 会员优惠劵服务
    /// <summary>
    /// 查询会员优惠劵列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryMemberCouponList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string MemberNo, MemberName, MemberCouponNo;
        double ParValue;
        int MemberID, OrderID, Status, PageIndex, PageCount = 0, RowCount = 0, PageSize, StartIndex;

        GetIntParameter("MemberID", 0, out MemberID);
        GetStringParameter("MemberNo", String.Empty, out MemberNo);
        GetStringParameter("MemberName", String.Empty, out MemberName);
        GetIntParameter("OrderID", 0, out OrderID);
        GetIntParameter("Status", 0, out Status);
        GetStringParameter("MemberCouponNo", String.Empty, out MemberCouponNo);
        GetDoubleParameter("ParValue", 0, out ParValue);

        GetIntParameter("PageIndex", 1, out PageIndex);
        GetIntParameter("PageSize", 10, out PageSize);

        xSubReturn = MemberImpl.QueryMemberCouponCount(MemberID, MemberNo, MemberName, OrderID, MemberCouponNo, ParValue, Status);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询会员优惠劵数量失败");
        }

        RowCount = xSubReturn.GetIntValue();

        if (RowCount > 0)
        {
            PageCount = GenericUtil.CalculatePageCount(RowCount, PageSize);
            if (PageIndex > PageCount) PageIndex = PageCount;
            StartIndex = (PageIndex - 1) * PageSize;

            xSubReturn = MemberImpl.QueryMemberCouponList(MemberID, MemberNo, MemberName, OrderID, MemberCouponNo, ParValue, Status, StartIndex, PageSize, out dt);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询会员优惠劵列表失败");
            }

            ExportXmlAsAttribute(m_XmlTextWriter, dt, "MemberCoupon");
        }

        xReturn.SetValue("RowCount", RowCount);
        xReturn.SetValue("PageCount", PageCount);

        return xReturn.ReturnSuccess();
    }
    #endregion
}