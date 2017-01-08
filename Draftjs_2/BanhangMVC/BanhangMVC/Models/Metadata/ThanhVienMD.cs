using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanhangMVC.Models
{
    [MetadataTypeAttribute(typeof(ThanhVienMetadata))]
    public partial class ThanhVien
    {
        internal sealed class ThanhVienMetadata
        {
            //Danh sách các thuộc tính
            public int MaThanhVien { get; set; }

            [DisplayName("Tài khoản:")]
            [Required(ErrorMessage = "{0} còn trống. Xin nhập lại !!!")]
            public string TaiKhoan { get; set; }

            [DisplayName("Mật khẩu:")]
            [Required(ErrorMessage = "{0} còn trống. Xin nhập lại !!!")]
            public string MatKhau { get; set; }

            [DisplayName("Họ tên:")]
            [Required(ErrorMessage = "{0} còn trống. Xin nhập lại !!!")]
            public string HoTen { get; set; }

            [DisplayName("Địa chỉ:")]
            [Required(ErrorMessage = "{0} còn trống. Xin nhập lại !!!")]
            public string DiaChi { get; set; }

            [DisplayName("Email:")]
            [Required(ErrorMessage = "{0} còn trống. Xin nhập lại !!!")]
            public string Email { get; set; }

            [DisplayName("Số diện thoại:")]
            [Required(ErrorMessage = "{0} còn trống. Xin nhập lại !!!")]
            public string SoDienThoai { get; set; }

            [DisplayName("Câu hỏi bí mật:")]
            [Required(ErrorMessage = "{0} còn trống. Xin nhập lại !!!")]
            public string CauHoi { get; set; }

            [DisplayName("Câu trả lời:")]
            [Required(ErrorMessage = "{0} còn trống. Xin nhập lại !!!")]
            public string CauTraLoi { get; set; }
            public Nullable<int> MaLoaiTV { get; set; }
        }
    }
}