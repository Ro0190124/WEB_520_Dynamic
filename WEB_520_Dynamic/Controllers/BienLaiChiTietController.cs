using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_520_Dynamic.DataAccess.Data;
using WEB_520_Dynamic.Model;

namespace WEB_520_Dynamic.Controllers
{
    public class BienLaiChiTietController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BienLaiChiTietController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index(int id)
		{
			var bienLai = _db.BIEN_LAIs.FirstOrDefault(b => b.MaBienLai == id);
			var tenNhaCungCap = _db.NHA_CUNG_CAPs.FirstOrDefault(n => n.MaNhaCungCap == bienLai.MaNhaCungCap)?.TenNhaCungCap;

			ViewBag.TenNhaCungCap = tenNhaCungCap;
			return View(bienLai);
        }
        
        public IActionResult ThemBienLaiChiTiet()
        {
            return View();
        }
    }
}
