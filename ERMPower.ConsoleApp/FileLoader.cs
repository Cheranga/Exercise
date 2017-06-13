using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERMPower.ConsoleApp
{
    public class FileLoader
    {
        public async Task GetFiles(string directoryLocation)
        {
            var filePaths = Directory.GetFiles(directoryLocation);
            var fileReadTasks = filePaths.Select(async filePath=> await ReadFileAsync(filePath));

            try
            {
                //var fileContent = await Task.WhenAll(fileReadTasks);

                var fileContent = await Task.WhenAll(filePaths.Select(async filePath =>
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (var streamReader = new StreamReader(fileStream))
                        {
                            var content = await streamReader.ReadToEndAsync();
                            return content;
                        }
                    }
                }));

                var oneList = fileContent.AsParallel().Where(content => content.Contains("214612653")).ToList();

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