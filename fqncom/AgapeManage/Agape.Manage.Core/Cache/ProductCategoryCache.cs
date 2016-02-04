using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Leopard.Util;
using Leopard.Data;
using Leopard.Cache;
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Impl;

namespace Agape.Manage.Core.Cache
{
    public class BSC_ProductCategoryExt
    {
        public BSC_ProductCategory ProductCategory;
        public ArrayList SubProductCategoryList;
        public ArrayList AssoProductBrandList;

        public BSC_ProductCategoryExt()
        {
            ProductCategory = new BSC_ProductCategory();
            SubProductCategoryList = new ArrayList();
            AssoProductBrandList = new ArrayList();
        }
    }

    public class ProductCategoryCache
    {
        #region 静态部分
        private static ProductCategoryCache m_Instance;

        static ProductCategoryCache()
        {
            m_Instance = null;
        }

        public static ProductCategoryCache Current
        {
            get { return GetInstance(); }
        }

        public static ProductCategoryCache GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new ProductCategoryCache();
            }
            return m_Instance;
        }
        #endregion

        private Hashtable m_ProductCategoryExts;
        protected string m_ProductCategoryTreeHtml;

        private ProductCategoryCache()
        {
        }

        /// <summary>
        /// 导入商品类型数据
        /// </summary>
        public XReturn LoadCache()
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            m_ProductCategoryExts = new Hashtable();
            m_ProductCategoryTreeHtml = String.Empty;

            BSC_ProductCategoryExt _ProductCategoryExt;
            BSC_ProductCategoryExt _ParentProductCategoryExt;
            DataTable _ProductCategoryTable;

            xSubReturn = ProductImpl.QueryProductCategoryList(String.Empty, 0, 0, out _ProductCategoryTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品类型失败");
            }

            _ProductCategoryExt = new BSC_ProductCategoryExt();
            _ProductCategoryExt.ProductCategory.ProductCategoryID = 0;
            _ProductCategoryExt.ProductCategory.ProductCategoryName = "所以分类";
            _ProductCategoryExt.ProductCategory.NodeLevel = 0;
            m_ProductCategoryExts.Add(_ProductCategoryExt.ProductCategory.ProductCategoryID, _ProductCategoryExt);

            foreach (DataRow dr in _ProductCategoryTable.Rows)
            {
                _ProductCategoryExt = new BSC_ProductCategoryExt();
                _ProductCategoryExt.ProductCategory.x.CopyFrom(dr);
                _ProductCategoryExt.ProductCategory.x.TrimEntity();
                m_ProductCategoryExts.Add(_ProductCategoryExt.ProductCategory.ProductCategoryID, _ProductCategoryExt);

                _ParentProductCategoryExt = GetProductCategoryExt(_ProductCategoryExt.ProductCategory.ParentID);
                if (_ParentProductCategoryExt == null)
                {
                    return xReturn.ReturnError("找不到商品类型");
                }
                _ParentProductCategoryExt.SubProductCategoryList.Add(_ProductCategoryExt);
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 载入关联商品品牌。
        /// </summary>
        /// <returns></returns>
        public XReturn LoadAssoProductBrand()
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();
            
            BSC_ProductCategoryExt cpxProductCategory;
            BSC_ProductBrandExt cpxProductBrand;
            DataTable _ProductCategoryBrandTable;
            BSC_ProductCategoryBrand ProductCatetoryBrand = new BSC_ProductCategoryBrand();

            xSubReturn = ProductImpl.QueryProductCategoryBrandList(0, 0, out _ProductCategoryBrandTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品类型商品关联列表失败");
            }

            foreach (DataRow dr in _ProductCategoryBrandTable.Rows)
            {
                ProductCatetoryBrand.x.CopyFrom(dr);

                cpxProductCategory = GetProductCategoryExt(ProductCatetoryBrand.ProductCategoryID);
                if (cpxProductCategory == null)
                {
                    return xReturn.ReturnError("获取商品类型失败");
                }

                cpxProductBrand = ProductBrandCache.Current.GetProductBrandExt(ProductCatetoryBrand.ProductBrandID);
                if (cpxProductBrand == null)
                {
                    return xReturn.ReturnError("获取商品品牌失败");
                }

                cpxProductCategory.AssoProductBrandList.Add(cpxProductBrand);
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 增加管理商品品牌
        /// </summary>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <param name="ProductBrandID">商品品牌ID</param>
        /// <returns></returns>
        public XReturn AddAssoProductBrand(int ProductCategoryID, int ProductBrandID)
        {
            XReturn xReturn=new XReturn();
            BSC_ProductCategoryExt cpxProductCategory = GetProductCategoryExt(ProductCategoryID);
            if (cpxProductCategory == null)
            {
                return xReturn.ReturnErrorNotLog(string.Format("找不到商品类型[{0:D}]", ProductCategoryID));
            }

            BSC_ProductBrandExt cpxProductBrand = ProductBrandCache.Current.GetProductBrandExt(ProductBrandID);
            if (cpxProductBrand == null)
            {
                return xReturn.ReturnErrorNotLog(string.Format("找不到商品商品[{0:D}]", ProductBrandID));
            }

            foreach (BSC_ProductBrandExt cpxAssoProductBrand in cpxProductCategory.AssoProductBrandList)
            {
                if (cpxAssoProductBrand == cpxProductBrand)
                {
                    return xReturn.ReturnSuccess();
                }
            }

            cpxProductCategory.AssoProductBrandList.Add(cpxProductBrand);

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询商品类型扩展对象。
        /// </summary>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <returns></returns>
        public BSC_ProductCategoryExt GetProductCategoryExt(int ProductCategoryID)
        {
            if (m_ProductCategoryExts.Contains(ProductCategoryID))
            {
                return m_ProductCategoryExts[ProductCategoryID] as BSC_ProductCategoryExt;
            }

            return null;
        }

        /// <summary>
        /// 查询商品类型对象。
        /// </summary>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <returns></returns>
        public BSC_ProductCategory GetProductCategory(int ProductCategoryID)
        {
            if (m_ProductCategoryExts.Contains(ProductCategoryID))
            {
                return ((BSC_ProductCategoryExt)m_ProductCategoryExts[ProductCategoryID]).ProductCategory;
            }

            return null;
        }

        /// <summary>
        /// 获取商品类型名称。
        /// </summary>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <returns>返回商品类型名称</returns>
        public string GetProductCategoryName(int ProductCategoryID)
        {
            BSC_ProductCategory ProductCategory = GetProductCategory(ProductCategoryID);
            if (ProductCategory == null) return String.Empty;
            else return ProductCategory.ProductCategoryName;
        }

        /// <summary>
        /// 获取商品类型ID字符串
        /// </summary>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <returns></returns>
        public string GetProductCategoryIDs(int ProductCategoryID)
        {
            string strSql, strSubSql;
            BSC_ProductCategoryExt ProductCategoryExt = GetProductCategoryExt(ProductCategoryID);
            if (ProductCategoryExt.ProductCategory.LeafFlag == 1)
            {
                strSql = ProductCategoryExt.ProductCategory.ProductCategoryID.ToString();
            }
            else
            {
                strSql = String.Empty;
                foreach (BSC_ProductCategoryExt SubProductCategoryExt in ProductCategoryExt.SubProductCategoryList)
                {
                    strSubSql = GetProductCategoryIDs(SubProductCategoryExt.ProductCategory.ProductCategoryID);
                    if (strSubSql != String.Empty)
                    {
                        if (strSql != String.Empty) strSql += ",";
                        strSql += strSubSql;
                    }
                }
            }
            return strSql;
        }

        /// <summary>
        /// 获取商品类型对象。
        /// </summary>
        /// <param name="ProductCategoryName">商品类型名称</param>
        /// <returns></returns>
        public BSC_ProductCategoryExt GetProductCategoryExtByName(string ProductCategoryName)
        {
            BSC_ProductCategoryExt cpxProductCategory;

            foreach (int id in m_ProductCategoryExts.Keys)
            {
                cpxProductCategory = (BSC_ProductCategoryExt)m_ProductCategoryExts[id];
                if (cpxProductCategory.ProductCategory.ProductCategoryName == ProductCategoryName)
                {
                    return cpxProductCategory;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取关联商品品牌列表。
        /// </summary>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <returns></returns>
        public ArrayList GetAssoProductBrands(int ProductCategoryID)
        {
            ArrayList _SubAssoProductBrands;
            ArrayList _AssoProductBrands = new ArrayList();
            BSC_ProductCategoryExt ProductCategoryExt = GetProductCategoryExt(ProductCategoryID);

            if (ProductCategoryExt.AssoProductBrandList.Count>0)
            {
                foreach (BSC_ProductBrandExt cpxProductBrand in ProductCategoryExt.AssoProductBrandList)
                {
                    _AssoProductBrands.Add(cpxProductBrand);
                }
            }

            if (ProductCategoryExt.ProductCategory.LeafFlag == 0)
            {
                foreach (BSC_ProductCategoryExt cpxSubProductCategory in ProductCategoryExt.SubProductCategoryList)
                {
                    _SubAssoProductBrands = GetAssoProductBrands(cpxSubProductCategory.ProductCategory.ProductCategoryID);
                    if (_SubAssoProductBrands.Count > 0)
                    {
                        foreach (BSC_ProductBrandExt cpxProductBrand in _SubAssoProductBrands)
                        {
                            if (!_AssoProductBrands.Contains(cpxProductBrand))
                            {
                                _AssoProductBrands.Add(cpxProductBrand);
                            }
                        }
                    }
                }
            }

            return _AssoProductBrands;
        }

        /// <summary>
        /// 获取关联商品类型列表。
        /// </summary>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <returns></returns>
        public ArrayList GetAssoProductCategories(int ProductCategoryID)
        {
            BSC_ProductCategoryExt cpxProductCategory = GetProductCategoryExt(ProductCategoryID);
            if (cpxProductCategory == null)
            {
                return null;
            }

            BSC_ProductCategoryExt cpxParentProductCategory = GetProductCategoryExt(cpxProductCategory.ProductCategory.ParentID);
            if (cpxParentProductCategory == null)
            {
                return null;
            }

            ArrayList AssoProductCategories = new ArrayList();
            foreach (BSC_ProductCategoryExt cpxAssoProductCategory in cpxParentProductCategory.SubProductCategoryList)
            {
                AssoProductCategories.Add(cpxAssoProductCategory);
            }

            return AssoProductCategories;
        }


        /// <summary>
        /// 更新所有商品类型全路径。
        /// </summary>
        /// <returns></returns>
        public XReturn UpdateAllProductCategoryFullPath()
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            foreach (int ProductCategoryID in m_ProductCategoryExts.Keys)
            {
                BSC_ProductCategoryExt cpxProductCategory = m_ProductCategoryExts[ProductCategoryID] as BSC_ProductCategoryExt;
                if (cpxProductCategory.ProductCategory.ProductCategoryID == 0) continue;

                cpxProductCategory.ProductCategory.FullPath = GetProductCategoryFullPath(cpxProductCategory.ProductCategory.ProductCategoryID);
                xSubReturn = cpxProductCategory.ProductCategory.x.UpdateByIdentity();
                if (xSubReturn.IsUnSuccess() && xSubReturn.ReturnCode != "0001")
                {
                    return xReturn.ReturnError(xSubReturn, "更新商品类型全路径失败");
                }
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 检查商品类型被包含
        /// </summary>
        /// <param name="CheckProductCategoryID">检查商品类型ID</param>
        /// <param name="TargetProductCategoryID">目标商品类型ID</param>
        /// <returns></returns>
        public bool CheckProductCategoryIncluded(int CheckProductCategoryID, int TargetProductCategoryID)
        {
            while (CheckProductCategoryID > 0)
            {
                if (CheckProductCategoryID == TargetProductCategoryID)
                {
                    return true;
                }

                BSC_ProductCategory checkProductCategory = GetProductCategory(CheckProductCategoryID);
                if (checkProductCategory == null)
                {
                    return false;
                }
                CheckProductCategoryID = checkProductCategory.ParentID;
            }

            return false;
        }

        /// <summary>
        /// 获取商品类型全路径。
        /// </summary>
        /// <param name="ParentID">父商品类型ID</param>
        /// <returns></returns>
        public string GetProductCategoryFullPath(int ParentID)
        {
            string strFullPath = String.Empty;
            BSC_ProductCategoryExt cpxProductCategory = GetProductCategoryExt(ParentID);
            while (cpxProductCategory != null && cpxProductCategory.ProductCategory.ProductCategoryID > 0)
            {
                strFullPath = string.Format("#{0:D}{1:S}", cpxProductCategory.ProductCategory.ProductCategoryID, strFullPath);
                cpxProductCategory = GetProductCategoryExt(cpxProductCategory.ProductCategory.ParentID);
            }

            return strFullPath;
        }

        /// <summary>
        /// 获取商品类型全路径。
        /// </summary>
        /// <param name="ProductCategoryID">父商品类型ID</param>
        /// <returns></returns>
        public string GetProductCategoryFullName(int ProductCategoryID)
        {
            string strFullPath = String.Empty;
            BSC_ProductCategoryExt cpxProductCategory = GetProductCategoryExt(ProductCategoryID);
            while (cpxProductCategory != null && cpxProductCategory.ProductCategory.ProductCategoryID > 0)
            {
                if (strFullPath != String.Empty) strFullPath = "->" + strFullPath;
                strFullPath = cpxProductCategory.ProductCategory.ProductCategoryName + strFullPath;
                cpxProductCategory = GetProductCategoryExt(cpxProductCategory.ProductCategory.ParentID);
            }

            return strFullPath;
        }

        /// <summary>
        /// 获取条件SQL语句
        /// </summary>
        /// <param name="productCategoryID">商品类型ID</param>
        /// <param name="productCategoryIDFieldName">商品类型ID字段名称</param>
        /// <param name="fullPathFieldName">全路径字段名称</param>
        /// <returns></returns>
        public string GetFilterSql(int productCategoryID, string productCategoryIDFieldName, string fullPathFieldName)
        {
            string sqlFilter = String.Empty;

            if (productCategoryID > 0)
            {
                if (ProductCategoryCache.Current.IsProductCategoryLeaf(productCategoryID))
                {
                    sqlFilter = string.Format("{0:S} = {1:D}", productCategoryIDFieldName, productCategoryID);
                }
                else
                {
                    string strFullPath = ProductCategoryCache.Current.GetProductCategoryFullPath(productCategoryID);
                    if (strFullPath.Length > 0)
                    {
                        if (LeopardConfigs.DBMS == EDBMS.SqlServer)
                        {
                            sqlFilter = string.Format("left({0:S},{1:D})='{2:S}'", fullPathFieldName, strFullPath.Length, strFullPath);
                        }
                        else
                        {
                            sqlFilter = string.Format("substr({0:S},1,{1:D})='{2:S}'", fullPathFieldName, strFullPath.Length, strFullPath);
                        }
                    }
                }
            }

            return sqlFilter;
        }

        /// <summary>
        /// 判断商品类型是否子叶。
        /// </summary>
        /// <param name="ProductCategoryID">商品类型ID</param>
        /// <returns></returns>
        public bool IsProductCategoryLeaf(int ProductCategoryID)
        {
            BSC_ProductCategory ProductCategory = GetProductCategory(ProductCategoryID);
            if (ProductCategory == null)
            {
                return false;
            }
            return ProductCategory.LeafFlag == (int)EYesNo.Yes ? true : false;
        }

        /// <summary>
        /// 获取商品类型HTML
        /// </summary>
        /// <returns></returns>
        public string GetProductCategoryHtml()
        {
            string ProductCategory1Html = String.Empty;

            string ProductCategory1FormatHtml =
                "<div class='ProductCategory'>" +
                "   <div class='ProductCategory1'>" +
                "       <p>{0:S}</p>" +
                "   </div>" +
                "   <div class='ProductCategory2'><ul>{1:S}</ul></div>" +
                "   <div class='clear'></div>" +
                "</div>";

            BSC_ProductCategoryExt cpxRootProductCategory = GetProductCategoryExt(0);
            foreach (BSC_ProductCategoryExt cpxProductCategory1 in cpxRootProductCategory.SubProductCategoryList)
            {
                string ProductCategory2Html = String.Empty;
                foreach (BSC_ProductCategoryExt cpxProductCategory2 in cpxProductCategory1.SubProductCategoryList)
                {
                    ProductCategory2Html += string.Format("<li><a href='#ProductCategory-{0:D}'>{1:S}</a></li>", cpxProductCategory2.ProductCategory.ProductCategoryID, cpxProductCategory2.ProductCategory.ProductCategoryName);
                }

                ProductCategory1Html += string.Format(ProductCategory1FormatHtml, cpxProductCategory1.ProductCategory.ProductCategoryName, ProductCategory2Html);
            }

            return ProductCategory1Html;
        }

        /// <summary>
        /// 获取商品品牌HTML
        /// </summary>
        /// <returns></returns>
        public string GetProductBrandHtml()
        {
            string ProductCategoryListHtml = String.Empty;
            ArrayList ProductBrandList = new ArrayList();

            string ProductBrandFormatHtml =
                "<li>" +
                "   <p class='BrandLogo'><a href='ProductList.aspx?ProductBrandID={0:D}'><img src='brandpic/mammy.jpg' /></a></p>" +
                "   <p class='BrandName'><a href='ProductList.aspx?ProductBrandID={0:D}'>{1:S}</a></p>" +
                "</li>";

            string ProductCategoryFormatHtml =
                "<li class='Category {2:S}'>" +
                "   <p class='CategoryTitle'><a id='ProductCategory-{0:D}' href='ProductList.aspx?ProductCategoryID={0:D}'>{1:S}</a></p>" +
                "   <div class='BrandList'>"+
                "       <ul>{3:S}</ul>"+
                "       <div class='clear'></div>"+
                "   </div>" +
                "</li>";

            int CategoryIndex = 1;
            BSC_ProductCategoryExt cpxRootProductCategory = GetProductCategoryExt(0);
            foreach (BSC_ProductCategoryExt cpxProductCategory1 in cpxRootProductCategory.SubProductCategoryList)
            {
                foreach (BSC_ProductCategoryExt cpxProductCategory2 in cpxProductCategory1.SubProductCategoryList)
                {
                    ProductBrandList.Clear();

                    ProductBrandList.AddRange(cpxProductCategory2.AssoProductBrandList);

                    foreach (BSC_ProductCategoryExt cpxProductCategory3 in cpxProductCategory2.SubProductCategoryList)
                    {
                        foreach (BSC_ProductBrandExt cpxProductBrand in cpxProductCategory3.AssoProductBrandList)
                        {
                            if (!ProductBrandList.Contains(cpxProductBrand))
                            {
                                ProductBrandList.Add(cpxProductBrand);
                            }
                        }
                    }

                    if (ProductBrandList.Count == 0)
                    {
                        continue;
                    }

                    string ProductBrandListHtml = String.Empty;
                    foreach (BSC_ProductBrandExt cpxProductBrand in ProductBrandList)
                    {
                        ProductBrandListHtml += string.Format(ProductBrandFormatHtml, cpxProductBrand.ProductBrand.ProductBrandID, cpxProductBrand.ProductBrand.ProductBrandName);
                    }

                    string AlternateClass = CategoryIndex % 2 == 0 ? "Even" : "Odd";
                    ProductCategoryListHtml += string.Format(ProductCategoryFormatHtml, cpxProductCategory2.ProductCategory.ProductCategoryID, cpxProductCategory2.ProductCategory.ProductCategoryName, AlternateClass, ProductBrandListHtml);

                    CategoryIndex++;
                }
            }

            return ProductCategoryListHtml;
        }

        public string GetProductCategoryTreeHtml(BSC_ProductCategoryExt cpxProductCategory)
        {
            int ProductCategoryID;
            string ProductCategoryName;
            string strHtml = String.Empty, strSubHtml, strFormatHtml;

            if (cpxProductCategory == null)
            {
                cpxProductCategory = GetProductCategoryExt(0);
                strHtml += "<ul id='ProductCategoryTree' class='filetree'>";
            }
            else
            {
                strHtml += "<ul>";
            }

            foreach (BSC_ProductCategoryExt cpxChildProductCategory in cpxProductCategory.SubProductCategoryList)
            {
                ProductCategoryID = cpxChildProductCategory.ProductCategory.ProductCategoryID;
                ProductCategoryName = cpxChildProductCategory.ProductCategory.ProductCategoryName;
                if (cpxChildProductCategory.ProductCategory.LeafFlag == 1 || cpxChildProductCategory.SubProductCategoryList.Count == 0)
                {
                    strFormatHtml = "<li id='pc{0:D}'><span style='cursor:pointer;' onclick='OnSelectProductCategory({0:D});'>{1:S}</span></li>";
                    strHtml += string.Format(strFormatHtml, ProductCategoryID, ProductCategoryName);
                }
                else
                {
                    strSubHtml = GetProductCategoryTreeHtml(cpxChildProductCategory);
                    strFormatHtml = "<li id='pc{0:D}'><span onclick='OnSelectProductCategory({0:D});'>{1:S}</span>{2:S}</li>";
                    strHtml += string.Format(strFormatHtml, ProductCategoryID, ProductCategoryName, strSubHtml);
                }
            }
            strHtml += "</ul>";

            return strHtml;
        }
    }
}
