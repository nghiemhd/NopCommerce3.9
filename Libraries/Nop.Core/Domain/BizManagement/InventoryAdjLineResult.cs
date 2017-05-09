namespace Nop.Core.Domain.BizManagement
{
    public class InventoryAdjLineResult : BaseEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string AttributeDesc { get; set; }
        public string Barcode { get; set; }
        public int BarcodeId { get; set; }
        public decimal CostPrice { get; set; }
        public int Qty { get; set; }
        public decimal CostAmount { get; set; }

        public string FullName { get; set; }
        
        public int AdjId { get; set; }
        public int Seq { get; set; }

        public string PictureUrl { get; set; }
    }
}