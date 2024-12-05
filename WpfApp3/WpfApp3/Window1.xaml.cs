using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Contact Contact { get; private set; }

        public Window1()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Contact = new Contact
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                Email = EmailTextBox.Text,
            };

            string ptrnEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regexEmail = new Regex(ptrnEmail);

            string ptrnFirstName = @"^[A-Z][a-zA-Zàáâäèéêëìíôöùúûüńłżźć\s'-]{1,49}$";
            Regex regexFirstName = new Regex(ptrnFirstName);

            string ptrnLastName = @"^[A-Z][a-zA-Zàáâäèéêëìíôöùúûüńłżźć\s'-]{1,99}$";
            Regex regexLastName = new Regex(ptrnLastName);

            bool emailValid = regexEmail.IsMatch(Contact.Email);
            bool firstNameValid = regexFirstName.IsMatch(Contact.FirstName);
            bool lastNameValid = regexLastName.IsMatch(Contact.LastName);

            if (emailValid && firstNameValid && lastNameValid)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Jedno lub więcej pól jest niepoprawnych...");
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

    }
}
