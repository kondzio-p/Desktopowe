using System.Windows;
using System.Windows.Controls;

namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        private int positionCount = 1; // Zmienna do numeracji pozycji

        public MainWindow()
        {
            InitializeComponent();
        }

        private void addPosition(object sender, RoutedEventArgs e)
        {
            positionCount++;
            var newMenuItem = new MenuItem
            {
                Header = $"Pozycja {positionCount}" 
            };
            newMenuItem.Click += Menu_Click;
            opcjeMenu.Items.Add(newMenuItem); 
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem clMenu = (MenuItem)sender;
            MessageBox.Show("Kliknięto na opcję: " + clMenu.Header);
        }
    }
}
