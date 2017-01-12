using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanhangMVC.Models;

namespace BanhangMVC.Controllers
{
    public class QuyenController : Controller
    {
        QuanlyBanhangEntities db = new QuanlyBanhangEntities();

        public ActionResult DanhSachThanhVien()
        {
            var listTV = db.ThanhViens.ToList();

            return View(listTV);
        }
    }
}