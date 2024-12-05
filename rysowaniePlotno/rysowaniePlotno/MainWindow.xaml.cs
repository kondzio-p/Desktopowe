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

namespace rysowaniePlotno
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Line linia = new Line
            {
                X1 = 300,
                Y1 = 200,
                X2 = 300,
                Y2 = 350,
                Stroke = Brushes.Brown,
                StrokeThickness = 8
            };

            cvPlotno.Children.Add(linia);

            Ellipse elipsa = new Ellipse
            {
                Width = 200,
                Height = 300,
                Fill = Brushes.White,
                StrokeThickness = 8,
            };

            Canvas.SetLeft(elipsa, 300);
            Canvas.SetTop(elipsa, 200);
            cvPlotno.Children.Add(elipsa);
        }

        private void btnRysuj_Click(object sender, RoutedEventArgs e)
        {
            Rectangle rec = new Rectangle { Width=100, Height=100, Fill = Brushes.Yellow, Stroke=Brushes.Black, StrokeThickness=3
            };
        }

        private void msnDown(object sender, MouseButtonEventArgs e)
        {
            Point punkt = e.GetPosition(cvPlotno);

            Ellipse el = new Ellipse
            {
                Width = 30,
                Height = 30,
                Fill = Brushes.Red,
                Stroke = Brushes.White,
                StrokeThickness = 2
            };
            Canvas.SetLeft(el, punkt.X - 15);
            Canvas.SetTop(el, punkt.Y - 15);
            cvPlotno.Children.Add(el);
        }
        }
}