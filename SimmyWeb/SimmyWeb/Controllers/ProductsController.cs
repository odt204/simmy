using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimmyWeb.Data;

namespace SimmyWeb.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SimmyWebContext _context;

        public ProductsController(SimmyWebContext context)
        {
            _context = context;
        }

        // GET: Products
        public IActionResult Index()
        {
            var sanPhams = _context.SanPhams.ToList();
            return View(sanPhams);
        }

        // GET: Products/ChiTietSP/5
        public async Task<IActionResult> ChiTietSP(int? id)
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


        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["MaDanhMuc"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "MaDanhMuc");
            ViewData["MaNhaCungCap"] = new SelectList(_context.NhaCungCaps, "MaNhaCungCap", "MaNhaCungCap");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSanPham,TenSanPham,GiaTienMoi,GiaTienCu,MoTa,AnhSp,MaDanhMuc,NgayTao,MaNhaCungCap")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDanhMuc"] = new SelectList(_context.DanhMucs, "MaDanhMuc", "MaDanhMuc", sanPham.MaDanhMuc);
            ViewData["MaNhaCungCap"] = new SelectList(_context.NhaCungCaps, "MaNhaCungCap", "MaNhaCungCap", sanPham.MaNhaCungCap);
            return View(sanPham);
        }

        // GET: Products/Edit/5
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

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSanPham,TenSanPham,GiaTienMoi,GiaTienCu,MoTa,AnhSp,MaDanhMuc,NgayTao,MaNhaCungCap")] SanPham sanPham)
        {
            if (id != sanPham.MaSanPham)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
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

        // GET: Products/Delete/5
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

        // POST: Products/Delete/5
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
    }
}
