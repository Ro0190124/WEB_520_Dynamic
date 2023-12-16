using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WEB_520_Dynamic.DataAccess.Data;
using WEB_520_Dynamic.Model;

namespace WEB_520_Dynamic.Controllers
{
	public class LoController : Controller
	{
		public class PrivateResponse
		{
			public string type { get; set; }
			public string res { get; set; }

			//public PrivateResponse() { }
			public PrivateResponse(string res) { this.res = res; }
			public PrivateResponse(string type, string res) { this.type = type; this.res = res; }
		}
		private readonly ApplicationDbContext _db;
		public LoController(ApplicationDbContext db)
		{
			_db = db;
		}
		[HttpGet]
		public IActionResult Index(string searchString)
		{
			var cookie = Request.Cookies["ID"];
			// check cookie
			Console.WriteLine(cookie);
			if (cookie == null)
			{
				return RedirectToAction("DangNhap", "Home");
			}
			
			IEnumerable<BIEN_LAI_CHI_TIET> loBienLaiCT = _db.BIEN_LAI_CHI_TIETs.Where(x => x.BIEN_LAI.TrangThai == 2).Include(x => x.LO).ThenInclude(x => x.SAN_PHAM).OrderByDescending(x=> x.MaLo).ToList();
			if (!string.IsNullOrEmpty(searchString))
			{
				loBienLaiCT = (List<BIEN_LAI_CHI_TIET>)loBienLaiCT.Where(x => x.LO.SAN_PHAM.TenSanPham.Contains(searchString) || x.LO.TenLo.Contains(searchString));
			}
		
			return View(loBienLaiCT);
			//IEnumerable<LO> groupSpL = _db.LOs.Include(x => x.SAN_PHAM);
			//return View(groupSpL);
		}
		/*public IActionResult ThemLo()
		{
			IEnumerable<SelectListItem> SP = _db.SAN_PHAMs.Where(x => x.TrangThai == true).Select(

			   s => new SelectListItem()
			   {
				   Text = s.TenSanPham,
				   Value = s.MaSanPham.ToString()
			   });
			ViewBag.SanPham = SP;
			return View();
		}*/
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ThemLo(LO lo, int id)
		{
			
			Console.WriteLine(lo.MaLo);
			Console.WriteLine(lo.MaLo.ToString());
			Console.WriteLine(lo.SoLuong);
			Console.WriteLine(lo.SAN_PHAM.MaSanPham);
			Console.WriteLine("Mã biên lai : " + id);
			lo.MaSanPham = lo.SAN_PHAM.MaSanPham;
			lo.SAN_PHAM = _db.SAN_PHAMs.Where(x => x.MaSanPham == lo.SAN_PHAM.MaSanPham).First();
            try
            {
                foreach (var e in ModelState["lo.Soluong"].Errors)
                {
                    if (e.ErrorMessage.Contains("is invalid") || e.ErrorMessage.Contains("is not valid")) { ModelState["lo.SoLuong"].Errors.Remove(e); ModelState["lo.SoLuong"].Errors.Add("Số Lượng Không hợp lệ"); }
                }
                foreach (var e in ModelState["lo.TenLo"].Errors)
                {
                    if (e.ErrorMessage.Contains("is invalid") || e.ErrorMessage.Contains("is not valid")) { ModelState["lo.TenLo"].Errors.Remove(e); ModelState["lo.TenLo"].Errors.Add("Tên Lô Không hợp lệ"); }
                }
            }
            catch (Exception e)
            {
                // ke me t
            }
            if (ModelState.IsValid)
			{
				_db.LOs.Add(lo);
				_db.SaveChanges();
				
				//thêm biên lai chi tiết với mã biên lai = 
				BIEN_LAI_CHI_TIET bienLaiCT = new BIEN_LAI_CHI_TIET();
				//bienLaiCT.BIEN_LAI = _db.BIEN_LAIs.FirstOrDefault(x => x.MaBienLai == id);
				bienLaiCT.MaBienLai = id;
				bienLaiCT.MaLo = lo.MaLo;
				bienLaiCT.SoLuong = lo.SoLuong;
				Console.WriteLine(bienLaiCT.MaBienLai + " " + bienLaiCT.MaLo);
				_db.BIEN_LAI_CHI_TIETs.Add(bienLaiCT);
				_db.SaveChanges();
				TempData["ThongBao"] = "Thêm lô thành công";
				//return RedirectToAction("Index", "BienLaiChiTiet", new { id = bienLaiCT.MaBienLai });
				var j = Json(new PrivateResponse("link", "/BienLaiChiTiet/Index/" + bienLaiCT.MaBienLai));
				
				return j;
				//return RedirectToAction("Index", "BienLaiChiTiet");
			}
			string s = "";
			foreach (var i in ModelState.Values)
			{
				if (i.Errors.Count() > 0)
				{
					foreach(var j in i.Errors)
					{
                        s = s + j.ErrorMessage + "\n";
                    }
				}
			}
			// return "Thêm lô thất bại" as utf8 encoded string
			return Json(new PrivateResponse("error", s));


        }
        public IActionResult XoaLo(int? ID)
		{
			if (ID == null || ID == 0)
			{
				return NotFound();
			}
			// biên lai chi tiết có mã lô và mã loại biên lai == false 
			BIEN_LAI_CHI_TIET bienLaiCT = _db.BIEN_LAI_CHI_TIETs.FirstOrDefault(x => x.MaLo == ID && x.BIEN_LAI.LoaiBienLai == false );
			BIEN_LAI bienlai = _db.BIEN_LAIs.FirstOrDefault(x => x.MaBienLai == bienLaiCT.MaBienLai);
			if (bienlai.TrangThai == 0)
			{
				LO lo = _db.LOs.FirstOrDefault(x => x.MaLo == ID);
				if (lo == null)
				{
					return NotFound();
				}
				else
				{
					_db.LOs.Remove(lo);
					_db.SaveChanges();
					TempData["ThongBaoXoa"] = "Xóa lô thành công";
				}

			}
			else
			{
				TempData["ThongBaoXoaLok"] = "Xóa lô không thành công";
			}

			return RedirectToAction("Index", "BienLaiChiTiet", new { id = bienLaiCT.MaBienLai});
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ThemLoXuat(BIEN_LAI_CHI_TIET bienLai, int id)
		{

			Console.WriteLine("Mã biên lai : " + id);
			Console.WriteLine("Mã lô: " + bienLai.MaLo);
			Console.WriteLine("Số lượng: " + bienLai.SoLuong);
			try
			{
				foreach (var e in ModelState["Soluong"].Errors)
				{
					if (e.ErrorMessage.Contains("is invalid") || e.ErrorMessage.Contains("is not valid")) { ModelState["SoLuong"].Errors.Remove(e); ModelState["SoLuong"].Errors.Add("Số Lượng Không hợp lệ"); }
				}
			
			}
			catch (Exception e)
			{
				// ke me t
			}
			if (ModelState.IsValid)
			{
				LO lo = _db.LOs.Where(x => x.MaLo == bienLai.MaLo && x.SoLuong >= bienLai.SoLuong).FirstOrDefault();
				if (lo != null)
				{
				
					bienLai.MaBienLai = id;
					Console.WriteLine(bienLai.MaBienLai + " " + bienLai.MaLo + " " + bienLai.SoLuong);
					_db.BIEN_LAI_CHI_TIETs.Add(bienLai);
					_db.SaveChanges();


					lo.SoLuong -= bienLai.SoLuong;
					_db.LOs.Update(lo);
					_db.SaveChanges();
					TempData["ThongBao"] = "Thêm lô thành công";
					//return RedirectToAction("BienLaiCTXuat", "BienLaiChiTiet", new { id = bienLai.MaBienLai });
					var j = Json(new PrivateResponse("link", "/BienLaiChiTiet/BienLaiCTXuat/" + bienLai.MaBienLai));

					return j;

				}
				else
				{
					ModelState["SoLuong"].Errors.Add("Số lượng đéo đủ cảm ơn");
				}

				
			}
			string s = "";
			foreach (var i in ModelState.Values)
			{
				if (i.Errors.Count() > 0)
				{
					foreach (var j in i.Errors)
					{
						s = s + j.ErrorMessage + "\n";
					}
				}
			}
			// return "Thêm lô thất bại" as utf8 encoded string
			return Json(new PrivateResponse("error", s));
			return View(bienLai);
		}
		public IActionResult XoaLoXuat(int? ID)
		{
			if (ID == null || ID == 0)
			{
				return NotFound();
			}
			Console.WriteLine("mã biên lai chi tiết: " + ID);
			
			BIEN_LAI_CHI_TIET bienLaiCT = _db.BIEN_LAI_CHI_TIETs.FirstOrDefault(x => x.MaBienLaiChiTiet == ID);
			BIEN_LAI bienlai = _db.BIEN_LAIs.FirstOrDefault(x => x.MaBienLai == bienLaiCT.MaBienLai );
			Console.WriteLine("Mã biên lai : " + bienLaiCT.MaBienLai);
			Console.WriteLine("trạng thái biên lai: " + bienlai.TrangThai);
			if (bienlai.TrangThai == 0)
			{
				LO lo = _db.LOs.FirstOrDefault(x => x.MaLo == bienLaiCT.MaLo);
				if (lo == null)
				{
					return NotFound();
				}
				else
				{
					LO lobandau = _db.LOs.Where(x => x.MaLo == bienLaiCT.MaLo).FirstOrDefault();
					Console.WriteLine("Mã lô " + lobandau.MaLo);
					lobandau.SoLuong += bienLaiCT.SoLuong;
					_db.LOs.Update(lobandau);
					_db.SaveChanges();
					_db.BIEN_LAI_CHI_TIETs.Remove(bienLaiCT);
					_db.SaveChanges();

					TempData["ThongBaoXoak"] = "Xóa Sàn Phẩm thành công";

				}
			}
			else
			{
				TempData["ThongBaoXoak"] = "Xóa Sàn Phẩm không thành công";
			}
			Console.WriteLine("Mã biên lai : " + bienLaiCT.MaBienLai);
			Console.WriteLine("Mã lô: " + bienLaiCT.MaLo);
			//Console.WriteLine("Loại Biên lai: " + bienLaiCT.BIEN_LAI.LoaiBienLai);
			

			return RedirectToAction("BienLaiCTXuat", "BienLaiChiTiet", new { id = bienlai.MaBienLai });
		}
		


	}
}
