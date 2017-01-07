using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanhangMVC.Models;
using System.IO;
using System.Web.Security;

namespace BanhangMVC.Controllers
{
    [Authorize(Roles = "QuanTri,QuanLySanPham")]
    public class QuanLySanPhamController : Controller
    {
        QuanlyBanhangEntities db = new QuanlyBanhangEntities();

        public ActionResult QuanLySanPham()
        {
            var listSP = db.SanPhams.Where(x => x.DaXoa == false).OrderBy(x => x.MaLoaiSP);

            return View(listSP);
        }

        // Thêm
        [HttpGet]
        public ActionResult ThemSanPhamMoi()
        {
            // đổ dũ liệu dropdownlist
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(x => x.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(x => x.MaNSX), "MaNSX", "TenNSX");

            return View();
        }

        [ValidateInput(false)] // tắt để sử dụng tinyme
        [HttpPost]
        public ActionResult ThemSanPhamMoi(SanPham sp, HttpPostedFileBase[] HinhAnh)
        {
            // đổ dũ liệu dropdownlist
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(x => x.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(x => x.MaNSX), "MaNSX", "TenNSX");
            int loi = 0;

            for (int i = 0; i < HinhAnh.Count(); i++)
            {
                if (HinhAnh[i] != null) //kiểm tra tên hình xem đã tồn tại hay chưa.
                {
                    if (HinhAnh[i].ContentLength > 0)
                    {
                        if (HinhAnh[i].ContentType != "image/jpeg"
                            && HinhAnh[i].ContentType != "image/png"
                            && HinhAnh[i].ContentType != "image/gif"
                            && HinhAnh[i].ContentType != "image/jpg"
                            && HinhAnh[i].ContentType != "image/bmp")
                        {
                            ViewBag.upload = "Định dạng hình ảnh " + i + " Không hợp lệ !!! </br>";
                            loi++;
                        }
                        else
                        {
                            // lay ten hinh anh
                            var fileName = Path.GetFileName(HinhAnh[i].FileName);
                            // lay hinh anh chuyen vao thu muc HinhanhSP
                            var path = Path.Combine(Server.MapPath("~/Content/HinhanhSP"), fileName);
                            if (System.IO.File.Exists(path))
                            {
                                ViewBag.upload = "Hình ảnh " + i + " đã tồn tại !!!</br>";
                                loi++;
                            }
                            HinhAnh[i].SaveAs(path);
                        }
                    }
                }
            }
            if (loi > 0)
            {
                return View(sp);
            }
            //  lấy tên hình ảnh

            sp.HinhAnh  = HinhAnh[0].FileName;
            sp.HinhAnh1 = HinhAnh[1].FileName;
            sp.HinhAnh2 = HinhAnh[2].FileName;
            sp.HinhAnh3 = HinhAnh[3].FileName;
            sp.HinhAnh4 = HinhAnh[4].FileName;
            db.SanPhams.Add(sp);
            db.SaveChanges();
            return RedirectToAction("QuanLySanPham");
        }

        // Chỉnh Sửa
        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(x => x.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(x => x.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);


            return View(sp);
        }

        //[ValidateInput(false)] // tắt để sử dụng tinyme
        //[HttpPost]
        //public ActionResult ChinhSua(SanPham sp, HttpPostedFileBase HinhAnh1, HttpPostedFileBase HinhAnh2, HttpPostedFileBase HinhAnh3, HttpPostedFileBase HinhAnh4)
        //{
   
        //    var fileName1 = "";
        //    var fileName2 = "";
        //    var fileName3 = "";
        //    var fileName4 = "";
        //    var path1 = "";
        //    var path2 = "";
        //    var path3 = "";
        //    var path4 = "";


        //    ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(x => x.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
        //    ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
        //    ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(x => x.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);


        //    //var spCheck = db.SanPhams.SingleOrDefault(x => x.MaSP == sp.MaSP);



        //    ////  lấy tên hình ảnh
        //    //// lấy hình ảnh chuyển vào thư muc HinhanhSP
        //    //luu lai
        //    if (HinhAnh1 == null)
        //    {
        //        fileName1 = sp.HinhAnh1;
        //    }
        //    else
        //    {
        //        fileName1 = Path.GetFileName(HinhAnh1.FileName);
        //        path1 = Path.Combine(Server.MapPath("~/Content/HinhanhSP"), fileName1);
        //        HinhAnh1.SaveAs(path1);
        //    }
        //    if (HinhAnh1 == null)
        //    {
        //        fileName2 = sp.HinhAnh2;
        //    }
        //    else
        //    {
        //        fileName2 = Path.GetFileName(HinhAnh2.FileName);
        //        path2 = Path.Combine(Server.MapPath("~/Content/HinhanhSP"), fileName2);
        //        HinhAnh2.SaveAs(path2);
        //    }
        //    if (HinhAnh3 == null)
        //    {
        //        fileName3 = sp.HinhAnh3;
        //    }
        //    else
        //    {
        //        fileName1 = Path.GetFileName(HinhAnh3.FileName);
        //        path3 = Path.Combine(Server.MapPath("~/Content/HinhanhSP"), fileName3);
        //        HinhAnh3.SaveAs(path3);
        //    }
        //    if (HinhAnh4 == null)
        //    {
        //        fileName4 = sp.HinhAnh4;
        //    }
        //    else
        //    {
        //        fileName4 = Path.GetFileName(HinhAnh4.FileName);
        //        path4 = Path.Combine(Server.MapPath("~/Content/HinhanhSP"), fileName4);
        //        HinhAnh4.SaveAs(path4);
        //    }

        //    /////////////////////////////////////////////
        //    //if (sp.HinhAnh2 == null || sp.HinhAnh2 == "")
        //    //{
        //    //    fileName2 = spCheck.HinhAnh1.ToString();
        //    //}
        //    //else
        //    //{
        //    //    fileName2 = Path.GetFileName(HinhAnh2.FileName);
        //    //    path2 = Path.Combine(Server.MapPath("~/Content/HinhanhSP"), fileName2);
        //    //    HinhAnh2.SaveAs(path2);
        //    //}

        //    /////////////////////////////////////////////
        //    //if (sp.HinhAnh3 == null || sp.HinhAnh3 == "")
        //    //{
        //    //    fileName3 = spCheck.HinhAnh3.ToString();

        //    //}
        //    //else
        //    //{
        //    //    fileName3 = Path.GetFileName(HinhAnh3.FileName);
        //    //    path3 = Path.Combine(Server.MapPath("~/Content/HinhanhSP"), fileName3);
        //    //    HinhAnh3.SaveAs(path3);
        //    //}

        //    /////////////////////////////////////////////
        //    //if (sp.HinhAnh4 == null || sp.HinhAnh4 == "")
        //    //{
        //    //    fileName4 = spCheck.HinhAnh4.ToString();
        //    //}
        //    //else
        //    //{
        //    //    fileName4 = Path.GetFileName(HinhAnh4.FileName);
        //    //    path4 = Path.Combine(Server.MapPath("~/Content/HinhanhSP"), fileName4);
        //    //    HinhAnh4.SaveAs(path4);

        //    //}





        //    // lấy hình ảnh chuyển vào thư muc HinhanhSP



        //    //nếu hình ảnh đã tồn tại thì xuất ra thông báo
        //    //if (System.IO.File.Exists(path1) ||
        //    //    System.IO.File.Exists(path2) ||
        //    //    System.IO.File.Exists(path3) ||
        //    //    System.IO.File.Exists(path4))
        //    //{
        //    //    ViewBag.upload = "Hình đã tồn tại";
        //    //    return View(sp);
        //    //}
        //    //else
        //    ////{
        //    //    HinhAnh1.SaveAs(path1);
        //    //    HinhAnh2.SaveAs(path2);
        //    //    HinhAnh3.SaveAs(path3);
        //    //    HinhAnh4.SaveAs(path4);
        //    //    sp.HinhAnh1 = fileName1;
        //    //    sp.HinhAnh2 = fileName2;
        //    //    sp.HinhAnh3 = fileName3;
        //    //    sp.HinhAnh4 = fileName4;
        //    //}


        //    sp.HinhAnh1 = fileName1;
        //    sp.HinhAnh2 = fileName2;
        //    sp.HinhAnh3 = fileName3;
        //    sp.HinhAnh4 = fileName4;

        //    // nếu dữ liệu đầu vào đã chắc chắn đúng thì k cần kiểm tra ModelState.isvalid

        //    db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("QuanLySanPham");

        //    //if (ModelState.IsValid)
        //    //{
        //    //    db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
        //    //    db.SaveChanges();
        //    //    return RedirectToAction("QuanLySanPham");
        //    //}
        //    //return View(sp);
        //}

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(SanPham sp, HttpPostedFileBase[] HinhAnhChon)
        {

            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(x => x.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(x => x.MaNSX), "MaNSX", "TenNSX");
            int loi = 0;
            List<String> error = new List<string>();


            for (int i = 0; i < HinhAnhChon.Count(); i++)
            {
                if (HinhAnhChon[i] != null) //kiểm tra tên hình xem đã tồn tại hay chưa.
                {
                    if (HinhAnhChon[i].ContentLength > 0)
                    {
                        if (HinhAnhChon[i].ContentType != "image/jpeg"
                            && HinhAnhChon[i].ContentType != "image/png"
                            && HinhAnhChon[i].ContentType != "image/gif"
                            && HinhAnhChon[i].ContentType != "image/jpg"
                            && HinhAnhChon[i].ContentType != "image/bmp")
                        {
                            ViewBag.upload = "Định dạng hình ảnh " + i + " Không hợp lệ !!! </br>";
                            loi++;
                        }
                        else
                        {
                            //// lay ten hinh anh
                            //var fileName = Path.GetFileName(HinhAnhChon[i].FileName);
                            var fileName = "";
                            var path="";
                            //int[] error=new int[5];
                            if (HinhAnhChon[i] == null)
                            {
                                fileName = sp.HinhAnh;
                            }
                            else
                            {
                                if (HinhAnhChon[i].FileName == sp.HinhAnh)
                                {
                                    fileName = sp.HinhAnh;
                                    loi++;
                                }
                                else
                                {
                                    fileName = Path.GetFileName(HinhAnhChon[i].FileName);
                                    path = Path.Combine(Server.MapPath("~/Content/HinhanhSP"), fileName);
                                    if (System.IO.File.Exists(path))
                                    {
                                        error.Add("Hình ảnh " + i + " đã tồn tại !!!");
                                        loi++;
                                    }
                                    HinhAnhChon[i].SaveAs(path);
                                }
                            }
                        }
                    }
                }
            }
            if (error.Count() > 0)
            {
                ViewBag.upload = error;
                return View(sp);
            }
            //  lấy tên hình ảnh

            if (HinhAnhChon[0] != null)
            {
                sp.HinhAnh = HinhAnhChon[0].FileName;
            }
            if (HinhAnhChon[1] != null)
            {
                sp.HinhAnh1 = HinhAnhChon[1].FileName;
            }
            if (HinhAnhChon[2] != null)
            {
                sp.HinhAnh2 = HinhAnhChon[2].FileName;
            }
            if (HinhAnhChon[3] != null)
            {
                sp.HinhAnh3 = HinhAnhChon[3].FileName;
            }
            if (HinhAnhChon[4] != null)
            {
                sp.HinhAnh4 = HinhAnhChon[4].FileName;
            }

            db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("QuanLySanPham");
        }

        [HttpGet]
        public ActionResult Xoa(int? id)
        {
            // tien hanh kiem tra id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == id);

            if (sp == null)
            {
                return HttpNotFound();
            }

            // đổ dũ liệu dropdownlist
            //ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(x => x.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            //ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(x => x.MaLoaiSP), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            //ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(x => x.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);
            ViewBag.MaNCC = db.NhaCungCaps.Where(x => x.MaNCC == sp.MaNCC).Select(x => x.TenNCC).FirstOrDefault().ToString();
            ViewBag.MaLoaiSP = db.LoaiSanPhams.Where(x => x.MaLoaiSP == sp.MaLoaiSP).Select(x => x.TenLoai).FirstOrDefault().ToString();
            ViewBag.MaNSX = db.NhaSanXuats.Where(x => x.MaNSX == sp.MaNSX).Select(x => x.TenNSX).FirstOrDefault().ToString();

            return View(sp);
        }

        // Xóa
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Xoa(int? id, FormCollection f)
        {
            // tien hanh kiem tra id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = db.NhaCungCaps.Where(x => x.MaNCC == sp.MaNCC).Select(x => x.TenNCC).FirstOrDefault().ToString();
            ViewBag.MaLoaiSP = db.LoaiSanPhams.Where(x => x.MaLoaiSP == sp.MaLoaiSP).Select(x => x.TenLoai).FirstOrDefault().ToString();
            ViewBag.MaNSX = db.NhaSanXuats.Where(x => x.MaNSX == sp.MaNSX).Select(x => x.TenNSX).FirstOrDefault().ToString();

            ChiTietDonDatHang sp_ddh = db.ChiTietDonDatHangs.SingleOrDefault(x => x.MaSP == id);
            if (sp_ddh != null)
            {
                ViewBag.ThongBao = "Không thể xóa sản phẩm này vì lý do về khóa";
                return View(sp);
            }
            db.SanPhams.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("QuanLySanPham");
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

        public void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                                                        TaiKhoan, //user
                                                        DateTime.Now, //begin
                                                        DateTime.Now.AddHours(3), //timeout
                                                        false, //remember?
                                                        Quyen, // permission.. "admin" or for more than one admin,marketing,sales"
                                                        FormsAuthentication.FormsCookiePath);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            //encrypt để mã hóa bảo mật thông tin
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;// kiểm tra cookie có tồn tại hay chưa
            Response.Cookies.Add(cookie);
        }
    }
}