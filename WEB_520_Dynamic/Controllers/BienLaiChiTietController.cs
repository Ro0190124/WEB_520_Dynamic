﻿using Microsoft.AspNetCore.Mvc;
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
			BIEN_LAI? bienLai = _db.BIEN_LAIs.Include(b => b.NHA_CUNG_CAP).FirstOrDefault(b => b.MaBienLai == id);
			if (bienLai == null)
			{
				bienLai = new BIEN_LAI();
			}
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
                if (lo == null)
				{
					listSanPham.Add(new LO { MaLo = int.Parse(lo)});
				}
			}
            _db.SaveChanges();
            
            var modelview = new BIEN_LAI_CHI_TIET
            {
                BIEN_LAI = bienLai,
                LOs = listSanPham
            };
			//return RedirectToAction("Index", "BienLaiChiTiet", new { id = bienLai.MaBienLai })

			return View(modelview);
        }
        
        public IActionResult ThemBienLaiChiTiet()
        {
            return View();
        }
  
		public IActionResult LoIndex(int? ID)
		{
            Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
			Console.WriteLine(ID);
			IEnumerable<LO> groupSpL = _db.LOs.Include(x => x.SAN_PHAM).ToList();
			Console.WriteLine(groupSpL.ElementAt(0).SAN_PHAM.TenSanPham);
			return View(groupSpL);
		}

		public IActionResult BienLaiCTXuat(int id)
		{

			
			BIEN_LAI? bienLai = _db.BIEN_LAIs.Include(b => b.NHA_CUNG_CAP).FirstOrDefault(b => b.MaBienLai == id);
			if (bienLai == null)
			{
				bienLai = new BIEN_LAI();
			}
		
			var loBienLai = _db.BIEN_LAI_CHI_TIETs.Where(x => x.MaBienLai == id).Select(x => x.MaLo).ToList();

			// trả về list lô 
			List<LO> listSanPham = new List<LO>();
			foreach (var item in loBienLai)
			{
				var lo = _db.LOs.Where(l => l.MaLo == item).Select(x => x.MaLo).ToString();
				
			}_db.SaveChanges();

			var modelview = new BIEN_LAI_CHI_TIET
			{
				BIEN_LAI = bienLai,
				LOs = listSanPham
			};
			/*IEnumerable<BIEN_LAI_CHI_TIET> loBienLaiCT = loBienLaiCT = _db.BIEN_LAI_CHI_TIETs
											.Where(x => x.BIEN_LAI.TrangThai == 2)
											.Include(x => x.LO)
											.ThenInclude(x => x.SAN_PHAM)
											.OrderByDescending(x => x.MaLo)
											.GroupBy(x => x.MaLo)
											.Select(group => group.First())
											.ToList();
*/
			var LovaSP = _db.BIEN_LAI_CHI_TIETs
				.Where(n => n.BIEN_LAI.TrangThai == 2 && n.LO.HanSuDung > DateTime.Now && n.LO.SoLuong > 0)
				
				.Select(x => new
				{
					id = x.MaLo,
					TenL = x.LO.TenLo,
					TenSP = x.LO.SAN_PHAM.TenSanPham,
					HSD = x.LO.HanSuDung.ToString("dd/MM/yyyy")
				})
				.GroupBy(x => x.id).Select(group => group.First()).ToList();
			var modifiedList = LovaSP.Select(item => new
			{
				id = item.id,
				TenL = $"{item.TenL} - {item.TenSP} (Hạn sử dụng: {item.HSD})"
			}).ToList();

			ViewBag.LovaSP = new SelectList(modifiedList, "id", "TenL");
			return View(modelview);



		}
		public IActionResult XacNhanDanhSach(int ID)
		{
			BIEN_LAI bienLai = _db.BIEN_LAIs.FirstOrDefault(b => b.MaBienLai == ID);
			bienLai.TrangThai = 1;
			_db.BIEN_LAIs.Update(bienLai);
			_db.SaveChanges();
			return RedirectToAction("Index", "BienLai");
		}
		public IActionResult HoanThanh(int ID)
		{
			BIEN_LAI bienLai = _db.BIEN_LAIs.FirstOrDefault(b => b.MaBienLai == ID);
			bienLai.TrangThai = 2;
			_db.BIEN_LAIs.Update(bienLai);
			_db.SaveChanges();
			return RedirectToAction("Index", "BienLai");
		}
	}
}
