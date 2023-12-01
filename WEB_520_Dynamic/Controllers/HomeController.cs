using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEB_520_Dynamic.DataAccess.Data;
using WEB_520_Dynamic.Model;

namespace WEB_520_Dynamic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
       // private readonly ILogger<HomeController> _logger;

        /* public HomeController(ILogger<HomeController> logger)
         {
             _logger = logger;
         }*/
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult DangNhap()
        {
            ViewData["HideHeader"] = true;
            return View();

        }
       
        public IActionResult DangKi()
        {
            ViewData["HideHeader"] = true;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangKi(NGUOI_DUNG nguoiDung)
        {
            Console.WriteLine("Submit");
            /* if (nguoiDung.TenTaiKhoan != @"^[a-zA-Z0-9]+$")
             {
                 ModelState.AddModelError("TenTaiKhoan", "Tên tài khoản chỉ được chứa ký tự và số");
             }*/

            if (ModelState.IsValid)
            {

                Console.WriteLine("Create user");
                _db.NGUOI_DUNGs.Add(nguoiDung);
                _db.SaveChanges();
                return RedirectToAction("DangNhap");
            }
            else
            {
                Console.WriteLine("No, i can't create");
                return View(nguoiDung);
            }

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}