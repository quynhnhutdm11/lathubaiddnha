using lathubaiddnha.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity; // Đảm bảo bạn có dòng này

namespace lathubaiddnha.Controllers
{
    public class QuanLyDanhGiaController : Controller
    {
        private readonly luckEntities db = new luckEntities();

        // GET: DanhGia
        public ActionResult Index()
        {
            // Sử dụng Include để lấy thông tin sản phẩm liên quan
            var danhGias = db.DanhGias.Include("SanPham").ToList();
            return View(danhGias);
        }



        // POST: Cập nhật đánh giá
        [HttpPost]
        public JsonResult UpdateDanhGia(string madanggia, string noidung, int sosao)
        {
            var danhGia = db.DanhGias.FirstOrDefault(d => d.madanggia.Equals(madanggia));
            if (danhGia != null)
            {
                danhGia.noidung = noidung;
                danhGia.sosao = sosao;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        // POST: Xóa đánh giá
        [HttpPost]
        public JsonResult DeleteDanhGia(string madanggia)
        {
            var danhGia = db.DanhGias.FirstOrDefault(d => d.madanggia.Equals(madanggia));
            if (danhGia != null)
            {
                db.DanhGias.Remove(danhGia);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        // POST: Phản hồi đánh giá
        [HttpPost]
        public JsonResult ReplyToDanhGia(string madanggia, string phanHoi)
        {
            var danhGia = db.DanhGias.FirstOrDefault(d => d.madanggia.Equals(madanggia));
            if (danhGia != null)
            {
                danhGia.PhanHoi = phanHoi;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public JsonResult SubmitRating()
        {
            // Lấy thông tin từ Request.Form
            var maKHStr = Request.Form["maKH"];
            var noidung = Request.Form["noidung"];
            var sosaoStr = Request.Form["sosao"];

            if (!int.TryParse(maKHStr, out int maKH))
            {
                return Json(new { success = false, message = "Mã khách hàng không hợp lệ." });
            }

            if (!int.TryParse(sosaoStr, out int sosao))
            {
                return Json(new { success = false, message = "Số sao không hợp lệ." });
            }

            if (string.IsNullOrWhiteSpace(noidung))
            {
                return Json(new { success = false, message = "Nội dung đánh giá không được để trống." });
            }

            var newRating = new DanhGia
            {
                madanggia = "DG_" + Guid.NewGuid(),
                maKH = maKH,
                noidung = noidung,
                sosao = sosao,
                ngayDanhGia = DateTime.Now
            };

            try
            {
                db.DanhGias.Add(newRating);
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }


    }
}
