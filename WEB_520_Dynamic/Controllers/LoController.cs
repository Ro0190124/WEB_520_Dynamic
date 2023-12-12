﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WEB_520_Dynamic.DataAccess.Data;
using WEB_520_Dynamic.Model;

namespace WEB_520_Dynamic.Controllers
{
	public class LoController : Controller
	{
		private readonly ApplicationDbContext _db;
		public LoController(ApplicationDbContext db)
		{
			_db = db;
		}
		[HttpGet]
		public IActionResult Index(int? ID)
		{
		
			Console.WriteLine(ID);
			IEnumerable<LO> groupSpL = _db.LOs.Include(x => x.SAN_PHAM);
			return View(groupSpL);
		}
		public IActionResult ThemLo()
		{
			IEnumerable<SelectListItem> SP = _db.SAN_PHAMs.Where(x => x.TrangThai == true).Select(

			   s => new SelectListItem()
			   {
				   Text = s.TenSanPham,
				   Value = s.MaSanPham.ToString()
			   });
			ViewBag.SanPham = SP;
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ThemLo(LO lo, int id)
		{
			
			Console.WriteLine(lo.MaLo);
			Console.WriteLine(lo.MaLo.ToString());
			Console.WriteLine(lo.SoLuong);
			Console.WriteLine(lo.SAN_PHAM.MaSanPham);
			Console.WriteLine("Mã biên lai : " + id);
			lo.MaSanPham = lo.SAN_PHAM.MaSanPham;
			lo.SAN_PHAM = _db.SAN_PHAMs.Where(x => x.MaSanPham == lo.SAN_PHAM.MaSanPham).First();
			
			if (ModelState.IsValid)
			{
				_db.LOs.Add(lo);
				_db.SaveChanges();
				/*IEnumerable<EntityEntry> es = _db.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
				IEnumerable<EntityEntry> es2 = _db.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
				IEnumerable<EntityEntry> es3 = _db.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
				foreach(EntityEntry e in es)
				{
					Console.WriteLine(e.ToString());
				}
				foreach(EntityEntry e in es2)
				{ Console.WriteLine(e.ToString()); }
				foreach(EntityEntry e in es3)
				{ Console.WriteLine(e.ToString()); }*/

				//thêm biên lai chi tiết với mã biên lai = 
				BIEN_LAI_CHI_TIET bienLaiCT = new BIEN_LAI_CHI_TIET();
				//bienLaiCT.BIEN_LAI = _db.BIEN_LAIs.FirstOrDefault(x => x.MaBienLai == id);
				bienLaiCT.MaBienLai = id;
				bienLaiCT.MaLo = lo.MaLo;
				bienLaiCT.SoLuong = lo.SoLuong;
				Console.WriteLine(bienLaiCT.MaBienLai + " " + bienLaiCT.MaLo);
				_db.BIEN_LAI_CHI_TIETs.Add(bienLaiCT);
				_db.SaveChanges();
				TempData["ThongBao"] = "Thêm lô thành công";
				return RedirectToAction("Index", "BienLaiChiTiet", new { id = bienLaiCT.MaBienLai });
				//return RedirectToAction("Index", "BienLaiChiTiet");
			}
			return View(lo);
		}
	}
}
