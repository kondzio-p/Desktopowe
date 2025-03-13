using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace BudgetApp
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000/api/") };

        public ObservableCollection<Transaction> Transactions { get; set; } = new ObservableCollection<Transaction>();
        public ObservableCollection<TaxObligation> Taxes { get; set; } = new ObservableCollection<TaxObligation>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            LoadTransactionsAsync();
            LoadTaxesAsync();
        }

        public async Task LoadTransactionsAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("transactions");
                var transactions = JsonConvert.DeserializeObject<ObservableCollection<Transaction>>(response);
                Transactions.Clear();
                foreach (var transaction in transactions)
                {
                    Transactions.Add(transaction);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd pobierania transakcji: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task LoadTaxesAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("taxes");
                var taxes = JsonConvert.DeserializeObject<ObservableCollection<TaxObligation>>(response);
                Taxes.Clear();
                foreach (var tax in taxes)
                {
                    Taxes.Add(tax);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd pobierania podatków: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            var addTransactionWindow = new AddTransactionWindow
            {
                Owner = this // Ustaw właściciela
            };
            addTransactionWindow.ShowDialog();
        }

        private async void RemoveTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (TransactionsGrid.SelectedItem is Transaction selectedTransaction)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync($"transactions/{selectedTransaction.Id}");
                    if (response.IsSuccessStatusCode)
                    {
                        await LoadTransactionsAsync();
                    }
                    else
                    {
                        MessageBox.Show("Błąd usuwania transakcji.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddTax_Click(object sender, RoutedEventArgs e)
        {
            var addTaxWindow = new AddTaxWindow
            {
                Owner = this // Ustaw właściciela
            };
            addTaxWindow.ShowDialog();
        }

        private async void EditTax_Click(object sender, RoutedEventArgs e)
        {
            if (TaxesGrid.SelectedItem is TaxObligation selectedTax)
            {
                var editTaxWindow = new EditTaxWindow(selectedTax)
                {
                    Owner = this // Ustaw właściciela
                };
                if (editTaxWindow.ShowDialog() == true)
                {
                    await LoadTaxesAsync();
                }
            }
        }

        private async void RemoveTax_Click(object sender, RoutedEventArgs e)
        {
            if (TaxesGrid.SelectedItem is TaxObligation selectedTax)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync($"taxes/{selectedTax.Id}");
                    if (response.IsSuccessStatusCode)
                    {
                        await LoadTaxesAsync();
                    }
                    else
                    {
                        MessageBox.Show("Błąd usuwania podatku.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

    public class Transaction
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class TaxObligation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime DueDate { get; set; }
    }
}