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
			IEnumerable<LO> groupSpL = _db.LOs.Include(x => x.SAN_PHAM).ToList();
			if(groupSpL == null)
			{
				return View();
			}
			Console.WriteLine(groupSpL.ElementAt(0).SAN_PHAM.TenSanPham);
			
			return View(groupSpL);
		}
	}
}
