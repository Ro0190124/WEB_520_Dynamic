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
			IEnumerable<LO> lo = _db.LOs.ToList();

			return View(lo);
		}
		public IActionResult ThemLo()
		{
			return View();
		}
	}
}
