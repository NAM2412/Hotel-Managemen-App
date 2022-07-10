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

namespace HotelManagement.Windows
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result=CustomMessageBox.ShowOKCancel("Chúng tôi sẽ gửi một OTP đã được gửi về số điện thoại bạn đã đăng kí", "GỬI MÃ XÁC NHẬN", "ACCEPT", "DECLINE");
            if (result == MessageBoxResult.OK)
            {
                CustomMessageBox.Show("Mã OPT đã được gửi đi!");
                this.Close();
                return;
            }
            else this.Close();
        }
    }
}
