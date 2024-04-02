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
            var csvFilePath = "../../../TestData/validcsvdata.csv";

            // Act
            var dataModel = dataService.GetDataFromFile<CsvDataModel>(csvFilePath, FileFormat.CSV);

            // Assert
            Assert.That(dataModel, Is.Not.Null);
            Assert.That(dataModel, Is.InstanceOf<IEnumerable<CsvDataModel>>());
        }

        [Test]
        public void GetDataFromFile_ValidJsonFile_ReturnsJsonDataModel()
        {
            // Arrange
            var dataService = new DataService();
            var jsonFilePath = "../../../TestData/validjsondata.json";

            // Act
            var dataModel = dataService.GetDataFromFile<JsonDataModel>(jsonFilePath, FileFormat.JSON);

            // Assert
            Assert.That(dataModel, Is.Not.Null);
            Assert.That(dataModel, Is.InstanceOf<IEnumerable<JsonDataModel>>());
        }

        [Test]
        public void GetDataFromFile_InvalidFileFormat_ThrowsArgumentException()
        {
            // Arrange
            var dataService = new DataService();
            var filePath = "../../../TestData/invalidcsvdata.csv";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => dataService.GetDataFromFile<CsvDataModel>(filePath, (FileFormat)(-1)));
        }

        [Test]
        public void GetDataFromFile_FileNotFound_ThrowsFileNotFoundException()
        {
            // Arrange
            var dataService = new DataService();
            var nonExistentFilePath =  "../../../TestData/nonexistent.csv";

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => dataService.GetDataFromFile<CsvDataModel>(nonExistentFilePath, FileFormat.CSV));
        }
    }
}