using lathubaiddnha.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;

namespace lathubaiddnha.Controllers
{
    public class UserController : Controller
    {
        luckEntities db = new luckEntities();

        // GET: User
        public ActionResult Register(NguoiDung model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên đăng nhập đã tồn tại chưa
                var existingUser = db.NguoiDungs.SingleOrDefault(u => u.TenDangNhap == model.TenDangNhap);
                if (existingUser != null)
                {
                    ModelState.AddModelError("TenDangNhap", "Tài khoản đã tồn tại.");
                    return View(model); // Trả về view với lỗi
                }

                // Tạo đối tượng NguoiDung mới
                var kh = new NguoiDung
                {
                    TenKH = model.TenKH,
                    TenDangNhap = model.TenDangNhap,
                    MatKhau = model.MatKhau, // Nên hash mật khẩu trước khi lưu
                    DiaChi = model.DiaChi,
                    SoDienThoai = model.SoDienThoai,
                    ThangSinh = model.ThangSinh,
                    GioiTinh = model.GioiTinh,
                    Email = model.Email
                };

                try
                {
                    db.NguoiDungs.Add(kh);
                    db.SaveChanges();
                    // Chuyển hướng đến trang chính hoặc trang đăng nhập
                    return RedirectToAction("Index", "Home"); // Hoặc chuyển đến trang đăng nhập
                }
                catch (DbUpdateException ex)
                {
                    var innerException = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    ModelState.AddModelError("", $"Có lỗi khi cập nhật: {innerException}");
                    return View(model);
                }
                catch (DbEntityValidationException ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi lưu dữ liệu.");
                    foreach (var validationError in ex.EntityValidationErrors)
                    {
                        foreach (var validation in validationError.ValidationErrors)
                        {
                            ModelState.AddModelError(validation.PropertyName, validation.ErrorMessage);
                        }
                    }
                    return View(model); // Trả về view với lỗi
                }
                catch (Exception e)
                {
                    System.IO.File.WriteAllText("path_to_log_file.txt", e.ToString()); // Ghi log lỗi  
                    ModelState.AddModelError("", "Đã xảy ra lỗi, vui lòng thử lại sau.");
                    return View(model);
                }
            }

            return View(model); // Trả về view nếu ModelState không hợp lệ
        }


        /*     private bool SendRegistrationSuccessEmail(string toEmail, string fullName, string password)
             {
                 var fromEmail = "2224802010596@student.tdmu.edu.vn"; // Địa chỉ email của bạn
                 var fromPassword = "lqci gzzj tvaq ocss"; // Mật khẩu email của bạn
                 var subject = "Chào mừng bạn đã đăng ký tài khoản thành công!";
                 var body = $"Chào {fullName},<br/><br/>" +
                            "Chúc mừng bạn đã đăng ký tài khoản thành công tại hệ thống của chúng tôi.<br/>" +
                            "Vui lòng truy cập website và đăng nhập bằng tài khoản của bạn.<br/><br/>" +
                            "Cảm ơn bạn đã tin tưởng sử dụng dịch vụ của chúng tôi!<br/><br/>" +
                            "Trân trọng,<br/>Đội ngũ hỗ trợ";

                 var smtpClient = new SmtpClient("smtp.gmail.com")
                 {
                     Port = 587,
                     Credentials = new NetworkCredential(fromEmail, fromPassword),
                     EnableSsl = true
                 };

                 var mailMessage = new MailMessage
                 {
                     From = new MailAddress(fromEmail),
                     Subject = subject,
                     Body = body,
                     IsBodyHtml = true,
                 };

                 mailMessage.To.Add(toEmail);

                 try
                 {
                     smtpClient.Send(mailMessage);
                     return true;
                 }
                 catch (Exception ex)
                 {
                     ViewBag.Message = $"Đã xảy ra lỗi khi gửi email: {ex.Message}";
                     return false;
                 }
             }

             */

        // GET: User/Login
        public ActionResult Login()
        {
            // Kiểm tra cookie và gán giá trị cho ViewBag
            if (Request.Cookies["UserInfo"] != null)
            {
                ViewBag.TenDangNhap = Request.Cookies["UserInfo"]["TenDangNhap"];
            }
            return View();
        }


        [HttpPost]
        public ActionResult Login(string taiKhoan, string matKhau, bool rememberMe = false)
        {
            // Use hashing for passwords in a real application
            var kh = db.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == taiKhoan && u.MatKhau == matKhau);
            if (kh != null)
            {
                Session["User"] = kh;
                if (rememberMe)
                {
                    var cookie = new HttpCookie("UserInfo")
                    {
                        Values = { { "TaiKhoan", kh.TenDangNhap }, { "HoTen", kh.TenKH } },
                        Expires = DateTime.Now.AddDays(30)
                    };
                    Response.Cookies.Add(cookie);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            if (Request.Cookies["UserInfo"] != null)
            {
                var cookie = new HttpCookie("UserInfo")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}