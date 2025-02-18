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



        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Logika obsługi rejestracji
        }
    }
}
