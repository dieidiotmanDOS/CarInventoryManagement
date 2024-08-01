using System.IO;
using System.Windows;

namespace CarInventoryManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            StartupWindow startup = new StartupWindow();

            this.Visibility = Visibility.Hidden;

            startup.Show();
            // Displays the startup window.

            string str = @"C:\Users\alidi\Documents\CIM\Users";

            if (!Directory.Exists(str))
            {
                Directory.CreateDirectory(str);
                // Creates the directory if it doesn't already exist.
            }
        }
    }
}