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
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        NHANVIEN NhanVien;

        public EmployeePage()
        {
            InitializeComponent();
            dtTuNgayNV.Text = "01/01/2010";
            dtDenNgayNV.Text = DateTime.Now.ToString();
        }

        private void datagridNV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NhanVien = (NHANVIEN)datagridNV.SelectedItem;

            Windows.AddEmployeeWindow addEmployeeWindow = new Windows.AddEmployeeWindow(NhanVien);
            addEmployeeWindow.ShowDialog();
            datagridNV.ItemsSource = new ObservableCollection<NHANVIEN>(DataProvider.Ins.DB.NHANVIENs);
        }
    }
}
