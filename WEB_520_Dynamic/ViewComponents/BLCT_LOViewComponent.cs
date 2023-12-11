//using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_520_Dynamic.DataAccess.Data;
using WEB_520_Dynamic.Model;

namespace WEB_520_Dynamic.ViewComponents
{
	public class BLCT_LOViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _db;

		public BLCT_LOViewComponent(ApplicationDbContext dbContext)
		{
			_db = dbContext;
		}
		[HttpGet]
		public async Task<IViewComponentResult> InvokeAsync(int? ID)
		{
			Console.WriteLine(ID +" MÃ nhàaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaasdasdkljfhalsjd");
			//IEnumerable<LO> groupSpL = _db.LOs.Include(x => x.SAN_PHAM).ToList();
			IEnumerable<LO> groupSpL = _db.LOs.Join(_db.BIEN_LAI_CHI_TIETs, lo => lo.MaLo, blct => blct.MaLo, (lo, blct) => new { lo, blct }).Where(x => x.blct.MaBienLai == ID).Select(x => x.lo).Include(x => x.SAN_PHAM).ToList();
			//LO lo_BLCT = _db.LOs.Join(_db.BIEN_LAI_CHI_TIETs, lo => lo.MaLo, blct => blct.MaLo, (lo, blct) => new { lo, blct }).Where(x => x.blct.MaBienLai == ID).Select(x => x.lo).ToList().FirstOrDefault();*/
			//List<LO> lo_BLCT = _db.LOs.Join(_db.BIEN_LAI_CHI_TIETs, lo => lo.MaLo, blct => blct.MaLo, (lo, blct) => new { lo, blct }).Where(x => x.blct.MaBienLai == ID).Select(x => x.lo).Include(x => x.SAN_PHAM).ToList();
			/*foreach ( var item in lo_BLCT)
			{
				{
					Console.WriteLine(item.MaLo + item.LO.TenLo);
					Console.WriteLine("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
				}
			}*/
			if (groupSpL == null)
			{
				return View();
			}
			//Console.WriteLine(groupSpL.ElementAt(0).SAN_PHAM.TenSanPham);
			
			return View(groupSpL);
		}
	}
}
