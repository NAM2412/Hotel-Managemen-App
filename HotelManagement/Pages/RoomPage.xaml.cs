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
    /// Interaction logic for RoomPage.xaml
    /// </summary>
    public partial class RoomPage : Page
    {
        PHONG Phong;
        public RoomPage()
        {
            InitializeComponent();
        }

        private void datagridP_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Phong = (PHONG)datagridP.SelectedItem;
            Windows.AddRoomWindow addRoomWindow = new Windows.AddRoomWindow(Phong);
            addRoomWindow.ShowDialog();
            datagridP.ItemsSource = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs);
        }
    }
}
