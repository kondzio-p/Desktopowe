using System.Diagnostics.Eventing.Reader;
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

namespace WpfApp4
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

        public void Nowy(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Kliknięty nowy");
        }
        public void Otworz(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Kliknięty otworz");
        }
        public void Zapisz(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Kliknięty zapisz");
        }
        public void Zakoncz(object sender, RoutedEventArgs e)
        {
            MessageBoxResult odpowiedz = MessageBox.Show("Czy chcesz zakończyć działanie aplikacji?", "Zamykanie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (odpowiedz == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        public void Kopiuj(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Kliknięty kopiuj");
        }
        public void Wklej(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Kliknięty wklej");
        }

        public void Kopiuj2(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtKopiowany.SelectedText))
            {
                Clipboard.SetText(txtKopiowany.Text);
                MessageBox.Show("Skopiowano do schowka");
            }
            else
            {
                MessageBox.Show("Nic do skopiowania. Zaznacz tekst", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void Wklej2(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                txtWklejany.Text = Clipboard.GetText();
                MessageBox.Show("Wklejono tekst...");
            }
            else
            {
                MessageBox.Show("Nic do wklejenia. Skopiuj najpierw tekst...", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}