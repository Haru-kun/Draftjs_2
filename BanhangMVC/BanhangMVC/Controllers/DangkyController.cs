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
            list_cauhoi.Add("T�n con v?t m� b?n y�u th�ch ?");
            list_cauhoi.Add("T�n c�u l?c b? th? thao m� b?n y�u th�ch ?");
            list_cauhoi.Add("T�n ca si m� b?n y�u th�ch ?");
            list_cauhoi.Add("T�n c�ng vi?c m� b?n y�u th�ch ?");
            list_cauhoi.Add("T�n th� nu�i c?a b?n l� g� ?");

            return list_cauhoi;
        }

        // Gi?i ph�ng b? nh? khi kh�ng s? d?ng
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