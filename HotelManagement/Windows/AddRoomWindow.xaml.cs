using HotelManagement.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public partial class AddRoomWindow : Window
    {
        PHONG p;
        string filename = null;
        bool CheckIMG = false;

        public AddRoomWindow()
        {
            InitializeComponent();
            edtNameRoom.Text = "";
            edtDayPrice.Text = "0";
            edtNightPrice.Text = "0";
            edtDescription.Text = "";
            cbStatus.Text = "0";
            cbTypeRoom.Text = "";
            SaveRoom_btn.Visibility = Visibility.Hidden;
            AddRoom_btn.Visibility = Visibility.Visible;
            cbStatus.Visibility = Visibility.Hidden;
        }

        public AddRoomWindow(PHONG Phong)
        {
            InitializeComponent();
            p = Phong;
            edtNameRoom.Text = Phong.TENPHONG;
            edtDayPrice.Text = Phong.GIAPHONG_DAY.ToString();
            edtNightPrice.Text = Phong.GIAPHONG_NIGHT.ToString();
            edtDescription.Text = Phong.MOTA;
            cbStatus.Text = Phong.TINHTRANG;
            cbTypeRoom.Text = Phong.LOAIPHONG.TENLOAI;

            PHONG phong = DataProvider.Ins.DB.PHONGs.Where(x => x.MAPHONG == Phong.MAPHONG).First();
            if (phong.IMG != null)
            {
                Stream StreamObj = new MemoryStream(phong.IMG);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();

                imgP.ImageSource = BitObj;
            }

            SaveRoom_btn.Visibility = Visibility.Visible;
            AddRoom_btn.Visibility = Visibility.Hidden;
            cbStatus.Visibility = Visibility.Visible;
            edtNameRoom.IsEnabled = false;
            lbNameRoom.Content = "";
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

        private void btnImgP_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            img.Title = "Select a picture";
            img.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";

            if (img.ShowDialog() == true)
            {
                filename = img.FileName;
                imgP.ImageSource = new BitmapImage(new Uri(img.FileName));
                CheckIMG = true;
            }
        }

        private void AddRoom_btn_Click(object sender, RoutedEventArgs e)
        {
            AddRooms();
            notifier.ShowSuccess("Thêm phòng thành công!");
            this.Close();
        }

        private void SaveRoom_btn_Click(object sender, RoutedEventArgs e)
        {
            UpdateRooms();
            notifier.ShowSuccess("Cập nhật thông tin thành công!");
            this.Close();
        }

        private void AddRooms()
        {
            SqlCommand command;
            SqlConnection connection = new SqlConnection(@"Data Source=QUANGMANH;initial catalog=HOTELMANAGEMENT;integrated security=True");
            if (CheckIMG)
            {
                string QueryCustomer1 = "INSERT INTO PHONG (TENPHONG,TINHTRANG,GIAPHONG_DAY,GIAPHONG_NIGHT,MOTA,MALOAI,IMG) " +
                    "VALUES (@TENPHONG, @TINHTRANG, @GIAPHONG_DAY, @GIAPHONG_NIGHT, @MOTA, @MALOAI, @IMG)";
                connection.Open();
                command = new SqlCommand(QueryCustomer1, connection);
                command.Parameters.Add(new SqlParameter("@TENPHONG", edtNameRoom.Text));
                command.Parameters.Add(new SqlParameter("@TINHTRANG", "Trống"));
                command.Parameters.Add(new SqlParameter("@GIAPHONG_DAY", edtDayPrice.Text));
                command.Parameters.Add(new SqlParameter("@GIAPHONG_NIGHT", edtNightPrice.Text));
                command.Parameters.Add(new SqlParameter("@MOTA", edtDescription.Text));

                LOAIPHONG loai = DataProvider.Ins.DB.LOAIPHONGs.Where(x => x.TENLOAI == cbTypeRoom.Text).First();
                command.Parameters.Add(new SqlParameter("@MALOAI", loai.MALOAI));

                BitmapImage BitObj = (BitmapImage)imgP.ImageSource;
                command.Parameters.Add(new SqlParameter("@IMG", File.ReadAllBytes(filename)));
                command.ExecuteNonQuery();
                CheckIMG = false;
            }
            else
            {
                string QueryCustomer1 = "INSERT INTO PHONG (TENPHONG,TINHTRANG,GIAPHONG_DAY,GIAPHONG_NIGHT,MOTA,MALOAI) " +
                   "VALUES (@TENPHONG, @TINHTRANG, @GIAPHONG_DAY, @GIAPHONG_NIGHT, @MOTA, @MALOAI)";
                connection.Open();
                command = new SqlCommand(QueryCustomer1, connection);
                command.Parameters.Add(new SqlParameter("@TENPHONG", edtNameRoom.Text));
                command.Parameters.Add(new SqlParameter("@TINHTRANG", cbStatus.Text));
                command.Parameters.Add(new SqlParameter("@GIAPHONG_DAY", edtDayPrice.Text));
                command.Parameters.Add(new SqlParameter("@GIAPHONG_NIGHT", edtNightPrice.Text));
                command.Parameters.Add(new SqlParameter("@MOTA", edtDescription.Text));

                LOAIPHONG loai = DataProvider.Ins.DB.LOAIPHONGs.Where(x => x.TENLOAI == cbTypeRoom.Text).First();
                command.Parameters.Add(new SqlParameter("@MALOAI", loai.MALOAI));
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void UpdateRooms()
        {
            PHONG P = DataProvider.Ins.DB.PHONGs.Where(x => x.MAPHONG == p.MAPHONG).SingleOrDefault();
            P.TENPHONG = edtNameRoom.Text;
            P.TINHTRANG = cbStatus.Text;
            P.GIAPHONG_DAY = decimal.Parse(edtDayPrice.Text);
            P.GIAPHONG_NIGHT = decimal.Parse(edtNightPrice.Text);
            P.MOTA = edtDescription.Text;

            LOAIPHONG loai = DataProvider.Ins.DB.LOAIPHONGs.Where(x => x.TENLOAI == cbTypeRoom.Text).First();
            P.MALOAI = loai.MALOAI;
            if (CheckIMG)
            {
                P.IMG = File.ReadAllBytes(filename);
            }
            DataProvider.Ins.DB.SaveChanges();
        }

        private void CancelRoom_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddRoom_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void edtDayPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void edtNightPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
