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
            var jsonData = @"
            [
              {
                ""Id"": ""1"",
                ""PatientId"": ""P001"",
                ""AppointmentID"": ""A001"",
                ""Gender"": ""Male"",
                ""ScheduledDay"": ""2023-11-20"",
                ""AppointmentDay"": ""2023-11-20"",
                ""Age"": 30,
                ""Neighbourhood"": ""Central District"",
                ""Scholarship"": true,
                ""Hipertension"": false,
                ""Diabetes"": false,
                ""Alcoholism"": false,
                ""Handcap"": false,
                ""SMS_received"": true,
                ""No_show"": false
              },
              {
                ""Id"": ""2"",
                ""PatientId"": ""P002"",
                ""AppointmentID"": ""A002"",
                ""Gender"": ""Female"",
                ""ScheduledDay"": ""2023-11-21"",
                ""AppointmentDay"": ""2023-11-21"",
                ""Age"": 25,
                ""Neighbourhood"": ""West End"",
                ""Scholarship"": false,
                ""Hipertension"": true,
                ""Diabetes"": false,
                ""Alcoholism"": false,
                ""Handcap"": false,
                ""SMS_received"": false,
                ""No_show"": true
              }
            ]";

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
            var jsonMapper = new JsonDataMapper<JsonDataModel>();

            // Act
            var dataModels =  jsonMapper.MapData(stream);

            // Assert
            Assert.That(dataModels, Is.Not.Null);
            Assert.That(dataModels.Count(), Is.EqualTo(2));

            var dictionary = GetPrivateField<Dictionary<string, JsonDataModel>>(jsonMapper, "_dataStore");
            Assert.That(dictionary, Is.Not.Null);
            Assert.That(dictionary.Count, Is.EqualTo(2));

            // Assert values for the first record
            Assert.Multiple(() =>
            {
                Assert.That(dictionary["1"].PatientId, Is.EqualTo("P001"));
                Assert.That(dictionary["1"].AppointmentID, Is.EqualTo("A001"));
                Assert.That(dictionary["1"].Gender, Is.EqualTo("Male"));
                Assert.That(dictionary["1"].ScheduledDay, Is.EqualTo("2023-11-20"));
                Assert.That(dictionary["1"].AppointmentDay, Is.EqualTo("2023-11-20"));
                Assert.That(dictionary["1"].Age, Is.EqualTo(30));
                Assert.That(dictionary["1"].Neighbourhood, Is.EqualTo("Central District"));
                Assert.That(dictionary["1"].Scholarship, Is.True);
                Assert.That(dictionary["1"].Hipertension, Is.False);
                Assert.That(dictionary["1"].Diabetes, Is.False);
                Assert.That(dictionary["1"].Alcoholism, Is.False);
                Assert.That(dictionary["1"].Handcap, Is.False);
                Assert.That(dictionary["1"].SMS_received, Is.True);
                Assert.That(dictionary["1"].No_show, Is.False);
            });

            Assert.Multiple(() =>
            {
                Assert.That(dictionary["2"].PatientId, Is.EqualTo("P002"));
                Assert.That(dictionary["2"].AppointmentID, Is.EqualTo("A002"));
                Assert.That(dictionary["2"].Gender, Is.EqualTo("Female"));
                Assert.That(dictionary["2"].ScheduledDay, Is.EqualTo("2023-11-21"));
                Assert.That(dictionary["2"].AppointmentDay, Is.EqualTo("2023-11-21"));
                Assert.That(dictionary["2"].Age, Is.EqualTo(25));
                Assert.That(dictionary["2"].Neighbourhood, Is.EqualTo("West End"));
                Assert.That(dictionary["2"].Scholarship, Is.False);
                Assert.That(dictionary["2"].Hipertension, Is.True);
                Assert.That(dictionary["2"].Diabetes, Is.False);
                Assert.That(dictionary["2"].Alcoholism, Is.False);
                Assert.That(dictionary["2"].Handcap, Is.False);
                Assert.That(dictionary["2"].SMS_received, Is.False);
                Assert.That(dictionary["2"].No_show, Is.True);
            });
        }

        [Test]
        public void MapData_EmptyJsonData_ReturnsEmptyEnumerable()
        {
            // Arrange
            var jsonData = "";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData));
            var jsonDataMapper = new JsonDataMapper<JsonDataModel>();

            // Act
            var dataModels = jsonDataMapper.MapData(stream);

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
            var invalidJsonData = "InvalidData";
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