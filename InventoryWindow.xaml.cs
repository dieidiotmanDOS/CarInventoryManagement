using CarInventoryManagement.Objects;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Win32;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace CarInventoryManagement
{
    public partial class InventoryWindow : Window
    {

        // Integers
        int curCarId = 0; // Current car ID.

        public InventoryWindow()
        {
            InitializeComponent();

            int linecount = File.ReadLines(@"C:\Users\alidi\Documents\CIM\Cars\carobj.csv").Count();
            curCarId = linecount - 1;

            try 
            { 
                LoadDataTable();
                WarningText.Text = "Welcome back...\nPlease make sure that the fields are filled and you use an interger value for the make and a decimal value for the price.";
                // Attempts to load the data from the csv file.
            }
            catch 
            {
                WarningText.Text = "Seems that this is your first time here...\nPlease make sure that the fields are filled and you use an interger value for the make and a decimal value for the price."; 
                // Tells the user to begin adding their first car if it returns an error.
            }
        }

        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            if (FullCheck()) // Runs a full check. Check the function for more information.
            {
                if (ConfirmBox.IsChecked == true)
                {
                    AddNewCar(); // Adds the new car.
                    LoadDataTable(); // Reloads the data grid.
                }
                else
                {
                    WarningText.Text = "Please make sure to check the box above to confirm your changes.";
                    WarningText.Foreground = Brushes.Red;
                }
            }
            else
            {
                WarningText.Text = "Please make sure that the fields are filled and you use an interger value for the make and a decimal value for the price.";
                WarningText.Foreground = Brushes.Red;
            }
        }

        private void LoadDataTable()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = @"C:\Users\alidi\Documents\CIM\Cars\carobj.csv";

            CarObject carObj = new CarObject();
            string[] carList;
            // Array fo each record in the csv file.

            DataTable dataTbl = new DataTable();
            dataTbl.Columns.Add("ID", typeof(string));
            dataTbl.Columns.Add("Brand", typeof(string));
            dataTbl.Columns.Add("Model", typeof(string));
            dataTbl.Columns.Add("Make", typeof(string));
            dataTbl.Columns.Add("Price (£)", typeof(string));
            dataTbl.Columns.Add("Colour", typeof(string));
            // Assigns what each column of the data table should contain.

            using (StreamReader reader = new StreamReader(openFile.FileName)) // Closes the reader once the code in the "using" block ends.
            {
                reader.ReadLine(); // Skips the first line in the csv, which are the names of the columns.

                while (!reader.EndOfStream) // Loops as long as the reader hasn't reached the end of the file.
                {
                    carList = reader.ReadLine().Split(",");
                    carObj.CarID = carList[0];
                    carObj.CarBrand = carList[1];
                    carObj.CarModel = carList[2];
                    carObj.CarMake = carList[3];
                    carObj.CarPrice = carList[4];
                    carObj.CarColour = carList[5];
                    // Adds each value to the row.

                    dataTbl.Rows.Add(carList);
                    // Adds the row of data to the table.
                }
                DataView dataView = new DataView(dataTbl);
                // Creates the object used to actually display the information.

                CarDataGrid.ColumnWidth = 119;
                // Sets the width of the columns.

                CarDataGrid.ItemsSource = dataView; 
                // Sets the information in the data grid element to display what needs to be shown.
            }
        }

        private bool FullCheck()
        {
            if (BrandTextbox.Text == "" || ModelTextbox.Text == "" || MakeTextbox.Text == "" || PriceTextbox.Text == "" || ColourTextbox.Text == "") // Checks if any of the fields are empty.
            {
                return false;
            }
            else if (!int.TryParse(MakeTextbox.Text, out int num1)) // Trys to convert the make value into an int.
            {
                return false;
            }
            else if (!float.TryParse(PriceTextbox.Text, out float num2)) // Try to convert the price into a float.
            {
                return false;
            }
            return true;
        }

        private void AddNewCar()
        {

            CarObject newCar = new CarObject();

            newCar.CarID = $"CAR{curCarId.ToString()}";
            // Sets the car ID

            curCarId++;
            // Increments the current car ID.

            newCar.CarBrand = BrandTextbox.Text;
            newCar.CarModel = ModelTextbox.Text;
            newCar.CarMake = MakeTextbox.Text;
            newCar.CarPrice = PriceTextbox.Text;
            newCar.CarColour = ColourTextbox.Text;
            // Sets each attribute to what the user has written.

            string firstline;
            // Initialises the variable.

            try 
            {
                firstline = File.ReadLines(@"C:\Users\alidi\Documents\CIM\Cars\carobj.csv").First(); // Trys to read the first line
            }
            catch
            {
                firstline = ""; // Returns blank if there is an error.
            }
            // Gets the first line (header) of the csv file.

            if (!(firstline == "CarID,CarBrand,CarModel,CarMake,CarPrice,CarColour")) // Checks if the heading have already been written
            {
                var records = new List<CarObject>()
                {
                    newCar
                };
                // Creates an array to hold the user objects, read to be written to the csv file

                using (var writer = new StreamWriter(@"C:\Users\alidi\Documents\CIM\Cars\carobj.csv", true))
                // This is the directory where the file will be saved
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);
                }
                // This writes the information directly to the file, it will create a new file if 1 is not found.
            }
            else
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    // Doesn't allow the header to be written again.
                };

                var records = new List<CarObject>()
                {
                    newCar
                };
                // Creates an array to hold the user objects, read to be written to the csv file

                using (var writer = new StreamWriter(@"C:\Users\alidi\Documents\CIM\Cars\carobj.csv", true))
                // This is the directory where the file will be saved
                using (var csv = new CsvWriter(writer, config)) // Uses the created configs.
                {
                    csv.WriteRecords(records);
                }
                // This writes the information directly to the file, it will create a new file if 1 is not found.
            }
        }

    }
}
