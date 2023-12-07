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
        public IActionResult Index()
		{
			
			return View();
        }
        
        public IActionResult ThemBienLaiChiTiet()
        {
            return View();
        }
    }
}
