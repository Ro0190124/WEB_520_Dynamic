using Microsoft.AspNetCore.Mvc;
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
		public IActionResult Index()
		{
			IEnumerable<NHA_CUNG_CAP> obj = _db.NHA_CUNG_CAPs.ToList();
			return View(obj);
		}
		public IActionResult ThemNhaCungCap()
		{
			return View();
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
	}
}
