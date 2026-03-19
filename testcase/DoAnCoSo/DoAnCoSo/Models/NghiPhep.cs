using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DoAnCoSo.Models
{
    public class NghiPhep
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Mã Phiếu Nghỉ Phép")]
        public int Id { get; set; }
        [DisplayName("Mã Nhân Viên")]
        public int MaNhanVien { get; set; }
        [Required(ErrorMessage = "Tên nhân viên là bắt buộc"), StringLength(100, ErrorMessage = "Tên nhân viên không được quá 100 ký tự")]
        [DisplayName("Tên Nhân Viên")]
        public string TenNhanVien { get; set; }
        [DisplayName("Lý do")]
        public string LyDo { get; set; }
        [DisplayName("Ngày Bắt Đầu Nghỉ")]
        public DateOnly NgayBatDau { get; set; }
        [DisplayName("Ngày Kết Thúc Nghỉ")]
        public DateOnly NgayKetThuc { get; set; }
        [DisplayName("Loại Nghỉ")]
        public string LoaiNghi { get; set; }
        [DisplayName("Trạng Thái")]
        public string TrangThai { get; set; }

        [ForeignKey("MaNhanVien")]
        [ValidateNever]
        [DisplayName("Mã Nhân Viên")]
        public NhanVien? NhanViens { get; set; }

        [DisplayName("Ngày Chỉnh Sửa Gần Nhất")]
        public DateOnly NgayTao { get; set; }

    }
}
