using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContentControl1
{
    /// <summary>
    /// Logika interakcji dla klasy UserControl2.xaml
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        public static readonly DependencyProperty MessageProperty 
            = DependencyProperty.Register("info", typeof(string), typeof(UserControl2), new PropertyMetadata(""));

        public string info {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }
        public UserControl2()
        {
            InitializeComponent();
        }
    }
}
