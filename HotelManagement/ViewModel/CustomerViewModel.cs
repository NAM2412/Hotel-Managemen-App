using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelManagement.ViewModel
{
    class CustomerViewModel : BaseViewModel
    {
        #region Tìm kiếm
        private string _cbTimKiemKH;
        public string cbTimKiemKH { get => _cbTimKiemKH; set { _cbTimKiemKH = value; OnPropertyChanged(); TimKiemKH(); } }
        private string _TextTimKiemKH;
        public string TextTimKiemKH { get => _TextTimKiemKH; set { _TextTimKiemKH = value; OnPropertyChanged(); TimKiemKH(); } }
        private DateTime _selectedDateTimeStartKH;
        public DateTime SelectedDateTimeStartKH { get => _selectedDateTimeStartKH; set { _selectedDateTimeStartKH = value; OnPropertyChanged(); TimKiemKH(); } }
        private DateTime _selectedDateTimeEndKH;
        public DateTime SelectedDateTimeEndKH { get => _selectedDateTimeEndKH; set { _selectedDateTimeEndKH = value; OnPropertyChanged(); TimKiemKH(); } }
        #endregion TimKiem

        private ObservableCollection<KHACHHANG> _ListKH;
        public ObservableCollection<KHACHHANG> ListKH { get => _ListKH; set { _ListKH = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUTHUE> _ListPT;

        private KHACHHANG _SelectedItemKH;
        public KHACHHANG SelectedItemKH
        {
            get => _SelectedItemKH;
            set
            {
                _SelectedItemKH = value;
                OnPropertyChanged();
                if (SelectedItemKH != null)
                {
                    MAKH = SelectedItemKH.MAKH;
                    TENKH = SelectedItemKH.TENKH;
                    TAIKHOANKH = SelectedItemKH.TAIKHOANKH;
                    NGAYSINH = SelectedItemKH.NGAYSINH;
                    GIOITINH = SelectedItemKH.GIOITINH;
                    CMND = SelectedItemKH.SOCMND;
                    SDT = SelectedItemKH.SDT;
                    DIACHI = SelectedItemKH.DIACHI;
                    NGAYDANGKY = SelectedItemKH.NGAYDANGKY;
                    MALTV = SelectedItemKH.MALTV;
                    DOANHSO = SelectedItemKH.DOANHSO;
                }
            }
        }

        public ICommand DeleteCustomerCommand { get; set; }

        public CustomerViewModel()
        {
            ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);

            DeleteCustomerCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItemKH == null)
                    return false;

                var displayList = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MAKH == SelectedItemKH.MAKH);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;
            }, (p) =>
            {
                MessageBoxResult result = CustomMessageBox.Show("Xóa nhân viên?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    _ListPT = new ObservableCollection<PHIEUTHUE>(DataProvider.Ins.DB.PHIEUTHUEs.Where(x => x.MAKH == SelectedItemKH.MAKH));
                    foreach (var pt in _ListPT)
                    {
                        pt.MAKH = null;
                    }

                    DataProvider.Ins.DB.KHACHHANGs.Remove(SelectedItemKH);
                    DataProvider.Ins.DB.SaveChanges();
                    ListKH.Remove(SelectedItemKH);
                }
            });
        }

        #region Item
        private string _MAKH;
        public string MAKH { get => _MAKH; set { _MAKH = value; OnPropertyChanged(); } }
        private string _TENKH;
        public string TENKH { get => _TENKH; set { _TENKH = value; OnPropertyChanged(); } }
        private string _TAIKHOANKH;
        public string TAIKHOANKH { get => _TAIKHOANKH; set { _TAIKHOANKH = value; OnPropertyChanged(); } }
        private DateTime? _NGAYSINH;
        public DateTime? NGAYSINH { get => _NGAYSINH; set { _NGAYSINH = value; OnPropertyChanged(); } }
        private string _GIOITINH;
        public string GIOITINH { get => _GIOITINH; set { _GIOITINH = value; OnPropertyChanged(); } }
        private string _CMND;
        public string CMND { get => _CMND; set { _CMND = value; OnPropertyChanged(); } }
        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value; OnPropertyChanged(); } }
        private string _DIACHI;
        public string DIACHI { get => _DIACHI; set { _DIACHI = value; OnPropertyChanged(); } }
        private DateTime? _NGAYDANGKY;
        public DateTime? NGAYDANGKY { get => _NGAYDANGKY; set { _NGAYDANGKY = value; OnPropertyChanged(); } }
        private string _MALTV;
        public string MALTV { get => _MALTV; set { _MALTV = value; OnPropertyChanged(); } }
        private decimal? _DOANHSO;
        public decimal? DOANHSO { get => _DOANHSO; set { _DOANHSO = value; OnPropertyChanged(); } }
        #endregion Item

        private void TimKiemKH()
        {
            if (TextTimKiemKH == "" || TextTimKiemKH == null)
                ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.NGAYDANGKY >= SelectedDateTimeStartKH && x.NGAYDANGKY <= SelectedDateTimeEndKH));
            else if (cbTimKiemKH == "Mã khách hàng")
            {
                ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.NGAYDANGKY >= SelectedDateTimeStartKH && x.NGAYDANGKY <= SelectedDateTimeEndKH && x.MAKH.ToLower().Contains(TextTimKiemKH.ToLower())));
            }
            else if (cbTimKiemKH == "Tên khách hàng")
            {
                ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.NGAYDANGKY >= SelectedDateTimeStartKH && x.NGAYDANGKY <= SelectedDateTimeEndKH && x.TENKH.ToLower().Contains(TextTimKiemKH.ToLower())));
            }
            else if (cbTimKiemKH == "Tài khoản khách hàng")
            {
                ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.NGAYDANGKY >= SelectedDateTimeStartKH && x.NGAYDANGKY <= SelectedDateTimeEndKH && x.TAIKHOANKH.ToLower().Contains(TextTimKiemKH.ToLower())));
            }
            else if (cbTimKiemKH == "Số điện thoại")
            {
                ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.NGAYDANGKY >= SelectedDateTimeStartKH && x.NGAYDANGKY <= SelectedDateTimeEndKH && x.SDT.ToLower().Contains(TextTimKiemKH.ToLower())));
            }
            else if (cbTimKiemKH == "Số CMND")
            {
                ListKH = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs.Where(x => x.NGAYDANGKY >= SelectedDateTimeStartKH && x.NGAYDANGKY <= SelectedDateTimeEndKH && x.SOCMND.ToLower().Contains(TextTimKiemKH.ToLower())));
            }          
        }
    }
}
