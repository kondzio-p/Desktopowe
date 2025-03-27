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

namespace ApkaLosoweOkregi
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

        private void OnLosujKolkaClick(object sender, RoutedEventArgs e)
        {
            Random losowa = new Random();
            for (int i = 0; i < int.Parse(tbLiczbaKolek.Text); i++)
            {
                Ellipse elipka = new Ellipse();
                elipka.Width = losowa.Next(20, 100);
                elipka.Height = elipka.Width;
                elipka.Fill = new SolidColorBrush(Color.FromRgb((byte)losowa.Next(0, 255), (byte)losowa.Next(0, 255), (byte)losowa.Next(0, 255)));
                /* 
                 * Odejmowanie szerokosci i wysokosci okregu
                 * od wielkosci plotna w losowaniach,
                 * zeby nie bylo wychodzenia za canvas 
                */
                Canvas.SetLeft(elipka, losowa.Next(0, (int)canvas.ActualWidth - (int)elipka.Width)); 
                Canvas.SetTop(elipka, losowa.Next(0, (int)canvas.ActualHeight - (int)elipka.Height));
                canvas.Children.Add(elipka); 
            }
        }
    }
}
