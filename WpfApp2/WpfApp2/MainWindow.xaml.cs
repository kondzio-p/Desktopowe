using System;
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

namespace WpfApp2
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

        // Metoda obsługująca kliknięcie przycisku "Sprawdź Datę"
        private void btnData_Click(object sender, RoutedEventArgs e)
        {
            string text = txtBoxData.Text;
            string pattern = @"\b\d{2}-\d{2}-\d{4}\b";

            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(text);

            if (matches.Count > 0)
            {
                string result = "Znalezione daty:\n";
                foreach (Match match in matches)
                {
                    result += match.Value + "\n";
                }
                MessageBox.Show(result, "Wynik", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Nie znaleziono żadnych dat.", "Wynik", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        private void btnPesel_Click(object sender, RoutedEventArgs e)
        {
            string textPesel = txtBoxPesel.Text;
            string peselptrn = @"^\d{11}$"; // Wzorzec na PESEL (dokładnie 11 cyfr)

            Regex regexPesel = new Regex(peselptrn);
            bool isValidPesel = regexPesel.IsMatch(textPesel);

            if (isValidPesel)
            {
                MessageBox.Show("PESEL jest poprawny (11 cyfr).", "Wynik", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("PESEL powinien składać się z dokładnie 11 cyfr.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnZnajdzDuze_Click(object sender, RoutedEventArgs e)
        {
            string textDuze = txtBoxDuze.Text;
            string duzePtrn = @"\b[A-Z][a-z]*";

            Regex regex = new Regex(duzePtrn);
            MatchCollection matches = regex.Matches(textDuze);

            if (matches.Count > 0)
            {
                string result = "Znalezione słowa z dużej:\n";
                foreach (Match match in matches)
                {
                    result += match.Value + "\n";
                }
                MessageBox.Show(result, "Wynik", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Nie znaleziono żadnych słów z dużej", "Wynik", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void btnLink_Click(object sender, RoutedEventArgs e)
        {
            string textLink = txtBoxLink.Text;
            string urlPattern = @"(http|https):\/\/[a-zA-Z0-9\.-]+\.[a-zA-Z]{2,}(\/\S*)?";

            Regex regexLink = new Regex(urlPattern);
            bool isValidLink = regexLink.IsMatch(textLink);

            if (isValidLink)
            {
                MessageBox.Show("Link jest poprawny", "Wynik", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Link nie jest poprawny", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }
    }
}
