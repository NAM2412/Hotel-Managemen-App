using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModel
{
    class BillViewModel : BaseViewModel
    {
        #region Tìm kiếm
        DateTime _selectedDateTimeStartHD;
        DateTime _selectedDateTimeEndHD;
        string _TextTimKiemHD;
        string _cbTimKiemHD;
        public DateTime SelectedDateTimeStartHD { get => _selectedDateTimeStartHD; set { _selectedDateTimeStartHD = value; OnPropertyChanged(); TimKiemHD(); } }
        public DateTime SelectedDateTimeEndHD { get => _selectedDateTimeEndHD; set { _selectedDateTimeEndHD = value; OnPropertyChanged(); TimKiemHD(); } }
        public string TextTimKiemHD { get => _TextTimKiemHD; set { _TextTimKiemHD = value; OnPropertyChanged(); TimKiemHD(); } }
        public string cbTimKiemHD { get => _cbTimKiemHD; set { _cbTimKiemHD = value; OnPropertyChanged(); } }
        #endregion Tìm kiếm

        private ObservableCollection<HOADON> _ListHD;
        public ObservableCollection<HOADON> ListHD { get => _ListHD; set { _ListHD = value; OnPropertyChanged(); } }

        private HOADON _SelectedItemHD;
        public HOADON SelectedItemHD
        {
            get => _SelectedItemHD;
            set
            {
                _SelectedItemHD = value;
                OnPropertyChanged();
                if (SelectedItemHD != null)
                {
                    MAHD = SelectedItemHD.MAHD;
                    MAPHIEU = SelectedItemHD.MAPHIEU;
                    NGAYHOADON = SelectedItemHD.NGAYHOADON;
                    KHUYENMAI = SelectedItemHD.KHUYENMAI;
                    SOTIENPHAITRA = SelectedItemHD.SOTIENPHAITRA;
                }
            }
        }

        public ICommand AddBillCommand { get; set; }

        public BillViewModel()
        {
            ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);

        }


        #region item
        private string _MAHD;
        public string MAHD { get => _MAHD; set { _MAHD = value; OnPropertyChanged(); } }
        private string _MAPHIEU;
        public string MAPHIEU { get => _MAPHIEU; set { _MAPHIEU = value; OnPropertyChanged(); } }
        private DateTime? _NGAYHOADON;
        public DateTime? NGAYHOADON { get => _NGAYHOADON; set { _NGAYHOADON = value; OnPropertyChanged(); } }
        private decimal? _KHUYENMAI;
        public decimal? KHUYENMAI { get => _KHUYENMAI; set { _KHUYENMAI = value; OnPropertyChanged(); } }
        private decimal? _SOTIENPHAITRA;
        public decimal? SOTIENPHAITRA { get => _SOTIENPHAITRA; set { _SOTIENPHAITRA = value; OnPropertyChanged(); } }
        #endregion Tìm kiếm

        private void TimKiemHD()
        {

            if (TextTimKiemHD == "" || TextTimKiemHD == null)
                ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs.Where(x => x.NGAYHOADON >= SelectedDateTimeStartHD && x.NGAYHOADON <= SelectedDateTimeEndHD));
            else if (cbTimKiemHD == "Mã hóa đơn")
            {
                ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs.Where(x => x.NGAYHOADON >= SelectedDateTimeStartHD && x.NGAYHOADON <= SelectedDateTimeEndHD && x.MAHD.ToLower().Contains(TextTimKiemHD.ToLower())));
            }
            else if (cbTimKiemHD == "Mã nhân viên")
            {
                ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs.Where(x => x.NGAYHOADON >= SelectedDateTimeStartHD && x.NGAYHOADON <= SelectedDateTimeEndHD && x.PHIEUTHUE.MANV.ToLower().Contains(TextTimKiemHD.ToLower())));
            }
            else if (cbTimKiemHD == "Mã khách hàng")
            {
                ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs.Where(x => x.NGAYHOADON >= SelectedDateTimeStartHD && x.NGAYHOADON <= SelectedDateTimeEndHD && x.PHIEUTHUE.MAKH.ToLower().Contains(TextTimKiemHD.ToLower())));
            }
            else if (cbTimKiemHD == "Tên khách hàng")
            {
                ListHD = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs.Where(x => x.NGAYHOADON >= SelectedDateTimeStartHD && x.NGAYHOADON <= SelectedDateTimeEndHD && x.PHIEUTHUE.KHACHHANG.TENKH.ToLower().Contains(TextTimKiemHD.ToLower())));
            }
        }
    }
}
