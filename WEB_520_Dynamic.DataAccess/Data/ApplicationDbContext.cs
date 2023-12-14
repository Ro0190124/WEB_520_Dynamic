using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB_520_Dynamic.Model;

namespace WEB_520_Dynamic.DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<NHA_CUNG_CAP> NHA_CUNG_CAPs { get; set; }
        public DbSet<SAN_PHAM> SAN_PHAMs { get; set; }
        public DbSet<LO> LOs { get; set; }
        public DbSet<BIEN_LAI> BIEN_LAIs { get; set; }
        public DbSet<BIEN_LAI_CHI_TIET> BIEN_LAI_CHI_TIETs { get; set; }
        public DbSet<NGUOI_DUNG> NGUOI_DUNGs { get; set; }

	}
}
