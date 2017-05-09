using System;

namespace Nop.Core.Domain.BizManagement
{
    public class InventoryTransactionResult : BaseEntity
    {
        public int RefId { get; set; }
        public DateTime TransDate { get; set; }
        public int TypeId { get; set; }
        public string TransNo { get; set; }
        public int StoreId { get; set; }
        public string StoreCode { get; set; }
        public string ProductCode { get; set; }
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string AttributeDesc { get; set; }
        public string PictureUrl { get; set; }
        public int Qty { get; set; }
        public string CreatedUser { get; set; }
    }
}