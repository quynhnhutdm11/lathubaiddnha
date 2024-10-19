using lathubaiddnha.Models; // Kiểm tra import đúng namespace
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace lathubaiddnha.Controllers
{
    public class HomeController : Controller
    {
        luckEntities2 db = new luckEntities2();

        public ActionResult Index()
        {
            var danhSachSanPham = db.SanPhams.ToList();
            return View(danhSachSanPham); // Truyền danh sách sản phẩm sang view
        }
        public ActionResult Slider()
        {
            return View();
        }
    }
}
