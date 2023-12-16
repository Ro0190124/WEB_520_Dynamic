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
		[MinLength(7, ErrorMessage = "Tên nhà cung cấp không dưới 7 kí tự")]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Tên nhà cung cấp chỉ chứa chữ ")]
		public string TenNhaCungCap { get; set; }


        [MaxLength(150)]
        [Required(ErrorMessage = "Không được trống")]
		[MinLength(5, ErrorMessage = "Địa chỉ không dưới dưới 5 kí tự")]
        public string DiaChi { get; set; }


        [StringLength(10)]
        [MinLength(10, ErrorMessage = "Số Điện thoại phải có 10 kí tự")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Số điện thoại chỉ chứa số")]
        [Required(ErrorMessage = "Không được trống")]
        public string SoDienThoai { get; set; }


        [StringLength(10)]
        [Required(ErrorMessage = "Không được trống")]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Mã số thuế chỉ chứa số")]
		[MinLength(10, ErrorMessage = "Mã số thuế phải có 10 kí tự")]

		public string MaSoThue { get; set; }


        [MaxLength(30)]
        [MinLength(8, ErrorMessage = "số tài khoản phải có ít nhất 8 kí tự")]
        [Required(ErrorMessage = "Không được trống")]
		[RegularExpression(@"^[0-9a-zA-Z]+$", ErrorMessage = "Số tài khoản không chứa kí tự đặc biệt")]
		public string SoTaiKhoan { get; set; }


        [MaxLength(50)]
        [Required(ErrorMessage = "Không được trống")]
		[MinLength(5, ErrorMessage = "Tên người đại diện không dưới 5 kí tự")]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Tên người đại diện chỉ chứa chữ ")]
		public string NguoiDaiDien { get; set; }
        public bool TrangThai { get; set; } = true;
    }
}
