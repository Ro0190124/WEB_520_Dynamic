﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public DateTime NgayLap { get; private set; } = DateTime.Now;
        public bool LoaiBienLai { get; set; } = true;

        [Required(ErrorMessage = "Không được trống")]
        public DateTime NgayGiao { get; set; }


        public int MaNguoiDung { get; set; }
        [ValidateNever]
        [ForeignKey("MaNguoiDung")]
        public NGUOI_DUNG? NGUOI_DUNG { get; set; }


        public int? MaNhaCungCap { get; set; }
        [ValidateNever]
        [ForeignKey("MaNhaCungCap")]
        public NHA_CUNG_CAP? NHA_CUNG_CAP { get; set; }


        [MaxLength(150)]
        [MinLength(10, ErrorMessage ="Thông tin giao hàng không dưới 10 kí tự")]
        public string? ThongTinGiaoHang { get; set; }
        public byte TrangThai { get; set; } = 0;
        [MaxLength(100)]
        public string? GhiChu { get; set; }
    }
}
