using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<TaxObligation> TaxObligations { get; set; }
        private TaxObligation _selectedTax;
        private readonly HttpClient _httpClient;

        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000/api/") };
            DataContext = this;
            LoadTaxObligations();
        }

        private async void LoadTaxObligations()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("taxes");
                TaxObligations = JsonConvert.DeserializeObject<ObservableCollection<TaxObligation>>(response);
                TaxObligationsList.ItemsSource = TaxObligations;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd pobierania podatków: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ✅ Obsługa wyboru elementu z listy podatków
        private void TaxObligationsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TaxObligationsList.SelectedItem != null)
            {
                EditTaxPanel.Visibility = Visibility.Visible;
                _selectedTax = (TaxObligation)TaxObligationsList.SelectedItem;
                EditTaxName.Text = _selectedTax.Name;
                EditTaxAmount.Text = _selectedTax.Amount.ToString();
                EditTaxDueDate.SelectedDate = _selectedTax.DueDate;
            }
        }

        // ✅ Obsługa edycji podatków
        private async void SaveTaxChanges_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedTax == null) return;

            _selectedTax.Name = EditTaxName.Text;
            if (!double.TryParse(EditTaxAmount.Text, out double amount))
            {
                MessageBox.Show("Podaj poprawną kwotę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _selectedTax.Amount = amount;
            _selectedTax.DueDate = EditTaxDueDate.SelectedDate ?? DateTime.Now;

            try
            {
                var json = JsonConvert.SerializeObject(_selectedTax);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"taxes/{_selectedTax.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Dane podatkowe zaktualizowane!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    EditTaxPanel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Błąd aktualizacji podatku.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd komunikacji z serwerem: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ✅ Obsługa przycisku "Dodaj transakcję"
        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funkcja dodawania transakcji w trakcie implementacji.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ✅ Obsługa przycisku "Usuń transakcję"
        private void RemoveTransaction_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funkcja usuwania transakcji w trakcie implementacji.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ✅ Obsługa przycisku "Podatki"
        private void EditTaxes_Click(object sender, RoutedEventArgs e)
        {
            if (TaxObligationsList.SelectedItem == null)
            {
                MessageBox.Show("Wybierz podatek do edycji!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            EditTaxPanel.Visibility = Visibility.Visible;
            _selectedTax = (TaxObligation)TaxObligationsList.SelectedItem;
            EditTaxName.Text = _selectedTax.Name;
            EditTaxAmount.Text = _selectedTax.Amount.ToString();
            EditTaxDueDate.SelectedDate = _selectedTax.DueDate;
        }
    }

    public class TaxObligation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime DueDate { get; set; }
    }
}
