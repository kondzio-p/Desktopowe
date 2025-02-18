using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

public class LoginRegisterViewModel : INotifyPropertyChanged
{
    private readonly AuthService _authService;
    public SnackbarMessageQueue MessageQueue { get; } = new SnackbarMessageQueue();

    public string LoginUsername { get; set; }
    public string LoginPassword { get; set; }
    public string RegisterUsername { get; set; }
    public string RegisterEmail { get; set; }
    public string RegisterPassword { get; set; }
    public string RegisterCompanyName { get; set; }
    public string RegisterCompanyAddress { get; set; }

    public ICommand LoginCommand => new RelayCommand(async () => await LoginAsync());
    public ICommand RegisterCommand => new RelayCommand(async () => await RegisterAsync());

    public LoginRegisterViewModel()
    {
        _authService = new AuthService();
    }

    public async Task<bool> LoginAsync()
    {
        try
        {
            MessageBox.Show($"Wysyłane dane:\nUsername: {LoginUsername}\nPassword: {LoginPassword}", "Debug WPF");

            var response = await _authService.LoginAsync(LoginUsername, LoginPassword);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var responseData = JsonConvert.DeserializeObject<dynamic>(responseBody);
                string token = responseData.token;
                string userId = responseData.userId;

                MessageQueue.Enqueue("Zalogowano pomyślnie!", true);
                return true;
            }
            else
            {
                MessageBox.Show($"Serwer zwrócił błąd: {response.StatusCode}\n{responseBody}", "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Problem z połączeniem: {ex.Message}", "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }



    private async Task RegisterAsync()
    {
        try
        {
            var response = await _authService.RegisterAsync(
                RegisterUsername,
                RegisterEmail,
                RegisterPassword,
                RegisterCompanyName,
                RegisterCompanyAddress);

            if (response.IsSuccessStatusCode)
            {
                MessageQueue.Enqueue("Konto i firma zostały utworzone!", true);
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageQueue.Enqueue($"Błąd rejestracji: {error}", true);
            }
        }
        catch (HttpRequestException ex)
        {
            MessageQueue.Enqueue($"Problem z połączeniem: {ex.Message}", true);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}