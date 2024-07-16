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
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {

        public bool hasSetup = false;
        // Ensures that the user can only either setup the system for the first time or login in instead.


        public StartupWindow()
        {
            InitializeComponent();
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

            this.Visibility = Visibility.Hidden;
            // Hides the current window, incase the user presses the back button.

            setupWin.Show();
            // Displays the assigned window.
            } 
            else
            {
                LoginText.Foreground = Brushes.Red;
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

                this.Visibility = Visibility.Hidden;
                // Hides the current window, incase the user presses the back button.

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
    }
}
