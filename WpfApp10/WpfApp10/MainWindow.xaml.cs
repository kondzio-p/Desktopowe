    using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WpfApp10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Zdarzenie click na przycisku OK
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string imie = txtImie.Text;
            string nazwisko = txtNazwisko.Text;
            string kolor = "";

            if (rdNiebieskie.IsChecked == true)
            {
                kolor = "Niebieski";
            }
            if (rdZielone.IsChecked == true)
            {
                kolor = "Zielony";
            }
            if (rdPiwne.IsChecked == true)
            {
                kolor = "piwne";
            }

            //Sprwadzenie czy użytkownik podał dane
            if (String.IsNullOrEmpty(imie) || String.IsNullOrEmpty(nazwisko))
            {
                MessageBox.Show("Proszę wypełnić wszystkie pola!");
                return;
            }

            MessageBox.Show(imie + " " + nazwisko + " " + kolor);
            
            
        }


            private void txtNumer_LostFocus(object sender, RoutedEventArgs e)
            {
            //Pobranie numeru z pola edycyjnego
            string numer = txtNumer.Text;

            //Utworzenie ścieżki pliku
            string source = "C:\\Users\\t4\\Documents\\kody\\Desktopowe\\WpfApp10\\media\\";
            string odcisk = source + numer + "-zdjecie.jpg";
            string zdjecie = source + numer + "-odcisk.jpg";

            //Czy ścieżki plików istnieją?
            if (!File.Exists(zdjecie) || !File.Exists(odcisk))
            {
                imgZdjecie.Source = null;
                imgOdcisk.Source = null;
                return;
            }

            //Utworzenie obiektu ścieżki zdjęcia
            BitmapImage zdjecie1 = new BitmapImage();
            zdjecie1.BeginInit();
            zdjecie1.UriSource = new Uri(zdjecie, UriKind.RelativeOrAbsolute);
            zdjecie1.EndInit();

            //Utworzenie obiektu ścieżki odcisku
            BitmapImage odcisk1 = new BitmapImage();
            odcisk1.BeginInit();
            odcisk1.UriSource = new Uri(odcisk, UriKind.RelativeOrAbsolute);
            odcisk1.EndInit();

            //Ustawienie ścieżki pliku dla zdjęcia i odcisku
            imgZdjecie.Source = zdjecie1;
            imgOdcisk.Source = odcisk1;
        }

        private void chckProporcje_Checked(object sender, RoutedEventArgs e)
        {
            imgZdjecie.Stretch = Stretch.Uniform;
            imgOdcisk.Stretch = Stretch.Uniform;
        }

        private void chckProporcje_UnChecked(object sender, RoutedEventArgs e)
        {
            imgZdjecie.Stretch = Stretch.Fill;
            imgOdcisk.Stretch = Stretch.Fill;
        }

        private void chckPrzezroczystosc_Checked(object sender, RoutedEventArgs e)
        {
            imgZdjecie.Opacity = 0.5;
            imgOdcisk.Opacity = 0.5;
        }

        private void chckPrzezroczystosc_UnChecked(object sender, RoutedEventArgs e)
        {
            imgZdjecie.Opacity = 1;
            imgOdcisk.Opacity = 1;
        }
    }
}