using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanhangMVC.Models;
using CaptchaMvc;
using CaptchaMvc.HtmlHelpers;

namespace BanhangMVC.Controllers
{
    public class DangkyController : Controller
    {
        QuanlyBanhangEntities db = new QuanlyBanhangEntities();

        [HttpGet]
        public ActionResult Dangky1()
        {
            ViewBag.CauHoi = ( Cauhoi());
            return View();
        }

        public List<String> Cauhoi()
        {
            List<String> list_cauhoi = new List<string>();
            list_cauhoi.Add("Tên con v?t mà b?n yêu thích ?");
            list_cauhoi.Add("Tên câu l?c b? th? thao mà b?n yêu thích ?");
            list_cauhoi.Add("Tên ca si mà b?n yêu thích ?");
            list_cauhoi.Add("Tên công vi?c mà b?n yêu thích ?");
            list_cauhoi.Add("Tên thú nuôi c?a b?n là gì ?");

            return list_cauhoi;
        }

        // Gi?i phóng b? nh? khi không s? d?ng
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