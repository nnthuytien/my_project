using DoAnCoSo.Data;
using DoAnCoSo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace DoAnCoSo.Controllers
{
    [Authorize]
    public class QuanLyController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public QuanLyController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var id = _userManager.GetUserAsync(User).Result.MaNhanVien;
            var Luongs = await _context.luongThangs.Include(x => x.bangLuongs.NhanViens).Where(x=>x.bangLuongs.NhanViens.MaNhanVien == id).ToListAsync();
            return View(Luongs);
        }
        public async Task<IActionResult> QuanLyNV()
        {
            var nhanViens = await _context.NhanViens.Include(x => x.ChucVu).ToListAsync();
            return View(nhanViens);
        }

        public async Task<IActionResult> CreateNV()
        {
            var categories = await _context.ChucVus.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "MaChucVu", "TenChucVu");
            return View();
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var savePart = Path.Combine("wwwroot/images", image.FileName);
            using (var fileStream = new FileStream(savePart, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNV(NhanVien nhanVien, IFormFile HinhAnh)
        {
            if (ModelState.IsValid)
            {
                var email = await _context.NhanViens.FirstOrDefaultAsync(p => p.Email == nhanVien.Email);
                if (email != null)
                {
                    ModelState.AddModelError("", "Email này đã tồn tại trong một nhân viên nào đó");
                    return View(nhanVien);
                }
                if (HinhAnh != null)
                {
                    nhanVien.HinhAnh = await SaveImage(HinhAnh);
                }
                _context.NhanViens.Add(nhanVien);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm nhân viên thành công";
                return RedirectToAction(nameof(QuanLyNV));
            }
            var categories = await _context.ChucVus.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "MaChucVu", "TenChucVu");
            return View(nhanVien);
        }
        public async Task<IActionResult> DetailsNhanVien(int id)
        {
            var nhanVien = await _context.NhanViens.Include(x => x.ChucVu).SingleOrDefaultAsync(x => x.MaNhanVien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return View(nhanVien);
        }
        public async Task<IActionResult> DetailsNhanVienUser(int id)
        {
            var nhanVien = await _context.NhanViens.Include(x => x.ChucVu).SingleOrDefaultAsync(x => x.MaNhanVien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return View(nhanVien);
        }

        public async Task<IActionResult> EditNhanVien(int id)
        {
            var nhanVien = await _context.NhanViens.Include(x => x.ChucVu).SingleOrDefaultAsync(x => x.MaNhanVien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            var categories = await _context.ChucVus.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "MaChucVu", "TenChucVu");
            return View(nhanVien);
        }
        [HttpPost]
        public async Task<IActionResult> EditNhanVien(NhanVien nhanVien, IFormFile HinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (HinhAnh != null)
                {
                    nhanVien.HinhAnh = await SaveImage(HinhAnh);
                }
                _context.NhanViens.Update(nhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(QuanLyNV));
            }
            var categories = await _context.ChucVus.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "MaChucVu", "TenChucVu");
            return View(nhanVien);
        }
        public async Task<IActionResult> DeleteNV(int id)
        {
            var nhanVien = await _context.NhanViens.Include(x => x.ChucVu).SingleOrDefaultAsync(x => x.MaNhanVien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return View(nhanVien);
        }

        [HttpPost, ActionName("DeleteNV")]

        public async Task<IActionResult> DeleteConfirmedNV(int id)
        {
            var nhanVien = await _context.NhanViens.Include(x => x.ChucVu).SingleOrDefaultAsync(x => x.MaNhanVien == id);
            nhanVien.TrinhTrang = "Đã nghỉ việc";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(QuanLyNV));
        }
        public async Task<IActionResult> KhoiphucNV(int id)
        {
            var nhanVien = await _context.NhanViens.Include(x => x.ChucVu).SingleOrDefaultAsync(x => x.MaNhanVien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return View(nhanVien);
        }

        [HttpPost,ActionName("KhoiphucNV")]

        public async Task<IActionResult> KhoiphucConfirmedNV(int id)
        {
            var nhanVien = await _context.NhanViens.Include(x => x.ChucVu).SingleOrDefaultAsync(x => x.MaNhanVien == id);
            nhanVien.TrinhTrang = null;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(QuanLyNV));
        }
        public async Task<IActionResult> QuanLyTK()
        {
            var nhanViens = await _context.Users.Include(x => x.NhanVien).ToListAsync();
            return View(nhanViens);
        }
        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("QuanLyTK");
        }

        public async Task<IActionResult> DeleteTK(String id)
        {
            var user = await _context.User.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("DeleteTK")]

        public async Task<IActionResult> DeleteConfirmedTK(String id)
        {
            var iduser = _userManager.GetUserAsync(User).Result.Id;
            var user = await _context.User.SingleOrDefaultAsync(x => x.Id == id);
            if(user.Id == iduser)
            {
                TempData["danger"] = "Bạn không thể xóa tài khoản của chính bản thân mình";
                return RedirectToAction(nameof(DeleteTK));
            }
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(QuanLyTK));
        }

        public async Task<IActionResult> QuanLyNP()
        {
            var nghiPheps = await _context.NghiPheps.Include(x => x.NhanViens).ToListAsync();
            return View(nghiPheps);
        }
        public async Task<IActionResult> QuanLyNPUser(int id)
        {
            var nghiPheps = await _context.NghiPheps.Include(x => x.NhanViens).Where(t => t.MaNhanVien == id).ToListAsync();
            return View(nghiPheps);
        }
        public async Task<IActionResult> CreateNP()
        {
            var id = _userManager.GetUserAsync(User).Result.MaNhanVien;
            var tenNV = await _context.NhanViens.Where(t => t.MaNhanVien == id).FirstOrDefaultAsync();
            ViewBag.tenNV= tenNV.TenNhanVien;
            ViewBag.Categories = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNP(NghiPhep nghiPhep)
        {
            var id = _userManager.GetUserAsync(User).Result.MaNhanVien;
            ViewBag.Categories = id;
            if (ModelState.IsValid)
            {
                if (nghiPhep.NgayBatDau == null || nghiPhep.NgayKetThuc == null || nghiPhep.NgayKetThuc < nghiPhep.NgayBatDau)
                {
                    ModelState.AddModelError("", "Ngày nghỉ kết thúc nghỉ phép phải lớn hơn bằng ngày nghỉ phép");
                    return View(nghiPhep);
                }
                nghiPhep.NgayTao = DateOnly.FromDateTime(DateTime.Now);
                _context.NghiPheps.Add(nghiPhep);
                await _context.SaveChangesAsync();
                TempData["success"] = "Tạo phiếu nghỉ phép thành công";
                return RedirectToAction(nameof(CreateNP));
            }
            return View(nghiPhep);
        }

        public async Task<IActionResult> PheDuyetNghiPhep(int id)
        {
            var nghiphep = await _context.NghiPheps.Include(x => x.NhanViens).SingleOrDefaultAsync(x => x.Id == id);
            var NV = await _context.NhanViens.Where(t => t.MaNhanVien == nghiphep.MaNhanVien).FirstOrDefaultAsync();
            ViewBag.tenNV = NV.TenNhanVien;
            ViewBag.Categories = NV.MaNhanVien;
            if (nghiphep == null)
            {
                return NotFound();
            }
            return View(nghiphep);
        }

        [HttpPost]
        public async Task<IActionResult> PheDuyetNghiPhep(NghiPhep nghiPhep)
        {
            if (ModelState.IsValid)
            {
                _context.NghiPheps.Update(nghiPhep);
                nghiPhep.NgayTao = DateOnly.FromDateTime(DateTime.Now);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(QuanLyNP));
            }
            return View(nghiPhep);
        }

        public async Task<IActionResult> QuanLyKhenThuong()
        {
            var khenThuongs = await _context.KhenThuongs.Include(x => x.NhanViens).Include(x => x.loaiKhenThuongs).ToListAsync();
            return View(khenThuongs);
        }

        public async Task<IActionResult> CreateLoaiKT()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateLoaiKT(LoaiKhenThuong loaiKhenThuong)
        {
            if (loaiKhenThuong.TenKT == null)
            {
                return View(loaiKhenThuong);
            }
            _context.LoaiKhenThuongs.AddAsync(loaiKhenThuong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(QuanLyKhenThuong));
        }

        public async Task<IActionResult> CreateKT()
        {
            var khenthuong = await _context.LoaiKhenThuongs.ToListAsync();
            ViewBag.khenthuong = new SelectList(khenthuong, "KTId", "TenKT");
            var nhanvien = await _context.NhanViens.Include(x => x.ChucVu).Where(x=>x.TrinhTrang == null).ToListAsync();
            ViewBag.nhanvien = new SelectList(nhanvien, "MaNhanVien", "TenNhanVien");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateKT(KhenThuong khenThuong)
        {
            var khenthuong = await _context.LoaiKhenThuongs.ToListAsync();
            ViewBag.khenthuong = new SelectList(khenthuong, "KTId", "TenKT");
            var nhanvien = await _context.NhanViens.Include(x => x.ChucVu).Where(x => x.TrinhTrang == null).ToListAsync();
            ViewBag.nhanvien = new SelectList(nhanvien, "MaNhanVien", "TenNhanVien");
            if (ModelState.IsValid)
            {
                _context.KhenThuongs.Add(khenThuong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(QuanLyKhenThuong));
            }
            return View(khenThuong);
        }

        public async Task<IActionResult> EditKT(int id)
        {
            var khenThuong = await _context.KhenThuongs.Include(x => x.NhanViens).Include(x => x.loaiKhenThuongs).SingleOrDefaultAsync(x => x.Id == id);
            if (khenThuong == null)
            {
                return NotFound();
            }
            var khenthuong = await _context.LoaiKhenThuongs.ToListAsync();
            ViewBag.khenthuong = new SelectList(khenthuong, "KTId", "TenKT");
            var nhanvien = await _context.NhanViens.Include(x => x.ChucVu).Where(x => x.TrinhTrang == null).ToListAsync();
            ViewBag.nhanvien = new SelectList(nhanvien, "MaNhanVien", "TenNhanVien");
            return View(khenThuong);
        }
        [HttpPost]
        public async Task<IActionResult> EditKT(KhenThuong khenThuong)
        {
            if (ModelState.IsValid)
            {
                _context.KhenThuongs.Update(khenThuong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(QuanLyKhenThuong));
            }
            var khenthuong = await _context.LoaiKhenThuongs.ToListAsync();
            ViewBag.khenthuong = new SelectList(khenthuong, "KTId", "TenKT");
            var nhanvien = await _context.NhanViens.Include(x => x.ChucVu).Where(x => x.TrinhTrang == null).ToListAsync();
            ViewBag.nhanvien = new SelectList(nhanvien, "MaNhanVien", "TenNhanVien");
            return View(khenThuong);
        }
        public async Task<IActionResult> DeleteKT(int id)
        {
            var khenThuong = await _context.KhenThuongs.Include(x => x.NhanViens).Include(x => x.loaiKhenThuongs).SingleOrDefaultAsync(x => x.Id == id);
            if (khenThuong == null)
            {
                return NotFound();
            }
            return View(khenThuong);
        }

        [HttpPost, ActionName("DeleteKT")]

        public async Task<IActionResult> DeleteConfirmedKT(int id)
        {
            var khenThuong = await _context.KhenThuongs.Include(x => x.NhanViens).Include(x => x.loaiKhenThuongs).SingleOrDefaultAsync(x => x.Id == id);
            _context.KhenThuongs.Remove(khenThuong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(QuanLyKhenThuong));
        }

        public async Task<IActionResult> QuanLyKyLuat()
        {
            var kyluats = await _context.KyLuats.Include(x => x.NhanViens).Include(x => x.loaiKyLuats).ToListAsync();
            return View(kyluats);
        }

        public async Task<IActionResult> CreateLoaiKL()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateLoaiKL(LoaiKyLuat loaiKyLuat)
        {
            if (loaiKyLuat.TenKL == null)
            {
                return View(loaiKyLuat);
            }
            _context.LoaiKyLuats.AddAsync(loaiKyLuat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(QuanLyKyLuat));
        }

        public async Task<IActionResult> CreateKL()
        {
            var kyluat = await _context.LoaiKyLuats.ToListAsync();
            ViewBag.kyluat = new SelectList(kyluat, "KLId", "TenKL");
            var nhanvien = await _context.NhanViens.Include(x => x.ChucVu).Where(x => x.TrinhTrang == null).ToListAsync();
            ViewBag.nhanvien = new SelectList(nhanvien, "MaNhanVien", "TenNhanVien");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateKL(KyLuat kyLuats)
        {
            var kyluat = await _context.LoaiKyLuats.ToListAsync();
            ViewBag.kyluat = new SelectList(kyluat, "KLId", "TenKL");
            var nhanvien = await _context.NhanViens.Include(x => x.ChucVu).Where(x => x.TrinhTrang == null).ToListAsync();
            ViewBag.nhanvien = new SelectList(nhanvien, "MaNhanVien", "TenNhanVien");
            if (ModelState.IsValid)
            {
                _context.KyLuats.Add(kyLuats);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(QuanLyKyLuat));
            }
            return View(kyLuats);
        }

        public async Task<IActionResult> EditKL(int id)
        {
            var kyLuats = await _context.KyLuats.Include(x => x.NhanViens).Include(x => x.loaiKyLuats).SingleOrDefaultAsync(x => x.Id == id);
            if (kyLuats == null)
            {
                return NotFound();
            }
            var kyluat = await _context.LoaiKyLuats.ToListAsync();
            ViewBag.kyluat = new SelectList(kyluat, "KLId", "TenKL");
            var nhanvien = await _context.NhanViens.Include(x => x.ChucVu).Where(x => x.TrinhTrang == null).ToListAsync();
            ViewBag.nhanvien = new SelectList(nhanvien, "MaNhanVien", "TenNhanVien");
            return View(kyLuats);
        }
        [HttpPost]
        public async Task<IActionResult> EditKL(KyLuat kyLuats)
        {
            if (ModelState.IsValid)
            {
                _context.KyLuats.Update(kyLuats);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(QuanLyKyLuat));
            }
            var kyluat = await _context.LoaiKyLuats.ToListAsync();
            ViewBag.kyluat = new SelectList(kyluat, "KLId", "TenKL");
            var nhanvien = await _context.NhanViens.Include(x => x.ChucVu).Where(x => x.TrinhTrang == null).ToListAsync();
            ViewBag.nhanvien = new SelectList(nhanvien, "MaNhanVien", "TenNhanVien");
            return View(kyLuats);
        }
        public async Task<IActionResult> DeleteKL(int id)
        {
            var kyLuats = await _context.KyLuats.Include(x => x.NhanViens).Include(x => x.loaiKyLuats).SingleOrDefaultAsync(x => x.Id == id);
            if (kyLuats == null)
            {
                return NotFound();
            }
            return View(kyLuats);
        }

        [HttpPost, ActionName("DeleteKL")]

        public async Task<IActionResult> DeleteConfirmedKL(int id)
        {
            var kyLuats = await _context.KyLuats.Include(x => x.NhanViens).Include(x => x.loaiKyLuats).SingleOrDefaultAsync(x => x.Id == id);
            _context.KyLuats.Remove(kyLuats);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(QuanLyKyLuat));
        }
        public async Task<IActionResult> QuanLyKTUser(int id)
        {
            var nghiPheps = await _context.KhenThuongs.Include(x => x.NhanViens).Include(x => x.loaiKhenThuongs).Where(t => t.MaNhanVien == id).ToListAsync();
            return View(nghiPheps);
        }
        public async Task<IActionResult> QuanLyKLUser(int id)
        {
            var nghiPheps = await _context.KyLuats.Include(x => x.NhanViens).Include(x => x.loaiKyLuats).Where(t => t.MaNhanVien == id).ToListAsync();
            return View(nghiPheps);
        }

        public async Task<IActionResult> QuanLyLuongNV()
        {
            var Luongs = await _context.bangLuongs.Include(x => x.NhanViens.ChucVu).ToListAsync();
            return View(Luongs);
        }
        public async Task<IActionResult> CreateLuongNV()
        {
            var nhanvien = await _context.NhanViens.Include(x => x.ChucVu).Where(x => x.TrinhTrang == null).ToListAsync();
            ViewBag.nhanvien = new SelectList(nhanvien, "MaNhanVien", "TenNhanVien");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLuongNV(BangLuong bangLuong)
        {
            var nhanvien = await _context.NhanViens.Include(x => x.ChucVu).Where(x => x.TrinhTrang == null).ToListAsync();
            ViewBag.nhanvien = new SelectList(nhanvien, "MaNhanVien", "TenNhanVien");
            if (ModelState.IsValid)
            {
                _context.bangLuongs.Add(bangLuong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(QuanLyLuongNV));
            }
            return View(bangLuong);
        }
        public async Task<IActionResult> EditLuongNV(int id)
        {
            var bangluong = await _context.bangLuongs.Include(x => x.NhanViens.ChucVu).SingleOrDefaultAsync(x => x.LId == id);
            if (bangluong == null)
            {
                return NotFound();
            }
            var nhanvien = await _context.NhanViens.Include(x => x.ChucVu).Where(x => x.TrinhTrang == null).ToListAsync();
            ViewBag.nhanvien = new SelectList(nhanvien, "MaNhanVien", "TenNhanVien");
            return View(bangluong);
        }
        [HttpPost]
        public async Task<IActionResult> EditLuongNV(BangLuong bangLuong)
        {
            var nhanvien = await _context.NhanViens.Include(x => x.ChucVu).Where(x => x.TrinhTrang == null).ToListAsync();
            ViewBag.nhanvien = new SelectList(nhanvien, "MaNhanVien", "TenNhanVien");
            if (ModelState.IsValid)
            {
                _context.bangLuongs.Update(bangLuong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(QuanLyLuongNV));
            }
            return View(bangLuong);
        }
        public async Task<IActionResult> DeleteLuongNV(int id)
        {
            var bangluong = await _context.bangLuongs.Include(x => x.NhanViens.ChucVu).SingleOrDefaultAsync(x => x.LId == id);
            if (bangluong == null)
            {
                return NotFound();
            }
            return View(bangluong);
        }

        [HttpPost, ActionName("DeleteLuongNV")]

        public async Task<IActionResult> DeleteConfirmedLUongNV(int id)
        {
            var bangluong = await _context.bangLuongs.Include(x => x.NhanViens.ChucVu).SingleOrDefaultAsync(x => x.LId == id);
            _context.bangLuongs.Remove(bangluong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(QuanLyLuongNV));
        }
        public async Task<IActionResult> QuanLyTinhLuong()
        {
            var Luongs = await _context.luongThangs.Include(x => x.bangLuongs.NhanViens).ToListAsync();
            return View(Luongs);
        }
        public static int CountWeekdays(DateOnly startDate, DateOnly endDate)
        {
            int count = 0;
            DateOnly currentDate = startDate;

            while (currentDate <= endDate)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    count++;
                }

                currentDate = currentDate.AddDays(1);
            }

            return count;
        }
        public async Task<IActionResult> CreateLuongNVThang()
        {
            var bangluong = await _context.bangLuongs.Include(x => x.NhanViens).ToListAsync();
            ViewBag.nhanvien = new SelectList(bangluong, "LId", "NhanViens.TenNhanVien");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLuongNVThang(LuongThang luongThang)
        {
            var bangluong = await _context.bangLuongs.Include(x => x.NhanViens).ToListAsync();
            ViewBag.nhanvien = new SelectList(bangluong, "LId", "NhanViens.TenNhanVien");
            if (ModelState.IsValid)
            {
                var bangluongss = await _context.bangLuongs.Include(x => x.NhanViens.ChucVu).SingleOrDefaultAsync(x => x.LId == luongThang.LId);
                luongThang.LuongThoaThuan = bangluongss.LuongThoaThuan;
                var nghicoluong = _context.NghiPheps.Where(x => x.NgayBatDau.Month == luongThang.Thang && x.NgayKetThuc.Year == luongThang.Nam && x.TrangThai == "Đã Xét Duyệt" && x.LoaiNghi == "Có lương");
                if (nghicoluong != null)
                {
                    foreach (var x in nghicoluong)
                    {
                        
                        luongThang.NgayNghiCoLuong+= CountWeekdays(x.NgayBatDau, x.NgayKetThuc);
                    }
                }
                var nghikhongluong = _context.NghiPheps.Where(x => x.NgayBatDau.Month == luongThang.Thang && x.NgayKetThuc.Year == luongThang.Nam && x.TrangThai == "Đã Xét Duyệt" && x.LoaiNghi == "Không lương");
                if (nghikhongluong != null)
                {
                    foreach (var x in nghikhongluong)
                    {

                        luongThang.NgayNghiKhongLuong += CountWeekdays(x.NgayBatDau, x.NgayKetThuc);
                    }
                }
                luongThang.KyLuat = Convert.ToInt32(_context.KyLuats.Where(x => x.NgayQD.Month == luongThang.Thang && x.NgayQD.Year == luongThang.Nam).Sum(x => x.SoTien));
                if (_context.KyLuats.Where(x => x.NgayQD.Month == luongThang.Thang && x.NgayQD.Year == luongThang.Nam).Sum(x => x.SoTien) != null)
                    luongThang.KyLuat = Convert.ToInt32(_context.KyLuats.Where(x=>x.NgayQD.Month == luongThang.Thang && x.NgayQD.Year == luongThang.Nam).Sum(x => x.SoTien));
                if (_context.KhenThuongs.Where(x => x.NgayQD.Month == luongThang.Thang && x.NgayQD.Year == luongThang.Nam).Sum(x => x.SoTien) != null)
                    luongThang.KhenThuong = Convert.ToInt32(_context.KhenThuongs.Where(x => x.NgayQD.Month == luongThang.Thang && x.NgayQD.Year == luongThang.Nam).Sum(x => x.SoTien));
                luongThang.ThucLanh = luongThang.LuongThoaThuan -((luongThang.LuongThoaThuan / 26) * luongThang.NgayNghiKhongLuong) - luongThang.KyLuat + luongThang.KhenThuong - luongThang.UngTruoc + luongThang.PhuCap;
                _context.luongThangs.Add(luongThang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(QuanLyTinhLuong));
            }
            return View(luongThang);
        }
        public async Task<IActionResult> DeleteLuongNVThang(int id)
        {
            var luongthang = await _context.luongThangs.Include(x => x.bangLuongs.NhanViens).SingleOrDefaultAsync(x => x.LTId == id);
            if (luongthang == null)
            {
                return NotFound();
            }
            return View(luongthang);
        }

        [HttpPost, ActionName("DeleteLuongNVThang")]

        public async Task<IActionResult> DeleteConfirmedLUongNVThang(int id)
        {
            var luongthang = await _context.luongThangs.Include(x => x.bangLuongs.NhanViens).SingleOrDefaultAsync(x => x.LTId == id);
            _context.luongThangs.Remove(luongthang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(QuanLyTinhLuong));
        }
        public async Task<IActionResult> QuanLyPhongBan()
        {
            var phongBans = await _context.phongBans.ToListAsync();
            return View(phongBans);
        }
        public async Task<IActionResult> QuanLyPhongBanUser(int id)
        {
            var chitietpb = await _context.chiTietPhongBans.Where(x => x.MaNhanVien == id).ToListAsync();
            var phongBans = new List<PhongBan>();
            foreach(var chitiet in chitietpb)
            {
                var phongBans2 = await _context.phongBans.Where(x=>x.PBId == chitiet.PBId).FirstOrDefaultAsync();
                phongBans.Add(phongBans2);
            }
            return View(phongBans);
        }
        public async Task<IActionResult> CreatePhongBan()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePhongBan(PhongBan phongBan)
        {
            _context.phongBans.Add(phongBan);
            await _context.SaveChangesAsync();
            phongBan.NgayTao = DateOnly.FromDateTime(DateTime.Now);
            return RedirectToAction(nameof(QuanLyPhongBan));
        }

        public async Task<IActionResult> DeletePhongBan(int id)
        {
            var phongBan = await _context.phongBans.SingleOrDefaultAsync(x => x.PBId == id);
            if (phongBan == null)
            {
                return NotFound();
            }
            return View(phongBan);
        }

        [HttpPost, ActionName("DeletePhongBan")]

        public async Task<IActionResult> DeleteConfirmedPhongBan(int id)
        {
            var phongBan = await _context.phongBans.SingleOrDefaultAsync(x => x.PBId == id);
            _context.phongBans.Remove(phongBan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(QuanLyPhongBan));
        }

        public async Task<IActionResult> DetailsPhongBan(int id,int idnv,int iddelete)
        {
            var nhanvien = await _context.NhanViens.Include(x => x.ChucVu).Where(x => x.TrinhTrang == null).ToListAsync();
            ViewBag.nhanvien = new SelectList(nhanvien, "MaNhanVien", "TenNhanVien");
            var ChiTietPB = await _context.chiTietPhongBans.Include(x=>x.phongBans).Include(x => x.NhanViens.ChucVu).Where(x => x.PBId == id).ToListAsync();
            if (ChiTietPB == null)
            {
                return NotFound();
            }
            if (idnv != 0)
            {
                var nhanvien2 = await _context.NhanViens.Include(x => x.ChucVu).Where(x=> x.MaNhanVien == idnv).FirstOrDefaultAsync();
                foreach(var nhanvienphongban in ChiTietPB)
                {
                    if(nhanvienphongban.NhanViens.MaNhanVien == nhanvien2.MaNhanVien)
                    {
                        TempData["danger"] = "Nhân viên này đã tồn tại trong phòng ban này";
                        return RedirectToAction(nameof(DetailsPhongBan));
                    }
                }
                var chitietpb2 = new ChiTietPhongBan()
                {
                    PBId = id,
                    MaNhanVien = nhanvien2.MaNhanVien,
                };
                _context.chiTietPhongBans.Add(chitietpb2);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm nhân viên thành công";
                return RedirectToAction(nameof(DetailsPhongBan));
            }
            if(iddelete != 0)
            {
                var ChiTietPB3 = await _context.chiTietPhongBans.Include(x => x.phongBans).Include(x => x.NhanViens.ChucVu).Where(x => x.Id == iddelete).FirstOrDefaultAsync();
                _context.chiTietPhongBans.Remove(ChiTietPB3);
                await _context.SaveChangesAsync();
                TempData["success"] = "Xóa nhân viên thành công";
                return RedirectToAction(nameof(DetailsPhongBan));
            }
            return View(ChiTietPB);
        }

        public async Task<IActionResult> DetailsPhongBanUser(int id)
        {
            var ChiTietPB = await _context.chiTietPhongBans.Include(x => x.phongBans).Include(x => x.NhanViens.ChucVu).Where(x => x.PBId == id).ToListAsync();
            if (ChiTietPB == null)
            {
                return NotFound();
            }
            return View(ChiTietPB);
        }
    }
}
