using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MasterMind
{
    public partial class MainWindow : Window
    {
        // Dostępne kolory dla kul gry
        private readonly Color[] pinColors = {
            Colors.Red, Colors.Green, Colors.Blue, Colors.Yellow,
            Colors.Pink, Colors.Orange, Colors.Indigo, Colors.Purple
        };

        // Kolory dla podpowiedzi (czarne i białe kule)
        private readonly Color[] pinColorsBS = {
            Colors.Black, Colors.White
        };

        private Color[] secretPins; // Tajny kod do odgadnięcia
        private int currentAttempt = 0; // Aktualna liczba prób
        private const string HighScoresFile = "highscores.txt"; // Plik z najlepszymi wynikami

        public MainWindow()
        {
            InitializeComponent();
        }

        // Rozpoczęcie nowej gry
        private void nowaGra(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        // Przygotowanie nowej gry - czyszczenie planszy i generowanie kodu
        private void StartNewGame()
        {
            currentAttempt = 0;
            MainPanel.Children.Clear();
            MainPanel.Children.Add(Sprawdzanie);

            // Pytanie czy kolory mogą się powtarzać
            var odp = MessageBox.Show("Chcesz losować z powtórzeniami?", "Rodzaj gry", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (odp == MessageBoxResult.Yes)
            {
                GenerateSecretPins();
            }
            else
            {
                GenerateSecretPinsBez();
            }
            CreateMasterBoard();
        }

        // Tworzenie planszy gry z 10 rzędami do zgadywania
        private void CreateMasterBoard()
        {
            for (int i = 0; i < 10; i++)
            {
                StackPanel rowPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(5)
                };
                rowPanel.Tag = i;

                // Panel z kulami do zgadywania
                StackPanel mainCirclePanel = new StackPanel { Orientation = Orientation.Horizontal };

                // 4 główne kule w rzędzie
                for (int j = 0; j < 4; j++)
                {
                    mainCirclePanel.Children.Add(drawPins(30, i == 0, true));
                }

                // Panel z podpowiedziami (czarne/białe kule)
                Grid feedbackPanel = new Grid
                {
                    Width = 50,
                    Height = 50,
                    Margin = new Thickness(10, 0, 0, 0)
                };

                feedbackPanel.RowDefinitions.Add(new RowDefinition());
                feedbackPanel.RowDefinitions.Add(new RowDefinition());
                feedbackPanel.ColumnDefinitions.Add(new ColumnDefinition());
                feedbackPanel.ColumnDefinitions.Add(new ColumnDefinition());

                // 4 małe kule podpowiedzi
                for (int x = 0; x < 2; x++)
                {
                    for (int y = 0; y < 2; y++)
                    {
                        Ellipse smallCircle = drawPins(15, false, false);
                        Grid.SetRow(smallCircle, x);
                        Grid.SetColumn(smallCircle, y);
                        feedbackPanel.Children.Add(smallCircle);
                    }
                }

                rowPanel.Children.Add(mainCirclePanel);
                rowPanel.Children.Add(feedbackPanel);
                MainPanel.Children.Add(rowPanel);
            }
        }

        // Otwiera menu wyboru koloru po kliknięciu prawego przycisku
        private void OpenPickerColor(Ellipse circle, Color[] optionColor)
        {
            if (circle == null || !circle.IsEnabled) return;

            ContextMenu colorMenu = new ContextMenu();
            foreach (Color color in optionColor)
            {
                // Mapowanie kolorów na polskie nazwy
                Dictionary<Color, string> colorNames = new Dictionary<Color, string>
                {
                    { Colors.Red, "Czerwony" },
                    { Colors.Green, "Zielony" },
                    { Colors.Blue, "Niebieski" },
                    { Colors.Yellow, "Żółty" },
                    { Colors.Pink, "Różowy" },
                    { Colors.Orange, "Pomarańczowy" },
                    { Colors.Indigo, "Indygo" },
                    { Colors.Purple, "Fioletowy" },
                    { Colors.Black, "Czarny" },
                    { Colors.White, "Biały" }
                };

                string colorName = colorNames.TryGetValue(color, out var name) ? name : color.ToString();

                // Dodanie opcji wyboru koloru
                MenuItem item = new MenuItem
                {
                    Header = colorName,
                    Background = new SolidColorBrush(color),
                    Foreground = GetContrastColor(color)
                };
                item.Click += (s, e) => circle.Fill = new SolidColorBrush(color);
                colorMenu.Items.Add(item);
            }
            circle.ContextMenu = colorMenu;
            colorMenu.IsOpen = true;
        }

        // Wybiera kontrastowy kolor tekstu (czarny/biały) do tła
        private Brush GetContrastColor(Color color)
        {
            double brightness = (color.R * 0.299 + color.G * 0.587 + color.B * 0.114) / 255;
            return brightness > 0.5 ? Brushes.Black : Brushes.White;
        }

        // Rysuje pojedynczą kulę (Ellipse) o podanych parametrach
        private Ellipse drawPins(int size, bool isEnabled, bool isMainCircle)
        {
            Ellipse circle = new Ellipse
            {
                Width = size,
                Height = size,
                Fill = Brushes.Gray,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                Margin = new Thickness(5),
                IsEnabled = isEnabled
            };

            // Przypisanie odpowiedniego menu wyboru koloru
            if (isMainCircle)
            {
                circle.MouseRightButtonDown += (s, e) => OpenPickerColor(s as Ellipse, pinColors);
            }
            else
            {
                circle.MouseRightButtonDown += (s, e) => OpenPickerColor(s as Ellipse, pinColorsBS);
            }
            return circle;
        }

        // Słownik kolorów i ich polskich nazw
        private readonly Dictionary<Color, string> colorNames = new Dictionary<Color, string>
        {
            { Colors.Red, "Czerwony" },
            { Colors.Green, "Zielony" },
            { Colors.Blue, "Niebieski" },
            { Colors.Yellow, "Żółty" },
            { Colors.Pink, "Różowy" },
            { Colors.Orange, "Pomarańczowy" },
            { Colors.Indigo, "Indygo" },
            { Colors.Purple, "Fioletowy" },
            { Colors.Black, "Czarny" },
            { Colors.White, "Biały" }
        };

        // Generuje tajny kod z możliwością powtórzeń kolorów
        private void GenerateSecretPins()
        {
            Random random = new Random();
            secretPins = Enumerable.Range(0, 4)
                .Select(_ => pinColors[random.Next(pinColors.Length)])
                .ToArray();

            string secretMessage = string.Join(", ", secretPins.Select(c => colorNames[c]));
            MessageBox.Show($"Wylosowany kod: {secretMessage}", "Nowa gra", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Generuje tajny kod bez powtórzeń kolorów
        private void GenerateSecretPinsBez()
        {
            Random random = new Random();
            secretPins = pinColors.OrderBy(_ => random.Next(pinColors.Length))
                .Take(4)
                .ToArray();

            string secretMessage = string.Join(", ", secretPins.Select(c => colorNames[c]));
            MessageBox.Show($"Wylosowany kod: {secretMessage}", "Nowa gra", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Sprawdza aktualne zgadywanie i daje podpowiedź
        private void Sprawdzanie_Click(object sender, RoutedEventArgs e)
        {
            currentAttempt++;

            // Znajdź aktywny rząd (pierwszy z włączonymi kulami)
            var activeRow = MainPanel.Children
                .OfType<StackPanel>()
                .FirstOrDefault(row =>
                    ((StackPanel)row.Children[0]).Children
                        .OfType<Ellipse>()
                        .Any(e => e.IsEnabled));

            if (activeRow == null)
            {
                MessageBox.Show("Gra już się zakończyła!");
                return;
            }

            // Pobierz kolory z aktualnego zgadywania
            var mainCircles = ((StackPanel)activeRow.Children[0]).Children
                .OfType<Ellipse>()
                .Select(e => ((SolidColorBrush)e.Fill).Color)
                .ToArray();

            // Sprawdź czy wszystkie kolory są wybrane
            if (mainCircles.Any(c => c == Colors.Gray))
            {
                MessageBox.Show("Wybierz wszystkie kolory przed sprawdzeniem!");
                currentAttempt--; // Nie licz niekompletnej próby
                return;
            }

            // Oblicz podpowiedź (czarne i białe kule)
            var result = CalculateMasterMindResult(secretPins, mainCircles);

            // Aktualizacja panelu z podpowiedziami
            var feedbackPanel = (Grid)activeRow.Children[1];
            var feedbackCircles = feedbackPanel.Children.OfType<Ellipse>().ToList();

            int index = 0;
            // Najpierw czarne kule (dobry kolor i pozycja)
            for (int i = 0; i < result.BlackPins; i++)
            {
                feedbackCircles[index].Fill = new SolidColorBrush(Colors.Black);
                index++;
            }
            // Potem białe kule (tylko dobry kolor)
            for (int i = 0; i < result.WhitePins; i++)
            {
                feedbackCircles[index].Fill = new SolidColorBrush(Colors.White);
                index++;
            }

            // Sprawdź czy wygrana
            if (result.BlackPins == 4)
            {
                ShowVictoryDialog();
                DisableAllRows();
                return;
            }

            // Przejdź do następnego rzędu
            DisableRow(activeRow);
            EnableNextRow(activeRow);
        }

        // Pokazuje okno wygranej i zapisuje wynik
        private void ShowVictoryDialog()
        {
            var inputDialog = new InputDialog("Gratulacje! Wygrałeś w " + currentAttempt + " próbach!\nWpisz swój nick (max 20 znaków):", "Wygrana!", 20);
            if (inputDialog.ShowDialog() == true)
            {
                string nick = inputDialog.Answer;
                if (!string.IsNullOrWhiteSpace(nick))
                {
                    SaveHighScore(nick, currentAttempt);
                }
            }
        }

        // Zapisuje wynik do pliku
        private void SaveHighScore(string nick, int attempts)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(HighScoresFile))
                {
                    sw.WriteLine($"{nick};{attempts};{DateTime.Now:yyyy-MM-dd}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas zapisywania wyniku: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Pokazuje najlepsze wyniki
        private void ShowHighScores_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!File.Exists(HighScoresFile))
                {
                    MessageBox.Show("Brak zapisanych wyników.", "Tabela chwały", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Wczytaj i przetwórz wyniki
                var scores = File.ReadAllLines(HighScoresFile)
                    .Select(line => line.Split(';'))
                    .Where(parts => parts.Length >= 2 && int.TryParse(parts[1], out _))
                    .Select(parts => new
                    {
                        Nick = parts[0],
                        Attempts = int.Parse(parts[1]),
                        Date = parts.Length > 2 ? parts[2] : "Nieznana data"
                    })
                    .OrderBy(x => x.Attempts)  // Sortuj po liczbie prób
                    .ThenBy(x => x.Date)     // Potem po dacie
                    .Take(10)                // Top 10 wyników
                    .ToList();

                if (scores.Count == 0)
                {
                    MessageBox.Show("Brak poprawnych wyników do wyświetlenia.", "Tabela chwały", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Formatuj wyniki do wyświetlenia
                string message = "Najlepsze wyniki:\n\n";
                int position = 1;
                foreach (var score in scores)
                {
                    message += $"{position}. {score.Nick} - {score.Attempts} prób ({score.Date})\n";
                    position++;
                }

                MessageBox.Show(message, "Tabela chwały", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wczytywania wyników: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Oblicza podpowiedź (czarne i białe kule)
        private (int BlackPins, int WhitePins) CalculateMasterMindResult(Color[] secret, Color[] guess)
        {
            int blackPins = 0;
            int whitePins = 0;
            Color[] secretCopy = (Color[])secret.Clone();
            Color[] guessCopy = (Color[])guess.Clone();

            // Najpierw sprawdź dokładne trafienia (czarne kule)
            for (int i = 0; i < 4; i++)
            {
                if (secretCopy[i] == guessCopy[i])
                {
                    blackPins++;
                    secretCopy[i] = Colors.Transparent;
                    guessCopy[i] = Colors.Transparent;
                }
            }

            // Potem sprawdź kolory na złych pozycjach (białe kule)
            for (int i = 0; i < 4; i++)
            {
                if (guessCopy[i] != Colors.Transparent)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (secretCopy[j] == guessCopy[i])
                        {
                            whitePins++;
                            secretCopy[j] = Colors.Transparent;
                            break;
                        }
                    }
                }
            }

            return (blackPins, whitePins);
        }

        // Wyłącza cały rząd po sprawdzeniu
        private void DisableRow(StackPanel row)
        {
            foreach (var ellipse in ((StackPanel)row.Children[0]).Children.OfType<Ellipse>())
            {
                ellipse.IsEnabled = false;
            }
        }

        // Włącza następny rząd do zgadywania
        private void EnableNextRow(StackPanel currentRow)
        {
            int currentIndex = MainPanel.Children.IndexOf(currentRow);
            if (currentIndex < MainPanel.Children.Count - 1)
            {
                var nextRow = (StackPanel)MainPanel.Children[currentIndex + 1];
                foreach (var ellipse in ((StackPanel)nextRow.Children[0]).Children.OfType<Ellipse>())
                {
                    ellipse.IsEnabled = true;
                }
            }
            else
            {
                // Koniec gry - pokaż tajny kod
                string secretMessage = string.Join(", ", secretPins.Select(c => colorNames[c]));
                MessageBox.Show($"Koniec gry! Sekretny kod to: {secretMessage}", "Koniec gry", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Wyłącza wszystkie rzędy (po wygranej)
        private void DisableAllRows()
        {
            foreach (var row in MainPanel.Children.OfType<StackPanel>())
            {
                foreach (var ellipse in ((StackPanel)row.Children[0]).Children.OfType<Ellipse>())
                {
                    ellipse.IsEnabled = false;
                }
            }
        }

        // Resetuje grę do stanu początkowego
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            currentAttempt = 0;
            MainPanel.Children.Clear();
            MainPanel.Children.Add(Sprawdzanie);

            var odp = MessageBox.Show("Czy chcesz losować z powtórzeniami?", "Reset gry",
                                    MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (odp == MessageBoxResult.Yes)
            {
                GenerateSecretPins();
            }
            else
            {
                GenerateSecretPinsBez();
            }

            CreateMasterBoard();
        }

        // Zamyka aplikację
        private void Zamknij_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Pokazuje zasady gry
        private void ShowGameRules_Click(object sender, RoutedEventArgs e)
        {
            string rules = "Zasady gry MasterMind:\n\n" +
                "1. Komputer losuje sekretny kod składający się z 4 kolorów\n" +
                "2. Kolory mogą się powtarzać (w zależności od wybranego trybu)\n" +
                "3. Twoim zadaniem jest odgadnąć sekretny kod w maksymalnie 10 próbach\n" +
                "4. Po każdej próbie otrzymujesz podpowiedź:\n" +
                "   - Czarny pin: poprawny kolor na poprawnej pozycji\n" +
                "   - Biały pin: poprawny kolor, ale na złej pozycji\n" +
                "5. Wygrywasz, gdy odgadniesz wszystkie kolory na właściwych pozycjach";

            MessageBox.Show(rules, "Zasady gry", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    // Okienko do wpisania nicku po wygranej
    public class InputDialog
    {
        public string Answer { get; set; }

        public InputDialog(string question, string title, int maxLength)
        {
            Window window = new Window
            {
                Title = title,
                Width = 350,
                Height = 200,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            StackPanel panel = new StackPanel { Margin = new Thickness(10) };

            panel.Children.Add(new TextBlock
            {
                Text = question,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 10)
            });

            panel.Children.Add(new TextBlock
            {
                Text = $"Data: {DateTime.Now:yyyy-MM-dd}",
                FontStyle = FontStyles.Italic
            });

            TextBox textBox = new TextBox { Margin = new Thickness(0, 10, 0, 0) };
            textBox.MaxLength = maxLength;
            panel.Children.Add(textBox);

            StackPanel buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 20, 0, 0)
            };

            Button okButton = new Button { Content = "OK", Width = 80, Margin = new Thickness(0, 0, 10, 0) };
            okButton.Click += (sender, e) => { Answer = textBox.Text; window.DialogResult = true; };
            buttonPanel.Children.Add(okButton);

            Button cancelButton = new Button { Content = "Pomiń", Width = 80 };
            cancelButton.Click += (sender, e) => { Answer = string.Empty; window.DialogResult = true; };
            buttonPanel.Children.Add(cancelButton);

            panel.Children.Add(buttonPanel);
            window.Content = panel;

            window.ShowDialog();
        }

        public bool ShowDialog()
        {
            return true;
        }
    }
}