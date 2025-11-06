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

namespace ksztaltKolorWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string wybranyKolor = "";
        private string wybranyKsztalt = "";
        private string wybranyKolorPL = "";
        private string wybranyKsztaltPL = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void lbKsztalty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbKsztalty.SelectedItem is ListBoxItem item)
            {
                wybranyKsztalt = item.Tag.ToString();
                wybranyKsztaltPL = item.Content.ToString();
                AktualizujWidok();
            }
        }

        private void cbKolor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbKolor.SelectedItem is ComboBoxItem item)
            {
                wybranyKolor = item.Tag.ToString();
                wybranyKolorPL = item.Content.ToString();
                AktualizujWidok();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            AktualizujWidok();
        }

        private void AktualizujWidok()
        {
            mojCanvas.Children.Clear();
            if (string.IsNullOrEmpty(wybranyKolor) || string.IsNullOrEmpty(wybranyKsztalt))
            {
                txtAktualnyWybor.Text = "Brak wyboru";
                return;
            }
            else
            {
                txtAktualnyWybor.Text = "Kolor: " + wybranyKolorPL + ", figura: " + wybranyKsztaltPL;
            }

            Brush kolor = Brushes.Black;
            if (wybranyKolor == "Red") kolor = Brushes.Red;
            else if (wybranyKolor == "Green") kolor = Brushes.Green;
            else if (wybranyKolor == "Blue") kolor = Brushes.Blue;

            double szer = sliderSzerokosc.Value;
            double wys = sliderWysokosc.Value;

            Shape figura = null;

            if (wybranyKsztalt == "Circle")
            {
                figura = new Ellipse()
                {
                    Width = szer,
                    Height = wys,
                    Fill = kolor
                };
            }
            else if (wybranyKsztalt == "Square")
            {
                figura = new Rectangle()
                {
                    Width = szer,
                    Height = wys, 
                    Fill = kolor
                };
            }
            else if (wybranyKsztalt == "Rectangle")
            {
                figura = new Rectangle()
                {
                    Width = szer,
                    Height = wys, 
                    Fill = kolor
                };
            }

            if (figura != null)
            {
                double x = (mojCanvas.ActualWidth - figura.Width) / 2;
                double y = (mojCanvas.ActualHeight - figura.Height) / 2;

                Canvas.SetLeft(figura, x);
                Canvas.SetTop(figura, y);
                mojCanvas.Children.Add(figura);
            }
        }
    }
}