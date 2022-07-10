using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModel
{
    class AddBillViewModel : BaseViewModel
    {
        #region Label
        private string _lbMAP;
        public string lbMAP { get => _lbMAP; set { _lbMAP = value; OnPropertyChanged(); } }
        private string _lbMaKH;
        public string lbMaKH { get => _lbMaKH; set { _lbMaKH = value; OnPropertyChanged(); } }
        private string _lbMaPT;
        public string lbMaPT { get => _lbMaPT; set { _lbMaPT = value; OnPropertyChanged(); } }
        #endregion label

        #region Item
        private string _MAP;
        public string MAP { get => _MAP; set { _MAP = value; OnPropertyChanged(); KiemTraMaP(); } }
        private string _MAKH;
        public string MAKH { get => _MAKH; set { _MAKH = value; OnPropertyChanged(); KiemTraMaKH(); } }
        private string _MaPT;
        public string MaPT { get => _MaPT; set { _MaPT = value; OnPropertyChanged(); KiemTraMaPT(); } }
        private string _LOAITHUE;
        public string LOAITHUE { get => _LOAITHUE; set { _LOAITHUE = value; OnPropertyChanged(); } }
        private DateTime _TUNGAY;
        public DateTime TUNGAY { get => _TUNGAY; set { _TUNGAY = value; OnPropertyChanged(); } }
        private DateTime _DENNGAY;
        public DateTime DENNGAY { get => _DENNGAY; set { _DENNGAY = value; OnPropertyChanged(); } }
        private DateTime _NGAYDAT;
        public DateTime NGAYDAT { get => _NGAYDAT; set { _NGAYDAT = value; OnPropertyChanged(); } }
        #endregion Item

        public ICommand BookPCommand { get; set; }

        public AddBillViewModel()
        {
            BookPCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {

            });
        }

        #region KiemTra
        private void KiemTraMaP()
        {
            var p = 0;
            p = DataProvider.Ins.DB.PHONGs.Where(x => x.MAPHONG == MAP).Count();
            if (MAP == null || MAP == "")
            {
                lbMAP = "Vui lòng nhập đầy đủ mã phòng*";
            }
            else if (p == 0)
            {
                lbMAP = "Mã phòng không tồn tại*";
            }
            else
            {
                lbMAP = "";
            }
        }
        private void KiemTraMaKH()
        {
            var kh = 0;
            kh = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MAKH == MAKH).Count();
            if (MAKH == null || MAKH == "")
            {
                lbMaKH = "Vui lòng nhập mã khách hàng đặt phòng*";
            }
            else if(kh == 0)
            {
                lbMaKH = "Mã khách hàng không tồn tại*";
            }
            else
            {
                lbMaKH = "";
            }
        }
        private void KiemTraMaPT()
        {
            var pt = 0;
            pt = DataProvider.Ins.DB.PHIEUTHUEs.Where(x => x.MAPHIEU == MaPT).Count();
            if (pt > 0)
            {
                lbMaPT = "Mã phiếu thuê đã tồn tại tồn tại*";
            }
            else
            {
                lbMaPT = "";
            }
        }
        #endregion KiemTra
    }
}
