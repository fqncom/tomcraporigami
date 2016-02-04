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
using Agape.Manage.Core.Util;
using Agape.Manage.Core.Common;
using Agape.Manage.Core.Impl;
using Agape.Manage.Core.Cache;
using Agape.Manage.Core.Session;

public partial class ProductService : BaseServicePage
{
    #region 商品服务
    /// <summary>
    /// 查询商品临时数量。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryProductTempCount()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string ProductNo, ProductName, FromEditDate, ToEditDate;
        int ProductCategoryID, ProductBrandID, EditOperatorID, AuditOperatorID, Status;

        GetStringParameter("ProductNo", String.Empty, out ProductNo);
        GetStringParameter("ProductName", String.Empty, out ProductName);
        GetIntParameter("ProductCategoryID", 0, out ProductCategoryID);
        GetIntParameter("ProductBrandID", 0, out ProductBrandID);
        GetIntParameter("EditOperatorID", 0, out EditOperatorID);
        GetIntParameter("AuditOperatorID", 0, out AuditOperatorID);
        GetStringParameter("FromEditDate", String.Empty, out FromEditDate);
        GetStringParameter("ToEditDate", String.Empty, out ToEditDate);
        GetIntParameter("Status", 0, out Status);

        xSubReturn = ProductImpl.QueryProductTempCount(ProductNo, ProductName, ProductCategoryID, ProductBrandID, EditOperatorID, AuditOperatorID, FromEditDate, ToEditDate, Status);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        xReturn.SetValue("RowCount", xSubReturn.ReturnValue);
        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询商品临时列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryProductTempList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string ProductNo, ProductName, FromEditDate, ToEditDate;
        int ProductCategoryID, ProductBrandID, EditOperatorID, AuditOperatorID, Status;
        int PageIndex, PageCount = 0, RowCount = 0, PageSize, StartIndex;

        GetStringParameter("ProductNo", String.Empty, out ProductNo);
        GetStringParameter("ProductName", String.Empty, out ProductName);
        GetIntParameter("ProductCategoryID", 0, out ProductCategoryID);
        GetIntParameter("ProductBrandID", 0, out ProductBrandID);
        GetIntParameter("EditOperatorID", 0, out EditOperatorID);
        GetIntParameter("AuditOperatorID", 0, out AuditOperatorID);
        GetStringParameter("FromEditDate", String.Empty, out FromEditDate);
        GetStringParameter("ToEditDate", String.Empty, out ToEditDate);
        GetIntParameter("Status", 0, out Status);
        GetIntParameter("PageIndex", 1, out PageIndex);
        GetIntParameter("PageSize", 10, out PageSize);

        xSubReturn = ProductImpl.QueryProductTempCount(ProductNo, ProductName, ProductCategoryID, ProductBrandID, EditOperatorID, AuditOperatorID, FromEditDate, ToEditDate, Status);
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

            xSubReturn = ProductImpl.QueryProductTempList(ProductNo, ProductName, ProductCategoryID, ProductBrandID, EditOperatorID, AuditOperatorID, FromEditDate, ToEditDate, Status, StartIndex, PageSize, out dt);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            ExportXmlAsAttribute(m_XmlTextWriter, dt, "ProductTemp");
        }

        xReturn.SetValue("RowCount", RowCount);
        xReturn.SetValue("PageCount", PageCount);

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询商品数量。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryProductCount()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string ProductNo, ProductName;
        int ProductCategoryID, ProductBrandID;

        GetStringParameter("ProductNo", String.Empty, out ProductNo);
        GetStringParameter("ProductName", String.Empty, out ProductName);
        GetIntParameter("ProductCategoryID", 0, out ProductCategoryID);
        GetIntParameter("ProductBrandID", 0, out ProductBrandID);

        xSubReturn = ProductImpl.QueryProductCount(ProductNo, ProductName, ProductCategoryID, ProductBrandID, 0, 0);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        xReturn.SetValue("RowCount", xSubReturn.ReturnValue);
        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询商品列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryProductList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string ProductNo, ProductName;
        int ProductCategoryID, ProductBrandID, PageIndex, PageCount, RowCount, PageSize, StartIndex;

        GetStringParameter("ProductNo", String.Empty, out ProductNo);
        GetStringParameter("ProductName", String.Empty, out ProductName);
        GetIntParameter("ProductCategoryID", 0, out ProductCategoryID);
        GetIntParameter("ProductBrandID", 0, out ProductBrandID);
        GetIntParameter("PageIndex", 1, out PageIndex);
        GetIntParameter("PageSize", 10, out PageSize);

        xSubReturn = ProductImpl.QueryProductCount(ProductNo, ProductName, ProductCategoryID, ProductBrandID, 0, 0);
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

            xSubReturn = ProductImpl.QueryProductList(ProductNo, ProductName, ProductCategoryID, ProductBrandID, 0, 0, StartIndex, PageSize, out dt);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn);
            }

            ExportXmlAsAttribute(m_XmlTextWriter, dt, "Product");
        }
        else
        {
            PageCount = 0;
        }

        xReturn.SetValue("RowCount", RowCount);
        xReturn.SetValue("PageCount", PageCount);

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询商品。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryProduct()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_Product Product = new BSC_Product();
        BSC_ProductSpec ProductSpec = new BSC_ProductSpec();
        BSC_ProductBrand ProductBrand;
        BSC_ProductCategory ProductCategory;
        int ProductID;
        string ProductNo, BarCode;
        bool CheckTempFlag;

        GetIntParameter("ProductID", 0, out ProductID);
        GetStringParameter("ProductNo", String.Empty, out ProductNo);
        GetStringParameter("BarCode", String.Empty, out BarCode);
        GetBooleanParameter("CheckTempFlag", false, out CheckTempFlag);

        // 查询商品对象。
        ArrayList Entities;
        Hashtable FilterValues = new Hashtable();
        if (ProductID > 0) FilterValues["ProductID"] = ProductID;
        if (ProductNo != String.Empty) FilterValues["ProductNo"] = ProductNo;
        if (BarCode != String.Empty) FilterValues["BarCode"] = BarCode;
        if (FilterValues.Count == 0)
        {
            return xReturn.ReturnError("请指定查询条件");
        }

        xSubReturn = BaseEntityEx.MSelect("BSC_Product", FilterValues, out Entities);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }
        if (Entities.Count != 1)
        {
            return xReturn.ReturnError("没有找到商品");
        }
        Product.x.CopyFrom((BaseEntity)Entities[0]);

        // 查询默认商品规格对象。
        ProductSpec.ProductSpecID = Product.DefaultProductSpecID;
        xSubReturn = ProductSpec.x.SelectByIdentity();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        ProductCategory = ProductCategoryCache.Current.GetProductCategory(Product.ProductCategoryID);
        ProductBrand = ProductBrandCache.Current.GetProductBrand(Product.ProductBrandID);

        if (CheckTempFlag)
        {
            EntityQueryer qryProductTemp = new EntityQueryer("BSC_ProductTemp");
            qryProductTemp.AddFilter("ProductID", ProductID);
            qryProductTemp.AddFilter("Status", (int)EProductTempStatus.AuditBack);
            xSubReturn = qryProductTemp.Query();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品临时表失败");
            }

            if (qryProductTemp.EntityCount == 1)
            {
                BSC_ProductTemp ProductTemp = (BSC_ProductTemp)qryProductTemp.GetEntity(0);
                Product.ProductNo = ProductTemp.ProductNo;
                Product.ProductName = ProductTemp.ProductName;
                Product.ProductCategoryID = ProductTemp.ProductCategoryID;
                Product.ProductUnit = ProductTemp.ProductUnit;
                Product.MarketPrice = ProductTemp.MarketPrice;
                Product.SalesPrice = ProductTemp.SalesPrice;
                Product.PurchasePrice = ProductTemp.PurchasePrice;
                Product.FitAge = ProductTemp.FitAge;
                Product.BarCode = ProductTemp.BarCode;
                Product.Description = ProductTemp.Description;
            }
        }

        ExportXmlAsNode(m_XmlTextWriter, Product, "Product");
        ExportXmlAsNode(m_XmlTextWriter, ProductSpec, "ProductSpec");
        ExportXmlAsNode(m_XmlTextWriter, ProductCategory, "ProductCategory");
        ExportXmlAsNode(m_XmlTextWriter, ProductBrand, "ProductBrand");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存商品临时编辑记录。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveProductTemp()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int ProductID;
        string ProductBrandName;
        BSC_ProductTemp ProductTemp;
        BSC_ProductBrandExt ProductBrandExt;

        ProductID = GetIntParameter("ProductID", 0);
        if (ProductID == 0)
        {
            return xReturn.ReturnError("商品ID参数不存在");
        }

        // 检查是否存在待审核的商品编辑记录
        string strSQL = "select count(*) from BSC_ProductTemp where ProductID={0:D} and Status={1:D}";
        strSQL = string.Format(strSQL, ProductID, (int)EProductTempStatus.WaitAudit);
        xSubReturn = DatabaseFactory.GetCurrent().ExecuteScalar(strSQL);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询商品临时记录失败");
        }
        if (xSubReturn.GetIntValue() > 0)
        {
            return xReturn.ReturnError("存在待审核的商品临时记录");
        }

        // 检查是否存在审核退回的商品编辑记录
        EntityQueryer qryProductTemp = new EntityQueryer("BSC_ProductTemp");
        qryProductTemp.AddFilter("ProductID", ProductID);
        qryProductTemp.AddFilter("Status", (int)EProductTempStatus.AuditBack);
        xSubReturn = qryProductTemp.Query();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询商品临时表失败");
        }

        if (qryProductTemp.EntityCount == 1)
        {
            ProductTemp = (BSC_ProductTemp)qryProductTemp.GetEntity(0);
        }
        else
        {
            ProductTemp = new BSC_ProductTemp();
            ProductTemp.ProductID = ProductID;
        }

        // 获取商品品牌信息
        ProductBrandName = GetStringParameter("ProductBrand", String.Empty);
        ProductBrandExt = ProductBrandCache.Current.GetProductBrandByName(ProductBrandName);
        if (ProductBrandExt != null)
        {
            ProductTemp.ProductBrandID = ProductBrandExt.ProductBrand.ProductBrandID;
        }
        else
        {
            BSC_ProductBrand ProductBrand = new BSC_ProductBrand();
            ProductBrand.ProductBrandName = ProductBrandName;

            xSubReturn = ProductBrand.x.Insert();
            if (xSubReturn.IsUnSuccess())
            {
                return xSubReturn.ReturnError(xSubReturn, string.Format("插入商品品牌[{0:S}]失败", ProductBrand.ProductBrandName));
            }

            ProductTemp.ProductBrandID = ProductBrand.ProductBrandID;

            xSubReturn = ProductBrandCache.Current.AddProductBrand(ProductBrand);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, string.Format("增加商品品牌[{0:S}]到缓存失败", ProductBrand.ProductBrandName));
            }
        }

        ProductTemp.Status = (int)EProductTempStatus.WaitAudit;
        ProductTemp.ProductNo = GetStringParameter("ProductNo", String.Empty);
        ProductTemp.ProductName = GetStringParameter("ProductName", String.Empty);
        ProductTemp.ProductCategoryID = GetIntParameter("ProductCategoryID", 0);
        ProductTemp.ProductUnit = GetStringParameter("ProductUnit", String.Empty);
        ProductTemp.MarketPrice = GetDoubleParameter("MarketPrice", 0);
        ProductTemp.SalesPrice = GetDoubleParameter("SalesPrice", 0);
        ProductTemp.PurchasePrice = GetDoubleParameter("PurchasePrice", 0);
        ProductTemp.FitAge = GetStringParameter("FitAge", String.Empty);
        ProductTemp.BarCode = GetStringParameter("BarCode", String.Empty);
        ProductTemp.EditOperatorID = OperatorSession.Operator.OperatorID;
        ProductTemp.EditTime = DateTime.Now;
        string strTemp = GetStringParameter("Description", String.Empty);
        ProductTemp.Description = HttpUtility.UrlDecode(strTemp, ClientEncoding);

        xSubReturn = ProductImpl.SaveProductTemp(ProductTemp);

        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "保存待审核商品编辑记录失败");
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 通过商品临时编辑记录审核。
    /// </summary>
    /// <returns></returns>
    public XReturn AuditPassProduct()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_Product Product = new BSC_Product();
        BSC_ProductTemp ProductTemp = new BSC_ProductTemp();

        //  获取商品编辑临时ID
        ProductTemp.ProductTempID = GetIntParameter("ProductTempID", 0);
        if (ProductTemp.ProductTempID == 0)
        {
            return xReturn.ReturnError("待审核商品编辑ID参数不存在");
        }

        xSubReturn = ProductTemp.x.SelectByIdentity();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询商品待审核编辑记录失败");
        }

        Product.ProductID = ProductTemp.ProductID;
        xSubReturn = Product.x.SelectByIdentity();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询商品记录失败");
        }

        Product.ProductNo = ProductTemp.ProductNo;
        Product.ProductName = ProductTemp.ProductName;
        Product.WordKey = ProductTemp.WordKey;
        Product.ProductCategoryID = ProductTemp.ProductCategoryID;
        Product.ProductBrandID = ProductTemp.ProductBrandID;
        Product.SalesPrice = ProductTemp.SalesPrice;
        Product.Description = ProductTemp.Description;

        xSubReturn = Product.x.UpdateByIdentity();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "更新商品记录失败");
        }

        ProductTemp.AuditOperatorID = OperatorSession.Operator.OperatorID;
        ProductTemp.AuditTime = DateTime.Now;
        ProductTemp.Remark = String.Empty;
        ProductTemp.Status = (int)EProductTempStatus.AuditPass;
        xSubReturn = ProductTemp.x.UpdateByIdentity();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "更新商品临时记录失败");
        }

        xSubReturn = ProductImpl.RefreshProductCache(Product.ProductID);

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 退回商品临时编辑记录。
    /// </summary>
    /// <returns></returns>
    public XReturn AuditBackProduct()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();

        BSC_ProductTemp ProductTemp = new BSC_ProductTemp();

        ProductTemp.ProductTempID = GetIntParameter("ProductTempID", 0);
        if (ProductTemp.ProductTempID == 0)
        {
            return xReturn.ReturnError("待审核商品编辑ID参数不存在");
        }
        ProductTemp.Remark = GetStringParameter("Remark", String.Empty);

        ProductTemp.AuditOperatorID = OperatorSession.Operator.OperatorID;
        ProductTemp.AuditTime = DateTime.Now;
        ProductTemp.Status = (int)EProductTempStatus.AuditBack;
        xSubReturn = ProductTemp.x.UpdateByIdentity();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "退回商品临时记录失败");
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存商品。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveProduct()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string ProductBrandName;
        BSC_Product Product = new BSC_Product();
        BSC_ProductSpec ProductSpec = new BSC_ProductSpec();
        BSC_ProductBrandExt cpxProductBrand;

        Product.ProductID = GetIntParameter("ProductID", 0);
        if (Product.ProductID > 0)
        {
            xSubReturn = Product.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品失败");
            }

            ProductSpec.ProductSpecID = Product.DefaultProductSpecID;

            xSubReturn = ProductSpec.x.SelectByIdentity();
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品默认规格失败");
            }
        }
        else
        {
            Product.Status = (int)EProductStatus.New;
        }

        // 获取商品品牌信息。
        ProductBrandName = GetStringParameter("ProductBrand", String.Empty);
        cpxProductBrand = ProductBrandCache.Current.GetProductBrandByName(ProductBrandName);
        if (cpxProductBrand != null)
        {
            Product.ProductBrandID = cpxProductBrand.ProductBrand.ProductBrandID;
        }
        else
        {
            BSC_ProductBrand ProductBrand = new BSC_ProductBrand();
            ProductBrand.ProductBrandName = ProductBrandName;

            xSubReturn = ProductBrand.x.Insert();
            if (xSubReturn.IsUnSuccess())
            {
                return xSubReturn.ReturnError(xSubReturn, string.Format("插入商品品牌[{0:S}]失败", ProductBrand.ProductBrandName));
            }

            Product.ProductBrandID = ProductBrand.ProductBrandID;

            xSubReturn = ProductBrandCache.Current.AddProductBrand(ProductBrand);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, string.Format("增加商品品牌[{0:S}]到缓存失败", ProductBrand.ProductBrandName));
            }
        }

        Product.ProductNo = GetStringParameter("ProductNo", String.Empty);
        Product.ProductName = GetStringParameter("ProductName", String.Empty);
        Product.ProductCategoryID = GetIntParameter("ProductCategoryID", 0);
        Product.ProductUnit = GetStringParameter("ProductUnit", String.Empty);
        Product.MarketPrice = GetDoubleParameter("MarketPrice", 0);
        Product.SalesPrice = GetDoubleParameter("SalesPrice", 0);
        Product.PurchasePrice = GetDoubleParameter("PurchasePrice", 0);
        Product.FitAge = GetStringParameter("FitAge", String.Empty);
        Product.BarCode = GetStringParameter("BarCode", String.Empty);
        string strTemp = GetStringParameter("Description", String.Empty);
        Product.Description = HttpUtility.UrlDecode(strTemp, ClientEncoding);

        ProductSpec.ProductSpec = GetStringParameter("ProductSpec", String.Empty);

        xSubReturn = ProductImpl.SaveProduct(Product, ProductSpec);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "保存商品失败");
        }

        //ProductPageContentCache.Current.RemoveProductPageContent(Product.ProductID);

        xSubReturn = ProductCategoryCache.Current.AddAssoProductBrand(Product.ProductCategoryID, Product.ProductBrandID);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, string.Format("增加商品类型[{0:D}]关联商品品牌[{1:D}]缓存失败", Product.ProductCategoryID, Product.ProductBrandID));
        }

        xReturn.SetValue("ProductID", Product.ProductID);
        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 导入商品。
    /// </summary>
    /// <returns></returns>
    public XReturn ImportProduct()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_Product Product = new BSC_Product();
        BSC_ProductSpec ProductSpec = new BSC_ProductSpec();
        BSC_ProductCategoryExt cpxProductCategory;
        BSC_ProductBrandExt cpxProductBrand;

        // 获取商品类型信息。
        string ProductCategoryName = GetStringParameter("ProductCategory", String.Empty);
        string ProductBrandName = GetStringParameter("ProductBrand", String.Empty);
        cpxProductCategory = ProductCategoryCache.Current.GetProductCategoryExtByName(ProductCategoryName);
        if (cpxProductCategory == null)
        {
            return xReturn.ReturnError(string.Format("找不到商品类型[{0:S}]", ProductCategoryName));
        }
        Product.ProductCategoryID = cpxProductCategory.ProductCategory.ProductCategoryID;

        // 获取商品品牌信息。
        cpxProductBrand = ProductBrandCache.Current.GetProductBrandByName(ProductBrandName);
        if (cpxProductBrand != null)
        {
            Product.ProductBrandID = cpxProductBrand.ProductBrand.ProductBrandID;
        }
        else
        {
            BSC_ProductBrand ProductBrand = new BSC_ProductBrand();
            ProductBrand.ProductBrandName = ProductBrandName;

            xSubReturn = ProductBrand.x.Insert();
            if (xSubReturn.IsUnSuccess())
            {
                return xSubReturn.ReturnError(xSubReturn, string.Format("插入商品品牌[{0:S}]失败", ProductBrand.ProductBrandName));
            }

            Product.ProductBrandID = ProductBrand.ProductBrandID;

            xSubReturn = ProductBrandCache.Current.AddProductBrand(ProductBrand);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, string.Format("增加商品品牌[{0:S}]到缓存失败", ProductBrand.ProductBrandName));
            }
        }

        Product.ProductNo = GetStringParameter("ProductNo", String.Empty);
        xSubReturn = Product.x.Select();
        if (xSubReturn.IsSuccess())
        {
            return xReturn.ReturnError("商品已经存在");
        }

        Product.Status = (int)EProductStatus.New;
        Product.ProductName = GetStringParameter("ProductName", String.Empty);
        Product.ProductUnit = GetStringParameter("ProductUnit", String.Empty);
        Product.BarCode = GetStringParameter("BarCode", String.Empty);
        Product.MarketPrice = GetDoubleParameter("MarketPrice", 0);
        Product.SalesPrice = GetDoubleParameter("SalesPrice", 0);
        Product.PurchasePrice = GetDoubleParameter("PurchasePrice", 0);
        Product.FitAge = GetStringParameter("FitAge", String.Empty);
        Product.BarCode = GetStringParameter("BarCode", String.Empty);
        Product.Description = GetStringParameter("Description", String.Empty);

        ProductSpec.ProductSpec = GetStringParameter("ProductSpec", String.Empty);

        xSubReturn = ProductImpl.SaveProduct(Product, ProductSpec);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "保存商品失败");
        }

        xSubReturn = ProductCategoryCache.Current.AddAssoProductBrand(Product.ProductCategoryID, Product.ProductBrandID);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, string.Format("增加商品类型[{0:D}]关联商品品牌[{1:D}]缓存失败", Product.ProductCategoryID, Product.ProductBrandID));
        }

        xReturn.SetValue("ProductID", Product.ProductID);
        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 删除商品。
    /// </summary>
    /// <returns></returns>
    public XReturn DeleteProduct()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_Product Product = new BSC_Product();

        int ProductID = GetIntParameter("ProductID", 0);

        xSubReturn = ProductImpl.DeleteProduct(ProductID);
        if (xSubReturn.IsUnSuccess())
        {
            xReturn.ReturnError(xSubReturn);
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 刷新商品缓存
    /// </summary>
    /// <returns></returns>
    public XReturn RefreshProductCache()
    {
        XReturn xReturn = new XReturn();

        int ProductID = GetIntParameter("ProductID", 0);
        if (ProductID <= 0)
        {
            return xReturn.ReturnError("商品ID参数未定义");
        }

        string strUrl = string.Format("http://www.ibaby361.com/RefreshCache.aspx?CacheCode=Product&&ProductID={0:D}", ProductID);

        string strResp = WebUtil.PostWebRequest(strUrl);

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 商品规格服务
    /// <summary>
    /// 查询商品规格列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryProductSpecList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int ProductID;

        if (!GetIntParameter("ProductID", 0, out ProductID))
        {
            return xReturn.ReturnError("没有指定商品ID");
        }

        xSubReturn = ProductImpl.QueryProductSpecList(ProductID, String.Empty, String.Empty, out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        ExportXmlAsAttribute(m_XmlTextWriter, dt, "ProductSpec");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存商品规格。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveProductSpec()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_ProductSpec ProductSpec = new BSC_ProductSpec();

        ProductSpec.ProductSpecID = GetIntParameter("ProductSpecID", 0);
        ProductSpec.ProductID = GetIntParameter("ProductID", 0);
        ProductSpec.ProductSpec = GetStringParameter("ProductSpec", String.Empty);
        ProductSpec.StockInitQuantity = GetIntParameter("StockInitQuantity", 0);
        ProductSpec.StockLowerLimit = GetIntParameter("StockLowerLimit", 0);
        ProductSpec.StockUpperLimit = GetIntParameter("StockUpperLimit", 0);
        ProductSpec.DefaultFlag = GetIntParameter("DefaultFlag", 0);
        ProductSpec.Remark = GetStringParameter("Remark", String.Empty);

        xSubReturn = ProductImpl.SaveProductSpec(ProductSpec);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        xReturn.SetValue("ProductSpecID", ProductSpec.ProductSpecID);
        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 删除商品规格。
    /// </summary>
    /// <returns></returns>
    public XReturn DeleteProductSpec()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int ProductSpecID;

        if (!GetIntParameter("ProductSpecID", 0, out ProductSpecID))
        {
            return xReturn.ReturnError("没有指定商品规格ID");
        }

        xSubReturn = ProductImpl.DeleteProductSpec(ProductSpecID);
        if (xSubReturn.IsUnSuccess())
        {
            xReturn.ReturnError(xSubReturn, "删除商品规格失败");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 商品图片服务
    /// <summary>
    /// 查询商品图片列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryProductPictureList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int ProductID;

        if (!GetIntParameter("ProductID", 0, out ProductID))
        {
            return xReturn.ReturnError("没有指定商品ID");
        }

        xSubReturn = ProductImpl.QueryProductPictureList(ProductID, String.Empty, out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        ExportXmlAsAttribute(m_XmlTextWriter, dt, "ProductPicture");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存商品图片。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveProductPicture()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_ProductPicture ProductPicture = new BSC_ProductPicture();

        ProductPicture.ProductPictureID = GetIntParameter("ProductPictureID", 0);
        ProductPicture.ProductID = GetIntParameter("ProductID", 0);
        ProductPicture.FileName = GetStringParameter("FileName", String.Empty);
        ProductPicture.FileType = GetStringParameter("FileType", String.Empty);
        ProductPicture.OrderNo = GetIntParameter("OrderNo", 0);
        ProductPicture.Title = GetStringParameter("Title", String.Empty);
        ProductPicture.Remark = GetStringParameter("Remark", String.Empty);

        xSubReturn = ProductImpl.SaveProductPicture(ProductPicture);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        xReturn.SetValue("ProductPictureID", ProductPicture.ProductPictureID);
        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 删除商品图片。
    /// </summary>
    /// <returns></returns>
    public XReturn DeleteProductPicture()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int ProductPictureID;

        if (!GetIntParameter("ProductPictureID", 0, out ProductPictureID))
        {
            return xReturn.ReturnError("没有指定商品图片ID");
        }

        xSubReturn = ProductImpl.DeleteProductPicture(ProductPictureID);
        if (xSubReturn.IsUnSuccess())
        {
            xReturn.ReturnError(xSubReturn, "删除商品图片失败");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 商品品牌服务
    /// <summary>
    /// 查询商品品牌列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryProductBrandList()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int ProductCategoryID;
        string ProductBrandCode, ProductBrandName;
        int StartIndex, PageIndex, PageSize, PageCount = 0, RowCount = 0;
        bool StateFlag = false;

        GetIntParameter("ProductCategoryID", 0, out ProductCategoryID);
        GetBooleanParameter("StateFlag", true, out StateFlag);
        GetStringParameter("ProductBrandCode", String.Empty, out ProductBrandCode);
        GetStringParameter("ProductBrandName", String.Empty, out ProductBrandName);
        GetIntParameter("PageIndex", 1, out PageIndex);
        GetIntParameter("PageSize", 10, out PageSize);

        if (StateFlag)
        {
            ArrayList ProductBrandList = ProductBrandCache.Current.GetProductBrandList();

            RowCount = ProductBrandList.Count;

            foreach (BSC_ProductBrand ProductBrand in ProductBrandList)
            {
                ExportXmlAsNode(m_XmlTextWriter, ProductBrand, "ProductBrand");
            }
        }
        else
        {
            DataTable dt;
            xSubReturn = ProductImpl.QueryProductBrandCount(ProductBrandCode, ProductBrandName);
            if (xSubReturn.IsUnSuccess())
            {
                return xReturn.ReturnError(xSubReturn, "查询商品品牌数量失败");
            }

            RowCount = xSubReturn.GetIntValue();

            if (RowCount > 0)
            {
                PageCount = GenericUtil.CalculatePageCount(RowCount, PageSize);
                if (PageIndex > PageCount) PageIndex = PageCount;
                StartIndex = (PageIndex - 1) * PageSize;

                xSubReturn = ProductImpl.QueryProductBrandList(ProductBrandCode, ProductBrandName, StartIndex, PageSize, out dt);
                if (xSubReturn.IsUnSuccess())
                {
                    return xReturn.ReturnError(xSubReturn, "查询商品品牌列表失败");
                }

                ExportXmlAsAttribute(m_XmlTextWriter, dt, "ProductBrand");
            }
        }

        xReturn.SetValue("RowCount", RowCount);
        xReturn.SetValue("PageCount", PageCount);

        return xReturn.ReturnSuccess();
    }
    #endregion
}
