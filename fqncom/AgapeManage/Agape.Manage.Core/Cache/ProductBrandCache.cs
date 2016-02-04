using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Leopard.Util;
using Leopard.Data;
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Impl;

namespace Agape.Manage.Core.Cache
{
    public class BSC_ProductBrandExt
    {
        public BSC_ProductBrand ProductBrand;
        public ArrayList AssoProductCategoryList;

        public BSC_ProductBrandExt()
        {
            ProductBrand = new BSC_ProductBrand();
            AssoProductCategoryList = new ArrayList();
        }
    }

    public class ProductBrandCache
    {
        #region 静态部分
        private static ProductBrandCache m_Instance;

        static ProductBrandCache()
        {
            m_Instance = null;
        }

        public static ProductBrandCache Current
        {
            get { return GetInstance(); }
        }

        public static ProductBrandCache GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new ProductBrandCache();
            }
            return m_Instance;
        }
        #endregion

        private Hashtable m_ProductBrandExts;


        private ProductBrandCache()
        {
        }

        /// <summary>
        /// 导入商品类型数据
        /// </summary>
        public XReturn LoadCache()
        {
            XReturn xSubReturn;
            XReturn xReturn = new XReturn();

            m_ProductBrandExts = new Hashtable();

            BSC_ProductBrandExt _ProductBrandExt;
            DataTable _ProductBrandTable;

            xSubReturn = ProductImpl.QueryProductBrandList(String.Empty, String.Empty, 0, 0, out _ProductBrandTable);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品品牌失败");
            }

            foreach (DataRow dr in _ProductBrandTable.Rows)
            {
                _ProductBrandExt = new BSC_ProductBrandExt();
                _ProductBrandExt.ProductBrand.x.CopyFrom(dr);
                _ProductBrandExt.ProductBrand.x.TrimEntity();
                m_ProductBrandExts.Add(_ProductBrandExt.ProductBrand.ProductBrandID, _ProductBrandExt);
            }

            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 查询商品品牌扩展对象。
        /// </summary>
        /// <param name="ProductBrandID">商品类型ID</param>
        /// <returns></returns>
        public BSC_ProductBrandExt GetProductBrandExt(int ProductBrandID)
        {
            if (m_ProductBrandExts.Contains(ProductBrandID))
            {
                return m_ProductBrandExts[ProductBrandID] as BSC_ProductBrandExt;
            }

            return null;
        }

        /// <summary>
        /// 查询商品类型对象。
        /// </summary>
        /// <param name="ProductBrandID">商品类型ID</param>
        /// <returns></returns>
        public BSC_ProductBrand GetProductBrand(int ProductBrandID)
        {
            if (m_ProductBrandExts.Contains(ProductBrandID))
            {
                return ((BSC_ProductBrandExt)m_ProductBrandExts[ProductBrandID]).ProductBrand;
            }

            return null;
        }

        /// <summary>
        /// 获取商品品牌名称。
        /// </summary>
        /// <param name="ProductBrandID">商品品牌ID</param>
        /// <returns>返回商品品牌名称</returns>
        public string GetProductBrandName(int ProductBrandID)
        {
            BSC_ProductBrand ProductBrand = GetProductBrand(ProductBrandID);
            if (ProductBrand == null)
            {
                return String.Empty;
            }
            else
            {
                return ProductBrand.ProductBrandName;
            }
        }

        /// <summary>
        /// 查询复合商品类型对象。
        /// </summary>
        /// <param name="ProductBrandName">商品类型名称</param>
        /// <returns></returns>
        public BSC_ProductBrandExt GetProductBrandByName(string ProductBrandName)
        {
            BSC_ProductBrandExt cpxProductBrand;

            foreach (int id in m_ProductBrandExts.Keys)
            {
                cpxProductBrand = (BSC_ProductBrandExt)m_ProductBrandExts[id];
                if (cpxProductBrand.ProductBrand.ProductBrandName == ProductBrandName)
                {
                    return cpxProductBrand;
                }
            }
            return null;
        }

        /// <summary>
        /// 添加商品品牌。
        /// </summary>
        /// <param name="ProductBrand">商品品牌对象</param>
        /// <returns></returns>
        public XReturn AddProductBrand(BSC_ProductBrand ProductBrand)
        {
            XReturn xReturn = new XReturn();

            BSC_ProductBrandExt cpxProductBrand = GetProductBrandExt(ProductBrand.ProductBrandID);
            if (cpxProductBrand != null)
            {
                return xReturn.ReturnSuccess();
            }

            cpxProductBrand = new BSC_ProductBrandExt();
            cpxProductBrand.ProductBrand.x.CopyFrom(ProductBrand);
            m_ProductBrandExts[ProductBrand.ProductBrandID] = cpxProductBrand;
            
            return xReturn.ReturnSuccess();
        }

        /// <summary>
        /// 获取商品品牌列表。
        /// </summary>
        /// <returns></returns>
        public ArrayList GetProductBrandList()
        {
            ArrayList ProductBrandList = new ArrayList();
            foreach (int ProductBrandID in m_ProductBrandExts.Keys)
            {
                BSC_ProductBrandExt cpxProductBrand = (BSC_ProductBrandExt)m_ProductBrandExts[ProductBrandID];
                ProductBrandList.Add(cpxProductBrand.ProductBrand);
            }
            return ProductBrandList;
        }
    }
}
