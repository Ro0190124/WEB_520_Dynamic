using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_520_Dynamic.Model
{
    public class NHA_CUNG_CAP
    {
        [Key]

        public int MaNhaCungCap { get; set; }


        [MaxLength(50)]
        [Required(ErrorMessage = "Không được trống")]
        public string TenNhaCungCap { get; set; }


        [MaxLength(150)]
        [Required(ErrorMessage = "Không được trống")]
        public string DiaChi { get; set; }


        [StringLength(10)]
        [MinLength(10, ErrorMessage = "Số Điện thoại phải có 10 kí tự")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Số điện thoại chỉ chứa số")]
        [Required(ErrorMessage = "Không được trống")]
        public string SoDienThoai { get; set; }


        [StringLength(10)]
        [Required(ErrorMessage = "Không được trống")]
        public string MaSoThue { get; set; }


        [MaxLength(30)]
        [Required(ErrorMessage = "Không được trống")]
        public string SoTaiKhoan { get; set; }


        [MaxLength(50)]
        [Required(ErrorMessage = "Không được trống")]
        public string NguoiDaiDien { get; set; }


        public bool TrangThai { get; set; } = true;
    }
}
