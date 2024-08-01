using CarInventoryManagement.Classes;
using CarInventoryManagement.Objects;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;


namespace CarInventoryManagement
{
    public partial class ResetSystemWindow : Window
    {
        // Booleans
        bool setupStatus;

        public ResetSystemWindow()
        {
            InitializeComponent();

            setupStatus = StartupWindow.instance.hasSetup;
            // Assigns where or not the system has been setup or not.

            if (setupStatus)
            {
                MessageTextblock.Text = "Warning: Proceeding from here will result in all saved information being completely deleted. " +
                                        "If you wish to proceed please enter the admin password (entered when setting up the system) and press reset.";                
            }
            else
            {
                MessageTextblock.Text = "It seems that you haven't setup the system yet, so there is no data to remove. " +
                                        "Please close this window and come back later if you need to.";
            }
            // Changes the dialog based on the setup status.
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            if (setupStatus)
            {
                using var reader = new StreamReader(@"C:\Users\alidi\Documents\CIM\Users\userobj.csv");
                // This is the directory where the csv file is.

                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                // Creates a CsvWriter object.

                var records = csv.GetRecords<UserObject>();
                // Reads all records of the csv into an array.

                string userpass = records.First<UserObject>().UserPassword;
                // Stores the password from the csv as it will be dumped from the memory after the reader is closed.

                reader.Close();
                // Stops the reader from reading to avoid conflict when deleting the directory.

                if (Hash.ToSHA256(PasswordTextbox.Text) == userpass ) // Comparison of the hashed entered password and the first record's password.
                {
                    Directory.Delete(@"C:\Users\alidi\Documents\CIM", true);
                    // Deletes the specified directoryt and all of its sub-directories

                    this.Close();
                    // Closes this window.
                }
                else
                {
                    MessageTextblock.Text = "If you wish to proceed please enter the admin password(entered when setting up the system) and press reset. The password you entered was wrong.";
                }
                // Checks if the password entered matches the first password stored.
            }
            else
            {
                MessageTextblock.Foreground = Brushes.Red;
                // Turns the text red to warn the user.
            }
        }
    }
}
