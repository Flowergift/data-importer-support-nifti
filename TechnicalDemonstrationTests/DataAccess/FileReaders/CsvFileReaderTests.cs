using Medica.Uk.TechnicalDemonstration.DataAccess.FileReaders;


namespace TechnicalDemonstrationTests.DataAccess.FileReaders
{
    [TestFixture]
    public class CsvFileReaderTests
    {
        private string _testDataPath = "../../../TestData/";

        [Test]
        public void ReadFile_ValidFilePath_ReturnsStream()
        {
            // Arrange
            var csvFilePath = Path.Combine(_testDataPath, "validcsvdata.csv");
            var csvFileReader = new CsvFileReader(csvFilePath);

            // Act
            using var stream = csvFileReader.ReadFile();

            // Assert
            Assert.That(stream, Is.Not.Null);
            Assert.That(stream.CanRead, Is.True);
        }

        [Test]
        public void ReadFile_FileNotFound_ThrowsFileNotFoundException()
        {
            // Arrange
            var invalidFilePath = Path.Combine(_testDataPath, "nonexistent_file.csv");

            var csvFileReader = new CsvFileReader(invalidFilePath);

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => csvFileReader.ReadFile());
        }
    }
}
