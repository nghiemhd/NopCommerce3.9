namespace Nop.Core.Domain.BizManagement
{
    public class InventoryTransferLineResult : BaseEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string AttributeDesc { get; set; }
        public string Barcode { get; set; }
        public int Qty { get; set; }
        public int TransferId { get; set; }
        public int Seq { get; set; }
        public string PictureUrl { get; set; }
    }
}