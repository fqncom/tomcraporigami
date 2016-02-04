using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Leopard.Util;
using Leopard.Data;
using Leopard.Cache;
using Agape.Manage.Core.Util;
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Cache;

namespace Agape.Manage.Core.Impl
{
    public class InventoryImpl
    {
        #region 商品入库接口
        /// <summary>
        /// 查询入库数量。
        /// </summary>
        /// <param name="StockInNo">入库号</param>
        /// <param name="WarehouseID">仓库ID</param>
        /// <param name="BuyerID">采购员ID</param>
        /// <param name="FromDate">入库开始时间</param>
        /// <param name="ToDate">入库结束时间</param>
        /// <param name="Status">状态</param>
        /// <returns>返回执行码</returns>
        public static XReturn QueryStockInCount(string StockInNo, int WarehouseID, int BuyerID, string FromDate, string ToDate, string Status)
        {
            string strSql = String.Empty;

            strSql = "select count(*) from IVT_StockIn a where 1=1";
            if (StockInNo != String.Empty) strSql += string.Format(" and a.StockInNo like '%{0:S}%'", StockInNo);
            if (WarehouseID != 0) strSql += string.Format(" and a.WarehouseID={0:D}", WarehouseID);
            if (BuyerID != 0) strSql += string.Format(" and a.BuyerID={0:D}", BuyerID);
            if (FromDate != String.Empty) strSql += string.Format(" and a.StockInDate>='{0:S}'", FromDate);
            if (ToDate != String.Empty) strSql += string.Format(" and a.StockInDate<='{0:S}'", ToDate);
            if (Status != String.Empty) strSql += string.Format(" and a.Status='{0:S}'", Status);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 查询商品入库列表。
        /// </summary>
        /// <param name="StockInNo">入库号</param>
        /// <param name="WarehouseID">仓库ID</param>
        /// <param name="BuyerID">采购员ID</param>
        /// <param name="FromDate">入库开始时间</param>
        /// <param name="ToDate">入库结束时间</param>
        /// <param name="Status">状态</param>
        /// <param name="StartIndex">起始行序号</param>
        /// <param name="MaxCount">最大行数</param>
        /// <param name="tbStockInItems">返回查询入库结果表</param>
        /// <returns>返回执行码</returns>
        public static XReturn QueryStockInList(string StockInNo, int WarehouseID, int BuyerID, string FromDate, string ToDate, string Status, int StartIndex, int MaxCount, out DataTable tbStockInItems)
        {
            string strWhere = String.Empty;
            string strSql = String.Empty;
            tbStockInItems = null;

            strWhere = "1=1";
            if (StockInNo != String.Empty) strWhere += string.Format(" and a.StockInNo like '%{0:S}%'", StockInNo);
            if (WarehouseID != 0) strWhere += string.Format(" and a.WarehouseID={0:D}", WarehouseID);
            if (BuyerID != 0) strWhere += string.Format(" and a.BuyerID={0:D}", BuyerID);
            if (FromDate != String.Empty) strWhere += string.Format(" and a.StockInDate>='{0:S}'", FromDate);
            if (ToDate != String.Empty) strWhere += string.Format(" and a.StockInDate<='{0:S}'", ToDate);
            if (Status != String.Empty) strWhere += string.Format(" and a.Status='{0:S}'", Status);

            strSql = "select a.*,b.WarehouseName from IVT_StockIn a" +
                    " left join BSC_Warehouse b on a.WarehouseID=b.WarehouseID " +
                    " where {0:S} order by CreateTime desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out tbStockInItems);
        }

        /// <summary>
        /// 查询入库的项目列表。
        /// </summary>
        /// <param name="nStockInID">入库ID</param>
        /// <param name="dtbStockInItems">返回查询结果</param>
        /// <param name="strErrorMsg">返回执行错误信息</param>
        /// <returns></returns>
        public static XReturn QueryStockInItemList(int nStockInID, out DataTable dtbStockInItems)
        {
            dtbStockInItems = null;

            string sql = "select a.*,b.ProductNo,b.ProductName,c.ProductSpec,c.ProductUnit from IVT_StockInItem a" +
                        " left join BSC_Product b on a.ProductID=b.ProductID " +
                        " left join BSC_ProductSpec c on a.ProductSpecID=c.ProductSpecID " +
                        " where a.StockInID=" + nStockInID.ToString();

            return DatabaseFactory.GetCurrent().GetDataTable(sql, out dtbStockInItems);
        }

        /// <summary>
        /// 查询入库单及明细。
        /// </summary>
        /// <param name="StockInID">入库单ID</param>
        /// <param name="StockInNo">入库单编号</param>
        /// <param name="StockInTable">返回入库单数据表</param>
        /// <param name="StockInItemTable">返回入库单明细数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryStockInAndItems(int StockInID, string StockInNo, out DataTable StockInTable, out DataTable StockInItemTable)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            StockInTable = null;
            StockInItemTable = null;
            string strSql, strWhere;

            // 查询入库单
            strWhere = "1=1";
            if (StockInID > 0) strWhere += string.Format(" and StockInID={0:D}", StockInID);
            if (StockInNo != String.Empty) strWhere += string.Format(" and StockInNo='{0:S}'", StockInNo);
            strSql = "select * from IVT_StockIn where {0:S}";
            strSql = string.Format(strSql, strWhere);

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out StockInTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            // 查询入库单明细
            strWhere = "1=1";
            if (StockInID > 0) strWhere += string.Format(" and a.StockInID={0:D}", StockInID);
            if (StockInNo != String.Empty) strWhere += string.Format(" and b.StockInNo='{0:S}'", StockInNo);

            strSql = "select a.*,c.ProductNo,c.ProductName,c.ProductUnit,c.BarCode,d.ProductSpec from IVT_StockInItem a" +
                        " left join IVT_StockIn b on a.StockInID=b.StockInID " +
                        " left join BSC_Product c on a.ProductID=c.ProductID " +
                        " left join BSC_ProductSpec d on a.ProductSpecID=d.ProductSpecID where {0:S}";
            strSql = string.Format(strSql, strWhere);

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out StockInItemTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 增加销售入库。
        /// </summary>
        /// <param name="inStockIn">销售入库</param>
        /// <param name="inStockInItems">销售入库明细</param>
        /// <returns>返回代码</returns>
        public static XReturn SubmitStockIn(IVT_StockIn StockIn, ArrayList StockInItems)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();

            xTransaction.BeginTransition();

            // 插入其他入库记录。
            xSubReturn = StockIn.x.Insert(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();

                xReturn.ReturnCode = "9999";
                xReturn.ReturnMessage = "插入其他入库记录失败！";
                return xReturn;
            }

            foreach (IVT_StockInItem StockInItem in StockInItems)
            {
                // 插入其他入库项记录。
                StockInItem.StockInID = StockIn.StockInID;
                xSubReturn = StockInItem.x.Insert(xTransaction);
                if (xSubReturn.IsUnSuccess())
                {
                    xTransaction.Rollback();

                    xReturn.ReturnCode = "9999";
                    xReturn.ReturnMessage = "插入其他入库记录失败！";
                    return xReturn;
                }

                ProductStockChangeRequestParameter RequestParameter = new ProductStockChangeRequestParameter();
                RequestParameter.ProductSpecID = StockInItem.ProductSpecID;
                RequestParameter.WarehouseID = StockIn.WarehouseID;
                RequestParameter.ChangeType = (int)EProductStockChangeType.StockIn;
                RequestParameter.ChangeQuantity = StockInItem.Quantity;
                RequestParameter.ChangeReason = String.Empty;
                RequestParameter.AssociateVoucherType = (int)EVoucherType.OtherStockIn;
                RequestParameter.AssociateVoucherNo = StockIn.StockInNo;
                RequestParameter.AssociateID = StockInItem.StockInItemID;
                RequestParameter.OperatorID = StockIn.OperatorID;

                // 提交商品库存变动。
                xSubReturn = InventoryImpl.SubmitProductStockChange(xTransaction, RequestParameter);
                if (xSubReturn.IsUnSuccess())
                {
                    xTransaction.Rollback();

                    xReturn.SetError(xSubReturn);
                    return xReturn;
                }
            }

            xTransaction.Commit();

            xReturn.ReturnCode = "0000";
            return xReturn;
        }

        /// <summary>
        /// 撤销其他入库单。
        /// </summary>
        /// <param name="StockInID">采购单ID</param>
        /// <returns>返回操作结果</returns>
        public static XReturn CancelStockIn(int StockInID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();

            IVT_StockIn StockIn = new IVT_StockIn();
            StockIn.StockInID = StockInID;
            xSubReturn = StockIn.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.ReturnCode = "9999";
                xReturn.ReturnMessage = "查询其他入库失败";
                return xReturn;
            }

            // 查询其他入库项。
            ArrayList Entities;
            Hashtable FilterValues = new Hashtable();
            FilterValues["StockInID"] = StockInID;
            xSubReturn = BaseEntityEx.MSelect("IVT_StockInItem", FilterValues, out Entities);

            xTransaction.BeginTransition();

            foreach (IVT_StockInItem StockInItem in Entities)
            {
                // 撤销商品库存变动。
                xSubReturn = InventoryImpl.CancelProductStockChange(xTransaction, StockInItem.ProductSpecID, StockIn.WarehouseID, (int)EVoucherType.OtherStockIn, StockIn.StockInNo, StockInItem.StockInItemID);
                if (xSubReturn.IsUnSuccess())
                {
                    xTransaction.Rollback();

                    xReturn.SetError(xSubReturn);
                    return xSubReturn;
                }

                // 更新其他入库项状态。
                StockInItem.Status = (int)EInventoryStockInStatus.OK;
                xSubReturn = StockInItem.x.UpdateByIdentity();
                if (xSubReturn.IsUnSuccess())
                {
                    xTransaction.Rollback();

                    xReturn.ReturnCode = "9999";
                    xReturn.ReturnMessage = "更新其他入库项状态失败";
                    return xReturn;
                }
            }

            // 更新其他入库状态。
            StockIn.Status = (int)EInventoryStockInStatus.Cancel;
            xSubReturn = StockIn.x.UpdateByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();

                xReturn.ReturnCode = "9999";
                xReturn.ReturnMessage = "更新其他入库状态失败";
                return xReturn;
            }

            xTransaction.Commit();

            xReturn.ReturnCode = "0000";
            return xReturn;
        }
        #endregion

        #region 商品出库接口
        /// <summary>
        /// 查询出库数量。
        /// </summary>
        /// <param name="StockOutNo">出库号</param>
        /// <param name="WarehouseID">仓库ID</param>
        /// <param name="SalesmanID">采购员ID</param>
        /// <param name="FromDate">出库开始时间</param>
        /// <param name="ToDate">出库结束时间</param>
        /// <param name="Status">状态</param>
        /// <returns>返回执行码</returns>
        public static XReturn QueryStockOutCount(string StockOutNo, int WarehouseID, int SalesmanID, string FromDate, string ToDate, string Status)
        {
            string strSql = String.Empty;

            strSql = "select count(*) from IVT_StockOut a where 1=1";
            if (StockOutNo != String.Empty) strSql += string.Format(" and a.StockOutNo like '%{0:S}%'", StockOutNo);
            if (WarehouseID != 0) strSql += string.Format(" and a.WarehouseID={0:D}", WarehouseID);
            if (SalesmanID != 0) strSql += string.Format(" and a.SalesmanID={0:D}", SalesmanID);
            if (FromDate != String.Empty) strSql += string.Format(" and a.StockOutDate>='{0:S}'", FromDate);
            if (ToDate != String.Empty) strSql += string.Format(" and a.StockOutDate<='{0:S}'", ToDate);
            if (Status != String.Empty) strSql += string.Format(" and a.Status='{0:S}'", Status);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 查询商品出库列表。
        /// </summary>
        /// <param name="StockOutNo">出库号</param>
        /// <param name="WarehouseID">仓库ID</param>
        /// <param name="SalesmanID">采购员ID</param>
        /// <param name="FromDate">出库开始时间</param>
        /// <param name="ToDate">出库结束时间</param>
        /// <param name="Status">状态</param>
        /// <param name="StartIndex">起始行序号</param>
        /// <param name="MaxCount">最大行数</param>
        /// <param name="tbStockOutItems">返回查询出库结果表</param>
        /// <returns>返回执行码</returns>
        public static XReturn QueryStockOutList(string StockOutNo, int WarehouseID, int SalesmanID, string FromDate, string ToDate, string Status, int StartIndex, int MaxCount, out DataTable tbStockOutItems)
        {
            string strWhere = String.Empty;
            string strSql = String.Empty;
            tbStockOutItems = null;

            strWhere = "1=1";
            if (StockOutNo != String.Empty) strWhere += string.Format(" and a.StockOutNo like '%{0:S}%'", StockOutNo);
            if (WarehouseID != 0) strWhere += string.Format(" and a.WarehouseID={0:D}", WarehouseID);
            if (SalesmanID != 0) strWhere += string.Format(" and a.SalesmanID={0:D}", SalesmanID);
            if (FromDate != String.Empty) strWhere += string.Format(" and a.StockOutDate>='{0:S}'", FromDate);
            if (ToDate != String.Empty) strWhere += string.Format(" and a.StockOutDate<='{0:S}'", ToDate);
            if (Status != String.Empty) strWhere += string.Format(" and a.Status='{0:S}'", Status);

            strSql = "select a.*,b.WarehouseName from IVT_StockOut a" +
                    " left join BSC_Warehouse b on a.WarehouseID=b.WarehouseID " +
                    " where {0:S} order by CreateTime desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out tbStockOutItems);
        }

        /// <summary>
        /// 查询出库的项目列表。
        /// </summary>
        /// <param name="nStockOutID">出库ID</param>
        /// <param name="dtbStockOutItems">返回查询结果</param>
        /// <param name="strErrorMsg">返回执行错误信息</param>
        /// <returns></returns>
        public static XReturn QueryStockOutItemList(int nStockOutID, out DataTable dtbStockOutItems)
        {
            dtbStockOutItems = null;

            string sql = "select a.*,b.ProductNo,b.ProductName,c.ProductSpec,c.ProductUnit from IVT_StockOutItem a" +
                        " left join BSC_Product b on a.ProductID=b.ProductID " +
                        " left join BSC_ProductSpec c on a.ProductSpecID=c.ProductSpecID " +
                        " where a.StockOutID=" + nStockOutID.ToString();

            return DatabaseFactory.GetCurrent().GetDataTable(sql, out dtbStockOutItems);
        }

        /// <summary>
        /// 查询出库单及明细。
        /// </summary>
        /// <param name="StockOutID">出库单ID</param>
        /// <param name="StockOutNo">出库单编号</param>
        /// <param name="StockOutTable">返回出库单数据表</param>
        /// <param name="StockOutItemTable">返回出库单明细数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryStockOutAndItems(int StockOutID, string StockOutNo, out DataTable StockOutTable, out DataTable StockOutItemTable)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            StockOutTable = null;
            StockOutItemTable = null;
            string strSql, strWhere;

            // 查询出库单
            strWhere = "1=1";
            if (StockOutID > 0) strWhere += string.Format(" and StockOutID={0:D}", StockOutID);
            if (StockOutNo != String.Empty) strWhere += string.Format(" and StockOutNo='{0:S}'", StockOutNo);
            strSql = "select * from IVT_StockOut where {0:S}";
            strSql = string.Format(strSql, strWhere);

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out StockOutTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            // 查询出库单明细
            strWhere = "1=1";
            if (StockOutID > 0) strWhere += string.Format(" and a.StockOutID={0:D}", StockOutID);
            if (StockOutNo != String.Empty) strWhere += string.Format(" and b.StockOutNo='{0:S}'", StockOutNo);

            strSql = "select a.*,c.ProductNo,c.ProductName,c.ProductUnit,c.BarCode,d.ProductSpec from IVT_StockOutItem a" +
                        " left join IVT_StockOut b on a.StockOutID=b.StockOutID " +
                        " left join BSC_Product c on a.ProductID=c.ProductID " +
                        " left join BSC_ProductSpec d on a.ProductSpecID=d.ProductSpecID where {0:S}";
            strSql = string.Format(strSql, strWhere);

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out StockOutItemTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 增加销售出库。
        /// </summary>
        /// <param name="inStockOut">销售出库</param>
        /// <param name="inStockOutItems">销售出库明细</param>
        /// <returns>返回代码</returns>
        public static XReturn SubmitStockOut(IVT_StockOut StockOut, ArrayList StockOutItems)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();

            xTransaction.BeginTransition();

            // 插入其他出库记录。
            xSubReturn = StockOut.x.Insert(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();

                xReturn.ReturnCode = "9999";
                xReturn.ReturnMessage = "插入其他出库记录失败！";
                return xReturn;
            }

            foreach (IVT_StockOutItem StockOutItem in StockOutItems)
            {
                // 插入其他出库项记录。
                StockOutItem.StockOutID = StockOut.StockOutID;
                xSubReturn = StockOutItem.x.Insert(xTransaction);
                if (xSubReturn.IsUnSuccess())
                {
                    xTransaction.Rollback();

                    xReturn.ReturnCode = "9999";
                    xReturn.ReturnMessage = "插入其他出库记录失败！";
                    return xReturn;
                }

                ProductStockChangeRequestParameter RequestParameter = new ProductStockChangeRequestParameter();
                RequestParameter.ProductSpecID = StockOutItem.ProductSpecID;
                RequestParameter.WarehouseID = StockOut.WarehouseID;
                RequestParameter.ChangeType = (int)EProductStockChangeType.StockOut;
                RequestParameter.ChangeQuantity = StockOutItem.Quantity;
                RequestParameter.ChangeReason = String.Empty;
                RequestParameter.AssociateVoucherType = (int)EVoucherType.OtherStockOut;
                RequestParameter.AssociateVoucherNo = StockOut.StockOutNo;
                RequestParameter.AssociateID = StockOutItem.StockOutItemID;
                RequestParameter.OperatorID = StockOut.OperatorID;

                // 提交商品库存变动。
                xSubReturn = InventoryImpl.SubmitProductStockChange(xTransaction, RequestParameter);
                if (xSubReturn.IsUnSuccess())
                {
                    xTransaction.Rollback();

                    xReturn.SetError(xSubReturn);
                    return xReturn;
                }
            }

            xTransaction.Commit();

            xReturn.ReturnCode = "0000";
            return xReturn;
        }

        /// <summary>
        /// 撤销其他出库单。
        /// </summary>
        /// <param name="StockOutID">采购单ID</param>
        /// <returns>返回操作结果</returns>
        public static XReturn CancelStockOut(int StockOutID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();

            IVT_StockOut StockOut = new IVT_StockOut();
            StockOut.StockOutID = StockOutID;
            xSubReturn = StockOut.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.ReturnCode = "9999";
                xReturn.ReturnMessage = "查询其他出库失败";
                return xReturn;
            }

            // 查询其他出库项。
            ArrayList Entities;
            Hashtable FilterValues = new Hashtable();
            FilterValues["StockOutID"] = StockOutID;
            xSubReturn = BaseEntityEx.MSelect("IVT_StockOutItem", FilterValues, out Entities);

            xTransaction.BeginTransition();

            foreach (IVT_StockOutItem StockOutItem in Entities)
            {
                // 撤销商品库存变动。
                xSubReturn = InventoryImpl.CancelProductStockChange(xTransaction, StockOutItem.ProductSpecID, StockOut.WarehouseID, (int)EVoucherType.OtherStockOut, StockOut.StockOutNo, StockOutItem.StockOutItemID);
                if (xSubReturn.IsUnSuccess())
                {
                    xTransaction.Rollback();

                    xReturn.SetError(xSubReturn);
                    return xSubReturn;
                }

                // 更新其他出库项状态。
                StockOutItem.Status = (int)EInventoryStockOutStatus.OK;
                xSubReturn = StockOutItem.x.UpdateByIdentity();
                if (xSubReturn.IsUnSuccess())
                {
                    xTransaction.Rollback();

                    xReturn.ReturnCode = "9999";
                    xReturn.ReturnMessage = "更新其他出库项状态失败";
                    return xReturn;
                }
            }

            // 更新其他出库状态。
            StockOut.Status = (int)EInventoryStockOutStatus.Cancel;
            xSubReturn = StockOut.x.UpdateByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();

                xReturn.ReturnCode = "9999";
                xReturn.ReturnMessage = "更新其他出库状态失败";
                return xReturn;
            }

            xTransaction.Commit();

            xReturn.ReturnCode = "0000";
            return xReturn;
        }
        #endregion

        #region 库存查询接口
        /// <summary>
        /// 查询商品库存数量。
        /// </summary>
        /// <param name="WarehouseID">仓库ID</param>
        /// <param name="ProductID">商品ID</param>
        /// <param name="ProductSpecID">商品规格ID</param>
        /// <returns></returns>
        public static XReturn QueryProductStockQuantity(int WarehouseID, int ProductID, int ProductSpecID)
        {
            string strSql = String.Empty;
            XReturn xReturn = new XReturn();

            strSql = "select sum(Quantity) as Quantity,sum(FrozenQuantity) as FrozenQuantity,sum(RemainQuantity) as RemainQuantity from IVT_ProductStock where 1=1";

            if (WarehouseID > 0) strSql += string.Format(" and WarehouseID={0:D}", WarehouseID);
            if (ProductID > 0) strSql += string.Format(" and ProductID={0:D}", ProductID);
            if (ProductSpecID > 0) strSql += string.Format(" and ProductSpecID={0:D}", ProductSpecID);

            XReturn xSubReturn = DatabaseFactory.GetCurrent().ExecuteMultiScalar(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品库存数量");
            }

            xReturn.SetValue("Quantity", xSubReturn.GetValue("Quantity"));
            xReturn.SetValue("FrozenQuantity", xSubReturn.GetValue("FrozenQuantity"));
            xReturn.SetValue("RemainQuantity", xSubReturn.GetValue("RemainQuantity"));

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询商品库存数量。
        /// </summary>
        /// <param name="WarehouseID">仓库ID</param>
        /// <param name="ProductID">商品ID</param>
        /// <returns>返回执行码</returns>
        public static XReturn QueryProductStockTotal(int WarehouseID, int ProductID)
        {
            string strSql = String.Empty;

            strSql = "select count(*) as TotalCount,isnull(sum(a.Quantity),0) as TotalQuantity from IVT_ProductStock a where 1=1";
            if (WarehouseID != 0) strSql += " and a.WarehouseID=" + WarehouseID.ToString();
            if (ProductID != 0) strSql += " and a.ProductID=" + ProductID.ToString();

            return DatabaseFactory.GetCurrent().ExecuteMultiScalar(strSql);
        }

        /// <summary>
        /// 查询商品库存。
        /// </summary>
        /// <param name="WarehouseID">仓库ID</param>
        /// <param name="ProductID">商品ID</param>
        /// <param name="StartIndex">起始行序号</param>
        /// <param name="MaxCount">最大行数</param>
        /// <param name="tbStockOutItems">返回查询出库结果表</param>
        /// <returns>返回执行码</returns>
        public static XReturn QueryProductStockList(int WarehouseID, int ProductID, int StartIndex, int MaxCount, out DataTable tbProductStocks)
        {
            string strSql = String.Empty;
            tbProductStocks = null;

            strSql = "select a.*,b.WarehouseName,c.ProductNo,c.ProductName,c.ProductUnit,d.ProductSpec from IVT_ProductStock a" +
                " left join BSC_Warehouse b on a.WarehouseID=b.WarehouseID " +
                " left join BSC_Product c on a.ProductID=c.ProductID " +
                " left join BSC_ProductSpec d on a.ProductSpecID=d.ProductSpecID " +
                " where 1=1";
            if (WarehouseID != 0) strSql += " and b.WarehouseID=" + WarehouseID.ToString();
            if (ProductID != 0) strSql += " and d.ProductID=" + ProductID.ToString();

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out tbProductStocks);
        }

        /// <summary>
        /// 查询商品库存变动数量。
        /// </summary>
        /// <param name="WarehouseID">仓库ID</param>
        /// <param name="ProductID">商品ID</param>
        /// <param name="ChangeType">变动类型</param>
        /// <param name="FromChangeDate">变动开始日期</param>
        /// <param name="ToChangeDate">变动结束日期</param>
        /// <param name="Status">状态</param>
        /// <returns>返回执行码</returns>
        public static XReturn QueryProductStockChangeTotal(int WarehouseID, int ProductID, int ChangeType, string FromChangeDate, string ToChangeDate, int Status)
        {
            string strSql = String.Empty, strWhere;

            strWhere = "1=1";
            if (WarehouseID != 0) strWhere += string.Format(" and b.WarehouseID={0:D}", WarehouseID);
            if (ProductID != 0) strWhere += string.Format(" and b.ProductID={0:D}", ProductID);
            if (ChangeType != 0) strWhere += string.Format(" and a.ChangeType={0:D}", ChangeType);
            if (FromChangeDate != String.Empty) strWhere += string.Format(" and a.ChangeDate>='{0:S}'", FromChangeDate);
            if (ToChangeDate != String.Empty) strWhere += string.Format(" and a.ChangeDate<='{0:S}'", ToChangeDate);
            if (Status != 0) strWhere += string.Format(" and a.Status={0:D}", Status);

            strSql = "select count(*) as TotalCount,isnull(sum(a.ChangeQuantity),0) as TotalQuantity from IVT_ProductStockChange a " +
                    " left join IVT_ProductStock b on a.ProductStockID=b.ProductStockID where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().ExecuteMultiScalar(strSql);
        }

        /// <summary>
        /// 查询商品库存变动。
        /// </summary>
        /// <param name="WarehouseID">仓库ID</param>
        /// <param name="ProductID">商品ID</param>
        /// <param name="ChangeType">变动类型</param>
        /// <param name="FromChangeDate">变动开始日期</param>
        /// <param name="ToChangeDate">变动结束日期</param>
        /// <param name="Status">状态</param>
        /// <param name="StartIndex">起始行序号</param>
        /// <param name="MaxCount">最大行数</param>
        /// <param name="tbProductStockChanges">返回查询出库结果表</param>
        /// <returns>返回执行码</returns>
        public static XReturn QueryProductStockChangeList(int WarehouseID, int ProductID, int ChangeType, string FromChangeDate, string ToChangeDate, int Status, int StartIndex, int MaxCount, out DataTable tbProductStockChanges)
        {
            string strSql = String.Empty, strWhere;
            tbProductStockChanges = null;

            strWhere = "1=1";
            if (WarehouseID != 0) strWhere += string.Format(" and c.WarehouseID={0:D}", WarehouseID);
            if (ProductID != 0) strWhere += string.Format(" and e.ProductID={0:D}", ProductID);
            if (ChangeType != 0) strWhere += string.Format(" and a.ChangeType={0:D}", ChangeType);
            if (FromChangeDate != String.Empty) strWhere += string.Format(" and a.ChangeDate>='{0:S}'", FromChangeDate);
            if (ToChangeDate != String.Empty) strWhere += string.Format(" and a.ChangeDate<='{0:S}'", ToChangeDate);
            if (Status != 0) strWhere += string.Format(" and a.Status={0:D}", Status);

            strSql = "select a.*,c.WarehouseName,d.ProductNo,d.ProductName,d.ProductUnit,e.ProductSpec,f.OperatorName from IVT_ProductStockChange a" +
                " left join IVT_ProductStock b on a.ProductStockID=b.ProductStockID " +
                " left join BSC_Warehouse c on b.WarehouseID=c.WarehouseID " +
                " left join BSC_Product d on b.ProductID=d.ProductID " +
                " left join BSC_ProductSpec e on b.ProductSpecID=e.ProductSpecID " +
                " left join LPD_Operator f on a.OperatorID=f.OperatorID " +
                " where {0:S}" +
                " order by a.CreateTime desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out tbProductStockChanges);
        }
        #endregion

        #region 库存变动接口
        /// <summary>
        /// 递交商品库存变动。
        /// </summary>
        /// <param name="RequestParameter">请求参数</param>
        /// <returns>返回执行结果</returns>
        static public XReturn SubmitProductStockChange(ProductStockChangeRequestParameter RequestParameter)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();

            xTransaction.BeginTransition();

            xSubReturn = SubmitProductStockChange(xTransaction, RequestParameter);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();

                return xReturn.ReturnError(xSubReturn);
            }

            xTransaction.Commit();

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 递交商品库存变动。
        /// </summary>
        /// <param name="Transaction">事务</param>
        /// <param name="RequestParameter">请求参数</param>
        /// <returns>返回执行结果</returns>
        static public XReturn SubmitProductStockChange(TransactionWork Transaction, ProductStockChangeRequestParameter RequestParameter)
        {
            string strErrorMsg;
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            // 查询商品规格。
            BSC_ProductSpec ProductSpec = new BSC_ProductSpec();
            ProductSpec.ProductSpecID = RequestParameter.ProductSpecID;
            xSubReturn = ProductSpec.x.SelectByIdentity(Transaction);
            if (xSubReturn.IsUnSuccess())
            {
                strErrorMsg = string.Format("找不到商品规格记录[ID={0:D}]", RequestParameter.ProductSpecID);
                return xReturn.ReturnError(strErrorMsg);
            }

            // 查询商品。
            BSC_Product Product = new BSC_Product();
            Product.ProductID = ProductSpec.ProductID;
            xSubReturn = Product.x.SelectByIdentity(Transaction);
            if (xSubReturn.IsUnSuccess())
            {
                strErrorMsg = string.Format("找不到商品记录[ID={0:D}]", ProductSpec.ProductID);
                return xReturn.ReturnError(strErrorMsg);
            }

            // 查询商品库存记录。
            IVT_ProductStock ProductStock = new IVT_ProductStock();
            ProductStock.ProductSpecID = RequestParameter.ProductSpecID;
            ProductStock.WarehouseID = RequestParameter.WarehouseID;
            xSubReturn = ProductStock.x.Select(Transaction);
            if (xSubReturn.IsUnSuccess())
            {
                ProductStock.ProductID = ProductSpec.ProductID;
                ProductStock.ProductSpecID = RequestParameter.ProductSpecID;
                ProductStock.WarehouseID = RequestParameter.WarehouseID;
                ProductStock.Quantity = 0;
                ProductStock.FrozenQuantity = 0;
                xSubReturn = ProductStock.x.Insert(Transaction);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn);
                }
            }

            // 设置商品库存变动记录。
            IVT_ProductStockChange ProductStockChange = new IVT_ProductStockChange();
            ProductStockChange.ProductStockID= ProductStock.ProductStockID;
            ProductStockChange.ChangeDate = RequestParameter.ChangeDate != String.Empty ? RequestParameter.ChangeDate : DateTimeUtil.GetShortDateString();
            ProductStockChange.ChangeReason = RequestParameter.ChangeReason;
            ProductStockChange.AssociateVoucherType = RequestParameter.AssociateVoucherType;
            ProductStockChange.AssociateVoucherNo = RequestParameter.AssociateVoucherNo;
            ProductStockChange.AssociateID = RequestParameter.AssociateID;
            ProductStockChange.CreateTime = DateTime.Now;
            ProductStockChange.Status = (int)EProductStockChangeStatus.OK;

            // 插入商品库存变动记录。
            if (RequestParameter.ChangeType > 0)
            {
                ProductStockChange.ChangeType = RequestParameter.ChangeType;
                ProductStockChange.ChangeQuantity = RequestParameter.ChangeQuantity;
                
                // 插入商品库存变动记录。
                xSubReturn = ProductStockChange.x.Insert(Transaction);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn);
                }

                ProductStock.Quantity += RequestParameter.ChangeType == (int)EProductStockChangeType.StockIn ? RequestParameter.ChangeQuantity : -RequestParameter.ChangeQuantity;;
            }

            // 插入商品库存冻结变动记录。
            if (RequestParameter.FrozenChangeType > 0)
            {
                ProductStockChange.ChangeType = RequestParameter.FrozenChangeType;
                ProductStockChange.ChangeQuantity = RequestParameter.FrozenChangeQuantity;

                // 插入商品库存变动记录。
                xSubReturn = ProductStockChange.x.Insert(Transaction);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn);
                }

                ProductStock.FrozenQuantity += RequestParameter.FrozenChangeType == (int)EProductStockChangeType.StockFrozen ? RequestParameter.FrozenChangeQuantity : -RequestParameter.FrozenChangeQuantity;
            }

            // 更新商品库存数量。
            ProductStock.RemainQuantity = ProductStock.Quantity - ProductStock.FrozenQuantity;
            xSubReturn = ProductStock.x.UpdateByIdentity(Transaction);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "更新商品库存数量失败");
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 撤销商品库存变动。
        /// </summary>
        /// <param name="ProductSpecID">商品规格ID</param>
        /// <param name="WarehouseID">仓库ID</param>
        /// <param name="AssociateVoucherType">关联凭单类型</param>
        /// <param name="AssociateVoucherNo">关联凭单编号</param>
        /// <param name="AssociateID">关联ID</param>
        /// <returns></returns>
        static public XReturn CancelProductStockChange(int ProductSpecID, int WarehouseID, int AssociateVoucherType, string AssociateVoucherNo, int AssociateID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();

            xTransaction.BeginTransition();

            xSubReturn = CancelProductStockChange(xTransaction, ProductSpecID, WarehouseID, AssociateVoucherType, AssociateVoucherNo, AssociateID);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();

                xReturn.SetError(xSubReturn);
                return xReturn;
            }

            xTransaction.Commit();
            xReturn.ReturnCode = "0000";
            return xReturn;
        }

        /// <summary>
        /// 撤销商品库存变动。
        /// </summary>
        /// <param name="Transaction">事务</param>
        /// <param name="ProductSpecID">商品规格ID</param>
        /// <param name="WarehouseID">仓库ID</param>
        /// <param name="AssociateVoucherType">关联凭单类型</param>
        /// <param name="AssociateVoucherNo">关联凭单编号</param>
        /// <param name="AssociateID">关联ID</param>
        /// <returns></returns>
        static public XReturn CancelProductStockChange(TransactionWork transactionWork, int ProductSpecID, int WarehouseID, int AssociateVoucherType, string AssociateVoucherNo, int AssociateID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            // 查询商品库存记录。
            IVT_ProductStock ProductStock = new IVT_ProductStock();
            ProductStock.ProductSpecID = ProductSpecID;
            ProductStock.WarehouseID = WarehouseID;
            xSubReturn = ProductStock.x.Select(transactionWork);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "找不到商品库存记录");
            }

            // 查询商品库存变动记录。
            Hashtable FilterValues = new Hashtable();
            FilterValues["AssociateVoucherType"] = AssociateVoucherType;
            FilterValues["AssociateVoucherNo"] = AssociateVoucherNo;
            FilterValues["AssociateID"] = AssociateID;

            ArrayList Entities;
            xSubReturn = BaseEntityEx.MSelect(transactionWork, "IVT_ProductStockChange", FilterValues, out Entities);
            if (Entities.Count != 1)
            {
                return xReturn.ReturnError("找不到库存变动记录");
            }
            IVT_ProductStockChange ProductStockChange = (IVT_ProductStockChange)Entities[0];
            double ChangeQuantity = ProductStockChange.ChangeType == (int)EProductStockChangeType.StockOut ? ProductStockChange.ChangeQuantity : -ProductStockChange.ChangeQuantity;

            // 更改商品库存变动状态为撤销。
            ProductStockChange.Status = (int)EProductStockChangeStatus.Cancel;
            xSubReturn = ProductStockChange.x.UpdateByIdentity(transactionWork);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "更新商品库存变动状态失败");
            }

            // 更新商品库存数量。
            ProductStock.Quantity += ChangeQuantity;
            xSubReturn = ProductStock.x.UpdateByIdentity(transactionWork);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "更新商品库存数量失败");
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 撤销订单商品库存变动。
        /// </summary>
        /// <param name="transactionWork">事务</param>
        /// <param name="OrderID">订单ID</param>
        /// <returns></returns>
        static public XReturn CancelOrderProductStockChange(TransactionWork transactionWork, int OrderID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            EntityQueryer qryProductStockChange = new EntityQueryer("IVT_ProductStockChange");
            qryProductStockChange.AddFilter("AssociateVoucherType", (int)EVoucherType.SalesOrder);
            qryProductStockChange.AddFilter("AssociateID", OrderID);
            xSubReturn = qryProductStockChange.Query(transactionWork);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError("查询商品库存变动失败");
            }
            if (qryProductStockChange.EntityCount == 0)
            {
                return xReturn.ReturnError("查询商品库存变动无记录");
            }

            foreach (IVT_ProductStockChange ProductStockChange in qryProductStockChange.EntityList)
            {

                // 查询商品库存记录。
                IVT_ProductStock ProductStock = new IVT_ProductStock();
                ProductStock.ProductStockID = ProductStockChange.ProductStockID;
                xSubReturn = ProductStock.x.SelectByIdentity(transactionWork);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, "找不到商品库存记录");
                }

                double ChangeQuantity = ProductStockChange.ChangeType == (int)EProductStockChangeType.StockOut ? ProductStockChange.ChangeQuantity : -ProductStockChange.ChangeQuantity;

                // 更改商品库存变动状态为撤销。
                ProductStockChange.Status = (int)EProductStockChangeStatus.Cancel;
                xSubReturn = ProductStockChange.x.UpdateByIdentity(transactionWork);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, "更新商品库存变动状态失败");
                }

                // 更新商品库存数量。
                ProductStock.Quantity += ChangeQuantity;
                xSubReturn = ProductStock.x.UpdateByIdentity(transactionWork);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, "更新商品库存数量失败");
                }
            }

            return xReturn.ReturnSuccess();
        }
        #endregion
    }
}
