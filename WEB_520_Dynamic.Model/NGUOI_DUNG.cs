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
        [MaxLength(50)]
        public string? TenNguoiDung { get; set; }
        public bool? GioiTinh { get; set; }
        [MaxLength(10)]
        public string SoDienThoai { get; set; }
        // get only date of NgaySinh
        public DateTime? NgaySinh { get; set; }
        [MaxLength(25)]
        public string TenTaiKhoan { get; set; }
        [MaxLength(20)]
        public string MatKhau { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao { get; private set; } = DateTime.Now;
    }
}
