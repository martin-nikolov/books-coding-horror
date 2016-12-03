namespace DataAccess.EncodingDecoding
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Text;

    public class EncodingDecoding
    {
        private const string FilePath = "../../output.txt";
        private const string CompressedFilePath = "../../output_compressed.gz";

        private static readonly Stopwatch stopWatch = new Stopwatch();

        private static void Main()
        {
            try
            {
                Console.WriteLine("Creating text file...");
                stopWatch.Start();

                var fileInfo = EncodingDecoding.CreateFile(EncodingDecoding.FilePath, Guid.NewGuid().ToString(), 20000000);

                stopWatch.Stop();
                Console.WriteLine("Text file was created. Elapsed time: {0}", stopWatch.Elapsed);
                Console.WriteLine("File's length: {0} MBs\n", fileInfo.Length / 1024 / 1024);

                Console.WriteLine("Compressing data...");
                stopWatch.Reset();
                stopWatch.Start();

                var compressedFileInfo = EncodingDecoding.CompressData(EncodingDecoding.FilePath, EncodingDecoding.CompressedFilePath); 
                     
                stopWatch.Stop();
                Console.WriteLine("Data was compressed. Elapsed time: {0}", stopWatch.Elapsed);
                Console.WriteLine("Compressed file's length: {0} MBs\n", compressedFileInfo.Length / 1024 / 1024);
            }
            finally
            {
                EncodingDecoding.DeleteFileIfExists(EncodingDecoding.FilePath);
                EncodingDecoding.DeleteFileIfExists(EncodingDecoding.CompressedFilePath);
            }
        }

        private static FileInfo CreateFile(string path, string textToApply, int timesToApply)
        {
            var fileInfo = new FileInfo(path);

            using (var fileStream = File.CreateText(fileInfo.FullName))
            {
                for (int i = 0; i < timesToApply; i++)
                {
                    fileStream.Write(textToApply);
                }
            }

            return fileInfo;
        }

        private static FileInfo CompressData(string sourceFilePath, string destinationFilePath)
        {
            var fileInfo = new FileInfo(destinationFilePath);

            using (var uncompressedFileStream = File.OpenRead(sourceFilePath))
            {
                using (var compressedFileStream = File.Create(fileInfo.FullName))
                {
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        uncompressedFileStream.CopyTo(compressionStream);
                    }
                }
            }

            return fileInfo;
        }

        private static void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}