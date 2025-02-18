using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace BudgetApp.Services
{
    public class TransactionService
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "http://localhost:5000/api/";

        public TransactionService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(ApiBaseUrl) };
        }

        public async Task<ObservableCollection<TaxObligation>> GetTaxObligationsAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("taxes");
                return JsonConvert.DeserializeObject<ObservableCollection<TaxObligation>>(response);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Błąd pobierania danych: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return new ObservableCollection<TaxObligation>();
            }
        }


        public async Task<bool> AddTransactionAsync(string name, double amount, DateTime date)
        {
            var data = new
            {
                name,
                amount,
                date
            };

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("transactions/add", content);
            return response.IsSuccessStatusCode;
        }
    }
}
