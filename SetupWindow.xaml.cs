using CarInventoryManagement.Objects;
using System.Reflection;
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
            }
            else 
            {      
                WarningText.Text = "Please ensure that all fields are filled.\nAnd that the passwords entered match!";
                // Warns the user of their mistake
            }

        }
        
        private bool checkFields()
        {
            if (!(companyName.Text == null && userFullName.Text == null && password1.Text == null && password2.Text == null))
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
            // Code to generate an ID based on the user's name and company name
            return "null";
        }

        private string hashPassword(string password)
        {
            return password;
        }


        private void AddNewUser(string UserID, string UserPassword, int UserTier)
        {

            // Creates a new csv file.

        }
    }
}
