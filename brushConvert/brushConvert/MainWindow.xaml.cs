using Microsoft.VisualBasic;
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

namespace brushConvert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DodajFigure("Koło");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            DodajFigure("Kwadrat");
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            DodajFigure("Prostokąt");
        }


        private void DodajFigure(string typFigury)
        {
            string inputSzer = Interaction.InputBox("Podaj szerokość (max 300): ", typFigury, "100");

            string inputWys = (typFigury == "Koło" || typFigury == "Kwadrat") ? inputSzer : Interaction.InputBox("Podaj wysokość (max 300):", typFigury, "100");

            if(!int.TryParse(inputSzer, out int szer) || szer <=0 || szer > 300)
            {
                MessageBox.Show("Wprowadzona szerokość jest błędna", "Błąd szerokości", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(inputSzer, out int wys) || wys <= 0 || wys > 300)
            {
                MessageBox.Show("Wprowadzona wysokość jest błędna", "Błąd wysokości", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string kolorText = Interaction.InputBox("Podaj kolor (np. Red, Blue, #FF000): ", "Kolor", "Red");

            SolidColorBrush brush;
            try
            {
                brush = (SolidColorBrush) new BrushConverter().ConvertFromString(kolorText);
            } catch
            {
                MessageBox.Show("Nieprawidłowy kolor...");
                brush = Brushes.Black;
            }

            Shape figure;
            if (typFigury == "Koło")
            {
                figure = new Ellipse()
                {
                    Width = szer,
                    Height = szer,
                };
            }
            else if (typFigury == "Kwadrat")
            {
                figure = new Rectangle()
                {
                    Width = szer,
                    Height = szer,
                };
            }
            else 
            {
                figure = new Rectangle()
                {
                    Width = szer,
                    Height = wys,
                };
                
            } 

            figure.Fill = brush;

            var rand = new Random();
            double x = rand.Next(0, (int)Math.Max(9, canvasFigury.ActualHeight - figure.Width));
            double y = rand.Next(0, (int)Math.Max(9, canvasFigury.ActualHeight - figure.Width));

            Canvas.SetLeft(figure, x);
            Canvas.SetTop(figure, y);

            canvasFigury.Children.Add(figure);
            OpisTextBlock.Text = $"{typFigury} - Rozmiar: {szer}x{wys}, Kolor: {kolorText.ToUpper()}";

        }
    }
}