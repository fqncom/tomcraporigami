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
    public class StatImpl
    {
        #region 统计主接口
        /// <summary>
        /// 提交统计商品每日汇总
        /// </summary>
        /// <param name="SummaryDate">汇总日期</param>
        /// <returns></returns>
        public static XReturn SubmitStatProductDaySummary(string SummaryDate)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            string strSql;

            TransactionWork transactionWork = DatabaseFactory.GetCurrent().GetTransaction();
            transactionWork.BeginTransition();

            // 删除商品每日汇总数据
            strSql = "delete from STAT_ProductDaySummary where SummaryDate='{0:S}'";
            strSql = string.Format(strSql, SummaryDate);
            xSubReturn = transactionWork.ExecuteNonQuery(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                transactionWork.Rollback();
                return xReturn.ReturnError(xSubReturn, string.Format("删除{0:S}日商品汇总数据失败", SummaryDate));
            }

            // 插入商品每日汇总数据
            strSql = "insert into STAT_ProductDaySummary (SummaryDate,ProductID,HitCount,SalesCount,SalesQuantity,SalesAmount) " +
                    " (" +
                    "   select '{0:S}' as SummaryDate,isnull(e.ProductID,f.ProductID) as ProductID,isnull(e.HitCount,0) as HitCount,isnull(f.SalesCount,0) as SalesCount,isnull(f.SalesQuantity,0) as SalesQuantity,isnull(f.SalesAmount,0) as SalesAmount" +
                    "   from " +
                    "       (select a.ProductID,count(*) as HitCount from STAT_PageHit a where a.PageCode='product_aspx' and a.HitDate='{0:S}' group by a.ProductID) e" +
                    "   full join " +
                    "	    (select c.ProductID,count(*) SalesCount,sum(c.Quantity) as SalesQuantity,sum(c.Amount) as SalesAmount from SLS_OrderItem c left join SLS_Order d on c.OrderID=d.OrderID where d.OrderDate='{0:S}' group by c.ProductID) f" +
                    "   on e.ProductID=f.ProductID" +
                    " )";
            strSql = string.Format(strSql, SummaryDate);
            xSubReturn = transactionWork.ExecuteNonQuery(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                transactionWork.Rollback();
                return xReturn.ReturnError(xSubReturn, string.Format("插入{0:S}日商品汇总数据失败", SummaryDate));
            }

            transactionWork.Commit();

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 提交统计商品排行榜
        /// </summary>
        /// <returns></returns>
        public static XReturn SubmitStatProductRankingList()
        {
            return SubmitStatProductRankingList((int)EStatPeriod.All, "00000000", "00000000");
        }

        /// <summary>
        /// 提交统计商品排行榜
        /// </summary>
        /// <param name="Period">周期</param>
        /// <param name="FromDate">开始日期</param>
        /// <param name="ToDate">结束日期</param>
        /// <returns></returns>
        public static XReturn SubmitStatProductRankingList(int Period, string FromDate, string ToDate)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            BSC_ProductCategoryExt cpxProductCategory = ProductCategoryCache.Current.GetProductCategoryExt(0);
            foreach (BSC_ProductCategoryExt cpxChildProductCategory in cpxProductCategory.SubProductCategoryList)
            {
                xSubReturn = SubmitStatProductRankingList_IterateProductCategory(Period, FromDate, ToDate, 0);
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 提交统计商品排行榜之迭代商品类型
        /// </summary>
        /// <param name="Period">周期</param>
        /// <param name="FromDate">开始日期</param>
        /// <param name="ToDate">结束日期</param>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <returns></returns>
        private static XReturn SubmitStatProductRankingList_IterateProductCategory(int Period, string FromDate, string ToDate, int ProductCategoryID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            string strSql;
            DataTable dt;
            int ItemIndex;

            BSC_ProductCategoryExt cpxProductCategory = ProductCategoryCache.Current.GetProductCategoryExt(ProductCategoryID);

            if (ProductCategoryID > 0)
            {
                TransactionWork transactionWork = DatabaseFactory.GetCurrent().GetTransaction();
                transactionWork.BeginTransition();

                // 保存或者新增商品排行榜记录
                STAT_ProductRankingList ProductRankingList = new STAT_ProductRankingList();
                ProductRankingList.Category = (int)EStatCategory.Sales;
                ProductRankingList.Period = Period;
                ProductRankingList.FromDate = FromDate;
                ProductRankingList.ToDate = ToDate;
                ProductRankingList.ProductCategoryID = ProductCategoryID;
                xSubReturn = ProductRankingList.x.Select(transactionWork);
                if (xSubReturn.IsUnSuccess())
                {
                    xSubReturn = ProductRankingList.x.Insert(transactionWork);
                    if (xSubReturn.IsUnSuccess())
                    {
                        transactionWork.Rollback();
                        return xReturn.ReturnError(xSubReturn, "插入商品排行榜记录失败");
                    }
                }

                // 从商品每日汇总表查询排行榜明细
                strSql = "select top 10 d.* from" +
                        " (" +
                        "   select a.ProductID,sum(a.SalesCount) as TotalSalesCount from STAT_ProductDaySummary a " +
                        "   left join BSC_Product b on a.ProductID=b.ProductID" +
                        "   left join BSC_ProductCategory c on b.ProductCategoryID=c.ProductCategoryID" +
                        "   where a.SalesCount>0 and " + ProductCategoryCache.Current.GetFilterSql(ProductCategoryID, "b.ProductCategoryID", "c.FullPath") +
                        "   group by a.ProductID" +
                        " ) d" +
                        " order by d.TotalSalesCount desc";
                xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out dt);
                if (xSubReturn.IsUnSuccess())
                {
                    transactionWork.Rollback();

                    return xReturn.ReturnError(xSubReturn, "查询排行榜明细失败");
                }

                // 插入排行榜明细记录
                for (ItemIndex = 0; ItemIndex < dt.Rows.Count; ItemIndex++)
                {
                    STAT_ProductRankingListItem ProductRankingListItem = new STAT_ProductRankingListItem();
                    ProductRankingListItem.ProductID = (int)dt.Rows[ItemIndex]["ProductID"];
                    ProductRankingListItem.TotalQuantiy = Convert.ToDouble(dt.Rows[ItemIndex]["TotalSalesCount"]);
                    ProductRankingListItem.ProductRankingListID = ProductRankingList.ProductRankingListID;
                    ProductRankingListItem.OrderNo = ItemIndex + 1;

                    xSubReturn = ProductRankingListItem.x.Insert(transactionWork);
                    if (xSubReturn.IsUnSuccess())
                    {
                        transactionWork.Rollback();

                        return xReturn.ReturnError(xSubReturn, "插入排行榜明细失败");
                    }
                }

                int ItemCount2 = 10 - ItemIndex;

                // 如果不足10条，则从商品列表选取推荐的商品
                if (ItemCount2 > 0)
                {
                    string strProductCategorySql = ProductCategoryCache.Current.GetFilterSql(ProductCategoryID, "a.ProductCategoryID", "b.FullPath");
                    strSql = "select top {0:D} a.ProductID from BSC_Product a " +
                            " left join BSC_ProductCategory b on a.ProductCategoryID=b.ProductCategoryID " +
                            " where {2:S}" + " and a.ProductID not in (select z.ProductID from STAT_ProductRankingListItem z where ProductRankingListID={1:D})" +
                            " order by a.WeightValue desc";

                    strSql = string.Format(strSql, ItemCount2, ProductRankingList.ProductRankingListID, strProductCategorySql);
                    xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out dt);
                    if (xSubReturn.IsUnSuccess())
                    {
                        transactionWork.Rollback();

                        return xReturn.ReturnError(xSubReturn, "查询排行榜明细2失败");
                    }

                    // 插入排行榜明细记录
                    for (; ItemIndex < dt.Rows.Count; ItemIndex++)
                    {
                        STAT_ProductRankingListItem ProductRankingListItem = new STAT_ProductRankingListItem();
                        ProductRankingListItem.ProductID = (int)dt.Rows[ItemIndex]["ProductID"];
                        ProductRankingListItem.ProductRankingListID = ProductRankingList.ProductRankingListID;
                        ProductRankingListItem.OrderNo = ItemIndex + 1;

                        xSubReturn = ProductRankingListItem.x.Insert(transactionWork);
                        if (xSubReturn.IsUnSuccess())
                        {
                            transactionWork.Rollback();

                            return xReturn.ReturnError(xSubReturn, "插入排行榜明细失败");
                        }
                    }
                }

                transactionWork.Commit();
            }

            foreach (BSC_ProductCategoryExt cpxChildProductCategory in cpxProductCategory.SubProductCategoryList)
            {
                xSubReturn = SubmitStatProductRankingList_IterateProductCategory(Period, FromDate, ToDate, cpxChildProductCategory.ProductCategory.ProductCategoryID);
            }

            return xReturn.ReturnSuccess();
        }
        #endregion

        #region 页面点击接口
        /// <summary>
        /// 保存页面点击
        /// </summary>
        /// <param name="PageHit">页面点击</param>
        /// <returns></returns>
        public static XReturn SavePageHit(STAT_PageHit PageHit)
        {
            return PageHit.x.Insert();
        }
        #endregion

        #region 排行榜接口
        /// <summary>
        /// 查询排行榜商品列表。
        /// </summary>
        /// <param name="PeriodNo">期号</param>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <param name="ProductBrandID">商品品牌ID</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryProductRankingList(string PeriodNo, int ProductCategoryID, int ProductBrandID, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = " c.ProductID is not null ";
            if (ProductCategoryID > 0) strWhere += string.Format(" and b.ProductCategoryID={0:D}", ProductCategoryID);
            if (ProductBrandID > 0) strWhere += string.Format(" and b.ProductBrandID={0:D}", ProductBrandID);
            if (PeriodNo != String.Empty) strWhere += string.Format(" and b.Period='{0:S}'", PeriodNo);

            strSql = "select a.*,c.ProductNo,c.ProductName,c.MarketPrice,c.SalesPrice from STAT_ProductRankingListItem a" +
                " left join STAT_ProductRankingList b on b.ProductRankingListID=a.ProductRankingListID" +
                " left join BSC_Product c on c.ProductID=a.ProductID" +
                " where {0:S}" +
                " order by a.OrderNo";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
        }
        #endregion
    }
}
