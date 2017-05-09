namespace Nop.Core.Domain.BizManagement
{
    public class WarehouseCountResult : BaseEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int Qty { get; set; }
        public decimal CostAmount { get; set; }
        public string PictureUrl { get; set; }
        public int Age { get; set; }
        public decimal Price { get; set; }
        public decimal CurrentPrice { get; set; }

        public decimal DiscountPercent
        {
            get {
                if (Price == 0) return 0;
                return ((Price - CurrentPrice) * 100) / Price;
            }
        }

        public decimal CurrentMargin
        {
            get
            {
                if (CurrentPrice == 0) return 0;

                return ((CurrentPrice - CostPrice) * 100) / CurrentPrice;
            }
        }

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
