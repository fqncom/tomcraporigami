using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Agape.Manage.Core.Common
{
    public class EStockDirection
    {
        public const string Empty = "";
        public const string StockIn = "StockIn";
        public const string StockOut = "StockOut";
    }

    public class ESubjectChangeType
    {
        public const string OK = "1";
        public const string Cancel = "2";
    }

    public class ConstStatus
    {
        public const string None = "";
        public const string Draft = "Draft";
        public const string Cancel = "Cancel";
        public const string Confirm = "Confirm";
        public const string PartStockIn = "PartStockIn";
        public const string PartStockOut = "PartStockOut";
        public const string AllStockIn = "AllStockIn";
        public const string AllStockOut = "AllStockOut";
    }

    public class ReturnCode
    {
        public const string Success = "0000";
        public const string Unsuccess = "9999";
    }

    public class ESalesBillType
    {
        public const string Retail = "Retail";
        public const string Batch = "Batch";
        public const string Refund = "Refund";
    }

    public class EPurchaseBillType
    {
        public const string Batch = "Batch";
        public const string Refund = "Refund";
    }

    public class ESalesOrderStatus
    {
        public const string OK = "1";
        public const string Cancel = "2";
    }

    public class ESalesBillStatus
    {
        public const string Draft = "0";
        public const string Pay = "1";
        public const string Cancel = "2";
        public const string Square = "3";
    }

    public class ESalesRefundStatus
    {
        public const string OK = "1";
        public const string Cancel = "2";
    }

    public class ESalesRetailStatus
    {
        public const string Draft = "0";
        public const string Pay = "1";
        public const string Cancel = "2";
        public const string Square = "3";
    }

    public class EPurchaseOrderStatus
    {
        public const string OK = "1";
    }

    public class EPurchaseBillStatus
    {
        public const string OK = "1";
        public const string Cancel = "2";
    }

    public class EPurchaseRefundStatus
    {
        public const string OK = "1";
        public const string Cancel = "2";
    }

    public class EPaymentStatus
    {
        public const string OK = "1";
        public const string Cancel = "2";
    }

    public class EReceiptStatus
    {
        public const string OK = "1";
        public const string Cancel = "2";
    }

    public class EStockShiftStatus
    {
        public const string OK = "1";
        public const string Cancel = "2";
    }

    public class EStockCheckStatus
    {
        public const string Created = "0";
        public const string Doing = "1";
        public const string Finish = "2";
    }

    public class ECRMAccountStatus
    {
        public const string OK = "1";
        public const string Invalid = "9";
    }

    public class ECRMCustomerStatus
    {
        public const string OK = "1";
        public const string Invalid = "9";
    }

    public class ECRMProviderStatus
    {
        public const string OK = "1";
        public const string Invalid = "9";
    }

    public class ECRMMemberStatus
    {
        public const string OK = "1";
        public const string Invalid = "9";
    }

    public class ECRMMemberCategoryStatus
    {
        public const string OK = "1";
        public const string Invalid = "9";
    }

    public class EProductAuditStatus
    {
        public const string Audit = "1";
        public const string Pass = "2";
        public const string Return = "3";
    }

    public class EWarehouseStatus
    {
        public const string OK = "1";
        public const string Invalid = "9";
    }

    public class EParameterCategory
    {
        public const string Member = "Member";
        public const string Point = "Point";
    }

    public class EParameter
    {
        public const string AmountEveryPoint = "AmountEveryPoint";
        public const string MinAmount = "MinAmount";
    }

    #region System
    #endregion

    #region Basic
    public class EPriceConcessionScope
    {
        public const string All = "All";
        public const string Product = "Product";
        public const string ProductCategory = "ProductCategory";
    }

    public class EPriceConcessionType
    {
        public const string Agio = "Agio";
        public const string AgioAmount = "AgioAmount";
        public const string ActualPrice = "ActualPrice";
    }

    public class EWorkTermStatus
    {
        public const string Current = "0";
        public const string Finish = "1";
    }

    public class EPriceType
    {
        public const string Agio = "Agio";
        public const string AgioAmount = "AgioAmount";
        public const string ActualAmount = "ActualAmount";
    }
    #endregion

    #region Inventory
    public class EProductCombineStatus
    {
        public const string OK = "1";
        public const string Cancel = "2";
    }

    public class EProductSplitStatus
    {
        public const string OK = "1";
        public const string Cancel = "2";
    }

    public class EStockProfitLossStatus
    {
        public const string OK = "1";
        public const string Cancel = "2";
    }

    public class EStockProfitLossType
    {
        public const string StockProfit = "P";
        public const string StockLoss = "L";
    }
    #endregion

    #region CRM
    public class EAccountChangeStatus
    {
        public const string OK = "1";
        public const string Cancel = "2";
    }

    public class EAccountChangeCategory
    {
        public const string AccountReceivable = "R";
        public const string AccountPayable = "P";
    }

    public class EAccountChangeType
    {
        public const string In = "I";
        public const string Out = "O";
    }
    #endregion

    #region Finance
    public class EDCFlag
    {
        public const string Debit = "D";
        public const string Credit = "C";
    }

    public class ESubjectCatetory
    {
        public const string Asset = "1";
        public const string Owes = "2";
        public const string measure = "4";
        public const string Cost = "5";
        public const string ProfitAndLoss = "6";
    }

    public class EProfitLossChangeType
    {
        public const string Profit = "P";
        public const string Loss = "L";
    }

    public class EFinanceAccountChangeType
    {
        public const string In = "I";
        public const string Out = "O";
    }

    public class EFinanceAccountChangeStatus
    {
        public const string OK = "1";
        public const string Cancel = "2";
    }

    public class EFixedAssetsChangeType
    {
        public const string In = "I";
        public const string Out = "O";
    }

    public class ESubjectNo
    {
        public const string Cash = "100100";
        public const string BankDeposit = "100200";
        public const string ProductStock = "140500";
        public const string AccountReceivable = "112200";
        public const string AccountPayable = "220200";
    }
    #endregion

    #region Common
    public class EYesNoFlag
    {
        public const string Yes = "1";
        public const string No = "0";
    }
    #endregion
}
