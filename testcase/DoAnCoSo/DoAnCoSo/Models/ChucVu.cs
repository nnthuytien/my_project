using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnCoSo.Models
{
    public class ChucVu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaChucVu { get; set; }

        [Required(ErrorMessage = "Tên chức vụ là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên chức vụ không vượt quá 50 Ký tự")]
        public string TenChucVu { get; set; }
        [StringLength(300)]
        public string? MoTa { get; set; }

        public List<NhanVien> NhanViens { get; set; }
    }
}
