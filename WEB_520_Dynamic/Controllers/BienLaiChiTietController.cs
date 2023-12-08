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
			IEnumerable<SelectListItem> SP = _db.SAN_PHAMs.Where(x=>x.TrangThai == true).Select(

			   s => new SelectListItem()
			   {
				   Text = s.TenSanPham,
				   Value = s.MaSanPham.ToString()
			   });
			ViewBag.SanPham = SP;

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
            //IEnumerable<LO>? lo = _db.LOs.Include(s => s.SAN_PHAM);


            // lấy mã lô theo mã biên lai.
            var loBienLai = _db.BIEN_LAI_CHI_TIETs.Where(x => x.MaBienLai == id).Select(x => x.MaLo).ToList();
            // trả về list lô 
            List<LO> listSanPham = new List<LO>();
            foreach (var item in loBienLai)
            {
                var lo = _db.LOs.Where(l => l.MaLo == item).Select(x=> x.MaLo).ToString() ;
                //var sanPham = _db.SAN_PHAMs.Where(s => s.MaSanPham == lo.MaSanPham).FirstOrDefault();
                listSanPham.Add(new LO { MaLo = int.Parse(lo)});
            }
            
            var modelview = new BIEN_LAI_CHI_TIET
            {
                BIEN_LAI = bienLai,
                LOs = listSanPham
            };
            return View(modelview);
        }
        
        public IActionResult ThemBienLaiChiTiet()
        {
            return View();
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ThemBienLaiChiTiet(BIEN_LAI_CHI_TIET bl)
		{
			return View();
		}
		public IActionResult ChiTietBienLai(int id)
		{
			var cookie = Request.Cookies["ID"];
			// check cookie
			Console.WriteLine(cookie);
			if (cookie == null)
			{
				return RedirectToAction("DangNhap", "Home");
			}
			var bienLai = _db.BIEN_LAIs.Include(b => b.NHA_CUNG_CAP).Include(b => b.NGUOI_DUNG).Where(x => x.MaBienLai == id).FirstOrDefault();
			return View(bienLai);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ThemLo(LO lo)
		{
			
			if (ModelState.IsValid)
			{
				_db.LOs.Add(lo);
				_db.SaveChanges();
				TempData["ThongBao"] = "Thêm lô thành công";
				return RedirectToAction("Index", "BienLaiChiTiet");
			}
			return View(lo);
		}
	}
}
