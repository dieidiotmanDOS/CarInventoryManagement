using CarInventoryManagement.Objects;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace CarInventoryManagement
{
    public partial class GetCarIdPopup : Window
    {
        // Strings
        string? recordToDel;

        // Booleans
        bool idFound = false;

        // File Loction
        string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CIM\Cars\carobj.csv";
        string tmpdir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CIM\Cars\tmp.csv";
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
                SearchResultTextblock.Text = "Car ID Not Found! Please try another id...";
                // Has not been found.
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {

            if (idFound)
            {

                var records_write = new List<CarObject>();
                // Creates an array to hold the car objects.

                using var reader = new StreamReader(dir);
                // This is the directory where the csv file is.

                using var csv_r = new CsvReader(reader, CultureInfo.InvariantCulture);
                // Creates a CsvWriter object.

                var records_read = csv_r.GetRecords<CarObject>();
                // Reads all records of the csv into an array.

                foreach (var record in records_read)
                {
                    if (!(record.CarID == recordToDel))  // Checks the records are mismatched.
                    {
                        records_write.Add(record);
                        // Adds records that are not to be deleted to a temporary file.
                    }
                }

                reader.Close();
                // Close the reader stream and release the file from memory.

                using (var writer = new StreamWriter(tmpdir, true))
                using (var csv_w= new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv_w.WriteRecords(records_write);
                }
                
                File.Delete(dir);
                // Deletes the old csv file.

                File.Move(tmpdir, dir);
                // Replaces the old csv file with the new file after deleting the requested record.

                MessageBox.Show($"Successfully removed {recordToDel}!");
                // Confirms to the user of their changes.

                this.Close();
                // Closes the window
            }
        }

        private bool FindCarId()
        {
            using var reader = new StreamReader(dir);
            // This is the directory where the csv file is.

            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            // Creates a CsvWriter object.

            var records = csv.GetRecords<CarObject>();
            // Reads all records of the csv into an array.

            foreach (var record in records)
            {
                if (record.CarID == CarIdTextbox.Text)  // Checks if they match.
                {
                    InventoryWindow.instance.desiredCar = record;
                    recordToDel = record.CarID;
                    return true;
                }
            }
            // Linear search to find the right record temporarily.

            return false;
        }
    }
}
