using Microsoft.AspNetCore.Mvc;
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
		public IActionResult XoaLo(int? ID)
		{
			if (ID == null || ID == 0)
			{
				return NotFound();
			}
			BIEN_LAI_CHI_TIET bienLaiCT = _db.BIEN_LAI_CHI_TIETs.FirstOrDefault(x => x.MaLo == ID && x.BIEN_LAI.LoaiBienLai == false);
			LO lo = _db.LOs.FirstOrDefault(x => x.MaLo == ID);
			if (lo == null)
			{
				return NotFound();
			}
			else
			{
				_db.LOs.Remove(lo);
				_db.SaveChanges();
				TempData["ThongBaoXoa"] = "Xóa Sàn Phẩm thành công";
			}
			
			return RedirectToAction("Index", "BienLaiChiTiet", new { id = bienLaiCT.MaBienLai});
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ThemLoXuat(BIEN_LAI_CHI_TIET bienLai, int id)
		{

			Console.WriteLine("Mã biên lai : " + id);
			Console.WriteLine("Mã lô: " + bienLai.MaLo);
			Console.WriteLine("Số lượng: " + bienLai.SoLuong);
			if (ModelState.IsValid)
			{
				
				//thêm biên lai chi tiết với mã biên lai = 
				/*BIEN_LAI_CHI_TIET bienLaiCT = new BIEN_LAI_CHI_TIET();
				bienLaiCT.MaBienLai = id;*/
				bienLai.MaBienLai = id;
				Console.WriteLine(bienLai.MaBienLai + " " + bienLai.MaLo + " " + bienLai.SoLuong);
				_db.BIEN_LAI_CHI_TIETs.Add(bienLai);
				_db.SaveChanges();
				LO lo = _db.LOs.Where(x=> x.MaLo ==  bienLai.MaLo).FirstOrDefault();
				lo.SoLuong -= bienLai.SoLuong;
				_db.LOs.Update(lo);
				_db.SaveChanges();
				TempData["ThongBao"] = "Thêm lô thành công";
				return RedirectToAction("BienLaiCTXuat", "BienLaiChiTiet", new { id = bienLai.MaBienLai });
			}
			return View(bienLai);
		}
		public IActionResult XoaLoXuat(int? ID)
		{
			if (ID == null || ID == 0)
			{
				return NotFound();
			}
			BIEN_LAI_CHI_TIET bienLaiCT = _db.BIEN_LAI_CHI_TIETs.FirstOrDefault(x => x.MaLo == ID && x.BIEN_LAI.LoaiBienLai == true);
			Console.WriteLine("Mã biên lai : " + bienLaiCT.MaBienLai);
			Console.WriteLine("Mã lô: " + bienLaiCT.MaLo);
			//Console.WriteLine("Loại Biên lai: " + bienLaiCT.BIEN_LAI.LoaiBienLai);
			LO lo = _db.LOs.FirstOrDefault(x => x.MaLo == ID);
			if (lo == null)
			{
				return NotFound();
			}
			else
			{
				LO lobandau = _db.LOs.Where(x=> x.MaLo == ID).FirstOrDefault();
				lobandau.SoLuong += bienLaiCT.SoLuong;
				_db.LOs.Update(lobandau);
				_db.SaveChanges();
				_db.BIEN_LAI_CHI_TIETs.Remove(bienLaiCT);
				_db.SaveChanges();

				TempData["ThongBaoXoa"] = "Xóa Sàn Phẩm thành công";
			}

			return RedirectToAction("BienLaiCTXuat", "BienLaiChiTiet", new { id = bienLaiCT.MaBienLai });
		}
		


	}
}
