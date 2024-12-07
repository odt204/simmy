using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimmyWeb.Data;

namespace SimmyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamsController : Controller
    {
        private readonly SimmyWebContext _context;

        public SanPhamsController(SimmyWebContext context)
        {
            _context = context;
        }

        // GET: Admin/SanPhams
        public async Task<IActionResult> Index()
        {
            var simmyWebContext = _context.SanPhams.Include(s => s.MaDanhMucNavigation).Include(s => s.MaNhaCungCapNavigation);
            return View(await simmyWebContext.ToListAsync());
        }

        // GET: Admin/SanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaDanhMucNavigation)
                .Include(s => s.MaNhaCungCapNavigation)
                .FirstOrDefaultAsync(m => m.MaSanPham == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: Admin/SanPhams/Create
        public IActionResult Create()
        {
            ViewData["MaDanhMuc"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            ViewData["MaNhaCungCap"] = new SelectList(_context.NhaCungCaps, "MaNhaCungCap", "TenNhaCungCap");
            return View();
        }

        // POST: Admin/SanPhams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenSanPham,GiaTienMoi,GiaTienCu,MoTa,MaDanhMuc,MaNhaCungCap,SoLuong")] SanPham sanPham, IFormFile AnhSp)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra và lưu hình ảnh nếu có
                if (AnhSp != null && AnhSp.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", AnhSp.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await AnhSp.CopyToAsync(stream);
                    }
                    sanPham.AnhSp = "/images/" + AnhSp.FileName;
                }
                else
                {
                    // Nếu không có ảnh, gán giá trị mặc định
                    sanPham.AnhSp = "/images/default.png"; // Đảm bảo bạn có ảnh mặc định
                }

                // Thêm ngày tạo hiện tại
                sanPham.NgayTao = DateOnly.FromDateTime(DateTime.Now);

                // Thêm sản phẩm vào cơ sở dữ liệu
                _context.Add(sanPham);
                await _context.SaveChangesAsync();

                // Chuyển hướng về trang danh sách sản phẩm
                return RedirectToAction(nameof(Index));
            }

            // Nếu ModelState không hợp lệ, trả về lại View
            ViewData["MaDanhMuc"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewData["MaNhaCungCap"] = new SelectList(_context.NhaCungCaps, "MaNhaCungCap", "TenNhaCungCap", sanPham.MaNhaCungCap);
            return View(sanPham);
        }






        // GET: Admin/SanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["MaDanhMuc"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "MaDanhMuc", sanPham.MaDanhMuc);
            ViewData["MaNhaCungCap"] = new SelectList(_context.NhaCungCaps, "MaNhaCungCap", "MaNhaCungCap", sanPham.MaNhaCungCap);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSanPham,TenSanPham,GiaTienMoi,GiaTienCu,MoTa,AnhSp,MaVatLieu,MaDanhMuc,NgayTao,MaNhaCungCap")]
        SanPham sanPham, IFormFile file)
        {
            if (id != sanPham.MaSanPham)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingSanPham = await _context.SanPhams.FindAsync(id);
                if (existingSanPham == null)
                {
                    return NotFound();
                }

                // Lưu giá cũ để tạo bản ghi lịch sử
                int giaCu = existingSanPham.GiaTienMoi;
                string anhSp = existingSanPham.AnhSp;
                try
                {
                    // Kiểm tra xem có tệp hình ảnh mới không
                    if (file != null && file.Length > 0)
                    {
                        // Tạo tên file duy nhất để tránh xung đột
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                        // Xóa hình ảnh cũ (nếu cần)

                        // Lưu file vào thư mục wwwroot/images
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Gán đường dẫn của ảnh mới cho thuộc tính AnhSp
                        existingSanPham.AnhSp = "/images/" + fileName; // Đảm bảo đường dẫn hợp lệ
                    }
                    else
                    {
                        existingSanPham.AnhSp = anhSp;
                    }
                    // Kiểm tra và lưu lịch sử giá sản phẩm
                    if (sanPham.GiaTienMoi != giaCu)
                    {
                        var lichSuGia = new LichSuGiaSanPham
                        {
                            MaSanPham = sanPham.MaSanPham,
                            GiaCu = giaCu,
                            GiaMoi = sanPham.GiaTienMoi,
                            NgayCapNhat = DateTime.Now
                        };

                        _context.LichSuGiaSanPhams.Add(lichSuGia);
                    }

                    // Cập nhật thông tin sản phẩm, chỉ cập nhật giá mới
                    existingSanPham.GiaTienMoi = sanPham.GiaTienMoi; // Cập nhật giá mới
                    existingSanPham.GiaTienCu = giaCu; // Cập nhật giá mới
                    existingSanPham.TenSanPham = sanPham.TenSanPham;
                    existingSanPham.MaDanhMuc = sanPham.MaDanhMuc;
                    existingSanPham.MaNhaCungCap = sanPham.MaNhaCungCap;
                    existingSanPham.MoTa = sanPham.MoTa;

                    // Cập nhật thông tin sản phẩm
                    _context.Update(existingSanPham);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.MaSanPham))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDanhMuc"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "MaDanhMuc", sanPham.MaDanhMuc);
            ViewData["MaNhaCungCap"] = new SelectList(_context.NhaCungCaps, "MaNhaCungCap", "MaNhaCungCap", sanPham.MaNhaCungCap);
            return View(sanPham);
        }

      // GET: Admin/SanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaDanhMucNavigation)
                .Include(s => s.MaNhaCungCapNavigation)
                
                .FirstOrDefaultAsync(m => m.MaSanPham == id);
            if (sanPham == null)
            {
                return NotFound();
            }
                
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPhams.Any(e => e.MaSanPham == id);
        }
        public IActionResult CheckProductName(string productName)
        {
            var exists = _context.SanPhams.Any(sp => sp.TenSanPham.Equals(productName, StringComparison.OrdinalIgnoreCase));
            return Json(new { exists });
        }
    }
}
