using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DoAnCoSo.Models
{
    public class LoaiKyLuat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Mã Kỷ Luật")]
        public int KLId { get; set; }
        [DisplayName("Tên Loại Kỷ Luật")]
        public string TenKL { get; set; }
        [DisplayName("Mô tả")]
        public string? MoTa { get; set; }

        public List<KyLuat> KyLuats { get; set; }
    }
}
