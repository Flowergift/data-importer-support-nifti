using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Medica.Uk.TechnicalDemonstration.DataAccess.FileReaders;
using Medica.Uk.TechnicalDemonstration.DataAccess.Mappers;
using Medica.Uk.TechnicalDemonstration.DataModels;

namespace Medica.Uk.TechnicalDemonstration.Services
{
        public class DataService
    {

        public  IEnumerable<T>? GetDataFromFile<T>(string filePath, FileFormat fileFormat) where T : TechnicalDemonstration.DataModel.DataModel, new()
        {
            FileReader fileReader;
            DataMapper<T> dataMapper;

            switch (fileFormat)
            {
                case FileFormat.CSV:
                    fileReader = new CsvFileReader(filePath);
                    dataMapper = new CsvDataMapper<T>();
                    break;
                case FileFormat.JSON:
                    fileReader = new CsvFileReader(filePath);
                    dataMapper = new JsonDataMapper<T>();
                    break;
                default:
                    throw new ArgumentException($"Unsupported file format: {fileFormat}");
            }

            using var stream =  fileReader.ReadFile();
            return dataMapper.MapData(stream);
        }
    }

    public enum FileFormat
    {
        CSV,
        JSON
    }
}