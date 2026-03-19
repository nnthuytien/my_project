using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DoAnCoSo.Models
{
    public class NhanVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Mã Nhân Viên")]
        public int MaNhanVien { get; set; }
        [Required(ErrorMessage = "Tên nhân viên là bắt buộc"), StringLength(100, ErrorMessage = "Tên nhân viên không được quá 100 ký tự")]
        [DisplayName("Tên Nhân Viên")]
        public string TenNhanVien { get; set; }
        [DisplayName("Giới Tính")]
        public string GioiTinh { get; set; }
        [DisplayName("Ngày Sinh")]
        public DateOnly NgaySinh {  get; set; }
        [DisplayName("Địa Chỉ")]
        [StringLength(300)]
        public string? DiaChi { get; set; }
        [DisplayName("Số Điện Thoại")]
        public string SDT { get; set; }

        [DisplayName("Quê Quán")]
        [StringLength(100)]
        public string? QueQuan { get; set; }
        [DisplayName("Hình Ảnh")]
        public string? HinhAnh { get; set; }
        public string? CCCD { get; set; }

        [EmailAddress]
        public string? Email {  get; set; }
        [DisplayName("Trình Trạng")]
        public string? TrinhTrang { get; set; }

        [DisplayName("Mã Chức vụ")]
        public int MaChucVu { get; set; }

        [ForeignKey("MaChucVu")]
        [ValidateNever]
        [DisplayName("Chức Vụ")]
        public ChucVu? ChucVu { get; set; }
    }
}
