using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModel
{
    class AddRoomViewModel : BaseViewModel
    {
        #region Label
        private string _lbTENP;
        public string lbTENP { get => _lbTENP; set { _lbTENP = value; OnPropertyChanged(); } }
        private string _lbGIANGAY;
        public string lbGIANGAY { get => _lbGIANGAY; set { _lbGIANGAY = value; OnPropertyChanged(); } }
        private string _lbGIADEM;
        public string lbGIADEM { get => _lbGIADEM; set { _lbGIADEM = value; OnPropertyChanged(); } }
        #endregion label

        #region Item
        private string _TENP;
        public string TENP { get => _TENP; set { _TENP = value; OnPropertyChanged(); KiemTraTenP(); } }
        private decimal _GIANGAY;
        public decimal GIANGAY { get => _GIANGAY; set { _GIANGAY = value; OnPropertyChanged(); KiemTraGiaNgay(); } }
        private decimal _GIADEM;
        public decimal GIADEM { get => _GIADEM; set { _GIADEM = value; OnPropertyChanged(); KiemTraGiaDem(); } }
        private string _TINHTRANGP;
        public string TINHTRANGP { get => _TINHTRANGP; set { _TINHTRANGP = value; OnPropertyChanged(); } }
        private string _LOAIP;
        public string LOAIP { get => _LOAIP; set { _LOAIP = value; OnPropertyChanged(); } }
        private string _MOTA;
        public string MOTA { get => _MOTA; set { _MOTA = value; OnPropertyChanged(); } }
        #endregion Item

        public ICommand SavePCommand { get; set; }
        public ICommand AddPCommand { get; set; }

        public AddRoomViewModel()
        {
            AddPCommand = new RelayCommand<object>((p) =>
            {
                var phong = DataProvider.Ins.DB.PHONGs.Where(x => x.TENPHONG == TENP).Count();
                if (TENP == null || TENP == "" || LOAIP == null || LOAIP == "" || GIANGAY.ToString() == null || GIANGAY.ToString() == "" || GIADEM.ToString() == null || GIADEM.ToString() == "" || MOTA.ToString() == null || MOTA.ToString() == "")
                    return false;
                else if (phong > 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {

            });

            SavePCommand = new RelayCommand<object>((p) =>
            {
                var phong = DataProvider.Ins.DB.PHONGs.Where(x => x.TENPHONG == TENP).Count();
                if (TENP == null || TENP == "" || GIANGAY.ToString() == null || GIANGAY.ToString() == "" || GIADEM.ToString() == null || GIADEM.ToString() == "" || MOTA.ToString() == null || MOTA.ToString() == "")
                    return false;
                else if (phong > 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {

            });
        }

        #region KiemTra
        private void KiemTraTenP()
        {
            var p = DataProvider.Ins.DB.PHONGs.Where(x => x.TENPHONG == TENP).Count();
            if (TENP == null || TENP == "")
            {
                lbTENP = "Vui lòng nhập đầy đủ tên phòng*";
            }
            else if (p > 0)
            {
                lbTENP = "Tên phòng bị trùng*";
            }
            else
            {
                lbTENP = "";
            }
        }
        private void KiemTraGiaNgay()
        {
            if (GIANGAY.ToString() == null || GIANGAY.ToString() == "")
            {
                lbGIANGAY = "Vui lòng nhập giá ngày cho phòng*";
            }
            else
            {
                lbGIANGAY = "";
            }
        }
        private void KiemTraGiaDem()
        {
            if (GIADEM.ToString() == null || GIADEM.ToString() == "")
            {
                lbGIADEM = "Vui lòng nhập giá đêm cho phòng*";
            }
            else
            {
                lbGIADEM = "";
            }
        }
        #endregion KiemTra
    }
}
