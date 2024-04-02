using Medica.Uk.TechnicalDemonstration.DataModel;
using Medica.Uk.TechnicalDemonstration.Services;

namespace TechnicalDemonstrationTests.Services
{
    [TestFixture]
    public class DataServiceTests
    {
        [Test]
        public void GetDataFromFile_ValidCsvFile_ReturnsCsvDataModel()
        {
            // Arrange
            var dataService = new DataService();
            var csvFilePath = "path/to/valid/data.csv";

            // Act
            var dataModel = dataService.GetDataFromFile<CsvDataModel>(csvFilePath, FileFormat.CSV);

            // Assert
            Assert.That(dataModel, Is.Not.Null);
            Assert.That(dataModel, Is.InstanceOf<CsvDataModel>());
        }

        [Test]
        public void GetDataFromFile_ValidJsonFile_ReturnsJsonDataModel()
        {
            // Arrange
            var dataService = new DataService();
            var jsonFilePath = "path/to/valid/data.json";

            // Act
            var dataModel = dataService.GetDataFromFile<JsonDataModel>(jsonFilePath, FileFormat.JSON);

            // Assert
            Assert.That(dataModel, Is.Not.Null);
            Assert.That(dataModel, Is.InstanceOf<JsonDataModel>());
        }

        [Test]
        public void GetDataFromFile_InvalidFileFormat_ThrowsArgumentException()
        {
            // Arrange
            var dataService = new DataService();
            var filePath = "path/to/file.txt";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => dataService.GetDataFromFile<CsvDataModel>(filePath, (FileFormat)(-1)));
        }

        [Test]
        public void GetDataFromFile_FileNotFound_ThrowsFileNotFoundException()
        {
            // Arrange
            var dataService = new DataService();
            var nonExistentFilePath = "path/to/nonexistent/file.csv";

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => dataService.GetDataFromFile<CsvDataModel>(nonExistentFilePath, FileFormat.CSV));
        }
    }
}