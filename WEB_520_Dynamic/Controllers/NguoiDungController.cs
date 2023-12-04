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
		public IActionResult Index()
		{
            IEnumerable<NGUOI_DUNG> nguoiDung = _db.NGUOI_DUNGs.Where(x=>x.TrangThai == true).ToList();
		
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
			/*// Xử lý dữ liệu biểu mẫu và lưu vào cơ sở dữ liệu
			// ...
			Console.WriteLine(nguoiDung.SingleUser.TenNguoiDung);
			Console.WriteLine("11111111111111");
			*//*Console.WriteLine(ModelState.Count);
			foreach (var key in ModelState.Keys)
			{
				Console.WriteLine(key);
				Console.WriteLine(ModelState[key].RawValue);
			}*//*
			if (ModelState.IsValid)
            {
				Console.WriteLine("222222222222222222");
				_db.NGUOI_DUNGs.Add(nguoiDung.SingleUser);
                _db.SaveChanges();
                
				return RedirectToAction("Index", "NguoiDung");
			}
            else
            {
				Console.WriteLine(nguoiDung.SingleUser.TenTaiKhoan);
                return View(nguoiDung);
            }*/
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
			}
			if (nguoiDung.NgaySinh.Value.Year > DateTime.Now.Year - 18)
			{
				ModelState.AddModelError("NgaySinh", "Ngày sinh không hợp lệ");
				return View(nguoiDung);
			}*/
			
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
                _db.SaveChanges();
               
            }
            return RedirectToAction("Index");
        }
        
    }

}
