using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Data.SqlClient;
using Leopard.Util;
using Leopard.Data;
using log4net;
using System.Reflection;
using Agape.Manage.Core.Util;
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Cache;

namespace Agape.Manage.Core.Impl
{
    public class MemberImpl
    {
        #region 会员基本接口
        /// <summary>
        /// 保存会员信息。
        /// </summary>
        /// <param name="Member">会员对象</param>
        /// <param name="MemberAddress">会员地址对象</param>
        /// <returns></returns>
        public static XReturn SaveMemberInfo(BSC_Member Member, BSC_MemberAddress MemberAddress)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();

            xTransaction.BeginTransition();

            if (MemberAddress.MemberAddressID == 0)
            {
                MemberAddress.MemberID = Member.MemberID;
                xSubReturn = MemberAddress.x.Insert(xTransaction);
                if (xSubReturn.IsUnSuccess())
                {
                    xTransaction.Rollback();
                    return xReturn.ReturnError(xSubReturn, "增加默认会员地址失败");
                }

                Member.DefaultMemberAddressID = MemberAddress.MemberAddressID;
            }
            else
            {
                xSubReturn = MemberAddress.x.UpdateByIdentity(xTransaction);
                if (xSubReturn.IsUnSuccess())
                {
                    xTransaction.Rollback();
                    return xReturn.ReturnError(xSubReturn, "更新默认会员地址失败");
                }
            }

            xSubReturn = Member.x.UpdateByIdentity(xTransaction);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();

                return xReturn.ReturnError(xSubReturn, "更新会员失败");
            }

            xTransaction.Commit();

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 修改密码。
        /// </summary>
        /// <param name="MemberID">会员ID</param>
        /// <param name="OldPassword">原密码</param>
        /// <param name="NewPassword">新密码</param>
        /// <returns></returns>
        public static XReturn ChangePassword(int MemberID, string OldPassword, string NewPassword)
        {
            string strSql;
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            strSql = string.Format("select count(*) from BSC_Member where MemberID={0:D} and Password='{1:S}'", MemberID, OldPassword);
            xSubReturn = DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "执行验证原密码语句失败");
            }
            if (xSubReturn.ReturnValue.Equals(0))
            {
                return xReturn.ReturnError("原密码错误");
            }

            strSql = string.Format("update BSC_Member set Password='{2:S}' where MemberID={0:D} and Password='{1:S}'", MemberID, OldPassword, NewPassword);
            xSubReturn = DatabaseFactory.GetCurrent().ExecuteNonQuery(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "执行更改新密码语句失败");
            }
            if (xSubReturn.RowCount != 1)
            {
                return xReturn.ReturnError("更改新密码失败");
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询会员数目。
        /// </summary>
        /// <param name="MemberID">会员ID</param>
        /// <param name="MemberNo">会员编号</param>
        /// <param name="MemberName">会员名</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryMemberCount(int MemberID, string MemberNo,string MemberName)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (MemberID > 0) strWhere += string.Format(" and MemberID={0:D}", MemberID);
            if (MemberNo != String.Empty) strWhere += string.Format(" and MemberNo like '%{0:S}%'", MemberNo);
            if (MemberName != String.Empty) strWhere += string.Format(" and MemberName like '%{0:S}%'", MemberName);

            strSql = "select count(*) from BSC_Member where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 查询商品列表。
        /// </summary>
        /// <param name="MemberID">商品类型ID</param>
        /// <param name="MemberNo">会员编号</param>
        /// <param name="MemberName">会员名</param>
        /// <param name="StartIndex">开始序号</param>
        /// <param name="MaxCount">最大返回数量</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryMemberList(int MemberID, string MemberNo, string MemberName, int StartIndex, int MaxCount, out DataTable ReturnTable)
        {
            string strSql, strWhere;
            strWhere = "1=1";
            if (MemberID > 0) strWhere += string.Format(" and a.MemberID={0:D}", MemberID);
            if (MemberNo != String.Empty) strWhere += string.Format(" and a.MemberNo like '%{0:S}%'", MemberNo);
            if (MemberName != String.Empty) strWhere += string.Format(" and a.MemberName like '%{0:S}%'", MemberName);

            strSql = "select a.* from BSC_Member a " +
                    " where {0:S} order by a.MemberID desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out ReturnTable);
        }
        #endregion

        #region 会员地址接口
        /// <summary>
        /// 获取会员地址列表。
        /// </summary>
        /// <param name="MemberID">会员ID</param>
        /// <param name="ReturnTable">返回结果数据表</param>
        /// <returns></returns>
        public static XReturn QueryMemberAddressList(int MemberID, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = " where 1=1";
            if (MemberID > 0) strWhere += string.Format(" and MemberID={0:D}", MemberID);

            strSql = "select * from BSC_MemberAddress";
            strSql += strWhere;

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
        }

        /// <summary>
        /// 保存会员地址。
        /// </summary>
        /// <param name="MemberAddress">会员地址对象</param>
        /// <returns></returns>
        public static XReturn SaveMemberAddress(BSC_MemberAddress MemberAddress)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            if (MemberAddress.MemberAddressID > 0)
            {
                xSubReturn = MemberAddress.x.UpdateByIdentity();
            }
            else
            {
                xSubReturn = MemberAddress.x.Insert();
            }

            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "保存会员收货信息失败");
            }

            return xReturn.ReturnSuccess();
        }
        
        /// <summary>
        /// 删除会员地址。
        /// </summary>
        /// <param name="MemberAddressID">会员地址ID</param>
        /// <returns></returns>
        public static XReturn DeleteMemberAddress(int MemberAddressID)
        {
            BSC_MemberAddress MemberAddress = new BSC_MemberAddress();
            MemberAddress.MemberAddressID = MemberAddressID;
            return MemberAddress.x.DeleteByIdentity();
        }

        /// <summary>
        /// 查询默认会员地址。
        /// </summary>
        /// <param name="MemberAddress">会员地址</param>
        /// <returns></returns>
        public static XReturn QueryDefaultMemberAddress(BSC_MemberAddress MemberAddress)
        {
            return MemberAddress.x.SelectByIdentity();
        }

        /// <summary>
        /// 保存默认会员地址。
        /// </summary>
        /// <param name="MemberID">会员ID</param>
        /// <param name="MemberAddressID">会员地址ID</param>
        /// <returns></returns>
        public static XReturn SaveDefaultMemberAddress(int MemberID, int MemberAddressID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            string strSql;

            strSql = string.Format("update BSC_Member set DefaultMemberAddressID={1:D} where MemberID={0:D}", MemberID, MemberAddressID);
            xSubReturn = DatabaseFactory.GetCurrent().ExecuteNonQuery(strSql);
            if (xSubReturn.IsUnSuccess() || xSubReturn.RowCount != 1)
            {
                return xReturn.ReturnError(xSubReturn, "更新会员表默认地址ID失败");
            }

            return xReturn.ReturnSuccess();
        }
        #endregion

        #region 会员发票接口
        /// <summary>
        /// 获取会员发票列表。
        /// </summary>
        /// <param name="MemberID">会员ID</param>
        /// <param name="ReturnTable">返回结果数据表</param>
        /// <returns></returns>
        public static XReturn QueryMemberInvoiceList(int MemberID, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = " where 1=1";
            if (MemberID > 0) strWhere += string.Format(" and MemberID={0:D}", MemberID);

            strSql = "select * from BSC_MemberInvoice";
            strSql += strWhere;

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
        }

        /// <summary>
        /// 保存会员发票。
        /// </summary>
        /// <param name="MemberInvoice">会员发票对象</param>
        /// <returns></returns>
        public static XReturn SaveMemberInvoice(BSC_MemberInvoice MemberInvoice)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            if (MemberInvoice.MemberInvoiceID > 0)
            {
                xSubReturn = MemberInvoice.x.UpdateByIdentity();
            }
            else
            {
                xSubReturn = MemberInvoice.x.Insert();
            }

            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "保存会员发票失败");
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 删除会员发票。
        /// </summary>
        /// <param name="MemberInvoiceID">会员发票ID</param>
        /// <returns></returns>
        public static XReturn DeleteMemberInvoice(int MemberInvoiceID)
        {
            BSC_MemberInvoice MemberInvoice = new BSC_MemberInvoice();
            MemberInvoice.MemberInvoiceID = MemberInvoiceID;
            return MemberInvoice.x.DeleteByIdentity();
        }
     
        /// <summary>
        /// 查询默认会员发票。
        /// </summary>
        /// <param name="MemberInvoice">会员发票</param>
        /// <returns></returns>
        public static XReturn QueryDefaultMemberInvoice(BSC_MemberInvoice MemberInvoice)
        {
            return MemberInvoice.x.SelectByIdentity();
        }

        /// <summary>
        /// 保存默认会员发票。
        /// </summary>
        /// <param name="MemberID">会员ID</param>
        /// <param name="MemberInvoiceID">会员发票ID</param>
        /// <returns></returns>
        public static XReturn SaveDefaultMemberInvoice(int MemberID, int MemberInvoiceID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            string strSql;

            strSql = string.Format("update BSC_Member set DefaultMemberInvoiceID={1:D} where MemberID={0:D}", MemberID, MemberInvoiceID);
            xSubReturn = DatabaseFactory.GetCurrent().ExecuteNonQuery(strSql);
            if (xSubReturn.IsUnSuccess() || xSubReturn.RowCount != 1)
            {
                return xReturn.ReturnError(xSubReturn, "更新会员表默认发票ID失败");
            }

            return xReturn.ReturnSuccess();
        }
        #endregion

        #region 会员评论接口
        /// <summary>
        /// 查询会员评论列表。
        /// </summary>
        /// <param name="MemberID">会员ID</param>
        /// <param name="ProductID">商品ID</param>
        /// <param name="StartIndex">开始序号</param>
        /// <param name="MaxCount">最大返回数量</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryMemberReviewList(int MemberID, int ProductID, int StartIndex, int MaxCount, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = "where 1=1";
            if (ProductID > 0) strWhere += string.Format(" and a.ProductID={0:D}", ProductID);

            strSql = "select a.*,b.MemberName,b.MemberCategoryID,c.OrderDate from BSC_MemberReview a " +
                    " left join BSC_Member b on b.MemberID=a.MemberID " +
                    " left join SLS_Order c on c.OrderID=a.OrderID " +
                    " {0:S} order by a.CreateTime desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out ReturnTable);
        }

        /// <summary>
        /// 查询会员评论数目。
        /// </summary>
        /// <param name="MemberID">会员ID</param>
        /// <param name="ProductID">商品ID</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryMemberReviewCount(int MemberID, int ProductID)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (ProductID > 0) strWhere += string.Format(" and a.ProductID={0:D}", ProductID);

            strSql = "select count(*) from BSC_MemberReview a where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 提交会员评论。
        /// </summary>
        /// <param name="MemberReview">会员评论</param>
        /// <returns></returns>
        public static XReturn SubmitMemberReview(BSC_MemberReview MemberReview)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            SLS_Order Order = new SLS_Order();
            Order.OrderID = MemberReview.OrderID;
            xSubReturn = Order.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "查询订单失败");
                return xReturn;
            }

            if (Order.MemberReviewID > 0)
            {
                xReturn.SetError("该订单已经发表评论");
                return xReturn;
            }

            TransactionWork transactionWork = DatabaseFactory.GetCurrent().GetTransaction();

            transactionWork.BeginTransition();

            xSubReturn = MemberReview.x.Insert(transactionWork);
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "插入会员评论失败");

                transactionWork.Rollback();
                return xReturn;
            }

            Order.MemberReviewID = MemberReview.MemberReviewID;
            xSubReturn = Order.x.UpdateByIdentity(transactionWork);
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "更新订单失败");

                transactionWork.Rollback();
                return xReturn;
            }

            transactionWork.Commit();

            return xReturn.ReturnSuccess();
        }
        #endregion

        #region 会员优惠劵接口
        /// <summary>
        /// 查询会员优惠劵数量。
        /// </summary>
        /// <param name="MemberID">会员ID</param>
        /// <param name="MemberNo">会员编号</param>
        /// <param name="MemberName">会员名</param>
        /// <param name="ToOrderID">用于订单ID</param>
        /// <param name="MemberCouponNo">会员优惠劵编号</param>
        /// <param name="ParValue">面值</param>
        /// <param name="Status">状态</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryMemberCouponCount(int MemberID, string MemberNo, string MemberName, int ToOrderID, string MemberCouponNo, double ParValue, int Status)
        {
            string strSql, strWhere;

            strWhere = " 1=1 ";
            if (MemberID > 0) strWhere += string.Format(" and a.MemberID={0:D}", MemberID);
            if (MemberNo != String.Empty) strWhere += string.Format(" and b.MemberNo='{0:S}'", MemberNo);
            if (MemberName != String.Empty) strWhere += string.Format(" and b.MemberName='{0:S}'", MemberName);
            if (ToOrderID > 0) strWhere += string.Format(" and a.ToOrderID={0:D}", ToOrderID);
            if (MemberCouponNo != String.Empty) strWhere += string.Format(" and a.MemberCouponNo='{0:S}'", MemberCouponNo);
            if (ParValue > 0) strWhere += string.Format(" and a.ParValue={0:D}", ParValue);
            if (Status > 0) strWhere += string.Format(" and a.Status={0:D}", Status);

            strSql = "select count(*) from BSC_MemberCoupon a "+
                    " left join BSC_Member b on a.MemberID=b.MemberID" +
                    " where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 查询会员优惠劵列表。
        /// </summary>
        /// <param name="MemberID">会员ID</param>
        /// <param name="MemberNo">会员编号</param>
        /// <param name="MemberName">会员名</param>
        /// <param name="ToOrderID">用于订单ID</param>
        /// <param name="MemberCouponNo">会员优惠劵编号</param>
        /// <param name="ParValue">面值</param>
        /// <param name="Status">状态</param>
        /// <param name="StartIndex">开始序号</param>
        /// <param name="MaxCount">最大返回数量</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryMemberCouponList(int MemberID, string MemberNo, string MemberName, int ToOrderID, string MemberCouponNo, double ParValue, int Status, int StartIndex, int MaxCount, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = " 1=1 ";
            if (MemberID > 0) strWhere += string.Format(" and a.MemberID={0:D}", MemberID);
            if (MemberNo != String.Empty) strWhere += string.Format(" and b.MemberNo='{0:S}'", MemberNo);
            if (MemberName != String.Empty) strWhere += string.Format(" and b.MemberName='{0:S}'", MemberName);
            if (ToOrderID > 0) strWhere += string.Format(" and a.ToOrderID={0:D}", ToOrderID);
            if (MemberCouponNo != String.Empty) strWhere += string.Format(" and a.MemberCouponNo='{0:S}'", MemberCouponNo);
            if (ParValue > 0) strWhere += string.Format(" and a.ParValue={0:D}", ParValue);
            if (Status > 0) strWhere += string.Format(" and a.Status={0:D}", Status);

            strSql = "select a.*,b.MemberNo,b.MemberName,b.RealName,c.ProductScopeName from BSC_MemberCoupon a " +
                    " left join BSC_Member b on a.MemberID=b.MemberID" +
                    " left join BSC_ProductScope c on a.ProductScopeID=c.ProductScopeID" +
                    " where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out ReturnTable);
        }

        /// <summary>
        /// 保存会员优惠劵。
        /// </summary>
        /// <param name="MemberCoupon">会员优惠劵对象</param>
        /// <returns></returns>
        public static XReturn SaveMemberCoupon(BSC_MemberCoupon MemberCoupon)
        {
            if (MemberCoupon.MemberCouponID == 0)
            {
                return MemberCoupon.x.Insert();
            }
            else
            {
                return MemberCoupon.x.UpdateByIdentity();
            }
        }

        /// <summary>
        /// 指派会员优惠劵。
        /// </summary>
        /// <param name="transactionWork">事务对象</param>
        /// <param name="MemberCouponID">会员优惠劵ID</param>
        /// <param name="MemberCouponNo">会员优惠劵编号</param>
        /// <param name="MemberID">会员ID</param>
        /// <param name="ToOrderID">用于订单ID</param>
        /// <returns></returns>
        public static XReturn AssignMemberCoupon(TransactionWork transactionWork, int MemberCouponID, string MemberCouponNo, int MemberID, int ToOrderID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            BSC_MemberCoupon MemberCoupon = new BSC_MemberCoupon();
            if (MemberCouponID > 0)
            {
                MemberCoupon.MemberCouponID = MemberCouponID;
                xSubReturn = MemberCoupon.x.SelectByIdentity(transactionWork);
            }
            else
            {
                MemberCoupon.MemberCouponNo = MemberCouponNo;
                xSubReturn = MemberCoupon.x.Select(transactionWork);
            }
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询会员优惠劵不存在");
            }

            if (MemberCoupon.MemberID != MemberID)
            {
                return xReturn.ReturnError("优惠劵不属于该会员");
            }

            if (MemberCoupon.Status != (int)EMemberCouponStatus.Active)
            {
                return xReturn.ReturnError("优惠劵不是激活状态");
            }

            SLS_Order Order = new SLS_Order();
            Order.OrderID = ToOrderID;
            xSubReturn = Order.x.SelectByIdentity(transactionWork);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询订单不存在");
            }

            MemberCoupon.ToOrderID = ToOrderID;
            MemberCoupon.Status = (int)EMemberCouponStatus.Assign;
            xSubReturn = MemberCoupon.x.UpdateByIdentity(transactionWork);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "更好会员优惠劵失败");
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 完成会员优惠劵集合。
        /// </summary>
        /// <param name="transacionWork">事务对象</param>
        /// <param name="MemberID">会员ID</param>
        /// <param name="ToOrderID">用于订单ID</param>
        /// <returns></returns>
        public static XReturn FinishMemberCoupons(TransactionWork transacionWork, int MemberID, int ToOrderID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            
            EntityQueryer qryMemberCoupon = new EntityQueryer("BSC_MemberCoupon");
            qryMemberCoupon.AddFilter("MemberID", MemberID);
            qryMemberCoupon.AddFilter("ToOrderID", ToOrderID);
            xSubReturn = qryMemberCoupon.Query(transacionWork);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询会员优惠劵列表失败");
            }
            if (qryMemberCoupon.EntityCount == 0)
            {
                return xReturn.ReturnError(xSubReturn, "查询会员优惠劵无记录");
            }

            foreach (BSC_MemberCoupon MemberCoupon in qryMemberCoupon.EntityList)
            {
                if (MemberCoupon.Status != (int)EMemberCouponStatus.Assign)
                {
                    return xReturn.ReturnError(string.Format("会员优惠劵[{0:S}]状态不是指派状态", MemberCoupon.MemberCouponNo));
                }

                MemberCoupon.Status = (int)EMemberCouponStatus.Finish;
                xSubReturn = MemberCoupon.x.UpdateByIdentity(transacionWork);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, "更新会员优惠劵状态失败");
                }
            }

            return xReturn.ReturnSuccess();
        }
        #endregion

        #region 会员咨询接口
        /// <summary>
        /// 查询会员咨询列表。
        /// </summary>
        /// <param name="MemberID">会员ID</param>
        /// <param name="MemberName">会员名</param>
        /// <param name="RealName">会员姓名</param>
        /// <param name="ProductID">商品ID</param>
        /// <param name="ProductNo">商品编号</param>
        /// <param name="ProductName">商品名称</param>
        /// <param name="Status">状态</param>
        /// <param name="StartIndex">开始序号</param>
        /// <param name="MaxCount">最大返回数量</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryMemberConsultationList(int MemberID, string MemberName, string RealName, int ProductID, string ProductNo, string ProductName, int Status, int StartIndex, int MaxCount, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = " 1=1 ";
            if (MemberID > 0) strWhere += string.Format(" and a.MemberID={0:D}", MemberID);
            if (MemberName != String.Empty) strWhere += string.Format(" and b.MemberName='{0:S}'", MemberName);
            if (RealName != String.Empty) strWhere += string.Format(" and b.RealName='{0:S}'", RealName);
            if (ProductID > 0) strWhere += string.Format(" and a.ProductID={0:D}", ProductID);
            if (ProductNo != String.Empty) strWhere += string.Format(" and c.ProductNo='{0:S}'", ProductNo);
            if (ProductName != String.Empty) strWhere += string.Format(" and c.ProductName='{0:S}'", ProductName);
            if (Status > 0) strWhere += string.Format(" and a.Status={0:D}", Status);

            strSql = "select a.*,b.MemberName,b.RealName,b.MemberCategoryID,c.ProductNo,c.ProductName from BSC_MemberConsultation a " +
                    " left join BSC_Member b on b.MemberID=a.MemberID " +
                    " left join BSC_Product c on c.ProductID=a.ProductID " +
                    " where {0:S} order by a.QuestionTime desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out ReturnTable);
        }

        /// <summary>
        /// 查询会员咨询数目。
        /// </summary>
        /// <param name="MemberID">会员ID</param>
        /// <param name="MemberName">会员名</param>
        /// <param name="RealName">会员姓名</param>
        /// <param name="ProductID">商品ID</param>
        /// <param name="ProductNo">商品编号</param>
        /// <param name="ProductName">商品名称</param>
        /// <param name="Status">状态</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryMemberConsultationCount(int MemberID, string MemberName, string RealName, int ProductID, string ProductNo, string ProductName, int Status)
        {
            string strSql, strWhere;

            strWhere = " 1=1 ";
            if (MemberID > 0) strWhere += string.Format(" and a.MemberID={0:D}", MemberID);
            if (MemberName != String.Empty) strWhere += string.Format(" and b.MemberName='{0:S}'", MemberName);
            if (RealName != String.Empty) strWhere += string.Format(" and b.RealName='{0:S}'", RealName);
            if (ProductID > 0) strWhere += string.Format(" and a.ProductID={0:D}", ProductID);
            if (ProductNo != String.Empty) strWhere += string.Format(" and c.ProductNo='{0:S}'", ProductNo);
            if (ProductName != String.Empty) strWhere += string.Format(" and c.ProductName='{0:S}'", ProductName);
            if (Status > 0) strWhere += string.Format(" and a.Status={0:D}", Status);

            strSql = "select count(*) from BSC_MemberConsultation a" +
                    " left join BSC_Member b on b.MemberID=a.MemberID " +
                    " left join BSC_Product c on c.ProductID=a.ProductID " +
                    " where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 提交会员咨询。
        /// </summary>
        /// <param name="MemberConsultation">会员咨询</param>
        /// <returns></returns>
        public static XReturn SubmitMemberConsultation(BSC_MemberConsultation MemberConsultation)
        {
            return MemberConsultation.x.Insert();
        }

        /// <summary>
        /// 答复会员咨询
        /// </summary>
        /// <param name="MemberConsultationID">会员咨询ID</param>
        /// <param name="Answer">答复内容</param>
        /// <returns></returns>
        public static XReturn AnswerMemberConsultation(int MemberConsultationID, string Answer)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            BSC_MemberConsultation MemberConsultation = new BSC_MemberConsultation();
            MemberConsultation.MemberConsultationID = MemberConsultationID;
            xSubReturn = MemberConsultation.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "查询会员咨询失败");
                return xReturn;
            }

            if (MemberConsultation.Status != (int)EMemberConsultationStatus.Question)
            {
                xReturn.SetError(string.Format("会员咨询状态[{0:S}]不能答复", MemberConsultation.Status));
                return xReturn;
            }

            MemberConsultation.Answer = Answer;
            MemberConsultation.AnswerTime = DateTime.Now;
            MemberConsultation.Status = (int)EMemberConsultationStatus.Answer;

            xSubReturn = MemberConsultation.x.UpdateByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "更新会员咨询失败");
                return xReturn;
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 删除会员咨询
        /// </summary>
        /// <param name="MemberConsultationID">会员咨询ID</param>
        /// <returns></returns>
        public static XReturn DeleteMemberConsultation(int MemberConsultationID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            BSC_MemberConsultation MemberConsultation = new BSC_MemberConsultation();
            MemberConsultation.MemberConsultationID = MemberConsultationID;
            xSubReturn = MemberConsultation.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "查询会员咨询失败");
                return xReturn;
            }

            MemberConsultation.Status = (int)EMemberConsultationStatus.Delete;

            xSubReturn = MemberConsultation.x.UpdateByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn, "更新会员咨询失败");
                return xReturn;
            }

            return xReturn.ReturnSuccess();
        }
        #endregion
    }
}
