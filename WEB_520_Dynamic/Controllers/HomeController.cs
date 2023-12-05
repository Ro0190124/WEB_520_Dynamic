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
            ViewData["HideHeader"] = true;
            Console.WriteLine("Submit");
			if (ModelState.IsValid)
			{
				_db.NGUOI_DUNGs.Add(nguoiDung);
				_db.SaveChanges();
				TempData["ThongBao"] = "Đăng kí thành công";
				return View();

			}
			else
			{
				return View(nguoiDung);
			}
			/*//var n = _db.NGUOI_DUNGs.Where(x => x.TenTaiKhoan == nguoiDung.TenTaiKhoan).First();
			if (n != null)
			{
				ModelState.AddModelError("TenTaiKhoan", "Tên tài khoản đã tồn tại");
				return View(nguoiDung);
			}
            else
            {
				
			}*/
			

        }

        [HttpPost]
        public IActionResult DangNhap(string tenTaiKhoan, string matKhau)
        {
            ViewData["HideHeader"] = true;
            var nguoiDung = _db.NGUOI_DUNGs.First(x => x.TenTaiKhoan == tenTaiKhoan && x.MatKhau == matKhau && x.TrangThai == true);

            if (nguoiDung == null)
            {
                TempData["ThongBaoDangNhap"] = "Tên tài khoản hoặc mật khẩu không đúng";
                return View();
            }
            else
            {

                return RedirectToAction("Index", "NguoiDung");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}