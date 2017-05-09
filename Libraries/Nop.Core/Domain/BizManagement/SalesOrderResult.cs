using System;

namespace Nop.Core.Domain.BizManagement
{
    public class SalesOrderResult : BaseEntity
    {
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string Desc { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal AeroShippingFee { get; set; }
        public decimal Discount { get; set; }
        public decimal Amount { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }

        public int StoreId { get; set; }
        public string StoreCode { get; set; }

        public int Status { get; set; }

        public PSStatus PSStatus { 
            get {
            return (PSStatus)Status;
            }
            set
            {
                Status = (int)value;
            }
        }

        public string PSStatusName
        {
            get
            {
                return CommonHelper.DisplayForEnumValue(this.PSStatus);
            }
        }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public string CreatedUser { get; set; }

        public int ChanelId { get; set; }

        public SaleChanel SaleChanel
        {
            get
            {
                return (SaleChanel)ChanelId;
            }
            set
            {
                ChanelId = (int)value;
            }
        }

        public string SaleChanelName
        {
            get
            {
                return CommonHelper.DisplayForEnumValue(this.SaleChanel);
            }
        }

        public string RefNo { get; set; }
        public string CarrierCode { get; set; }
        public string ShippingCode { get; set; }
        public int PrintCount { get; set; }
        public bool IsMasterCard { get; set; }
        public string SalesSource { get; set; }
        public string SalesReturnReason { get; set; }        
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}