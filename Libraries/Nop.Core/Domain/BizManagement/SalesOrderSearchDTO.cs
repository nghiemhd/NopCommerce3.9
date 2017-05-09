using System;

namespace Nop.Core.Domain.BizManagement
{
    public class SalesOrderSearchDTO
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? StoreId { get; set; }
        public int? PSStatus { get; set; }
        public string OrderNo { get; set; }
        public string RefNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string ProductCode { get; set; }
        public string Barcode { get; set; }
        public string Desc { get; set; }
        public int? CarrierId { get; set; }
        public int? SaleChanelId { get; set; }
        public int? SourceId { get; set; }
        public int? ReturnReasonId { get; set; }
        public string ShippingCode { get; set; }
        public int? PaymentType { get; set; }
        public int? PaymentStatus { get; set; }
    }
}
