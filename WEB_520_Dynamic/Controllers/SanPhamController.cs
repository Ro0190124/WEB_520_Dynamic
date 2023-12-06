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
            // get cookies
            var cookie = Request.Cookies["ID"];
            // check cookie
            Console.WriteLine(cookie);
            if (cookie == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            else
            {
				IEnumerable<SAN_PHAM> sanPham = _db.SAN_PHAMs.ToList();

				return View(sanPham);

            }
           
        }
		public IActionResult ThemSanPham()
        {// get cookies
            var cookie = Request.Cookies["ID"];
            // check cookie
            Console.WriteLine(cookie);
            if (cookie == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            else
            {
				return View();

            }
           
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPham(SAN_PHAM sanPham)
        {
            if (ModelState.IsValid)
            {
                _db.SAN_PHAMs.Add(sanPham);
                _db.SaveChanges(); 
                TempData["ThongBao"] = "Thêm sản phẩm thành công";
				return RedirectToAction("Index", "SanPham");
			}
            else
            {
                return View(sanPham);
            }
        }
		public IActionResult SuaSanPham(int? ID)
		{
			if (ID == null || ID == 0)
			{
				return NotFound();
			}
			SAN_PHAM sanPham = _db.SAN_PHAMs.First(x => x.MaSanPham == ID);
			if (sanPham == null)
			{
				return NotFound();
			}
			return View(sanPham);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult SuaSanPham(SAN_PHAM sanPham)
		{
			if (ModelState.IsValid)
			{
				_db.SAN_PHAMs.Update(sanPham);
				_db.SaveChanges();
				TempData["ThongBao"] = "Sửa sản phẩm thành công";
				return RedirectToAction("Index", "SanPham");
			}
			else
			{
				return View(sanPham);
			}
		}
		public IActionResult XoaSanPham(int? ID)
		{
			if (ID == null || ID == 0)
			{
				return NotFound();
			}
			SAN_PHAM sanPham = _db.SAN_PHAMs.First(x => x.MaSanPham == ID);
			if (sanPham == null)
			{
				return NotFound();
			}
			else
			{
				sanPham.TrangThai = false;
				_db.SAN_PHAMs.Update(sanPham);
				TempData["ThongBaoXoa"] = "Xóa Sàn Phẩm thành công";
				_db.SaveChanges();

			}
			return RedirectToAction("Index");
		}
		

	}
}
