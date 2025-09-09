using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace obslugaPlikow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class mojeDane
    {
        public string Imie { get; set; }

        public string Nazwisko { get; set; }
        public int Wiek { get; set; }

        public string Ulica { get; set; }

        public string Kod { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Lista z wczytanymi danymi z pliku JSON
        private List<mojeDane> dane;

        // Aktualnie wyświetlany indeks w liście
        private int currentIndex = 0;


        // Wyświetla dane osoby o podanym indeksie z listy
        private void PokazDane(int index)
        {
            if (dane != null && index >= 0 && index < dane.Count)
            {
                tboxImie.Text = dane[index].Imie;
                tboxNazwisko.Text = dane[index].Nazwisko;
                tboxWiek.Text = dane[index].Wiek.ToString();
                tboxUlica.Text = dane[index].Ulica;
                tboxKod.Text = dane[index].Kod;
            }
        }

        // string jsonString = File.ReadAllText("sciezka/do/twojego/pliku.json");

        // Obsługa przycisku "Wczytaj" – otwiera dialog wyboru pliku i ładuje dane
        private void btnWczytaj_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki JSON (*.json|*.json|Wszystkie pliki (*.*)|*.*";
            openFileDialog.Title = "Wybierz plik JSON";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string jsonContent = File.ReadAllText(filePath);

                try
                {
                    dane = JsonSerializer.Deserialize<List<mojeDane>>(jsonContent);

                    if (dane == null  || dane.Count == 0)
                    {
                        MessageBox.Show("Plik nie zawiera rekordów.");
                        return;
                    }

                    // indeks na pierwszy element i wyświetlamy dane
                    currentIndex = 0;
                    PokazDane(currentIndex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd przy wczytywaniu pliku: \n" + ex.Message);
                }
            }
        }

        private void btnZapisz_Click(object sender, RoutedEventArgs e)
        {

        }

        // Obsługa przycisku "Poprzedni"
        private void btnPoprzedni_Click(object sender, RoutedEventArgs e)
        {
            if (dane != null && currentIndex < dane.Count - 1)
            {
                currentIndex++;
                PokazDane(currentIndex);
            }
        }

        // Obsługa przycisku "Następny"
        private void btnNastepny_Click(object sender, RoutedEventArgs e)
        {
            if (dane != null && currentIndex > 0)
            {
                currentIndex--;
                PokazDane(currentIndex);
            }
        }
    }
}