using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BanhangMVC.Models
{
    [MetadataTypeAttribute(typeof(SanPhamMetaData))]
    public partial class SanPham
    {
        internal sealed class SanPhamMetaData
        {
            [DisplayName("Mã sản phẩm:")]
            public int MaSP { get; set; }

            [DisplayName("Tên sản phẩm:")]
            public string TenSP { get; set; }

            [DisplayName("Đơn giá:")]
            public Nullable<decimal> DonGia { get; set; }

            [DisplayName("Ngày cập nhật:")]
            public Nullable<System.DateTime> NgayCapNhat { get; set; }

            [DisplayName("Cấu hình:")]
            public string CauHinh { get; set; }

            [DisplayName("Mô tả:")]
            public string MoTa { get; set; }

            [DisplayName("Hình ảnh:")]
            public string HinhAnh { get; set; }

            [DisplayName("Hình ảnh 1:")]
            public string HinhAnh1 { get; set; }

            [DisplayName("Hình ảnh 2:")]
            public string HinhAnh2 { get; set; }

            [DisplayName("Hình ảnh 3:")]
            public string HinhAnh3 { get; set; }

            [DisplayName("Hình ảnh 4:")]
            public string HinhAnh4 { get; set; }

            [DisplayName("Số lượng tồn:")]
            public Nullable<int> SoLuongTon { get; set; }

            [DisplayName("Lượt xem:")]
            public Nullable<int> LuotXem { get; set; }
            public Nullable<int> LuotBinhChon { get; set; }
            public Nullable<int> LuotBinhLuan { get; set; }
            public Nullable<int> Moi { get; set; }

            [DisplayName("Nhà cung cấp:")]
            public Nullable<int> MaNCC { get; set; }

            [DisplayName("Nhà sản xuất:")]
            public Nullable<int> MaNSX { get; set; }

            [DisplayName("Loại sản phẩm:")]
            public Nullable<int> MaLoaiSP { get; set; }
            public Nullable<bool> DaXoa { get; set; }

        }
    }
}