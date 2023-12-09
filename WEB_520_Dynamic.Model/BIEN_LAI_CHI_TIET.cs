using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
    
        public int MaBienLai { get; set; }

        [ForeignKey("MaBienLai")]
        [ValidateNever]
        public BIEN_LAI BIEN_LAI { get; set; } = new BIEN_LAI();
        public int MaLo { get; set; }
       
        [ForeignKey("MaLo")]
        [ValidateNever]
        public LO LO { get; set; } = new LO();

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        public IEnumerable<LO>? LOs { get; set; }
    }
}
