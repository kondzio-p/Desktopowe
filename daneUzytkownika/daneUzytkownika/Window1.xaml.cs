using System;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace daneUzytkownika
{
    public partial class Window1 : Window
    {
        public Window1(string imie, DateTime dataUr, string jezyki, string kolorOczu, BitmapImage zdjecie)
        {
            InitializeComponent();

            if (zdjecie != null) imgZdjecie.Source = zdjecie;

            txtImieNazwisko.Text = $"Imię i nazwisko: {imie}";
            txtPodsumowanie.Text =
                $"Data urodzenia: {dataUr.ToShortDateString()}\n" +
                $"Kolor oczu: {kolorOczu}\n" +
                $"Języki: {jezyki}";
        }

        // drukowanie z chatu
        private void BtnDrukuj_Click(object sender, RoutedEventArgs e)
        {
            MemoryStream stream = new MemoryStream();
            Package package = Package.Open(stream, FileMode.Create, FileAccess.ReadWrite);

            string pack = "pack://temp.xps";
            PackageStore.AddPackage(new Uri(pack), package);

            XpsDocument xps = new XpsDocument(package, CompressionOption.Maximum, pack);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xps);

            writer.Write(printArea);

            FixedDocumentSequence fds = xps.GetFixedDocumentSequence();

            Window preview = new Window { Title = "Podgląd wydruku", Width = 800, Height = 600 };
            preview.Content = new DocumentViewer { Document = fds };
            preview.Show();

            preview.Closed += (s, a) =>
            {
                xps.Close();
                PackageStore.RemovePackage(new Uri(pack));
                package.Close();
            };
        }
    }
}
