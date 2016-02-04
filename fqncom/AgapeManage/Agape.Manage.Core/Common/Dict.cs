using System;
using System.Collections.Generic;
using System.Text;

namespace Agape.Manage.Core.Common
{
    public enum EBatchStatus
    {
        BatchActive = 1,
        BatchCreated = 2,
        OrderConfirm = 3,
        OrderPick = 4,
        OrderPack = 5,
        OrderDeliver = 6,
        OrderFinish = 7
    }

    public enum EOrderStatus
    {
        Created = 0,
        Submit = 1,
        Payed = 2,
        Cancel = 3,
        Failed = 4,
        Fronzen = 5,
        Process = 6,
        Deliver = 7,
        Finish = 8,
        Refund = 9
    }

    public enum EMemberStatus
    {
        Created = 0,
        Normal = 1,
        Frozen = 2,
        Forbid = 3,
        Cancel = 4
    }

    public enum EPointMode
    {
        ByRate = 1,
        ByPiece = 2
    }

    public enum EShoppingType
    {
        Sales = 1,
        Exchange = 2
    }

    public enum EMemberConsultationStatus
    {
        Question = 1,
        Answer = 2,
        Delete = 4
    }

    public enum EProductStatus
    {
        New = 1,
        Offline = 2,
        Online = 3,
        Invalid = 4
    }

    public enum EProductTempStatus
    {
        Save = 1,
        WaitAudit = 2,
        AuditPass = 3,
        AuditBack = 4
    }

    public enum EProductScopeItemMode
    {
        Inclusion = 1,
        Exclusion = 2
    }

    public enum EProductScopeItemType
    {
        All = 1,
        ProductCategory = 2,
        Product = 3
    }

    public enum EProductAgioType
    {
        Agio = 1,
        AgioAmount = 2,
        ActualAmount = 3
    }

    public enum EPromotionTarget
    {
        Order = 1,
        OrderItem = 2
    }

    public enum EPromotionConditionMode
    {
        OverQuantity = 1,
        OverAmount = 2
    }

    public enum EPromotionImplementOrderMode
    {
        CutAmount = 1,
        Agio = 2,
        MoreThenAgio = 3,
        Largess = 4
    }

    public enum EPromotionImplementOrderItemMode
    {
        Agio = 1,
        CutPrice = 2,
        BargainPrice = 3,
        MoreThenAgio = 4,
        MoreThenCutPrice = 5,
        MoreThenBargainPrice = 6,
        SomeThenFree = 7,
        Largess = 8
    }

    public enum EMemberCouponStatus
    {
        Create = 1,
        Grant = 2,
        Active = 3,
        Assign = 5,
        Finish = 6,
        Cancel = 9
    }

    public enum EProductAppearanceStatus
    {
        Damage = 1,
        scratch = 2,
        Good = 3
    }

    public enum EProductPackageStatus
    {
        Good = 1,
        Damage = 2,
        NoPackage = 3
    }

    public enum ERepairStatus
    {
        SubmitRequest = 1,
        CancelRequest = 2,
        VerifySuccess = 3,
        VerifyFail = 4,
        ProductRepair = 5,
        ProductDelivery = 6,
        RepairFinish = 7,
        CustomerConfirm = 8
    }
}
