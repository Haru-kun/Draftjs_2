using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanhangMVC.Models;

namespace BanhangMVC.Controllers
{
    public class HomeController : Controller
    {
        QuanlyBanhangEntities db = new QuanlyBanhangEntities();
        // GET: Home
        public ActionResult Index()
        {

            var listDTM = db.SanPhams.Where(x => x.MaLoaiSP == 1 && x.Moi == 1);
            ViewBag.lstDTM = listDTM;

            var listLTM = db.SanPhams.Where(x => x.MaLoaiSP == 2 && x.Moi == 1);
            ViewBag.lstLTM = listLTM;

            var listMTBM = db.SanPhams.Where(x => x.MaLoaiSP == 3 && x.Moi == 1);
            ViewBag.lstMTBM = listMTBM;
            return View();
        }

        public ActionResult MenuPartial()
        {
            var listSP = db.SanPhams;
            return PartialView(listSP);
        }

        public ActionResult Dangky()
        {

            return View();
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