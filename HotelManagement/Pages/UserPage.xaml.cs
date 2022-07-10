using HotelManagement.Model;
using HotelManagement.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManagement.Pages
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        string filename = null;
        bool CheckAvt = false;
        NHANVIEN NV = null;
        KHACHHANG KH = null;
        public UserPage()
        {
            InitializeComponent();
            name.Text = "Quang Mạnh";
            birthday.Text = "12/11/2002";
            sex.Text = "Nam";
            phone.Text = "123456789";
            address.Text = "123 ABC";
            position.Text = "Admin";
            edit_btn.Visibility = Visibility.Hidden;
            changePassword.Visibility = Visibility.Hidden;
        }

        public UserPage(NHANVIEN nv)
        {
            InitializeComponent();
            NV = nv;
            name.Text = nv.TENNV;
            birthday.Text = nv.NGAYSINH.ToString();
            sex.Text = nv.GIOITINH;
            phone.Text = nv.SDT;
            address.Text = nv.DIACHI;
            position.Text = nv.VAITRO;
            if (nv.IMG != null)
            {
                Stream StreamObj = new MemoryStream(nv.IMG);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();

                ava.ImageSource = BitObj;
            }
        }

        public UserPage(KHACHHANG kh)
        {
            InitializeComponent();
            KH = kh;
            name.Text = kh.TENKH;
            birthday.Text = kh.NGAYSINH.ToString();
            sex.Text = kh.GIOITINH;
            phone.Text = kh.SDT;
            address.Text = kh.DIACHI;
            position.Text = "Khách hàng - Hạng " + kh.LOAITV.TENLTV;
            if (kh.IMG != null)
            {
                Stream StreamObj = new MemoryStream(kh.IMG);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();

                ava.ImageSource = BitObj;
            }
        }

        private void changePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePassWindow a;
            if (NV != null)
            {
                a = new ChangePassWindow(NV);
                a.ShowDialog();
            }    
            else if(KH != null)
            {
                a = new ChangePassWindow(KH);
                a.ShowDialog();
            }           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            img.Title = "Select a picture";
            img.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";

            if (img.ShowDialog() == true)
            {
                filename = img.FileName;
                ava.ImageSource = new BitmapImage(new Uri(img.FileName));
                CheckAvt = true;
            }
        }

        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            phone.IsEnabled = true;
            address.IsEnabled = true;
            changePassword.Visibility = Visibility.Hidden;
            edit_btn.Visibility = Visibility.Hidden;
            Save_btn.Visibility = Visibility.Visible;
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            if(NV != null)
            {
                NV.DIACHI = address.Text;
                NV.SDT = phone.Text;
                if (CheckAvt)
                {
                    NV.IMG = File.ReadAllBytes(filename);
                }
                DataProvider.Ins.DB.SaveChanges();
            }
            else if(KH != null)
            {
                KH.DIACHI = address.Text;
                KH.SDT = phone.Text;
                if (CheckAvt)
                {
                    KH.IMG = File.ReadAllBytes(filename);
                }
                DataProvider.Ins.DB.SaveChanges();
            }
        }
    }
}
