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
    public class ProductImpl
    {
        #region 商品接口
        /// <summary>
        /// 查询商品临时数目。
        /// </summary>
        /// <param name="ProductNo">商品编号</param>
        /// <param name="ProductName">商品名称</param>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <param name="ProductBrandID">商品品牌ID</param>
        /// <param name="EditOperatorID">编辑操作员ID</param>
        /// <param name="AuditOperatorID">审核操作员ID</param>
        /// <param name="FromEditDate">开始日期</param>
        /// <param name="ToEditDate">结束日期</param>
        /// <param name="Status">状态</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryProductTempCount(string ProductNo, string ProductName, int ProductCategoryID, int ProductBrandID, int EditOperatorID, int AuditOperatorID, string FromEditDate, string ToEditDate, int Status)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (ProductNo != String.Empty) strWhere += string.Format(" and a.ProductNo like '%{0:S}%'", ProductNo);
            if (ProductName != String.Empty) strWhere += string.Format(" and a.ProductName like '%{0:S}%'", ProductName);
            if (ProductCategoryID > 0) strWhere += " and " + ProductCategoryCache.Current.GetFilterSql(ProductCategoryID, "a.ProductCategoryID", "b.FullPath");
            if (ProductBrandID > 0) strWhere += string.Format(" and a.ProductBrandID={0:D}", ProductBrandID);
            if (EditOperatorID > 0) strWhere += string.Format(" and a.EditOperatorID={0:D}", EditOperatorID);
            if (AuditOperatorID > 0) strWhere += string.Format(" and a.AuditOperatorID={0:D}", AuditOperatorID);
            if (!String.IsNullOrEmpty(FromEditDate)) strWhere += string.Format(" and a.EditTime>='{0:G}'", DateTimeUtil.ConvertStringToDateTime(FromEditDate));
            if (!String.IsNullOrEmpty(ToEditDate)) strWhere += string.Format(" and a.EditTime<='{0:G}'", DateTimeUtil.ConvertStringToDateTime(ToEditDate).AddDays(1));
            if (Status != 0) strWhere += string.Format(" and a.Status={0:D}", Status);

            strSql = "select count(*) from BSC_ProductTemp a" +
                    " left join BSC_ProductCategory b on a.ProductCategoryID=b.ProductCategoryID " +
                    " where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 查询商品临时列表。
        /// </summary>
        /// <param name="ProductNo">商品编号</param>
        /// <param name="ProductName">商品名称</param>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <param name="ProductBrandID">商品品牌ID</param>
        /// <param name="EditOperatorID">编辑操作员ID</param>
        /// <param name="AuditOperatorID">审核操作员ID</param>
        /// <param name="FromEditDate">开始日期</param>
        /// <param name="ToEditDate">结束日期</param>
        /// <param name="Status">状态</param>
        /// <param name="StartIndex">开始序号</param>
        /// <param name="MaxCount">最大返回数量</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryProductTempList(string ProductNo, string ProductName, int ProductCategoryID, int ProductBrandID, int EditOperatorID, int AuditOperatorID, string FromEditDate, string ToEditDate, int Status, int StartIndex, int MaxCount, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (ProductNo != String.Empty) strWhere += string.Format(" and a.ProductNo like '%{0:S}%'", ProductNo);
            if (ProductName != String.Empty) strWhere += string.Format(" and a.ProductName like '%{0:S}%'", ProductName);
            if (ProductCategoryID > 0) strWhere += " and " + ProductCategoryCache.Current.GetFilterSql(ProductCategoryID, "a.ProductCategoryID", "b.FullPath");
            if (ProductBrandID > 0) strWhere += string.Format(" and ProductBrandID={0:D}", ProductBrandID);
            if (EditOperatorID > 0) strWhere += string.Format(" and a.EditOperatorID={0:D}", EditOperatorID);
            if (AuditOperatorID > 0) strWhere += string.Format(" and a.AuditOperatorID={0:D}", AuditOperatorID);
            if (!String.IsNullOrEmpty(FromEditDate)) strWhere += string.Format(" and a.EditTime>='{0:G}'", DateTimeUtil.ConvertStringToDateTime(FromEditDate));
            if (!String.IsNullOrEmpty(ToEditDate)) strWhere += string.Format(" and a.EditTime<='{0:G}'", DateTimeUtil.ConvertStringToDateTime(ToEditDate).AddDays(1));
            if (Status != 0) strWhere += string.Format(" and a.Status={0:D}", Status);

            strSql = "select a.*,b.ProductCategoryName,c.ProductBrandName,d.OperatorName as EditOperatorName,e.OperatorName as AuditOperatorName " +
                    " from BSC_ProductTemp a " +
                    " left join BSC_ProductCategory b on a.ProductCategoryID=b.ProductCategoryID" +
                    " left join BSC_ProductBrand c on a.ProductBrandID=c.ProductBrandID" +
                    " left join LPD_Operator d on a.EditOperatorID=d.OperatorID" +
                    " left join LPD_Operator e on a.AuditOperatorID=e.OperatorID" +
                    " where {0:S} order by a.EditTime desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out ReturnTable);
        }

        /// <summary>
        /// 查询商品数目。
        /// </summary>
        /// <param name="ProductNo">商品编号</param>
        /// <param name="ProductName">商品名称</param>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <param name="ProductBrandID">商品品牌ID</param>
        /// <param name="FromPrice">开始价格</param>
        /// <param name="ToPrice">结束价格</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryProductCount(string ProductNo, string ProductName, int ProductCategoryID, int ProductBrandID, double FromPrice, double ToPrice)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (ProductNo != String.Empty) strWhere += string.Format(" and ProductNo like '%{0:S}%'", ProductNo);
            if (ProductName != String.Empty) strWhere += string.Format(" and ProductName like '%{0:S}%'", ProductName);
            if (ProductCategoryID != 0)
            {
                string strProductCategoryIDs = ProductCategoryCache.Current.GetProductCategoryIDs(ProductCategoryID);
                if (strProductCategoryIDs != String.Empty)
                {
                    strWhere += string.Format(" and ProductCategoryID in ({0:S})", strProductCategoryIDs);
                }
            }
            if (ProductBrandID != 0) strWhere += string.Format(" and ProductBrandID={0:D}", ProductBrandID);
            if (FromPrice > 0) strWhere += string.Format(" and SalesPrice>={0:F}", FromPrice);
            if (ToPrice > 0) strWhere += string.Format(" and SalesPrice<={0:F}", ToPrice);

            strSql = "select count(*) from BSC_Product where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 查询商品列表。
        /// </summary>
        /// <param name="ProductNo">商品编号</param>
        /// <param name="ProductName">商品名称</param>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <param name="ProductBrandID">商品品牌ID</param>
        /// <param name="FromPrice">开始价格</param>
        /// <param name="ToPrice">结束价格</param>
        /// <param name="StartIndex">开始序号</param>
        /// <param name="MaxCount">最大返回数量</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryProductList(string ProductNo, string ProductName, int ProductCategoryID, int ProductBrandID, double FromPrice, double ToPrice, int StartIndex, int MaxCount, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (ProductNo != String.Empty) strWhere += string.Format(" and a.ProductNo like '%{0:S}%'", ProductNo);
            if (ProductName != String.Empty) strWhere += string.Format(" and a.ProductName like '%{0:S}%'", ProductName);
            if (ProductCategoryID != 0)
            {
                string strProductCategoryIDs = ProductCategoryCache.Current.GetProductCategoryIDs(ProductCategoryID);
                if (strProductCategoryIDs != String.Empty)
                {
                    strWhere += string.Format(" and ProductCategoryID in ({0:S})", strProductCategoryIDs);
                }
            }
            if (ProductBrandID != 0) strWhere += string.Format(" and a.ProductBrandID={0:D}", ProductBrandID);
            if (FromPrice > 0) strWhere += string.Format(" and a.SalesPrice>={0:F}", FromPrice);
            if (ToPrice > 0) strWhere += string.Format(" and a.SalesPrice<={0:F}", ToPrice);

            strSql = "select a.*,b.ProductCategoryName,c.ProductBrandName from BSC_Product a " +
                    " left join BSC_ProductCategory b on a.ProductCategoryID=b.ProductCategoryID" +
                    " left join BSC_ProductBrand c on a.ProductBrandID=c.ProductBrandID" +
                    " where {0:S} order by a.ProductID desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out ReturnTable);
        }

        /// <summary>
        /// 保存商品。
        /// </summary>
        /// <param name="EditMode">数据变更模式</param>
        /// <param name="Product">商品实例</param>
        /// <param name="ProductSpec">商品规格实例</param>
        /// <returns>返回执行结果</returns>
        public static XReturn SaveProduct(BSC_Product Product, BSC_ProductSpec ProductSpec)
        {
            XReturn xReturn = new XReturn();
            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();

            xTransaction.BeginTransition();

            XReturn xSubReturn = SaveProduct(xTransaction, Product, ProductSpec);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();

                xReturn.SetError(xSubReturn);
                return xReturn;
            }

            xTransaction.Commit();

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 保存商品临时表。
        /// </summary>
        /// <param name="EditMode">数据变更模式</param>
        /// <param name="Product">商品实例</param>
        /// <param name="ProductSpec">商品规格实例</param>
        /// <returns>返回执行结果</returns>
        public static XReturn SaveProductTemp(BSC_ProductTemp ProductTemp)
        {
            XReturn xReturn = new XReturn();
            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();

            xTransaction.BeginTransition();

            XReturn xSubReturn = SaveProductTemp(xTransaction, ProductTemp);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError(xSubReturn);
            }

            xTransaction.Commit();

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 保存商品临时表。
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="Product">商品实例</param>
        /// <param name="ProductSpec">商品规格实例</param>
        /// <returns>返回执行结果</returns>
        public static XReturn SaveProductTemp(TransactionWork transaction, BSC_ProductTemp ProductTemp)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            string workKey = LeopardFactory.GetWordKeyCache().GetWordKeyDigest(ProductTemp.ProductName);
            string productCategoryName = ProductCategoryCache.Current.GetProductCategoryName(ProductTemp.ProductCategoryID);
            string productBrandName = ProductBrandCache.Current.GetProductBrandName(ProductTemp.ProductBrandID);

            ProductTemp.WordKey = productCategoryName + "," + productBrandName + "," + ProductTemp.ProductNo.Trim() + "," + workKey;

            if (ProductTemp.ProductTempID == 0)
            {
                xSubReturn = ProductTemp.x.Insert(transaction);
            }
            else
            {
                xSubReturn = ProductTemp.x.UpdateByIdentity(transaction);
            }

            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "插入或者更新商品临时记录失败");
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 通过商品临时表审核。
        /// </summary>
        /// <param name="EditMode">数据变更模式</param>
        /// <param name="Product">商品实例</param>
        /// <param name="ProductSpec">商品规格实例</param>
        /// <returns>返回执行结果</returns>
        public static XReturn AuditPassProduct(BSC_Product Product)
        {
            XReturn xReturn = new XReturn();
            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();

            xTransaction.BeginTransition();

            XReturn xSubReturn = AuditPassProduct(xTransaction, Product);
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
        /// 通过商品临时表审核。
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="Product">商品实例</param>
        /// <param name="ProductSpec">商品规格实例</param>
        /// <returns>返回执行结果</returns>
        public static XReturn AuditPassProduct(TransactionWork transaction, BSC_Product Product)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            string workKey = LeopardFactory.GetWordKeyCache().GetWordKeyDigest(Product.ProductName);
            string productCategoryName = ProductCategoryCache.Current.GetProductCategoryName(Product.ProductCategoryID);
            string productBrandName = ProductBrandCache.Current.GetProductBrandName(Product.ProductBrandID);

            Product.WordKey = productCategoryName + "," + productBrandName + "," + Product.ProductNo.Trim() + "," + workKey;

            xSubReturn = Product.x.Insert(transaction);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 刷新商品缓存
        /// </summary>
        /// <param name="ProductID">商品ID</param>
        /// <returns></returns>
        public static XReturn RefreshProductCache(int ProductID)
        {
            XReturn xReturn = new XReturn();
            
            string strUrl = string.Format("http://www.ibaby361.com/RefreshCache.aspx?CacheCode=Product&&ProductID={0:D}", ProductID);

            string strResp = WebUtil.PostWebRequest(strUrl);

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 保存商品。
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="Product">商品实例</param>
        /// <param name="ProductSpec">商品规格实例</param>
        /// <returns>返回执行结果</returns>
        public static XReturn SaveProduct(TransactionWork transaction, BSC_Product Product, BSC_ProductSpec ProductSpec)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            string workKey = LeopardFactory.GetWordKeyCache().GetWordKeyDigest(Product.ProductName);
            string productCategoryName = ProductCategoryCache.Current.GetProductCategoryName(Product.ProductCategoryID);
            string productBrandName = ProductBrandCache.Current.GetProductBrandName(Product.ProductBrandID);

            Product.WordKey = productCategoryName + "," + productBrandName + "," + Product.ProductNo.Trim() + "," + workKey;

            if (Product.ProductID == 0)
            {
                xSubReturn = Product.x.Insert(transaction);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn);
                }

                ProductSpec.ProductID = Product.ProductID;
                ProductSpec.DefaultFlag = (int)EYesNo.Yes;
                xSubReturn = ProductSpec.x.Insert(transaction);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn);
                }

                Product.DefaultProductSpecID = ProductSpec.ProductSpecID;
                xSubReturn = Product.x.UpdateByIdentity(transaction);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn);
                }

                BSC_ProductPicture ProductPicture = new BSC_ProductPicture();
                ProductPicture.ProductID = Product.ProductID;
                ProductPicture.FileName = Product.ProductNo;
                ProductPicture.FileType = "jpg";
                ProductPicture.OrderNo = 0;
                xSubReturn = ProductPicture.x.Insert(transaction);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn);
                }

                if (ProductSpec.StockInitQuantity > 0)
                {
                    ProductStockChangeRequestParameter RequestParameter = new ProductStockChangeRequestParameter();
                    RequestParameter.ProductSpecID = Product.DefaultProductSpecID;
                    RequestParameter.WarehouseID = 0;
                    RequestParameter.ChangeType = (int)EProductStockChangeType.StockInit;
                    RequestParameter.ChangeQuantity = ProductSpec.StockInitQuantity;
                    RequestParameter.ChangeReason = String.Empty;
                    RequestParameter.AssociateVoucherType = 0;
                    RequestParameter.AssociateVoucherNo = String.Empty;
                    RequestParameter.AssociateID = 0;
                    RequestParameter.OperatorID = 0;

                    // 提交商品库存变动。
                    xSubReturn = InventoryImpl.SubmitProductStockChange(transaction, RequestParameter);
                    if (xSubReturn.IsUnSuccess())
                    {
                        return xReturn.ReturnError(xSubReturn);
                    }
                }
            }
            else
            {
                xSubReturn = Product.x.UpdateByIdentity(transaction);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn);
                }

                xSubReturn = ProductSpec.x.UpdateByIdentity(transaction);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn);
                }
            }

            BSC_ProductCategoryBrand ProductCategoryBrand = new BSC_ProductCategoryBrand();
            ProductCategoryBrand.ProductBrandID = Product.ProductBrandID;
            ProductCategoryBrand.ProductCategoryID = Product.ProductCategoryID;
            xReturn = ProductImpl.SaveProductCategoryBrand(ProductCategoryBrand);
            if (xReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }


            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 更新所有商品检索词。
        /// </summary>
        /// <returns></returns>
        public static XReturn UpdateAllProductWordKey()
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            EntityQueryer entityQueryer = new EntityQueryer("BSC_Product");
            xSubReturn = entityQueryer.Query();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询所有商品失败");
            }

            foreach (BSC_Product Product in entityQueryer.EntityList)
            {
                string workKey = LeopardFactory.GetWordKeyCache().GetWordKeyDigest(Product.ProductName);
                string productCategoryName = ProductCategoryCache.Current.GetProductCategoryName(Product.ProductCategoryID);
                string productBrandName = ProductBrandCache.Current.GetProductBrandName(Product.ProductBrandID);

                Product.WordKey = productCategoryName + "," + productBrandName + "," + Product.ProductNo.Trim() + "," + workKey;
                xSubReturn = Product.x.UpdateByIdentity();
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, string.Format("更新商品[{0:S}][{1:S}]检索词失败", Product.ProductNo, Product.ProductName));
                }
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 保存商品类型品牌对应。
        /// </summary>
        /// <param name="ProductCategoryBrand">商品品牌</param>
        /// <returns>返回操作结果</returns>
        public static XReturn SaveProductCategoryBrand(BSC_ProductCategoryBrand ProductCategoryBrand)
        {
            XReturn xReturn = new XReturn();

            if (!ProductCategoryBrand.x.Exist())
            {
                return ProductCategoryBrand.x.Insert();
            }

            xReturn.ReturnCode = "0000";
            return xReturn;
        }

        /// <summary>
        /// 删除商品。
        /// </summary>
        /// <param name="ProductID">商品ID</param>
        /// <returns></returns>
        public static XReturn DeleteProduct(int ProductID)
        {
            string strSql;
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            TransactionWork xTransaction = DatabaseFactory.GetCurrent().GetTransaction();

            xTransaction.BeginTransition();

            strSql = string.Format("delete from BSC_ProductPicture where ProductID={0:D}", ProductID);
            xSubReturn = xTransaction.ExecuteNonQuery(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError("删除商品图片失败");
            }

            strSql = string.Format("delete from BSC_ProductSpec where ProductID={0:D}", ProductID);
            xSubReturn = xTransaction.ExecuteNonQuery(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError("删除商品规格失败");
            }

            strSql = string.Format("delete from BSC_Product where ProductID={0:D}", ProductID);
            xSubReturn = xTransaction.ExecuteNonQuery(strSql);
            if (xSubReturn.IsUnSuccess())
            {
                xTransaction.Rollback();
                return xReturn.ReturnError("删除商品失败");
            }

            xTransaction.Commit();
            return xReturn.ReturnSuccess();
        }
        #endregion

        #region 商品规格处理
        /// <summary>
        /// 查询商品规格。
        /// </summary>
        /// <param name="ProductID">商品ID</param>
        /// <param name="ProductSpec">规格</param>
        /// <param name="ProductUnit">单位</param>
        /// <param name="ReturnTable">返回查询结果</param>
        /// <returns></returns>
        public static XReturn QueryProductSpecList(int ProductID, string ProductSpec, string ProductUnit, out DataTable ReturnTable)
        {
            ReturnTable = null;

            string strSql = "select a.*,b.ProductNo,b.ProductName,b.ProductUnit,b.SalesPrice from BSC_ProductSpec a left join BSC_Product b on a.ProductID=b.ProductID where 1=1";

            if (ProductID != 0) strSql += string.Format(" and a.ProductID={0:D}", ProductID);
            if (ProductSpec != String.Empty) strSql += string.Format(" and a.ProductSpec like '%{0:S}%'", ProductSpec);
            if (ProductUnit != String.Empty) strSql += string.Format(" and b.ProductUnit = '{0:S}'", ProductUnit);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
        }

        /// <summary>
        /// 保存商品规格。
        /// </summary>
        /// <param name="ProductSpec">商品规格实例</param>
        /// <returns>返回执行结果</returns>
        public static XReturn SaveProductSpec(BSC_ProductSpec ProductSpec)
        {
            if (ProductSpec.ProductSpecID > 0)
            {
                return ProductSpec.x.UpdateByIdentity();
            }
            else
            {
                return ProductSpec.x.Insert();
            }
        }

        /// <summary>
        /// 删除商品规格。
        /// </summary>
        /// <param name="ProductSpecID">商品规格ID</param>
        /// <returns></returns>
        public static XReturn DeleteProductSpec(int ProductSpecID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            BSC_ProductSpec ProductSpec = new BSC_ProductSpec();

            // 查询商品规格。
            ProductSpec.ProductSpecID = ProductSpecID;
            xSubReturn = ProductSpec.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn);
                return xReturn;
            }

            if (ProductSpec.DefaultFlag == 1)
            {
                xReturn.ReturnCode = "9999";
                xReturn.ReturnMessage = "默认规格不能删除";
                return xReturn;
            }

            // 删除规格
            xSubReturn = ProductSpec.x.DeleteByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn);
                return xReturn;
            }

            xReturn.ReturnCode = "0000";
            return xReturn;
        }
        #endregion

        #region 商品图片处理
        /// <summary>
        /// 查询商品图片。
        /// </summary>
        /// <param name="ProductID">商品ID</param>
        /// <param name="FileName">图片名称</param>
        /// <param name="ReturnTable">返回查询结果</param>
        /// <returns></returns>
        public static XReturn QueryProductPictureList(int ProductID, string FileName, out DataTable ReturnTable)
        {
            string strSql, strWhere;
            ReturnTable = null;

            strWhere = "1=1";
            if (ProductID > 0) strWhere += string.Format(" and a.ProductID={0:D}", ProductID);
            if (FileName != String.Empty) strWhere += string.Format(" and a.FileName like '%{0:S}%'", FileName);

            strSql = "select a.*,b.ProductNo,b.ProductName from BSC_ProductPicture a " +
                    " left join BSC_Product b on a.ProductID=b.ProductID " +
                    " where {0:S} order by a.OrderNo";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
        }

        /// <summary>
        /// 保存商品图片。
        /// </summary>
        /// <param name="ProductPicture">商品图片实例</param>
        /// <returns>返回执行结果</returns>
        public static XReturn SaveProductPicture(BSC_ProductPicture ProductPicture)
        {
            if (ProductPicture.ProductPictureID > 0)
            {
                return ProductPicture.x.UpdateByIdentity();
            }
            else
            {
                return ProductPicture.x.Insert();
            }
        }

        /// <summary>
        /// 删除商品图片。
        /// </summary>
        /// <param name="ProductPictureID">商品图片ID</param>
        /// <returns></returns>
        public static XReturn DeleteProductPicture(int ProductPictureID)
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            BSC_ProductPicture ProductPicture = new BSC_ProductPicture();
            ProductPicture.ProductPictureID = ProductPictureID;

            // 删除图片
            xSubReturn = ProductPicture.x.DeleteByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                xReturn.SetError(xSubReturn);
                return xReturn;
            }

            xReturn.ReturnCode = "0000";
            return xReturn;
        }
        #endregion

        #region 商品类型接口
        /// <summary>
        /// 查询商品类型列表。
        /// </summary>
        /// <param name="ProductCategoryName">商品类型名称</param>
        /// <param name="ParentID">父商品类型代码</param>
        /// <param name="NodeLevel">节点级别</param>
        /// <param name="ReturnTable">返回结果数据表</param>
        /// <returns></returns>
        public static XReturn QueryProductCategoryList(string ProductCategoryName, int ParentID, int NodeLevel, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (ProductCategoryName != String.Empty) strWhere += string.Format(" and ProductCategoryName like '%{0:S}%'", ProductCategoryName);
            if (ParentID > 0) strWhere += string.Format(" and ParentID={0:D}", ParentID);
            if (NodeLevel > 0) strWhere += string.Format(" and NodeLevel={0:D}", NodeLevel);

            strSql = "select * from BSC_ProductCategory where {0:S} order by NodeLevel,OrderNo";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
        }

        /// <summary>
        /// 查询商品类型商品关联列表。
        /// </summary>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <param name="ProductBrandID">商品品牌ID</param>
        /// <param name="ReturnTable">返回结果数据表</param>
        /// <returns></returns>
        public static XReturn QueryProductCategoryBrandList(int ProductCategoryID, int ProductBrandID, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (ProductCategoryID > 0) strWhere += string.Format(" and ProductCategoryID={0:D}", ProductCategoryID);
            if (ProductBrandID > 0) strWhere += string.Format(" and ProductBrandID={0:D}", ProductBrandID);

            strSql = "select * from BSC_ProductCategoryBrand where {0:S} order by ProductCategoryID,ProductBrandID";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
        }
        #endregion

        #region 商品品牌接口
        /// <summary>
        /// 查询商品品牌数目。
        /// </summary>
        /// <param name="ProductBrandCode">商品代码</param>
        /// <param name="ProductBrandName">商品名称</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryProductBrandCount(string ProductBrandCode, string ProductBrandName)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (ProductBrandCode != String.Empty) strWhere += string.Format(" and ProductBrandCode like '%{0:S}%'", ProductBrandCode);
            if (ProductBrandName != String.Empty) strWhere += string.Format(" and ProductBrandName like '%{0:S}%'", ProductBrandName);

            strSql = "select count(*) from BSC_ProductBrand where {0:S}";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().ExecuteScalar(strSql);
        }

        /// <summary>
        /// 查询商品品牌列表。
        /// </summary>
        /// <param name="ProductBrandCode">商品品牌代码</param>
        /// <param name="ProductBrandName">商品品牌名称</param>
        /// <param name="StartIndex">开始序号</param>
        /// <param name="MaxCount">最大返回数量</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryProductBrandList(string ProductBrandCode, string ProductBrandName, int StartIndex, int MaxCount, out DataTable ReturnTable)
        {
            string strSql, strWhere;

            strWhere = "1=1";
            if (ProductBrandCode != String.Empty) strWhere += string.Format(" and ProductBrandCode like '%{0:S}%'", ProductBrandCode);
            if (ProductBrandName != String.Empty) strWhere += string.Format(" and ProductBrandName like '%{0:S}%'", ProductBrandName);

            strSql = "select * from BSC_ProductBrand where {0:S} order by ProductBrandCode desc";
            strSql = string.Format(strSql, strWhere);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, StartIndex, MaxCount, out ReturnTable);
        }
        #endregion

        #region 数据挖掘接口
        /// <summary>
        /// 查询浏览后购买其他商品列表。
        /// </summary>
        /// <param name="ViewProductID">浏览商品</param>
        /// <param name="ReturnTable">返回数据表</param>
        /// <returns>返回执行结果</returns>
        public static XReturn QueryViewThenBuyOtherProductList(int ViewProductID, out DataTable ReturnTable)
        {
            string strSql;

            strSql = "select b.ProductID,b.ProductName,b.SalesPrice from CFG_ViewThenBuyOtherProductList a left join BSC_Product b on a.BuyProductID=b.ProductID where ViewProductID={0:D} order by a.Quantity desc";
            strSql = string.Format(strSql, ViewProductID);

            return DatabaseFactory.GetCurrent().GetDataTable(strSql, out ReturnTable);
        }
        #endregion
    }
}
