﻿using CarInventoryManagement.Classes;
using CarInventoryManagement.Objects;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Windows;

namespace CarInventoryManagement
{
    public partial class SetupWindow : Window
    {
        // Booleans
        bool isFilled = false;
        bool passMatch = false;

        // Integers
        int accountNum = 0;

        // Strings
        public string userIdentifier = "";

        // Objects
        UserObject newAdminUser = new UserObject();

        // Reference to this window's instance.
        public static SetupWindow? instance;
       
        public SetupWindow()
        {
            InitializeComponent();

            instance = this;
            // Declares this window's instance.
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            isFilled = checkFields();
            // Checks if the fields are filled.

            passMatch = checkPass();
            // Checks if the passwords match.

            if (isFilled && passMatch) 
            {
                newAdminUser.userID = EncryptionSys.Encrypt(GenerateUserID(userFullName.Text, companyName.Text));
                // Generates a userID and adds it to the object.
                // 05/08/2024 - Now includes the Encrypt() method to encrypt the user ID.

                newAdminUser.UserTier = 3;
                // Sets the new user's admin permissions to max and adds it to the object.

                newAdminUser.UserPassword = hashPassword(password1.Password);
                // Adds a hashed variant of the user's password to the object

                AddNewUser();
                WarningText.Text = "Setup Successful!";

                ShowUserIdPopup popup = new ShowUserIdPopup();
                // Gets the other window's object and assigns it to a variable.

                popup.ShowDialog();
                // Opens that window as a popup, so that the user cannot open this window twice by switching back to the other one.

                StartupWindow startupWin = new StartupWindow();

                startupWin.hasSetup = true;
                startupWin.Visibility = Visibility.Visible;
                this.Close();
                // Sends the user back to the startup window.

            }
            else 
            {      
                WarningText.Text = "Please ensure that all fields are filled.\nAnd that the passwords entered match!";
                // Warns the user of their mistake
            }
        }
        
        private bool checkFields()
        {
            if (!(companyName.Text == "" && userFullName.Text == "" && password1.Password == "" && password2.Password == ""))
            {
                return true;
            }
            else return false;

            // Checks if any of the fields are empty/null.
        }

        private bool checkPass()
        {
            if (password1.Password == password2.Password)
            { 
                return true;
            }
            else return false;
            
            // Checks if the content of the text boxes are the same.
        }

        private string GenerateUserID(string userName, string companyName)
        {
            string firstname = userName.Split(' ')[0];
            // Get the first name of the user.

            string company = "";
            // Initialise the string.

            string[] comStr = companyName.Split(" ");
            // Create a list of substrings that are split via a SPACE character.

            foreach (string str in comStr)
            {
                company += str;
            }
            // For each substring, add them to the company string.

            userIdentifier = firstname + company + accountNum.ToString();
            // Storing the id in variable for later.

            accountNum++;
            // Increases the account number.

            return userIdentifier;
            // Return the formatted user ID.
        }

        private string hashPassword(string password)
        {
            password = Hash.ToSHA256(password);
            // Uses the ToSHA256 method in the Hash class in order to hash the password

            return password;
        }

        private void AddNewUser()
        {
            var records = new List<UserObject>()
            {
                newAdminUser
            };
            // Creates an array to hold the user objects, read to be written to the csv file

            using (var writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CIM\Users\userobj.csv"))
                // This is the directory where the file will be saved
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
            // This writes the information directly to the file, it will create a new file if 1 is not found.
        }
    }
}
