using DoAnCoSo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<ChucVu> ChucVus { get; set; }
        public DbSet<NghiPhep> NghiPheps { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<KyLuat> KyLuats { get; set; }
        public DbSet<KhenThuong> KhenThuongs { get; set; }
        public DbSet<LoaiKhenThuong> LoaiKhenThuongs { get; set; }
        public DbSet<LoaiKyLuat> LoaiKyLuats { get; set; }

        public DbSet<BangLuong> bangLuongs { get; set; }

        public DbSet<LuongThang> luongThangs { get; set; }

        public DbSet<ChiTietPhongBan> chiTietPhongBans { get; set; }

        public DbSet<PhongBan> phongBans { get; set; }

    }
}