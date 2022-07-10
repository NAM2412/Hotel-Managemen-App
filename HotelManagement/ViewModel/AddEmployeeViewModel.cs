using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModel
{
    class AddEmployeeViewModel : BaseViewModel
    {
        #region Label
        private string _lbHOTENNV;
        public string lbHOTENNV { get => _lbHOTENNV; set { _lbHOTENNV = value; OnPropertyChanged(); } }
        //private string _lbMANV;
        //public string lbMANV { get => _lbMANV; set { _lbMANV = value; OnPropertyChanged(); } }
        private string _lbTAIKHOANNV;
        public string lbTAIKHOANNV { get => _lbTAIKHOANNV; set { _lbTAIKHOANNV = value; OnPropertyChanged(); } }
        #endregion label

        #region Item
        //private string _MANV;
        //public string MANV { get => _MANV; set { _MANV = value; OnPropertyChanged(); KiemTraMa(); } }
        private string _TENNV;
        public string TENNV { get => _TENNV; set { _TENNV = value; OnPropertyChanged(); KiemTraTen(); } }
        private string _TAIKHOANNV;
        public string TAIKHOANNV { get => _TAIKHOANNV; set { _TAIKHOANNV = value; OnPropertyChanged(); KiemTraTaiKhoan(); } }
        private DateTime _NGAYSINH;
        public DateTime NGAYSINH { get => _NGAYSINH; set { _NGAYSINH = value; OnPropertyChanged(); } }
        private string _GIOITINH;
        public string GIOITINH { get => _GIOITINH; set { _GIOITINH = value; OnPropertyChanged(); } }
        private string _CMND;
        public string CMND { get => _CMND; set { _CMND = value; OnPropertyChanged(); } }
        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        private string _DIACHI;
        public string DIACHI { get => _DIACHI; set { _DIACHI = value; OnPropertyChanged(); } }
        private DateTime _NGAYVAOLAM;
        public DateTime NGAYVAOLAM { get => _NGAYVAOLAM; set { _NGAYVAOLAM = value; OnPropertyChanged(); } }
        private decimal _LUONG;
        public decimal LUONG { get => _LUONG; set { _LUONG = value; OnPropertyChanged(); } }
        private string _VAITRO;
        public string VAITRO { get => _VAITRO; set { _VAITRO = value; OnPropertyChanged(); } }
        #endregion Item

        public ICommand SaveNVCommand { get; set; }
        public ICommand AddNVCommand { get; set; }

        public AddEmployeeViewModel()
        {
            AddNVCommand = new RelayCommand<object>((p) =>
            {
                var taikhoan = DataProvider.Ins.DB.NHANVIENs.Where(x => x.TAIKHOANNV == TAIKHOANNV).Count();
                if (TENNV == null || TENNV == "" || TAIKHOANNV == null || TAIKHOANNV == "")
                    return false;
                else if (taikhoan > 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                
            });

            SaveNVCommand = new RelayCommand<object>((p) =>
            {              
                if (TENNV == null || TENNV == "")
                    return false;               
                return true;
            }, (p) =>
            {

            });
        }

        #region KiemTra
        private void KiemTraTen()
        {
            if (TENNV == null || TENNV == "")
            {
                lbHOTENNV = "Vui lòng nhập đầy đủ họ tên*";
            }
            else
            {
                lbHOTENNV = "";
            }
        }
        //private void KiemTraMa()
        //{
        //    var nv = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == MANV).Count();
        //    if (MANV == null || MANV == "")
        //    {
        //        lbMANV = "Vui lòng nhập mã nhân viên*";
        //    }
        //    else if (nv > 0)
        //    {
        //        lbMANV = "Mã nhân viên đã tồn tại*";
        //    }
        //    else
        //    {
        //        lbMANV = "";
        //    }
        //}
        private void KiemTraTaiKhoan()
        {
            var nv = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TAIKHOAN == TAIKHOANNV).Count();
            if (TAIKHOANNV == null || TAIKHOANNV == "")
            {
                lbTAIKHOANNV = "Vui lòng nhập tài khoản đăng nhập*";
            }
            else if (nv > 0)
            {
                lbTAIKHOANNV = "Tài khoản đã tồn tại";
            }
            else
            {
                lbTAIKHOANNV = "";
            }
        }
        #endregion KiemTra
    }
}
