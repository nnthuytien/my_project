using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DoAnCoSo.Models
{
    public class KhenThuong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Mã Phiếu Khen Thưởng")]
        public int Id { get; set; }
        [DisplayName("Mã Nhân Viên")]
        public int MaNhanVien { get; set; }
        [DisplayName("Lý do")]
        public string LyDo { get; set; }
        [DisplayName("Ngày Quyết Định")]
        public DateOnly NgayQD { get; set; }
        [DisplayName("Mã Khen Thưởng")]
        public int KTId { get; set; }

        [DisplayName("Số Tiền")]
        public int SoTien { get; set; }
        [ForeignKey("MaNhanVien")]
        [ValidateNever]
        public NhanVien? NhanViens { get; set; }

        [ForeignKey("KTId")]
        [ValidateNever]
        public LoaiKhenThuong loaiKhenThuongs { get; set; }
    }
}
