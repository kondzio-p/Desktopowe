using System;
using System.Windows;
using System.Windows.Controls;

namespace tworzenieDaty
{
    public partial class MainWindow : Window
    {
        private DateTime generatedDate; // Przechowuje wygenerowaną datę

        public MainWindow()
        {
            InitializeComponent();
            ContentFrame.Content = new Page1(); // Ustawienie domyślnej strony w ramce
        }

        private void GenerateDateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Pobranie danych wejściowych z pól tekstowych i konwersja na liczby całkowite
                int day = int.Parse(DayTextBox.Text);
                int month = int.Parse(MonthTextBox.Text);
                int year = int.Parse(YearTextBox.Text);

                // Nowy obiekt DateTime z podanymi wartościami
                generatedDate = new DateTime(year, month, day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                // Komunikat z wygenerowaną datą
                MessageBox.Show("Utworzona data: " + generatedDate.ToString("yyyy-MM-dd HH:mm:ss"), "Jest, udało się", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // Obsługa błędu gdy niepoprawne dane
                MessageBox.Show("Niepoprawne dane!\n" + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SendDateButton_Click(object sender, RoutedEventArgs e)
        {
            // Czy data została wcześniej wygenerowana
            if (generatedDate == DateTime.MinValue)
            {
                MessageBox.Show("Najpierw utwórz datę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Pobranie strony Page1 z ContentFrame - rzutowanie
            Page1 page = ContentFrame.Content as Page1;
            if (page != null)
            {
                // Przekazanie wygenerowanej daty do OdbierzDate w Page1
                page.OdbierzDate(generatedDate);
            }
        }
    }
}
