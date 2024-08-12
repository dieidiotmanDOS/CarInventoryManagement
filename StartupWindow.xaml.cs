using CarInventoryManagement.Objects;
using CsvHelper;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace CarInventoryManagement
{
    public partial class StartupWindow : Window 
    { 
        // Booleans
        public bool hasSetup = false;
        
        // Reference to this window's instance.
        public static StartupWindow? instance;

        public StartupWindow()
        {
            InitializeComponent();

            instance = this;
            // Declares this window's instance.

            hasSetup = checkSetup();
            // Checks if the system is already setup or not.
        }

        private void ResetAllDataButton_Click(object sender, RoutedEventArgs e)
        {    
            ResetSystemWindow resetWin = new ResetSystemWindow();
            // Gets the other window's object and assigns it to a variable.

            resetWin.ShowDialog();
            // Opens that window as a popup, so that the user cannot open this window twice by switching back to the other one.
        }

        private void SystemSetupButton_Click(object sender, RoutedEventArgs e)
        {
            if (!hasSetup) {

            SetupWindow setupWin = new SetupWindow();
            // Gets the other window's object and assigns it to a variable

            this.Close();
            // Closes this window.

            setupWin.Show();
            // Displays the assigned window.
            } 
            else
            {
                SetupText.Foreground = Brushes.Red;
                // Turns the text red to warn the user about their mistake.

            }
            // A decision is made whether the user has setup the system or not.
        }

        private void SystemLoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (hasSetup)
            {
                LoginWindow loginWin = new LoginWindow();
                // Gets the other window's object and assigns it to a variable

                this.Close();
                // Closes this window.

                loginWin.Show();
                // Displays the assigned window.
            }
            else
            {
                LoginText.Foreground = Brushes.Red;
                // Turns the text red to warn the user about their mistake.
            }
            // A decision is made whether the user has setup the system or not.
        }

        private bool checkSetup()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CIM\Users\userobj.csv";
            // Points to the file location of the saved login data.

            if (File.Exists(filePath)) // Checks if the file exists
            {
                using var reader = new StreamReader(filePath);
                // This is the directory where the csv file is.

                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                // Creates a CsvWriter object.

                var records = csv.GetRecords<UserObject>();
                // Reads all records of the csv into an array.

                try
                {
                    var firstRec = records.First<UserObject>();

                    if (firstRec.userID == "" || firstRec.UserPassword == "" || firstRec.UserTier == 0)
                    {
                        return false;
                    }
                    else { return true; }

                }
                catch { return false; }

                // Using a try-catch, I attempt to read the first record, if it doesn't exist, usually the program crashes.
                // However now it will return a false value as it indicates that no records exist.
            }

            return false;
        }
    }
}
