using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lathubaiddnha.Models
{
    public class RegisterViewModel
    {
        public string TenKH { get; set; }  // Tên khách hàng
        public string TenDangNhap { get; set; }  // Tên đăng nhập
        public string MatKhau { get; set; }  // Mật khẩu
        public string SoDienThoai { get; set; }  // Số điện thoại
        public string DiaChi { get; set; }  // Địa chỉ
        public int ThangSinh { get; set; }  // Tháng sinh
        public string GioiTinh { get; set; }  // Giới tính
        public string Email { get; set; }  // Email
    }
}