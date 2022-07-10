
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

namespace HotelManagement.Windows
{
    /// <summary>
    /// Interaction logic for PrintBillWindow.xaml
    /// </summary>
    public partial class PrintBillWindow : Window
    {
        public PrintBillWindow(PHIEUTHUE pt)
        {
            InitializeComponent();
            MAKH.Text = pt.MAKH;
            TENKH.Text = pt.KHACHHANG.TENKH;
            NGAYHOADON.Text = pt.NGAYTHUE.ToString();
            invoiceNo.Text = pt.MAPHIEU;
            MALTV.Text = pt.KHACHHANG.LOAITV.TENLTV;
            ObservableCollection<CHITIETPHIEUTHUE> ListCTHD = new ObservableCollection<CHITIETPHIEUTHUE>(DataProvider.Ins.DB.CHITIETPHIEUTHUEs.Where(x => x.MAPHIEU == pt.MAPHIEU));
            datagridCTHD.ItemsSource = ListCTHD;
            decimal t = 0;
            for (int i = 0; i < ListCTHD.Count; i++)
            {
                t += (decimal)ListCTHD[i].DONGIA;
            }
            price.Text = "$" + t.ToString("N0") + " VNĐ";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "invoice");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
