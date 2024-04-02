using Medica.Uk.TechnicalDemonstration.DataAccess.FileReaders;
using Moq;
using System;
using System.IO;

namespace TechnicalDemonstrationTests.DataAccess.FileReaders
{
    [TestFixture]
    public class CsvFileReaderTests
    {
        private string _testDataPath = "../../TestData/";

        [Test]
        public void ReadFile_ValidFilePath_ReturnsStream()
        {
            // Arrange
            var csvFilePath = Path.Combine(_testDataPath, "validcsvdata.csv");
            var mockFileSystem = new Mock<>();
            var mockFile = new Mock<FileInfo>(csvFilePath);
            mockFile.Setup(f => f.Exists).Returns(true);
            mockFile.Setup(f => f.Length).Returns(1024); 

            mockFileSystem.Setup(fs => fs.File).Returns(mockFile.Object);

            var csvFileReader = new CsvFileReader(csvFilePath, mockFileSystem.Object);

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
            var mockFileSystem = new Mock<>();
            var mockFile = new Mock<FileInfo>(invalidFilePath);
            mockFile.Setup(f => f.Exists).Returns(false);

            mockFileSystem.Setup(fs => fs.File).Returns(mockFile.Object);

            var csvFileReader = new CsvFileReader(invalidFilePath, mockFileSystem.Object);

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => csvFileReader.ReadFile());
        }
    }
}