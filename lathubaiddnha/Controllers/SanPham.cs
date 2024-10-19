using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lathubaiddnha.Controllers
{
    public class SanPham
    {
        public string MaSanPham { get; set; }  // Mã sản phẩm
        public string TenSanPham { get; set; } // Tên sản phẩm
        public string MoTa { get; set; }       // Mô tả sản phẩm
        public decimal Gia { get; set; }       // Giá sản phẩm
        public string Hinh { get; set; }       // Đường dẫn hoặc tên tệp hình ảnh sản phẩm
    }
}