using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanhangMVC.Models.View;
using BanhangMVC.Models;

namespace BanhangMVC.Controllers
{
    public class GioHangController : Controller
    {
        QuanlyBanhangEntities db = new QuanlyBanhangEntities();

        public ActionResult XemGioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongTien = TinhTongTien();
            ViewBag.TongSoLuong = TinhTongSoLuong();
            return View(lstGioHang);
        }

        public ActionResult GioHangPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TSL = 0;
                ViewBag.TT = 0;
                return PartialView();
            }
            ViewBag.TSL = TinhTongSoLuong();
            ViewBag.TT = TinhTongTien();

            return PartialView();

        }

        public List<GioHang> LayGioHang()
        {
            // Gio Hang Da Ton Tai
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                // lstGioHang chua ton tai
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        public ActionResult ThemGioHang(int masp,String url)
        {
            //Kiem tra san pham co ton tai hay khong
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //---------------------------------------------------------------

            // lay gio hang
            List<GioHang> lstGioHang = LayGioHang();
            //truong hop san pham da ton tai trong gio hang
            GioHang spcheck = lstGioHang.SingleOrDefault(x => x.MaSP == masp);
            if (spcheck != null)
            {
                //kiem tra so luong ton
                if(sp.SoLuongTon < spcheck.Soluong)
                {
                    return View("ThongBaoGioHang");
                }
                spcheck.Soluong++;
                spcheck.ThanhTien = spcheck.Soluong * spcheck.Dongia;
                return Redirect(url);
            }
        
            GioHang itemGioHang = new GioHang(masp);
            if (sp.SoLuongTon < itemGioHang.Soluong)
            {
                return View("ThongBaoGioHang");
            }
            lstGioHang.Add(itemGioHang);
            return Redirect(url);
        }

        // tong so luong san pham trong gio hang
        public Double TinhTongSoLuong()
        {
            // lay gio hang

            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(x => x.Soluong);
        }

        //tinh tong tien trong gio hang
        public Decimal TinhTongTien()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(x => x.ThanhTien);
        }
   
       // chỉnh sửa giỏ hàng

        [HttpGet]
        public ActionResult SuaGioHang(int masp)
        {
            //kiem tra gio hang co rong hay khong
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // kiem tra san pham co trong csdl hay k
            SanPham sp = db.SanPhams.FirstOrDefault(x => x.MaSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            // kiem tra san pham da co trong gio hang hay k
            List<GioHang> lstGioHang = LayGioHang();
            GioHang spcheck = lstGioHang.FirstOrDefault(x => x.MaSP == masp);
            if (spcheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //lay list gio hang
            ViewBag.GioHang = lstGioHang;
            return View(spcheck);
        }

        // Cap Nhap Gio Hang
        [HttpPost]
        public ActionResult CapNhatGioHang(GioHang GH)
        {
            // kiem tra so luong vua nhap
            SanPham spcheck = db.SanPhams.SingleOrDefault(x => x.MaSP == GH.MaSP);
            if (spcheck.SoLuongTon < GH.Soluong)
            {
                return View("ThongBaoGioHang");
            }

            // cap nhat laij gio hangg
            List<GioHang> listGH = LayGioHang();
            GioHang GHupdate = listGH.Find(x => x.MaSP == GH.MaSP);
            GHupdate.Soluong = GH.Soluong;
            GHupdate.ThanhTien = GHupdate.Soluong * GHupdate.Dongia;
            return RedirectToAction("XemGioHang");
        }

        // xóa sp trong giohang
        public ActionResult XoaGioHang(int masp)
        {
            //kiem tra gio hang co rong hay khong
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // kiem tra san pham co trong csdl hay k
            SanPham sp = db.SanPhams.FirstOrDefault(x => x.MaSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // kiem tra ton tai trong gio hang
            List<GioHang> lstGioHang = LayGioHang();
            GioHang spcheck = lstGioHang.FirstOrDefault(x => x.MaSP == masp);
            if (spcheck == null)
            {
                return RedirectToAction("Index", "Home");
            }

            lstGioHang.Remove(spcheck);

            return RedirectToAction("XemGioHang");
        }

        // dat hang

        public ActionResult DatHang(KhachHang kh)
        {
            // kiem tra gio hang
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            KhachHang khachhang = new KhachHang();
            if (Session["taikhoan"] == null)
            {
                khachhang = kh;
                db.KhachHangs.Add(khachhang);
                db.SaveChanges();
            }
            else
            {
                ThanhVien tv = Session["taikhoan"] as ThanhVien;
                khachhang = new KhachHang();

                khachhang.TenKH = tv.HoTen;
                khachhang.DiaChi = tv.DiaChi;
                khachhang.SoDienThoai = tv.SoDienThoai;
                khachhang.Email = tv.Email;

                db.KhachHangs.Add(khachhang);
                db.SaveChanges();

            }
            // them don dat hang
            DonDatHang ddh = new DonDatHang();
            ddh.MaKH = khachhang.MaKH;
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrang = false;
            ddh.DaThanhToan = false;
            ddh.UuDai = 0;
            ddh.DaHuy = false;
            ddh.DaXoa = false;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();

            // them chi tiet don dat hang

            List<GioHang> lstGioHang = LayGioHang();
            foreach (var item in lstGioHang)
            {
                ChiTietDonDatHang ctddh = new ChiTietDonDatHang();
                ctddh.MaDDH = ddh.MaDDH;
                ctddh.MaSP = item.MaSP;
                ctddh.TenSP = item.TenSP;
                ctddh.SoLuong = item.Soluong;
                ctddh.DonGia = item.Dongia;

                db.ChiTietDonDatHangs.Add(ctddh);
            }

            db.SaveChanges();

            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang", "GioHang");
        }


        // them gio hang Ajax
        public ActionResult ThemGioHangAjax(int masp, String url)
        {
            //Kiem tra san pham co ton tai hay khong
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == masp);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //---------------------------------------------------------------

            // lay gio hang
            List<GioHang> lstGioHang = LayGioHang();
            //truong hop san pham da ton tai trong gio hang
            GioHang spcheck = lstGioHang.SingleOrDefault(x => x.MaSP == masp);
            if (spcheck != null)
            {
                //kiem tra so luong ton
                if (sp.SoLuongTon < spcheck.Soluong)
                {
                    return Content("<script>alert(\" Sản phẩm đã hết hàng !!! \")</script>");
                }
                spcheck.Soluong++;
                spcheck.ThanhTien = spcheck.Soluong * spcheck.Dongia;
                ViewBag.TT = TinhTongSoLuong();
                ViewBag.TSL = TinhTongTien().ToString("#,##");
                return PartialView("GioHangPartial");
            }

            GioHang itemGioHang = new GioHang(masp);
            if (sp.SoLuongTon < itemGioHang.Soluong)
            {
                return Content("<script>alert(\" Sản phẩm đã hết hàng !!! \")</script>");
            }
            lstGioHang.Add(itemGioHang);
            ViewBag.TT = TinhTongTien().ToString("#,## VNĐ");
            ViewBag.TSL = TinhTongSoLuong(); 
            return PartialView("GioHangPartial");
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