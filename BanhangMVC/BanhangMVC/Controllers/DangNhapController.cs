using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanhangMVC.Models;
using System.Web.Security;

namespace BanhangMVC.Controllers
{
    public class DangNhapController : Controller
    {
        QuanlyBanhangEntities db = new QuanlyBanhangEntities();

        public ActionResult DangnhapPartial()
        {
            return PartialView();
        }

        public ActionResult XulyDangnhap(FormCollection f,String url)
        {
            String user = f["user"].ToString();
            String pass = f["pass"].ToString();

            ThanhVien tv = db.ThanhViens.SingleOrDefault(x => x.TaiKhoan == user && x.MatKhau == pass);
            if (tv != null)
            {
                var listQuyen = db.LoaiThanhVien_Quyen.Where(x=>x.MaLoaiTV==tv.MaLoaiTV);
                Session["taikhoan"] = tv;
                String Quyen = "";
                foreach(var item in listQuyen)
                {
                    Quyen += item.Quyen.MaQuyen + ",";
                }
                Quyen = Quyen.Substring(0, Quyen.Length - 1);
                PhanQuyen(tv.TaiKhoan, Quyen);
                

                return JavaScript("window.location.reload();");
            }
            return Content("Tên dang nh?p ho?c m?t kh?u không dúng !!!");
        }

        public ActionResult LoiCapQuyen()
        {
            return View();
        }
		public ActionResult XulyDangxuat()
        {
            Session["taikhoan"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
		public ActionResult DangNhapAdmin()
        {
            return View();
        }

        public ActionResult DangNhapAdminPartial()
        {
            return PartialView();
        }
		public ActionResult XuLyDangNhapAdmin(FormCollection f)
        {
            String user = f["username"].ToString();
            String pass = f["password"].ToString();

            ThanhVien tv = db.ThanhViens.SingleOrDefault(x => x.TaiKhoan == user && x.MatKhau == pass);
            if (tv != null)
            {
                var listQuyen = db.LoaiThanhVien_Quyen.Where(x => x.MaLoaiTV == tv.MaLoaiTV);
                Session["thanhvien"] = tv;
                String Quyen = "";
                foreach (var item in listQuyen)
                {
                    Quyen += item.Quyen.MaQuyen + ",";
                }
                Quyen = Quyen.Substring(0, Quyen.Length - 1);
                PhanQuyen(tv.TaiKhoan, Quyen);
                return RedirectToAction("QuanLySanPham", "QuanLySanPham");
            }
            return View("Tên dang nh?p ho?c m?t kh?u không dúng !!!");
        }

        public ActionResult XuLyDangXuatAdmin()
        {
            Session["thanhvien"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("DangNhapAdmin");
        }
		 public void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                                                        TaiKhoan, //user
                                                        DateTime.Now, //begin
                                                        DateTime.Now.AddHours(3), //timeout
                                                        false, //remember?
                                                        Quyen, // permission.. "admin" or for more than one admin,marketing,sales"
                                                        FormsAuthentication.FormsCookiePath);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            //encrypt d? mã hóa b?o m?t thông tin
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;// ki?m tra cookie có t?n t?i hay chua
            Response.Cookies.Add(cookie);
        }
    }
}