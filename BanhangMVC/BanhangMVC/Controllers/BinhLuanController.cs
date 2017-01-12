using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanhangMVC.Models;

namespace BanhangMVC.Controllers
{
    public class BinhLuanController : Controller
    {
        QuanlyBanhangEntities db = new QuanlyBanhangEntities();

        public ActionResult BinhLuan(FormCollection f,BinhLuan bl)
        {
            //String Binhluan = f["txt_binhluan"].ToString();
            ThanhVien tv = Session["taikhoan"] as ThanhVien;
            if (tv == null)
            {
                return PartialView("ThongBao");
            }
            bl.MaThanhVien = tv.MaThanhVien;
            db.BinhLuans.Add(bl);
            db.SaveChanges();

            var listBL = db.BinhLuans.Where(x => x.MaSP ==bl.MaSP);
            ViewBag.LISTBL = listBL;

            return PartialView();
        }

        public ActionResult ThongBao()
        {
            return PartialView();
        }
    }
}