using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using Newtonsoft.Json;

namespace BudgetApp
{
    public partial class AddTaxWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000/api/") };

        public AddTaxWindow()
        {
            InitializeComponent();
        }

        private async void AddTax_Click(object sender, RoutedEventArgs e)
        {
            string name = TaxName.Text;
            if (!double.TryParse(TaxAmount.Text, out double amount))
            {
                MessageBox.Show("Podaj poprawną kwotę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime dueDate = TaxDueDate.SelectedDate ?? DateTime.Now;

            var data = new { name, amount, dueDate };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("taxes/add", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Podatek dodany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Odśwież listę podatków w głównym oknie
                    if (Owner is MainWindow mainWindow)
                    {
                        await mainWindow.LoadTaxesAsync();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Błąd podczas dodawania podatku.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd komunikacji z serwerem: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}