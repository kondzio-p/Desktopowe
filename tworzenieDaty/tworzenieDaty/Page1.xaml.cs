using System;
using System.Windows;
using System.Windows.Controls;

namespace tworzenieDaty
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        // Odbieranie daty i aktualizacja pola tekstowego
        public void OdbierzDate(DateTime date)
        {
            // Odebrana data jest zmieniana o -2 godziny
            DateTime zmienionaDate = date.AddHours(-2);

            // Wrzucenie to pola tekstowego oryginalnej i zmienionej daty
            DateDisplayTextBox.Text = $"Oryginalna: {date:yyyy-MM-dd HH:mm:ss} | -2h: {zmienionaDate:yyyy-MM-dd HH:mm:ss}";
        }
    }
}