using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Nop.Core.Domain.BizManagement
{
    public class InventoryAdjResult : BaseEntity
    {
        [Display(Name = "Số CT")]
        public string AdjNo { get; set; }

        [Display(Name = "Ngày CT")]
        public DateTime AdjDate { get; set; }

        [Display(Name = "Diễn giải")]
        public string Desc { get; set; }

        [Display(Name = "Tài khoản ghi nợ")]
        public int DebitAcctId { get; set; }

        [Display(Name = "Mã khoản ghi nợ")]
        public string DebitAcctCode { get; set; }

        [Display(Name = "Tên khoản ghi nợ")]
        public string DebitAcctName { get; set; }

        [Display(Name = "Kho")]
        public int StoreId { get; set; }

        public string StoreCode { get; set; }

        public IList<SelectListItem> Stores { get; set; }

        [Display(Name = "Đối tượng")]
        public int? ObjId { get; set; }

        [Display(Name = "Mã đối tượng")]
        public string ObjCode { get; set; }

        [Display(Name = "Tên đối tượng")]
        public string ObjName { get; set; }

        public decimal CostAmount { get; set; }
        public int Qty { get; set; }

        public int Status { get; set; }

        public IAStatus IAStatus
        { 
            get {
                return (IAStatus)Status;
            }
            set
            {
                Status = (int)value;
            }
        }

        public string IAStatusName
        {
            get
            {
                return CommonHelper.DisplayForEnumValue(this.IAStatus);
            }
        }
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Người cập nhật")]
        public string UpdatedUser { get; set; }

        [Display(Name = "Người tạo")]
        public string CreatedUser { get; set; }

        public IList<InventoryAdjLineResult> Items { get; set; }
    }
}