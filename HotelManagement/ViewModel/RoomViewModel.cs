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
    class RoomViewModel : BaseViewModel
    {
        #region Tìm kiếm
        string _cbTimKiemP;
        string _txtTimKiemP;
        string _cbGiaP;
        public string cbTimKiemP { get => _cbTimKiemP; set { _cbTimKiemP = value; OnPropertyChanged(); } }
        public string txtTimKiemP { get => _txtTimKiemP; set { _txtTimKiemP = value; OnPropertyChanged(); TimKiemP(); } }
        public string cbGiaP { get => _cbGiaP; set { _cbGiaP = value; OnPropertyChanged(); TimKiemP(); } }
        #endregion Tìm kiếm

        private ObservableCollection<PHONG> _ListP;
        public ObservableCollection<PHONG> ListP { get => _ListP; set { _ListP = value; OnPropertyChanged(); } }

        private ObservableCollection<CHITIETPHIEUTHUE> _ListCTPT;

        private PHONG _SelectedItemP;
        public PHONG SelectedItemP
        {
            get => _SelectedItemP;
            set
            {
                _SelectedItemP = value;
                OnPropertyChanged();
                if(SelectedItemP != null)
                {
                    MAPHONG = SelectedItemP.MAPHONG;
                    TENPHONG = SelectedItemP.TENPHONG;
                    TINHTRANG = SelectedItemP.TINHTRANG;
                    GIAPHONG_DAY = SelectedItemP.GIAPHONG_DAY;
                    GIAPHONG_NIGHT = SelectedItemP.GIAPHONG_NIGHT;
                    MOTA = SelectedItemP.MOTA;
                    MALOAI = SelectedItemP.MALOAI;
                }    
            }
        }

        #region Item
        private string _MAPHONG;
        public string MAPHONG { get => _MAPHONG; set { _MAPHONG = value; OnPropertyChanged(); } }
        private string _TENPHONG;
        public string TENPHONG { get => _TENPHONG; set { _TENPHONG = value; OnPropertyChanged(); } }
        private string _TINHTRANG;
        public string TINHTRANG { get => _TINHTRANG; set { _TINHTRANG = value; OnPropertyChanged(); } }
        private decimal? _GIAPHONG_DAY;
        public decimal? GIAPHONG_DAY { get => _GIAPHONG_DAY; set { _GIAPHONG_DAY = value; OnPropertyChanged(); } }
        private decimal? _GIAPHONG_NIGHT;
        public decimal? GIAPHONG_NIGHT { get => _GIAPHONG_NIGHT; set { _GIAPHONG_NIGHT = value; OnPropertyChanged(); } }
        private string _MOTA;
        public string MOTA { get => _MOTA; set { _MOTA = value; OnPropertyChanged(); } }
        private string _MALOAI;
        public string MALOAI { get => _MALOAI; set { _MALOAI = value; OnPropertyChanged(); } }
        #endregion Item

        public ICommand AddRoomCommand { get; set; }
        public ICommand DeleteRoomCommand { get; set; }

        public RoomViewModel()
        {
            ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs);

            DeleteRoomCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItemP == null)
                    return false;

                var displayList = DataProvider.Ins.DB.PHONGs.Where(x => x.MAPHONG == SelectedItemP.MAPHONG);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;
            }, (p) =>
            {
                MessageBoxResult result = CustomMessageBox.Show("Xóa phòng?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    _ListCTPT = new ObservableCollection<CHITIETPHIEUTHUE>(DataProvider.Ins.DB.CHITIETPHIEUTHUEs.Where(x => x.MAPHONG == SelectedItemP.MAPHONG));
                    foreach (var ctpt in _ListCTPT)
                    {
                        ctpt.MAPHONG = null;
                    }


                    DataProvider.Ins.DB.PHONGs.Remove(SelectedItemP);
                    DataProvider.Ins.DB.SaveChanges();
                    ListP.Remove(SelectedItemP);
                }
            });

            AddRoomCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Windows.AddRoomWindow addRoomWindow = new Windows.AddRoomWindow();
                addRoomWindow.ShowDialog();
                txtTimKiemP = "";
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs);
            });
        }

        private void TimKiemP()
        {

            if ((txtTimKiemP == "" || txtTimKiemP == null) && cbGiaP == null)
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs);
            else if ((txtTimKiemP == "" || txtTimKiemP == null) && cbGiaP == "Dưới 800")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.GIAPHONG_DAY < 800000));
            }
            else if ((txtTimKiemP == "" || txtTimKiemP == null) && cbGiaP == "800 - 1,5 triệu")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.GIAPHONG_DAY >= 800000 && x.GIAPHONG_DAY < 1500000));
            }
            else if ((txtTimKiemP == "" || txtTimKiemP == null) && cbGiaP == "1,5 triệu - 3 triệu")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.GIAPHONG_DAY >= 1500000 && x.GIAPHONG_DAY < 3000000));
            }
            else if ((txtTimKiemP == "" || txtTimKiemP == null) && cbGiaP == "Trên 3 triệu")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.GIAPHONG_DAY >= 3000000));
            }

            else if (cbTimKiemP == "Mã Phòng" && cbGiaP == null)
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.MAPHONG.ToLower().Contains(txtTimKiemP.ToLower())));
            }
            else if (cbTimKiemP == "Mã Phòng" && cbGiaP == "Dưới 800")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.MAPHONG.ToLower().Contains(txtTimKiemP.ToLower()) && x.GIAPHONG_DAY < 800000));
            }
            else if (cbTimKiemP == "Mã Phòng" && cbGiaP == "800 - 1,5 triệu")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.MAPHONG.ToLower().Contains(txtTimKiemP.ToLower()) && x.GIAPHONG_DAY >= 800000 && x.GIAPHONG_DAY < 1500000));
            }
            else if (cbTimKiemP == "Mã Phòng" && cbGiaP == "1,5 triệu - 3 triệu")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.MAPHONG.ToLower().Contains(txtTimKiemP.ToLower()) && x.GIAPHONG_DAY >= 1500000 && x.GIAPHONG_DAY < 3000000));
            }
            else if (cbTimKiemP == "Mã Phòng" && cbGiaP == "Trên 3 triệu")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.MAPHONG.ToLower().Contains(txtTimKiemP.ToLower()) && x.GIAPHONG_DAY >= 3000000));
            }            
           
            else if (cbTimKiemP == "Tên Phòng" && cbGiaP == null)
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.TENPHONG.ToLower().Contains(txtTimKiemP.ToLower())));
            }
            else if (cbTimKiemP == "Tên Phòng" && cbGiaP == "Dưới 800")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.TENPHONG.ToLower().Contains(txtTimKiemP.ToLower()) && x.GIAPHONG_DAY < 800000));
            }
            else if (cbTimKiemP == "Tên Phòng" && cbGiaP == "800 - 1,5 triệu")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.TENPHONG.ToLower().Contains(txtTimKiemP.ToLower()) && x.GIAPHONG_DAY >= 800000 && x.GIAPHONG_DAY < 1500000));
            }
            else if (cbTimKiemP == "Tên Phòng" && cbGiaP == "1,5 triệu - 3 triệu")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.TENPHONG.ToLower().Contains(txtTimKiemP.ToLower()) && x.GIAPHONG_DAY >= 1500000 && x.GIAPHONG_DAY < 3000000));
            }
            else if (cbTimKiemP == "Tên Phòng" && cbGiaP == "Trên 3 triệu")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.TENPHONG.ToLower().Contains(txtTimKiemP.ToLower()) && x.GIAPHONG_DAY >= 3000000));
            }

            else if (cbTimKiemP == "Loại Phòng" && cbGiaP == null)
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.LOAIPHONG.TENLOAI.ToLower().Contains(txtTimKiemP.ToLower())));
            }
            else if (cbTimKiemP == "Loại Phòng" && cbGiaP == "Dưới 800")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.LOAIPHONG.TENLOAI.ToLower().Contains(txtTimKiemP.ToLower()) && x.GIAPHONG_DAY < 800000));
            }
            else if (cbTimKiemP == "Loại Phòng" && cbGiaP == "800 - 1,5 triệu")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.LOAIPHONG.TENLOAI.ToLower().Contains(txtTimKiemP.ToLower()) && x.GIAPHONG_DAY >= 800000 && x.GIAPHONG_DAY < 1500000));
            }
            else if (cbTimKiemP == "Loại Phòng" && cbGiaP == "1,5 triệu - 3 triệu")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.LOAIPHONG.TENLOAI.ToLower().Contains(txtTimKiemP.ToLower()) && x.GIAPHONG_DAY >= 1500000 && x.GIAPHONG_DAY < 3000000));
            }
            else if (cbTimKiemP == "Loại Phòng" && cbGiaP == "Trên 3 triệu")
            {
                ListP = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs.Where(x => x.LOAIPHONG.TENLOAI.ToLower().Contains(txtTimKiemP.ToLower()) && x.GIAPHONG_DAY >= 3000000));
            }

            //decimal sum = 0;
            //for (int i = 0; i < ListHD.Count; i++)
            //{
            //    sum += (decimal)ListHD[i].SOTIENPHAITRA;
            //    //ListHD[i].GiaTriPhieuNhap = ListPhieuNhap[i].CTPhieuNhaps.Sum(x => x.DonGiaNhap * x.SoLuongNhap);
            //    //ListPhieuNhap[i].TongSoSachNhap = ListPhieuNhap[i].CTPhieuNhaps.Sum(x => x.SoLuongNhap);
            //}
            //TongTien = "Tổng tiền: " + sum.ToString("N0") + " đ";
        }
    }
}
