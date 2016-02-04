using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Leopard.Util;
using Leopard.Data;
using Agape.Manage.Core.Util;
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Cache;

namespace Agape.Manage.Core.Impl
{
    public class SalesImpl
    {
        /// <summary>
        /// 查询订单数目。
        /// </summary>
        /// <param name="MemberID">会员ID</param>
        /// <param name="OrderNo">订单编号</param>
        /// <param name="FromDate">开始日期</param>
        /// <param name="ToDate">结束日期</param>
        /// <param name="Status">状态</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryOrderCount(int MemberID, string OrderNo, string FromDate, string ToDate, int Status)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (MemberID > 0) strWhere += string.Format(" and a.MemberID={0:D}", MemberID);
            if (OrderNo != String.Empty) strWhere += string.Format(" and a.OrderNo='{0:S}'", OrderNo);
            if (FromDate != String.Empty) strWhere += string.Format(" and a.OrderDate>='{0:S}'", FromDate);
            if (ToDate != String.Empty) strWhere += string.Format(" and a.OrderDate<='{0:S}'", ToDate);
            if (Status != 0) strWhere += string.Format(" and a.Status={0:D}", Status);

            strSql = "select count(*) from SLS_Order a where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 查询商品列表。
        /// </summary>
        /// <param name="MemberID">商品类型ID</param>
        /// <param name="OrderNo">订单编号</param>
        /// <param name="FromDate">开始日期</param>
        /// <param name="ToDate">结束日期</param>
        /// <param name="Status">状态</param>
        /// <param name="StartIndex">开始序号</param>
        /// <param name="MaxCount">最大返回数量</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryOrderList(int MemberID, string OrderNo, string FromDate, string ToDate, int Status, int StartIndex, int MaxCount, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (MemberID > 0) strWhere += string.Format(" and a.MemberID={0:D}", MemberID);
            if (OrderNo != String.Empty) strWhere += string.Format(" and a.OrderNo='{0:S}'", OrderNo);
            if (FromDate != String.Empty) strWhere += string.Format(" and a.OrderDate>='{0:S}'", FromDate);
            if (ToDate != String.Empty) strWhere += string.Format(" and a.OrderDate<='{0:S}'", ToDate);
            if (Status != 0) strWhere += string.Format(" and a.Status={0:D}", Status);

            strSql = "select a.*,b.MemberName,c.ReceiverName from SLS_Order a " +
                    " left join BSC_Member b on b.MemberID=a.MemberID" +
                    " left join BSC_MemberAddress c on c.MemberAddressID=a.MemberAddressID" +
                    " where {0:S} order by CreateTime desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out ReturnTable);
        }

        /// <summary>
        /// 查询订单及明细。
        /// </summary>
        /// <param name="OrderID">订单ID</param>
        /// <param name="OrderNo">订单编号</param>
        /// <param name="OrderTable">返回订单数据表</param>
        /// <param name="OrderItemTable">返回订单明细数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryOrderAndItems(int OrderID, string OrderNo, out DataTable OrderTable, out DataTable OrderItemTable)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            OrderTable = null;
            OrderItemTable = null;
            string strSql, strWhere;

            // 查询订单
            strWhere = "1=1";
            if (OrderID > 0) strWhere += string.Format(" and OrderID={0:D}", OrderID);
            if (OrderNo != String.Empty) strWhere += string.Format(" and OrderNo='{0:S}'", OrderNo);
            strSql = "select a.*,b.MemberName,b.RealName,c.Province,c.City,c.District,c.Detail as AddressDetail,c.ReceiverName,c.MobilePhone," +
                    "d.InvoiceType,d.InvoiceHeaderType,d.InvoiceContent,d.CompanyName,d.TaxPayerNo,d.RegisterAddress,d.RegisterPhone,d.OpenBankName,d.BankAccountNo from SLS_Order a " +
                    " left join BSC_Member b on a.MemberID=b.MemberID" +
                    " left join BSC_MemberAddress c on a.MemberAddressID=c.MemberAddressID" +
                    " left join BSC_MemberInvoice d on a.MemberInvoiceID=d.MemberInvoiceID" +
                    " where {0:S}";
            strSql = string.Format(strSql, strWhere);

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out OrderTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            // 查询订单明细
            strWhere = "1=1";
            if (OrderID > 0) strWhere += string.Format(" and b.OrderID={0:D}", OrderID);
            if (OrderNo != String.Empty) strWhere += string.Format(" and b.OrderNo='{0:S}'", OrderNo);

            strSql = "select a.*,b.OrderNo,c.ProductNo,c.ProductName,c.ProductUnit,d.ProductSpec from SLS_OrderItem a " +
                    " left join SLS_Order b on b.OrderID=a.OrderID" +
                    " left join BSC_Product c on c.ProductID=a.ProductID" +
                    " left join BSC_ProductSpec d on d.ProductSpecID=a.ProductSpecID" +
                    " where {0:S} order by a.ProductID desc";
            strSql = string.Format(strSql, strWhere);

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out OrderItemTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询订单明细。
        /// </summary>
        /// <param name="OrderID">订单ID</param>
        /// <param name="OrderItemTable">返回订单明细数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryOrderItems(int OrderID, out DataTable OrderItemTable)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            OrderItemTable = null;
            string strSql, strWhere;

            // 查询订单明细
            strWhere = "1=1";
            if (OrderID > 0) strWhere += string.Format(" and b.OrderID={0:D}", OrderID);

            strSql = "select a.*,b.OrderNo,c.ProductNo,c.ProductName,c.ProductUnit,c.SalesPrice,d.ProductSpec from SLS_OrderItem a " +
                    " left join SLS_Order b on b.OrderID=a.OrderID" +
                    " left join BSC_Product c on c.ProductID=a.ProductID" +
                    " left join BSC_ProductSpec d on d.ProductSpecID=a.ProductSpecID" +
                    " where {0:S} order by a.ProductID desc";
            strSql = string.Format(strSql, strWhere);

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out OrderItemTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询返修列表。
        /// </summary>
        /// <param name="MemberID">会员ID</param>
        /// <param name="RepairNo">返修编号</param>
        /// <param name="FromDate">开始日期</param>
        /// <param name="ToDate">结束日期</param>
        /// <param name="Status">状态</param>
        /// <param name="ReturnTable">返回结果数据表</param>
        /// <returns></returns>
        public static XReturn QueryRepairList(int MemberID, string RepairNo, string FromDate, string ToDate, int Status, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = " where 1=1";
            if (MemberID > 0) strWhere += string.Format(" and a.MemberID={0:D}", MemberID);
            if (RepairNo != String.Empty) strWhere += string.Format(" and a.RepairNo like '%{0:S}%'", RepairNo);
            if (FromDate != String.Empty) strWhere += string.Format(" and a.ApplyDate >= '{0:S}'", FromDate);
            if (ToDate != String.Empty) strWhere += string.Format(" and a.ApplyDate <= '{0:S}'", ToDate);
            if (Status > 0) strWhere += string.Format(" and a.Status={0:D}", Status);

            strSql = "select a.*,b.ProductNo,b.ProductName,b.SalesPrice,c.MemberName,RealName from SLS_Repair a " +
                    " left join BSC_Product b on a.ProductID=b.ProductID" +
                    " left join BSC_Member c on a.MemberID=c.MemberID";
            strSql += strWhere;

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
        }
    }
}
