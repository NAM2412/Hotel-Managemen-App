using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Globalization;

namespace HotelManagement.Pages
{
    /// <summary>
    /// Interaction logic for BookingRoomPage.xaml
    /// </summary>
    public partial class BookingRoomPage : Page
    {
        ObservableCollection<PHONG> phongs;
        KHACHHANG KH;

        public BookingRoomPage(KHACHHANG kh)
        {
            InitializeComponent();
            KH = kh;

            Thread thread = new Thread(delegate ()
            {
                // Get và xác định số trang
                var db = new HOTELMANAGEMENTEntities1();
                int numPages = (int)Math.Ceiling(db.PHONGs.Count() / 10.0);
                if (numPages == 0) numPages = 1;

                // Đưa vào Combobox
                List<string> pageNumber = new List<string>();
                for (int i = 1; i <= numPages; i++)
                {
                    pageNumber.Add(i + "/" + numPages);
                }
                Dispatcher.Invoke(() =>
                {
                    cbPage.ItemsSource = pageNumber;
                    cbPage.SelectedIndex = 0;
                });

                // Lấy danh sách sản phẩm
                phongs = new ObservableCollection<PHONG>(db.PHONGs);
                for (int i = 0; i < phongs.Count; i++)
                {
                    for (int j = i; j < phongs.Count; j++)
                    {
                        if (compare_sort(phongs[i], phongs[j]))
                        {
                            PHONG temp = phongs[i];
                            phongs[i] = phongs[j];
                            phongs[j] = temp;
                        }
                    }
                }
                // Cập nhật UI
                Dispatcher.Invoke(() =>
                {
                    listRoom.ItemsSource = phongs.Skip(cbPage.SelectedIndex * 10).Take(10);
                });
            });
            thread.Start();
        }

        //hàm so sánh để sắp xếp 
        public bool compare_sort(PHONG a, PHONG b)
        {
            int sortType = 0;
            Dispatcher.Invoke(() => { sortType = cbSort.SelectedIndex; });
            switch (sortType)
            {
                case 2: return a.GIAPHONG_DAY > b.GIAPHONG_DAY; // Tăng dần giá cả
                case 3: return a.GIAPHONG_DAY < b.GIAPHONG_DAY; // Giảm dần giá cả
                default: return true;
            }
        }

        public void reset_page(ObservableCollection<PHONG> products)
        {
            List<string> pageNumber = new List<string>();
            int numPages = (int)Math.Ceiling(products.Count() / 10.0);
            if (numPages == 0) numPages = 1;

            for (int i = 1; i <= numPages; i++)
            {
                pageNumber.Add(i + "/" + numPages);
            }
            Dispatcher.Invoke(() =>
            {
                cbPage.ItemsSource = pageNumber;
                cbPage.SelectedIndex = 0;
            });
        }

        #region Xử lý hiệu ứng Comboxbox
        /// <summary>
        /// Hiệu ứng khi chọn
        /// </summary>
        private void ComboProductTypes_DropDownOpened(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            comboBox.Background = Brushes.LightGray;
        }

        /// <summary>
        /// Hiệu ứng khi bỏ chọn
        /// </summary>
        private void ComboProductTypes_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            comboBox.Background = Brushes.Transparent;
        }
        #endregion

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            // Tạo mới danh sách sản phẩm có tên chứa nội dung ô tìm kiếm
            ObservableCollection<PHONG> searchProducts = new ObservableCollection<PHONG>();
            // Nếu ô tìm kiếm rỗng, thì lấy tất cả sản phẩm
            if (tbSearch.Text.Length == 0)
            {
                refresh(true);
            }

            // Nếu ô tìm kiếm có nội dung
            else
            {
                for (int i = 0; i < phongs.Count; i++)
                {
                    if (phongs[i].TENPHONG.ToUpper().Contains(tbSearch.Text.ToUpper())) // Nếu tìm thấy tên phù hợp
                    {
                        searchProducts.Add(phongs[i]); // Thì thêm vào danh sách mới
                    }
                }

                // Nếu tìm thấy ít nhất 1 sản phẩm thì hiển thị, không thì thông báo
                if (searchProducts.Count > 0)
                {
                    reset_page(searchProducts);

                    listRoom.ItemsSource = searchProducts;

                }
                else
                {
                    CustomMessageBox.Show("Không tìm thấy sản phẩm nào", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Làm trống ô tìm kiếm
                tbSearch.Text = "";
            }
        }
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Tạo mới danh sách sản phẩm có tên chứa nội dung ô tìm kiếm
            ObservableCollection<PHONG> searchProducts = new ObservableCollection<PHONG>();
            // Nếu ô tìm kiếm rỗng, thì lấy tất cả sản phẩm
            if (tbSearch.Text.Length == 0)
            {
                refresh(true);
            }

            // Nếu ô tìm kiếm có nội dung
            else
            {
                for (int i = 0; i < phongs.Count; i++)
                {
                    if (phongs[i].TENPHONG.ToUpper().Contains(tbSearch.Text.ToUpper())) // Nếu tìm thấy tên phù hợp
                    {
                        searchProducts.Add(phongs[i]); // Thì thêm vào danh sách mới
                    }
                }

                // Nếu tìm thấy ít nhất 1 sản phẩm thì hiển thị, không thì thông báo
                if (searchProducts.Count > 0)
                {
                    reset_page(searchProducts);
                    listRoom.Visibility = Visibility.Visible;
                    listRoom.ItemsSource = searchProducts;

                }
                else
                {
                    listRoom.Visibility = Visibility.Hidden;
                }
            }
        }
        /// <summary>
        /// Chọn trang bằng Combobox
        /// </summary>
        private void cbPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (phongs != null)
            {
                // Sắp xếp lại
                for (int i = 0; i < phongs.Count; i++)
                {
                    for (int j = i; j < phongs.Count; j++)
                    {
                        if (compare_sort(phongs[i], phongs[j]))
                        {
                            PHONG temp = phongs[i];
                            phongs[i] = phongs[j];
                            phongs[j] = temp;
                        }
                    }
                }

                listRoom.ItemsSource = phongs.Skip(cbPage.SelectedIndex * 10).Take(10);
            }
        }

        //Lọc sản phẩm theo loại
        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Tạo mới danh sách sản phẩm có tên chứa loại sản phẩm cần lọc
            ObservableCollection<PHONG> filterProducts = new ObservableCollection<PHONG>();
            if (cbType.SelectedIndex == -1)
            {
                reset_page(phongs);
                listRoom.ItemsSource = phongs;
            }
            else
            {
                switch (cbType.SelectedIndex)
                {
                    case 0:
                        {
                            for (int i = 0; i < phongs.Count; i++)
                            {
                                if (phongs[i].LOAIPHONG.TENLOAI == "Standard") // Nếu tìm thấy tên phù hợp
                                {
                                    filterProducts.Add(phongs[i]); // Thì thêm vào danh sách mới
                                }
                            }
                            reset_page(filterProducts);

                            listRoom.ItemsSource = filterProducts;
                        }
                        break;
                    case 1:
                        {
                            for (int i = 0; i < phongs.Count; i++)
                            {
                                if (phongs[i].LOAIPHONG.TENLOAI == "Superior") // Nếu tìm thấy tên phù hợp
                                {
                                    filterProducts.Add(phongs[i]); // Thì thêm vào danh sách mới
                                }
                            }
                            reset_page(filterProducts);

                            listRoom.ItemsSource = filterProducts;
                        }
                        break;
                    case 2:
                        {
                            for (int i = 0; i < phongs.Count; i++)
                            {
                                if (phongs[i].LOAIPHONG.TENLOAI == "Deluxe") // Nếu tìm thấy tên phù hợp
                                {
                                    filterProducts.Add(phongs[i]); // Thì thêm vào danh sách mới
                                }
                            }
                            reset_page(filterProducts);

                            listRoom.ItemsSource = filterProducts;
                        }
                        break;
                    case 3:
                        {
                            for (int i = 0; i < phongs.Count; i++)
                            {
                                if (phongs[i].LOAIPHONG.TENLOAI == "Suite") // Nếu tìm thấy tên phù hợp
                                {
                                    filterProducts.Add(phongs[i]); // Thì thêm vào danh sách mới
                                }
                            }
                            reset_page(filterProducts);

                            listRoom.ItemsSource = filterProducts;
                        }
                        break;
                    case 4:
                        {
                            for (int i = 0; i < phongs.Count; i++)
                            {
                                if (phongs[i].LOAIPHONG.TENLOAI == "Family") // Nếu tìm thấy tên phù hợp
                                {
                                    filterProducts.Add(phongs[i]); // Thì thêm vào danh sách mới
                                }
                            }
                            reset_page(filterProducts);

                            listRoom.ItemsSource = filterProducts;
                        }
                        break;
                }
            }
        }     

        //Chuyển trang sp 
        private void btLeft_Click(object sender, RoutedEventArgs e)
        {
            if (phongs != null)
            {
                if (cbPage.SelectedIndex > 0)
                {
                    listRoom.ItemsSource = phongs.Skip(--cbPage.SelectedIndex * 10).Take(10);
                }
            }
        }

        private void btRight_Click(object sender, RoutedEventArgs e)
        {
            if (phongs != null)
            {
                if (cbPage.SelectedIndex < cbPage.Items.Count - 1)
                {
                    listRoom.ItemsSource = phongs.Skip(++cbPage.SelectedIndex * 10).Take(10);
                }
            }
        }

        /// <summary>
        /// Làm mới danh sách sản phẩm (list view)
        /// </summary>
        public void refresh(bool Data)
        {
            if (!Data) return;


            // Nếu lượng sản phẩm thêm vào nhiều và tạo thành trang mới
            int curPage = cbPage.SelectedIndex;
            int newNumPages = (int)Math.Ceiling(phongs.Count / 10.0);
            List<string> pageNumber = new List<string>();
            for (int j = 1; j <= newNumPages; j++)
            {
                pageNumber.Add(j + "/" + newNumPages);
            }
            cbPage.ItemsSource = pageNumber;
            cbPage.SelectedIndex = curPage;

            // Sắp xếp lại
            for (int i = 0; i < phongs.Count; i++)
            {
                for (int j = i; j < phongs.Count; j++)
                {
                    if (compare_sort(phongs[i], phongs[j]))
                    {
                        PHONG temp = phongs[i];
                        phongs[i] = phongs[j];
                        phongs[j] = temp;
                    }
                }
            }
            listRoom.ItemsSource = phongs.Skip(cbPage.SelectedIndex * 10).Take(10);
        }

        private void listRoom_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PHONG Phong = (PHONG)listRoom.SelectedItem;

            Windows.InfRoomWindow infRoomWindow = new Windows.InfRoomWindow(Phong, KH);
            infRoomWindow.ShowDialog();
            listRoom.ItemsSource = new ObservableCollection<PHONG>(DataProvider.Ins.DB.PHONGs);
        }

        /// <summary>
        /// Xem một sản phẩm
        /// </summary>
        //private void listProduct_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    var item = (sender as ListView).SelectedItem as PHONG;
        //    if (item != null)
        //    {
        //        // Nếu người dùng đang muốn Pick sản phẩm để tạo hóa đơn
        //        if (PickProductId != null)
        //        {
        //            PickProductId.Invoke(item);
        //            if (NavigationService.CanGoBack) NavigationService.GoBack();

        //        }
        //        else
        //        {
        //            var detail = new ProductPage(item);
        //            detail.RefreshProductList = refresh;
        //            NavigationService.Navigate(detail);
        //        }
        //    }
        //}
    }
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            double newValue = double.Parse(value.ToString());
            return newValue.ToString("N0");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
