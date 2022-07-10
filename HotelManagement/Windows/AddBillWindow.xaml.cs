using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public partial class AddBillWindow : Window
    {
        KHACHHANG KH;
        PHONG P;

        public AddBillWindow()
        {
            InitializeComponent();

        }

        public AddBillWindow(PHONG p, KHACHHANG kh)
        {
            InitializeComponent();
            KH = kh;
            P = p;
            txtMaP.Text = p.MAPHONG;
            txtMaKH.Text = kh.MAKH;
            booking_btn.IsEnabled = true;
        }

        private void AddBill_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CancelBooking_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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



        private void AddPT()
        {
            SqlCommand command;
            SqlConnection connection = new SqlConnection(@"Data Source=QUANGMANH;initial catalog=HOTELMANAGEMENT;integrated security=True");
            string QueryCustomer1 = "INSERT INTO PHIEUTHUE (MAPHIEU, MANV, MAKH, NGAYBATDAU, NGAYTRAPHONG, NGAYTHUE, TRIGIA) " +
                    "VALUES (@MAPHIEU, @MANV, @MAKH, @NGAYBATDAU, @NGAYTRAPHONG, @NGAYTHUE, 15000000)";
            connection.Open();
            command = new SqlCommand(QueryCustomer1, connection);
            command.Parameters.Add(new SqlParameter("@MAPHIEU", txtMaPT.Text));
            command.Parameters.Add(new SqlParameter("@MANV", "NV01"));

            command.Parameters.Add(new SqlParameter("@MAKH", KH.MAKH));
            command.Parameters.Add(new SqlParameter("@NGAYBATDAU", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@NGAYTRAPHONG", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@NGAYTHUE", DateTime.Now));

            PHONG P = DataProvider.Ins.DB.PHONGs.Where(x => x.MAPHONG == txtMaP.Text).SingleOrDefault();
            if(cbLoaiThue.Text == "Theo ngày")
            {
                command.Parameters.Add(new SqlParameter("@TRIGIA", decimal.Parse(P.GIAPHONG_DAY.ToString())));
            }
            else if(cbLoaiThue.Text == "Theo ngày")
            {
                command.Parameters.Add(new SqlParameter("@TRIGIA", decimal.Parse(P.GIAPHONG_NIGHT.ToString())));
            }
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void AddCTPT()
        {
            SqlCommand command;
            SqlConnection connection = new SqlConnection(@"Data Source=QUANGMANH;initial catalog=HOTELMANAGEMENT;integrated security=True");
            string QueryCustomer1 = "INSERT INTO CHITIETPHIEUTHUE (MAPHIEU, MAPHONG, DONGIA, LOAITHUE) " +
                    "VALUES (@MAPHIEU, @MAPHONG, 15000000, @LOAITHUE)";
            connection.Open();
            command = new SqlCommand(QueryCustomer1, connection);           

            command.Parameters.Add(new SqlParameter("@MAPHIEU", txtMaPT.Text));           
            command.Parameters.Add(new SqlParameter("@MAPHONG", txtMaP.Text));
            command.Parameters.Add(new SqlParameter("@LOAITHUE", cbLoaiThue.Text));

            PHONG P = DataProvider.Ins.DB.PHONGs.Where(x => x.MAPHONG == txtMaP.Text).SingleOrDefault();
            if (cbLoaiThue.Text == "Theo ngày")
            {
                command.Parameters.Add(new SqlParameter("15000000", decimal.Parse(P.GIAPHONG_DAY.ToString())));
            }
            else if (cbLoaiThue.Text == "Theo ngày")
            {
                command.Parameters.Add(new SqlParameter("15000000", decimal.Parse(P.GIAPHONG_NIGHT.ToString())));
            }
          
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void AddHD()
        {
            SqlCommand command;
            SqlConnection connection = new SqlConnection(@"Data Source=QUANGMANH;initial catalog=HOTELMANAGEMENT;integrated security=True");
            string QueryCustomer1 = "INSERT INTO HOADON (MAHD, MAPHIEU, NGAYHOADON, KHUYENMAI, SOTIENPHAITRA, TINHTRANG) " +
                    "VALUES (@MAHD, @MAPHIEU, @NGAYHOADON, @KHUYENMAI, @SOTIENPHAITRA, @TINHTRANG)";
            connection.Open();
            command = new SqlCommand(QueryCustomer1, connection);
         
            PHIEUTHUE PT = DataProvider.Ins.DB.PHIEUTHUEs.Where(x => x.MAPHIEU == txtMaPT.Text).SingleOrDefault();
            command.Parameters.Add(new SqlParameter("@MAHD", "HD37"));
            command.Parameters.Add(new SqlParameter("@MAPHIEU", txtMaPT.Text));
            command.Parameters.Add(new SqlParameter("@NGAYHOADON", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@KHUYENMAI", (decimal)((int)PT.TRIGIA * KH.LOAITV.KHUYENMAI)));
            command.Parameters.Add(new SqlParameter("@SOTIENPHAITRA", (decimal)((int)PT.TRIGIA * (1 - KH.LOAITV.KHUYENMAI))));
            command.Parameters.Add(new SqlParameter("@TINHTRANG", "Chưa thanh toán"));          
            command.ExecuteNonQuery();
            connection.Close();
        }

        

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PHIEUTHUE PT = DataProvider.Ins.DB.PHIEUTHUEs.Where(x => x.MAPHIEU == txtMaPT.Text).SingleOrDefault();
            PrintBillWindow printBillWindow = new PrintBillWindow(PT);
            printBillWindow.ShowDialog();
        }

        private void booking_btn_Click(object sender, RoutedEventArgs e)
        {
            AddPT();
            AddCTPT();
            AddHD();
            CustomMessageBox.Show("Thêm hóa đơn thành công!", "THÔNG BÁO!", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
