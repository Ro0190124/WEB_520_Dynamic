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
	}
}
