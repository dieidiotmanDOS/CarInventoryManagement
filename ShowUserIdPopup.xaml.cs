using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarInventoryManagement
{
    /// <summary>
    /// Interaction logic for ShowUserIdPopup.xaml
    /// </summary>
    public partial class ShowUserIdPopup : Window
    {
        public ShowUserIdPopup()
        {
            InitializeComponent();

            useridTextBlock.Text = "Your user ID is: " + SetupWindow.instance.userIdentifier;
            // Sets the useridTextBlock's text to show the user id.

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            // Closes the popup.
        }
    }
}
