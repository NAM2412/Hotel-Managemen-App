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
    class EmployeeViewModel : BaseViewModel
    {
        #region Tìm kiếm
        DateTime _selectedDateTimeStartNV;
        DateTime _selectedDateTimeEndNV;
        string _TextTimKiemNV;
        string _cbTimKiemNV;
        public DateTime SelectedDateTimeStartNV { get => _selectedDateTimeStartNV; set { _selectedDateTimeStartNV = value; OnPropertyChanged(); TimKiemNV(); } }
        public DateTime SelectedDateTimeEndNV { get => _selectedDateTimeEndNV; set { _selectedDateTimeEndNV = value; OnPropertyChanged(); TimKiemNV(); } }
        public string TextTimKiemNV { get => _TextTimKiemNV; set { _TextTimKiemNV = value; OnPropertyChanged(); TimKiemNV(); } }
        public string cbTimKiemNV { get => _cbTimKiemNV; set { _cbTimKiemNV = value; OnPropertyChanged(); } }
        #endregion Tìm kiếm

        private ObservableCollection<NHANVIEN> _ListNV;
        public ObservableCollection<NHANVIEN> ListNV { get => _ListNV; set { _ListNV = value; OnPropertyChanged(); } }

        private ObservableCollection<PHIEUTHUE> _ListPT;

        private NHANVIEN _SelectedItemNV;
        public NHANVIEN SelectedItemNV
        {
            get => _SelectedItemNV;
            set
            {
                _SelectedItemNV = value;
                OnPropertyChanged();
                if (SelectedItemNV != null)
                {
                    MANV = SelectedItemNV.MANV;
                    TENNV = SelectedItemNV.TENNV;
                    TAIKHOANNV = SelectedItemNV.TAIKHOANNV;
                    NGAYSINH = SelectedItemNV.NGAYSINH;
                    GIOITINH = SelectedItemNV.GIOITINH;
                    CMND = SelectedItemNV.CMND;
                    SDT = SelectedItemNV.SDT;
                    DIACHI = SelectedItemNV.DIACHI;
                    NGAYVAOLAM = SelectedItemNV.NGAYVAOLAM;
                    LUONG = SelectedItemNV.LUONG;
                    VAITRO = SelectedItemNV.VAITRO;
                }
            }
        }

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }

        public EmployeeViewModel()
        {
            ListNV = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);

            AddEmployeeCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Windows.AddEmployeeWindow addEmployeeWindow = new Windows.AddEmployeeWindow();
                addEmployeeWindow.ShowDialog();
                TextTimKiemNV = "";
                SelectedDateTimeStartNV = new DateTime(2010, 01, 01);
                SelectedDateTimeEndNV = DateTime.Now;
                ListNV = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
            });

            DeleteEmployeeCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItemNV == null)
                    return false;

                var displayList = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == SelectedItemNV.MANV);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;
            }, (p) =>
            {
                MessageBoxResult result = CustomMessageBox.Show("Xóa nhân viên?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    _ListPT = new ObservableCollection<PHIEUTHUE>(DataProvider.Ins.DB.PHIEUTHUEs.Where(x => x.MANV == SelectedItemNV.MANV));
                    foreach (var pt in _ListPT)
                    {
                        pt.MANV = null;
                    }

                    DataProvider.Ins.DB.NHANVIENs.Remove(SelectedItemNV);
                    DataProvider.Ins.DB.SaveChanges();
                    ListNV.Remove(SelectedItemNV);
                }
            });
        }

        #region Item
        private string _MANV;
        public string MANV { get => _MANV; set { _MANV = value; OnPropertyChanged(); } }
        private string _TENNV;
        public string TENNV { get => _TENNV; set { _TENNV = value; OnPropertyChanged(); } }
        private string _TAIKHOANNV;
        public string TAIKHOANNV { get => _TAIKHOANNV; set { _TAIKHOANNV = value; OnPropertyChanged(); } }
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
        private DateTime? _NGAYVAOLAM;
        public DateTime? NGAYVAOLAM { get => _NGAYVAOLAM; set { _NGAYVAOLAM = value; OnPropertyChanged(); } }
        private decimal? _LUONG;
        public decimal? LUONG { get => _LUONG; set { _LUONG = value; OnPropertyChanged(); } }
        private string _VAITRO;
        public string VAITRO { get => _VAITRO; set { _VAITRO = value; OnPropertyChanged(); } }
        #endregion Item

        private void TimKiemNV()
        {
            //var date = new DateTime(SelectedDateTimeEnd.Year, SelectedDateTimeEnd.Month, SelectedDateTimeEnd.Day, 23, 59, 59);

            if (TextTimKiemNV == "" || TextTimKiemNV == null)
                ListNV = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs.Where(x => x.NGAYVAOLAM >= SelectedDateTimeStartNV && x.NGAYVAOLAM <= SelectedDateTimeEndNV));
            else if (cbTimKiemNV == "Mã nhân viên")
            {
                ListNV = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs.Where(x => x.NGAYVAOLAM >= SelectedDateTimeStartNV && x.NGAYVAOLAM <= SelectedDateTimeEndNV && x.MANV.ToLower().Contains(TextTimKiemNV.ToLower())));
            }
            else if (cbTimKiemNV == "Tên nhân viên")
            {
                ListNV = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs.Where(x => x.NGAYVAOLAM >= SelectedDateTimeStartNV && x.NGAYVAOLAM <= SelectedDateTimeEndNV && x.TENNV.ToLower().Contains(TextTimKiemNV.ToLower())));
            }
            else if (cbTimKiemNV == "Tài khoản nhân viên")
            {
                ListNV = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs.Where(x => x.NGAYVAOLAM >= SelectedDateTimeStartNV && x.NGAYVAOLAM <= SelectedDateTimeEndNV && x.TAIKHOANNV.ToLower().Contains(TextTimKiemNV.ToLower())));
            }

            //for (int i = 0; i < ListPhieuNhap.Count; i++)
            //{
            //    ListPhieuNhap[i].GiaTriPhieuNhap = ListPhieuNhap[i].CTPhieuNhaps.Sum(x => x.DonGiaNhap * x.SoLuongNhap);
            //    ListPhieuNhap[i].TongSoSachNhap = ListPhieuNhap[i].CTPhieuNhaps.Sum(x => x.SoLuongNhap);
            //}
        }
    }
}
