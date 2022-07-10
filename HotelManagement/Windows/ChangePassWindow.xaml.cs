using HotelManagement.Model;
using System;
using System.Collections.Generic;
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

    public partial class ChangePassWindow : Window
    {
        NHANVIEN NV;
        KHACHHANG KH;
        public ChangePassWindow(NHANVIEN nv)
        {
            InitializeComponent();
            NV = nv;
        }
        public ChangePassWindow(KHACHHANG kh)
        {
            InitializeComponent();
            KH = kh;
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (NV != null)
            {
                NGUOIDUNG ND = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TAIKHOAN == NV.TAIKHOANNV).SingleOrDefault();
                if(ND.MATKHAU != oldPassword.Password)
                {
                    CustomMessageBox.Show("Mật khẩu không đúng, vui lòng nhập lại!", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if(newPassword != passwordVerify)
                {
                    CustomMessageBox.Show("Mật khẩu mới không trùng khớp, vui lòng nhập lại!", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (newPassword.Password == "" || newPassword == null || passwordVerify.Password == "" || passwordVerify == null)
                {
                    MessageBox.Show("Phải nhập đầy đủ thông tin!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }               
                else
                {
                    ND.MATKHAU = newPassword.Password;
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thay đôi mật khẩu thành công", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }                
            }
            else
            {
                NGUOIDUNG ND = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TAIKHOAN == KH.TAIKHOANKH).SingleOrDefault();
                if (ND.MATKHAU != oldPassword.Password)
                {
                    CustomMessageBox.Show("Mật khẩu không đúng, vui lòng nhập lại!", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (newPassword.Password != passwordVerify.Password)
                {
                    CustomMessageBox.Show("Mật khẩu mới không trùng khớp, vui lòng nhập lại!", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (newPassword.Password == "" || newPassword == null || passwordVerify.Password == "" || passwordVerify == null)
                {
                    MessageBox.Show("Phải nhập đầy đủ thông tin!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    ND.MATKHAU = newPassword.Password;
                    DataProvider.Ins.DB.SaveChanges();
                    this.Close();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
