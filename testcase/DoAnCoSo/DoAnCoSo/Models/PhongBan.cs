using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DoAnCoSo.Models
{
    public class PhongBan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Mã Phòng Ban")]
        public int PBId { get; set; }
        [DisplayName("Tên Phòng Ban")]
        public string TenPB { get; set; }
        [DisplayName("Mô Tả")]
        public string? MoTa { get; set; }
        [DisplayName("Ngày Tạo")]
        public DateOnly NgayTao { get; set; }

        public List<ChiTietPhongBan> chiTietPhongBans { get; set; }

    }
}
