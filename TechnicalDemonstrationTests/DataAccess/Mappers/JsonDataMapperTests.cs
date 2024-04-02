using Medica.Uk.TechnicalDemonstration.DataAccess.Mappers;
using Medica.Uk.TechnicalDemonstration.DataModel;
using Newtonsoft.Json;
using System.Text;

namespace TechnicalDemonstrationTests.DataAccess.DataAccess.Mappers
{
    [TestFixture]
    public class JsonDataMapperTests
    {
        [Test]
        public void MapData_ValidJsonData_StoresDataInDictionary()
        {
            // Arrange
            var jsonFilePath = "../../../TestData/validjsondata.json";
            var jsonMapper = new JsonDataMapper<JsonDataModel>();

            // Act
            using var stream = new StreamReader(jsonFilePath);

            // Act
            var dataModels =  jsonMapper.MapData(stream.BaseStream);

            // Assert
            Assert.That(dataModels, Is.Not.Null);
            Assert.That(dataModels.Count(), Is.EqualTo(2));

            var dictionary = GetPrivateField<Dictionary<string, JsonDataModel>>(jsonMapper, "_dataStore");
            Assert.That(dictionary, Is.Not.Null);
            Assert.That(dictionary.Count, Is.EqualTo(2));

            // Assert values for the first record
            Assert.Multiple(() =>
            {
                Assert.That(dictionary["P001"].PatientId, Is.EqualTo("P001"));
                Assert.That(dictionary["P001"].AppointmentID, Is.EqualTo("A001"));
                Assert.That(dictionary["P001"].Gender, Is.EqualTo("Male"));
                Assert.That(dictionary["P001"].ScheduledDay, Is.EqualTo("2023-11-20"));
                Assert.That(dictionary["P001"].AppointmentDay, Is.EqualTo("2023-11-20"));
                Assert.That(dictionary["P001"].Age, Is.EqualTo("30"));
                Assert.That(dictionary["P001"].Neighbourhood, Is.EqualTo("Central District"));
                Assert.That(dictionary["P001"].Scholarship, Is.EqualTo("1"));
                Assert.That(dictionary["P001"].Hipertension, Is.EqualTo("0"));
                Assert.That(dictionary["P001"].Diabetes, Is.EqualTo("0"));
                Assert.That(dictionary["P001"].Alcoholism, Is.EqualTo("0"));
                Assert.That(dictionary["P001"].Handcap, Is.EqualTo("0"));
                Assert.That(dictionary["P001"].SMS_received, Is.EqualTo("1"));
                Assert.That(dictionary["P001"].No_show, Is.EqualTo("0"));
            });

            Assert.Multiple(() =>
            {
                Assert.That(dictionary["P002"].PatientId, Is.EqualTo("P002"));
                Assert.That(dictionary["P002"].AppointmentID, Is.EqualTo("A002"));
                Assert.That(dictionary["P002"].Gender, Is.EqualTo("Female"));
                Assert.That(dictionary["P002"].ScheduledDay, Is.EqualTo("2023-11-21"));
                Assert.That(dictionary["P002"].AppointmentDay, Is.EqualTo("2023-11-21"));
                Assert.That(dictionary["P002"].Age, Is.EqualTo("25"));
                Assert.That(dictionary["P002"].Neighbourhood, Is.EqualTo("West End"));
                Assert.That(dictionary["P002"].Scholarship, Is.EqualTo("0"));
                Assert.That(dictionary["P002"].Hipertension, Is.EqualTo("1"));
                Assert.That(dictionary["P002"].Diabetes, Is.EqualTo("0"));
                Assert.That(dictionary["P002"].Alcoholism, Is.EqualTo("0"));
                Assert.That(dictionary["P002"].Handcap, Is.EqualTo("0"));
                Assert.That(dictionary["P002"].SMS_received, Is.EqualTo("0"));
                Assert.That(dictionary["P002"].No_show, Is.EqualTo("1"));
            });
        }

        [Test]
        public void MapData_EmptyJsonData_ReturnsEmptyEnumerable()
        {
            // Arrange
            var jsonFilePath = "../../../TestData/emptyjsondata.json";
            var jsonDataMapper = new JsonDataMapper<JsonDataModel>();
            
            // Act
            using var stream = new StreamReader(jsonFilePath);
            var dataModels = jsonDataMapper.MapData(stream.BaseStream);

            // Assert
            Assert.IsNotNull(dataModels);
            Assert.That(dataModels.Count(), Is.EqualTo(0));

            var dictionary = GetPrivateField<Dictionary<string, JsonDataModel>>(jsonDataMapper, "_dataStore");
            Assert.That(dictionary, Is.Not.Null);
            Assert.That(dictionary.Count, Is.EqualTo(0));
        }

        [Test]
        public void MapData_InvalidJsonData_ThrowsException()
        {
            // Arrange
            var invalidJsonData = "../../../TestData/invalidjsondata";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(invalidJsonData));
            var jsonDataMapper = new JsonDataMapper<JsonDataModel>();

            // Act & Assert
            Assert.Throws<JsonReaderException>(() =>  jsonDataMapper.MapData(stream));
        }


        private static T? GetPrivateField<T>(object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (T)field.GetValue(obj);
        }
    }
}