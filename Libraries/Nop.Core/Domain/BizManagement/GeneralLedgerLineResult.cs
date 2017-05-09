using System;

namespace Nop.Core.Domain.BizManagement
{
    public class GeneralLedgerLineResult : BaseEntity
    {
        public int TransId { get; set; }
        public int LineId { get; set; }
        public int AcctId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public int? RefObjId { get; set; }
        public string Desc { get; set; }

        public string AcctCode { get; set; }
        public string AcctName { get; set; }
        public string ObjCode { get; set; }
        public string ObjName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }
        public string CreatedUser { get; set; }

        //For Other Using
        public string DocNo { get; set; }
        public DateTime? DocDate { get; set; }
    }
}
