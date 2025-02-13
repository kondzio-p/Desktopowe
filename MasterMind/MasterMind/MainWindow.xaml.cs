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
        private readonly Color[] pinColors =
        {
            Colors.Red,
            Colors.Blue,
            Colors.Green,
            Colors.Yellow,
            Colors.Pink,
            Colors.Orange,
            Colors.Indigo,
            Colors.Purple
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateSecretPins()
        {
            Random random = new Random();
            secretPins = Enumerable.Range(0, 4).Select(_ => pinColors[random.Next(pinColors.Length)]).ToArray();
        }

        private void nowaGra(object sender, RoutedEventArgs e)
        {
            GenerateSecretPins();
            for (int i = 0; i < 4; i++)
            {
                GenerateSecretPins();
            }
        }


    }
}