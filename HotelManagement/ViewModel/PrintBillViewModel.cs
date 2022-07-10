
using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelManagement.ViewModel
{
    public class PrintBillViewModel:  BaseViewModel
    {
        private string _MAPHONG { get; set; }
        public string MAPHONG { get => _MAPHONG; set { _MAPHONG = value; OnPropertyChanged(); } }
        private string _MAKH { get; set; }
        public string MAKH { get => _MAKH; set { _MAKH = value; OnPropertyChanged(); } }
        private string _TENKH { get; set; }
        public string TENKH { get => _TENKH; set { _TENKH = value; OnPropertyChanged(); } }
        
        private DateTime _NGAYHOADON { get; set; }
        public DateTime NGAYHOADON { get => _NGAYHOADON; set { _NGAYHOADON = value; OnPropertyChanged(); } }
        private double _Cost { get; set; }
        public double Cost { get => _Cost; set { _Cost = value; OnPropertyChanged(); } }
        public ICommand CloseCommand { get; set; }
        public ICommand PayCommand { get; set; }

        private ObservableCollection<HOADON> _BillList;
        public ObservableCollection<HOADON> BillList { get => _BillList; set { _BillList = value; OnPropertyChanged(); } }

        public PrintBillViewModel()
        {
            BillList = new ObservableCollection<HOADON>();
            var billList = DataProvider.Ins.DB.HOADONs;


            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            PayCommand = new RelayCommand<Window>((p) => { return true; },
                (p) =>
                {


                    p.Close();
                });
        }
        void Close(Window p)
        {
            p.Close();
        }
    }
}

