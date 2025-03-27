using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasterMind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Color[] pinColors = {
            Colors.Red, Colors.Green, Colors.Blue, Colors.Yellow,
            Colors.Pink, Colors.Orange, Colors.Indigo, Colors.Purple
        };
        private readonly Color[] pinColorsBS = {
            Colors.Black, Colors.White
        };
        private Color[] secretPins;

        public MainWindow()
        {
            InitializeComponent();

        }
        private void nowaGra(object sender, RoutedEventArgs e)
        {
            var odp = MessageBox.Show("Chcezsz losować z powtórzeniami?", "Rodzaj gry", MessageBoxButton.YesNo, MessageBoxImage.Question);
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

        // tworzenie okna gry
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

                StackPanel mainCirclePanel = new StackPanel { Orientation = Orientation.Horizontal };

                for (int j = 0; j < 4; j++)
                {
                    mainCirclePanel.Children.Add(drawPins(30, i == 0, true));
                }

                // TO DO: piny 2x2 
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
                // dodawanie elementów do boarda
                rowPanel.Children.Add(mainCirclePanel);
                rowPanel.Children.Add(feedbackPanel);
                MainPanel.Children.Add(rowPanel);
            }
        }

        //picker z kolorami z menu podręcznego

        private void OpenPickerColor(Ellipse circle, Color[] optionColor)
        {

            if (circle == null || !circle.IsEnabled) return;

            ContextMenu colorMenu = new ContextMenu();
            foreach (Color color in optionColor)
            {
                MenuItem item = new MenuItem
                {
                    Header = color.ToString(),
                    Background = new SolidColorBrush(color)
                };
                item.Click += (s, e) => circle.Fill = new SolidColorBrush(color);
                colorMenu.Items.Add(item);
            }
            circle.ContextMenu = colorMenu;
            colorMenu.IsOpen = true;

        }
        // rysowanie pinów
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

        // Generowanie nowych pinów do gry z powtórzeniami
        private void GenerateSecretPins()
        {
            Random random = new Random();
            secretPins = Enumerable.Range(0, 4).Select(_ => pinColors[random.Next(pinColors.Length)]).ToArray();
            MessageBox.Show(string.Join(" ", secretPins));
        }

        // Generowanie nowych pinów do gry bez powtórzeń
        private void GenerateSecretPinsBez()
        {
            Random random = new Random();
            secretPins = pinColors.OrderBy(_ => random.Next(pinColors.Length)).Take(4).ToArray();
            MessageBox.Show(string.Join(" ", secretPins));
        }


        private void Sprawdzanie_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}