using System;
using System.Collections.Generic;
using System.Text;

namespace Agape.Manage.Core.Common
{
    /// <summary>
    /// 商品库存变动请求参数。
    /// </summary>
    public class ProductStockChangeRequestParameter
    {
        public int WarehouseID;
        public int ProductID;
        public int ProductSpecID;
        public string ChangeDate;
        public string ChangeReason;
        public int ChangeType;
        public double ChangeQuantity;
        public int FrozenChangeType;
        public double FrozenChangeQuantity;
        public int AssociateVoucherType;
        public string AssociateVoucherNo;
        public int AssociateID;
        public int OperatorID;

        public ProductStockChangeRequestParameter()
        {
            WarehouseID = 0;
            ProductID = 0;
            ProductSpecID = 0;
            ChangeDate = String.Empty;
            ChangeReason = String.Empty;
            ChangeType = 0;
            ChangeQuantity = 0;
            FrozenChangeType = 0;
            FrozenChangeQuantity = 0;
            AssociateVoucherType = 0;
            AssociateVoucherNo = String.Empty;
            AssociateID = 0;
            OperatorID = 0;
        }
    }
}
