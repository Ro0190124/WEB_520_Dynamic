using Microsoft.AspNetCore.Mvc;
using WEB_520_Dynamic.DataAccess.Data;

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
            return View();

        }
        [HttpGet]
        public IActionResult Linechartctl(int year)
        {
            var allMonths = Enumerable.Range(1, 12); // Tạo danh sách từ tháng 1 đến tháng 12
            var data = _db.BIEN_LAI_CHI_TIETs
                .Where(bl => bl.BIEN_LAI.NgayLap.Year == year)
                .GroupBy(bl => bl.BIEN_LAI.NgayLap.Month)
                .Select(group => new { Month = group.Key, TotalQuantity = group.Sum(bl => bl.SoLuong) })
                .OrderBy(x => x.Month)
                .ToList();

            var result = allMonths.GroupJoin(data, m => m, d => d.Month, (m, d) => new { Month = m, TotalQuantity = d.Select(x => x.TotalQuantity).DefaultIfEmpty(0).FirstOrDefault() })
                                  .OrderBy(x => x.Month)
                                  .ToList();
            return Json(result);
        }
    }


}

