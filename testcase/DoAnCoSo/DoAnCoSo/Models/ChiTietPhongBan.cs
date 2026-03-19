using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DoAnCoSo.Models
{
    public class ChiTietPhongBan
    {
        public int Id { get; set; }

        public int PBId { get; set; }
        public int MaNhanVien { get; set; }

        [ForeignKey("MaNhanVien")]
        [ValidateNever]
        public NhanVien? NhanViens { get; set; }

        [ForeignKey("PBId")]
        [ValidateNever]
        public PhongBan? phongBans { get; set; }
    }
}
