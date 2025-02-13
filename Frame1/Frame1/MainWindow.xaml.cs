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

namespace Frame1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SharedViewModel SharedViewModel { get; set; } = new SharedViewModel();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page1("Hej, to tekst z Mainframe"));
        }
        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page2(2025));
        }
        //btn3 w ktorym bedziemy przekazywac liste imion
        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            List<string> imiona = new List<string>();
            imiona.Add("Jan");
            imiona.Add("Anna");
            imiona.Add("Piotr");
            imiona.Add("Kasia");
            MainFrame.Navigate(new Page3(imiona));
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            List<string> nazwiska = new List<string>();
            nazwiska.Add("Kowalski");
            nazwiska.Add("Nowak");
            nazwiska.Add("Wiśniewski");
            nazwiska.Add("Dąbrowski");
            MainFrame.Navigate(new Page4(nazwiska));
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            //var page3 i datacontext
            Page5 page5 = new Page5();
            page5.DataContext = SharedViewModel;
            MainFrame.Navigate(page5);
        }
    }
    public class SharedViewModel
    {
        public string wiadomosc { get; set; } = "Hello, to ja z modelu!";
    }
}