using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using Newtonsoft.Json;

namespace BudgetApp
{
    public partial class AddTransactionWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000/api/") };

        public AddTransactionWindow()
        {
            InitializeComponent();
        }

        private async void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            string name = TransactionName.Text;
            if (!double.TryParse(TransactionAmount.Text, out double amount))
            {
                MessageBox.Show("Podaj poprawną kwotę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime date = TransactionDate.SelectedDate ?? DateTime.Now;

            var data = new { name, amount, date };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("transactions/add", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Transakcja dodana pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Odśwież listę transakcji w głównym oknie
                    if (Owner is MainWindow mainWindow)
                    {
                        await mainWindow.LoadTransactionsAsync();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Błąd podczas dodawania transakcji.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd komunikacji z serwerem: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}