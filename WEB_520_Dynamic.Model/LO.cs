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
    public class LO
    {
        [Key]
        public int MaLo { get; set; }
        [MaxLength(50)]
        [MinLength(5, ErrorMessage ="Tên lô không dưới 5 kí tự")]
        [Required(ErrorMessage = "Tên lô Không được trống")]
        public string TenLo { get; set;}


        public int MaSanPham { get; set; }


        [ForeignKey("MaSanPham")]
		[ValidateNever]
		public SAN_PHAM SAN_PHAM { get; set; }


        [Required(ErrorMessage = "Số lượng Không được trống")]
        [Range(1, int.MaxValue, ErrorMessage = "số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Hạn sử dụng Không được trống")]
        public DateTime HanSuDung { get; set; }

        //[ForeignKey("BIEN_LAI_CHI_TIETMaBienLai")]
        //[ValidateNever]
        //public string cc { get; set; }
    }
}
