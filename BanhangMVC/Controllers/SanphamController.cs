using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanhangMVC.Models;
using System.Net;
using PagedList;

namespace BanhangMVC.Controllers
{
    
    public class SanphamController : Controller
    {
        QuanlyBanhangEntities db = new QuanlyBanhangEntities();

        [ChildActionOnly]

        public ActionResult SanphamPartial1()
        {
            return PartialView();
        }
        [ChildActionOnly]

        public ActionResult SanphamPartial2()
        {

            return PartialView();
        }

        public ActionResult SanphamPartial3()
        {

            return PartialView();
        }

        public ActionResult ThuonghieuPartial()
        {
            return PartialView();
        }

        public ActionResult ChitietSanpham(int? id,string tensp)
        {   
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sp = db.SanPhams.SingleOrDefault(x => x.MaSP == id && x.DaXoa==false);
            if(sp==null)
            {
                return HttpNotFound();

            }
            var lstBL = db.BinhLuans.Where(x => x.MaSP == id);
            var lstLQ = db.SanPhams.Where(x => x.MaLoaiSP == sp.MaLoaiSP);
            sp.LuotXem += 1;
            db.SaveChanges();
            ViewBag.BinhLuan = lstBL;
            ViewBag.SanPhamLienQuan = lstLQ;
            return View(sp);
        }

        public ActionResult XemNhieuNhat()
        {
            var listLuotXem = db.SanPhams.OrderByDescending(x => x.LuotXem);
            return PartialView(listLuotXem);
        }


        public ActionResult DanhsachSanpham(int? MaLoaiSp,int? MaNSX,int? page )
        {
            if(MaNSX==null || MaLoaiSp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstSP = db.SanPhams.Where(x => x.MaLoaiSP == MaLoaiSp && x.MaNSX == MaNSX);
            if (lstSP.Count() == 0)
            {
                return HttpNotFound();
            }
            //Tien hang thuc hien phan trang
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }

            int soluongsanpham = 9;
            int tranghientai = (page ?? 1); 
            // neu page k co gia tri thi normal la 1

            ViewBag.MALSP=MaLoaiSp;
            ViewBag.MANSX = MaNSX;

            
            return View(lstSP.OrderBy(x=>x.DonGia).ToPagedList(tranghientai,soluongsanpham));
            // phân trang sản phẩm
        }

        public ActionResult MenuPartialLeft()
        {
            var listSP = db.SanPhams;
            return PartialView(listSP);
        }
    }
}