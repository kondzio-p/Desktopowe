using Microsoft.Win32;
using System.Reflection.Metadata;
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
using System.IO;

    namespace WprowadzanieDanych
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

                private void Button_Click(object sender, RoutedEventArgs e)
                {
                    //imie i nazwisko
                    string imieNazwisko = txtImieNazwisko.Text;
                
                    //klasa
                    ComboBoxItem wybranaKlasa = (ComboBoxItem)cbKlasa.SelectedItem;
                    string klasa = wybranaKlasa != null ? wybranaKlasa.Content.ToString() : "nie wybrano klasy";

                    //hobby
                    var wybraneHobby = new List<string>();
                    foreach (ListBoxItem item in lbHobby.SelectedItems) 
                    {
                    wybraneHobby.Add(item.Content.ToString());
                    }
                    string hobby = wybraneHobby.Count > 0 ? string.Join(", ", wybraneHobby) : "nie wybrano";

                    //przedmioty
                    string ulubionePrzedmioty = "";
                    if (cbPolski.IsChecked == true) ulubionePrzedmioty += "polski, ";
                    if (cbMatematyka.IsChecked == true) ulubionePrzedmioty += "matematyka, ";
                    if (cbChemia.IsChecked == true) ulubionePrzedmioty += "chemia, ";
                    if (cbFizyka.IsChecked == true) ulubionePrzedmioty += "fizyka, ";
                    if (cbGeografia.IsChecked == true) ulubionePrzedmioty += "geografia, ";


                    if (ulubionePrzedmioty == "")
                        ulubionePrzedmioty = "brak";
                    else
                        ulubionePrzedmioty = ulubionePrzedmioty.TrimEnd(',', ' ');

                    //podsumowanie
                    string podsumowanie = $"-----\n" + "Dane ucznia:\n" + $"Imię i nazwisko: {imieNazwisko}\n" + $"Klasa: {klasa}\n" + $"Hobby: {hobby}\n" + $"Ulubione przedmioty: {ulubionePrzedmioty}\n";

                    MessageBox.Show(podsumowanie, "Podsumowanie", MessageBoxButton.OK, MessageBoxImage.Information );

                    //zapis
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "Pliki tekstowe (*.txt)|*.txt";

                    if(dialog.ShowDialog() == true)
                    {
                        string sciezka = dialog.FileName;
                        File.AppendAllText(sciezka, podsumowanie);
                    }
                }
        }
    }