using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_520_Dynamic.Model
{
    public class NGUOI_DUNG
    {
        [Key]
        public int MaNguoiDung { get; set; }


        [Required(ErrorMessage = "Không được trống")]
        [MinLength(5, ErrorMessage ="Tên người dùng không dưới 5 kí tự ")]
        [MaxLength(50)]
		[RegularExpression(@"^[\p{L}a-zA-Z\s]*$", ErrorMessage = "Chỉ cho phép nhập ký tự từ chữ.")]
		public string TenNguoiDung { get; set; }


        public bool? GioiTinh { get; set; } = true;


        [StringLength(10, ErrorMessage ="Số Điện thoại phải có 10 kí tự")]
        [MinLength(10, ErrorMessage = "Số Điện thoại phải có 10 kí tự")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Số điện thoại chỉ chứa số")]
        [Required(ErrorMessage = "Không được trống")]
        public string SoDienThoai { get; set; }


        // get only date of NgaySinh
        public DateTime? NgaySinh { get; set; } = new DateTime(1990, 01, 01);


        [Required(ErrorMessage = "Không được trống")]
        [MaxLength(25)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Tên tài khoản chỉ được chứa ký tự và số.")]
        [Display (Name = "Tên tài khoản")]
        [MinLength(5, ErrorMessage ="Tên tài khoản không dưới 5 kí tự")]
        public string TenTaiKhoan { get; set; }


        [Required(ErrorMessage = "Không được trống")]
        [MaxLength(20)]
        [MinLength(5, ErrorMessage ="Mật khẩu không dưới 5 kí tự")]
        public string MatKhau { get; set; }


        public bool TrangThai { get; set; } = true;
        public DateTime NgayTao { get; private set; } = DateTime.Now;
    }
}
