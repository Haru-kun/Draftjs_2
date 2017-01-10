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
    }
}