using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Core.Domain.BizManagement
{
    public class GeneralLedgerResult : BaseEntity
    {
        [Display(Name = "Số CT")]
        public string DocNo { get; set; }

        public int TypeId { get; set; }

        [Display(Name = "Loại chứng từ")]
        public string TypeName
        {
            get
            {
                return CommonHelper.DisplayForEnumValue((DocType)TypeId);
            }
        }

        [Display(Name = "Ngày CT")]
        public DateTime DocDate { get; set; }

        [Display(Name = "Diễn giải")]
        public string Desc { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Người cập nhật")]
        public string UpdatedUser { get; set; }

        [Display(Name = "Người tạo")]
        public string CreatedUser { get; set; }

        public IList<GeneralLedgerLineResult> Items { get; set; }
    }
}
