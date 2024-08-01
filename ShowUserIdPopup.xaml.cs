using System.Windows;

namespace CarInventoryManagement
{
    public partial class ShowUserIdPopup : Window
    {
        public ShowUserIdPopup()
        {
            InitializeComponent();

            useridTextBlock.Text = $"Your user ID is: {SetupWindow.instance.userIdentifier}";
            // Sets the useridTextBlock's text to show the user id.
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            // Closes the popup.
        }
    }
}
