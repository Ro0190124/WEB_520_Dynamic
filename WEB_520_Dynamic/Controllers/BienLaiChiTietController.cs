using Microsoft.AspNetCore.Mvc;
using WEB_520_Dynamic.DataAccess.Data;

namespace WEB_520_Dynamic.Controllers
{
    public class BienLaiChiTietController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BienLaiChiTietController(ApplicationDbContext db)
        {
            _db = db;
        }
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
