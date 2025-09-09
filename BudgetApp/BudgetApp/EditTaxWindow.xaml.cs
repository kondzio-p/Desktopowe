using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using Newtonsoft.Json;

namespace BudgetApp
{
    public partial class EditTaxWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000/api/") };
        private readonly TaxObligation _tax;

        public EditTaxWindow(TaxObligation tax)
        {
            InitializeComponent();
            _tax = tax;
            TaxName.Text = tax.Name;
            TaxAmount.Text = tax.Amount.ToString();
            TaxDueDate.SelectedDate = tax.DueDate;
        }

        private async void SaveTax_Click(object sender, RoutedEventArgs e)
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
                var response = await _httpClient.PutAsync($"taxes/{_tax.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Podatek zaktualizowany pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Błąd podczas aktualizacji podatku.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd komunikacji z serwerem: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}