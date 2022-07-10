using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelManagement.ViewModel
{
    class InfCustomerViewModel : BaseViewModel
    {
        #region Label
        private string _lbHOTENKH;
        public string lbHOTENKH { get => _lbHOTENKH; set { _lbHOTENKH = value; OnPropertyChanged(); } }
        #endregion label

        #region Item
        private string _TENKH;
        public string TENKH { get => _TENKH; set { _TENKH = value; OnPropertyChanged(); KiemTraTenKH(); } }
        private string _TAIKHOANKH;
        public string TAIKHOANKH { get => _TAIKHOANKH; set { _TAIKHOANKH = value; OnPropertyChanged(); } }
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
        private DateTime _NGAYDANGKY;
        public DateTime NGAYDANGKY { get => _NGAYDANGKY; set { _NGAYDANGKY = value; OnPropertyChanged(); } }
        private decimal _DOANHSO;
        public decimal DOANHSO { get => _DOANHSO; set { _DOANHSO = value; OnPropertyChanged(); } }
        private string _LOAITV;
        public string LOAITV { get => _LOAITV; set { _LOAITV = value; OnPropertyChanged(); } }
        #endregion Item

        public ICommand SaveKHCommand { get; set; }

        public InfCustomerViewModel()
        {
            SaveKHCommand = new RelayCommand<object>((p) =>
            {
                if (TENKH == null || TENKH == "")
                    return false;
                return true;
            }, (p) =>
            {

            });
        }

        #region KiemTra
        private void KiemTraTenKH()
        {
            if (TENKH == null || TENKH == "")
            {
                lbHOTENKH = "Vui lòng nhập đầy đủ họ tên*";
            }
            else
            {
                lbHOTENKH = "";
            }
        }
        #endregion KiemTra
    }
}
