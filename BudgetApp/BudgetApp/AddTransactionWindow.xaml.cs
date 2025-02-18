using System;
using System.Windows;

namespace BudgetApp
{
    public partial class AddTransactionWindow : Window
    {
        public AddTransactionWindow()
        {
            InitializeComponent();
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            string name = TransactionName.Text;
            if (!double.TryParse(TransactionAmount.Text, out double amount))
            {
                MessageBox.Show("Podaj poprawną kwotę!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime date = TransactionDate.SelectedDate ?? DateTime.Now;

            // Możesz dodać kod do wysyłania danych do API

            MessageBox.Show($"Dodano transakcję: {name} na kwotę {amount} PLN!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
