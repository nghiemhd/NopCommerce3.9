namespace Nop.Core.Domain.BizManagement
{
    public class CF_Acct : BaseEntity
    {
        public string AcctCode { get; set; }
        public string AcctName { get; set; }

        public bool RequireObj { get; set; }

        public string AcctFull
        {
            get
            {
                return AcctCode + " - " + AcctName;
            }
        }
    }
}
