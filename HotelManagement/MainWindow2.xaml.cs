using HotelManagement.Model;
using HotelManagement.Pages;
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

namespace HotelManagement
{
    /// <summary>
    /// Interaction logic for MainWindow2.xaml
    /// </summary>
    public partial class MainWindow2 : Window
    {
        KHACHHANG KH;
        public MainWindow2(KHACHHANG kh)
        {
            InitializeComponent();
            KH = kh;
            PagesNavigation.Navigate(new BookingRoomPage(kh));
            name.Content = kh.TENKH;
            SDT.Content = kh.SDT;
            if (kh.IMG != null)
            {
                Stream StreamObj = new MemoryStream(kh.IMG);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();

                img.ImageSource = BitObj;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void rdRoom_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new BookingRoomPage(KH));
        }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = CustomMessageBox.Show("Bạn có muốn đăng xuất?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                //LoginWindow loginWindow = new LoginWindow();
                //loginWindow.Show();
                this.Close();
            }
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (KH != null)
            {
                PagesNavigation.Navigate(new UserPage(KH));
            }
            else
            {
                PagesNavigation.Navigate(new UserPage());
            }
        }

        private void home_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
