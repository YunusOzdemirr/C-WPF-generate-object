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

namespace _271118_NesneÜretSil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rastgele = new Random();

        ObservableCollection<string> Resimler = new ObservableCollection<string>()
        {
            "1.jpg ", "2.jpg ", "6.jpg ", "7.jpg ", "8.jpg ", "9.jpg ", "10.jpg ",
            "11.jpg", "12.jpg ", "13.jpg ", "14.jpg ", "15.jpg", "16.jpg ", "17.jpg ", "19.jpg ",
            "20.jpg ", "21.jpg ", "22.jpg ", "23.jpg ", "24.jpg ", "25.jpg ",
        };

        public MainWindow()
        {
            InitializeComponent();

            #region Combobox'ı doldur.
            //CbSayılar.ItemsSource = new List<int> {4,9,16,25,36,49,64,81,100 };
            CbSayılar.ItemsSource = Enumerable.Range(2, 19).Select(x=>Math.Pow(x,2));

            #endregion

            #region Olaylar
            BtnNesneÜret.Click += BtnNesneÜret_Click;
            this.SizeChanged += MainWindow_SizeChanged;
            #endregion
        }

        #region pencerenin boyut değişim olayı
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GrdLayout.Width = GrdLayout.ActualHeight;

        }
        #endregion


        #region nesne üretme butonu
        private void BtnNesneÜret_Click(object sender, RoutedEventArgs e)
        {
            if (CbSayılar.SelectedItem != null)
            {

                double boyut = (double)CbSayılar.SelectedItem;
                GridSatırSütunOluştur((int)Math.Sqrt(boyut));

                if (RbImage.IsChecked==true)
                {
                    ImageÜret(boyut);
                }
                else if (RbLabel.IsChecked==true)
                {
                    LabelÜret(boyut);
                }
                else
                {
                    RectangleÜret(boyut);
                }
                


              


            }
        }


        #endregion




        #region grid satır sütun oluştur
        private void GridSatırSütunOluştur(int boyut)
        {
            GrdLayout.Width = GrdLayout.ActualHeight; //ActualHeight o anki yüksekliği verir.
            //önceden oluşturulan tanımları ve nesneleri temizle
            GrdLayout.RowDefinitions.Clear();
            GrdLayout.ColumnDefinitions.Clear();
            GrdLayout.Children.Clear();

            for (int i = 0; i < boyut; i++)
            {
                GrdLayout.RowDefinitions.Add(new RowDefinition());
                GrdLayout.ColumnDefinitions.Add(new ColumnDefinition());

            }
        }
        #endregion
        #region Image üret
        private void ImageÜret(double adet)
        {
            for (int satır = 0; satır < GrdLayout.RowDefinitions.Count; satır++)
            {
                for (int sütun = 0; sütun < GrdLayout.ColumnDefinitions.Count; sütun++)
                {
                    Image yImage = new Image()
                    {
                        Source = new BitmapImage(new Uri($"Images/{Resimler[rastgele.Next(Resimler.Count)]}", UriKind.Relative)),
                        Stretch=Stretch.UniformToFill,
                        Cursor = Cursors.Hand
                    };

                    yImage.MouseLeftButtonDown += YImage_MouseLeftButtonDown;
                    GrdLayout.Children.Add(yImage); //oluşturulan nesneyi ekle
                    Grid.SetRow(yImage, satır);
                    Grid.SetColumn(yImage, sütun);

                }
            }
        }


       
        #endregion

        #region label üret
        private void LabelÜret(double adet)
        {
            for (int satır = 0; satır < GrdLayout.RowDefinitions.Count; satır++)
            {
                for (int sütun = 0; sütun < GrdLayout.ColumnDefinitions.Count; sütun++)
                {
                    Label yLabel = new Label()
                    {
                        Background = new SolidColorBrush(Color.FromRgb((byte)rastgele.Next(255), (byte)rastgele.Next(255), (byte)rastgele.Next(255))),
                        Content = $"{satır}{sütun}",
                        FontSize=16,
                        HorizontalContentAlignment=HorizontalAlignment.Center,
                        VerticalContentAlignment=VerticalAlignment.Center,
                        Cursor = Cursors.Hand
                    };
                   
                    GrdLayout.Children.Add(yLabel); //oluşturulan nesneyi ekle
                    Grid.SetRow(yLabel, satır);
                    Grid.SetColumn(yLabel, sütun);

                }
            }
        }

        #endregion
        #region Image mouseleft
        private void YImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image seçilenImage = (Image)sender;
            ImgBüyükResim.Source = seçilenImage.Source;
            ImgBüyükResim.Visibility = Visibility.Visible;

            ImgBüyükResim.MouseLeftButtonDown += ImgBüyükResim_MouseLeftButtonDown;

        }

        private void ImgBüyükResim_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ImgBüyükResim.Visibility = Visibility.Collapsed;

        }
        #endregion

        #region rectangle üret
        private void RectangleÜret(double adet)
        {
            for (int satır = 0; satır < GrdLayout.RowDefinitions.Count; satır++)
            {
                for (int sütun = 0; sütun < GrdLayout.ColumnDefinitions.Count; sütun++)
                {
                    Rectangle yRect = new Rectangle()
                    {
                        Fill = new SolidColorBrush(Color.FromRgb((byte)rastgele.Next(255), (byte)rastgele.Next(255), (byte)rastgele.Next(255))),
                        Cursor = Cursors.Hand
                    };
                    yRect.MouseLeftButtonDown += YRect_MouseLeftButtonDown;
                    GrdLayout.Children.Add(yRect); //oluşturulan nesneyi ekle
                    Grid.SetRow(yRect, satır);
                    Grid.SetColumn(yRect, sütun);

                }
            }
        }

        #endregion

        #region Rectangle MouseLeftBUttonDownOlayı
        private void YRect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle seçiliRect = (Rectangle)sender;
            MessageBox.Show(seçiliRect.Fill.ToString());
        }
        #endregion


        


    }
}
