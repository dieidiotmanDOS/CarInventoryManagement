using CarInventoryManagement.Objects;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Windows;

namespace CarInventoryManagement
{
    public partial class CarSearcher : Window
    {
        string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CIM\Cars\carobj.csv";
        // Directory where the carsobj.csv is saved.

        string dir_found = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CIM\Cars\foundcars.csv";
        // Directory where the found records are saved.

        CarObject[]? foundRecordsRAW;
        CarObject[]? foundRecords;


        public CarSearcher()
        {
            InitializeComponent();     
        }

        private void SearchRecords()
        {
            int comboIndex = -1;
            int curIndex = 0;

            if (SearchCombo.SelectedIndex == -1) // Checks if nothing is selected.
            {
                MessageBox.Show("Nothing is selected, please select something first!");
                // Warns the user.
                return;
                // Stops the procedure.
            }

            comboIndex = SearchCombo.SelectedIndex;
            // Sets the index to search.

            using var reader = new StreamReader(dir);
            // Reader object that reads the given directory.

            using var csv_r = new CsvReader(reader, CultureInfo.InvariantCulture);
            // Creates a CsvWriter object.

            var records_r = csv_r.GetRecords<CarObject>();
            // Reads all records of the csv into an array.

            foreach (var record in records_r)
            {
                if (curIndex > int.Parse(NumToFindText.Text) - 1) break;
                // Stops searching if the foundRecords array is full.

                switch (comboIndex)
                {
                    case 0:
                        // When the user is looking for Car IDs.
                        if (record.CarID == FindTextbox.Text)
                        {
                            foundRecordsRAW[curIndex] = record;
                            curIndex++;
                            break;
                        }
                        break;

                    case 1:
                        // When the user is looking for Brands.
                        if (record.CarBrand == FindTextbox.Text)
                        {
                            foundRecordsRAW[curIndex] = record;
                            curIndex++;
                            break; 
                        }
                        break;

                    case 2:
                        // When the user is looking for Models.
                        if (record.CarModel == FindTextbox.Text)
                        {
                            foundRecordsRAW[curIndex] = record;
                            curIndex++;
                            break;
                        }
                        break;

                    case 3:
                        // When the user is looking for Make.
                        if (record.CarMake == FindTextbox.Text)
                        {
                            foundRecordsRAW[curIndex] = record;
                            curIndex++;
                            break;
                        }
                        break;

                    case 4:
                        // When the user is looking for Colour.
                        if (record.CarColour == FindTextbox.Text)
                        {
                            foundRecordsRAW[curIndex] = record;
                            curIndex++;
                            break;
                        }
                        break;
                }
            }
            // Loops through all records

            GenerateFoundRecordsArray();
            SaveFoundRecords();

            SearchResults resultsWindow = new SearchResults();
            resultsWindow.ShowDialog();
        }

        private void SaveFoundRecords()
        {  

            File.WriteAllText(dir_found, string.Empty);
            // Clear the file to avoid duplicate column names.

            using (var writer = new StreamWriter(dir_found, true))
            // This is the directory where the writer will be working.
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(foundRecords);
            }
            // This writes the information directly to the file, it will create a new file if 1 is not found.
        }

        private void GenerateFoundRecordsArrayRAW(int recordMax)
        {
            foundRecordsRAW = new CarObject[recordMax];
            // Initialise the array.

            for (int i = 0; i < recordMax; i++)
            {
                foundRecordsRAW[i] = new CarObject();
            }
            // Creates a new blank record until the ammount specified.
        }

        private void GenerateFoundRecordsArray()
        {
            int recNum = 0;

            foreach (var record in foundRecordsRAW)
            {
                if (record.CarID != null)
                {
                    recNum++;                  
                }
            }

            foundRecords = new CarObject[recNum];

            for (int i = 0;i < recNum; i++)
            {
                foundRecords[i] = new CarObject();
                foundRecords[i] = foundRecordsRAW[i];
            }

            // Checks all of the records in the foundRecordsRAW and removes any null records.
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            int tmp1;
            int tmp2;
            // Temporary int value.


            if (FindTextbox.Text == string.Empty || NumToFindText.Text == string.Empty)
            {
                WarningText.Text = "Please ensure all text boxes are filled...";
                return;
            }

            if (!(int.TryParse(NumToFindText.Text, out tmp1)))
            {
                if (tmp1 < 1)
                {
                    WarningText.Text = "The number of items to search for must be an integer larger than 0";
                    return;
                }
                WarningText.Text = "The number of items to search for must be an integer larger than 0";
                return;
            }

            if (SearchCombo.SelectedIndex == 3) // If the user is searching for a specific Make.
            {
                if (int.TryParse(FindTextbox.Text, out tmp2)) // Attempts to convert the string into an int to check that it is valid
                {
                    if (tmp2 > -1)
                    {
                        GenerateFoundRecordsArrayRAW(int.Parse(NumToFindText.Text));
                        // Creates an array of the correct size.

                        SearchRecords();
                    }
                    else
                    {
                        WarningText.Text = "When searching for the make of a car, the value entered must be a valid year (positive integer).";
                    }
                }
                else
                {
                    WarningText.Text = "When searching for make of a car, the value entered must be a valid year (positive integer).";
                }
            }
            else
            {
                GenerateFoundRecordsArrayRAW(int.Parse(NumToFindText.Text));
                // Creates an array of the correct size.

                SearchRecords();
            }

        }
    }
}
