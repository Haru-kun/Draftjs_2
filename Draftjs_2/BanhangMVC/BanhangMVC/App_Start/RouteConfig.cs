using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BanhangMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");




            // URL khách hàng
            routes.MapRoute(
               name: "khachhang",
               url: "khach-hang",
               defaults: new { controller = "KhachHang", action = "index1", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Liên Hệ",
                url:"lien-he",
                defaults:new { Controller = "LienHe", Action = "Index", id = UrlParameter.Optional }   
            );
            routes.MapRoute(
                name: "Admin",
                url: "Admin",
                defaults: new { Controller = "DangNhap", Action = "DangNhapAdmin", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Trangchu",
                url: "Trangchu",
                defaults: new { Controller = "Home", Action = "Index", id = UrlParameter.Optional }
            );


            // URL ChiTietSanPham
            //routes.MapRoute(
            // name: "xemchitiet",
            //url: "{id}-{tensp}",
            //defaults: new { controller = "Sanpham", action = "ChitietSanpham", id = UrlParameter.Optional }
            //);

            //URL HOME
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
