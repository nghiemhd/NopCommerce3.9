namespace Nop.Core.Domain.BizManagement
{
    public class PurchaseInvoiceDetailLineResult : BaseEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string AttributeDesc { get; set; }
        public string Barcode { get; set; }
        public int BarcodeId { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public int SaleOrderId { get; set; }
        public decimal Amount { get; set; }
        public string PictureUrl { get; set; }
    }
}
