namespace ProgrammingInCSharp.UsingBufferedStream
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Text;
    using System.Threading.Tasks;

    public class UsingBufferedStream
    {
        private const string FilePath = "../../output.txt";

        private static readonly Stopwatch stopWatch = new Stopwatch();

        private static void Main()
        {
            try
            {
                Console.WriteLine("Creating text file...");
                stopWatch.Start();

                var fileInfo = UsingBufferedStream.CreateFile(UsingBufferedStream.FilePath, Guid.NewGuid().ToString(), 20000000);

                stopWatch.Stop();
                Console.WriteLine("Text file was created. Elapsed time: {0}", stopWatch.Elapsed);
                Console.WriteLine("File's length: {0} MBs\n", fileInfo.Length / 1024 / 1024);
            }
            finally
            {
                UsingBufferedStream.DeleteFileIfExists(UsingBufferedStream.FilePath);
            }
        }

        private static FileInfo CreateFile(string path, string textToApply, int timesToApply)
        {
            var fileInfo = new FileInfo(path);

            using (FileStream fileStream = File.Create(fileInfo.FullName))
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    using (StreamWriter streamWriter = new StreamWriter(bufferedStream))
                    {
                        for (int i = 0; i < timesToApply; i++)
                        {
                            streamWriter.Write(textToApply);
                        }
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