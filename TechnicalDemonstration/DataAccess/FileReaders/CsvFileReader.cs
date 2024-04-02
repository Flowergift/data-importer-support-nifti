using System.IO;

namespace Medica.Uk.TechnicalDemonstration.DataAccess.FileReaders
{
    public class CsvFileReader : FileReader
    {
        public CsvFileReader(string filePath) : base(filePath)
        {
        }

        public override Stream ReadFile()
        {
            if (!FileExists())
            {
                throw new FileNotFoundException($"The CSV file '{FilePath}' does not exist.");
            }

            try
            {
                using var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                var memoryStream = new MemoryStream();

                byte[] buffer = new byte[ChunkSize];
                int bytesRead;

                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, bytesRead);
                }

                memoryStream.Position = 0;

                if (!memoryStream.CanRead)
                {
                    throw new IOException($"The stream is not readable.");
                }

                return memoryStream;
            }
            catch (IOException ex)
            {
                throw new IOException($"An error occurred while reading the CSV file '{FilePath}'.", ex);
            }
        }



    }
}