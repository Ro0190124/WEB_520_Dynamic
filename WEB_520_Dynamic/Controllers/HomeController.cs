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
            Response.Cookies.Delete("ID");
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
            
            var n = _db.NGUOI_DUNGs.Where(x => x.TenTaiKhoan == nguoiDung.TenTaiKhoan && x.TrangThai == true).FirstOrDefault();
            var m = _db.NGUOI_DUNGs.Where(x => x.SoDienThoai == nguoiDung.SoDienThoai && x.TrangThai == true).FirstOrDefault();
            if (n != null )
            {
                ModelState.AddModelError("TenTaiKhoan", "Tên tài khoản đã tồn tại");
                return View(nguoiDung);
            }
            if (m != null )
            {
                ModelState.AddModelError("SoDienThoai", " Số điện thoại đã tồn tại");
                return View(nguoiDung);
            }
            else
            {
                if (ModelState.IsValid)
                {

                    _db.NGUOI_DUNGs.Add(nguoiDung);
                    _db.SaveChanges();
                    TempData["ThongBao"] = "Đăng kí thành công";
                    return RedirectToAction("DangNhap", "Home");
                }
                else
                {
                    return View(nguoiDung);
                }
            }

        }

        [HttpPost]
        public IActionResult DangNhap(string tenTaiKhoan, string matKhau)
        {
            
            ViewData["HideHeader"] = true;
           
            var nguoiDung = _db.NGUOI_DUNGs.FirstOrDefault(x => x.TenTaiKhoan == tenTaiKhoan && x.MatKhau == matKhau && x.TrangThai == true);
          
            if (nguoiDung != null)
            {
               
                Response.Cookies.Append("ID", nguoiDung.TenTaiKhoan.ToString());

                //TempData.Add("TenNguoiDung", nguoiDung.TenTaiKhoan);
				return RedirectToAction("Index", "NguoiDung");
            }
            else
            {
              
                TempData["ThongBaoDangNhap"] = "Tên tài khoản hoặc mật khẩu không đúng";
                return View();

            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}