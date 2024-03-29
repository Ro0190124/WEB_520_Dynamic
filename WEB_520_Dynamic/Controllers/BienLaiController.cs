﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_520_Dynamic.DataAccess.Data;
using WEB_520_Dynamic.Model;

namespace WEB_520_Dynamic.Controllers
{
	public class BienLaiController : Controller
	{
		private readonly ApplicationDbContext _db;
		public BienLaiController(ApplicationDbContext db)
		{
			_db = db;
		}
		[HttpGet]
		public IActionResult Index(string searchString)
		{
			var cookie = Request.Cookies["ID"];
			// check cookie
			Console.WriteLine(cookie);
			if (cookie == null)
			{
				return RedirectToAction("DangNhap", "Home");
			}

			var bienLai = _db.BIEN_LAIs.Include(b => b.NHA_CUNG_CAP).Include(b => b.NGUOI_DUNG).OrderByDescending(x => x.MaBienLai).ToList();
			bool loaibienlai = false;
			byte trangThai = 3;
			if (!string.IsNullOrEmpty(searchString))
			{
				
				if (searchString.Contains("nhập"))
				{
					loaibienlai = false;
					bienLai = bienLai.Where(x => x.LoaiBienLai == loaibienlai).ToList();
				}
				else if (searchString.Contains("xuất"))
				{
					loaibienlai = true;
					bienLai = bienLai.Where(x => x.LoaiBienLai == loaibienlai).ToList();
				}
				else if (searchString == "tạo" || searchString == "đang tạo")
				{
					trangThai = 0;
					bienLai = bienLai.Where(x => x.TrangThai == trangThai).ToList();
				}
				else if (searchString == "giao" || searchString == "đang giao")
				{
					trangThai = 1;
					bienLai = bienLai.Where(x => x.TrangThai == trangThai).ToList();
				}
				else if (searchString == "hoàn thành" )
				{
					trangThai = 2;
					bienLai = bienLai.Where(x => x.TrangThai == trangThai).ToList();
				}
				else
				{
					bienLai = bienLai.Where(x => x.NGUOI_DUNG.TenNguoiDung.Contains(searchString)).ToList();
				}
				
				Console.WriteLine(searchString);
				Console.WriteLine(trangThai);
				Console.WriteLine(loaibienlai);
				
            }
            return View(bienLai);
		}
		public void TenNCC()
		{
			IEnumerable<SelectListItem> NCC = _db.NHA_CUNG_CAPs.Where(x=> x.TrangThai == true).Select(
				u => new SelectListItem()
				{
					Text = u.TenNhaCungCap,
					Value = u.MaNhaCungCap.ToString()
				}
				).ToList();
			ViewBag.NhaCungCap = NCC;
		}
		public IActionResult ThemBienLai()
		{
			TenNCC();
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ThemBienLai(BIEN_LAI bienLai)
		{
			TenNCC();
			var cookie = Request.Cookies["ID"];
			var nguoiDung = _db.NGUOI_DUNGs.Where(x => x.TenTaiKhoan == cookie).FirstOrDefault();
			if (nguoiDung == null) nguoiDung = new NGUOI_DUNG();
			bienLai.MaNguoiDung = nguoiDung.MaNguoiDung;

			if (bienLai.NgayGiao.Date < DateTime.Now.Date )
			{
				ModelState.AddModelError("NgayGiao", "Ngày giao không hợp lệ");
				return View();

			}
			if(bienLai.LoaiBienLai == true && bienLai.ThongTinGiaoHang == null)
			{
				ModelState.AddModelError("ThongTinGiaoHang", "Phải nhập thông tin giao hàng");
			}
			if (ModelState.IsValid)
			{
				if (bienLai.LoaiBienLai == true)
				{
					Console.WriteLine(bienLai.MaBienLai + "  " + bienLai.MaNhaCungCap + "  " + bienLai.MaNguoiDung + "  " + bienLai.NgayGiao + "  " + bienLai.NgayLap);
					bienLai.MaNhaCungCap = null;
					_db.BIEN_LAIs.Add(bienLai);
					_db.SaveChanges();
					TempData["ThongBao"] = "Thêm biên lai thành công";
					return RedirectToAction("BienLaiCTXuat", "BienLaiChiTiet", new { id = bienLai.MaBienLai });
				}
				else
				{
					Console.WriteLine(bienLai.MaBienLai + "  " + bienLai.MaNhaCungCap + "  " + bienLai.MaNguoiDung + "  " + bienLai.NgayGiao + "  " + bienLai.NgayLap);
					_db.BIEN_LAIs.Add(bienLai);
					_db.SaveChanges();
					TempData["ThongBao"] = "Thêm biên lai thành công";
					return RedirectToAction("Index", "BienLaiChiTiet", new { id = bienLai.MaBienLai });
				}

			}
			return View();
		}
		public IActionResult ChiTietBienLai(int id)
		{
			var cookie = Request.Cookies["ID"];
			// check cookie
			Console.WriteLine(cookie);
			if (cookie == null)
			{
				return RedirectToAction("DangNhap", "Home");
			}
			//var bienLai = _db.BIEN_LAIs.Include(b => b.NHA_CUNG_CAP).Include(b => b.NGUOI_DUNG).Where(x => x.MaBienLai == id).FirstOrDefault();
			var bienLai = _db.BIEN_LAIs.Where(x => x.MaBienLai == id).FirstOrDefault();
			//var sanPham = 

			return View(bienLai);
		}
	}
}
