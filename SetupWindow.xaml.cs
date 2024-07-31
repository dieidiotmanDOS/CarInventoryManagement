using CarInventoryManagement.Classes;
using CarInventoryManagement.Objects;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CarInventoryManagement
{
    /// <summary>
    /// Interaction logic for SetupWindow.xaml 
    /// </summary>
    public partial class SetupWindow : Window
    {
        bool isFilled = false;
        bool passMatch = false;

        int accountNum = 0;

        public static SetupWindow? instance;
        // References this specific window instance.

        public string userIdentifier = "";
        

        UserObject newAdminUser = new UserObject();

        public SetupWindow()
        {
            InitializeComponent();
            instance = this;
            // Declares it.
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            isFilled = checkFields();
            // Checks if the fields are filled.

            passMatch = checkPass();
            // Checks if the passwords match.

            if (isFilled && passMatch) 
            {

                newAdminUser.userID = GenerateUserID(userFullName.Text, companyName.Text);
                // Generates a userID and adds it to the object.

                newAdminUser.UserTier = 3;
                // Sets the new user's admin permissions to max and adds it to the object.

                newAdminUser.UserPassword = hashPassword(password1.Text);
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



            }
            else 
            {      
                WarningText.Text = "Please ensure that all fields are filled.\nAnd that the passwords entered match!";
                // Warns the user of their mistake
            }

        }
        
        private bool checkFields()
        {
            if (!(companyName.Text == "" && userFullName.Text == "" && password1.Text == "" && password2.Text == ""))
            {
                return true;
            }
            else return false;

            // Checks if any of the fields are empty/null.
            
        }

        private bool checkPass()
        {
            if (password1.Text == password2.Text)
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

            using (var writer = new StreamWriter(@"C:\Users\alidi\Documents\CIM\Users\userobj.csv"))
                // This is the directory where the file will be saved
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
            // This writes the information directly to the file, it will create a new file if 1 is not found.

        }

        // IGNORE BEYOND THIS POINT //
        private void txtBox1_Down(object sender, RoutedEventArgs e)
        {
            if (companyName.Text == "Company Name") 
            {
                companyName.Text = "";
            }
            
        }
        private void txtBox2_Down(object sender, RoutedEventArgs e)
        {
            if (userFullName.Text == "Full Name") 
            {
                userFullName.Text = "";
            }
            
        }
        private void txtBox3_Down(object sender, RoutedEventArgs e)
        {
            if (password1.Text == "Password") 
            {
                password1.Text = "";
            }
            
        }
        private void txtBox4_Down(object sender, RoutedEventArgs e)
        {
            if (password2.Text == "Confirm Password") 
            {
                password2.Text = "";
            }
        }

        private void txtBox1_lostFocus(object sender, RoutedEventArgs e)
        {
            if (companyName.Text == "") 
            {
                companyName.Text = "Company Name";
            }

            
        }

        private void txtBox2_lostFocus(object sender, RoutedEventArgs e)
        {
            if (userFullName.Text == "")
            {
                userFullName.Text = "Full Name";
            }
           
        }

        private void txtBox3_lostFocus(object sender, RoutedEventArgs e)
        {
            if (password1.Text == "")
            {
                password1.Text = "Password";
            }
            
        }

        private void txtBox4_lostFocus(object sender, RoutedEventArgs e)
        {
            if (password2.Text == "")
            {
                password2.Text = "Confirm Password";
            }
            
        }

    }
}
