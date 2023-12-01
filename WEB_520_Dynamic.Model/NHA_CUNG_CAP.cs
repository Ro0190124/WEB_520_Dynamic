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
        [MaxLength(150)]
        public string DiaChi { get; set; }
        [StringLength(10)]
        public string SoDienThoai { get; set; }
        [StringLength(10)]
        public string MaSoThue { get; set; }
        [MaxLength(30)]
        public string SoTaiKhoan { get; set; }
        [MaxLength(50)]
        public string NguoiDaiDien { get; set; }
        public bool TrangThai { get; set; } = true;
    }
}
