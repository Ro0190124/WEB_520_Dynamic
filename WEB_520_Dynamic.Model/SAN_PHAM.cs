using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_520_Dynamic.Model
{
    public class SAN_PHAM
    {
        [Key]
        public int MaSanPham { get; set; }
        [MaxLength(50)]
        public string TenSanPham { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn 0")]
        public double DonGia { get; set; }
        [MaxLength(20)]
        public string DonVi { get; set; }
        [MaxLength(30)]
        public string QuyCach { get; set; }
    }
}
