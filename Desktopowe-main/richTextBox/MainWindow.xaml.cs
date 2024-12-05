using Microsoft.Win32;
using System.IO;
using System.IO.Pipes;
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

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki RTF (*.rtf)|*.rtf|Wszytskie pliki (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
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
                    MessageBox.Show($"Blad w czasie pobierania pliku: {ex.Message}", "Blad pliku", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Pliki RTF (*.rtf)|.rtf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        TextRange txtRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                        txtRange.Save(fileStream, DataFormats.Rtf);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd zapisu pliku", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }

        private void chbItalic_Checked(object sender, RoutedEventArgs e)
        {
            TextSelection selection = richTextBox.Selection;
            if (!selection.IsEmpty)
            {
                selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
        }

        private void chbItalic_Unchecked(object sender, RoutedEventArgs e)
        {
            TextSelection selection = richTextBox.Selection;
            if (!selection.IsEmpty)
            {
                selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            }
        }

        private void chbUnderline_Checked(object sender, RoutedEventArgs e)
        {
            TextSelection selection = richTextBox.Selection;
            if (!selection.IsEmpty)
            {
                selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }

        private void chbUnderline_Unchecked(object sender, RoutedEventArgs e)
        {
            TextSelection selection = richTextBox.Selection;
            if (!selection.IsEmpty)
            {
                selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
        }
    }
}