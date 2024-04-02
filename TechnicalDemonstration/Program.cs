using Medica.Uk.TechnicalDemonstration.DataModel;
using Medica.Uk.TechnicalDemonstration.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Medica.Uk.TechnicalDemonstration
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = ConfigurationHelper.Configuration;
            var dataService = new DataService();

            Console.WriteLine("Reading CSV data...");
            var csvData = dataService.GetDataFromFile<CsvDataModel>(configuration["Csv:FilePath"], FileFormat.CSV);
            ProcessData(csvData);

            Console.WriteLine("\nReading JSON data...");
            var jsonData = dataService.GetDataFromFile<JsonDataModel>(configuration["Json:FilePath"], FileFormat.JSON);
            ProcessData(jsonData);

            Console.ReadLine();
        }

        static void ProcessData<T>(IEnumerable<T>? data) where T : TechnicalDemonstration.DataModel.DataModel
        {
            if (data == null)
            {
                Console.WriteLine("No data to process.");
                return;
            }

            int recordCount = data.Count();
            Console.WriteLine($"Total records: {recordCount}");

            int displayedCount = 0;
            foreach (var record in data)
            {
                if (displayedCount >= 3)
                {
                    break; // Stop displaying after 3 records
                }

                switch (record)
                {
                    case CsvDataModel csvData:
                        Console.WriteLine("--------------------"); // Separator for each record
                        Console.WriteLine($"- Name: {csvData.Name}");
                        Console.WriteLine($"- Patient ID: {csvData.PatientId}, Date of Birth: {csvData.DateOfBirth:yyyy-MM-dd}"); // Format date
                        Console.WriteLine($"- Gender: {csvData.Gender}, Medical Conditions: {csvData.MedicalConditions}");
                        Console.WriteLine($"- Medications: {csvData.Medications}, Allergies: {csvData.Allergies}");
                        Console.WriteLine($"- Last Appointment Date: {csvData.LastAppointmentDate:yyyy-MM-dd}"); // Format date
                        break;
                    case JsonDataModel jsonData:
                        Console.WriteLine("--------------------"); // Separator for each record
                        Console.WriteLine($"- Patient ID: {jsonData.PatientId}, Appointment ID: {jsonData.AppointmentID}");
                        Console.WriteLine($"- Gender: {jsonData.Gender}, Scheduled Day: {jsonData.ScheduledDay}");
                        Console.WriteLine($"- Appointment Day: {jsonData.AppointmentDay}, Age: {jsonData.Age}");
                        Console.WriteLine($"- Neighbourhood: {jsonData.Neighbourhood}, Scholarship: {jsonData.Scholarship}");
                        Console.WriteLine($"- Hipertension: {jsonData.Hipertension}, Diabetes: {jsonData.Diabetes}");
                        Console.WriteLine($"- Alcoholism: {jsonData.Alcoholism}, Handcap: {jsonData.Handcap}");
                        Console.WriteLine($"- SMS Received: {jsonData.SMS_received}, No Show: {jsonData.No_show}");
                        break;
                    default:
                        Console.WriteLine($"Unsupported data type: {record.GetType()}");
                        break;
                }

                displayedCount++;
            }

            if (recordCount > 3)
            {
                Console.WriteLine("... more records exist"); // Indicate more records exist
            }
        }
    }
}