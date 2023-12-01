using Microsoft.AspNetCore.Mvc;
using WEB_520_Dynamic.DataAccess.Data;
using WEB_520_Dynamic.Model;

namespace WEB_520_Dynamic.Controllers
{
	public class NguoiDungController : Controller
	{
		private readonly ApplicationDbContext _db;
		public NguoiDungController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			IEnumerable<NGUOI_DUNG> obj = _db.NGUOI_DUNGs.ToList();
			return View(obj);
		}
	}
}
