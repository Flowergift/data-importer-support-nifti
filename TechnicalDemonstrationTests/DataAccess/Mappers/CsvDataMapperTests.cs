using FileHelpers;
using Medica.Uk.TechnicalDemonstration.DataAccess.Mappers;
using Medica.Uk.TechnicalDemonstration.DataModel;


namespace TechnicalDemonstrationTests.DataAccess.Mappers
{
    [TestFixture]
    public class CsvDataMapperTests
    {
        private string _testDataPath = "../../TestData/"; 

        [Test]
        public void MapData_ValidCsvData_StoresDataInDictionary()
        {
            // Arrange
            var csvFilePath = Path.Combine(_testDataPath, "validcsvdata.csv");
            var csvDataMapper = new CsvDataMapper<CsvDataModel>();

            using var stream = File.OpenRead(csvFilePath);

            // Act
            var dataModels = csvDataMapper.MapData(stream);

            // Assert
            Assert.That(dataModels, Is.Not.Null);
            Assert.That(dataModels.Count(), Is.EqualTo(2)); 

            var dictionary = GetPrivateField<Dictionary<string, CsvDataModel>>(csvDataMapper, "_dataStore");
            Assert.That(dictionary, Is.Not.Null);
            Assert.That(dictionary.Count, Is.EqualTo(2)); 


        }

        [Test]
        public void MapData_EmptyCsvData_ReturnsEmptyEnumerable()
        {
            // Arrange
            var csvFilePath = Path.Combine(_testDataPath, "emptycsvdata.csv");
            var csvDataMapper = new CsvDataMapper<CsvDataModel>();

            using var stream = File.OpenRead(csvFilePath);

            // Act
            var dataModels = csvDataMapper.MapData(stream);

            // Assert
            Assert.That(dataModels, Is.Not.Null);
            Assert.That(dataModels.Count(), Is.EqualTo(0));

            var dictionary = GetPrivateField<Dictionary<string, CsvDataModel>>(csvDataMapper, "_dataStore");
            Assert.That(dictionary, Is.Not.Null);
            Assert.That(dictionary, Is.Empty);
        }

        [Test]
        public void MapData_InvalidCsvData_ThrowsException()
        {
            // Arrange
            var csvFilePath = Path.Combine(_testDataPath, "invalidcsvdata.csv");
            var csvDataMapper = new CsvDataMapper<CsvDataModel>();

            using var stream = File.OpenRead(csvFilePath);

            // Act & Assert
            Assert.Throws<FileHelpersException>(() => csvDataMapper.MapData(stream));
        }

        private static T GetPrivateField<T>(object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            return (T)field.GetValue(obj);
        }
    }
}