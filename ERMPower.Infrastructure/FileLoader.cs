using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERMPower.Infrastructure
{
    public class FileLoader
    {
        public async Task GetFiles(string directoryLocation)
        {
            var filePaths = Directory.GetFiles(directoryLocation);
            var fileReadTasks = filePaths.Select(ReadFileAsync);

            try
            {
                var fileContent = await Task.WhenAll(fileReadTasks);

            }
            catch (Exception exception)
            {
                
            }
        }

        private async Task<string> ReadFileAsync(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    var content = await streamReader.ReadToEndAsync();
                    return content;
                }
            }
        }
    }
}