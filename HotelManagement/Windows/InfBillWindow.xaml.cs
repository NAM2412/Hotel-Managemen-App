
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
    
    public partial class InfBillWindow : Window
    {
        HOADON hd;
        public InfBillWindow(HOADON HoaDon)
        {
            InitializeComponent();
            hd = HoaDon;
            txtMaHD.Text = HoaDon.MAHD;
            txtTenKH.Text = HoaDon.PHIEUTHUE.KHACHHANG.TENKH;
            txtNgayHD.Text = HoaDon.NGAYHOADON.ToString();
            cbTinhTrang.Text = HoaDon.TINHTRANG;
            txtMaNV.Text = HoaDon.PHIEUTHUE.MANV;
            txtTuNgay.Text = HoaDon.PHIEUTHUE.NGAYBATDAU.ToString();
            txtDenNgay.Text = HoaDon.PHIEUTHUE.NGAYTRAPHONG.ToString();

            ObservableCollection<CHITIETPHIEUTHUE> ListCTHD = new ObservableCollection<CHITIETPHIEUTHUE>(DataProvider.Ins.DB.CHITIETPHIEUTHUEs.Where(x => x.MAPHIEU == HoaDon.MAPHIEU));
            datagridCTHD.ItemsSource = ListCTHD;

            decimal t = 0;
            for (int i = 0; i < ListCTHD.Count; i++)
            {
                t += (decimal)ListCTHD[i].DONGIA;
            }
            lbTongTien.Content = "$" + t.ToString("N0") + " VNĐ";
            t = (decimal)HoaDon.KHUYENMAI;
            lbKhuyenMai.Content = "$" + t.ToString("N0") + " VNĐ";
            t = (decimal)HoaDon.SOTIENPHAITRA;
            lbPhaiTra.Content = "$" + t.ToString("N0") + " VNĐ";

            if(HoaDon.TINHTRANG == "Đã thanh toán")
            {
                cbTinhTrang.IsEnabled = false;
                editHD.Visibility = Visibility.Hidden;
            }
            else
            {
                cbTinhTrang.IsEnabled = true;
                editHD.Visibility = Visibility.Visible;
            }
        }

        private void editHD_Click(object sender, RoutedEventArgs e)
        {
            HOADON HD = DataProvider.Ins.DB.HOADONs.Where(x => x.MAHD == txtMaHD.Text).SingleOrDefault();
            KHACHHANG KH = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MAKH == HD.PHIEUTHUE.MAKH).SingleOrDefault();

            if (cbTinhTrang.Text == "Đã thanh toán")
            {
                HD.TINHTRANG = "Đã thanh toán";
                KH.DOANHSO += HD.SOTIENPHAITRA;
            }

            DataProvider.Ins.DB.SaveChanges();
            this.Close();
        }

        private void InfBill_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CancelBooking_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
