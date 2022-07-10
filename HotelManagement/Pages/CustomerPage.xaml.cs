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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManagement.Pages
{
    /// <summary>
    /// Interaction logic for CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        KHACHHANG KhachHang;
        public CustomerPage()
        {
            InitializeComponent();
        }

        private void datagridKH_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            KhachHang = (KHACHHANG)datagridKH.SelectedItem;

            Windows.InfCustomerWindow infCustomerWindow = new Windows.InfCustomerWindow(KhachHang);
            infCustomerWindow.ShowDialog();
            datagridKH.ItemsSource = new ObservableCollection<KHACHHANG>(DataProvider.Ins.DB.KHACHHANGs);
        }
    }
}
