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
using Agape.Manage.Core.Session;

namespace Agape.Manage.Core.Impl
{
    public class BatchImpl
    {
        /// <summary>
        /// 查询批次数目。
        /// </summary>
        /// <param name="BatchNo">批次号</param>
        /// <param name="FromDate">开始时间</param>
        /// <param name="ToDate">结束时间</param>
        /// <param name="StatusSet">状态集合</param>
        /// <param name="HasOrder">有订单</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryBatchCount(string BatchNo, string FromDate, string ToDate, string StatusSet, bool HasOrder)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (BatchNo != String.Empty) strWhere += string.Format(" and BatchNo='{0:S}'", BatchNo);
            if (FromDate != String.Empty) strWhere += string.Format(" and FromDate>='{0:S}'", FromDate);
            if (ToDate != String.Empty) strWhere += string.Format(" and ToDate>='{0:S}'", ToDate);
            if (StatusSet != String.Empty) strWhere += string.Format(" and Status in ({0:S})", StatusSet);
            if (HasOrder) strWhere += " and ConfirmOrderCount>0";

            strSql = "select count(*) from SLS_Batch where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 查询批次列表。
        /// </summary>
        /// <param name="BatchNo">批次号</param>
        /// <param name="FromDate">开始时间</param>
        /// <param name="ToDate">结束时间</param>
        /// <param name="StatusSet">状态集合</param>
        /// <param name="HasOrder">有订单</param>
        /// <param name="StartIndex">开始序号</param>
        /// <param name="MaxCount">最大返回数量</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryBatchList(string BatchNo, string FromDate, string ToDate, string StatusSet, bool HasOrder, int StartIndex, int MaxCount, out DataTable ReturnTable)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            string strSql, strWhere;

            strWhere = "1=1";
            if (BatchNo != String.Empty) strWhere += string.Format(" and BatchNo='{0:S}'", BatchNo);
            if (FromDate != String.Empty) strWhere += string.Format(" and FromDate>='{0:S}'", FromDate);
            if (ToDate != String.Empty) strWhere += string.Format(" and ToDate>='{0:S}'", ToDate);
            if (StatusSet != String.Empty) strWhere += string.Format(" and Status in ({0:S})", StatusSet);
            if (HasOrder) strWhere += " and ConfirmOrderCount>0";

            strSql = "select * from SLS_Batch where {0:S} order by BatchNo desc";
            strSql = string.Format(strSql, strWhere);

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out ReturnTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询批次。
        /// </summary>
        /// <param name="BatchNo">批次号</param>
        /// <returns></returns>
        public static XReturn QueryBatch(string BatchNo)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            ArrayList Entities;
            Hashtable FilterValues = new Hashtable();
            FilterValues["BatchNo"] = BatchNo;
            xSubReturn = BaseEntityEx.MSelect("SLS_Batch", FilterValues, out Entities);
            if (xSubReturn.IsUnSuccess() || Entities.Count != 1)
            {
                return xReturn.ReturnError("查询批次失败");
            }
            xReturn.ReturnValue = Entities[0];

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询订单数目。
        /// </summary>
        /// <param name="BatchID">批次ID</param>
        /// <param name="OrderNo">订单编号</param>
        /// <param name="BarCode">条形码</param>
        /// <param name="BatchStatus">批次状态</param>
        /// <param name="OrderStatus">订单状态集合（使用逗号分割）</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryBatchOrderCount(int BatchID, string OrderNo, string BarCode, int BatchStatus, string OrderStatus)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (BatchID > 0) strWhere += string.Format(" and a.BatchID={0:D}", BatchID);
            if (OrderNo != String.Empty) strWhere += string.Format(" and a.OrderNo='{0:S}'", OrderNo);
            if (BarCode != String.Empty) strWhere += string.Format(" and a.BarCode='{0:S}'", BarCode);
            if (BatchStatus > 0) strWhere += string.Format(" and a.BatchStatus={0:D}", BatchStatus);
            if (!String.IsNullOrEmpty(OrderStatus)) strWhere += string.Format(" and a.Status in ({0:S})", OrderStatus);

            strSql = "select count(*) from SLS_Order a where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 查询批次订单列表。
        /// </summary>
        /// <param name="BatchID">批次ID</param>
        /// <param name="OrderNo">订单编号</param>
        /// <param name="BarCode">条形码</param>
        /// <param name="BatchStatus">批次状态</param>
        /// <param name="OrderStatus">订单状态集合（使用逗号分割）</param>
        /// <param name="StartIndex">开始序号</param>
        /// <param name="MaxCount">最大返回数量</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryBatchOrderList(int BatchID, string OrderNo, string BarCode, int BatchStatus, string OrderStatus, int StartIndex, int MaxCount, out DataTable ReturnTable)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            string strSql, strWhere;

            strWhere = "1=1";
            if (BatchID > 0) strWhere += string.Format(" and a.BatchID={0:D}", BatchID);
            if (OrderNo != String.Empty) strWhere += string.Format(" and a.OrderNo='{0:S}'", OrderNo);
            if (BarCode != String.Empty) strWhere += string.Format(" and a.BarCode='{0:S}'", BarCode);
            if (BatchStatus > 0) strWhere += string.Format(" and a.BatchStatus={0:D}", BatchStatus);
            if (!String.IsNullOrEmpty(OrderStatus)) strWhere += string.Format(" and a.Status in ({0:S})", OrderStatus);

            strSql = "select a.*,b.BatchNo,c.MemberName,c.RealName,d.ReceiverName from SLS_Order a " +
              " left join SLS_Batch b on b.BatchID=a.BatchID" +
              " left join BSC_Member c on c.MemberID=a.MemberID" +
              " left join BSC_MemberAddress d on d.MemberAddressID=a.MemberAddressID" +
              " where {0:S} order by CreateTime desc";
            strSql = string.Format(strSql, strWhere);

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out ReturnTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 批次订单进行确认处理。
        /// </summary>
        /// <param name="OrderID">订单ID</param>
        /// <returns>返回执行结果</returns>
        public static XReturn BatchOrderConfirm(int OrderID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            if (!OperatorSession.IsLogin)
            {
                return xReturn.ReturnError("请先登录");
            }

            // 查询订单记录
            SLS_Order Order = new SLS_Order();
            Order.OrderID = OrderID;
            xSubReturn = Order.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "找不到订单记录");
            }

            if (Order.PaymentDeliverType == (int)EPaymentDeliverType.ByHand)
            {
                if (Order.Status != (int)EOrderStatus.Submit)
                {
                    return xReturn.ReturnError("该订单不是订单提交状态");
                }
            }

            else
            {
                if (Order.Status != (int)EOrderStatus.Payed)
                {
                    return xReturn.ReturnError("该订单不是订单已付款状态");
                }
            }

            // 查询会员
            BSC_Member Member = new BSC_Member();
            Member.MemberID = Order.MemberID;
            xSubReturn = Member.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "会员不存在");
            }

            // 查询订单扩展记录
            SLS_OrderExtension OrderExtension = new SLS_OrderExtension();
            OrderExtension.OrderID = OrderID;
            xSubReturn = OrderExtension.x.Select();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "找不到订单扩展记录");
            }

            // 查询订单明细记录
            EntityQueryer entityQueryer = new EntityQueryer("SLS_OrderItem");
            entityQueryer.AddFilter("OrderID", OrderID);
            xSubReturn = entityQueryer.Query();
            if (xSubReturn.IsUnSuccess() || entityQueryer.EntityCount <= 0)
            {
                return xReturn.ReturnError(xSubReturn, "找不到订单明细记录");
            }

            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();
            xTransaction.BeginTransition();

            if (Order.ShoppingType == (int)EShoppingType.Exchange)
            {
                // 更新会员积分
                Member.FrozenPointValue -= Order.TotalExchangePointValue;
                Member.PointValue -= Order.TotalExchangePointValue;
                xSubReturn = Member.x.UpdateByIdentity(xTransaction);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, "更新会员积分失败");
                }
            }

            // 处理订单明细
            foreach (SLS_OrderItem OrderItem in entityQueryer.EntityList)
            {
                ProductStockChangeRequestParameter RequestParameter = new ProductStockChangeRequestParameter();
                RequestParameter.WarehouseID = 0;
                RequestParameter.ProductSpecID = OrderItem.ProductSpecID;
                RequestParameter.ChangeType = (int)EProductStockChangeType.StockOut;
                RequestParameter.ChangeQuantity = OrderItem.Quantity;
                RequestParameter.FrozenChangeType = (int)EProductStockChangeType.StockUnFrozen;
                RequestParameter.FrozenChangeQuantity = OrderItem.Quantity;
                RequestParameter.ChangeReason = "BatchOrderConfirm";

                xSubReturn = InventoryImpl.SubmitProductStockChange(xTransaction, RequestParameter);
                if (xSubReturn.IsUnSuccess())
                {
                    xTransaction.Rollback();

                    return xReturn.ReturnError(xSubReturn, "提交商品库存变动失败");
                }
            }

            // 更新订单状态
            Order.Status = (int)EOrderStatus.Process;
            Order.BatchStatus = (int)EBatchStatus.OrderConfirm;
            Order.BatchID = BatchCache.Current.GetCurrentBatchID();
            xSubReturn = Order.x.UpdateByIdentity(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();

                return xReturn.ReturnError(xSubReturn, "更新订单状态失败");
            }

            // 更新订单扩展记录
            OrderExtension.ConfirmTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            OrderExtension.ConfirmOperatorID = OperatorSession.Operator.OperatorID;
            xSubReturn = OrderExtension.x.Update(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();

                return xReturn.ReturnError(xSubReturn, "更新订单扩展记录失败");
            }

            xTransaction.Commit();

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 批次订单进行拣货处理。
        /// </summary>
        /// <param name="BatchID">批次ID</param>
        /// <returns>返回执行结果</returns>
        public static XReturn BatchOrderPick(int BatchID)
        {
            string strSql;
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();

            SLS_Batch Batch = new SLS_Batch();
            Batch.BatchID = BatchID;
            xSubReturn = Batch.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询批次失败");
            }

            if (Batch.Status != (int)EBatchStatus.OrderConfirm)
            {
                return xReturn.ReturnError("该批次状态不是确认状态");
            }

            strSql = "select count(*) as OrderCount, sum(TotalQuantity) as TotalQuantity, sum(TotalAmount) as TotalAmount from SLS_Order where BatchID={0:D} and BatchStatus={1:D}";
            strSql = string.Format(strSql, BatchID, (int)EBatchStatus.OrderConfirm);
            xSubReturn = DatabaseFactory.GetCurrent().ExecuteMultiScalar(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, string.Format("查询批次[{0:S}]订单汇总信息失败", Batch.BatchNo));
            }

            Batch.PickOrderCount = xSubReturn.GetIntValue("OrderCount");
            Batch.PickTotalQuantity = xSubReturn.GetDoubleValue("TotalQuantity");
            Batch.PickTotalAmount = xSubReturn.GetDoubleValue("TotalAmount");
            Batch.Status = (int)EBatchStatus.OrderPick;

            xTransaction.BeginTransition();

            strSql = "update SLS_OrderExtension set PickTime='{2:S}',PickOperatorID={3:D} where OrderID in (select OrderID from SLS_Order where BatchID={0:D} and BatchStatus={1:D})";
            strSql = string.Format(strSql, BatchID, (int)EBatchStatus.OrderConfirm, DateTime.Now.ToString("yyyyMMddHHmmss"), 1);
            xSubReturn = DatabaseFactory.GetCurrent().ExecuteNonQuery(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, string.Format("更新批次[{0:S}]所有订单扩展信息失败", Batch.BatchNo));
            }

            strSql = "update SLS_Order set BatchStatus={2:D} where BatchID={0:D} and BatchStatus={1:D}";
            strSql = string.Format(strSql, BatchID, (int)EBatchStatus.OrderConfirm, (int)EBatchStatus.OrderPick);
            xSubReturn = DatabaseFactory.GetCurrent().ExecuteNonQuery(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, string.Format("更新批次[{0:S}]所有订单信息失败", Batch.BatchNo));
            }

            xSubReturn = Batch.x.UpdateByIdentity(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, string.Format("更新批次[{0:S}]状态失败", Batch.BatchNo));
            }

            xTransaction.Commit();

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询批次商品列表。
        /// </summary>
        /// <param name="BatchID">批次ID</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryBatchProductList(int BatchID, out DataTable ReturnTable)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            string strSql, strWhere;

            strWhere = "1=1";
            if (BatchID > 0) strWhere += string.Format(" and b.BatchID={0:D}", BatchID);
            strWhere += string.Format("and b.BatchStatus>={0:D}", (int)EBatchStatus.OrderConfirm);

            strSql = "select a.*,b.ProductNo,b.ProductName,b.ProductUnit,c.ProductSpec from " +
                    " (select a.ProductID,a.ProductSpecID,sum(a.Quantity) TotalQuantity,sum(a.Amount) as TotalAmount from SLS_OrderItem a " +
                    "  left join SLS_Order b on a.OrderID=b.OrderID " +
                    "  where {0:S}" +
                    "  group by ProductID,ProductSpecID) a" +
                    " left join BSC_Product b on a.ProductID=b.ProductID" +
                    " left join BSC_ProductSpec c on a.ProductSpecID=c.ProductSpecID";
            strSql = string.Format(strSql, strWhere);

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 批次订单进行拣货处理。
        /// </summary>
        /// <param name="OrderID">订单ID</param>
        /// <param name="BarCode">条形码</param>
        /// <returns>返回执行结果</returns>
        public static XReturn BatchOrderPack(int OrderID, string BarCode)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            // 查询订单信息
            SLS_Order Order = new SLS_Order();
            Order.OrderID = OrderID;
            xSubReturn = Order.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询无此订单");
            }
            if (Order.BatchStatus != (int)EBatchStatus.OrderPick)
            {
                return xReturn.ReturnError("订单批次状态不是拣货状态");
            }

            // 查询订单扩展信息
            SLS_OrderExtension OrderExtension = new SLS_OrderExtension();
            OrderExtension.OrderID = OrderID;
            xSubReturn = OrderExtension.x.Select();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "此订单无扩展信息");
            }

            // 查询批次信息
            SLS_Batch Batch = new SLS_Batch();
            Batch.BatchID = Order.BatchID;
            xSubReturn = Batch.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询批次失败");
            }
            if (Batch.Status != (int)EBatchStatus.OrderPick)
            {
                return xReturn.ReturnError("该批次状态不是拣货状态");
            }

            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();
            xTransaction.BeginTransition();

            // 更新订单信息
            Order.BatchStatus = (int)EBatchStatus.OrderPack;
            Order.BarCode = BarCode;
            xSubReturn = Order.x.UpdateByIdentity(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, "更新订单状态失败");
            }

            // 更新订单扩展信息
            OrderExtension.PackOperatorID = OperatorSession.Operator.OperatorID;
            OrderExtension.PackTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            xSubReturn = OrderExtension.x.Update(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, "更新订单扩展信息失败");
            }

            // 判断该批次是否还有拣货之前状态的订单
            string strSql = string.Format("select count(*) from SLS_Order where BatchID={0:D} and BatchStatus>0 and BatchStatus<={1:D}", Batch.BatchID, (int)EBatchStatus.OrderPick);
            xSubReturn = xTransaction.ExecuteScalar(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, "查询订单失败");
            }
            int RowCount = (int)xSubReturn.ReturnValue;
            if (RowCount == 0)
            {
                Batch.Status = (int)EBatchStatus.OrderPack;
            }

            Batch.PackOrderCount++;
            Batch.PackTotalQuantity += Order.TotalQuantity;
            Batch.PackTotalAmount += Order.TotalAmount;
            xSubReturn = Batch.x.UpdateByIdentity(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, "更新批次状态失败");
            }

            xTransaction.Commit();

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 批次订单进行配送处理。
        /// </summary>
        /// <param name="OrderID">订单ID</param>
        /// <returns>返回执行结果</returns>
        public static XReturn BatchOrderDeliver(int OrderID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            // 查询订单信息
            SLS_Order Order = new SLS_Order();
            Order.OrderID = OrderID;
            xSubReturn = Order.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询无此订单");
            }
            if (Order.BatchStatus != (int)EBatchStatus.OrderPack)
            {
                return xReturn.ReturnError("该订单批次状态不是打包状态");
            }

            // 查询订单扩展信息
            SLS_OrderExtension OrderExtension = new SLS_OrderExtension();
            OrderExtension.OrderID = OrderID;
            xSubReturn = OrderExtension.x.Select();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "此订单无扩展信息");
            }

            // 查询批次信息
            SLS_Batch Batch = new SLS_Batch();
            Batch.BatchID = Order.BatchID;
            xSubReturn = Batch.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询批次失败");
            }
            if (Batch.Status != (int)EBatchStatus.OrderPick && Batch.Status != (int)EBatchStatus.OrderPack)
            {
                return xReturn.ReturnError("该批次状态不是拣货或者打包状态");
            }

            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();
            xTransaction.BeginTransition();

            // 更新订单信息
            Order.BatchStatus = (int)EBatchStatus.OrderDeliver;
            xSubReturn = Order.x.UpdateByIdentity(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, "更新订单状态失败");
            }

            // 更新订单扩展信息
            OrderExtension.DeliverOperatorID = OperatorSession.Operator.OperatorID;
            OrderExtension.DeliverTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            xSubReturn = OrderExtension.x.Update(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, "更新订单扩展信息失败");
            }

            // 判断该批次是否还有打包之前状态的订单
            string strSql = string.Format("select count(*) from SLS_Order where BatchID={0:D} and BatchStatus>0 and BatchStatus<={1:D}", Batch.BatchID, (int)EBatchStatus.OrderPack);
            xSubReturn = xTransaction.ExecuteScalar(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, "查询订单失败");
            }
            int RowCount = (int)xSubReturn.ReturnValue;
            if (RowCount == 0)
            {
                Batch.Status = (int)EBatchStatus.OrderDeliver;
            }

            Batch.DeliverOrderCount++;
            Batch.DeliverTotalQuantity += Order.TotalQuantity;
            Batch.DeliverTotalAmount += Order.TotalAmount;
            xSubReturn = Batch.x.UpdateByIdentity(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, "更新批次状态失败");
            }

            xTransaction.Commit();

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 批次订单进行完成处理。
        /// </summary>
        /// <param name="OrderID">订单ID</param>
        /// <returns>返回执行结果</returns>
        public static XReturn BatchOrderFinish(int OrderID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            // 查询订单信息
            SLS_Order Order = new SLS_Order();
            Order.OrderID = OrderID;
            xSubReturn = Order.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询无此订单");
            }
            if (Order.BatchStatus != (int)EBatchStatus.OrderDeliver)
            {
                return xReturn.ReturnError("该订单批次状态不是配送状态");
            }

            // 查询订单扩展信息
            SLS_OrderExtension OrderExtension = new SLS_OrderExtension();
            OrderExtension.OrderID = OrderID;
            xSubReturn = OrderExtension.x.Select();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "此订单无扩展信息");
            }

            // 查询批次信息
            SLS_Batch Batch = new SLS_Batch();
            Batch.BatchID = Order.BatchID;
            xSubReturn = Batch.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询批次失败");
            }

            BSC_Member Member = new BSC_Member();
            Member.MemberID = Order.MemberID;
            xSubReturn = Member.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询会员失败");
            }

            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();
            xTransaction.BeginTransition();

            // 更新订单信息
            Order.Status = (int)EOrderStatus.Finish;
            Order.BatchStatus = (int)EBatchStatus.OrderFinish;
            xSubReturn = Order.x.UpdateByIdentity(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, "更新订单状态失败");
            }

            if (Order.CouponPayAmount > 0)
            {
                // 查询使用会员优惠劵
                EntityQueryer qryUseMemberCoupon = new EntityQueryer("BSC_MemberCoupon");
                qryUseMemberCoupon.AddFilter("ToOrderID", Order.OrderID);
                qryUseMemberCoupon.AddFilter("Status", (int)EMemberCouponStatus.Assign);
                xSubReturn = qryUseMemberCoupon.Query();
                if (xSubReturn.IsUnSuccess())
                {
                    xTransaction.Rollback();
                    return xReturn.ReturnError(xSubReturn, "查询订单使用会员优惠劵失败");
                }

                if (qryUseMemberCoupon.EntityCount == 0)
                {
                    xTransaction.Rollback();
                    return xReturn.ReturnError("订单使用会员优惠劵不存在");
                }

                // 更新使用会员优惠劵状态
                foreach (BSC_MemberCoupon UseMemberCoupon in qryUseMemberCoupon.EntityList)
                {
                    UseMemberCoupon.Status = (int)EMemberCouponStatus.Finish;
                    xSubReturn = UseMemberCoupon.x.UpdateByIdentity(xTransaction);
                    if (xSubReturn.IsUnSuccess())
                    {
                        xTransaction.Rollback();
                        return xReturn.ReturnError(xSubReturn, "更新订单使用会员优惠劵失败");
                    }
                }
            }

            // 查询发放会员优惠劵
            EntityQueryer qryGrantMemberCoupon = new EntityQueryer("BSC_MemberCoupon");
            qryGrantMemberCoupon.AddFilter("FromOrderID", Order.OrderID);
            qryGrantMemberCoupon.AddFilter("Status", (int)EMemberCouponStatus.Grant);
            xSubReturn = qryGrantMemberCoupon.Query();
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, "查询订单发放会员优惠劵失败");
            }

            if (qryGrantMemberCoupon.EntityCount > 0)
            {
                // 更新发放会员优惠劵状态
                foreach (BSC_MemberCoupon GrantMemberCoupon in qryGrantMemberCoupon.EntityList)
                {
                    GrantMemberCoupon.Status = (int)EMemberCouponStatus.Active;
                    xSubReturn = GrantMemberCoupon.x.UpdateByIdentity(xTransaction);
                    if (xSubReturn.IsUnSuccess())
                    {
                        xTransaction.Rollback();
                        return xReturn.ReturnError(xSubReturn, "更新订单发放会员优惠劵失败");
                    }
                }
            }

            // 更新订单扩展信息
            OrderExtension.FinishOperatorID = OperatorSession.Operator.OperatorID;
            OrderExtension.FinishTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            xSubReturn = OrderExtension.x.Update(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, "更新订单扩展信息失败");
            }

            // 判断该批次是否还有配送之前状态的订单
            string strSql = string.Format("select count(*) from SLS_Order where BatchID={0:D} and BatchStatus>0 and BatchStatus<={1:D}", Batch.BatchID, (int)EBatchStatus.OrderDeliver);
            xSubReturn = xTransaction.ExecuteScalar(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, "查询订单失败");
            }
            int RowCount = (int)xSubReturn.ReturnValue;
            if (RowCount == 0)
            {
                Batch.Status = (int)EBatchStatus.OrderFinish;
            }

            Batch.FinishOrderCount++;
            Batch.FinishTotalQuantity += Order.TotalQuantity;
            Batch.FinishTotalAmount += Order.TotalAmount;
            xSubReturn = Batch.x.UpdateByIdentity(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn, "更新批次状态失败");
            }

            // 更新会员信息
            if (Order.TotalPointValue > 0)
            {
                Member.PointValue += Order.TotalPointValue;
                xSubReturn = Member.x.UpdateByIdentity(xTransaction);
                if (xSubReturn.IsUnSuccess())
                {
                    xTransaction.Rollback();
                    return xReturn.ReturnError(xSubReturn, "更新会员信息失败");
                }
            }

            xTransaction.Commit();

            return xReturn.ReturnSuccess();
        }
    }
}
