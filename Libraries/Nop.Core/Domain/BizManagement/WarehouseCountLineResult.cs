namespace Nop.Core.Domain.BizManagement
{
    public class WarehouseCountLineResult : BaseEntity
    {
        public string AttributeDesc { get; set; }
        public string Barcode { get; set; }
        public int Qty { get; set; }
        public decimal CostAmount { get; set; }
        public decimal CostPrice
        {
            get
            {
                if (Qty != 0)
                    return CostAmount / Qty;
                else return 0;
            }
        }
    }
}
