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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NHANVIEN NV = null;
        KHACHHANG KH = null;
        public MainWindow()
        {
            InitializeComponent();
            PagesNavigation.Navigate(new System.Uri("Pages/RoomPage.xaml", UriKind.RelativeOrAbsolute));
        }

        public MainWindow(NHANVIEN nv)
        {
            InitializeComponent();
            NV = nv;
            PagesNavigation.Navigate(new System.Uri("Pages/RoomPage.xaml", UriKind.RelativeOrAbsolute));
            name.Content = nv.TENNV;
            SDT.Content = nv.SDT;
            if (nv.IMG != null)
            {
                Stream StreamObj = new MemoryStream(nv.IMG);
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

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdEmployee_Click(object sender, RoutedEventArgs e)
        {
            // PagesNavigation.Navigate(new HomePage());

            PagesNavigation.Navigate(new System.Uri("Pages/EmployeePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdRoom_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Pages/RoomPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdCustomer_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Pages/CustomerPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdBill_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Pages/BillPage.xaml", UriKind.RelativeOrAbsolute));
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
            if (NV != null)
            {
                PagesNavigation.Navigate(new UserPage(NV));
            }
            else if(KH != null)
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
