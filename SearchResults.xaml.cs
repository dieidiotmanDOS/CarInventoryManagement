using CarInventoryManagement.Objects;
using Microsoft.Win32;
using System.Data;
using System.IO;
using System.Windows;

namespace CarInventoryManagement
{ 
    public partial class SearchResults : Window
    {
        string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CIM\Cars\foundcars.csv";
        // Directory where the found records are saved.

        public SearchResults()
        {
            InitializeComponent();
            LoadDataTable();
        }

        private void LoadDataTable()
        {

            // Copy of code from InventoryWindow.xaml.cs //

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

                SearchResultsDG.ColumnWidth = 119;
                // Sets the width of the columns.

                SearchResultsDG.ItemsSource = dataView;
                // Sets the information in the data grid element to display what needs to be shown.
            }
        }
    }
}
