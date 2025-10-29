using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DoAnCoSo.Models
{
    public class LoaiKhenThuong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Mã Khen Thưởng")]
        public int KTId { get; set; }
        [DisplayName("Tên Khen Thưởng")]
        public string TenKT { get; set; }
        [DisplayName("Mô tả")]
        public string? MoTa { get; set; }
        public List<KhenThuong> khenThuongs { get; set; }
    }
}
