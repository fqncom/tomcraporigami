using System;
using System.Collections.Generic;
using System.Text;

namespace Agape.Manage.Core.Common
{
    public enum EInventoryStockInStatus
    {
        OK = 1,
        Cancel = 2
    }

    public enum EInventoryStockOutStatus
    {
        OK = 1,
        Cancel = 2
    }

    public enum EProductStockChangeType
    {
        StockInit = 1,
        StockIn = 2,
        StockOut = 3,
        StockFrozen = 4,
        StockUnFrozen = 5
    }

    public enum EProductStockChangeStatus
    {
        OK = 1,
        Cancel = 2
    }

    public enum EMemberInvoiceType
    {
        General = 1,
        ValueAddedTax = 2
    }

    public enum EMemberInvoiceHeaderType
    {
        Individual = 1,
        Company = 2
    }

    public enum EMemberInvoiceContent
    {
        Detail = 1,
        Office = 2,
        Computer = 3,
        Expendable = 4
    }

    public enum EPaymentDeliverType
    {
        ByHand = 1,
        Online = 2
    }

    public enum EDeliverDateType
    {
        WorkDay = 1,
        RestDay = 2,
        Both = 3
    }

    public enum EPaymentMode
    {
        Cash = 1,
        POS = 2
    }

    public enum EVoucherType
    {
        SalesOrder = 1,
        SalesBill = 2,
        SalesRefundBill = 3,
        PurchaseOrder = 11,
        PurchaseBill = 12,
        PurchaseRefundBill = 13,
        OtherStockIn = 21,
        OtherStockOut = 22,
        StockShift = 23,
        StockCheck = 24,
        ProductCombine = 25,
        ProductSplit = 26,
        StockProfitLoss = 27,
        LargessExchange = 31,
        Receipt = 32,
        Payment = 33,
        ProfitLoss = 34
    }

    public enum EStatCategory
    {
        Sales = 1,
        Hit = 2
    }

    public enum EStatPeriod
    {
        Day = 1,
        Week = 2,
        Month = 3,
        Season = 4,
        Year = 6,
        All = 8
    }
}
