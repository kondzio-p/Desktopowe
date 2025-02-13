using Microsoft.Win32;
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
using System.IO;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private int recordCount = 0; // Zmienna do śledzenia liczby zapisanych rekordów

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnZapisz_Click(object sender, RoutedEventArgs e)
        {
            // Pobierz dane
            string imie = txtImie.Text.Trim();
            string nazwisko = txtNazwisko.Text.Trim();
            string email = txtEmail.Text.Trim();

            // Walidacja pól
            if (string.IsNullOrWhiteSpace(imie) || string.IsNullOrWhiteSpace(nazwisko) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Proszę wypełnić wszystkie pola.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Walidacja emaila
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Proszę podać poprawny adres email.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Walidacja imienia i nazwiska
            if (!IsValidName(imie))
            {
                MessageBox.Show("Proszę podać poprawne imię.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidName(nazwisko))
            {
                MessageBox.Show("Proszę podać poprawne nazwisko.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Walidacja wyboru płci
            string plec = "";
            if (dPlecWmn.IsChecked == true)
                plec = "Kobieta";
            else if (dPlecMan.IsChecked == true)
                plec = "Mężczyzna";
            else
            {
                MessageBox.Show("Proszę wybrać płeć.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Walidacja wyboru wieku
            string wiek = "";
            if (dWiek18.IsChecked == true)
                wiek = "Pełnoletni";
            else if (dWiekUnderage.IsChecked == true)
                wiek = "Nieletni";
            else
            {
                MessageBox.Show("Proszę wybrać wiek.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Przygotowanie danych do zapisania
            recordCount++; // Zwiększamy licznik zapisanych rekordów
            string dane = $"{recordCount}. {imie} {nazwisko}, Email: {email}, [{plec}, {wiek}]\n";

            // Użyj SaveFileDialog do zapisania pliku
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Pliki tekstowe (*.txt)|*.txt",
                Title = "Zapisz dane do pliku"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Dodaj dane do pliku
                    File.AppendAllText(saveFileDialog.FileName, dane);
                    MessageBox.Show("Dane zostały zapisane.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas zapisywania pliku: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Walidacja emaila
        private bool IsValidEmail(string email)
        {
            Regex emailRegex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$");
            return emailRegex.IsMatch(email);
        }

        // Walidacja imienia i nazwiska
        private bool IsValidName(string name)
        {
            Regex nameRegex = new Regex(@"^[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż]+$");
            return nameRegex.IsMatch(name);
        }
    }
}
