using CarInventoryManagement.Classes;
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

        // File Locations
        string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CIM\Cars\carobj.csv"; // Directory where carobj.csv is saved.
        string stat_dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CIM\Stats\statistics.csv"; // Directory where statistics.txt is saved.

        // Misc.
        public CarObject? desiredCar;
        public static InventoryWindow? instance;
        Statistics stats = new Statistics();

        public InventoryWindow()
        {
            InitializeComponent();

            instance = this;

            if (!File.Exists(stat_dir)) // Checks if the file exists
            {
                Statistics initial = new Statistics();
                initial.CarsSold = 0;
                initial.TotalRevenue = 0;
                // Initialises the values

                var records = new List<Statistics>()
                {
                    initial
                };
                // Creates an array to hold the user objects, read to be written to the csv file

                using (var writer = new StreamWriter(stat_dir, true))
                // This is the directory where the writer will be working.
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);
                }
                // This writes the information directly to the file, it will create a new file if 1 is not found.
            }

            try 
            { 
                LoadDataTable();
                WarningText.Text = "Welcome back...\nPlease make sure that the fields are filled and you use an interger value for the make and a decimal value for the price.";
                // Attempts to load the data from the csv file.

                int linecount = File.ReadLines(dir).Count();
                curCarId = linecount - 1;
                // Gets the current car ID via counting the number of lines in the csv file.
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
            openFile.FileName = dir;

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

        private void SaveDataTable()
        {
            File.WriteAllText(dir, string.Empty);
            // Clears the csv file so that data doesn't duplicate after saving.

            var records = new List<CarObject>();
            // Creates an array to hold the car objects.

            foreach (DataRowView row in CarDataGrid.ItemsSource) // Read through all rows in the data grid.
            {
                CarObject changedCar = new CarObject();
               
                changedCar.CarID = row[0].ToString();
                changedCar.CarBrand = row[1].ToString();
                changedCar.CarModel = row[2].ToString();
                changedCar.CarMake = row[3].ToString();
                changedCar.CarPrice = row[4].ToString();
                changedCar.CarColour = row[5].ToString();
                // Set each attribute.

                records.Add(changedCar);
                // Add it to the arrray.
            }

            using (var writer = new StreamWriter(dir, true))
            // This is the directory where the writer will be working.
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
            // This writes the information directly to the file, it will create a new file if 1 is not found.
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
                firstline = File.ReadLines(dir).First(); // Trys to read the first line
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

                using (var writer = new StreamWriter(dir, true))
                // This is the directory where the writer will be working.
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

                using (var writer = new StreamWriter(dir, true))
                // This is the directory where the writer will be working.
                using (var csv = new CsvWriter(writer, config)) // Uses the created configs.
                {
                    csv.WriteRecords(records);
                }
                // This writes the information directly to the file, it will create a new file if 1 is not found.
            }
        }

        private void ProcessSale()
        {
            using var reader = new StreamReader(stat_dir);
            // This is the directory where the csv file is.

            using var csv_r = new CsvReader(reader, CultureInfo.InvariantCulture);
            // Creates a CsvWriter object.

            var records_r = csv_r.GetRecords<Statistics>();
            // Reads all records of the csv into an array.

            foreach (var record in records_r)
            {
                stats.CarsSold = record.CarsSold + 1;
                stats.TotalRevenue = record.TotalRevenue + float.Parse(desiredCar.CarPrice);
            }

            reader.Close();
            // Closes the reader.

            File.Delete(stat_dir);
            // Deletes the stat_dir file to be replaced with a new one

            var records_w = new List<Statistics>()
                {
                    stats
                };
            // Creates an array to hold the user objects, read to be written to the csv file

            using (var writer = new StreamWriter(stat_dir, true))
            // This is the directory where the file will be saved
            using (var csv_w = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv_w.WriteRecords(records_w);
            }
            // This writes the information directly to the file, it will create a new file if 1 is not found.
        }


        private void DeleteCar_Click(object sender, RoutedEventArgs e)
        {
            GetCarIdPopup getCarId = new GetCarIdPopup();
            getCarId.ShowDialog();

            LoadDataTable();
            // Reloads the table after a record is deleted it is deleted.
        }

        private void SellCar_Click(object sender, RoutedEventArgs e)
        {
            GetCarIdPopup getCarId = new GetCarIdPopup();
            getCarId.ShowDialog();

            ProcessSale();

            LoadDataTable();
            // Reloads the table after a record is deleted it is deleted.
        }

        private void ModCar_Click(object sender, RoutedEventArgs e)
        {
            CarDataGrid.IsReadOnly = !CarDataGrid.IsReadOnly;
            // Toggles between read-only and read/write.

            if (CarDataGrid.IsReadOnly) // Saves changes after the user toggles off modify car mode.
            {
                SaveDataTable();
                ModButton.Content = "MODIFY CAR";
            }
            else
            {
                ModButton.Content = "SAVE CHANGES";
            }
        }

        
    }
}