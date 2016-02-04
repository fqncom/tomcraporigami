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
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Impl;
using Agape.Manage.Core.Cache;
using Agape.Manage.Core.Session;

public partial class BatchService : BaseServicePage
{
    #region 批次查询服务
    /// <summary>
    /// 查询批次数量。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryBatchCount()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        bool HasOrder;
        string BatchNo, FromDate, ToDate, StatusSet;

        if (!OperatorSession.IsLogin)
        {
            return xReturn.ReturnError("请先登录");
        }

        GetStringParameter("BatchNo", String.Empty, out BatchNo);
        GetStringParameter("FromDate", String.Empty, out FromDate);
        GetStringParameter("ToDate", String.Empty, out ToDate);
        GetStringParameter("StatusSet", String.Empty, out StatusSet);
        GetBooleanParameter("HasOrder", true, out HasOrder);

        xSubReturn = BatchImpl.QueryBatchCount(BatchNo, FromDate, ToDate, StatusSet, HasOrder);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        xReturn.SetValue("RowCount", xSubReturn.ReturnValue);
        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询批次列表。
    /// </summary>
    public XReturn QueryBatchList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        bool HasOrder;
        string BatchNo, FromDate, ToDate, StatusSet;
        int PageIndex, PageSize, StartIndex;

        if (!OperatorSession.IsLogin)
        {
            return xReturn.ReturnError("请先登录");
        }

        GetStringParameter("BatchNo", String.Empty, out BatchNo);
        GetStringParameter("FromDate", String.Empty, out FromDate);
        GetStringParameter("ToDate", String.Empty, out ToDate);
        GetStringParameter("StatusSet", String.Empty, out StatusSet);
        GetBooleanParameter("HasOrder", true, out HasOrder);
        GetIntParameter("PageIndex", 1, out PageIndex);
        GetIntParameter("PageSize", 0, out PageSize);

        StartIndex = (PageIndex - 1) * PageSize;
        xSubReturn = BatchImpl.QueryBatchList(BatchNo, FromDate, ToDate, StatusSet, HasOrder, StartIndex, PageSize, out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        ExportXmlAsAttribute(m_XmlTextWriter, dt, "Batch");

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 批次订单服务
    /// <summary>
    /// 查询批次订单数量。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryBatchOrderCount()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int BatchID, BatchStatus;
        string OrderNo, BatchNo, BarCode, OrderStatusSet;

        if (!OperatorSession.IsLogin)
        {
            return xReturn.ReturnError("请先登录");
        }

        GetIntParameter("BatchID", 0, out BatchID);
        GetStringParameter("BatchNo", String.Empty, out BatchNo);
        GetStringParameter("OrderNo", String.Empty, out OrderNo);
        GetStringParameter("BarCode", String.Empty, out BarCode);
        GetIntParameter("BatchStatus", 0, out BatchStatus);
        GetStringParameter("OrderStatusSet", String.Empty, out OrderStatusSet);

        if (BatchID == 0 && BatchNo != String.Empty)
        {
            xSubReturn = BatchImpl.QueryBatch(BatchNo);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询批次失败");
            }
            SLS_Batch Batch = (SLS_Batch)xSubReturn.ReturnValue;
            BatchID = Batch.BatchID;
        }

        xSubReturn = BatchImpl.QueryBatchOrderCount(BatchID, OrderNo, BarCode, BatchStatus, OrderStatusSet);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        xReturn.SetValue("RowCount", xSubReturn.ReturnValue);
        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询批次订单列表。
    /// </summary>
    public XReturn QueryBatchOrderList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int BatchID, BatchStatus;
        string OrderNo, BatchNo, BarCode, OrderStatusSet;
        int PageIndex, PageCount = 0, RowCount = 0, PageSize, StartIndex;

        if (!OperatorSession.IsLogin)
        {
            return xReturn.ReturnError("请先登录");
        }

        GetIntParameter("BatchID", 0, out BatchID);
        GetStringParameter("BatchNo", String.Empty, out BatchNo);
        GetStringParameter("OrderNo", String.Empty, out OrderNo);
        GetStringParameter("BarCode", String.Empty, out BarCode);
        GetIntParameter("BatchStatus", 0, out BatchStatus);
        GetStringParameter("OrderStatusSet", String.Empty, out OrderStatusSet);
        GetIntParameter("PageIndex", 1, out PageIndex);
        GetIntParameter("PageSize", 10, out PageSize);

        if (BatchID == 0 && BatchNo != String.Empty)
        {
            xSubReturn = BatchImpl.QueryBatch(BatchNo);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询批次失败");
            }
            SLS_Batch Batch = (SLS_Batch)xSubReturn.ReturnValue;
            BatchID = Batch.BatchID;
        }

        xSubReturn = BatchImpl.QueryBatchOrderCount(BatchID, OrderNo, BarCode, BatchStatus, OrderStatusSet);
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

            xSubReturn = BatchImpl.QueryBatchOrderList(BatchID, OrderNo, BarCode, BatchStatus, OrderStatusSet, StartIndex, PageSize, out dt);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            ExportXmlAsAttribute(m_XmlTextWriter, dt, "BatchOrder");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 批次订单确认服务
    /// <summary>
    /// 批次订单进行确认处理。
    /// </summary>
    /// <returns></returns>
    public XReturn BatchOrderConfirm()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int OrderID;

        if (!OperatorSession.IsLogin)
        {
            return xReturn.ReturnError("请先登录");
        }

        GetIntParameter("OrderID", 0, out OrderID);

        xSubReturn = BatchImpl.BatchOrderConfirm(OrderID);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "订单确认失败");
        }

        xSubReturn = BatchCache.Current.ConfirmOrder(OrderID);
        if (xSubReturn.IsUnSuccess())
        {
            xReturn.SetError(xSubReturn, string.Format("当前批次确认订单[{0:D}]失败", OrderID));
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 批次商品拣货服务
    /// <summary>
    /// 查询批次商品列表。
    /// </summary>
    public XReturn QueryBatchProductList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int BatchID;

        if (!OperatorSession.IsLogin)
        {
            return xReturn.ReturnError("请先登录");
        }

        GetIntParameter("BatchID", 0, out BatchID);

        xSubReturn = BatchImpl.QueryBatchProductList(BatchID, out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        ExportXmlAsAttribute(m_XmlTextWriter, dt, "BatchProduct");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 批次订单进行拣货处理。
    /// </summary>
    /// <returns></returns>
    public XReturn BatchOrderPick()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int BatchID;

        if (!OperatorSession.IsLogin)
        {
            return xReturn.ReturnError("请先登录");
        }

        GetIntParameter("BatchID", 0, out BatchID);

        xSubReturn = BatchImpl.BatchOrderPick(BatchID);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "订单商品拣货失败");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 批次订单打包服务
    /// <summary>
    /// 批次订单进行打包处理。
    /// </summary>
    /// <returns></returns>
    public XReturn BatchOrderPack()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int OrderID;
        string BarCode;

        if (!OperatorSession.IsLogin)
        {
            return xReturn.ReturnError("请先登录");
        }

        GetIntParameter("OrderID", 0, out OrderID);
        GetStringParameter("BarCode", String.Empty, out BarCode);

        xSubReturn = BatchImpl.BatchOrderPack(OrderID, BarCode);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "订单打包失败");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 批次订单配送服务
    /// <summary>
    /// 批次订单进行配送处理。
    /// </summary>
    /// <returns></returns>
    public XReturn BatchOrderDeliver()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int OrderID;

        if (!OperatorSession.IsLogin)
        {
            return xReturn.ReturnError("请先登录");
        }

        GetIntParameter("OrderID", 0, out OrderID);

        xSubReturn = BatchImpl.BatchOrderDeliver(OrderID);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "订单配送失败");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 批次订单完成服务
    /// <summary>
    /// 批次订单进行完成处理。
    /// </summary>
    /// <returns></returns>
    public XReturn BatchOrderFinish()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int OrderID;

        if (!OperatorSession.IsLogin)
        {
            return xReturn.ReturnError("请先登录");
        }

        GetIntParameter("OrderID", 0, out OrderID);

        xSubReturn = BatchImpl.BatchOrderFinish(OrderID);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "订单完成失败");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion
}