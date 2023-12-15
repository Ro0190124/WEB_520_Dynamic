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

        [HttpGet("GetDataForYear/{year}")]
        public IActionResult GetDataForYear(int year)
        {


            return View();
        }
        public IActionResult Index(int year)
        {

            return View();

        }

    }


}

