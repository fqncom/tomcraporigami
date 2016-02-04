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

public partial class ConfigService : BaseServicePage
{
    #region 兑换商品服务
    /// <summary>
    /// 查询兑换商品列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryExchangeProductList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string ProductNo, ProductName;

        GetStringParameter("ProductNo", String.Empty, out ProductNo);
        GetStringParameter("ProductName", String.Empty, out ProductName);

        xSubReturn = ConfigImpl.QueryExchangeProductList(ProductNo, ProductName, out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        ExportXmlAsAttribute(m_XmlTextWriter, dt, "ExchangeProduct");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存兑换商品。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveExchangeProduct()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_ExchangeProduct ExchangeProduct = new BSC_ExchangeProduct();

        ExchangeProduct.ProductID = GetIntParameter("ProductID", 0);
        ExchangeProduct.PointValue = GetIntParameter("PointValue", 0);

        xSubReturn = ConfigImpl.SaveExchangeProduct(ExchangeProduct);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 删除兑换商品。
    /// </summary>
    /// <returns></returns>
    public XReturn DeleteExchangeProduct()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int ProductID;

        if (!GetIntParameter("ProductID", 0, out ProductID))
        {
            return xReturn.ReturnError("没有指定兑换商品ID");
        }

        xSubReturn = ConfigImpl.DeleteExchangeProduct(ProductID);
        if (xSubReturn.IsUnSuccess())
        {
            xReturn.ReturnError(xSubReturn, "删除兑换商品失败");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 限购商品服务
    /// <summary>
    /// 查询限购商品列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryLimitSalesProductList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        string ProductNo, ProductName;

        GetStringParameter("ProductNo", String.Empty, out ProductNo);
        GetStringParameter("ProductName", String.Empty, out ProductName);

        xSubReturn = ConfigImpl.QueryLimitSalesProductList(ProductNo, ProductName, out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        ExportXmlAsAttribute(m_XmlTextWriter, dt, "LimitSalesProduct");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存限购商品。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveLimitSalesProduct()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_Product Product = new BSC_Product();
        BSC_LimitSalesProduct LimitSalesProduct = new BSC_LimitSalesProduct();
        string strTemp;
        DateTime dtmTemp;

        LimitSalesProduct.LimitSalesProductID = GetIntParameter("LimitSalesProductID", 0);
        LimitSalesProduct.ProductID = GetIntParameter("ProductID", 0);

        LimitSalesProduct.LimitSalesPrice = GetDoubleParameter("LimitSalesPrice", 0);
        if (LimitSalesProduct.LimitSalesPrice == 0)
        {
            return xReturn.ReturnError("限购金额不能等于零");
        }

        // 获取商品对象
        Product.ProductID = LimitSalesProduct.ProductID;
        xSubReturn = Product.x.SelectByIdentity();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "商品不存在");
        }
        LimitSalesProduct.SalesPrice = Product.SalesPrice;
        LimitSalesProduct.Agio = Math.Round(LimitSalesProduct.LimitSalesPrice / LimitSalesProduct.SalesPrice, 2);

        LimitSalesProduct.Title = GetStringParameter("Title", String.Empty);
        LimitSalesProduct.RecommendOrder = GetIntParameter("RecommendOrder", 0);
        LimitSalesProduct.RecommendTitle = GetStringParameter("RecommendTitle", String.Empty);
        LimitSalesProduct.RecommendBrief = GetStringParameter("RecommendBrief", String.Empty);
        LimitSalesProduct.WeightValue = GetIntParameter("WeightValue", 0);

        strTemp = GetStringParameter("BeginTime", String.Empty);
        if (!ConvertUtil.SafeToDateTime(strTemp, out dtmTemp))
        {
            return xReturn.ReturnError("开始时间格式错误");
        }
        LimitSalesProduct.BeginTime = dtmTemp;

        strTemp = GetStringParameter("EndTime", String.Empty);
        if (!ConvertUtil.SafeToDateTime(strTemp, out dtmTemp))
        {
            return xReturn.ReturnError("结束时间格式错误");
        }
        LimitSalesProduct.EndTime = dtmTemp;

        xSubReturn = ConfigImpl.SaveLimitSalesProduct(LimitSalesProduct);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn);
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 删除限购商品。
    /// </summary>
    /// <returns></returns>
    public XReturn DeleteLimitSalesProduct()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int LimitSalesProductID;

        if (!GetIntParameter("LimitSalesProductID", 0, out LimitSalesProductID))
        {
            return xReturn.ReturnError("没有指定限购商品ID");
        }

        xSubReturn = ConfigImpl.DeleteLimitSalesProduct(LimitSalesProductID);
        if (xSubReturn.IsUnSuccess())
        {
            xReturn.ReturnError(xSubReturn, "删除限购商品失败");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 积分配置服务
    /// <summary>
    /// 查询积分配置。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryPointConfig()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();

        xSubReturn = ConfigImpl.QueryPointConfig();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询积分配置失败");
        }
        BSC_PointConfig PointConfig = (BSC_PointConfig)xSubReturn.ReturnValue;

        ExportXmlAsAttribute(m_XmlTextWriter, PointConfig, "PointConfig");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存积分配置。
    /// </summary>
    /// <returns></returns>
    public XReturn SavePointConfig()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_PointConfig PointConfig = new BSC_PointConfig();

        PointConfig.PointMode = 1;
        PointConfig.PointValue = GetIntParameter("PointValue", 0);
        PointConfig.MinAmountToPoint = GetIntParameter("MinAmountToPoint", 0);

        xSubReturn = PointConfig.x.Update();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "保存积分配置失败");
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询商品类型积分配置列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryProductCategoryPointConfigList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();

        xSubReturn = ConfigImpl.QueryProductCategoryPointConfigList(out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询商品类型积分配置列表失败");
        }

        ExportXmlAsAttribute(m_XmlTextWriter, dt, "ProductCategoryPointConfig");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存商品类型积分配置。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveProductCategoryPointConfig()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_ProductCategoryPointConfig ProductCategoryPointConfig = new BSC_ProductCategoryPointConfig();

        ProductCategoryPointConfig.ProductCategoryID = GetIntParameter("ProductCategoryID", 0);
        ProductCategoryPointConfig.PointMode = 1;
        ProductCategoryPointConfig.PointValue = GetIntParameter("PointValue", 0);

        xSubReturn = ProductCategoryPointConfig.x.Insert();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "保存商品类型积分配置失败");
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 删除商品类型积分配置。
    /// </summary>
    /// <returns></returns>
    public XReturn DeleteProductCategoryPointConfig()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int ProductCategoryID;

        if (!GetIntParameter("ProductCategoryID", 0, out ProductCategoryID))
        {
            return xReturn.ReturnError("没有指定商品类型ID");
        }

        BSC_ProductCategoryPointConfig ProductCategoryPointConfig = new BSC_ProductCategoryPointConfig();
        ProductCategoryPointConfig.ProductCategoryID = ProductCategoryID;

        xSubReturn = ProductCategoryPointConfig.x.Delete();
        if (xSubReturn.IsUnSuccess())
        {
            xReturn.ReturnError(xSubReturn, "删除商品类型积分配置失败");
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询商品积分配置列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryProductPointConfigList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();

        xSubReturn = ConfigImpl.QueryProductPointConfigList(out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询商品积分配置列表失败");
        }

        ExportXmlAsAttribute(m_XmlTextWriter, dt, "ProductPointConfig");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存商品积分配置。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveProductPointConfig()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_ProductPointConfig ProductPointConfig = new BSC_ProductPointConfig();

        ProductPointConfig.ProductID = GetIntParameter("ProductID", 0);
        ProductPointConfig.PointMode = 1;
        ProductPointConfig.PointValue = GetIntParameter("PointValue", 0);

        xSubReturn = ProductPointConfig.x.Insert();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "保存商品积分配置失败");
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 删除商品积分配置。
    /// </summary>
    /// <returns></returns>
    public XReturn DeleteProductPointConfig()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int ProductID;

        if (!GetIntParameter("ProductID", 0, out ProductID))
        {
            return xReturn.ReturnError("没有指定商品ID");
        }

        BSC_ProductPointConfig ProductPointConfig = new BSC_ProductPointConfig();
        ProductPointConfig.ProductID = ProductID;

        xSubReturn = ProductPointConfig.x.Delete();
        if (xSubReturn.IsUnSuccess())
        {
            xReturn.ReturnError(xSubReturn, "删除商品积分配置失败");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 优惠劵配置服务
    /// <summary>
    /// 查询优惠劵发放金额区间配置列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryCouponGrantAmountConfigList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();

        xSubReturn = ConfigImpl.QueryCouponGrantAmountConfigList(out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询优惠劵发放金额区间配置列表失败");
        }

        ExportXmlAsAttribute(m_XmlTextWriter, dt, "CouponGrantAmountConfig");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存优惠劵发放金额区间配置。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveCouponGrantAmountConfig()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_CouponGrantAmountConfig CouponGrantAmountConfig = new BSC_CouponGrantAmountConfig();

        CouponGrantAmountConfig.CouponGrantAmountConfigID = GetIntParameter("CouponGrantAmountConfigID", 0);
        CouponGrantAmountConfig.BeginAmount = GetDoubleParameter("BeginAmount", 0);
        CouponGrantAmountConfig.EndAmount = GetDoubleParameter("EndAmount", 0);
        CouponGrantAmountConfig.ParValue = GetDoubleParameter("ParValue", 0);
        CouponGrantAmountConfig.Count = GetIntParameter("Count", 0);

        xSubReturn = ConfigImpl.SaveCouponGrantAmountConfig(CouponGrantAmountConfig);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "保存优惠劵发放金额区间配置失败");
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 删除优惠劵发放金额区间配置。
    /// </summary>
    /// <returns></returns>
    public XReturn DeleteCouponGrantAmountConfig()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int CouponGrantAmountConfigID;

        if (!GetIntParameter("CouponGrantAmountConfigID", 0, out CouponGrantAmountConfigID))
        {
            return xReturn.ReturnError("没有指定优惠劵发放金额区间配置ID");
        }

        BSC_CouponGrantAmountConfig CouponGrantAmountConfig = new BSC_CouponGrantAmountConfig();
        CouponGrantAmountConfig.CouponGrantAmountConfigID = CouponGrantAmountConfigID;

        xSubReturn = CouponGrantAmountConfig.x.DeleteByIdentity();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "删除优惠劵发放金额区间配置失败");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 促销配置服务
    /// <summary>
    /// 查询促销规则配置列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryPromotionRuleList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();

        xSubReturn = ConfigImpl.QueryPromotionRuleList(0, out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询促销规则配置列表失败");
        }

        ExportXmlAsAttribute(m_XmlTextWriter, dt, "PromotionRule");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存促销规则配置。
    /// </summary>
    /// <returns></returns>
    public XReturn SavePromotionRule()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_PromotionRule PromotionRule = new BSC_PromotionRule();

        PromotionRule.PromotionRuleID = GetIntParameter("PromotionRuleID", 0);
        PromotionRule.PromotionRuleName = GetStringParameter("PromotionRuleName", String.Empty);
        PromotionRule.ConditionTarget = GetIntParameter("ConditionTarget", 0);
        PromotionRule.ConditionMode = GetIntParameter("ConditionMode", 0);
        PromotionRule.ConditionProductScopeID = GetIntParameter("ConditionProductScopeID", 0);
        PromotionRule.ConditionParameterList = GetStringParameter("ConditionParameterList", String.Empty);
        PromotionRule.ImplementTarget = GetIntParameter("ImplementTarget", 0);
        PromotionRule.ImplementMode = GetIntParameter("ImplementMode", 0);
        PromotionRule.ImplementProductScopeID = GetIntParameter("ImplementProductScopeID", 0);
        PromotionRule.ImplementParameterList = GetStringParameter("ImplementParameterList", String.Empty);
        PromotionRule.ImplementMaxQuantity = GetIntParameter("ImplementMaxQuantity", 0);
        PromotionRule.BeginDate = GetStringParameter("BeginDate", String.Empty);
        PromotionRule.EndDate = GetStringParameter("EndDate", String.Empty);
        PromotionRule.OperateTime = DateTime.Now;
        //PromotionRule.OperatorID = OperatorState.Operator.OperatorID;

        xSubReturn = ConfigImpl.SavePromotionRule(PromotionRule);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "保存促销规则配置失败");
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 删除促销规则配置。
    /// </summary>
    /// <returns></returns>
    public XReturn DeletePromotionRule()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int PromotionRuleID;

        if (!GetIntParameter("PromotionRuleID", 0, out PromotionRuleID))
        {
            return xReturn.ReturnError("没有指定促销规则配置ID");
        }

        BSC_PromotionRule PromotionRule = new BSC_PromotionRule();
        PromotionRule.PromotionRuleID = PromotionRuleID;

        xSubReturn = PromotionRule.x.DeleteByIdentity();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "删除促销规则配置失败");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 商品提示配置服务
    /// <summary>
    /// 查询商品提示配置列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryProductHintList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();

        xSubReturn = ConfigImpl.QueryProductHintList(out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询商品提示配置列表失败");
        }

        ExportXmlAsAttribute(m_XmlTextWriter, dt, "ProductHint");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存商品提示配置。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveProductHint()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_ProductHint ProductHint = new BSC_ProductHint();

        ProductHint.ProductHintID = GetIntParameter("ProductHintID", 0);
        ProductHint.ProductScopeID = GetIntParameter("ProductScopeID", 0);
        ProductHint.HintImageCode = GetStringParameter("HintImageCode", String.Empty);
        ProductHint.HintTitle = GetStringParameter("HintTitle", String.Empty);

        xSubReturn = ConfigImpl.SaveProductHint(ProductHint);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "保存商品提示配置失败");
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 删除商品提示配置。
    /// </summary>
    /// <returns></returns>
    public XReturn DeleteProductHint()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int ProductHintID;

        if (!GetIntParameter("ProductHintID", 0, out ProductHintID))
        {
            return xReturn.ReturnError("没有指定商品提示配置ID");
        }

        BSC_ProductHint ProductHint = new BSC_ProductHint();
        ProductHint.ProductHintID = ProductHintID;

        xSubReturn = ProductHint.x.DeleteByIdentity();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "删除商品提示配置失败");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion

    #region 商品范围服务
    /// <summary>
    /// 查询商品范围列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryProductScopeList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();

        xSubReturn = ConfigImpl.QueryProductScopeList(out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询商品范围列表失败");
        }

        ExportXmlAsAttribute(m_XmlTextWriter, dt, "ProductScope");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存商品范围。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveProductScope()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_ProductScope ProductScope = new BSC_ProductScope();

        ProductScope.ProductScopeID = GetIntParameter("ProductScopeID", 0);
        ProductScope.ProductScopeName = GetStringParameter("ProductScopeName", String.Empty);
        ProductScope.Remark = GetStringParameter("Remark", String.Empty);

        xSubReturn = ConfigImpl.SaveProductScope(ProductScope);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "保存商品范围失败");
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 删除商品范围。
    /// </summary>
    /// <returns></returns>
    public XReturn DeleteProductScope()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int ProductScopeID;

        if (!GetIntParameter("ProductScopeID", 0, out ProductScopeID))
        {
            return xReturn.ReturnError("没有指定商品范围ID");
        }

        xSubReturn = ConfigImpl.DeleteProductScope(ProductScopeID);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "删除商品范围失败");
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 查询商品范围明细列表。
    /// </summary>
    /// <returns></returns>
    public XReturn QueryProductScopeItemList()
    {
        DataTable dt = null;
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int ProductScopeID;

        if (!GetIntParameter("ProductScopeID", 0, out ProductScopeID))
        {
            return xReturn.ReturnError("没有指定商品范围明细ID");
        }

        xSubReturn = ConfigImpl.QueryProductScopeItemList(ProductScopeID, out dt);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "查询商品范围明细列表失败");
        }

        ExportXmlAsAttribute(m_XmlTextWriter, dt, "ProductScopeItem");

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 保存商品范围明细。
    /// </summary>
    /// <returns></returns>
    public XReturn SaveProductScopeItem()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        BSC_ProductScopeItem ProductScopeItem = new BSC_ProductScopeItem();

        ProductScopeItem.ProductScopeItemID = GetIntParameter("ProductScopeItemID", 0);
        ProductScopeItem.ProductScopeID = GetIntParameter("ProductScopeID", 0);
        ProductScopeItem.ProductScopeItemMode = GetIntParameter("ProductScopeItemMode", 0);
        ProductScopeItem.ProductID = GetIntParameter("ProductID", 0);
        ProductScopeItem.ProductCategoryID = GetIntParameter("ProductCategoryID", 0);
        ProductScopeItem.ProductBrandID = GetIntParameter("ProductBrandID", 0);

        xSubReturn = ConfigImpl.SaveProductScopeItem(ProductScopeItem);
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "保存商品范围明细失败");
        }

        return xReturn.ReturnSuccess();
    }

    /// <summary>
    /// 删除商品范围明细。
    /// </summary>
    /// <returns></returns>
    public XReturn DeleteProductScopeItem()
    {
        XReturn xSubReturn;
        XReturn xReturn = new XReturn();
        int ProductScopeItemID;

        if (!GetIntParameter("ProductScopeItemID", 0, out ProductScopeItemID))
        {
            return xReturn.ReturnError("没有指定商品范围明细ID");
        }

        BSC_ProductScopeItem ProductScopeItem = new BSC_ProductScopeItem();
        ProductScopeItem.ProductScopeItemID = ProductScopeItemID;

        xSubReturn = ProductScopeItem.x.DeleteByIdentity();
        if (xSubReturn.IsUnSuccess())
        {
            return xReturn.ReturnError(xSubReturn, "删除商品范围明细失败");
        }

        return xReturn.ReturnSuccess();
    }
    #endregion
}