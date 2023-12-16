﻿using Microsoft.AspNetCore.Mvc;
using WEB_520_Dynamic.DataAccess.Data;
using WEB_520_Dynamic.Model;

namespace WEB_520_Dynamic.Controllers
{
	public class NhaCungCapController : Controller
	{
		private readonly ApplicationDbContext _db;
		public NhaCungCapController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index(string searchString)
		{
            // get cookies
            var cookie = Request.Cookies["ID"];
            // check cookie
            Console.WriteLine(cookie);
            if (cookie == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            else
            {
               var nhaCC = _db.NHA_CUNG_CAPs.Where(x=> x.TrangThai == true).OrderByDescending(x=> x.MaNhaCungCap).ToList();
				if (!string.IsNullOrEmpty(searchString))
				{
					nhaCC= nhaCC.Where(x => x.TenNhaCungCap.Contains(searchString) || x.NguoiDaiDien.Contains(searchString) || x.SoDienThoai.Contains(searchString)).ToList();
				}
				return View(nhaCC);

            }
           
		}
		public IActionResult ThemNhaCungCap()
        {// get cookies
            var cookie = Request.Cookies["ID"];
            // check cookie
            Console.WriteLine(cookie);
            if (cookie == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            else
            {
                return View();

            }
            
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ThemNhaCungCap(NHA_CUNG_CAP nhaCungCap)
		{
			if (ModelState.IsValid)
			{
				_db.NHA_CUNG_CAPs.Add(nhaCungCap);
				_db.SaveChanges();
				TempData["ThongBao"] = "Thêm nhà cung cấp thành công";
				return RedirectToAction("Index", "NhaCungCap");
			}
			else
			{
				return View(nhaCungCap);
			}
		}
        public IActionResult SuaNhaCungCap(int? ID)
        {
            // get cookies
            var cookie = Request.Cookies["ID"];
            // check cookie
            Console.WriteLine(cookie);
            if (cookie == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            else
            {
               
                if (ID == null || ID == 0)
                {
                    return NotFound();
                }
                NHA_CUNG_CAP nhaCC = _db.NHA_CUNG_CAPs.First(x => x.MaNhaCungCap == ID);
                if (nhaCC == null)
                {
                    return NotFound();
                }
                return View(nhaCC);
            }
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNhaCungCap(NHA_CUNG_CAP nhaCC)
        {
            if (ModelState.IsValid)
            {
                _db.NHA_CUNG_CAPs.Update(nhaCC);
                _db.SaveChanges();
                TempData["ThongBao"] = "Sửa nhà cung cấp thành công";
                return RedirectToAction("Index", "NhaCungCap");
            }
            else
            {
                return View(nhaCC);
            }
        }
        public IActionResult XoaNhaCungCap(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            NHA_CUNG_CAP nhaCC = _db.NHA_CUNG_CAPs.FirstOrDefault(x => x.MaNhaCungCap == id);
            if (nhaCC == null)
            {
                return NotFound();
            }
            else
            {
                nhaCC.TrangThai = false;
                _db.NHA_CUNG_CAPs.Update(nhaCC);
                TempData["ThongBaoXoa"] = "Xóa nhà cung cấp thành công";
                _db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}
