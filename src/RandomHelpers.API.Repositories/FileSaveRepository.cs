using RandomHelpers.API.Template.Interfaces.Repositories;
using System;
using System.IO;

namespace RandomHelpers.API.Repositories
{
    public class FileSaveRepository : IFileSaveRepository
    {
        private static readonly string exceptionFileName = $"ExceptionLog_{DateTime.Now.Ticks}.txt";

        public FileSaveRepository()
        {
        }

        public void SaveFileToDisk(byte[] file, string filePath, string fileName, bool isException)
        {
            if (!isException)
            {
                FileInfo htmlFileInfo = new FileInfo(string.Concat(filePath, fileName, ".html"));
                using (FileStream stream = htmlFileInfo.OpenWrite())
                {
                    stream.Write(file, 0, file.Length);
                    stream.Flush();
                    return;
                }
            }

            FileInfo exceptionFileInfo = new FileInfo(string.Concat(filePath, exceptionFileName));
            using (FileStream stream = exceptionFileInfo.OpenWrite())
            {
                stream.Write(file, 0, file.Length);
                stream.Flush();
                return;
            }
        }
    }
}
