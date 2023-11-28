using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_520_Dynamic.Model
{
    public class BIEN_LAI_CHI_TIET
    {
        [Key]
        public int MaBienLai { get; set; }
        [ForeignKey("MaBienLai")]
        public BIEN_LAI BIEN_LAI { get; set; }
        public int MaLo { get; set; }
        [ForeignKey("MaLo")]
        public LO LO { get; set; }
        public int SoLuong { get; set; }
    }
}
