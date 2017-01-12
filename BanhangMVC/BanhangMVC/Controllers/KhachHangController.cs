using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanhangMVC.Models;

namespace BanhangMVC.Controllers
{
    
    [Authorize(Roles = "QuanTri,QuanLySanPham")]
    public class KhachHangController : Controller
    {
        QuanlyBanhangEntities db = new QuanlyBanhangEntities();

        [Authorize(Roles = "QuanTri")]
        // GET: KhachHang
        public ActionResult Index()
        {
            var lstKH = from KH in db.KhachHangs select KH;
            return View(lstKH);
        }

        [Authorize(Roles = "QuanLySanPham")]
        public ActionResult Index1()
        {
            // Truy vấn dữ liệu qua câu lệnh
            // Đối tượng lstKH sẽ lấy toàn bộ dữ liệu từ bảng khách hàng
            //var lstKH = from KH in db.KhachHangs select KH; // Cách 1
            var lstKH = db.KhachHangs.ToList();   //cách 2
            return View(lstKH);
        }
        public ActionResult TruyVanMotDoiTuong()
        {
            //truy van moi doi tuong

            //cach 1
            //var listKH = from KH in db.KhachHangs where KH.MaKH==1 select KH ;
            //KhachHang khachhang = listKH.FirstOrDefault();
            //cach 2
            KhachHang khachhang = db.KhachHangs.Single(x=>x.MaKH==1);
            
            return View(khachhang);
        }
        public ActionResult GroupDulieu()
        {
            var listKH = db.ThanhViens.OrderByDescending(x=>x.TaiKhoan).ToList();
            return View(listKH);
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