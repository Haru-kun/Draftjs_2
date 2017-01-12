using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanhangMVC.Models;

namespace BanhangMVC.Controllers
{
    public class ThongKeController : Controller
    {

        QuanlyBanhangEntities db = new QuanlyBanhangEntities();

        public ActionResult ThongKeWeb()
        {
            decimal tkdoanhthu = (decimal)db.ChiTietDonDatHangs.Sum(x => x.SoLuong * x.DonGia).Value;

            ViewBag.SoNguoiTruyCap = (int)HttpContext.Application["SoNguoiTruyCap"];
            ViewBag.SoNguoiDangTruyCap = (int)HttpContext.Application["SoNguoiDangTruyCap"];
            ViewBag.TKDT = tkdoanhthu;
            return PartialView();
        }

        // Giải phóng bộ nhớ khi không sử dụng
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                }
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}