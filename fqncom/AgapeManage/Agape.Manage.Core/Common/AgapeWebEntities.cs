using System;
using System.Collections.Generic;
using System.Text;
using Leopard.Util;
using Leopard.Data;

namespace Agape.Manage.Core.Common
{
    [Serializable]
    public class BSC_Warehouse : BaseEntity
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get { return x.GetString("Address"); }
            set { x.SetValue("Address", value); }
        }

        /// <summary>
        /// 常用标志
        /// </summary>
        public string CommonFlag
        {
            get { return x.GetString("CommonFlag"); }
            set { x.SetValue("CommonFlag", value); }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return x.GetDateTime("CreateTime"); }
            set { x.SetValue("CreateTime", value); }
        }

        /// <summary>
        /// 简介
        /// </summary>
        public string Description
        {
            get { return x.GetString("Description"); }
            set { x.SetValue("Description", value); }
        }

        /// <summary>
        /// 传真
        /// </summary>
        public string Fax
        {
            get { return x.GetString("Fax"); }
            set { x.SetValue("Fax", value); }
        }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int HitCount
        {
            get { return x.GetInt32("HitCount"); }
            set { x.SetValue("HitCount", value); }
        }

        /// <summary>
        /// 管理员ID
        /// </summary>
        public int ManagerID
        {
            get { return x.GetInt32("ManagerID"); }
            set { x.SetValue("ManagerID", value); }
        }

        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName
        {
            get { return x.GetString("ShortName"); }
            set { x.SetValue("ShortName", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status
        {
            get { return x.GetString("Status"); }
            set { x.SetValue("Status", value); }
        }

        /// <summary>
        /// 电话
        /// </summary>
        public string Telphone
        {
            get { return x.GetString("Telphone"); }
            set { x.SetValue("Telphone", value); }
        }

        /// <summary>
        /// 仓库类型
        /// </summary>
        public string WarehouseCategory
        {
            get { return x.GetString("WarehouseCategory"); }
            set { x.SetValue("WarehouseCategory", value); }
        }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public int WarehouseID
        {
            get { return x.GetInt32("WarehouseID"); }
            set { x.SetValue("WarehouseID", value); }
        }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WarehouseName
        {
            get { return x.GetString("WarehouseName"); }
            set { x.SetValue("WarehouseName", value); }
        }

        /// <summary>
        /// 仓库编号
        /// </summary>
        public string WarehouseNo
        {
            get { return x.GetString("WarehouseNo"); }
            set { x.SetValue("WarehouseNo", value); }
        }

        /// <summary>
        /// 助记词
        /// </summary>
        public string WordKey
        {
            get { return x.GetString("WordKey"); }
            set { x.SetValue("WordKey", value); }
        }

        public BSC_Warehouse()
        {
            x.Init("BSC_Warehouse");
        }
    }
    [Serializable]
    public class BSC_Product : BaseEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string ProductNo
        {
            get { return x.GetString("ProductNo"); }
            set { x.SetValue("ProductNo", value); }
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName
        {
            get { return x.GetString("ProductName"); }
            set { x.SetValue("ProductName", value); }
        }

        /// <summary>
        /// 检索词
        /// </summary>
        public string WordKey
        {
            get { return x.GetString("WordKey"); }
            set { x.SetValue("WordKey", value); }
        }

        /// <summary>
        /// 商品类别ID
        /// </summary>
        public int ProductCategoryID
        {
            get { return x.GetInt32("ProductCategoryID"); }
            set { x.SetValue("ProductCategoryID", value); }
        }

        /// <summary>
        /// 商品品牌ID
        /// </summary>
        public int ProductBrandID
        {
            get { return x.GetInt32("ProductBrandID"); }
            set { x.SetValue("ProductBrandID", value); }
        }

        /// <summary>
        /// 商品单位
        /// </summary>
        public string ProductUnit
        {
            get { return x.GetString("ProductUnit"); }
            set { x.SetValue("ProductUnit", value); }
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string FitAge
        {
            get { return x.GetString("FitAge"); }
            set { x.SetValue("FitAge", value); }
        }

        /// <summary>
        /// 产地
        /// </summary>
        public string producingArea
        {
            get { return x.GetString("producingArea"); }
            set { x.SetValue("producingArea", value); }
        }

        /// <summary>
        /// 条形码
        /// </summary>
        public string BarCode
        {
            get { return x.GetString("BarCode"); }
            set { x.SetValue("BarCode", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int DefaultProductSpecID
        {
            get { return x.GetInt32("DefaultProductSpecID"); }
            set { x.SetValue("DefaultProductSpecID", value); }
        }

        /// <summary>
        /// 销售价格
        /// </summary>
        public double SalesPrice
        {
            get { return x.GetDouble("SalesPrice"); }
            set { x.SetValue("SalesPrice", value); }
        }

        /// <summary>
        /// 市场价格
        /// </summary>
        public double MarketPrice
        {
            get { return x.GetDouble("MarketPrice"); }
            set { x.SetValue("MarketPrice", value); }
        }

        /// <summary>
        /// 采购价格
        /// </summary>
        public double PurchasePrice
        {
            get { return x.GetDouble("PurchasePrice"); }
            set { x.SetValue("PurchasePrice", value); }
        }

        /// <summary>
        /// 成本价格
        /// </summary>
        public double CostPrice
        {
            get { return x.GetDouble("CostPrice"); }
            set { x.SetValue("CostPrice", value); }
        }

        /// <summary>
        /// 重量
        /// </summary>
        public double Weight
        {
            get { return x.GetDouble("Weight"); }
            set { x.SetValue("Weight", value); }
        }

        /// <summary>
        /// 权重值
        /// </summary>
        public int WeightValue
        {
            get { return x.GetInt32("WeightValue"); }
            set { x.SetValue("WeightValue", value); }
        }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int ReviewCount
        {
            get { return x.GetInt32("ReviewCount"); }
            set { x.SetValue("ReviewCount", value); }
        }

        /// <summary>
        /// 销售次数
        /// </summary>
        public int SalesCount
        {
            get { return x.GetInt32("SalesCount"); }
            set { x.SetValue("SalesCount", value); }
        }

        /// <summary>
        /// 销售数量
        /// </summary>
        public int SalesQuantity
        {
            get { return x.GetInt32("SalesQuantity"); }
            set { x.SetValue("SalesQuantity", value); }
        }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int HitCount
        {
            get { return x.GetInt32("HitCount"); }
            set { x.SetValue("HitCount", value); }
        }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime IssueDateTime
        {
            get { return x.GetDateTime("IssueDateTime"); }
            set { x.SetValue("IssueDateTime", value); }
        }

        /// <summary>
        /// 操作员ID
        /// </summary>
        public int OperatorID
        {
            get { return x.GetInt32("OperatorID"); }
            set { x.SetValue("OperatorID", value); }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return x.GetDateTime("CreateTime"); }
            set { x.SetValue("CreateTime", value); }
        }

        /// <summary>
        /// 简介
        /// </summary>
        public string Description
        {
            get { return x.GetString("Description"); }
            set { x.SetValue("Description", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_Product()
        {
            x.Init("BSC_Product");
        }
    }
    [Serializable]
    public class BSC_ProductSpec : BaseEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int ProductSpecID
        {
            get { return x.GetInt32("ProductSpecID"); }
            set { x.SetValue("ProductSpecID", value); }
        }

        /// <summary>
        /// 商品规格
        /// </summary>
        public string ProductSpec
        {
            get { return x.GetString("ProductSpec"); }
            set { x.SetValue("ProductSpec", value); }
        }

        /// <summary>
        /// 期初库存
        /// </summary>
        public double StockInitQuantity
        {
            get { return x.GetDouble("StockInitQuantity"); }
            set { x.SetValue("StockInitQuantity", value); }
        }

        /// <summary>
        /// 库存下限
        /// </summary>
        public double StockLowerLimit
        {
            get { return x.GetDouble("StockLowerLimit"); }
            set { x.SetValue("StockLowerLimit", value); }
        }

        /// <summary>
        /// 库存上限
        /// </summary>
        public double StockUpperLimit
        {
            get { return x.GetDouble("StockUpperLimit"); }
            set { x.SetValue("StockUpperLimit", value); }
        }

        /// <summary>
        /// 默认标志
        /// </summary>
        public int DefaultFlag
        {
            get { return x.GetInt32("DefaultFlag"); }
            set { x.SetValue("DefaultFlag", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_ProductSpec()
        {
            x.Init("BSC_ProductSpec");
        }
    }
    [Serializable]
    public class BSC_ProductPicture : BaseEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品图片ID
        /// </summary>
        public int ProductPictureID
        {
            get { return x.GetInt32("ProductPictureID"); }
            set { x.SetValue("ProductPictureID", value); }
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get { return x.GetString("FileName"); }
            set { x.SetValue("FileName", value); }
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType
        {
            get { return x.GetString("FileType"); }
            set { x.SetValue("FileType", value); }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return x.GetString("Title"); }
            set { x.SetValue("Title", value); }
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int OrderNo
        {
            get { return x.GetInt32("OrderNo"); }
            set { x.SetValue("OrderNo", value); }
        }

        /// <summary>
        /// 操作日期
        /// </summary>
        public DateTime OperateDate
        {
            get { return x.GetDateTime("OperateDate"); }
            set { x.SetValue("OperateDate", value); }
        }

        /// <summary>
        /// 操作员ID
        /// </summary>
        public int OperatorID
        {
            get { return x.GetInt32("OperatorID"); }
            set { x.SetValue("OperatorID", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        public BSC_ProductPicture()
        {
            x.Init("BSC_ProductPicture");
        }
    }
    [Serializable]
    public class BSC_ProductTemp : BaseEntity
    {
        /// <summary>
        /// 商品临时ID
        /// </summary>
        public int ProductTempID
        {
            get { return x.GetInt32("ProductTempID"); }
            set { x.SetValue("ProductTempID", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string ProductNo
        {
            get { return x.GetString("ProductNo"); }
            set { x.SetValue("ProductNo", value); }
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName
        {
            get { return x.GetString("ProductName"); }
            set { x.SetValue("ProductName", value); }
        }

        /// <summary>
        /// 检索词
        /// </summary>
        public string WordKey
        {
            get { return x.GetString("WordKey"); }
            set { x.SetValue("WordKey", value); }
        }

        /// <summary>
        /// 商品类别ID
        /// </summary>
        public int ProductCategoryID
        {
            get { return x.GetInt32("ProductCategoryID"); }
            set { x.SetValue("ProductCategoryID", value); }
        }

        /// <summary>
        /// 商品品牌ID
        /// </summary>
        public int ProductBrandID
        {
            get { return x.GetInt32("ProductBrandID"); }
            set { x.SetValue("ProductBrandID", value); }
        }

        /// <summary>
        /// 商品单位
        /// </summary>
        public string ProductUnit
        {
            get { return x.GetString("ProductUnit"); }
            set { x.SetValue("ProductUnit", value); }
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string FitAge
        {
            get { return x.GetString("FitAge"); }
            set { x.SetValue("FitAge", value); }
        }

        /// <summary>
        /// 产地
        /// </summary>
        public string producingArea
        {
            get { return x.GetString("producingArea"); }
            set { x.SetValue("producingArea", value); }
        }

        /// <summary>
        /// 条形码
        /// </summary>
        public string BarCode
        {
            get { return x.GetString("BarCode"); }
            set { x.SetValue("BarCode", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int DefaultProductSpecID
        {
            get { return x.GetInt32("DefaultProductSpecID"); }
            set { x.SetValue("DefaultProductSpecID", value); }
        }

        /// <summary>
        /// 销售价格
        /// </summary>
        public double SalesPrice
        {
            get { return x.GetDouble("SalesPrice"); }
            set { x.SetValue("SalesPrice", value); }
        }

        /// <summary>
        /// 市场价格
        /// </summary>
        public double MarketPrice
        {
            get { return x.GetDouble("MarketPrice"); }
            set { x.SetValue("MarketPrice", value); }
        }

        /// <summary>
        /// 采购价格
        /// </summary>
        public double PurchasePrice
        {
            get { return x.GetDouble("PurchasePrice"); }
            set { x.SetValue("PurchasePrice", value); }
        }

        /// <summary>
        /// 成本价格
        /// </summary>
        public double CostPrice
        {
            get { return x.GetDouble("CostPrice"); }
            set { x.SetValue("CostPrice", value); }
        }

        /// <summary>
        /// 权重值
        /// </summary>
        public int WeightValue
        {
            get { return x.GetInt32("WeightValue"); }
            set { x.SetValue("WeightValue", value); }
        }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int ReviewCount
        {
            get { return x.GetInt32("ReviewCount"); }
            set { x.SetValue("ReviewCount", value); }
        }

        /// <summary>
        /// 销售次数
        /// </summary>
        public int SalesCount
        {
            get { return x.GetInt32("SalesCount"); }
            set { x.SetValue("SalesCount", value); }
        }

        /// <summary>
        /// 销售数量
        /// </summary>
        public int SalesQuantity
        {
            get { return x.GetInt32("SalesQuantity"); }
            set { x.SetValue("SalesQuantity", value); }
        }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int HitCount
        {
            get { return x.GetInt32("HitCount"); }
            set { x.SetValue("HitCount", value); }
        }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime IssueDateTime
        {
            get { return x.GetDateTime("IssueDateTime"); }
            set { x.SetValue("IssueDateTime", value); }
        }

        /// <summary>
        /// 操作员ID
        /// </summary>
        public int OperatorID
        {
            get { return x.GetInt32("OperatorID"); }
            set { x.SetValue("OperatorID", value); }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return x.GetDateTime("CreateTime"); }
            set { x.SetValue("CreateTime", value); }
        }

        /// <summary>
        /// 简介
        /// </summary>
        public string Description
        {
            get { return x.GetString("Description"); }
            set { x.SetValue("Description", value); }
        }

        /// <summary>
        /// 编辑操作员ID
        /// </summary>
        public int EditOperatorID
        {
            get { return x.GetInt32("EditOperatorID"); }
            set { x.SetValue("EditOperatorID", value); }
        }

        /// <summary>
        /// 编辑时间
        /// </summary>
        public DateTime EditTime
        {
            get { return x.GetDateTime("EditTime"); }
            set { x.SetValue("EditTime", value); }
        }

        /// <summary>
        /// 审核操作员ID
        /// </summary>
        public int AuditOperatorID
        {
            get { return x.GetInt32("AuditOperatorID"); }
            set { x.SetValue("AuditOperatorID", value); }
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditTime
        {
            get { return x.GetDateTime("AuditTime"); }
            set { x.SetValue("AuditTime", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_ProductTemp()
        {
            x.Init("BSC_ProductTemp");
        }
    }
    [Serializable]
    public class BSC_ProductCategory : BaseEntity
    {
        /// <summary>
        /// 商品类型ID
        /// </summary>
        public int ProductCategoryID
        {
            get { return x.GetInt32("ProductCategoryID"); }
            set { x.SetValue("ProductCategoryID", value); }
        }

        /// <summary>
        /// 商品类型名称
        /// </summary>
        public string ProductCategoryName
        {
            get { return x.GetString("ProductCategoryName"); }
            set { x.SetValue("ProductCategoryName", value); }
        }

        /// <summary>
        /// 层级
        /// </summary>
        public int NodeLevel
        {
            get { return x.GetInt32("NodeLevel"); }
            set { x.SetValue("NodeLevel", value); }
        }

        /// <summary>
        /// 叶子标志
        /// </summary>
        public int LeafFlag
        {
            get { return x.GetInt32("LeafFlag"); }
            set { x.SetValue("LeafFlag", value); }
        }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public int ParentID
        {
            get { return x.GetInt32("ParentID"); }
            set { x.SetValue("ParentID", value); }
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int OrderNo
        {
            get { return x.GetInt32("OrderNo"); }
            set { x.SetValue("OrderNo", value); }
        }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int Quantity
        {
            get { return x.GetInt32("Quantity"); }
            set { x.SetValue("Quantity", value); }
        }

        /// <summary>
        /// 最低价格
        /// </summary>
        public double MinPrice
        {
            get { return x.GetDouble("MinPrice"); }
            set { x.SetValue("MinPrice", value); }
        }

        /// <summary>
        /// 最高价格
        /// </summary>
        public double MaxPrice
        {
            get { return x.GetDouble("MaxPrice"); }
            set { x.SetValue("MaxPrice", value); }
        }

        /// <summary>
        /// 全路径
        /// </summary>
        public string FullPath
        {
            get { return x.GetString("FullPath"); }
            set { x.SetValue("FullPath", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_ProductCategory()
        {
            x.Init("BSC_ProductCategory");
        }
    }
    [Serializable]
    public class BSC_ProductBrand : BaseEntity
    {
        /// <summary>
        /// 商品品牌ID
        /// </summary>
        public int ProductBrandID
        {
            get { return x.GetInt32("ProductBrandID"); }
            set { x.SetValue("ProductBrandID", value); }
        }

        /// <summary>
        /// 商品品牌代码
        /// </summary>
        public string ProductBrandCode
        {
            get { return x.GetString("ProductBrandCode"); }
            set { x.SetValue("ProductBrandCode", value); }
        }

        /// <summary>
        /// 商品品牌名称
        /// </summary>
        public string ProductBrandName
        {
            get { return x.GetString("ProductBrandName"); }
            set { x.SetValue("ProductBrandName", value); }
        }

        /// <summary>
        /// 产地
        /// </summary>
        public string producingArea
        {
            get { return x.GetString("producingArea"); }
            set { x.SetValue("producingArea", value); }
        }

        /// <summary>
        /// 制造商
        /// </summary>
        public string Producer
        {
            get { return x.GetString("Producer"); }
            set { x.SetValue("Producer", value); }
        }

        public BSC_ProductBrand()
        {
            x.Init("BSC_ProductBrand");
        }
    }
    [Serializable]
    public class BSC_ProductCategoryBrand : BaseEntity
    {
        /// <summary>
        /// 商品品牌ID
        /// </summary>
        public int ProductBrandID
        {
            get { return x.GetInt32("ProductBrandID"); }
            set { x.SetValue("ProductBrandID", value); }
        }

        /// <summary>
        /// 商品类型
        /// </summary>
        public int ProductCategoryID
        {
            get { return x.GetInt32("ProductCategoryID"); }
            set { x.SetValue("ProductCategoryID", value); }
        }

        public BSC_ProductCategoryBrand()
        {
            x.Init("BSC_ProductCategoryBrand");
        }
    }
    [Serializable]
    public class BSC_Member : BaseEntity
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return x.GetInt32("MemberID"); }
            set { x.SetValue("MemberID", value); }
        }

        /// <summary>
        /// 会员编号
        /// </summary>
        public string MemberNo
        {
            get { return x.GetString("MemberNo"); }
            set { x.SetValue("MemberNo", value); }
        }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email
        {
            get { return x.GetString("Email"); }
            set { x.SetValue("Email", value); }
        }

        /// <summary>
        /// 会员名
        /// </summary>
        public string MemberName
        {
            get { return x.GetString("MemberName"); }
            set { x.SetValue("MemberName", value); }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return x.GetString("Password"); }
            set { x.SetValue("Password", value); }
        }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName
        {
            get { return x.GetString("RealName"); }
            set { x.SetValue("RealName", value); }
        }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            get { return x.GetString("NickName"); }
            set { x.SetValue("NickName", value); }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender
        {
            get { return x.GetInt32("Gender"); }
            set { x.SetValue("Gender", value); }
        }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get { return x.GetInt32("Age"); }
            set { x.SetValue("Age", value); }
        }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday
        {
            get { return x.GetString("Birthday"); }
            set { x.SetValue("Birthday", value); }
        }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdentityNo
        {
            get { return x.GetString("IdentityNo"); }
            set { x.SetValue("IdentityNo", value); }
        }

        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhone
        {
            get { return x.GetString("MobilePhone"); }
            set { x.SetValue("MobilePhone", value); }
        }

        /// <summary>
        /// 会员类型ID
        /// </summary>
        public int MemberCategoryID
        {
            get { return x.GetInt32("MemberCategoryID"); }
            set { x.SetValue("MemberCategoryID", value); }
        }

        /// <summary>
        /// 默认会员地址ID
        /// </summary>
        public int DefaultMemberAddressID
        {
            get { return x.GetInt32("DefaultMemberAddressID"); }
            set { x.SetValue("DefaultMemberAddressID", value); }
        }

        /// <summary>
        /// 默认会员发票ID
        /// </summary>
        public int DefaultMemberInvoiceID
        {
            get { return x.GetInt32("DefaultMemberInvoiceID"); }
            set { x.SetValue("DefaultMemberInvoiceID", value); }
        }

        /// <summary>
        /// 默认支付配送方式
        /// </summary>
        public string DefaultPaymentDeliverType
        {
            get { return x.GetString("DefaultPaymentDeliverType"); }
            set { x.SetValue("DefaultPaymentDeliverType", value); }
        }

        /// <summary>
        /// 余额
        /// </summary>
        public double Balance
        {
            get { return x.GetDouble("Balance"); }
            set { x.SetValue("Balance", value); }
        }

        /// <summary>
        /// 优惠劵数量
        /// </summary>
        public int VoucherCount
        {
            get { return x.GetInt32("VoucherCount"); }
            set { x.SetValue("VoucherCount", value); }
        }

        /// <summary>
        /// 积分值
        /// </summary>
        public int PointValue
        {
            get { return x.GetInt32("PointValue"); }
            set { x.SetValue("PointValue", value); }
        }

        /// <summary>
        /// 冻结积分值
        /// </summary>
        public int FrozenPointValue
        {
            get { return x.GetInt32("FrozenPointValue"); }
            set { x.SetValue("FrozenPointValue", value); }
        }

        /// <summary>
        /// 长冻结积分值
        /// </summary>
        public int LongFrozenPointValue
        {
            get { return x.GetInt32("LongFrozenPointValue"); }
            set { x.SetValue("LongFrozenPointValue", value); }
        }

        /// <summary>
        /// 已兑换积分值
        /// </summary>
        public int ExchangedPointValue
        {
            get { return x.GetInt32("ExchangedPointValue"); }
            set { x.SetValue("ExchangedPointValue", value); }
        }

        /// <summary>
        /// 电子邮箱认证结果
        /// </summary>
        public int EmailVerifyResult
        {
            get { return x.GetInt32("EmailVerifyResult"); }
            set { x.SetValue("EmailVerifyResult", value); }
        }

        /// <summary>
        /// 电子邮箱认证码
        /// </summary>
        public string EmailVerifyCode
        {
            get { return x.GetString("EmailVerifyCode"); }
            set { x.SetValue("EmailVerifyCode", value); }
        }

        /// <summary>
        /// 手机认证结果
        /// </summary>
        public int MobilePhoneVerifyResult
        {
            get { return x.GetInt32("MobilePhoneVerifyResult"); }
            set { x.SetValue("MobilePhoneVerifyResult", value); }
        }

        /// <summary>
        /// 手机认证码
        /// </summary>
        public string MobilePhoneVerifyCode
        {
            get { return x.GetString("MobilePhoneVerifyCode"); }
            set { x.SetValue("MobilePhoneVerifyCode", value); }
        }

        /// <summary>
        /// 密码更新认证码
        /// </summary>
        public string UpdatePasswordVerifyCode
        {
            get { return x.GetString("UpdatePasswordVerifyCode"); }
            set { x.SetValue("UpdatePasswordVerifyCode", value); }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return x.GetDateTime("CreateTime"); }
            set { x.SetValue("CreateTime", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_Member()
        {
            x.Init("BSC_Member");
        }
    }
    [Serializable]
    public class BSC_MemberCategory : BaseEntity
    {
        /// <summary>
        /// 会员类型ID
        /// </summary>
        public int MemberCategoryID
        {
            get { return x.GetInt32("MemberCategoryID"); }
            set { x.SetValue("MemberCategoryID", value); }
        }

        /// <summary>
        /// 会员类型
        /// </summary>
        public string MemberCategory
        {
            get { return x.GetString("MemberCategory"); }
            set { x.SetValue("MemberCategory", value); }
        }

        public BSC_MemberCategory()
        {
            x.Init("BSC_MemberCategory");
        }
    }
    [Serializable]
    public class BSC_MemberAddress : BaseEntity
    {
        /// <summary>
        /// 收藏商品ID
        /// </summary>
        public int MemberAddressID
        {
            get { return x.GetInt32("MemberAddressID"); }
            set { x.SetValue("MemberAddressID", value); }
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return x.GetInt32("MemberID"); }
            set { x.SetValue("MemberID", value); }
        }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province
        {
            get { return x.GetString("Province"); }
            set { x.SetValue("Province", value); }
        }

        /// <summary>
        /// 城市
        /// </summary>
        public string City
        {
            get { return x.GetString("City"); }
            set { x.SetValue("City", value); }
        }

        /// <summary>
        /// 县区
        /// </summary>
        public string District
        {
            get { return x.GetString("District"); }
            set { x.SetValue("District", value); }
        }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Detail
        {
            get { return x.GetString("Detail"); }
            set { x.SetValue("Detail", value); }
        }

        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode
        {
            get { return x.GetString("PostCode"); }
            set { x.SetValue("PostCode", value); }
        }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ReceiverName
        {
            get { return x.GetString("ReceiverName"); }
            set { x.SetValue("ReceiverName", value); }
        }

        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhone
        {
            get { return x.GetString("MobilePhone"); }
            set { x.SetValue("MobilePhone", value); }
        }

        /// <summary>
        /// 电话
        /// </summary>
        public string Telphone
        {
            get { return x.GetString("Telphone"); }
            set { x.SetValue("Telphone", value); }
        }

        /// <summary>
        /// 默认标志
        /// </summary>
        public int DefaultFlag
        {
            get { return x.GetInt32("DefaultFlag"); }
            set { x.SetValue("DefaultFlag", value); }
        }

        public BSC_MemberAddress()
        {
            x.Init("BSC_MemberAddress");
        }
    }
    [Serializable]
    public class BSC_MemberInvoice : BaseEntity
    {
        /// <summary>
        /// 会员发票ID
        /// </summary>
        public int MemberInvoiceID
        {
            get { return x.GetInt32("MemberInvoiceID"); }
            set { x.SetValue("MemberInvoiceID", value); }
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return x.GetInt32("MemberID"); }
            set { x.SetValue("MemberID", value); }
        }

        /// <summary>
        /// 发票类型
        /// </summary>
        public int InvoiceType
        {
            get { return x.GetInt32("InvoiceType"); }
            set { x.SetValue("InvoiceType", value); }
        }

        /// <summary>
        /// 发票抬头类型
        /// </summary>
        public int InvoiceHeaderType
        {
            get { return x.GetInt32("InvoiceHeaderType"); }
            set { x.SetValue("InvoiceHeaderType", value); }
        }

        /// <summary>
        /// 发票内容
        /// </summary>
        public int InvoiceContent
        {
            get { return x.GetInt32("InvoiceContent"); }
            set { x.SetValue("InvoiceContent", value); }
        }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string CompanyName
        {
            get { return x.GetString("CompanyName"); }
            set { x.SetValue("CompanyName", value); }
        }

        /// <summary>
        /// 纳税人识别号
        /// </summary>
        public string TaxPayerNo
        {
            get { return x.GetString("TaxPayerNo"); }
            set { x.SetValue("TaxPayerNo", value); }
        }

        /// <summary>
        /// 注册地址
        /// </summary>
        public string RegisterAddress
        {
            get { return x.GetString("RegisterAddress"); }
            set { x.SetValue("RegisterAddress", value); }
        }

        /// <summary>
        /// 注册电话
        /// </summary>
        public string RegisterPhone
        {
            get { return x.GetString("RegisterPhone"); }
            set { x.SetValue("RegisterPhone", value); }
        }

        /// <summary>
        /// 开户银行
        /// </summary>
        public string OpenBankName
        {
            get { return x.GetString("OpenBankName"); }
            set { x.SetValue("OpenBankName", value); }
        }

        /// <summary>
        /// 银行账户
        /// </summary>
        public string BankAccountNo
        {
            get { return x.GetString("BankAccountNo"); }
            set { x.SetValue("BankAccountNo", value); }
        }

        public BSC_MemberInvoice()
        {
            x.Init("BSC_MemberInvoice");
        }
    }
    [Serializable]
    public class BSC_MemberReview : BaseEntity
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        public int MemberReviewID
        {
            get { return x.GetInt32("MemberReviewID"); }
            set { x.SetValue("MemberReviewID", value); }
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return x.GetInt32("MemberID"); }
            set { x.SetValue("MemberID", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return x.GetString("Title"); }
            set { x.SetValue("Title", value); }
        }

        /// <summary>
        /// 优点
        /// </summary>
        public string Advantage
        {
            get { return x.GetString("Advantage"); }
            set { x.SetValue("Advantage", value); }
        }

        /// <summary>
        /// 不足
        /// </summary>
        public string Disadvantage
        {
            get { return x.GetString("Disadvantage"); }
            set { x.SetValue("Disadvantage", value); }
        }

        /// <summary>
        /// 使用心得
        /// </summary>
        public string Experience
        {
            get { return x.GetString("Experience"); }
            set { x.SetValue("Experience", value); }
        }

        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderID
        {
            get { return x.GetInt32("OrderID"); }
            set { x.SetValue("OrderID", value); }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return x.GetDateTime("CreateTime"); }
            set { x.SetValue("CreateTime", value); }
        }

        /// <summary>
        /// 评论结果
        /// </summary>
        public int ReviewResult
        {
            get { return x.GetInt32("ReviewResult"); }
            set { x.SetValue("ReviewResult", value); }
        }

        /// <summary>
        /// 赞同数量
        /// </summary>
        public int AgreeCount
        {
            get { return x.GetInt32("AgreeCount"); }
            set { x.SetValue("AgreeCount", value); }
        }

        /// <summary>
        /// 反对数量
        /// </summary>
        public int DisagreeCount
        {
            get { return x.GetInt32("DisagreeCount"); }
            set { x.SetValue("DisagreeCount", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_MemberReview()
        {
            x.Init("BSC_MemberReview");
        }
    }
    [Serializable]
    public class BSC_MemberConsultation : BaseEntity
    {
        /// <summary>
        /// 会员咨询ID
        /// </summary>
        public int MemberConsultationID
        {
            get { return x.GetInt32("MemberConsultationID"); }
            set { x.SetValue("MemberConsultationID", value); }
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return x.GetInt32("MemberID"); }
            set { x.SetValue("MemberID", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 问题
        /// </summary>
        public string Question
        {
            get { return x.GetString("Question"); }
            set { x.SetValue("Question", value); }
        }

        /// <summary>
        /// 回答
        /// </summary>
        public string Answer
        {
            get { return x.GetString("Answer"); }
            set { x.SetValue("Answer", value); }
        }

        /// <summary>
        /// 提问时间
        /// </summary>
        public DateTime QuestionTime
        {
            get { return x.GetDateTime("QuestionTime"); }
            set { x.SetValue("QuestionTime", value); }
        }

        /// <summary>
        /// 回答时间
        /// </summary>
        public DateTime AnswerTime
        {
            get { return x.GetDateTime("AnswerTime"); }
            set { x.SetValue("AnswerTime", value); }
        }

        /// <summary>
        /// 赞同数量
        /// </summary>
        public int AgreeCount
        {
            get { return x.GetInt32("AgreeCount"); }
            set { x.SetValue("AgreeCount", value); }
        }

        /// <summary>
        /// 反对数量
        /// </summary>
        public int DisagreeCount
        {
            get { return x.GetInt32("DisagreeCount"); }
            set { x.SetValue("DisagreeCount", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_MemberConsultation()
        {
            x.Init("BSC_MemberConsultation");
        }
    }
    [Serializable]
    public class BSC_MemberCoupon : BaseEntity
    {
        /// <summary>
        /// 会员优惠劵ID
        /// </summary>
        public int MemberCouponID
        {
            get { return x.GetInt32("MemberCouponID"); }
            set { x.SetValue("MemberCouponID", value); }
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return x.GetInt32("MemberID"); }
            set { x.SetValue("MemberID", value); }
        }

        /// <summary>
        /// 订单ID
        /// </summary>
        public int ToOrderID
        {
            get { return x.GetInt32("ToOrderID"); }
            set { x.SetValue("ToOrderID", value); }
        }

        /// <summary>
        /// 订单ID
        /// </summary>
        public int FromOrderID
        {
            get { return x.GetInt32("FromOrderID"); }
            set { x.SetValue("FromOrderID", value); }
        }

        /// <summary>
        /// 会员优惠劵类型
        /// </summary>
        public int MemberCouponType
        {
            get { return x.GetInt32("MemberCouponType"); }
            set { x.SetValue("MemberCouponType", value); }
        }

        /// <summary>
        /// 会员优惠劵编号
        /// </summary>
        public string MemberCouponNo
        {
            get { return x.GetString("MemberCouponNo"); }
            set { x.SetValue("MemberCouponNo", value); }
        }

        /// <summary>
        /// 面值
        /// </summary>
        public double ParValue
        {
            get { return x.GetDouble("ParValue"); }
            set { x.SetValue("ParValue", value); }
        }

        /// <summary>
        /// 所需消费金额
        /// </summary>
        public double RequiredAmount
        {
            get { return x.GetDouble("RequiredAmount"); }
            set { x.SetValue("RequiredAmount", value); }
        }

        /// <summary>
        /// 有效期开始日期
        /// </summary>
        public string BeginDate
        {
            get { return x.GetString("BeginDate"); }
            set { x.SetValue("BeginDate", value); }
        }

        /// <summary>
        /// 有效期结束日期
        /// </summary>
        public string EndDate
        {
            get { return x.GetString("EndDate"); }
            set { x.SetValue("EndDate", value); }
        }

        /// <summary>
        /// 适用商品范围ID
        /// </summary>
        public int ProductScopeID
        {
            get { return x.GetInt32("ProductScopeID"); }
            set { x.SetValue("ProductScopeID", value); }
        }

        /// <summary>
        /// 会员优惠劵验证码
        /// </summary>
        public string MemberCouponVerifyCode
        {
            get { return x.GetString("MemberCouponVerifyCode"); }
            set { x.SetValue("MemberCouponVerifyCode", value); }
        }

        /// <summary>
        /// 会员优惠劵验证时间
        /// </summary>
        public DateTime MemberCouponVerifyTime
        {
            get { return x.GetDateTime("MemberCouponVerifyTime"); }
            set { x.SetValue("MemberCouponVerifyTime", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_MemberCoupon()
        {
            x.Init("BSC_MemberCoupon");
        }
    }
    [Serializable]
    public class BSC_ProductAgio : BaseEntity
    {
        /// <summary>
        /// 优惠方案ID
        /// </summary>
        public int PriceConcessionGroupID
        {
            get { return x.GetInt32("PriceConcessionGroupID"); }
            set { x.SetValue("PriceConcessionGroupID", value); }
        }

        /// <summary>
        /// 商品折扣ID
        /// </summary>
        public int ProductAgioID
        {
            get { return x.GetInt32("ProductAgioID"); }
            set { x.SetValue("ProductAgioID", value); }
        }

        /// <summary>
        /// 商品折扣名称
        /// </summary>
        public string ProductAgioName
        {
            get { return x.GetString("ProductAgioName"); }
            set { x.SetValue("ProductAgioName", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int ProductSpecID
        {
            get { return x.GetInt32("ProductSpecID"); }
            set { x.SetValue("ProductSpecID", value); }
        }

        /// <summary>
        /// 商品类别ID
        /// </summary>
        public int ProductCategoryID
        {
            get { return x.GetInt32("ProductCategoryID"); }
            set { x.SetValue("ProductCategoryID", value); }
        }

        /// <summary>
        /// 商品品牌ID
        /// </summary>
        public int ProductBrandID
        {
            get { return x.GetInt32("ProductBrandID"); }
            set { x.SetValue("ProductBrandID", value); }
        }

        /// <summary>
        /// 优惠方式
        /// </summary>
        public int ProductAgioType
        {
            get { return x.GetInt32("ProductAgioType"); }
            set { x.SetValue("ProductAgioType", value); }
        }

        /// <summary>
        /// 优惠值
        /// </summary>
        public double ProductAgioValue
        {
            get { return x.GetDouble("ProductAgioValue"); }
            set { x.SetValue("ProductAgioValue", value); }
        }

        /// <summary>
        /// 开始日期
        /// </summary>
        public string BeginDate
        {
            get { return x.GetString("BeginDate"); }
            set { x.SetValue("BeginDate", value); }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndDate
        {
            get { return x.GetString("EndDate"); }
            set { x.SetValue("EndDate", value); }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime
        {
            get { return x.GetDateTime("OperateTime"); }
            set { x.SetValue("OperateTime", value); }
        }

        /// <summary>
        /// 操作人员ID
        /// </summary>
        public int OperatorID
        {
            get { return x.GetInt32("OperatorID"); }
            set { x.SetValue("OperatorID", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_ProductAgio()
        {
            x.Init("BSC_ProductAgio");
        }
    }
    [Serializable]
    public class BSC_PromotionRule : BaseEntity
    {
        /// <summary>
        /// 商品促销规则ID
        /// </summary>
        public int PromotionRuleID
        {
            get { return x.GetInt32("PromotionRuleID"); }
            set { x.SetValue("PromotionRuleID", value); }
        }

        /// <summary>
        /// 商品促销名称
        /// </summary>
        public string PromotionRuleName
        {
            get { return x.GetString("PromotionRuleName"); }
            set { x.SetValue("PromotionRuleName", value); }
        }

        /// <summary>
        /// 优惠方案ID
        /// </summary>
        public int PriceConcessionGroupID
        {
            get { return x.GetInt32("PriceConcessionGroupID"); }
            set { x.SetValue("PriceConcessionGroupID", value); }
        }

        /// <summary>
        /// 条件对象
        /// </summary>
        public int ConditionTarget
        {
            get { return x.GetInt32("ConditionTarget"); }
            set { x.SetValue("ConditionTarget", value); }
        }

        /// <summary>
        /// 条件模式
        /// </summary>
        public int ConditionMode
        {
            get { return x.GetInt32("ConditionMode"); }
            set { x.SetValue("ConditionMode", value); }
        }

        /// <summary>
        /// 条件商品范围ID
        /// </summary>
        public int ConditionProductScopeID
        {
            get { return x.GetInt32("ConditionProductScopeID"); }
            set { x.SetValue("ConditionProductScopeID", value); }
        }

        /// <summary>
        /// 条件参数列表
        /// </summary>
        public string ConditionParameterList
        {
            get { return x.GetString("ConditionParameterList"); }
            set { x.SetValue("ConditionParameterList", value); }
        }

        /// <summary>
        /// 执行对象
        /// </summary>
        public int ImplementTarget
        {
            get { return x.GetInt32("ImplementTarget"); }
            set { x.SetValue("ImplementTarget", value); }
        }

        /// <summary>
        /// 执行模式
        /// </summary>
        public int ImplementMode
        {
            get { return x.GetInt32("ImplementMode"); }
            set { x.SetValue("ImplementMode", value); }
        }

        /// <summary>
        /// 执行商品范围ID
        /// </summary>
        public int ImplementProductScopeID
        {
            get { return x.GetInt32("ImplementProductScopeID"); }
            set { x.SetValue("ImplementProductScopeID", value); }
        }

        /// <summary>
        /// 执行参数列表
        /// </summary>
        public string ImplementParameterList
        {
            get { return x.GetString("ImplementParameterList"); }
            set { x.SetValue("ImplementParameterList", value); }
        }

        /// <summary>
        /// 执行数量上限
        /// </summary>
        public int ImplementMaxQuantity
        {
            get { return x.GetInt32("ImplementMaxQuantity"); }
            set { x.SetValue("ImplementMaxQuantity", value); }
        }

        /// <summary>
        /// 开始日期
        /// </summary>
        public string BeginDate
        {
            get { return x.GetString("BeginDate"); }
            set { x.SetValue("BeginDate", value); }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndDate
        {
            get { return x.GetString("EndDate"); }
            set { x.SetValue("EndDate", value); }
        }

        /// <summary>
        /// 优先权
        /// </summary>
        public int Priority
        {
            get { return x.GetInt32("Priority"); }
            set { x.SetValue("Priority", value); }
        }

        /// <summary>
        /// 折上折
        /// </summary>
        public int DoubleAgio
        {
            get { return x.GetInt32("DoubleAgio"); }
            set { x.SetValue("DoubleAgio", value); }
        }

        /// <summary>
        /// 操作人员ID
        /// </summary>
        public int OperatorID
        {
            get { return x.GetInt32("OperatorID"); }
            set { x.SetValue("OperatorID", value); }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime
        {
            get { return x.GetDateTime("OperateTime"); }
            set { x.SetValue("OperateTime", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_PromotionRule()
        {
            x.Init("BSC_PromotionRule");
        }
    }
    [Serializable]
    public class BSC_ProductHint : BaseEntity
    {
        /// <summary>
        /// 商品提示ID
        /// </summary>
        public int ProductHintID
        {
            get { return x.GetInt32("ProductHintID"); }
            set { x.SetValue("ProductHintID", value); }
        }

        /// <summary>
        /// 商品范围ID
        /// </summary>
        public int ProductScopeID
        {
            get { return x.GetInt32("ProductScopeID"); }
            set { x.SetValue("ProductScopeID", value); }
        }

        /// <summary>
        /// 提示图片代码
        /// </summary>
        public string HintImageCode
        {
            get { return x.GetString("HintImageCode"); }
            set { x.SetValue("HintImageCode", value); }
        }

        /// <summary>
        /// 提示标题
        /// </summary>
        public string HintTitle
        {
            get { return x.GetString("HintTitle"); }
            set { x.SetValue("HintTitle", value); }
        }

        public BSC_ProductHint()
        {
            x.Init("BSC_ProductHint");
        }
    }
    [Serializable]
    public class BSC_ProductPromotion : BaseEntity
    {
        /// <summary>
        /// 商品祖训规则ID
        /// </summary>
        public int ProductPromotionID
        {
            get { return x.GetInt32("ProductPromotionID"); }
            set { x.SetValue("ProductPromotionID", value); }
        }

        /// <summary>
        /// 商品促销名称
        /// </summary>
        public string ProductPromotionName
        {
            get { return x.GetString("ProductPromotionName"); }
            set { x.SetValue("ProductPromotionName", value); }
        }

        /// <summary>
        /// 优惠方案ID
        /// </summary>
        public int PriceConcessionGroupID
        {
            get { return x.GetInt32("PriceConcessionGroupID"); }
            set { x.SetValue("PriceConcessionGroupID", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int ProductSpecID
        {
            get { return x.GetInt32("ProductSpecID"); }
            set { x.SetValue("ProductSpecID", value); }
        }

        /// <summary>
        /// 商品类别ID
        /// </summary>
        public int ProductCategoryID
        {
            get { return x.GetInt32("ProductCategoryID"); }
            set { x.SetValue("ProductCategoryID", value); }
        }

        /// <summary>
        /// 商品品牌ID
        /// </summary>
        public int ProductBrandID
        {
            get { return x.GetInt32("ProductBrandID"); }
            set { x.SetValue("ProductBrandID", value); }
        }

        /// <summary>
        /// 商品范围ID
        /// </summary>
        public int ProductScopeID
        {
            get { return x.GetInt32("ProductScopeID"); }
            set { x.SetValue("ProductScopeID", value); }
        }

        /// <summary>
        /// 促销条件模式
        /// </summary>
        public int ConditionMode
        {
            get { return x.GetInt32("ConditionMode"); }
            set { x.SetValue("ConditionMode", value); }
        }

        /// <summary>
        /// 促销享受模式
        /// </summary>
        public int EnjoyMode
        {
            get { return x.GetInt32("EnjoyMode"); }
            set { x.SetValue("EnjoyMode", value); }
        }

        /// <summary>
        /// 参数列表
        /// </summary>
        public string ParameterList
        {
            get { return x.GetString("ParameterList"); }
            set { x.SetValue("ParameterList", value); }
        }

        /// <summary>
        /// 赠品ID
        /// </summary>
        public int LargessID
        {
            get { return x.GetInt32("LargessID"); }
            set { x.SetValue("LargessID", value); }
        }

        /// <summary>
        /// 赠品规格ID
        /// </summary>
        public int LargessSpecID
        {
            get { return x.GetInt32("LargessSpecID"); }
            set { x.SetValue("LargessSpecID", value); }
        }

        /// <summary>
        /// 开始日期
        /// </summary>
        public string BeginDate
        {
            get { return x.GetString("BeginDate"); }
            set { x.SetValue("BeginDate", value); }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndDate
        {
            get { return x.GetString("EndDate"); }
            set { x.SetValue("EndDate", value); }
        }

        /// <summary>
        /// 优先权
        /// </summary>
        public int Priority
        {
            get { return x.GetInt32("Priority"); }
            set { x.SetValue("Priority", value); }
        }

        /// <summary>
        /// 折上折
        /// </summary>
        public int DoubleAgio
        {
            get { return x.GetInt32("DoubleAgio"); }
            set { x.SetValue("DoubleAgio", value); }
        }

        /// <summary>
        /// 操作人员ID
        /// </summary>
        public int OperatorID
        {
            get { return x.GetInt32("OperatorID"); }
            set { x.SetValue("OperatorID", value); }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime
        {
            get { return x.GetDateTime("OperateTime"); }
            set { x.SetValue("OperateTime", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_ProductPromotion()
        {
            x.Init("BSC_ProductPromotion");
        }
    }
    [Serializable]
    public class BSC_OrderPromotion : BaseEntity
    {
        /// <summary>
        /// 订单促销ID
        /// </summary>
        public int OrderPromotionID
        {
            get { return x.GetInt32("OrderPromotionID"); }
            set { x.SetValue("OrderPromotionID", value); }
        }

        /// <summary>
        /// 订单促销名称
        /// </summary>
        public string OrderPromotionName
        {
            get { return x.GetString("OrderPromotionName"); }
            set { x.SetValue("OrderPromotionName", value); }
        }

        /// <summary>
        /// 优惠方案ID
        /// </summary>
        public int PriceConcessionGroupID
        {
            get { return x.GetInt32("PriceConcessionGroupID"); }
            set { x.SetValue("PriceConcessionGroupID", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int ProductSpecID
        {
            get { return x.GetInt32("ProductSpecID"); }
            set { x.SetValue("ProductSpecID", value); }
        }

        /// <summary>
        /// 商品类别ID
        /// </summary>
        public int ProductCategoryID
        {
            get { return x.GetInt32("ProductCategoryID"); }
            set { x.SetValue("ProductCategoryID", value); }
        }

        /// <summary>
        /// 商品品牌ID
        /// </summary>
        public int ProductBrandID
        {
            get { return x.GetInt32("ProductBrandID"); }
            set { x.SetValue("ProductBrandID", value); }
        }

        /// <summary>
        /// 商品范围ID
        /// </summary>
        public int ProductScopeID
        {
            get { return x.GetInt32("ProductScopeID"); }
            set { x.SetValue("ProductScopeID", value); }
        }

        /// <summary>
        /// 促销条件模式
        /// </summary>
        public int ConditionMode
        {
            get { return x.GetInt32("ConditionMode"); }
            set { x.SetValue("ConditionMode", value); }
        }

        /// <summary>
        /// 促销享受模式
        /// </summary>
        public int EnjoyMode
        {
            get { return x.GetInt32("EnjoyMode"); }
            set { x.SetValue("EnjoyMode", value); }
        }

        /// <summary>
        /// 参数列表
        /// </summary>
        public string ParameterList
        {
            get { return x.GetString("ParameterList"); }
            set { x.SetValue("ParameterList", value); }
        }

        /// <summary>
        /// 赠品ID
        /// </summary>
        public int LargessID
        {
            get { return x.GetInt32("LargessID"); }
            set { x.SetValue("LargessID", value); }
        }

        /// <summary>
        /// 赠品规格ID
        /// </summary>
        public int LargessSpecID
        {
            get { return x.GetInt32("LargessSpecID"); }
            set { x.SetValue("LargessSpecID", value); }
        }

        /// <summary>
        /// 开始日期
        /// </summary>
        public string BeginDate
        {
            get { return x.GetString("BeginDate"); }
            set { x.SetValue("BeginDate", value); }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndDate
        {
            get { return x.GetString("EndDate"); }
            set { x.SetValue("EndDate", value); }
        }

        /// <summary>
        /// 优先权
        /// </summary>
        public int Priority
        {
            get { return x.GetInt32("Priority"); }
            set { x.SetValue("Priority", value); }
        }

        /// <summary>
        /// 操作人员ID
        /// </summary>
        public int OperatorID
        {
            get { return x.GetInt32("OperatorID"); }
            set { x.SetValue("OperatorID", value); }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime
        {
            get { return x.GetDateTime("OperateTime"); }
            set { x.SetValue("OperateTime", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_OrderPromotion()
        {
            x.Init("BSC_OrderPromotion");
        }
    }
    [Serializable]
    public class BSC_ProductScope : BaseEntity
    {
        /// <summary>
        /// 商品范围ID
        /// </summary>
        public int ProductScopeID
        {
            get { return x.GetInt32("ProductScopeID"); }
            set { x.SetValue("ProductScopeID", value); }
        }

        /// <summary>
        /// 商品范围名称
        /// </summary>
        public string ProductScopeName
        {
            get { return x.GetString("ProductScopeName"); }
            set { x.SetValue("ProductScopeName", value); }
        }

        /// <summary>
        /// 操作日期
        /// </summary>
        public string OperatorDate
        {
            get { return x.GetString("OperatorDate"); }
            set { x.SetValue("OperatorDate", value); }
        }

        /// <summary>
        /// 操作人员ID
        /// </summary>
        public int OperatorID
        {
            get { return x.GetInt32("OperatorID"); }
            set { x.SetValue("OperatorID", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_ProductScope()
        {
            x.Init("BSC_ProductScope");
        }
    }
    [Serializable]
    public class BSC_ProductScopeItem : BaseEntity
    {
        /// <summary>
        /// 商品范围ID
        /// </summary>
        public int ProductScopeID
        {
            get { return x.GetInt32("ProductScopeID"); }
            set { x.SetValue("ProductScopeID", value); }
        }

        /// <summary>
        /// 商品范围明细ID
        /// </summary>
        public int ProductScopeItemID
        {
            get { return x.GetInt32("ProductScopeItemID"); }
            set { x.SetValue("ProductScopeItemID", value); }
        }

        /// <summary>
        /// 商品范围明细方式
        /// </summary>
        public int ProductScopeItemMode
        {
            get { return x.GetInt32("ProductScopeItemMode"); }
            set { x.SetValue("ProductScopeItemMode", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int ProductSpecID
        {
            get { return x.GetInt32("ProductSpecID"); }
            set { x.SetValue("ProductSpecID", value); }
        }

        /// <summary>
        /// 商品类别ID
        /// </summary>
        public int ProductCategoryID
        {
            get { return x.GetInt32("ProductCategoryID"); }
            set { x.SetValue("ProductCategoryID", value); }
        }

        /// <summary>
        /// 商品品牌ID
        /// </summary>
        public int ProductBrandID
        {
            get { return x.GetInt32("ProductBrandID"); }
            set { x.SetValue("ProductBrandID", value); }
        }

        public BSC_ProductScopeItem()
        {
            x.Init("BSC_ProductScopeItem");
        }
    }
    [Serializable]
    public class BSC_ProductScopeExpressFee : BaseEntity
    {
        /// <summary>
        /// 商品范围ID
        /// </summary>
        public int ProductScopeID
        {
            get { return x.GetInt32("ProductScopeID"); }
            set { x.SetValue("ProductScopeID", value); }
        }

        /// <summary>
        /// 区域代码列表
        /// </summary>
        public string AreaCodeList
        {
            get { return x.GetString("AreaCodeList"); }
            set { x.SetValue("AreaCodeList", value); }
        }

        /// <summary>
        /// 固定费用
        /// </summary>
        public double FixedFee
        {
            get { return x.GetDouble("FixedFee"); }
            set { x.SetValue("FixedFee", value); }
        }

        /// <summary>
        /// 免邮重量
        /// </summary>
        public double FreeWeight
        {
            get { return x.GetDouble("FreeWeight"); }
            set { x.SetValue("FreeWeight", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_ProductScopeExpressFee()
        {
            x.Init("BSC_ProductScopeExpressFee");
        }
    }
    [Serializable]
    public class BSC_CouponGrantAmountConfig : BaseEntity
    {
        /// <summary>
        /// 优惠劵发放金额配置ID
        /// </summary>
        public int CouponGrantAmountConfigID
        {
            get { return x.GetInt32("CouponGrantAmountConfigID"); }
            set { x.SetValue("CouponGrantAmountConfigID", value); }
        }

        /// <summary>
        /// 开始金额
        /// </summary>
        public double BeginAmount
        {
            get { return x.GetDouble("BeginAmount"); }
            set { x.SetValue("BeginAmount", value); }
        }

        /// <summary>
        /// 结束金额
        /// </summary>
        public double EndAmount
        {
            get { return x.GetDouble("EndAmount"); }
            set { x.SetValue("EndAmount", value); }
        }

        /// <summary>
        /// 每张面值
        /// </summary>
        public double ParValue
        {
            get { return x.GetDouble("ParValue"); }
            set { x.SetValue("ParValue", value); }
        }

        /// <summary>
        /// 发放数量
        /// </summary>
        public int Count
        {
            get { return x.GetInt32("Count"); }
            set { x.SetValue("Count", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_CouponGrantAmountConfig()
        {
            x.Init("BSC_CouponGrantAmountConfig");
        }
    }
    [Serializable]
    public class BSC_PointConfig : BaseEntity
    {
        /// <summary>
        /// 积分模式
        /// </summary>
        public int PointMode
        {
            get { return x.GetInt32("PointMode"); }
            set { x.SetValue("PointMode", value); }
        }

        /// <summary>
        /// 积分值
        /// </summary>
        public int PointValue
        {
            get { return x.GetInt32("PointValue"); }
            set { x.SetValue("PointValue", value); }
        }

        /// <summary>
        /// 最小消费积分金额
        /// </summary>
        public int MinAmountToPoint
        {
            get { return x.GetInt32("MinAmountToPoint"); }
            set { x.SetValue("MinAmountToPoint", value); }
        }

        public BSC_PointConfig()
        {
            x.Init("BSC_PointConfig");
        }
    }
    [Serializable]
    public class BSC_ProductCategoryPointConfig : BaseEntity
    {
        /// <summary>
        /// 商品类型ID
        /// </summary>
        public int ProductCategoryID
        {
            get { return x.GetInt32("ProductCategoryID"); }
            set { x.SetValue("ProductCategoryID", value); }
        }

        /// <summary>
        /// 积分模式
        /// </summary>
        public int PointMode
        {
            get { return x.GetInt32("PointMode"); }
            set { x.SetValue("PointMode", value); }
        }

        /// <summary>
        /// 积分值
        /// </summary>
        public int PointValue
        {
            get { return x.GetInt32("PointValue"); }
            set { x.SetValue("PointValue", value); }
        }

        public BSC_ProductCategoryPointConfig()
        {
            x.Init("BSC_ProductCategoryPointConfig");
        }
    }
    [Serializable]
    public class BSC_ProductPointConfig : BaseEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 积分模式
        /// </summary>
        public int PointMode
        {
            get { return x.GetInt32("PointMode"); }
            set { x.SetValue("PointMode", value); }
        }

        /// <summary>
        /// 积分值
        /// </summary>
        public int PointValue
        {
            get { return x.GetInt32("PointValue"); }
            set { x.SetValue("PointValue", value); }
        }

        public BSC_ProductPointConfig()
        {
            x.Init("BSC_ProductPointConfig");
        }
    }
    [Serializable]
    public class BSC_ExchangeProduct : BaseEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int ProductSpecID
        {
            get { return x.GetInt32("ProductSpecID"); }
            set { x.SetValue("ProductSpecID", value); }
        }

        /// <summary>
        /// 积分值
        /// </summary>
        public int PointValue
        {
            get { return x.GetInt32("PointValue"); }
            set { x.SetValue("PointValue", value); }
        }

        public BSC_ExchangeProduct()
        {
            x.Init("BSC_ExchangeProduct");
        }
    }
    [Serializable]
    public class BSC_LimitSalesProduct : BaseEntity
    {
        /// <summary>
        /// 限购商品ID
        /// </summary>
        public int LimitSalesProductID
        {
            get { return x.GetInt32("LimitSalesProductID"); }
            set { x.SetValue("LimitSalesProductID", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int ProductSpecID
        {
            get { return x.GetInt32("ProductSpecID"); }
            set { x.SetValue("ProductSpecID", value); }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return x.GetString("Title"); }
            set { x.SetValue("Title", value); }
        }

        /// <summary>
        /// 销售价格
        /// </summary>
        public double SalesPrice
        {
            get { return x.GetDouble("SalesPrice"); }
            set { x.SetValue("SalesPrice", value); }
        }

        /// <summary>
        /// 限购价格
        /// </summary>
        public double LimitSalesPrice
        {
            get { return x.GetDouble("LimitSalesPrice"); }
            set { x.SetValue("LimitSalesPrice", value); }
        }

        /// <summary>
        /// 折扣
        /// </summary>
        public double Agio
        {
            get { return x.GetDouble("Agio"); }
            set { x.SetValue("Agio", value); }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime
        {
            get { return x.GetDateTime("BeginTime"); }
            set { x.SetValue("BeginTime", value); }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return x.GetDateTime("EndTime"); }
            set { x.SetValue("EndTime", value); }
        }

        /// <summary>
        /// 销售总数量
        /// </summary>
        public double TotalQuantity
        {
            get { return x.GetDouble("TotalQuantity"); }
            set { x.SetValue("TotalQuantity", value); }
        }

        /// <summary>
        /// 每单限购数量
        /// </summary>
        public double BillMaxQuantity
        {
            get { return x.GetDouble("BillMaxQuantity"); }
            set { x.SetValue("BillMaxQuantity", value); }
        }

        /// <summary>
        /// 会员限购数量
        /// </summary>
        public double MemberMaxQuantity
        {
            get { return x.GetDouble("MemberMaxQuantity"); }
            set { x.SetValue("MemberMaxQuantity", value); }
        }

        /// <summary>
        /// 每天限购数量
        /// </summary>
        public double DayMaxQuantity
        {
            get { return x.GetDouble("DayMaxQuantity"); }
            set { x.SetValue("DayMaxQuantity", value); }
        }

        /// <summary>
        /// 推荐序号
        /// </summary>
        public int RecommendOrder
        {
            get { return x.GetInt32("RecommendOrder"); }
            set { x.SetValue("RecommendOrder", value); }
        }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int HitCount
        {
            get { return x.GetInt32("HitCount"); }
            set { x.SetValue("HitCount", value); }
        }

        /// <summary>
        /// 推荐标题
        /// </summary>
        public string RecommendTitle
        {
            get { return x.GetString("RecommendTitle"); }
            set { x.SetValue("RecommendTitle", value); }
        }

        /// <summary>
        /// 推荐摘要
        /// </summary>
        public string RecommendBrief
        {
            get { return x.GetString("RecommendBrief"); }
            set { x.SetValue("RecommendBrief", value); }
        }

        /// <summary>
        /// 权重值
        /// </summary>
        public int WeightValue
        {
            get { return x.GetInt32("WeightValue"); }
            set { x.SetValue("WeightValue", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_LimitSalesProduct()
        {
            x.Init("BSC_LimitSalesProduct");
        }
    }
    [Serializable]
    public class BSC_HelpItem : BaseEntity
    {
        /// <summary>
        /// 菜单代码
        /// </summary>
        public string HelpCode
        {
            get { return x.GetString("HelpCode"); }
            set { x.SetValue("HelpCode", value); }
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string HelpName
        {
            get { return x.GetString("HelpName"); }
            set { x.SetValue("HelpName", value); }
        }

        /// <summary>
        /// 菜单内容
        /// </summary>
        public string HelpContent
        {
            get { return x.GetString("HelpContent"); }
            set { x.SetValue("HelpContent", value); }
        }

        public BSC_HelpItem()
        {
            x.Init("BSC_HelpItem");
        }
    }
    [Serializable]
    public class SLS_Batch : BaseEntity
    {
        /// <summary>
        /// 订单批次ID
        /// </summary>
        public int BatchID
        {
            get { return x.GetInt32("BatchID"); }
            set { x.SetValue("BatchID", value); }
        }

        /// <summary>
        /// 批次日期
        /// </summary>
        public string BatchDate
        {
            get { return x.GetString("BatchDate"); }
            set { x.SetValue("BatchDate", value); }
        }

        /// <summary>
        /// 批次序号
        /// </summary>
        public int BatchOrder
        {
            get { return x.GetInt32("BatchOrder"); }
            set { x.SetValue("BatchOrder", value); }
        }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo
        {
            get { return x.GetString("BatchNo"); }
            set { x.SetValue("BatchNo", value); }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime FromTime
        {
            get { return x.GetDateTime("FromTime"); }
            set { x.SetValue("FromTime", value); }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime ToTime
        {
            get { return x.GetDateTime("ToTime"); }
            set { x.SetValue("ToTime", value); }
        }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalQuantity
        {
            get { return x.GetInt32("TotalQuantity"); }
            set { x.SetValue("TotalQuantity", value); }
        }

        /// <summary>
        /// 总金额
        /// </summary>
        public double TotalAmount
        {
            get { return x.GetDouble("TotalAmount"); }
            set { x.SetValue("TotalAmount", value); }
        }

        /// <summary>
        /// 确认订单数量
        /// </summary>
        public int ConfirmOrderCount
        {
            get { return x.GetInt32("ConfirmOrderCount"); }
            set { x.SetValue("ConfirmOrderCount", value); }
        }

        /// <summary>
        /// 确认总数量
        /// </summary>
        public double ConfirmTotalQuantity
        {
            get { return x.GetDouble("ConfirmTotalQuantity"); }
            set { x.SetValue("ConfirmTotalQuantity", value); }
        }

        /// <summary>
        /// 确认总金额
        /// </summary>
        public double ConfirmTotalAmount
        {
            get { return x.GetDouble("ConfirmTotalAmount"); }
            set { x.SetValue("ConfirmTotalAmount", value); }
        }

        /// <summary>
        /// 拣货订单数量
        /// </summary>
        public int PickOrderCount
        {
            get { return x.GetInt32("PickOrderCount"); }
            set { x.SetValue("PickOrderCount", value); }
        }

        /// <summary>
        /// 拣货总数量
        /// </summary>
        public double PickTotalQuantity
        {
            get { return x.GetDouble("PickTotalQuantity"); }
            set { x.SetValue("PickTotalQuantity", value); }
        }

        /// <summary>
        /// 拣货总金额
        /// </summary>
        public double PickTotalAmount
        {
            get { return x.GetDouble("PickTotalAmount"); }
            set { x.SetValue("PickTotalAmount", value); }
        }

        /// <summary>
        /// 打包订单数量
        /// </summary>
        public int PackOrderCount
        {
            get { return x.GetInt32("PackOrderCount"); }
            set { x.SetValue("PackOrderCount", value); }
        }

        /// <summary>
        /// 打包总数量
        /// </summary>
        public double PackTotalQuantity
        {
            get { return x.GetDouble("PackTotalQuantity"); }
            set { x.SetValue("PackTotalQuantity", value); }
        }

        /// <summary>
        /// 打包总金额
        /// </summary>
        public double PackTotalAmount
        {
            get { return x.GetDouble("PackTotalAmount"); }
            set { x.SetValue("PackTotalAmount", value); }
        }

        /// <summary>
        /// 配送订单数量
        /// </summary>
        public int DeliverOrderCount
        {
            get { return x.GetInt32("DeliverOrderCount"); }
            set { x.SetValue("DeliverOrderCount", value); }
        }

        /// <summary>
        /// 配送总数量
        /// </summary>
        public double DeliverTotalQuantity
        {
            get { return x.GetDouble("DeliverTotalQuantity"); }
            set { x.SetValue("DeliverTotalQuantity", value); }
        }

        /// <summary>
        /// 配送总金额
        /// </summary>
        public double DeliverTotalAmount
        {
            get { return x.GetDouble("DeliverTotalAmount"); }
            set { x.SetValue("DeliverTotalAmount", value); }
        }

        /// <summary>
        /// 完成订单数量
        /// </summary>
        public int FinishOrderCount
        {
            get { return x.GetInt32("FinishOrderCount"); }
            set { x.SetValue("FinishOrderCount", value); }
        }

        /// <summary>
        /// 完成总数量
        /// </summary>
        public double FinishTotalQuantity
        {
            get { return x.GetDouble("FinishTotalQuantity"); }
            set { x.SetValue("FinishTotalQuantity", value); }
        }

        /// <summary>
        /// 完成总金额
        /// </summary>
        public double FinishTotalAmount
        {
            get { return x.GetDouble("FinishTotalAmount"); }
            set { x.SetValue("FinishTotalAmount", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public SLS_Batch()
        {
            x.Init("SLS_Batch");
        }
    }
    [Serializable]
    public class SLS_Order : BaseEntity
    {
        /// <summary>
        /// 批次ID
        /// </summary>
        public int BatchID
        {
            get { return x.GetInt32("BatchID"); }
            set { x.SetValue("BatchID", value); }
        }

        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderID
        {
            get { return x.GetInt32("OrderID"); }
            set { x.SetValue("OrderID", value); }
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo
        {
            get { return x.GetString("OrderNo"); }
            set { x.SetValue("OrderNo", value); }
        }

        /// <summary>
        /// 订单日期
        /// </summary>
        public string OrderDate
        {
            get { return x.GetString("OrderDate"); }
            set { x.SetValue("OrderDate", value); }
        }

        /// <summary>
        /// 订单时间
        /// </summary>
        public string OrderTime
        {
            get { return x.GetString("OrderTime"); }
            set { x.SetValue("OrderTime", value); }
        }

        /// <summary>
        /// 条形码
        /// </summary>
        public string BarCode
        {
            get { return x.GetString("BarCode"); }
            set { x.SetValue("BarCode", value); }
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return x.GetInt32("MemberID"); }
            set { x.SetValue("MemberID", value); }
        }

        /// <summary>
        /// 购物类型
        /// </summary>
        public int ShoppingType
        {
            get { return x.GetInt32("ShoppingType"); }
            set { x.SetValue("ShoppingType", value); }
        }

        /// <summary>
        /// 总数量
        /// </summary>
        public double TotalQuantity
        {
            get { return x.GetDouble("TotalQuantity"); }
            set { x.SetValue("TotalQuantity", value); }
        }

        /// <summary>
        /// 总金额
        /// </summary>
        public double TotalAmount
        {
            get { return x.GetDouble("TotalAmount"); }
            set { x.SetValue("TotalAmount", value); }
        }

        /// <summary>
        /// 总重量
        /// </summary>
        public double TotalWeight
        {
            get { return x.GetDouble("TotalWeight"); }
            set { x.SetValue("TotalWeight", value); }
        }

        /// <summary>
        /// 送货费用
        /// </summary>
        public double DeliverAmount
        {
            get { return x.GetDouble("DeliverAmount"); }
            set { x.SetValue("DeliverAmount", value); }
        }

        /// <summary>
        /// 优惠卷支付金额
        /// </summary>
        public double CouponPayAmount
        {
            get { return x.GetDouble("CouponPayAmount"); }
            set { x.SetValue("CouponPayAmount", value); }
        }

        /// <summary>
        /// 应付总金额
        /// </summary>
        public double TotalPayAmount
        {
            get { return x.GetDouble("TotalPayAmount"); }
            set { x.SetValue("TotalPayAmount", value); }
        }

        /// <summary>
        /// 合计兑换积分值
        /// </summary>
        public int TotalExchangePointValue
        {
            get { return x.GetInt32("TotalExchangePointValue"); }
            set { x.SetValue("TotalExchangePointValue", value); }
        }

        /// <summary>
        /// 总积分
        /// </summary>
        public int TotalPointValue
        {
            get { return x.GetInt32("TotalPointValue"); }
            set { x.SetValue("TotalPointValue", value); }
        }

        /// <summary>
        /// 会员地址ID
        /// </summary>
        public int MemberAddressID
        {
            get { return x.GetInt32("MemberAddressID"); }
            set { x.SetValue("MemberAddressID", value); }
        }

        /// <summary>
        /// 会员发票ID
        /// </summary>
        public int MemberInvoiceID
        {
            get { return x.GetInt32("MemberInvoiceID"); }
            set { x.SetValue("MemberInvoiceID", value); }
        }

        /// <summary>
        /// 支付配送方式
        /// </summary>
        public int PaymentDeliverType
        {
            get { return x.GetInt32("PaymentDeliverType"); }
            set { x.SetValue("PaymentDeliverType", value); }
        }

        /// <summary>
        /// 快递公司代码
        /// </summary>
        public string ExpressCompanyCode
        {
            get { return x.GetString("ExpressCompanyCode"); }
            set { x.SetValue("ExpressCompanyCode", value); }
        }

        /// <summary>
        /// 送货日期类型
        /// </summary>
        public int DeliverDateType
        {
            get { return x.GetInt32("DeliverDateType"); }
            set { x.SetValue("DeliverDateType", value); }
        }

        /// <summary>
        /// 送货前电话通知
        /// </summary>
        public int NotifyBeforeDeliver
        {
            get { return x.GetInt32("NotifyBeforeDeliver"); }
            set { x.SetValue("NotifyBeforeDeliver", value); }
        }

        /// <summary>
        /// 付款方式
        /// </summary>
        public int PaymentMode
        {
            get { return x.GetInt32("PaymentMode"); }
            set { x.SetValue("PaymentMode", value); }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return x.GetDateTime("CreateTime"); }
            set { x.SetValue("CreateTime", value); }
        }

        /// <summary>
        /// 配送员ID
        /// </summary>
        public int DeliverWorkerID
        {
            get { return x.GetInt32("DeliverWorkerID"); }
            set { x.SetValue("DeliverWorkerID", value); }
        }

        /// <summary>
        /// 会员评论ID
        /// </summary>
        public int MemberReviewID
        {
            get { return x.GetInt32("MemberReviewID"); }
            set { x.SetValue("MemberReviewID", value); }
        }

        /// <summary>
        /// 批次状态
        /// </summary>
        public int BatchStatus
        {
            get { return x.GetInt32("BatchStatus"); }
            set { x.SetValue("BatchStatus", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public SLS_Order()
        {
            x.Init("SLS_Order");
        }
    }
    [Serializable]
    public class SLS_OrderExtension : BaseEntity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderID
        {
            get { return x.GetInt32("OrderID"); }
            set { x.SetValue("OrderID", value); }
        }

        /// <summary>
        /// 确认时间
        /// </summary>
        public string ConfirmTime
        {
            get { return x.GetString("ConfirmTime"); }
            set { x.SetValue("ConfirmTime", value); }
        }

        /// <summary>
        /// 确认操作员ID
        /// </summary>
        public int ConfirmOperatorID
        {
            get { return x.GetInt32("ConfirmOperatorID"); }
            set { x.SetValue("ConfirmOperatorID", value); }
        }

        /// <summary>
        /// 拣货时间
        /// </summary>
        public string PickTime
        {
            get { return x.GetString("PickTime"); }
            set { x.SetValue("PickTime", value); }
        }

        /// <summary>
        /// 拣货操作员ID
        /// </summary>
        public int PickOperatorID
        {
            get { return x.GetInt32("PickOperatorID"); }
            set { x.SetValue("PickOperatorID", value); }
        }

        /// <summary>
        /// 打包时间
        /// </summary>
        public string PackTime
        {
            get { return x.GetString("PackTime"); }
            set { x.SetValue("PackTime", value); }
        }

        /// <summary>
        /// 打包操作员ID
        /// </summary>
        public int PackOperatorID
        {
            get { return x.GetInt32("PackOperatorID"); }
            set { x.SetValue("PackOperatorID", value); }
        }

        /// <summary>
        /// 配送时间
        /// </summary>
        public string DeliverTime
        {
            get { return x.GetString("DeliverTime"); }
            set { x.SetValue("DeliverTime", value); }
        }

        /// <summary>
        /// 配送操作员ID
        /// </summary>
        public int DeliverOperatorID
        {
            get { return x.GetInt32("DeliverOperatorID"); }
            set { x.SetValue("DeliverOperatorID", value); }
        }

        /// <summary>
        /// 完成时间
        /// </summary>
        public string FinishTime
        {
            get { return x.GetString("FinishTime"); }
            set { x.SetValue("FinishTime", value); }
        }

        /// <summary>
        /// 完成操作员ID
        /// </summary>
        public int FinishOperatorID
        {
            get { return x.GetInt32("FinishOperatorID"); }
            set { x.SetValue("FinishOperatorID", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        public SLS_OrderExtension()
        {
            x.Init("SLS_OrderExtension");
        }
    }
    [Serializable]
    public class SLS_OrderItem : BaseEntity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderID
        {
            get { return x.GetInt32("OrderID"); }
            set { x.SetValue("OrderID", value); }
        }

        /// <summary>
        /// 订单明细ID
        /// </summary>
        public int OrderItemID
        {
            get { return x.GetInt32("OrderItemID"); }
            set { x.SetValue("OrderItemID", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int ProductSpecID
        {
            get { return x.GetInt32("ProductSpecID"); }
            set { x.SetValue("ProductSpecID", value); }
        }

        /// <summary>
        /// 销售价格
        /// </summary>
        public double SalesPrice
        {
            get { return x.GetDouble("SalesPrice"); }
            set { x.SetValue("SalesPrice", value); }
        }

        /// <summary>
        /// 促销价格
        /// </summary>
        public double PromotionPrice
        {
            get { return x.GetDouble("PromotionPrice"); }
            set { x.SetValue("PromotionPrice", value); }
        }

        /// <summary>
        /// 折算价格
        /// </summary>
        public double ObversionPrice
        {
            get { return x.GetDouble("ObversionPrice"); }
            set { x.SetValue("ObversionPrice", value); }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public double Quantity
        {
            get { return x.GetDouble("Quantity"); }
            set { x.SetValue("Quantity", value); }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public double Amount
        {
            get { return x.GetDouble("Amount"); }
            set { x.SetValue("Amount", value); }
        }

        /// <summary>
        /// 兑换积分值
        /// </summary>
        public int ExchangePointValue
        {
            get { return x.GetInt32("ExchangePointValue"); }
            set { x.SetValue("ExchangePointValue", value); }
        }

        /// <summary>
        /// 小计兑换积分值
        /// </summary>
        public int SubtotalExchangePointValue
        {
            get { return x.GetInt32("SubtotalExchangePointValue"); }
            set { x.SetValue("SubtotalExchangePointValue", value); }
        }

        /// <summary>
        /// 积分
        /// </summary>
        public int PointValue
        {
            get { return x.GetInt32("PointValue"); }
            set { x.SetValue("PointValue", value); }
        }

        /// <summary>
        /// 限购标志
        /// </summary>
        public int LimitSalesFlag
        {
            get { return x.GetInt32("LimitSalesFlag"); }
            set { x.SetValue("LimitSalesFlag", value); }
        }

        /// <summary>
        /// 赠品标志
        /// </summary>
        public int LargessFlag
        {
            get { return x.GetInt32("LargessFlag"); }
            set { x.SetValue("LargessFlag", value); }
        }

        /// <summary>
        /// 促销备注
        /// </summary>
        public string PromotionRemark
        {
            get { return x.GetString("PromotionRemark"); }
            set { x.SetValue("PromotionRemark", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        public SLS_OrderItem()
        {
            x.Init("SLS_OrderItem");
        }
    }
    [Serializable]
    public class SLS_Repair : BaseEntity
    {
        /// <summary>
        /// 返修ID
        /// </summary>
        public int RepairID
        {
            get { return x.GetInt32("RepairID"); }
            set { x.SetValue("RepairID", value); }
        }

        /// <summary>
        /// 返修编号
        /// </summary>
        public string RepairNo
        {
            get { return x.GetString("RepairNo"); }
            set { x.SetValue("RepairNo", value); }
        }

        /// <summary>
        /// 申请日期
        /// </summary>
        public string ApplyDate
        {
            get { return x.GetString("ApplyDate"); }
            set { x.SetValue("ApplyDate", value); }
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return x.GetInt32("MemberID"); }
            set { x.SetValue("MemberID", value); }
        }

        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderID
        {
            get { return x.GetInt32("OrderID"); }
            set { x.SetValue("OrderID", value); }
        }

        /// <summary>
        /// 订单明细ID
        /// </summary>
        public int OrderItemID
        {
            get { return x.GetInt32("OrderItemID"); }
            set { x.SetValue("OrderItemID", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int ProductSpecID
        {
            get { return x.GetInt32("ProductSpecID"); }
            set { x.SetValue("ProductSpecID", value); }
        }

        /// <summary>
        /// 价格
        /// </summary>
        public double Price
        {
            get { return x.GetDouble("Price"); }
            set { x.SetValue("Price", value); }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public double Quantity
        {
            get { return x.GetDouble("Quantity"); }
            set { x.SetValue("Quantity", value); }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public double Amount
        {
            get { return x.GetDouble("Amount"); }
            set { x.SetValue("Amount", value); }
        }

        /// <summary>
        /// 兑换积分值
        /// </summary>
        public int ExchangePointValue
        {
            get { return x.GetInt32("ExchangePointValue"); }
            set { x.SetValue("ExchangePointValue", value); }
        }

        /// <summary>
        /// 小计兑换积分值
        /// </summary>
        public int SubtotalExchangePointValue
        {
            get { return x.GetInt32("SubtotalExchangePointValue"); }
            set { x.SetValue("SubtotalExchangePointValue", value); }
        }

        /// <summary>
        /// 积分
        /// </summary>
        public int PointValue
        {
            get { return x.GetInt32("PointValue"); }
            set { x.SetValue("PointValue", value); }
        }

        /// <summary>
        /// 处理方式
        /// </summary>
        public int RepairType
        {
            get { return x.GetInt32("RepairType"); }
            set { x.SetValue("RepairType", value); }
        }

        /// <summary>
        /// 问题描述
        /// </summary>
        public string ProblemContent
        {
            get { return x.GetString("ProblemContent"); }
            set { x.SetValue("ProblemContent", value); }
        }

        /// <summary>
        /// 附件清单
        /// </summary>
        public string AttachmentList
        {
            get { return x.GetString("AttachmentList"); }
            set { x.SetValue("AttachmentList", value); }
        }

        /// <summary>
        /// 凭证类型
        /// </summary>
        public int VoucherType
        {
            get { return x.GetInt32("VoucherType"); }
            set { x.SetValue("VoucherType", value); }
        }

        /// <summary>
        /// 商品返回方式
        /// </summary>
        public int BackType
        {
            get { return x.GetInt32("BackType"); }
            set { x.SetValue("BackType", value); }
        }

        /// <summary>
        /// 商品外观状态
        /// </summary>
        public int AppearanceStatus
        {
            get { return x.GetInt32("AppearanceStatus"); }
            set { x.SetValue("AppearanceStatus", value); }
        }

        /// <summary>
        /// 商品包装状态
        /// </summary>
        public int PackageStatus
        {
            get { return x.GetInt32("PackageStatus"); }
            set { x.SetValue("PackageStatus", value); }
        }

        /// <summary>
        /// 会员地址ID
        /// </summary>
        public int MemberAddressID
        {
            get { return x.GetInt32("MemberAddressID"); }
            set { x.SetValue("MemberAddressID", value); }
        }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime
        {
            get { return x.GetDateTime("ApplyTime"); }
            set { x.SetValue("ApplyTime", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public SLS_Repair()
        {
            x.Init("SLS_Repair");
        }
    }
    [Serializable]
    public class SLS_RepairExtension : BaseEntity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int RepairID
        {
            get { return x.GetInt32("RepairID"); }
            set { x.SetValue("RepairID", value); }
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        public string AuditTime
        {
            get { return x.GetString("AuditTime"); }
            set { x.SetValue("AuditTime", value); }
        }

        /// <summary>
        /// 审核操作员ID
        /// </summary>
        public int AuditOperatorID
        {
            get { return x.GetInt32("AuditOperatorID"); }
            set { x.SetValue("AuditOperatorID", value); }
        }

        /// <summary>
        /// 验货时间
        /// </summary>
        public string CheckTime
        {
            get { return x.GetString("CheckTime"); }
            set { x.SetValue("CheckTime", value); }
        }

        /// <summary>
        /// 验货操作员ID
        /// </summary>
        public int CheckOperatorID
        {
            get { return x.GetInt32("CheckOperatorID"); }
            set { x.SetValue("CheckOperatorID", value); }
        }

        /// <summary>
        /// 拣货时间
        /// </summary>
        public string PickTime
        {
            get { return x.GetString("PickTime"); }
            set { x.SetValue("PickTime", value); }
        }

        /// <summary>
        /// 拣货操作员ID
        /// </summary>
        public int PickOperatorID
        {
            get { return x.GetInt32("PickOperatorID"); }
            set { x.SetValue("PickOperatorID", value); }
        }

        /// <summary>
        /// 打包时间
        /// </summary>
        public string PackTime
        {
            get { return x.GetString("PackTime"); }
            set { x.SetValue("PackTime", value); }
        }

        /// <summary>
        /// 打包操作员ID
        /// </summary>
        public int PackOperatorID
        {
            get { return x.GetInt32("PackOperatorID"); }
            set { x.SetValue("PackOperatorID", value); }
        }

        /// <summary>
        /// 配送时间
        /// </summary>
        public string DeliverTime
        {
            get { return x.GetString("DeliverTime"); }
            set { x.SetValue("DeliverTime", value); }
        }

        /// <summary>
        /// 配送操作员ID
        /// </summary>
        public int DeliverOperatorID
        {
            get { return x.GetInt32("DeliverOperatorID"); }
            set { x.SetValue("DeliverOperatorID", value); }
        }

        /// <summary>
        /// 完成时间
        /// </summary>
        public string FinishTime
        {
            get { return x.GetString("FinishTime"); }
            set { x.SetValue("FinishTime", value); }
        }

        /// <summary>
        /// 完成操作员ID
        /// </summary>
        public int FinishOperatorID
        {
            get { return x.GetInt32("FinishOperatorID"); }
            set { x.SetValue("FinishOperatorID", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        public SLS_RepairExtension()
        {
            x.Init("SLS_RepairExtension");
        }
    }
    [Serializable]
    public class SLS_ShoppingCart : BaseEntity
    {
        /// <summary>
        /// 购物车ID
        /// </summary>
        public int ShoppingCartID
        {
            get { return x.GetInt32("ShoppingCartID"); }
            set { x.SetValue("ShoppingCartID", value); }
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return x.GetInt32("MemberID"); }
            set { x.SetValue("MemberID", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int ProductSpecID
        {
            get { return x.GetInt32("ProductSpecID"); }
            set { x.SetValue("ProductSpecID", value); }
        }

        /// <summary>
        /// 购物类型
        /// </summary>
        public int ShoppingType
        {
            get { return x.GetInt32("ShoppingType"); }
            set { x.SetValue("ShoppingType", value); }
        }

        /// <summary>
        /// 单价
        /// </summary>
        public double Price
        {
            get { return x.GetDouble("Price"); }
            set { x.SetValue("Price", value); }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public double Quantity
        {
            get { return x.GetDouble("Quantity"); }
            set { x.SetValue("Quantity", value); }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public double Amount
        {
            get { return x.GetDouble("Amount"); }
            set { x.SetValue("Amount", value); }
        }

        /// <summary>
        /// 兑换积分值
        /// </summary>
        public int ExchangePointValue
        {
            get { return x.GetInt32("ExchangePointValue"); }
            set { x.SetValue("ExchangePointValue", value); }
        }

        /// <summary>
        /// 小计兑换积分值
        /// </summary>
        public int SubtotalExchangePointValue
        {
            get { return x.GetInt32("SubtotalExchangePointValue"); }
            set { x.SetValue("SubtotalExchangePointValue", value); }
        }

        public SLS_ShoppingCart()
        {
            x.Init("SLS_ShoppingCart");
        }
    }
    [Serializable]
    public class SLS_FavoriteProduct : BaseEntity
    {
        /// <summary>
        /// 收藏商品ID
        /// </summary>
        public int FavoriteProductID
        {
            get { return x.GetInt32("FavoriteProductID"); }
            set { x.SetValue("FavoriteProductID", value); }
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return x.GetInt32("MemberID"); }
            set { x.SetValue("MemberID", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int ProductSpecID
        {
            get { return x.GetInt32("ProductSpecID"); }
            set { x.SetValue("ProductSpecID", value); }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return x.GetDateTime("CreateTime"); }
            set { x.SetValue("CreateTime", value); }
        }

        public SLS_FavoriteProduct()
        {
            x.Init("SLS_FavoriteProduct");
        }
    }
    [Serializable]
    public class IVT_ProductStock : BaseEntity
    {
        /// <summary>
        /// 商品库存ID
        /// </summary>
        public int ProductStockID
        {
            get { return x.GetInt32("ProductStockID"); }
            set { x.SetValue("ProductStockID", value); }
        }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public int WarehouseID
        {
            get { return x.GetInt32("WarehouseID"); }
            set { x.SetValue("WarehouseID", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int ProductSpecID
        {
            get { return x.GetInt32("ProductSpecID"); }
            set { x.SetValue("ProductSpecID", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 库存数量
        /// </summary>
        public double Quantity
        {
            get { return x.GetDouble("Quantity"); }
            set { x.SetValue("Quantity", value); }
        }

        /// <summary>
        /// 冻结库存数量
        /// </summary>
        public double FrozenQuantity
        {
            get { return x.GetDouble("FrozenQuantity"); }
            set { x.SetValue("FrozenQuantity", value); }
        }

        /// <summary>
        /// 剩余库存数量
        /// </summary>
        public double RemainQuantity
        {
            get { return x.GetDouble("RemainQuantity"); }
            set { x.SetValue("RemainQuantity", value); }
        }

        public IVT_ProductStock()
        {
            x.Init("IVT_ProductStock");
        }
    }
    [Serializable]
    public class IVT_ProductStockChange : BaseEntity
    {
        /// <summary>
        /// 商品库存变动ID
        /// </summary>
        public int ProductStockChangeID
        {
            get { return x.GetInt32("ProductStockChangeID"); }
            set { x.SetValue("ProductStockChangeID", value); }
        }

        /// <summary>
        /// 商品库存ID
        /// </summary>
        public int ProductStockID
        {
            get { return x.GetInt32("ProductStockID"); }
            set { x.SetValue("ProductStockID", value); }
        }

        /// <summary>
        /// 变动类型
        /// </summary>
        public int ChangeType
        {
            get { return x.GetInt32("ChangeType"); }
            set { x.SetValue("ChangeType", value); }
        }

        /// <summary>
        /// 变动日期
        /// </summary>
        public string ChangeDate
        {
            get { return x.GetString("ChangeDate"); }
            set { x.SetValue("ChangeDate", value); }
        }

        /// <summary>
        /// 变动数量
        /// </summary>
        public double ChangeQuantity
        {
            get { return x.GetDouble("ChangeQuantity"); }
            set { x.SetValue("ChangeQuantity", value); }
        }

        /// <summary>
        /// 变动原因
        /// </summary>
        public string ChangeReason
        {
            get { return x.GetString("ChangeReason"); }
            set { x.SetValue("ChangeReason", value); }
        }

        /// <summary>
        /// 关联凭单类型
        /// </summary>
        public int AssociateVoucherType
        {
            get { return x.GetInt32("AssociateVoucherType"); }
            set { x.SetValue("AssociateVoucherType", value); }
        }

        /// <summary>
        /// 关联凭单号码
        /// </summary>
        public string AssociateVoucherNo
        {
            get { return x.GetString("AssociateVoucherNo"); }
            set { x.SetValue("AssociateVoucherNo", value); }
        }

        /// <summary>
        /// 关联凭单ID
        /// </summary>
        public int AssociateID
        {
            get { return x.GetInt32("AssociateID"); }
            set { x.SetValue("AssociateID", value); }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime
        {
            get { return x.GetDateTime("CreateTime"); }
            set { x.SetValue("CreateTime", value); }
        }

        /// <summary>
        /// 操作员ID
        /// </summary>
        public int OperatorID
        {
            get { return x.GetInt32("OperatorID"); }
            set { x.SetValue("OperatorID", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public IVT_ProductStockChange()
        {
            x.Init("IVT_ProductStockChange");
        }
    }
    [Serializable]
    public class IVT_StockIn : BaseEntity
    {
        /// <summary>
        /// 采购员ID
        /// </summary>
        public int BuyerID
        {
            get { return x.GetInt32("BuyerID"); }
            set { x.SetValue("BuyerID", value); }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return x.GetDateTime("CreateTime"); }
            set { x.SetValue("CreateTime", value); }
        }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentID
        {
            get { return x.GetInt32("DepartmentID"); }
            set { x.SetValue("DepartmentID", value); }
        }

        /// <summary>
        /// 操作员ID
        /// </summary>
        public int OperatorID
        {
            get { return x.GetInt32("OperatorID"); }
            set { x.SetValue("OperatorID", value); }
        }

        /// <summary>
        /// 供应商ID
        /// </summary>
        public int ProviderID
        {
            get { return x.GetInt32("ProviderID"); }
            set { x.SetValue("ProviderID", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        /// <summary>
        /// 入库日期
        /// </summary>
        public string StockInDate
        {
            get { return x.GetString("StockInDate"); }
            set { x.SetValue("StockInDate", value); }
        }

        /// <summary>
        /// 入库ID
        /// </summary>
        public int StockInID
        {
            get { return x.GetInt32("StockInID"); }
            set { x.SetValue("StockInID", value); }
        }

        /// <summary>
        /// 入库单号
        /// </summary>
        public string StockInNo
        {
            get { return x.GetString("StockInNo"); }
            set { x.SetValue("StockInNo", value); }
        }

        /// <summary>
        /// 入库原因
        /// </summary>
        public int StockInReason
        {
            get { return x.GetInt32("StockInReason"); }
            set { x.SetValue("StockInReason", value); }
        }

        /// <summary>
        /// 总金额
        /// </summary>
        public double TotalAmount
        {
            get { return x.GetDouble("TotalAmount"); }
            set { x.SetValue("TotalAmount", value); }
        }

        /// <summary>
        /// 总数量
        /// </summary>
        public double TotalQuantity
        {
            get { return x.GetDouble("TotalQuantity"); }
            set { x.SetValue("TotalQuantity", value); }
        }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public int WarehouseID
        {
            get { return x.GetInt32("WarehouseID"); }
            set { x.SetValue("WarehouseID", value); }
        }

        public IVT_StockIn()
        {
            x.Init("IVT_StockIn");
        }
    }
    [Serializable]
    public class IVT_StockInItem : BaseEntity
    {
        /// <summary>
        /// 金额
        /// </summary>
        public double Amount
        {
            get { return x.GetDouble("Amount"); }
            set { x.SetValue("Amount", value); }
        }

        /// <summary>
        /// 价格
        /// </summary>
        public double Price
        {
            get { return x.GetDouble("Price"); }
            set { x.SetValue("Price", value); }
        }

        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int ProductSpecID
        {
            get { return x.GetInt32("ProductSpecID"); }
            set { x.SetValue("ProductSpecID", value); }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public double Quantity
        {
            get { return x.GetDouble("Quantity"); }
            set { x.SetValue("Quantity", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        /// <summary>
        /// 入库ID
        /// </summary>
        public int StockInID
        {
            get { return x.GetInt32("StockInID"); }
            set { x.SetValue("StockInID", value); }
        }

        /// <summary>
        /// 入库明细ID
        /// </summary>
        public int StockInItemID
        {
            get { return x.GetInt32("StockInItemID"); }
            set { x.SetValue("StockInItemID", value); }
        }

        public IVT_StockInItem()
        {
            x.Init("IVT_StockInItem");
        }
    }
    [Serializable]
    public class IVT_StockOut : BaseEntity
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return x.GetDateTime("CreateTime"); }
            set { x.SetValue("CreateTime", value); }
        }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentID
        {
            get { return x.GetInt32("DepartmentID"); }
            set { x.SetValue("DepartmentID", value); }
        }

        /// <summary>
        /// 操作员ID
        /// </summary>
        public int OperatorID
        {
            get { return x.GetInt32("OperatorID"); }
            set { x.SetValue("OperatorID", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 销售员ID
        /// </summary>
        public int SalesmanID
        {
            get { return x.GetInt32("SalesmanID"); }
            set { x.SetValue("SalesmanID", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        /// <summary>
        /// 出库日期
        /// </summary>
        public string StockOutDate
        {
            get { return x.GetString("StockOutDate"); }
            set { x.SetValue("StockOutDate", value); }
        }

        /// <summary>
        /// 出库ID
        /// </summary>
        public int StockOutID
        {
            get { return x.GetInt32("StockOutID"); }
            set { x.SetValue("StockOutID", value); }
        }

        /// <summary>
        /// 出库单号
        /// </summary>
        public string StockOutNo
        {
            get { return x.GetString("StockOutNo"); }
            set { x.SetValue("StockOutNo", value); }
        }

        /// <summary>
        /// 出库原因
        /// </summary>
        public int StockOutReason
        {
            get { return x.GetInt32("StockOutReason"); }
            set { x.SetValue("StockOutReason", value); }
        }

        /// <summary>
        /// 总金额
        /// </summary>
        public double TotalAmount
        {
            get { return x.GetDouble("TotalAmount"); }
            set { x.SetValue("TotalAmount", value); }
        }

        /// <summary>
        /// 总数量
        /// </summary>
        public double TotalQuantity
        {
            get { return x.GetDouble("TotalQuantity"); }
            set { x.SetValue("TotalQuantity", value); }
        }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public int WarehouseID
        {
            get { return x.GetInt32("WarehouseID"); }
            set { x.SetValue("WarehouseID", value); }
        }

        public IVT_StockOut()
        {
            x.Init("IVT_StockOut");
        }
    }
    [Serializable]
    public class IVT_StockOutItem : BaseEntity
    {
        /// <summary>
        /// 出库明细ID
        /// </summary>
        public int StockOutItemID
        {
            get { return x.GetInt32("StockOutItemID"); }
            set { x.SetValue("StockOutItemID", value); }
        }

        /// <summary>
        /// 出库ID
        /// </summary>
        public int StockOutID
        {
            get { return x.GetInt32("StockOutID"); }
            set { x.SetValue("StockOutID", value); }
        }

        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 商品规格ID
        /// </summary>
        public int ProductSpecID
        {
            get { return x.GetInt32("ProductSpecID"); }
            set { x.SetValue("ProductSpecID", value); }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public double Amount
        {
            get { return x.GetDouble("Amount"); }
            set { x.SetValue("Amount", value); }
        }

        /// <summary>
        /// 价格
        /// </summary>
        public double Price
        {
            get { return x.GetDouble("Price"); }
            set { x.SetValue("Price", value); }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public double Quantity
        {
            get { return x.GetDouble("Quantity"); }
            set { x.SetValue("Quantity", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public IVT_StockOutItem()
        {
            x.Init("IVT_StockOutItem");
        }
    }
    [Serializable]
    public class CFG_PageUnit : BaseEntity
    {
        /// <summary>
        /// 页面代码
        /// </summary>
        public string PageCode
        {
            get { return x.GetString("PageCode"); }
            set { x.SetValue("PageCode", value); }
        }

        /// <summary>
        /// 页面参数
        /// </summary>
        public string PageParameter
        {
            get { return x.GetString("PageParameter"); }
            set { x.SetValue("PageParameter", value); }
        }

        /// <summary>
        /// 页面单元代码
        /// </summary>
        public string PageUnitCode
        {
            get { return x.GetString("PageUnitCode"); }
            set { x.SetValue("PageUnitCode", value); }
        }

        /// <summary>
        /// 页面单元参数
        /// </summary>
        public string PageUnitParameter
        {
            get { return x.GetString("PageUnitParameter"); }
            set { x.SetValue("PageUnitParameter", value); }
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int OrderNo
        {
            get { return x.GetInt32("OrderNo"); }
            set { x.SetValue("OrderNo", value); }
        }

        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentCategory
        {
            get { return x.GetString("ContentCategory"); }
            set { x.SetValue("ContentCategory", value); }
        }

        /// <summary>
        /// 内容值类型
        /// </summary>
        public string ContentValueType
        {
            get { return x.GetString("ContentValueType"); }
            set { x.SetValue("ContentValueType", value); }
        }

        /// <summary>
        /// 内容值
        /// </summary>
        public string ContentValue
        {
            get { return x.GetString("ContentValue"); }
            set { x.SetValue("ContentValue", value); }
        }

        public CFG_PageUnit()
        {
            x.Init("CFG_PageUnit");
        }
    }
    [Serializable]
    public class STAT_PageHit : BaseEntity
    {
        /// <summary>
        /// 点击日期
        /// </summary>
        public string HitDate
        {
            get { return x.GetString("HitDate"); }
            set { x.SetValue("HitDate", value); }
        }

        /// <summary>
        /// 点击时间
        /// </summary>
        public string HitTime
        {
            get { return x.GetString("HitTime"); }
            set { x.SetValue("HitTime", value); }
        }

        /// <summary>
        /// 页面代码
        /// </summary>
        public string PageCode
        {
            get { return x.GetString("PageCode"); }
            set { x.SetValue("PageCode", value); }
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return x.GetInt32("MemberID"); }
            set { x.SetValue("MemberID", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP
        {
            get { return x.GetString("IP"); }
            set { x.SetValue("IP", value); }
        }

        public STAT_PageHit()
        {
            x.Init("STAT_PageHit");
        }
    }
    [Serializable]
    public class STAT_ProductDaySummary : BaseEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 汇总日期
        /// </summary>
        public string SummaryDate
        {
            get { return x.GetString("SummaryDate"); }
            set { x.SetValue("SummaryDate", value); }
        }

        /// <summary>
        /// 销售次数
        /// </summary>
        public double SalesCount
        {
            get { return x.GetDouble("SalesCount"); }
            set { x.SetValue("SalesCount", value); }
        }

        /// <summary>
        /// 销售数量
        /// </summary>
        public double SalesQuantity
        {
            get { return x.GetDouble("SalesQuantity"); }
            set { x.SetValue("SalesQuantity", value); }
        }

        /// <summary>
        /// 销售金额
        /// </summary>
        public double SalesAmount
        {
            get { return x.GetDouble("SalesAmount"); }
            set { x.SetValue("SalesAmount", value); }
        }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int HitCount
        {
            get { return x.GetInt32("HitCount"); }
            set { x.SetValue("HitCount", value); }
        }

        public STAT_ProductDaySummary()
        {
            x.Init("STAT_ProductDaySummary");
        }
    }
    [Serializable]
    public class STAT_ProductRankingList : BaseEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductRankingListID
        {
            get { return x.GetInt32("ProductRankingListID"); }
            set { x.SetValue("ProductRankingListID", value); }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public int Category
        {
            get { return x.GetInt32("Category"); }
            set { x.SetValue("Category", value); }
        }

        /// <summary>
        /// 周期
        /// </summary>
        public int Period
        {
            get { return x.GetInt32("Period"); }
            set { x.SetValue("Period", value); }
        }

        /// <summary>
        /// 开始日期
        /// </summary>
        public string FromDate
        {
            get { return x.GetString("FromDate"); }
            set { x.SetValue("FromDate", value); }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string ToDate
        {
            get { return x.GetString("ToDate"); }
            set { x.SetValue("ToDate", value); }
        }

        /// <summary>
        /// 商品类型ID
        /// </summary>
        public int ProductCategoryID
        {
            get { return x.GetInt32("ProductCategoryID"); }
            set { x.SetValue("ProductCategoryID", value); }
        }

        /// <summary>
        /// 商品类型ID
        /// </summary>
        public int ProductBrandID
        {
            get { return x.GetInt32("ProductBrandID"); }
            set { x.SetValue("ProductBrandID", value); }
        }

        public STAT_ProductRankingList()
        {
            x.Init("STAT_ProductRankingList");
        }
    }
    [Serializable]
    public class STAT_ProductRankingListItem : BaseEntity
    {
        /// <summary>
        /// 排行榜ID
        /// </summary>
        public int ProductRankingListID
        {
            get { return x.GetInt32("ProductRankingListID"); }
            set { x.SetValue("ProductRankingListID", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int OrderNo
        {
            get { return x.GetInt32("OrderNo"); }
            set { x.SetValue("OrderNo", value); }
        }

        /// <summary>
        /// 总数量
        /// </summary>
        public double TotalQuantiy
        {
            get { return x.GetDouble("TotalQuantiy"); }
            set { x.SetValue("TotalQuantiy", value); }
        }

        /// <summary>
        /// 总金额
        /// </summary>
        public double Totalamount
        {
            get { return x.GetDouble("Totalamount"); }
            set { x.SetValue("Totalamount", value); }
        }

        /// <summary>
        /// 趋势
        /// </summary>
        public int Trend
        {
            get { return x.GetInt32("Trend"); }
            set { x.SetValue("Trend", value); }
        }

        public STAT_ProductRankingListItem()
        {
            x.Init("STAT_ProductRankingListItem");
        }
    }
    [Serializable]
    public class CFG_NewProductList : BaseEntity
    {
        /// <summary>
        /// 列表类型
        /// </summary>
        public string ListCategory
        {
            get { return x.GetString("ListCategory"); }
            set { x.SetValue("ListCategory", value); }
        }

        /// <summary>
        /// 列表参数
        /// </summary>
        public string ListParameter
        {
            get { return x.GetString("ListParameter"); }
            set { x.SetValue("ListParameter", value); }
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int OrderNo
        {
            get { return x.GetInt32("OrderNo"); }
            set { x.SetValue("OrderNo", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        public CFG_NewProductList()
        {
            x.Init("CFG_NewProductList");
        }
    }
    [Serializable]
    public class CFG_ProductRelation : BaseEntity
    {
        /// <summary>
        /// 关联类型
        /// </summary>
        public string RelationCategory
        {
            get { return x.GetString("RelationCategory"); }
            set { x.SetValue("RelationCategory", value); }
        }

        /// <summary>
        /// 关联参数
        /// </summary>
        public string RelationParameter
        {
            get { return x.GetString("RelationParameter"); }
            set { x.SetValue("RelationParameter", value); }
        }

        /// <summary>
        /// 序号
        /// </summary>
        public int OrderNo
        {
            get { return x.GetInt32("OrderNo"); }
            set { x.SetValue("OrderNo", value); }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductID
        {
            get { return x.GetInt32("ProductID"); }
            set { x.SetValue("ProductID", value); }
        }

        /// <summary>
        /// 关联商品ID
        /// </summary>
        public int RelationProductID
        {
            get { return x.GetInt32("RelationProductID"); }
            set { x.SetValue("RelationProductID", value); }
        }

        /// <summary>
        /// 关联率
        /// </summary>
        public double RelationRate
        {
            get { return x.GetDouble("RelationRate"); }
            set { x.SetValue("RelationRate", value); }
        }

        public CFG_ProductRelation()
        {
            x.Init("CFG_ProductRelation");
        }
    }
    [Serializable]
    public class CFG_ViewThenBuyOtherProductList : BaseEntity
    {
        /// <summary>
        /// 浏览商品ID
        /// </summary>
        public int ViewProductID
        {
            get { return x.GetInt32("ViewProductID"); }
            set { x.SetValue("ViewProductID", value); }
        }

        /// <summary>
        /// 购买商品ID
        /// </summary>
        public int BuyProductID
        {
            get { return x.GetInt32("BuyProductID"); }
            set { x.SetValue("BuyProductID", value); }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity
        {
            get { return x.GetInt32("Quantity"); }
            set { x.SetValue("Quantity", value); }
        }

        public CFG_ViewThenBuyOtherProductList()
        {
            x.Init("CFG_ViewThenBuyOtherProductList");
        }
    }
    [Serializable]
    public class INF_ArticleCategory : BaseEntity
    {
        /// <summary>
        /// 文章类型ID
        /// </summary>
        public int ArticleCategoryID
        {
            get { return x.GetInt32("ArticleCategoryID"); }
            set { x.SetValue("ArticleCategoryID", value); }
        }

        /// <summary>
        /// 文章类型名称
        /// </summary>
        public string ArticleCategoryName
        {
            get { return x.GetString("ArticleCategoryName"); }
            set { x.SetValue("ArticleCategoryName", value); }
        }

        public INF_ArticleCategory()
        {
            x.Init("INF_ArticleCategory");
        }
    }
    [Serializable]
    public class INF_Article : BaseEntity
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public int ArticleID
        {
            get { return x.GetInt32("ArticleID"); }
            set { x.SetValue("ArticleID", value); }
        }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string ArticleTitle
        {
            get { return x.GetString("ArticleTitle"); }
            set { x.SetValue("ArticleTitle", value); }
        }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string ArticleContent
        {
            get { return x.GetString("ArticleContent"); }
            set { x.SetValue("ArticleContent", value); }
        }

        /// <summary>
        /// 文章类型ID
        /// </summary>
        public int ArticleCategoryID
        {
            get { return x.GetInt32("ArticleCategoryID"); }
            set { x.SetValue("ArticleCategoryID", value); }
        }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime IssueDateTime
        {
            get { return x.GetDateTime("IssueDateTime"); }
            set { x.SetValue("IssueDateTime", value); }
        }

        /// <summary>
        /// 发布人ID
        /// </summary>
        public int IssueOperatorID
        {
            get { return x.GetInt32("IssueOperatorID"); }
            set { x.SetValue("IssueOperatorID", value); }
        }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int HitCount
        {
            get { return x.GetInt32("HitCount"); }
            set { x.SetValue("HitCount", value); }
        }

        /// <summary>
        /// 评论次数
        /// </summary>
        public int ReviewCount
        {
            get { return x.GetInt32("ReviewCount"); }
            set { x.SetValue("ReviewCount", value); }
        }

        /// <summary>
        /// 评价等级
        /// </summary>
        public int ScoreLevel
        {
            get { return x.GetInt32("ScoreLevel"); }
            set { x.SetValue("ScoreLevel", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public INF_Article()
        {
            x.Init("INF_Article");
        }
    }
    [Serializable]
    public class INF_ArticleReview : BaseEntity
    {
        /// <summary>
        /// 文章评论ID
        /// </summary>
        public int ArticleReviewID
        {
            get { return x.GetInt32("ArticleReviewID"); }
            set { x.SetValue("ArticleReviewID", value); }
        }

        /// <summary>
        /// 文章ID
        /// </summary>
        public int ArticleID
        {
            get { return x.GetInt32("ArticleID"); }
            set { x.SetValue("ArticleID", value); }
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID
        {
            get { return x.GetInt32("MemberID"); }
            set { x.SetValue("MemberID", value); }
        }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string ReviewContent
        {
            get { return x.GetString("ReviewContent"); }
            set { x.SetValue("ReviewContent", value); }
        }

        /// <summary>
        /// 评价等级
        /// </summary>
        public int ScoreLevel
        {
            get { return x.GetInt32("ScoreLevel"); }
            set { x.SetValue("ScoreLevel", value); }
        }

        /// <summary>
        /// 赞同数量
        /// </summary>
        public int AgreeCount
        {
            get { return x.GetInt32("AgreeCount"); }
            set { x.SetValue("AgreeCount", value); }
        }

        /// <summary>
        /// 反对数量
        /// </summary>
        public int DisagreeCount
        {
            get { return x.GetInt32("DisagreeCount"); }
            set { x.SetValue("DisagreeCount", value); }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return x.GetDateTime("CreateTime"); }
            set { x.SetValue("CreateTime", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public INF_ArticleReview()
        {
            x.Init("INF_ArticleReview");
        }
    }
    [Serializable]
    public class BSC_MobileMessage : BaseEntity
    {
        /// <summary>
        /// 短信ID
        /// </summary>
        public int MobileMessageID
        {
            get { return x.GetInt32("MobileMessageID"); }
            set { x.SetValue("MobileMessageID", value); }
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone
        {
            get { return x.GetString("MobilePhone"); }
            set { x.SetValue("MobilePhone", value); }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string MessageContent
        {
            get { return x.GetString("MessageContent"); }
            set { x.SetValue("MessageContent", value); }
        }

        /// <summary>
        /// 分类
        /// </summary>
        public int MessageCategory
        {
            get { return x.GetInt32("MessageCategory"); }
            set { x.SetValue("MessageCategory", value); }
        }

        /// <summary>
        /// 发送日期时间
        /// </summary>
        public DateTime SendDateTime
        {
            get { return x.GetDateTime("SendDateTime"); }
            set { x.SetValue("SendDateTime", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 返回码
        /// </summary>
        public string ReturnCode
        {
            get { return x.GetString("ReturnCode"); }
            set { x.SetValue("ReturnCode", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_MobileMessage()
        {
            x.Init("BSC_MobileMessage");
        }
    }
    [Serializable]
    public class BSC_EmailMessage : BaseEntity
    {
        /// <summary>
        /// 邮件ID
        /// </summary>
        public int EmailMessageID
        {
            get { return x.GetInt32("EmailMessageID"); }
            set { x.SetValue("EmailMessageID", value); }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return x.GetString("Title"); }
            set { x.SetValue("Title", value); }
        }

        /// <summary>
        /// 主题
        /// </summary>
        public string Body
        {
            get { return x.GetString("Body"); }
            set { x.SetValue("Body", value); }
        }

        /// <summary>
        /// 是否HTML格式
        /// </summary>
        public int IsBodyHtml
        {
            get { return x.GetInt32("IsBodyHtml"); }
            set { x.SetValue("IsBodyHtml", value); }
        }

        /// <summary>
        /// 分类
        /// </summary>
        public int Category
        {
            get { return x.GetInt32("Category"); }
            set { x.SetValue("Category", value); }
        }

        /// <summary>
        /// 发送日期时间
        /// </summary>
        public DateTime SendDateTime
        {
            get { return x.GetDateTime("SendDateTime"); }
            set { x.SetValue("SendDateTime", value); }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return x.GetString("Remark"); }
            set { x.SetValue("Remark", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_EmailMessage()
        {
            x.Init("BSC_EmailMessage");
        }
    }
    [Serializable]
    public class BSC_EmailMessageReceiver : BaseEntity
    {
        /// <summary>
        /// 邮件ID
        /// </summary>
        public int EmailMessageID
        {
            get { return x.GetInt32("EmailMessageID"); }
            set { x.SetValue("EmailMessageID", value); }
        }

        /// <summary>
        /// 接收者
        /// </summary>
        public string Receiver
        {
            get { return x.GetString("Receiver"); }
            set { x.SetValue("Receiver", value); }
        }

        /// <summary>
        /// 发送日期时间
        /// </summary>
        public DateTime SendDateTime
        {
            get { return x.GetDateTime("SendDateTime"); }
            set { x.SetValue("SendDateTime", value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            get { return x.GetInt32("Status"); }
            set { x.SetValue("Status", value); }
        }

        public BSC_EmailMessageReceiver()
        {
            x.Init("BSC_EmailMessageReceiver");
        }
    }
}
