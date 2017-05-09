using System;

namespace Nop.Core.Domain.BizManagement
{
    public class PurchaseInvoiceResult : BaseEntity
    {
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Desc { get; set; }
        public string RefNo { get; set; }
        public decimal Amount { get; set; }
        public int Qty { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public string CreatedUser { get; set; }

        public int Status { get; set; }

        public int StoreId { get; set; }
        public string StoreCode { get; set; }

        public PIStatus PIStatus
        {
            get
            {
                return (PIStatus)Status;
            }
            set
            {
                Status = (int)value;
            }
        }

        public string PIStatusName
        {
            get
            {
                return CommonHelper.DisplayForEnumValue(this.PIStatus);
            }
        }
    }
}