using CarInventoryManagement.Objects;
using CarInventoryManagement.Classes;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Windows;

namespace CarInventoryManagement
{
    public partial class LoginWindow : Window
    {
        bool isFilled = false;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            isFilled = checkFields();
            // Checks if the fields are filled.

            if (isFilled)
            {
                LoginUser();
            }
            else
            {
                WarningText.Text = "Please fill in all of the fields!";
            }
        }

        private bool checkFields()
        {
            if (!(userID.Text == "" && userPassword.Password == ""))
            {
                return true;
            }
            else return false;

            // Checks if any of the fields are empty/null.

        }

        private void LoginUser()
        {

            using var reader = new StreamReader(@"C:\Users\alidi\Documents\CIM\Users\userobj.csv");
            // This is the directory where the csv file is.

            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            // Creates a CsvWriter object.

            var records = csv.GetRecords<UserObject>();
            // Reads all records of the csv into an array.

            foreach (var record in records) 
            {
                if (EncryptionSys.Encrypt(userID.Text) == record.userID && Hash.ToSHA256(userPassword.Password) == record.UserPassword) 
                // 05/08/2024 - Now includes the Encrypt() method to encrypt the user's input for comparison.
                {
                    WarningText.Text = "Login Successful!";
                    // Open the inventory.
                } else
                {
                    WarningText.Text = "Check that your user ID and password are correct!";
                    // Warns the user of their mistake.
                }
            }
            // Goes through all records and checks for the case where both the user id and password match.

        }
    }
}
