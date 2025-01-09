using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace czasyNaZiemi
{
    public partial class MainWindow : Window
    {
        // Timer do aktualizacji czasu
        private DispatcherTimer? timer;

        // Kolekcja miast z ich czasami lokalnymi
        private ObservableCollection<CityTime>? cityTimes;

        // Słownik przechowujący różnice czasowe (offsety) dla miast
        private Dictionary<string, TimeSpan>? cityOffsets;

        // Ścieżka do pliku CSV z danymi o miastach
        private string csvFilePath;

        // Konstruktor głównego okna aplikacji
        public MainWindow()
        {
            InitializeComponent();
            SetDefaultCsvPath(); // Ustawienie domyślnej ścieżki do pliku CSV
            InitializeData();    // Inicjalizacja danych
            SetupTimer();        // Ustawienie timera
        }

        // Metoda ustawiająca domyślną ścieżkę pliku CSV
        private void SetDefaultCsvPath()
        {
            string userDownloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            csvFilePath = Path.Combine(userDownloadsPath, "czasy.csv");
        }

        // Inicjalizacja danych z pliku CSV
        private void InitializeData()
        {
            cityTimes = new ObservableCollection<CityTime>(); // Kolekcja do przechowywania czasu miast
            cityOffsets = new Dictionary<string, TimeSpan>(); // Słownik z różnicami czasowymi
            CitiesListView.ItemsSource = cityTimes;

            try
            {
                // Sprawdzenie czy plik istnieje
                if (!File.Exists(csvFilePath))
                {
                    MessageBox.Show($"Plik {csvFilePath} nie istnieje.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Odczytanie danych z pliku CSV
                var lines = File.ReadAllLines(csvFilePath, System.Text.Encoding.UTF8);
                foreach (var line in lines.Skip(1)) // Pominięcie nagłówka
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        var city = parts[0].Trim();
                        var offsetString = parts[1].Trim();

                        // Usuwanie znaku '+' lub '-' z początku różnicy czasowej
                        if (offsetString.StartsWith("+") || offsetString.StartsWith("-"))
                        {
                            offsetString = offsetString[1..];
                        }

                        // Parsowanie różnicy czasowej
                        if (TimeSpan.TryParse(offsetString, out TimeSpan offset))
                        {
                            if (parts[1].Trim().StartsWith("-"))
                            {
                                offset = -offset;
                            }

                            cityOffsets[city] = offset;
                        }
                    }
                }

                // Ustawienie źródła danych dla ComboBoxa
                CitiesComboBox.ItemsSource = cityOffsets.Keys.OrderBy(city => city).ToList();
            }
            catch (Exception ex)
            {
                // Obsługa błędów podczas odczytu pliku
                MessageBox.Show($"Błąd odczytu pliku CSV: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Ustawienie timera aktualizującego czas co sekundę
        private void SetupTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // Obsługa zdarzenia Tick timera
        private void Timer_Tick(object? sender, EventArgs e)
        {
            UpdateAllTimes(); // Aktualizacja wszystkich czasów
        }

        // Aktualizacja czasów lokalnych wszystkich miast
        private void UpdateAllTimes()
        {
            if (cityTimes == null || cityOffsets == null) return;

            var utcNow = DateTime.UtcNow;
            foreach (var cityTime in cityTimes)
            {
                var localTime = utcNow.Add(cityOffsets[cityTime.Capital]);
                cityTime.Date = localTime.ToString("yyyy-MM-dd");
                cityTime.Time = localTime.ToString("HH:mm:ss");
            }
        }

        // Obsługa kliknięcia przycisku "Dodaj"
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CitiesComboBox.SelectedItem != null && cityTimes != null)
            {
                var selectedCity = CitiesComboBox.SelectedItem.ToString();
                if (selectedCity != null && !cityTimes.Any(ct => ct.Capital == selectedCity))
                {
                    selectedCity = selectedCity.Replace("+", string.Empty);

                    // Dodanie miasta do listy
                    var cityTime = new CityTime
                    {
                        Capital = selectedCity,
                        Date = string.Empty,
                        Time = string.Empty,
                        ClickTime = DateTime.Now.ToString("HH:mm:ss") // Dodanie czasu kliknięcia
                    };
                    cityTimes.Add(cityTime);
                    UpdateAllTimes(); // Aktualizacja czasów
                }
            }
        }

        // Obsługa zamknięcia okna - zatrzymanie timera
        protected override void OnClosed(EventArgs e)
        {
            timer?.Stop();
            base.OnClosed(e);
        }
    }

    // Klasa reprezentująca miasto i jego czas
    public class CityTime : INotifyPropertyChanged
    {
        private string capital = string.Empty;
        private string date = string.Empty;
        private string time = string.Empty;

        public string Capital
        {
            get => capital;
            set
            {
                if (capital != value)
                {
                    capital = value;
                    OnPropertyChanged(nameof(Capital));
                }
            }
        }

        public string Date
        {
            get => date;
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public string Time
        {
            get => time;
            set
            {
                if (time != value)
                {
                    time = value;
                    OnPropertyChanged(nameof(Time));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        // Powiadamianie o zmianach wartości właściwości
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string clickTime = string.Empty;

        // Czas kliknięcia przez użytkownika
        public string ClickTime
        {
            get => clickTime;
            set
            {
                if (clickTime != value)
                {
                    clickTime = value;
                    OnPropertyChanged(nameof(ClickTime));
                }
            }
        }
    }
}
