using System;

namespace Nop.Core.Domain.BizManagement
{
    public class POS_SalesOrder : BaseEntity
    {
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string Desc { get; set; }
        public int ChanelId { get; set; }
        public string RefNo { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal Amount { get; set; }
        public decimal ReceiveMoney { get; set; }
        public decimal ReturnMoney { get; set; }
        public string CreatedUser { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public byte[] VersionNo { get; set; }
        public string Mobile { get; set; }
        public string CustomerName { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Discount { get; set; }
        public int Status { get; set; }
        public Nullable<int> DebitAcctId { get; set; }
        public Nullable<int> ObjId { get; set; }
        public Nullable<int> FeeAcctId { get; set; }
        public decimal FeeAmount { get; set; }
        public int StoreId { get; set; }
        public Nullable<int> CarrierId { get; set; }
        public decimal AeroShippingFee { get; set; }
        public int PrintCount { get; set; }
        public string Users { get; set; }
        public bool IsMasterCard { get; set; }
        public int SourceId { get; set; }
        public Nullable<int> ReturnReasonId { get; set; }
        public string ShippingCode { get; set; }
        public bool IsPaid { get; set; }
        public Nullable<DateTime> PaymentDate { get; set; }
    }
}
