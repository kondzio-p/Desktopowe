using System.Windows;
using System.Windows.Controls;

namespace pokazDane
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void showDane_Click(object sender, RoutedEventArgs e)
        {
            string imie = ImieTextBox.Text;
            string nazwisko = NazwiskoTextBox.Text;
            bool czyPalacy = PalacyTakRadioButton.IsChecked == true;

            Page1 page1 = new Page1(imie, nazwisko, czyPalacy);
            this.Content = page1;

        }

        private void showDaneControl_Click(object sender, RoutedEventArgs e)
        {
            string imie = ImieTextBox.Text;
            string nazwisko = NazwiskoTextBox.Text;
            bool czyPalacy = PalacyTakRadioButton.IsChecked == true;
            
            UserControl1 userControl1 = new UserControl1(imie, nazwisko, czyPalacy);
            contentControl1.Content = userControl1;
        }
    }
}