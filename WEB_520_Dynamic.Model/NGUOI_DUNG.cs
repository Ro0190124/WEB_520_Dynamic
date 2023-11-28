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
        public string? TenNguoiDung { get; set; }
        public byte? GioiTinh { get; set; }
        public string SoDienThoai { get; set; }
        // get only date of NgaySinh
        public DateTime? NgaySinh { get; set; }
        public string TenTaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public byte TrangThai { get; set; }
        public DateTime NgayTao { get; private set; } = DateTime.Now;
    }
}
