using System.Collections.ObjectModel;
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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Contact> Contacts { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Contacts = new ObservableCollection<Contact>();
            ContactsListView.ItemsSource = Contacts;
        }

        private void addContactButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 dialog = new Window1();
            if (dialog.ShowDialog() == true)
            {
                Contacts.Add(dialog.Contact);
            }
        }
    }
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}