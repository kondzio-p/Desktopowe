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

namespace ObslugaPlikow
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

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki RTF (*.rtf)|*.rtf|Wszystkie pliki(*.*)|*.*";

            if(openFileDialog.ShowDialog()==true)
            {
                try
                {
                    using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open))
                    {
                        TextRange txtRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                        txtRange.Load(fileStream, DataFormats.Rtf);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd w czasie otwierania pliku: {ex.Message}", "Błąd pliku", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

        }

        private void chbBold_Checked(object sender, RoutedEventArgs e)
        {
            TextSelection selection = richTextBox.Selection;
            if (!selection.IsEmpty)
            {
                selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
        }

        private void chbBold_Unchecked(object sender, RoutedEventArgs e)
        {
            TextSelection selection = richTextBox.Selection;
            if (!selection.IsEmpty)
            {
                selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
        }
    }
}