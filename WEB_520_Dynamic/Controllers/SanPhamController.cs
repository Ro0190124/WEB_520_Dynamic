using Microsoft.AspNetCore.Mvc;
using WEB_520_Dynamic.DataAccess.Data;
using WEB_520_Dynamic.Model;

namespace WEB_520_Dynamic.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SanPhamController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<SAN_PHAM> sanPham = _db.SAN_PHAMs.ToList();

            return View(sanPham);
        }
		public IActionResult ThemSanPham()
		{
			return View();
		}
	}
}
