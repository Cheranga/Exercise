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
            var fileReadTasks = filePaths.Select(ReadFileAsync);

            try
            {
                var fileContent = await Task.WhenAll(fileReadTasks);

                //var oneList = fileContent.AsParallel().Where(content => (content, "1", StringComparison.OrdinalIgnoreCase)).ToList();
                var stopWatch = System.Diagnostics.Stopwatch.StartNew();
                var oneList = fileContent.AsParallel().Where(content => content.Contains("214612653")).ToList();
                stopWatch.Stop();

                

                Console.WriteLine("Elapsed {0}ms", stopWatch.Elapsed.Milliseconds);

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