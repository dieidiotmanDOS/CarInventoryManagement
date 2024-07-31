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

namespace CarInventoryManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            StartupWindow startup = new StartupWindow();
            this.Visibility = Visibility.Hidden;
            startup.Show();

            string str = @"C:\Users\alidi\Documents\CIM\Users";
            if (!Directory.Exists(str))
            {
                Directory.CreateDirectory(str);
                // Creates the directory if it doesn't already exist.
            }
        }
    }
}