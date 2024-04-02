using Medica.Uk.TechnicalDemonstration.DataAccess.FileReaders;
using Moq;
using System;
using System.IO;

namespace TechnicalDemonstrationTests.DataAccess.FileReaders
{
    [TestFixture]
    public class JsonFileReaderTests
    {
        private string _testDataPath = "../../TestData/"; 

        [Test]
        public void ReadFile_ValidFilePath_ReturnsStream()
        {
            // Arrange
            var jsonFilePath = Path.Combine(_testDataPath, "validjsondata.json");
            var mockFileSystem = new Mock<>();
            var mockFile = new Mock<>();
            mockFileSystem.Setup(fs => fs.File).Returns(mockFile.Object);
            var jsonFileReader = new JsonFileReader(jsonFilePath, mockFileSystem.Object);

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
            var mockFileSystem = new Mock<>();
            var mockFile = new Mock<>();
            mockFileSystem.Setup(fs => fs.File).Returns(mockFile.Object);
            var jsonFileReader = new JsonFileReader(invalidFilePath, mockFileSystem.Object);

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => jsonFileReader.ReadFile());
        }

        [Test]
        public void ReadFile_InvalidFilePath_ThrowsArgumentException()
        {
            // Arrange
            var invalidFilePath = "../../invalidjsondata.json";
            var mockFileSystem = new Mock<>();
            var mockFile = new Mock<>();
            mockFileSystem.Setup(fs => fs.File).Returns(mockFile.Object);
            var jsonFileReader = new JsonFileReader(invalidFilePath, mockFileSystem.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => jsonFileReader.ReadFile());
        }
    }
}