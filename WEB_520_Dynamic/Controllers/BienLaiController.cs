using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_520_Dynamic.DataAccess.Data;
using WEB_520_Dynamic.Model;

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
            IEnumerable<SelectListItem> LO = _db.LOs.Select(

                u => new SelectListItem()
                {
                    Text = u.TenLo,
                    Value = u.MaLo.ToString()
                });
            ViewBag.Lo = LO;
            return View();
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemBienLai(BIEN_LAI bienLai)
        {
            if (ModelState.IsValid)
            {
				_db.BIEN_LAIs.Add(bienLai);
				_db.SaveChanges();
				TempData["ThongBao"] = "Thêm người dùng thành công";
				return RedirectToAction("Index", "BienLaiChiTiet");
			}
            return View();
        }
        

    }
}
