using HotelManagement.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace HotelManagement.Windows
{
    public partial class InfCustomerWindow : Window
    {
        KHACHHANG kh;
        string filename = null;
        bool CheckIMG = false;

        public InfCustomerWindow(KHACHHANG KhachHang)
        {
            InitializeComponent();
            kh = KhachHang;
            txtTenKH.Text = KhachHang.TENKH;
            txtNgaySinhKH.Text = KhachHang.NGAYSINH.ToString();
            txtDiaChiKH.Text = KhachHang.DIACHI;
            txtSoCMNDKH.Text = KhachHang.SOCMND;
            txtSDTKH.Text = KhachHang.SDT;
            cbGioiTinhKH.Text = KhachHang.GIOITINH;
            txtTaiKhoanKH.Text = KhachHang.TAIKHOANKH;
            if(KhachHang.MALTV != null)
            {
                cbLoaiKH.Text = KhachHang.LOAITV.TENLTV;
            }        
            txtDoanhSoKH.Text = KhachHang.DOANHSO.ToString();
            txtNgayDangKyKH.Text = KhachHang.NGAYDANGKY.ToString();

            KHACHHANG KH = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MAKH == KhachHang.MAKH).First();
            if (KhachHang.IMG != null)
            {
                Stream StreamObj = new MemoryStream(KH.IMG);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();

                imgKH.ImageSource = BitObj;
            }

            txtTenKH.IsEnabled = false;
            txtNgaySinhKH.IsEnabled = false;
            txtDiaChiKH.IsEnabled = false;
            txtSoCMNDKH.IsEnabled = false;
            txtSDTKH.IsEnabled = false;
            cbGioiTinhKH.IsEnabled = false;
            txtTaiKhoanKH.IsEnabled = false;
            txtDoanhSoKH.IsEnabled = false;
            txtNgayDangKyKH.IsEnabled = false;
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 0,
                offsetY: 40);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        private void btnImgKH_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            img.Title = "Select a picture";
            img.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";

            if (img.ShowDialog() == true)
            {
                filename = img.FileName;
                imgKH.ImageSource = new BitmapImage(new Uri(img.FileName));
                CheckIMG = true;
            }
        }

        private void SaveCustomer_btn_Click(object sender, RoutedEventArgs e)
        {
            UpdateCustomers();
            notifier.ShowSuccess("Cập nhật thông tin thành công!");
            this.Close();
        }

        private void UpdateCustomers()
        {
            KHACHHANG KH = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MAKH == kh.MAKH).SingleOrDefault();
            KH.TENKH = txtTenKH.Text;
            KH.GIOITINH = cbGioiTinhKH.Text;
            KH.DIACHI = txtDiaChiKH.Text;
            KH.SDT = txtSDTKH.Text;
            if(cbLoaiKH.Text == "Kim Cương")
            {
                KH.MALTV = "DIAMOND";
            }
            else if(cbLoaiKH.Text == "Vàng")
            {
                KH.MALTV = "GOLD";
            }
            else if(cbLoaiKH.Text == "Bạc")
            {
                KH.MALTV = "SILVER";
            }
            else if (cbLoaiKH.Text == "Đồng")
            {
                KH.MALTV = "BRONZE";
            }

            if (CheckIMG)
            {
                KH.IMG = File.ReadAllBytes(filename);
            }
            DataProvider.Ins.DB.SaveChanges();
        }

        private void InfCustomer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void txtSDTKH_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtSoCMNDKH_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void CancelCustomer_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
