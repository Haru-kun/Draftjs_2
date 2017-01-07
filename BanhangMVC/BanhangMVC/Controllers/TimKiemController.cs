using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanhangMVC.Models;
using PagedList;

namespace BanhangMVC.Controllers
{
    public class TimKiemController : Controller
    {
        QuanlyBanhangEntities db = new QuanlyBanhangEntities();

        [HttpGet]
        public ActionResult TimKiemTheoten(string tukhoa,int? page)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }

            int soluongsanpham = 9;
            int tranghientai = (page ?? 1); // neu page k co gia tri thi normal la 1

            //tim kiem theo ten san pham
            var lstSP = db.SanPhams.Where(x => x.TenSP.Contains(tukhoa)); // contain la tim kiem theo tu khoa, chi can giong tu khoa thi tra ve
            ViewBag.TK = tukhoa;

            if (tukhoa== "Máy Tính Bảng")
            {
                lstSP = db.SanPhams.Where(x => x.MaLoaiSP == 3);
            }
            if (tukhoa == "Laptop")
            {
                lstSP = db.SanPhams.Where(x => x.MaLoaiSP == 2);
            }
            if (tukhoa == "Điện Thoại")
            {
                lstSP = db.SanPhams.Where(x => x.MaLoaiSP == 1);
            }
            if (tukhoa == null)
            {
                var lstSP1 = db.SanPhams;
                return View(lstSP1.OrderBy(x => x.TenSP).ToPagedList(tranghientai, soluongsanpham));
            }
            return View(lstSP.OrderBy(x=>x.TenSP).ToPagedList(tranghientai, soluongsanpham));
        }


        [HttpPost]
        public ActionResult LayTuKhoa(string tukhoa, int? page,FormCollection f)
        {
            return RedirectToAction("TimKiemTheoten", new { @tukhoa = tukhoa });
        }

        public ActionResult TimKiemPartial(string tukhoa)
        {

            //tim kiem theo ten san pham
            var lstSP = db.SanPhams.Where(x => x.TenSP.Contains(tukhoa)); // contain la tim kiem theo tu khoa, chi can giong tu khoa thi tra ve
            ViewBag.TK = tukhoa;
            return PartialView(lstSP);
        }

        [HttpPost]
        public ActionResult XemTheo(int? Loai)
        {
            var lstSP = db.SanPhams.OrderByDescending(x=>x.NgayCapNhat);

            switch (Loai)
            {
                case 2:
                    {
                        lstSP = db.SanPhams.OrderByDescending(x=>x.DonGia);
                    }break;
                case 3:
                    {
                        lstSP = db.SanPhams.OrderBy(x => x.DonGia);
                    }
                    break;
                default:
                    {
                        lstSP = db.SanPhams.OrderByDescending(x => x.NgayCapNhat);
                    }break;
            }

            return PartialView(lstSP);
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