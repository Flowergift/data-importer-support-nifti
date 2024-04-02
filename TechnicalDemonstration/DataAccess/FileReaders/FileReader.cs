using System;
using System.IO;

namespace Medica.Uk.TechnicalDemonstration.DataAccess.FileReaders
{
    public abstract class FileReader
    {
        protected int ChunkSize = 1024;
        protected string FilePath { get; set; }

        public FileReader(string filePath)
        {
            FilePath = filePath;
            if (FileExists())
            {
                var fileSize = new FileInfo(FilePath).Length;
                long availableMemory = GetAvailableMemory();
                ChunkSize = CalculateChunkSize(ChunkSize, fileSize, availableMemory);
            }
        }

        public abstract Stream ReadFile();

        protected virtual bool FileExists()
        {
            return File.Exists(FilePath);
        }

        protected long GetAvailableMemory()
        {
            var memoryInfo = GC.GetGCMemoryInfo();
            return memoryInfo.TotalAvailableMemoryBytes;
        }

        protected int CalculateChunkSize(int mChunkSize, long fileSize, long availableMemory)
        {
            int minChunkSize = mChunkSize * mChunkSize;
            const double maxMemoryUsage = 0.2;

            long maxChunkSize = (long)(availableMemory * maxMemoryUsage);
            long chunkSize = Math.Min(maxChunkSize, fileSize);

            // Ensure chunk size is not zero or negative
            if (chunkSize <= 0)
            {
                throw new InsufficientMemoryException("Insufficient memory to process the file. Consider increasing the available memory");
            }

            return (int)Math.Max(chunkSize, minChunkSize);
        }
    }
}