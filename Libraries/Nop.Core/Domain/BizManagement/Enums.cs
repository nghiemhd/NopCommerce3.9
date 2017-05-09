using System.ComponentModel.DataAnnotations;

namespace Nop.Core.Domain.BizManagement
{
    public enum DocType
    {
        [Display(Name = "Nhật ký chung")]
        GJ = 3,
        [Display(Name = "HD Mua hàng")]
        PI = 1,
        [Display(Name = "HD Bán lẻ (POS)")]
        PS = 2,
        [Display(Name = "Điều chỉnh tồn kho")]
        IA = 4,
        [Display(Name = "Điều chuyển kho")]
        IT = 5
    }

    public enum PSStatus
    {
        [Display(Name = "Chờ duyệt")]
        Release = 1,
        [Display(Name = "Đã duyệt")]
        Approved = 2
    }

    public enum PIStatus
    {
        [Display(Name = "Chờ duyệt")]
        Release = 1,
        [Display(Name = "Đã duyệt")]
        Approved = 2
    }

    public enum IAStatus
    {
        [Display(Name = "Chờ duyệt")]
        Release = 1,
        [Display(Name = "Đã duyệt")]
        Approved = 2
    }

    public enum ITStatus
    {
        [Display(Name = "Chờ xác nhận")]
        Transfer = 1,
        [Display(Name = "Hoàn tất")]
        Complete = 2
    }

    public enum SaleChanel : int
    {
        [Display(Name = "Shop")]
        Shop = 1,
        [Display(Name = "Aero87")]
        Aero87 = 2,
        [Display(Name = "Sendo")]
        Sendo = 3,
        [Display(Name = "Phone")]
        Phone = 4,
        [Display(Name = "Nội bộ")]
        NoiBo = 5
    }

    public enum PaymentType : int
    {
        [Display(Name = "Tiền mặt")]
        Cash = 0,
        [Display(Name = "Thẻ")]
        MasterCard = 1
    }

    public enum PaymentStatus : int
    {
        [Display(Name = "Chưa thanh toán")]
        Unpaid = 0,
        [Display(Name = "Đã thanh toán")]
        Paid = 1
    }
}
