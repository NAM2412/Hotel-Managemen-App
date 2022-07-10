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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace HotelManagement.Windows
{
    public partial class AddEmployeeWindow : Window
    {
        string filename = null;
        bool CheckAvt = false;
        NHANVIEN nv;

        public AddEmployeeWindow()
        {
            InitializeComponent();
            txtTenNV.Text = "";
            txtTaiKhoanNV.Text = "";
            txtNgaySinhNV.Text = DateTime.Now.ToString();
            txtSoCMNDNV.Text = "";
            cbGioiTinhNV.Text = "";
            txtSDTNV.Text = "";
            txtLuongNV.Text = "0";
            txtDiaChiNV.Text = "";
            cbChucVuNV.Text = "";
            txtNgayVaoLamNV.Text = "";
            imgNV.ImageSource = new BitmapImage(new Uri(@"../../Assets/Avatars/UploadImage.png", UriKind.Relative));
            lbNgayVaoLamNV.Visibility = Visibility.Hidden;
            txtNgayVaoLamNV.Visibility = Visibility.Hidden;
            AddEmployee_btn.Visibility = Visibility.Visible;
            SaveEmployee_btn.Visibility = Visibility.Hidden;
        }

        public AddEmployeeWindow(NHANVIEN NhanVien)
        {
            InitializeComponent();
            nv = NhanVien;
            txtTenNV.Text = NhanVien.TENNV;
            txtTaiKhoanNV.Text = NhanVien.TAIKHOANNV;
            txtNgaySinhNV.Text = NhanVien.NGAYSINH.ToString();
            txtSoCMNDNV.Text = NhanVien.CMND;
            cbGioiTinhNV.Text = NhanVien.GIOITINH;
            txtSDTNV.Text = NhanVien.SDT;
            txtLuongNV.Text = NhanVien.LUONG.ToString();
            txtDiaChiNV.Text = NhanVien.DIACHI;
            cbChucVuNV.Text = NhanVien.VAITRO;
            txtNgayVaoLamNV.Text = NhanVien.NGAYVAOLAM.ToString();
            NHANVIEN NV = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == NhanVien.MANV).First();
            if (NhanVien.IMG != null)
            {
                Stream StreamObj = new MemoryStream(NV.IMG);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();

                imgNV.ImageSource = BitObj;
            }

            txtNgaySinhNV.IsEnabled = false;
            txtTaiKhoanNV.IsEnabled = false;
            txtSoCMNDNV.IsEnabled = false;
            txtNgayVaoLamNV.IsEnabled = false;
            AddEmployee_btn.Visibility = Visibility.Hidden;
            SaveEmployee_btn.Visibility = Visibility.Visible;
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

        private void SaveEmployee_btn_Click(object sender, RoutedEventArgs e)
        {
            UpdateEmployees();
            notifier.ShowSuccess("Cập nhật thông tin thành công!");
            this.Close();
        }

        private void AddEmployee_btn_Click(object sender, RoutedEventArgs e)
        {
            AddUser();
            AddEmployees();
            Clear();
            notifier.ShowSuccess("Thêm nhân viên thành công!");

            //notifier.ShowInformation(message);
            //notifier.ShowSuccess(message);
            //notifier.ShowWarning(message);
            //notifier.ShowError(message);

            this.Close();
        }

        private void btnImgNV_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            img.Title = "Select a picture";
            img.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";

            if (img.ShowDialog() == true)
            {
                filename = img.FileName;
                imgNV.ImageSource = new BitmapImage(new Uri(img.FileName));
                CheckAvt = true;
            }
        }

        private void AddUser()
        {
            SqlCommand command;
            SqlConnection connection = new SqlConnection(@"Data Source=QUANGMANH;initial catalog=HOTELMANAGEMENT;integrated security=True");
            string QueryUser = "INSERT INTO NGUOIDUNG (TAIKHOAN, MATKHAU, LOAIND) " +
                "VALUES (@TAIKHOAN, @MATKHAU, @LOAIND)";
            connection.Open();
            command = new SqlCommand(QueryUser, connection);
            command.Parameters.Add(new SqlParameter("@TAIKHOAN", txtTaiKhoanNV.Text));
            command.Parameters.Add(new SqlParameter("@MATKHAU", "12345"));
            command.Parameters.Add(new SqlParameter("@LOAIND", "Nhân viên"));
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void AddEmployees()
        {
            SqlCommand command;
            SqlConnection connection = new SqlConnection(@"Data Source=QUANGMANH;initial catalog=HOTELMANAGEMENT;integrated security=True");
            if (CheckAvt)
            {
                string QueryCustomer1 = "INSERT INTO NHANVIEN (TENNV, TAIKHOANNV, NGAYSINH, CMND, GIOITINH, SDT, DIACHI, NGAYVAOLAM, LUONG, VAITRO, IMG) " +
                    "VALUES (@TENNV, @TAIKHOANNV, @NGAYSINH, @CMND, @GIOITINH, @SDT, @DIACHI, @NGAYVAOLAM, @LUONG, @VAITRO, @IMG)";
                connection.Open();
                command = new SqlCommand(QueryCustomer1, connection);
                command.Parameters.Add(new SqlParameter("@TENNV", txtTenNV.Text));
                command.Parameters.Add(new SqlParameter("@TAIKHOANNV", txtTaiKhoanNV.Text));
                command.Parameters.Add(new SqlParameter("@NGAYSINH", txtNgaySinhNV.Text));
                command.Parameters.Add(new SqlParameter("@CMND", txtSoCMNDNV.Text));
                command.Parameters.Add(new SqlParameter("@GIOITINH", cbGioiTinhNV.Text));
                command.Parameters.Add(new SqlParameter("@SDT", txtSDTNV.Text));
                command.Parameters.Add(new SqlParameter("@DIACHI", txtDiaChiNV.Text));
                command.Parameters.Add(new SqlParameter("@NGAYVAOLAM", DateTime.Now));
                command.Parameters.Add(new SqlParameter("@LUONG", decimal.Parse(txtLuongNV.Text)));
                command.Parameters.Add(new SqlParameter("@VAITRO", cbChucVuNV.Text));

                BitmapImage BitObj = (BitmapImage)imgNV.ImageSource;
                command.Parameters.Add(new SqlParameter("@IMG", File.ReadAllBytes(filename)));
                command.ExecuteNonQuery();
                CheckAvt = false;
            }
            else
            {
                string QueryCustomer1 = "INSERT INTO NHANVIEN (TENNV, TAIKHOANNV, NGAYSINH, CMND, GIOITINH, SDT, DIACHI, NGAYVAOLAM, LUONG, VAITRO, IMG) " +
                     "VALUES (@TENNV, @TAIKHOANNV, @NGAYSINH, @CMND, @GIOITINH, @SDT, @DIACHI, @NGAYVAOLAM, @LUONG, @VAITRO)";
                connection.Open();
                command = new SqlCommand(QueryCustomer1, connection);
                command.Parameters.Add(new SqlParameter("@TENNV", txtTenNV.Text));
                command.Parameters.Add(new SqlParameter("@TAIKHOANNV", txtTaiKhoanNV.Text));
                command.Parameters.Add(new SqlParameter("@NGAYSINH", txtNgaySinhNV.Text));
                command.Parameters.Add(new SqlParameter("@CMND", txtSoCMNDNV.Text));
                command.Parameters.Add(new SqlParameter("@GIOITINH", cbGioiTinhNV.Text));
                command.Parameters.Add(new SqlParameter("@SDT", txtSDTNV.Text));
                command.Parameters.Add(new SqlParameter("@DIACHI", txtDiaChiNV.Text));
                command.Parameters.Add(new SqlParameter("@NGAYVAOLAM", DateTime.Now));
                command.Parameters.Add(new SqlParameter("@LUONG", decimal.Parse(txtLuongNV.Text)));
                command.Parameters.Add(new SqlParameter("@VAITRO", cbChucVuNV.Text));
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        private void UpdateEmployees()
        {
            NHANVIEN NV = DataProvider.Ins.DB.NHANVIENs.Where(x => x.MANV == nv.MANV).SingleOrDefault();
            NV.TENNV = txtTenNV.Text;
            NV.GIOITINH = cbGioiTinhNV.Text;
            NV.DIACHI = txtDiaChiNV.Text;
            NV.SDT = txtSDTNV.Text;
            NV.VAITRO = cbChucVuNV.Text;
            NV.LUONG = decimal.Parse(txtLuongNV.Text);
            if (CheckAvt)
            {
                NV.IMG = File.ReadAllBytes(filename);
            }
            DataProvider.Ins.DB.SaveChanges();           
        }

        private void Clear()
        {
            txtTenNV.Text = "";
            txtTaiKhoanNV.Text = "";
            txtNgaySinhNV.Text = DateTime.Now.ToString();
            txtSoCMNDNV.Text = "";
            cbGioiTinhNV.Text = "";
            txtSDTNV.Text = "";
            txtSoCMNDNV.Text = "";
            txtDiaChiNV.Text = "";
            txtLuongNV.Text = "0";
            cbChucVuNV.Text = "";
            txtNgayVaoLamNV.Text = "";
            imgNV.ImageSource = new BitmapImage(new Uri(@"../../Assets/Avatars/UploadImage.png", UriKind.Relative));          
        }

        private void CancelEmployee_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddEmployee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void txtSoCMNDNV_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtSDTNV_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtLuongNV_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
