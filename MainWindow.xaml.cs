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

            startup.Show();
            // Displays the startup window.

            string dir1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CIM\Users";
            string dir2 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CIM\Cars";
            string dir3 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CIM\Stats";
            // Location of the two main directories to be made.

            if (!Directory.Exists(dir1)) // Checks if the directory exists.
            {
                Directory.CreateDirectory(dir1);
                // Creates the directory if it doesn't already exist.
            }
            if (!Directory.Exists(dir2)) // Checks if the directory exists.
            {
                Directory.CreateDirectory(dir2);
                // Creates the directory if it doesn't already exist.
            }
            if (!Directory.Exists(dir3)) // Checks if the directory exists.
            {
                Directory.CreateDirectory(dir3);
                // Creates the directory if it doesn#t already exist.
            }

            this.Close();
        }
    }
}