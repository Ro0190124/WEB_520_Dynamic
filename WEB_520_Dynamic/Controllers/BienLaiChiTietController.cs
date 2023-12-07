using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

            /*
            var bienLai = _db.BIEN_LAIs.FirstOrDefault(b => b.MaBienLai == id);
			var tenNhaCungCap = _db.NHA_CUNG_CAPs.FirstOrDefault(n => n.MaNhaCungCap == bienLai.MaNhaCungCap)?.TenNhaCungCap;

			ViewBag.TenNhaCungCap = tenNhaCungCap;
			return View(bienLai);*/
            BIEN_LAI bienLai = _db.BIEN_LAIs.Include(b => b.NHA_CUNG_CAP).FirstOrDefault(b => b.MaBienLai == id);
            var tenNhaCungCap = _db.NHA_CUNG_CAPs.FirstOrDefault(n => n.MaNhaCungCap == bienLai.MaNhaCungCap)?.TenNhaCungCap;

            ViewBag.TenNhaCungCap = tenNhaCungCap;

            /*var Lo = _db.SAN_PHAMs.Join(_db.LOs, sp => sp.MaSanPham, lo => lo.MaSanPham, (sp, lo) => new LO_SAN_PHAM { lo = lo, sanPham = sp }).ToList();*/
            // lấy lô có mã biên lai = id
            //var Lo = _db.LOs.Where(l => l.MaBienLai == id).ToList();
            IEnumerable<LO>? lo = _db.LOs.ToList();
            var modelview = new BIEN_LAI_CHI_TIET
            {
                BIEN_LAI = bienLai,
                
                LOs = lo,
                
            };
            return View(modelview);
        }
        
        public IActionResult ThemBienLaiChiTiet()
        {
            return View();
        }
    }
}
