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
    public class ConfigImpl
    {
        #region 限购商品处理
        /// <summary>
        /// 查询限购商品。
        /// </summary>
        /// <param name="ProductNo">商品编号</param>
        /// <param name="ProductName">商品名称</param>
        /// <param name="ReturnTable">返回查询结果</param>
        /// <returns></returns>
        public static XReturn QueryLimitSalesProductList(string ProductNo, string ProductName, out DataTable ReturnTable)
        {
            ReturnTable = null;
            string strWhere;

            strWhere = "1=1";
            if (ProductNo != String.Empty) strWhere += string.Format(" and b.ProductNo='{0:S}'", ProductNo);
            if (ProductName != String.Empty) strWhere += string.Format(" and b.ProductName like '%{0:S}%'", ProductName);

            string strSql =
                "select a.*,b.ProductNo,b.ProductName,b.ProductUnit,c.ProductSpecID,c.ProductSpec " +
                " from BSC_LimitSalesProduct a" +
                " left join BSC_Product b on a.ProductID=b.ProductID " +
                " left join BSC_ProductSpec c on b.DefaultProductSpecID=c.ProductSpecID " +
                " where {0:S}" +
                " order by a.RecommendOrder desc,a.WeightValue desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
        }

        /// <summary>
        /// 保存限购商品。
        /// </summary>
        /// <param name="LimitSalesProduct">限购商品实例</param>
        /// <returns>返回执行结果</returns>
        public static XReturn SaveLimitSalesProduct(BSC_LimitSalesProduct LimitSalesProduct)
        {
            if (LimitSalesProduct.LimitSalesProductID > 0)
            {
                return LimitSalesProduct.x.UpdateByIdentity();
            }
            else
            {
                return LimitSalesProduct.x.Insert();
            }
        }

        /// <summary>
        /// 删除限购商品。
        /// </summary>
        /// <param name="LimitSalesProductID">限购商品ID</param>
        /// <returns></returns>
        public static XReturn DeleteLimitSalesProduct(int LimitSalesProductID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            BSC_LimitSalesProduct LimitSalesProduct = new BSC_LimitSalesProduct();

            LimitSalesProduct.LimitSalesProductID = LimitSalesProductID;
            xSubReturn = LimitSalesProduct.x.DeleteByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "执行删除语句失败");
            }

            return xReturn.ReturnSuccess();
        }
        #endregion

        #region 兑换商品处理
        /// <summary>
        /// 查询兑换商品。
        /// </summary>
        /// <param name="ProductNo">商品编号</param>
        /// <param name="ProductName">商品名称</param>
        /// <param name="ReturnTable">返回查询结果</param>
        /// <returns></returns>
        public static XReturn QueryExchangeProductList(string ProductNo, string ProductName, out DataTable ReturnTable)
        {
            ReturnTable = null;
            string strWhere;

            strWhere = "1=1";
            if (ProductNo != String.Empty) strWhere += string.Format(" and b.ProductNo='{0:S}'", ProductNo);
            if (ProductName != String.Empty) strWhere += string.Format(" and b.ProductName like '%{0:S}%'", ProductName);

            string strSql =
                "select a.*,b.ProductNo,b.ProductName,b.ProductUnit,b.SalesPrice,c.ProductSpecID,c.ProductSpec " +
                " from BSC_ExchangeProduct a" +
                " left join BSC_Product b on a.ProductID=b.ProductID " +
                " left join BSC_ProductSpec c on b.DefaultProductSpecID=c.ProductSpecID " +
                " where {0:S}" +
                " order by b.ProductNo";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
        }

        /// <summary>
        /// 保存兑换商品。
        /// </summary>
        /// <param name="ExchangeProduct">兑换商品实例</param>
        /// <returns>返回执行结果</returns>
        public static XReturn SaveExchangeProduct(BSC_ExchangeProduct ExchangeProduct)
        {
            if (ExchangeProduct.x.Exist())
            {
                return XReturn.ReturnNewError("兑换商品已经存在");
            }

            return ExchangeProduct.x.Insert();
        }

        /// <summary>
        /// 删除兑换商品。
        /// </summary>
        /// <param name="ProductID">兑换商品ID</param>
        /// <returns></returns>
        public static XReturn DeleteExchangeProduct(int ProductID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            BSC_ExchangeProduct ExchangeProduct = new BSC_ExchangeProduct();

            ExchangeProduct.ProductID = ProductID;
            xSubReturn = ExchangeProduct.x.Delete();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn);
                return xReturn;
            }

            xReturn.ReturnCode = "0000";
            return xReturn;
        }
        #endregion

        #region 积分配置接口
        /// <summary>
        /// 查询积分配置。
        /// </summary>
        /// <returns></returns>
        public static XReturn QueryPointConfig()
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            DbDataReader dr;

            string strSql = "select PointMode,PointValue,MinAmountToPoint from BSC_PointConfig";
            xSubReturn = DatabaseFactory.GetCurrent().GetReader(strSql, out dr);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询积分配置失败");
            }
            if (!dr.Read())
            {
                dr.Close();
                return xReturn.ReturnError(xSubReturn, "查询积分配置失败");
            }

            BSC_PointConfig PointConfig = new BSC_PointConfig();
            PointConfig.PointMode = dr.GetInt32(0);
            PointConfig.PointValue = dr.GetInt32(1);
            PointConfig.MinAmountToPoint = dr.GetInt32(2);
            xReturn.ReturnValue = PointConfig;

            dr.Close();

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询商品积分配置列表。
        /// </summary>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryProductCategoryPointConfigList(out DataTable ReturnTable)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            ReturnTable = null;

            string strSql = "select a.*,b.ProductCategoryName from BSC_ProductCategoryPointConfig a left join BSC_ProductCategory b on a.ProductCategoryID=b.ProductCategoryID";
            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品类型积分配置列表失败");
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询商品积分配置列表。
        /// </summary>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryProductPointConfigList(out DataTable ReturnTable)
        {
            string strSql;
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            ReturnTable = null;

            strSql = "select a.*,b.ProductNo,b.ProductName,b.ProductUnit,c.ProductSpec from BSC_ProductPointConfig a " +
                    " left join BSC_Product b on a.ProductID=b.ProductID" +
                    " left join BSC_ProductSpec c on c.ProductSpecID=b.DefaultProductSpecID";

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品积分配置列表失败");
            }

            return xReturn.ReturnSuccess();
        }
        #endregion

        #region 优惠劵接口
        /// <summary>
        /// 查询优惠劵发放金额区间配置列表。
        /// </summary>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryCouponGrantAmountConfigList(out DataTable ReturnTable)
        {
            string strSql;
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            ReturnTable = null;

            strSql = "select * from BSC_CouponGrantAmountConfig order by BeginAmount,EndAmount";

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品优惠劵发放金额区间配置列表失败");
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 保存优惠劵发放金额区间配置。
        /// </summary>
        /// <param name="CouponGrantAmountConfig">优惠劵发放金额区间配置对象</param>
        /// <returns></returns>
        public static XReturn SaveCouponGrantAmountConfig(BSC_CouponGrantAmountConfig CouponGrantAmountConfig)
        {
            if (CouponGrantAmountConfig.CouponGrantAmountConfigID == 0)
            {
                return CouponGrantAmountConfig.x.Insert();
            }
            else
            {
                return CouponGrantAmountConfig.x.UpdateByIdentity();
            }
        }
        #endregion

        #region 商品折扣接口
        /// <summary>
        /// 查询商品折扣。
        /// </summary>
        /// <param name="PriceConcessionGroupID">商品折扣方案ID</param>
        /// <param name="ReturnTable">返回查询结果</param>
        /// <returns></returns>
        public static XReturn QueryProductAgioList(int PriceConcessionGroupID, out DataTable ReturnTable)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            ReturnTable = null;
            string strSql = "select a.*,b.ProductName,c.ProductCategoryName,d.ProductBrandName,e.OperatorName from BSC_ProductAgio a" +
                            " left join BSC_Product b on a.ProductID=b.ProductID" +
                            " left join BSC_ProductCategory c on a.ProductCategoryID=c.ProductCategoryID" +
                            " left join BSC_ProductBrand d on a.ProductBrandID=d.ProductBrandID" +
                            " left join LPD_Operator e on a.OperatorID=e.OperatorID where 1=1";

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品折扣失败");
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 保存商品折扣。
        /// </summary>
        /// <param name="ProductAgio">商品折扣</param>
        /// <returns>返回执行结果</returns>
        public static XReturn SaveProductAgio(BSC_ProductAgio ProductAgio)
        {
            if (ProductAgio.ProductAgioID == 0)
            {
                return ProductAgio.x.Insert();
            }
            else
            {
                return ProductAgio.x.UpdateByIdentity();
            }
        }

        /// <summary>
        /// 删除商品折扣。
        /// </summary>
        /// <param name="ProductAgioID">商品折扣ID</param>
        /// <returns></returns>
        public static XReturn DeleteProductAgio(int ProductAgioID)
        {
            BSC_ProductAgio ProductAgio = new BSC_ProductAgio();
            ProductAgio.ProductAgioID = ProductAgioID;
            return ProductAgio.x.DeleteByIdentity();
        }
        #endregion

        #region 促销规则接口
        /// <summary>
        /// 查询促销规则。
        /// </summary>
        /// <param name="PriceConcessionGroupID">促销规则方案ID</param>
        /// <param name="ReturnTable">返回查询结果</param>
        /// <returns></returns>
        public static XReturn QueryPromotionRuleList(int PriceConcessionGroupID, out DataTable ReturnTable)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            ReturnTable = null;
            string strSql = "select a.*,c.OperatorName from BSC_PromotionRule a" +
                            " left join LPD_Operator c on a.OperatorID=c.OperatorID where 1=1";

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询促销规则失败");
            }

            ReturnTable.Columns.Add(new DataColumn("ConditionProductScope", typeof(string)));
            ReturnTable.Columns.Add(new DataColumn("ImplementProductScope", typeof(string)));

            string ProductScopeInfo, Inclusion, Exclusion;
            foreach (DataRow dr in ReturnTable.Rows)
            {
                // 获取条件商品范围信息
                int ConditionProductScopeID = Convert.ToInt32(dr["ConditionProductScopeID"]);
                xSubReturn = GetProductScopeInfo(ConditionProductScopeID);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, "查询商品范围信息失败");
                }

                ProductScopeInfo = String.Empty;
                Inclusion = xSubReturn.GetStringValue("Inclusion");
                if (!String.IsNullOrEmpty(Inclusion))
                {
                    ProductScopeInfo += string.Format("包含[{0:S}]", Inclusion);
                }
                Exclusion = xSubReturn.GetStringValue("Exclusion");
                if (!String.IsNullOrEmpty(Exclusion))
                {
                    if (!String.IsNullOrEmpty(ProductScopeInfo)) ProductScopeInfo += "\n";
                    ProductScopeInfo += string.Format("排除[{0:S}]", Exclusion);
                }
                dr["ConditionProductScope"] = ProductScopeInfo;

                // 获取执行商品范围信息
                int ImplementProductScopeID = Convert.ToInt32(dr["ImplementProductScopeID"]);
                xSubReturn = GetProductScopeInfo(ImplementProductScopeID);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, "查询商品范围信息失败");
                }

                ProductScopeInfo = String.Empty;
                Inclusion = xSubReturn.GetStringValue("Inclusion");
                if (!String.IsNullOrEmpty(Inclusion))
                {
                    ProductScopeInfo += string.Format("包含[{0:S}]", Inclusion);
                }
                Exclusion = xSubReturn.GetStringValue("Exclusion");
                if (!String.IsNullOrEmpty(Exclusion))
                {
                    if (!String.IsNullOrEmpty(ProductScopeInfo)) ProductScopeInfo += "\n";
                    ProductScopeInfo += string.Format("排除[{0:S}]", Exclusion);
                }

                dr["ImplementProductScope"] = ProductScopeInfo;
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 保存促销规则。
        /// </summary>
        /// <param name="PromotionRule">促销规则</param>
        /// <returns>返回执行结果</returns>
        public static XReturn SavePromotionRule(BSC_PromotionRule PromotionRule)
        {
            if (PromotionRule.PromotionRuleID == 0)
            {
                return PromotionRule.x.Insert();
            }
            else
            {
                return PromotionRule.x.UpdateByIdentity();
            }
        }

        /// <summary>
        /// 删除促销规则。
        /// </summary>
        /// <param name="PromotionRuleID">促销规则ID</param>
        /// <returns></returns>
        public static XReturn DeletePromotionRule(int PromotionRuleID)
        {
            BSC_PromotionRule PromotionRule = new BSC_PromotionRule();
            PromotionRule.PromotionRuleID = PromotionRuleID;
            return PromotionRule.x.DeleteByIdentity();
        }
        #endregion

        #region 商品提示接口
        /// <summary>
        /// 查询商品提示。
        /// </summary>
        /// <param name="ReturnTable">返回查询结果</param>
        /// <returns></returns>
        public static XReturn QueryProductHintList(out DataTable ReturnTable)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            ReturnTable = null;
            string strSql = "select a.* from BSC_ProductHint a where 1=1";

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品提示失败");
            }

            ReturnTable.Columns.Add(new DataColumn("ProductScope", typeof(string)));

            string ProductScopeInfo, Inclusion, Exclusion;
            foreach (DataRow dr in ReturnTable.Rows)
            {
                // 获取条件商品范围信息
                int ProductScopeID = Convert.ToInt32(dr["ProductScopeID"]);
                xSubReturn = GetProductScopeInfo(ProductScopeID);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, "查询商品范围信息失败");
                }

                ProductScopeInfo = String.Empty;
                Inclusion = xSubReturn.GetStringValue("Inclusion");
                if (!String.IsNullOrEmpty(Inclusion))
                {
                    ProductScopeInfo += string.Format("包含[{0:S}]", Inclusion);
                }
                Exclusion = xSubReturn.GetStringValue("Exclusion");
                if (!String.IsNullOrEmpty(Exclusion))
                {
                    if (!String.IsNullOrEmpty(ProductScopeInfo)) ProductScopeInfo += "\n";
                    ProductScopeInfo += string.Format("排除[{0:S}]", Exclusion);
                }
                dr["ProductScope"] = ProductScopeInfo;
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 保存商品提示。
        /// </summary>
        /// <param name="ProductHint">商品提示</param>
        /// <returns>返回执行结果</returns>
        public static XReturn SaveProductHint(BSC_ProductHint ProductHint)
        {
            if (ProductHint.ProductHintID == 0)
            {
                return ProductHint.x.Insert();
            }
            else
            {
                return ProductHint.x.UpdateByIdentity();
            }
        }

        /// <summary>
        /// 删除商品提示。
        /// </summary>
        /// <param name="ProductHintID">商品提示ID</param>
        /// <returns></returns>
        public static XReturn DeleteProductHint(int ProductHintID)
        {
            BSC_ProductHint ProductHint = new BSC_ProductHint();
            ProductHint.ProductHintID = ProductHintID;
            return ProductHint.x.DeleteByIdentity();
        }
        #endregion

        #region 商品范围接口
        /// <summary>
        /// 查询商品范围列表。
        /// </summary>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryProductScopeList(out DataTable ReturnTable)
        {
            string strSql;
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            ReturnTable = null;

            strSql = "select * from BSC_ProductScope";

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品范围列表失败");
            }

            xSubReturn = SupplyProductScopeInfo(ReturnTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "补充商品范围信息失败");
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 补充商品范围信息
        /// </summary>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns></returns>
        private static XReturn SupplyProductScopeInfo(DataTable ReturnTable)
        {
            string strSql;
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            ReturnTable.Columns.Add(new DataColumn("Inclusion", typeof(string)));
            ReturnTable.Columns.Add(new DataColumn("Exclusion", typeof(string)));

            foreach (DataRow dr in ReturnTable.Rows)
            {
                int ProductScopeID = Convert.ToInt32(dr["ProductScopeID"]);
                strSql = "select a.*,b.ProductName,c.ProductCategoryName,d.ProductBrandName from BSC_ProductScopeItem a " +
                        " left join BSC_Product b on a.ProductID=b.ProductID " +
                        " left join BSC_ProductCategory c on a.ProductCategoryID=c.ProductCategoryID " +
                        " left join BSC_ProductBrand d on a.ProductBrandID=d.ProductBrandID " +
                        " where a.ProductScopeID={0:D}" +
                        " order by ProductScopeItemMode";
                strSql = string.Format(strSql, ProductScopeID);

                DataTable dtItems;
                xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out dtItems);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, "查询商品范围明细失败");
                }

                string strItem;
                int ProductScopeItemMode;
                string strInclusion = String.Empty, strExclusion = String.Empty;
                foreach (DataRow drItem in dtItems.Rows)
                {
                    strItem = String.Empty;
                    ProductScopeItemMode = Convert.ToInt32(drItem["ProductScopeItemMode"]);

                    if (!drItem["ProductID"].Equals(0))
                    {
                        strItem += string.Format("商品:{0:S}", drItem["ProductName"]);
                    }

                    if (!drItem["ProductCategoryID"].Equals(0))
                    {
                        if (strItem != String.Empty) strItem += ",";
                        strItem += string.Format("商品类型:{0:S}", drItem["ProductCategoryName"]);
                    }

                    if (!drItem["ProductBrandID"].Equals(0))
                    {
                        if (strItem != String.Empty) strItem += ",";
                        strItem += string.Format("商品品牌:{0:S}", drItem["ProductBrandName"]);
                    }

                    if (ProductScopeItemMode == (int)EProductScopeItemMode.Inclusion)
                    {
                        if (strInclusion.Length > 0) strInclusion += ",";
                        strInclusion += "[" + strItem + "]";
                    }
                    else
                    {
                        if (strExclusion.Length > 0) strExclusion += ",";
                        strExclusion += "[" + strItem + "]";
                    }
                }

                dr["Inclusion"] = strInclusion;
                dr["Exclusion"] = strExclusion;
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 获取商品范围信息
        /// </summary>
        /// <param name="ProductScopeID">商品范围ID</param>
        /// <returns></returns>
        private static XReturn GetProductScopeInfo(int ProductScopeID)
        {
            string strSql;
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            strSql = "select a.*,b.ProductName,c.ProductCategoryName,d.ProductBrandName from BSC_ProductScopeItem a " +
                    " left join BSC_Product b on a.ProductID=b.ProductID " +
                    " left join BSC_ProductCategory c on a.ProductCategoryID=c.ProductCategoryID " +
                    " left join BSC_ProductBrand d on a.ProductBrandID=d.ProductBrandID " +
                    " where a.ProductScopeID={0:D}" +
                    " order by ProductScopeItemMode";
            strSql = string.Format(strSql, ProductScopeID);

            DataTable dtItems;
            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out dtItems);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品范围明细失败");
            }

            string strItem;
            int ProductScopeItemMode;
            string strInclusion = String.Empty, strExclusion = String.Empty;
            foreach (DataRow drItem in dtItems.Rows)
            {
                strItem = String.Empty;
                ProductScopeItemMode = Convert.ToInt32(drItem["ProductScopeItemMode"]);

                if (!drItem["ProductID"].Equals(0))
                {
                    strItem += string.Format("商品:{0:S}", drItem["ProductName"]);
                }

                if (!drItem["ProductCategoryID"].Equals(0))
                {
                    if (strItem != String.Empty) strItem += ",";
                    strItem += string.Format("商品类型:{0:S}", drItem["ProductCategoryName"]);
                }

                if (!drItem["ProductBrandID"].Equals(0))
                {
                    if (strItem != String.Empty) strItem += ",";
                    strItem += string.Format("商品品牌:{0:S}", drItem["ProductBrandName"]);
                }

                if (ProductScopeItemMode == (int)EProductScopeItemMode.Inclusion)
                {
                    if (strInclusion.Length > 0) strInclusion += ",";
                    strInclusion += string.Format("({0:S})", strItem);
                }
                else
                {
                    if (strExclusion.Length > 0) strExclusion += ",";
                    strExclusion += string.Format("({0:S})", strItem);
                }
            }

            xReturn.SetValue("Inclusion", strInclusion);
            xReturn.SetValue("Exclusion", strExclusion);

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 保存商品范围。
        /// </summary>
        /// <param name="ProductScope">商品范围对象</param>
        /// <returns></returns>
        public static XReturn SaveProductScope(BSC_ProductScope ProductScope)
        {
            if (ProductScope.ProductScopeID == 0)
            {
                return ProductScope.x.Insert();
            }
            else
            {
                return ProductScope.x.UpdateByIdentity();
            }
        }

        /// <summary>
        /// 删除商品范围
        /// </summary>
        /// <param name="ProductScopeID">商品范围ID</param>
        /// <returns></returns>
        public static XReturn DeleteProductScope(int ProductScopeID)
        {
            string strSql;
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            // 关联数据检测
            ConjunctionDetector Detector = new ConjunctionDetector();
            Detector.AddSourceField("ProductScopeID", ProductScopeID);
            Detector.AddTargetItem("BSC_CouponGrantConfig", "ProductScopeID");

            xSubReturn = Detector.Run();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "关联数据检测失败");
            }

            TransactionWork transactionWork = DatabaseFactory.GetCurrent().GetTransaction();

            transactionWork.BeginTransition();

            strSql = string.Format("delete from BSC_ProductScope where ProductScopeID={0:D}", ProductScopeID);
            xSubReturn = transactionWork.ExecuteNonQuery(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                transactionWork.Rollback();
                return xReturn.ReturnError(xSubReturn, "删除商品范围记录失败");
            }

            strSql = string.Format("delete from BSC_ProductScopeItem where ProductScopeID={0:D}", ProductScopeID);
            xSubReturn = transactionWork.ExecuteNonQuery(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                transactionWork.Rollback();
                return xReturn.ReturnError(xSubReturn, "删除商品范围明细记录失败");
            }

            transactionWork.Commit();

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询商品范围明细列表。
        /// </summary>
        /// <param name="ProductScopeID">商品范围ID</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryProductScopeItemList(int ProductScopeID, out DataTable ReturnTable)
        {
            string strSql;
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            ReturnTable = null;

            strSql = "select a.*,b.ProductName,c.ProductCategoryName,d.ProductBrandName from BSC_ProductScopeItem a " +
                    " left join BSC_Product b on a.ProductID=b.ProductID " +
                    " left join BSC_ProductCategory c on a.ProductCategoryID=c.ProductCategoryID " +
                    " left join BSC_ProductBrand d on a.ProductBrandID=d.ProductBrandID " +
                    " where a.ProductScopeID={0:D}" +
                    " order by ProductScopeItemMode";
            strSql = string.Format(strSql, ProductScopeID);

            xSubReturn = DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品范围列表失败");
            }

            ReturnTable.Columns.Add(new DataColumn("ProductCategoryFullName", typeof(string)));
            foreach (DataRow dr in ReturnTable.Rows)
            {
                int ProductCategoryID = (int)dr["ProductCategoryID"];
                dr["ProductCategoryFullName"] = ProductCategoryCache.Current.GetProductCategoryFullName(ProductCategoryID);
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 保存商品范围明细。
        /// </summary>
        /// <param name="ProductScopeItem">商品范围明细对象</param>
        /// <returns></returns>
        public static XReturn SaveProductScopeItem(BSC_ProductScopeItem ProductScopeItem)
        {
            if (ProductScopeItem.ProductScopeItemID == 0)
            {
                return ProductScopeItem.x.Insert();
            }
            else
            {
                return ProductScopeItem.x.UpdateByIdentity();
            }
        }
        #endregion
    }
}
