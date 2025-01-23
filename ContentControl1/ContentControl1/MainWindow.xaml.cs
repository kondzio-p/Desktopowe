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

namespace ContentControl1
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

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UserControl1("Wysłany z MainWindow");
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            var userControl2 = new UserControl2 { info = "Wysłany z MainWindow do 2 okna" };
            MainContent.Content = userControl2;
        }
    }
}