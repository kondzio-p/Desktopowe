using System;
using System.Windows;

namespace WpfApp11
{
    public partial class Window1 : Window
    {
        public Window1(string imie, string nazwisko, string email, bool palacy, bool plec)
        {
            InitializeComponent();

            // Wyświetlanie podstawowych danych
            txtWyswietl.Text = $"\n{imie} {nazwisko} {email}";

            // Wyświetlanie charakterystyki (palący/niepalący)
            if (palacy)
            {
                txtCharakterystyka.Text += "Palacz";
            }
            else
            {
                txtCharakterystyka.Text += "Nie palacz";
            }

            // Wyświetlanie płci (Kobieta/Mężczyzna)
            if (plec)
            {
                txtCharakterystyka.Text += " - Kobieta";
            }
            else
            {
                txtCharakterystyka.Text += " - Mężczyzna";
            }
        }
    }
}
