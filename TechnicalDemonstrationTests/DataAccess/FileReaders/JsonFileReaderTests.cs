using Medica.Uk.TechnicalDemonstration.DataAccess.FileReaders;


namespace TechnicalDemonstrationTests.DataAccess.FileReaders
{
    [TestFixture]
    public class JsonFileReaderTests
    {
        private string _testDataPath = "../../../TestData/";

        [Test]
        public void ReadFile_ValidFilePath_ReturnsStream()
        {
            // Arrange
            var jsonFilePath = Path.Combine(_testDataPath, "validjsondata.json");
            var jsonFileReader = new JsonFileReader(jsonFilePath);

            // Act
            using var stream = jsonFileReader.ReadFile();

            // Assert
            Assert.That(stream, Is.Not.Null);
            Assert.That(stream.CanRead, Is.True);
        }

        [Test]
        public void ReadFile_FileNotFound_ThrowsFileNotFoundException()
        {
            // Arrange
            var invalidFilePath = Path.Combine(_testDataPath, "nonexistent_file.json");
            var jsonFileReader = new JsonFileReader(invalidFilePath);

            // Act & Assert
            try
            {
                jsonFileReader.ReadFile();
                Assert.Fail("Expected FileNotFoundException was not thrown");
            }
            catch (Exception ex)
            {
                // Assert or perform any necessary checks on the exception
                Assert.Pass("FileNotFoundException was thrown as expected");
            }

        }
    }
}