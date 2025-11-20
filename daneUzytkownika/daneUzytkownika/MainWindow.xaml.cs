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

namespace daneUzytkownika
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

        private BitmapImage wczytaneZdjecie;

        private void btnZapisz_Click(object sender, RoutedEventArgs e)
        {
            // Walidacja
            if (string.IsNullOrWhiteSpace(txtImie.Text))
            {
                MessageBox.Show("Podaj imie i nazwisko.");
                return;
            }

            if(txtDataUr.SelectedDate == null)
            {
                MessageBox.Show("Wybierz datę urodzenia.");
                return;
            }

            // Pobranie jezykow
            string jezyki = "";

            if (chPolski.IsChecked == true)
            {
                jezyki += "Polski, ";
            }

            if (chNiemiecki.IsChecked == true)
            {
                jezyki += "Niemiecki, ";
            }

            if (chAngielski.IsChecked == true)
            {
                jezyki += "Angielski, ";
            }

            if (chRosyjski.IsChecked == true)
            {
                jezyki += "Rosyjski, ";
            }

            if (jezyki.EndsWith(", "))
            {
                jezyki = jezyki.Substring(0, jezyki.Length - 2);
            }

            if (jezyki == "")
            {
                jezyki = "brak";
            }

            // Kolor oczu
            ComboBoxItem item = (ComboBoxItem)comboKolorOczu.SelectedItem;
            string kolorOczu = item != null ? item.Content.ToString() : "Nie wybrano";

            // wywolanie okna
            Window1 okno = new Window1(
                txtImie.Text,
                txtDataUr.SelectedDate.Value,
                jezyki,
                kolorOczu,
                wczytaneZdjecie
                );

            okno.Show();
        }

        private void btnWczytajZdjecie_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Pliki graficzne|*.jpg;*.jpeg;*.png;*.bmp";

            if (dialog.ShowDialog() == true)
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(dialog.FileName);
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();

                // zapamiętanie obrazu
                wczytaneZdjecie = img;
            }
        }
    }
}