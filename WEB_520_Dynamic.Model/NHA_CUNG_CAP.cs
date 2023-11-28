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
        public string TenNhaCungCap { get; set; }
        [MaxLength(100)]
        public string DiaChi { get; set; }
        [MaxLength(10)]
        public string SoDienThoai { get; set; }
        public string MaSoThue { get; set; }
        public string SoTaiKhoan { get; set; }
        public string TenNganHang { get; set; }
        public string NguoiDaiDien { get; set; }
        public byte TrangThai { get; set; }
    }
}
