using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DoAnCoSo.Models
{
    public class BangLuong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Mã Lương")]
        public int LId { get; set; }
        [DisplayName("Mã Nhân Viên")]
        public int? MaNhanVien { get; set; }
        [DisplayName("Ngày Quyết Định")]
        public DateOnly NgayQD { get; set; }

        [DisplayName("Lương Thỏa Thuận")]
        public int LuongThoaThuan { get; set; }
        [ForeignKey("MaNhanVien")]
        public NhanVien? NhanViens { get; set; }

        public List<LuongThang>? luongThangs { get; set; }
    }
}
