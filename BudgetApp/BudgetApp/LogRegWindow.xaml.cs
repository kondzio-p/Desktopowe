using System;
using System.Threading.Tasks;
using System.Windows;

namespace BudgetApp
{
    public partial class LogRegWindow : Window
    {
        private readonly LoginRegisterViewModel _viewModel;

        public LogRegWindow()
        {
            InitializeComponent();
            _viewModel = new LoginRegisterViewModel();
            DataContext = _viewModel;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LoginPassword = LoginPassword.Password; // Pobranie hasła

            MessageBox.Show($"Login: {_viewModel.LoginUsername}\nPassword: {_viewModel.LoginPassword}", "Debug");

            bool loginSuccess = await _viewModel.LoginAsync();
            if (loginSuccess)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Niepoprawne dane logowania.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobranie danych z pól tekstowych
            string username = RegisterUsername.Text;
            string email = RegisterEmail.Text;
            string password = RegisterPassword.Password;
            string confirmPassword = RegisterConfirmPassword.Password;
            string companyName = RegisterCompanyName.Text;
            string companyAddress = RegisterCompanyAddress.Text;

            // Walidacja danych
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) ||
                string.IsNullOrEmpty(companyName))
            {
                MessageBox.Show("Wypełnij wszystkie wymagane pola.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Hasła nie są identyczne.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Utworzenie instancji AuthService
            AuthService authService = new AuthService();

            try
            {
                // Wysłanie żądania rejestracji do API
                var response = await authService.RegisterAsync(username, email, password, companyName, companyAddress);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Rejestracja zakończona pomyślnie.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Możesz automatycznie przełączyć użytkownika na zakładkę logowania
                    // lub zamknąć okno rejestracji/logowania i otworzyć główne okno aplikacji.
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Błąd rejestracji: {errorMessage}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
