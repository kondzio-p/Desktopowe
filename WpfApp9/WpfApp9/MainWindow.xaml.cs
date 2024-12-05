using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp9
{
    public partial class MainWindow : Window
    {
        // Zestawy znaków do generowania hasła
        private const string MaleLitery = "abcdefghijklmnopqrstuvwxyz";
        private const string WielkieLitery = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Cyfry = "0123456789";
        private const string ZnakiSpecjalne = "!@#$%^&*()-_=+[{]};:'\",<.>/?";

        // Zmienna do przechowywania wygenerowanego hasła i wzorzec do walidacji
        private string wygenerowaneHaslo;
        private static readonly string wzorzecRegex = @"^[A-Z][a-z]*$";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void generujBtn_Click(object sender, RoutedEventArgs e)
        {
            // Parsowanie długości hasła z pola edycyjnego
            if (!int.TryParse(inputIloscZnakow.Text, out int dlugoscHasla) || dlugoscHasla < 4)
            {
                MessageBox.Show("Proszę podać poprawną liczbę znaków (minimum 4).", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Inicjalizacja generatora hasła i zestawów znaków
            StringBuilder budowniczyHasla = new StringBuilder();
            Random random = new Random();

            // Najpierw dodajemy małe litery
            for (int i = 0; i < dlugoscHasla; i++)
            {
                budowniczyHasla.Append(MaleLitery[random.Next(MaleLitery.Length)]);
            }

            // Zastępujemy znaki, jeśli wybrano dodatkowe opcje
            if (smallBigLetters.IsChecked == true)
            {
                // Dodajemy jedną wielką literę na pierwszej pozycji
                budowniczyHasla[0] = WielkieLitery[random.Next(WielkieLitery.Length)];
            }
            if (numbers.IsChecked == true)
            {
                // Dodajemy jedną cyfrę na drugiej pozycji
                budowniczyHasla[1] = Cyfry[random.Next(Cyfry.Length)];
            }
            if (specialCharacters.IsChecked == true)
            {
                // Dodajemy jeden znak specjalny na trzeciej pozycji
                budowniczyHasla[2] = ZnakiSpecjalne[random.Next(ZnakiSpecjalne.Length)];
            }

            // Ustawienie wygenerowanego hasła
            wygenerowaneHaslo = budowniczyHasla.ToString();

            // Wyświetlenie hasła w oknie komunikatu
            MessageBox.Show($"Wygenerowane hasło: {wygenerowaneHaslo}", "Hasło wygenerowane", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void zatwierdzBtn_Click(object sender, RoutedEventArgs e)
        {
            // Pobranie danych pracownika
            string imie = inputImie.Text;
            string nazwisko = inputNazwisko.Text;
            string stanowisko = (inputStanowisko.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Walidacja imienia i nazwiska za pomocą regex
            if (!Regex.IsMatch(imie, wzorzecRegex) || !Regex.IsMatch(nazwisko, wzorzecRegex))
            {
                MessageBox.Show("Niepoprawny format imienia lub nazwiska. Powinny zaczynać się wielką literą, a reszta liter powinna być mała.", "Błąd w danych", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Wyświetlenie danych pracownika oraz hasła
            MessageBox.Show($"Dane pracownika:\nImię: {imie}\nNazwisko: {nazwisko}\nStanowisko: {stanowisko}\nHasło: {wygenerowaneHaslo}", "Informacje o pracowniku", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
