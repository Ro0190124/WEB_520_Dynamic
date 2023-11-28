using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_520_Dynamic.Model
{
    public class BIEN_LAI
    {
        [Key]
        public int MaBienLai { get; set; }
        public DateTime NgayLap { get; set; } = DateTime.Now;
        public bool LoaiBienLai { get; set; }
        public DateTime NgayGiao { get; set; }
        public int? MaNguoiDung { get; set; }
        [ForeignKey("MaNguoiDung")]
        public NGUOI_DUNG NGUOI_DUNG { get; set; }
        public int? MaNhaCungCap { get; set; }
        [ForeignKey("MaNhaCungCap")]
        public NHA_CUNG_CAP NHA_CUNG_CAP { get; set; }
        [MaxLength(100)]
        public string ThongTinGiaoHang { get; set; }
        public byte TrangThai { get; set; }
        [MaxLength(100)]
        public string? GhiChu { get; set; }
    }
}
