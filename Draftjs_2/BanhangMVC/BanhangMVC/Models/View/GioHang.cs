using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanhangMVC.Models;

namespace BanhangMVC.Models.View
{
    public class GioHang
    {
        QuanlyBanhangEntities db = new QuanlyBanhangEntities();
        public string TenSP { get; set; }
        public int MaSP { get; set; }
        public int Soluong { get; set; }
        public Decimal Dongia { get; set; }
        public String HinhAnh { get; set; }
        public Decimal ThanhTien { get; set; }

        public GioHang(int masp)
        {
            this.MaSP = masp;
            SanPham sp = db.SanPhams.Single(x => x.MaSP == masp);
            this.TenSP = sp.TenSP;
            this.HinhAnh = sp.HinhAnh;
            this.Dongia = sp.DonGia.Value;
            this.Soluong = 1;
            this.ThanhTien = Dongia * Soluong;

        }
        public GioHang(int masp,int soluong)
        {
            this.MaSP = masp;
            SanPham sp = db.SanPhams.Single(x => x.MaSP == masp);
            this.TenSP = sp.TenSP;
            this.HinhAnh = sp.HinhAnh;
            this.Dongia = sp.DonGia.Value;
            this.Soluong = soluong;
            this.ThanhTien = Dongia * Soluong;

        }
        public GioHang()
        {

        }
    }
}