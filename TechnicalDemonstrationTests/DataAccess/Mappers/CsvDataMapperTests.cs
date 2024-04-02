using Medica.Uk.TechnicalDemonstration.DataAccess.Mappers;
using Medica.Uk.TechnicalDemonstration.DataModel;

namespace TechnicalDemonstrationTests.DataAccess.Mappers
{
    [TestFixture]
    public class CsvDataMapperTests
    {
        [Test]
        public void MapData_ValidCsvData_StoresDataInDictionary()
        {
            // Arrange
            var csvFilePath = "../../../TestData/validcsvdata.csv";
            var csvDataMapper = new CsvDataMapper<CsvDataModel>();

            // Act
            using var stream = new StreamReader(csvFilePath);
            var dataModels = csvDataMapper.MapData(stream.BaseStream);

            // Assert
            Assert.NotNull(dataModels);
            Assert.That(dataModels.Count(), Is.EqualTo(2));


            var dictionary = GetPrivateField<Dictionary<string, CsvDataModel>>(csvDataMapper, "_dataStore");
            Assert.That(dictionary, Is.Not.Null);
            Assert.That(dictionary.Count, Is.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(dictionary.ContainsKey("P001"), Is.True);
                Assert.That(dictionary["P001"].PatientId, Is.EqualTo("P001"));
                Assert.That(dictionary["P001"].DateOfBirth, Is.EqualTo("1993-01-01"));
                Assert.That(dictionary["P001"].Gender, Is.EqualTo("Male"));
            });
            Assert.Multiple(() =>
            {
                Assert.That(dictionary.ContainsKey("P002"), Is.True);
                Assert.That(dictionary["P002"].PatientId, Is.EqualTo("P002"));
                Assert.That(dictionary["P002"].DateOfBirth, Is.EqualTo("1998-05-15"));
                Assert.That(dictionary["P002"].Gender, Is.EqualTo("Female"));
            });
        }

        [Test]
        public void MapData_EmptyCsvData_ReturnsEmptyEnumerable()
        {
            // Arrange
            var csvFilePath = "../../../TestData/emptycsvdata.csv";
            var csvDataMapper = new CsvDataMapper<CsvDataModel>();

            // Act
            using var stream = new StreamReader(csvFilePath);
            var dataModels = csvDataMapper.MapData(stream.BaseStream);

            // Assert
            Assert.NotNull(dataModels);
            Assert.IsEmpty(dataModels);

            var dictionary = GetPrivateField<Dictionary<string, CsvDataModel>>(csvDataMapper, "_dataStore");
            Assert.NotNull(dictionary);
            Assert.IsEmpty(dictionary);
        }


        private static T GetPrivateField<T>(object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            return (T)field.GetValue(obj);
        }
    }
}