using Microsoft.AspNetCore.Mvc;
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
		public IActionResult Index()
		{
			var cookie = Request.Cookies["ID"];
			// check cookie
			Console.WriteLine(cookie);
			if (cookie == null)
			{
				return RedirectToAction("DangNhap", "Home");
			}

			var bienLai = _db.BIEN_LAIs.Include(b => b.NHA_CUNG_CAP).Include(b => b.NGUOI_DUNG).OrderByDescending(x=> x.MaBienLai).ToList();
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
