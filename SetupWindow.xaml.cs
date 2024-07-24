using CarInventoryManagement.Classes;
using CarInventoryManagement.Objects;
using CsvHelper;
using System.Globalization;
using System.IO;
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
        

        UserObject newAdminUser = new UserObject();

        public SetupWindow()
        {
            InitializeComponent();

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
            Random rand = new Random();
            // Creates a new random object used to create random values in a given range.

            string idNum = rand.Next(100, 999).ToString();
            // Generates a random value from 100-999.

            string id = $"{userName.Substring(0,3)}{idNum}@{companyName.Substring(0,3)}";
            // Puts together the different parts of the string to make an ID for the admin user.

            return id;
            // passes out the id.
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
