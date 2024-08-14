using CarInventoryManagement.Objects;
using System.IO;

namespace CarInventoryManagement.Classes
{
    internal class LogData
    {
        public static void LogInfo(CarObject record, int type)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CIM\Cars\carlog.txt";
            // Directory where the log file will be saved.

            string[] cases = { "Added", "Deleted", "Sold" };
            // Using "type" we can decide whether a car was added, deleted or sold.

            string rec_combined = $"ID: {record.CarID}, Brand: {record.CarBrand}, Model: {record.CarModel}, Make: {record.CarMake}, Price: {record.CarPrice}, Colour: {record.CarColour}";
            // Combines all of the record information in a digestable manner.

            using (StreamWriter file = new StreamWriter(dir, true))
            {
                file.WriteLine($"{cases[type]} - {rec_combined} @{DateTime.Now}");
            }
            // Appends the text to the log file.

            
        }
    }
}
