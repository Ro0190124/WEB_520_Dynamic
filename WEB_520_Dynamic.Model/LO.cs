﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_520_Dynamic.Model
{
    public class LO
    {
        [Key]
        public int MaLo { get; set; }
        [MaxLength(50)]
        [MinLength(5, ErrorMessage ="Tên lô không dưới 5 kí tự")]
        public string TenLo { get; set;}
        public int MaSanPham { get; set; }
        [ForeignKey("MaSanPham")]
        public SAN_PHAM SAN_PHAM { get; set; }
        public int SoLuong { get; set; }
        public DateTime HanSuDung { get; set; }
    }
}
