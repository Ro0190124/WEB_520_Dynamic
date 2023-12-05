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

	}
}
