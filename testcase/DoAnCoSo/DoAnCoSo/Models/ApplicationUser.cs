using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DoAnCoSo.Data;

namespace DoAnCoSo.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int? MaNhanVien { get; set; }

        [ForeignKey("MaNhanVien")]
        [ValidateNever]
        public NhanVien? NhanVien { get; set; }

        [DisplayName("Hình Ảnh")]
        public string? HinhAnh { get; set; }
        [Display(Name = "Tên")]
        public string FirstName { get; set; }
        [Display(Name = "Họ")]
        public string LastName { get; set; }
    }
}
