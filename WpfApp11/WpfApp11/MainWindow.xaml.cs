using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace WpfApp11
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

        private void btnRejestruj_Click(object sender, RoutedEventArgs e)
        {
            Regex ptrnEmail = new Regex(@"^[A-Za-z0-9._-]+@[A-Za-z0-9.-]+\.[a-z]{2,}$");

            string imie = txtImie.Text;
            string nazwisko = txtNazwisko.Text;
            string email = txtEmail.Text;

            bool plec = rdKobieta.IsChecked == true;  // True, jeśli wybrano rdKobieta, false jeśli rdMezczyzna

            bool palacy = rdTak.IsChecked == true;  // True, jeśli wybrano rdTak, false jeśli rdNie

            if (!ptrnEmail.IsMatch(email))
            {
                MessageBox.Show("Podaj poprawny email");
                return;  // Jeśli email jest niepoprawny, kończymy dalsze działania
            }

            Window1 okno = new Window1(imie, nazwisko, email, palacy, plec);
            okno.Show();
        }

    }
}