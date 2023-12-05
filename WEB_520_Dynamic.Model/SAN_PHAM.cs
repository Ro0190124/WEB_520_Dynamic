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
        [MinLength(5, ErrorMessage ="Tên sản phẩm không dưới 5 kí tự")]
        public string TenSanPham { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn 0")]
        public double DonGia { get; set; }
        [MaxLength(20)]
		public string DonVi { get; set; } = "Thùng";
		[MaxLength(30)]
        public string QuyCach { get; set; }
    }
}
