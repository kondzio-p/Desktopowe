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

namespace pickShapeColor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            // Sprawdzenie, czy wybrano kolor i kształt
            if (ColorComboBox.SelectedItem is ComboBoxItem colorItem && ShapeComboBox.SelectedItem is ComboBoxItem shapeItem)
            {
                string colorName = colorItem.Tag.ToString();
                SolidColorBrush brush = (SolidColorBrush)new BrushConverter().ConvertFromString(colorName);

                // Rysowanie wybranego kształtu
                if (shapeItem.Tag.ToString() == "Rectangle")
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Width = 100,
                        Height = 50,
                        Fill = brush,
                        Stroke = Brushes.Black
                    };

                    Canvas.SetLeft(rectangle, 50);
                    Canvas.SetTop(rectangle, 50);
                    RysowanieCanvas.Children.Add(rectangle);
                }
                else if (shapeItem.Tag.ToString() == "Ellipse")
                {
                    Ellipse ellipse = new Ellipse
                    {
                        Width = 100,
                        Height = 50,
                        Fill = brush,
                        Stroke = Brushes.Black
                    };

                    Canvas.SetLeft(ellipse, 200);
                    Canvas.SetTop(ellipse, 50);
                    RysowanieCanvas.Children.Add(ellipse);
                }
            }
        }
    }
}