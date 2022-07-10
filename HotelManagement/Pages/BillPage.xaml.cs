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
    /// Interaction logic for BillPage.xaml
    /// </summary>
    public partial class BillPage : Page
    {
        HOADON HoaDon;
        public BillPage()
        {
            InitializeComponent();
        }

        private void datagridHD_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HoaDon = (HOADON)datagridHD.SelectedItem;

            Windows.InfBillWindow infBillWindow = new Windows.InfBillWindow(HoaDon);
            infBillWindow.ShowDialog();
            datagridHD.ItemsSource = new ObservableCollection<HOADON>(DataProvider.Ins.DB.HOADONs);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Windows.AddBillWindow addBillWindow = new Windows.AddBillWindow();
            addBillWindow.ShowDialog();
        }
    }
}
