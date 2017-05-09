using System;

namespace Nop.Core.Domain.BizManagement
{
    public class UpdatedSalesOrderDTO
    {
        public int Id { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
