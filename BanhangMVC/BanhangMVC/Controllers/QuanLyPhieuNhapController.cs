using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanhangMVC.Models;

namespace BanhangMVC.Controllers
{
    [Authorize(Roles = "QuanTri,QuanLySanPham")]
    public class QuanLyPhieuNhapController : Controller
    {
        QuanlyBanhangEntities db = new QuanlyBanhangEntities();
        [Authorize(Roles = "QuanTri,QuanLySanPham")]
        [HttpGet]
        public ActionResult NhapHang()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(x => x.TenNCC), "MaNCC", "TenNCC");
            ViewBag.ListSanPham = db.SanPhams;



            return View();
        }

        [Authorize(Roles = "QuanTri,QuanLySanPham")]
        [HttpPost]
        public ActionResult NhapHang(PhieuNhap Model,IEnumerable<ChiTietPhieuNhap> ListModel)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(x => x.TenNCC), "MaNCC", "TenNCC");
            ViewBag.ListSanPham = db.SanPhams;

            Model.DaXoa = false;
            db.PhieuNhaps.Add(Model);
            db.SaveChanges();
            SanPham sp;
            foreach(var item in ListModel)
            {
                sp = db.SanPhams.SingleOrDefault(x => x.MaSP == item.MaSP);
                sp.SoLuongTon += item.SoLuongNhap;
                sp.DonGia = item.DonGiaNhap;
                item.MaPN = Model.MaPN;
            }
            // addrange la add 1 list
            db.ChiTietPhieuNhaps.AddRange(ListModel);
            db.SaveChanges();

            return View();
        }

        [HttpGet]
        public ActionResult HetHang()
        {
            var lstSP = db.SanPhams.Where(x => x.DaXoa == false && x.SoLuongTon < 5);
            return View(lstSP);
        }

        [Authorize(Roles = "QuanTri,QuanLySanPham")]
        [HttpGet]
        public ActionResult NhapHangDon(int? id)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(x => x.TenNCC), "MaNCC", "TenNCC");
            ViewBag.ListSanPham = db.SanPhams;

            var sp = db.SanPhams.SingleOrDefault(x => x.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaSP = id;

            return View(sp);
        }

        [Authorize(Roles = "QuanTri,QuanLySanPham")]
        [HttpPost]
        public ActionResult NhapHangDon(PhieuNhap model,ChiTietPhieuNhap ctpn)
        {
            model.DaXoa = false;
            db.PhieuNhaps.Add(model);
            db.SaveChanges();

            ctpn.MaPN = model.MaPN;

            SanPham sp=db.SanPhams.SingleOrDefault(x=>x.MaSP==ctpn.MaSP);
            sp.SoLuongTon += ctpn.SoLuongNhap;
            db.ChiTietPhieuNhaps.Add(ctpn);
            db.SaveChanges();

            return RedirectToAction("HetHang");
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