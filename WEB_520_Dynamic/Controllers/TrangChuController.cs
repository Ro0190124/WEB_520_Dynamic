using Microsoft.AspNetCore.Mvc;
using WEB_520_Dynamic.DataAccess.Data;
using WEB_520_Dynamic.Model;

namespace WEB_520_Dynamic.Controllers
{
    public class TrangChuController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TrangChuController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            var tongSanPham = _db.BIEN_LAI_CHI_TIETs
                                .Where(blct => blct.BIEN_LAI.TrangThai == 2)
                                .Sum(blct => blct.SoLuong);

			var tongdonnhap = _db.BIEN_LAIs
								.Where(blct => blct.TrangThai == 2 && blct.LoaiBienLai == true)
								.Count();

			var tongdonxuat = _db.BIEN_LAIs
								.Where(blct => blct.TrangThai == 2 && blct.LoaiBienLai == false)
								.Count();
			Console.WriteLine(tongdonnhap + tongdonxuat);
			// Truyền các giá trị tính toán vào ViewBag hoặc ViewData
			ViewBag.TongSanPham = tongSanPham;
			ViewBag.TongDonNhap = tongdonnhap;
			ViewBag.TongDonXuat = tongdonxuat;
			Console.WriteLine(tongSanPham.ToString()+"    "+ tongdonnhap.ToString()+"    "+ tongdonxuat.ToString());

            return View();

        }
        [HttpGet]
        public IActionResult Linechartctl(int year)
        {
			//var allMonths = Enumerable.Range(1, 12); // Tạo danh sách từ tháng 1 đến tháng 12
			//var data = _db.BIEN_LAI_CHI_TIETs
			//    .Where(bl => bl.BIEN_LAI.NgayLap.Year == year)
			//    .GroupBy(bl => bl.BIEN_LAI.NgayLap.Month)
			//    .Select(group => new { Month = group.Key, TotalQuantity = group.Sum(bl => bl.SoLuong) })
			//    .OrderBy(x => x.Month)
			//    .ToList();

			//var result = allMonths.GroupJoin(data, m => m, d => d.Month, (m, d) => new { Month = m, TotalQuantity = d.Select(x => x.TotalQuantity).DefaultIfEmpty(0).FirstOrDefault() })
			//                      .OrderBy(x => x.Month)
			//                      .ToList();
			//return Json(result);
			var allMonths = Enumerable.Range(1, 12); // Tạo danh sách từ tháng 1 đến tháng 12

			// Tổng sản phẩm
			var tongsl = _db.BIEN_LAI_CHI_TIETs
				.Where(bl => bl.BIEN_LAI.NgayLap.Year == year)
				.GroupBy(bl => bl.BIEN_LAI.NgayLap.Month)
				.Select(group => new { Month = group.Key, TotalQuantity = group.Sum(bl => bl.SoLuong) })
				.OrderBy(x => x.Month)
				.ToList();

			// Tổng sản phẩm nhập
			var tongsln = _db.BIEN_LAI_CHI_TIETs
				.Where(bl => bl.BIEN_LAI.NgayLap.Year == year && bl.BIEN_LAI.LoaiBienLai == false) // Loại 0 là nhập
				.GroupBy(bl => bl.BIEN_LAI.NgayLap.Month)
				.Select(group => new { Month = group.Key, TotalQuantity = group.Sum(bl => bl.SoLuong) })
				.OrderBy(x => x.Month)
				.ToList();

			// Tổng sản phẩm xuất
			var tongslx = _db.BIEN_LAI_CHI_TIETs
				.Where(bl => bl.BIEN_LAI.NgayLap.Year == year && bl.BIEN_LAI.LoaiBienLai == true) // Loại 1 là xuất
				.GroupBy(bl => bl.BIEN_LAI.NgayLap.Month)
				.Select(group => new { Month = group.Key, TotalQuantity = group.Sum(bl => bl.SoLuong) })
				.OrderBy(x => x.Month)
				.ToList();

			// Gộp dữ liệu cho từng tháng
			var result = allMonths.GroupJoin(tongsl, m => m, d => d.Month, (m, d) =>
				new
				{
					Month = m,
					tongslbl = d.Select(x => x.TotalQuantity).DefaultIfEmpty(0).FirstOrDefault(),
					tongslbln = tongsln.Where(tpi => tpi.Month == m).Select(tpi => tpi.TotalQuantity).FirstOrDefault(),
					tongslblx = tongslx.Where(tpo => tpo.Month == m).Select(tpo => tpo.TotalQuantity).FirstOrDefault()
				})
				.OrderBy(x => x.Month)
				.ToList();

			return Json(result);
		}
    }


}

