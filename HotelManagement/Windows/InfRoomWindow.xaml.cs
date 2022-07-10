using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
    /// Interaction logic for InfRoomWindow.xaml
    /// </summary>
    public partial class InfRoomWindow : Window
    {
        decimal l;
        PHONG P;
        KHACHHANG KH;
        public InfRoomWindow(PHONG Phong, KHACHHANG kh)
        {
            InitializeComponent();
            P = Phong;
            KH = kh;
            lbTenP.Content = Phong.TENPHONG;
            l = (decimal)Phong.GIAPHONG_DAY;
            lbGiaNgay.Content = l.ToString("N0") + " VNĐ";
            lbLoaiP.Content = Phong.LOAIPHONG.TENLOAI;
            l = (decimal)Phong.GIAPHONG_NIGHT;
            lbGiaDem.Content = l.ToString("N0") + " VNĐ";
            lbTinhTrangP.Content = Phong.TINHTRANG;
            lbMoTaP.Content = Phong.MOTA;

            PHONG phong = DataProvider.Ins.DB.PHONGs.Where(x => x.MAPHONG == Phong.MAPHONG).First();
            if (phong.IMG != null)
            {
                Stream StreamObj = new MemoryStream(phong.IMG);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();

                imgP.ImageSource = BitObj;
            }
        }

        private void CancelBooking_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void InfRoom_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void PayRoom_btn_Click(object sender, RoutedEventArgs e)
        {
            AddBillWindow addBillWindow = new AddBillWindow(P, KH);
            addBillWindow.Show();
            this.Close();
        }
    }
}
