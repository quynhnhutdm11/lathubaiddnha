﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lathubaiddnha.Models
{
    namespace lathubaiddnha.Models
    {
        public class UserPointViewModel
        {
            public int MaKH { get; set; }
            public string TenKH { get; set; }
            public string Email { get; set; }
            public int? ThangSinh { get; set; } // Có thể nullable nếu cần
            public int Diem { get; set; }
            public string SoDienThoai { get; set; } // Thêm thuộc tính SoDienThoai
        }
    }
}
