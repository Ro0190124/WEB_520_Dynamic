using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_520_Dynamic.DataAccess.Data;

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
            return View();
        }
		public IActionResult ThemBienLai()
		{
            IEnumerable<SelectListItem> NCC = _db.NHA_CUNG_CAPs.Select(
                u => new SelectListItem()
                {
                    Text = u.TenNhaCungCap,
                    Value = u.MaNhaCungCap.ToString()
                }
                ).ToList();
            ViewBag.NhaCungCap = NCC;
            return View();
		}

	}
}
