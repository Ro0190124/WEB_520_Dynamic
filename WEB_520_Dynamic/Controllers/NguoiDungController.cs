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
			NGUOI_DUNG singleUser = new NGUOI_DUNG();// lấy thông tin của một người dùng cụ thể
			var userList = _db.NGUOI_DUNGs.ToList();// lấy danh sách người dùng

			var viewModel = new UserViewModel
			{
				SingleUser = singleUser,
				UserList = userList
			};
			
			return View(viewModel);
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
            if (ModelState.IsValid)
            {

                _db.NGUOI_DUNGs.Add(nguoiDung);
                _db.SaveChanges();
                TempData["ThongBao"] = "Đăng kí thành công";
                return View();

            }
            else
            {
                return View(nguoiDung);
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
        public IActionResult Edit(NGUOI_DUNG nguoiDung)
        {

            if (ModelState.IsValid)
            {
                _db.NGUOI_DUNGs.Update(nguoiDung);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nguoiDung);


        }
    }

}
