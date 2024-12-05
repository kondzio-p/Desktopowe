using System.Data;
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
using Npgsql;

namespace ApkaBazy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=technikum1";
        public MainWindow()
        {
            InitializeComponent();
            ZaladujUczniow();
        }

        private void ZaladujUczniow()
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM uczen", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            DataTable uczniowieTable = new DataTable();
                            uczniowieTable.Load(reader);
                            dgUczniowie.ItemsSource = uczniowieTable.DefaultView;
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd wczytywania danych z baxy: {ex}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void rejestruj(object sender, RoutedEventArgs e)
        {
            string imie = txtImie.Text;
            string nazwisko = txtNazwisko.Text;
            string plec = (cbPlec.SelectedItem as ComboBoxItem)?.Content.ToString();
            //wez pierwszą litere z stringa plec
            plec = plec.Substring(0, 1);
            DateTime? dataur = dpDataUr.SelectedDate;

            if (string.IsNullOrWhiteSpace(imie) || string.IsNullOrWhiteSpace(nazwisko)
                || string.IsNullOrWhiteSpace(plec) || dataur == null)
            {
                MessageBox.Show("Wszystkie pola muszą być uzupełnione", "Bład", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand("INSERT INTO uczen (imie, nazwisko, plec, dataUr) VALUES (@imie, @nazwisko, @plec, @dataUr)", connection))
                    {
                        cmd.Parameters.AddWithValue("imie", imie);
                        cmd.Parameters.AddWithValue("nazwisko", nazwisko);
                        cmd.Parameters.AddWithValue("plec", plec);
                        cmd.Parameters.AddWithValue("dataUr", dataur);
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                MessageBox.Show("Dodawanie ucznia przebiegło pomyślnie.", "Dodawanie ucznia", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                ZaladujUczniow();
                czyszczenieFormularza();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd dodawania ucznia: {ex.Message}", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void czyszczenieFormularza()
        {
            txtImie.Clear();
            txtNazwisko.Clear();
            cbPlec.SelectedIndex = -1;
            dpDataUr.SelectedDate = null;
        }

        private void szukaj(object sender, RoutedEventArgs e)
        {
            string szukanenazwisko = txtSzukaneNazwisko.Text.Trim(); // Retrieve the surname from the TextBox

            if (string.IsNullOrWhiteSpace(szukanenazwisko))
            {
                MessageBox.Show("Wpisz nazwisko do wyszukania", "Brak danych", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM uczen WHERE nazwisko ILIKE @nazwisko";  // ILIKE for case-insensitive search
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("nazwisko", "%" + szukanenazwisko + "%");

                        using (var reader = cmd.ExecuteReader())
                        {
                            DataTable uczniowieTable = new DataTable();
                            uczniowieTable.Load(reader);
                            dgUczniowie.ItemsSource = uczniowieTable.DefaultView;
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd wyszukiwania ucznia: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}