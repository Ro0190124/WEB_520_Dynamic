using Microsoft.AspNetCore.Mvc;
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
			return View();
		}
	}
}
