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
		public IActionResult Index()
		{
		
			IEnumerable<LO_SAN_PHAM> groupSpL = _db.SAN_PHAMs.Join(_db.LOs, sp => sp.MaSanPham, lo => lo.MaSanPham, (sp, lo) => new LO_SAN_PHAM { lo = lo, sanPham = sp }).ToList();
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
		public IActionResult ThemLo(LO lo)
		{
			
			Console.WriteLine(lo.MaLo);
			Console.WriteLine(lo.MaLo.ToString());
			Console.WriteLine(lo.SoLuong);
			Console.WriteLine(lo.SAN_PHAM.MaSanPham);
			lo.MaSanPham = lo.SAN_PHAM.MaSanPham;
			lo.SAN_PHAM = _db.SAN_PHAMs.Where(x => x.MaSanPham == lo.SAN_PHAM.MaSanPham).First();
			
			if (ModelState.IsValid)
			{
				_db.LOs.Add(lo);
				IEnumerable<EntityEntry> es = _db.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
				IEnumerable<EntityEntry> es2 = _db.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
				IEnumerable<EntityEntry> es3 = _db.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
				foreach(EntityEntry e in es)
				{
					Console.WriteLine(e.ToString());
				}
				foreach(EntityEntry e in es2)
				{ Console.WriteLine(e.ToString()); }
				foreach(EntityEntry e in es3)
				{ Console.WriteLine(e.ToString()); }
				_db.SaveChanges();
				TempData["ThongBao"] = "Thêm lô thành công";
				return RedirectToAction("Index", "Lo");
			}
			return View(lo);
		}
	}
}
