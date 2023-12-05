using Microsoft.AspNetCore.Mvc;
using WEB_520_Dynamic.DataAccess.Data;
using WEB_520_Dynamic.Model;

namespace WEB_520_Dynamic.Controllers
{
	public class NguoiDungController : Controller
	{
		private readonly ApplicationDbContext _db;
		public NguoiDungController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index(string searchString)
		{
            var nguoiDung = from u in _db.NGUOI_DUNGs
							where u.TrangThai == true// lấy toàn bộ liên kết
                        select u;
           // IEnumerable<NGUOI_DUNG> nguoiDung = _db.NGUOI_DUNGs.Where(x=>x.TrangThai == true).ToList();
            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                nguoiDung = nguoiDung.Where(s => s.TenTaiKhoan.Contains(searchString) || s.SoDienThoai.Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            return View(nguoiDung);
		}
		public IActionResult ThemNguoiDung()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ThemNguoiDung(NGUOI_DUNG nguoiDung)
		{
			
			var n = _db.NGUOI_DUNGs.Where(x => x.TenTaiKhoan == nguoiDung.TenTaiKhoan).FirstOrDefault();
			if (n != null)
			{
				ModelState.AddModelError("TenTaiKhoan", "Tên tài khoản đã tồn tại");
				return View(nguoiDung);
			}
			/*if ( == "")
			{
				ModelState.AddModelError("TenTaiKhoan", "Không được bỏ trống");
				return View(nguoiDung);
			}
			if (nguoiDung.MatKhau == "")
			{
				ModelState.AddModelError("MatKhau", "Không được bỏ trống");
				return View(nguoiDung);
			}
			if (nguoiDung.TenNguoiDung == "")
			{
				ModelState.AddModelError("TenNguoiDung", "Không được bỏ trống");
				return View(nguoiDung);
			}
			if (nguoiDung.SoDienThoai == "")
			{
				ModelState.AddModelError("SoDienThoai", "không được bỏ trống");
				return View(nguoiDung);
			}*/
			if (nguoiDung.NgaySinh.HasValue && nguoiDung.NgaySinh.Value.Year > DateTime.Now.Year - 18)
			{
				ModelState.AddModelError("NgaySinh", "Ngày sinh không hợp lệ");
				return View(nguoiDung);

			}
			else
			{
				if (ModelState.IsValid)
				{

					_db.NGUOI_DUNGs.Add(nguoiDung);
					_db.SaveChanges();
					TempData["ThongBao"] = "Thêm người dùng thành công";
					return RedirectToAction("Index", "NguoiDung");

				}
				else
				{
					return View(nguoiDung);
				}
			}
			
            
        }
		public IActionResult SuaNguoiDung(int? ID)
		{
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            NGUOI_DUNG nguoiDung = _db.NGUOI_DUNGs.First(x => x.MaNguoiDung == ID);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return View(nguoiDung);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNguoiDung(NGUOI_DUNG nguoiDung)
        {
			if (nguoiDung.NgaySinh.HasValue && nguoiDung.NgaySinh.Value.Year > DateTime.Now.Year - 18)
			{
				ModelState.AddModelError("NgaySinh", "Ngày sinh không hợp lệ");
				return View(nguoiDung);

			}
			if (ModelState.IsValid)
            {
				_db.NGUOI_DUNGs.Update(nguoiDung);
				_db.SaveChanges();
				TempData["ThongBao"] = "Sửa người dùng thành công";
				return RedirectToAction("Index");
            }
			return View(nguoiDung);


		}
        public ActionResult XoaNguoiDung(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            NGUOI_DUNG nguoiDung = _db.NGUOI_DUNGs.First(x => x.MaNguoiDung == ID);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            else
            {
                nguoiDung.TrangThai = false;
                _db.NGUOI_DUNGs.Update(nguoiDung);
				TempData["ThongBaoXoa"] = "Xóa người dùng thành công";
				_db.SaveChanges();
               
            }
            return RedirectToAction("Index");
        }
        
    }

}
