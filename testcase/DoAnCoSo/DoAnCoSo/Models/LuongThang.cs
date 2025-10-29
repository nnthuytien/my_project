using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DoAnCoSo.Models
{
    public class LuongThang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Mã Lương Tháng")]
        public int LTId { get; set; }
        [DisplayName("Mã Lương")]
        public int LId { get; set; }
        [DisplayName("Tháng")]
        public int? Thang { get; set; }
        [DisplayName("Năm")]
        public int? Nam { get; set; }
        [DisplayName("Ngày Tính Lương")]
        public DateOnly NgayTL { get; set; }
        [DisplayName("Lương Thỏa Thuận")]
        public int LuongThoaThuan { get; set; }
        [DisplayName("Số Ngày Nghỉ Có Lương")]
        public int? NgayNghiCoLuong { get; set; }
        [DisplayName("Số Ngày Nghỉ Không Lương")]
        public int? NgayNghiKhongLuong { get; set; }
        [DisplayName("Khen Thưởng")]
        public int? KhenThuong { get; set; }

        [DisplayName("Kỷ Luật")]
        public int? KyLuat { get; set; }
        [DisplayName("Phụ Cấp")]
        public int? PhuCap { get; set; }
        [DisplayName("Ứng Trước")]
        public int? UngTruoc { get; set; }
        [DisplayName("Thực Lãnh")]
        public int? ThucLanh { get; set; }

        [ForeignKey("LId")]
        public BangLuong? bangLuongs { get; set; }
    }
}
