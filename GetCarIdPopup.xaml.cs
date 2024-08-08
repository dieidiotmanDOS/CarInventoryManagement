using CarInventoryManagement.Objects;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace CarInventoryManagement
{
    public partial class GetCarIdPopup : Window
    {

        // Intergers
        int recordCount = File.ReadLines(@"C:\Users\alidi\Documents\CIM\Cars\carobj.csv").Count() - 1;

        // Booleans
        bool idFound = false;

        public GetCarIdPopup()
        {
            InitializeComponent();
        }

        private void CarIdTextbox_Changed(object sender, TextChangedEventArgs e)
        {
            idFound = FindCarId(); 

            if (idFound)
            {
                SearchResultTextblock.Text = "Car ID Found! Press Continue...";
                // Has been found.
            }
            else
            {
                SearchResultTextblock.Text = "Car ID Not Found! Please try again...";
                // Has not been found.
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (idFound)
            {
                // Delete record.
            }
        }

        private bool FindCarId()
        {
            int idPos = -1;

            using var reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CIM\Cars\carobj.csv");
            // This is the directory where the csv file is.

            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            // Creates a CsvWriter object.

            var records = csv.GetRecords<CarObject>();
            // Reads all records of the csv into an array.

            foreach (var record in records)
            {
                idPos++;
                if (record.CarID == CarIdTextbox.Text)  // Checks if they match
                {
                    return true;
                }
            }
            // Linear search to find the right record

            return false;
        }
    }
}
