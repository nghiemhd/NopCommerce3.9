namespace Nop.Core.Domain.BizManagement
{
    public class AccountBalanceResult
    {
        public int AcctId { get; set; }
        public string AcctCode { get; set; }
        public string AcctName { get; set; }
        public decimal BeforeDebit { get; set; }
        public decimal BeforeCredit { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal EndDebit { get; set; }
        public decimal EndCredit { get; set; }
    }
}
