using Microsoft.Win32;
using System.IO;
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

namespace wczytajObraz
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
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Wybierz obraz",
                Filter = "Pliki graficzne (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == true) {
            string filePath = openFileDialog.FileName;
            BitmapImage bitmap = new BitmapImage(new Uri(filePath));
                imagePreview.Source = bitmap;

                FileInfo fi = new FileInfo(filePath);
                txtFileName.Text = $"Nazwa pliku: {fi.Name}";
                txtFilePath.Text = $"Ścieżka do pliku: {fi.FullName}";
                txtDimensions.Text = $"Wymiary: {bitmap.PixelWidth} x {bitmap.PixelHeight}px";
                txtFileSize.Text = $"Rozmiar pliku: {fi.Length / 1024.0:F2} KB";
                long fileSize = new FileStream(filePath, FileMode.Open, FileAccess.Read).Length;
                txtFileSize2.Text = $"Rozmiar pliku z bitmapy: {fileSize/1024.0:F2} KB";
            }
        }
    }
}