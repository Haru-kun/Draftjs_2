using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanhangMVC.Models;
using System.Net.Mail;


namespace BanhangMVC.Controllers
{
    [Authorize(Roles = "QuanTri")]
    public class QuanLyDonDatHangController : Controller
    {
        QuanlyBanhangEntities db = new QuanlyBanhangEntities();


        public ActionResult ChuaThanhToan()
        {
            var lstCTT = db.DonDatHangs.Where(x => x.DaThanhToan == false && x.DaXoa == false && x.DaHuy == false);

            return View(lstCTT);
        }

        public ActionResult ChuaGiao()
        {
            var lstCG = db.DonDatHangs.Where(x => x.TinhTrang == false && x.DaThanhToan==true && x.DaXoa == false && x.DaHuy == false);

            return View(lstCG);
        }

        public ActionResult DaGiaoThanhToan()
        {
            var lstDGTT = db.DonDatHangs.Where(x => x.TinhTrang == true && x.DaThanhToan == true);

            return View(lstDGTT);
        }

        [HttpGet]
        public ActionResult DuyetDonHang(int? id)
        {
            if(id==0 || id == null)
            {
                return HttpNotFound();
            }
            DonDatHang ddt = db.DonDatHangs.SingleOrDefault(x=>x.MaDDH==id);
            if (ddt == null)
            {
                return HttpNotFound();
            }

            var listsp = db.ChiTietDonDatHangs.Where(x => x.MaDDH == id);
            decimal tongtien = 0;
            foreach(var item in listsp)
            {
               tongtien+=(Decimal) (item.DonGia * item.SoLuong);
            }
            ViewBag.TT=tongtien;
            ViewBag.ListSP = listsp;

            return View(ddt);
        }

        [HttpPost]
        public ActionResult DuyetDonHang(DonDatHang ddh)
        {

            DonDatHang ddhUpdate = db.DonDatHangs.Single(x => x.MaDDH == ddh.MaDDH);
            ddhUpdate.DaThanhToan = ddh.DaThanhToan;
            ddhUpdate.TinhTrang = ddh.TinhTrang;
            db.SaveChanges();
            GuiEmail("Xác nhận đơn hàng", "bachquangthang901603@gmail.com", "bachquangthang901603@gmail.com","thangbach1995","Don hang duoc xac nhan");
            //GuiEmail("Xác nhận đơn hàng", "bachquangthang901603@gmail.com", "bachquangthang901603@gmail.com", "thangbach1995", );

            return RedirectToAction("ChuaThanhToan");
        }

        public void GuiEmail(string Title, string ToEmail, string FromEmail, string PassWord, string Content)
        {
            // goi email
            MailMessage mail = new MailMessage();
            mail.To.Add(ToEmail); // Địa chỉ nhận
            mail.From = new MailAddress(ToEmail); // Địa chửi gửi
            mail.Subject = Title;  // tiêu đề gửi
            mail.Body = "";  // Nội dung
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; // host gửi của Gmail
            smtp.Port = 587;               //port của Gmail
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            (FromEmail, PassWord);//Tài khoản password người gửi
            smtp.EnableSsl = true;   //kích hoạt giao tiếp an toàn SSL
            smtp.Send(mail);   //Gửi mail đi
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