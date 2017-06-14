using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ERMPower.Core;
using ERMPower.Core.Interfaces;

namespace ERMPower.Infrastructure
{
    public class AsyncFileLoader : IFileDataLoader
    {
        public async Task<IEnumerable<FileData>> GetFileData(string searchPatterns, string[] directoryLocations)
        {
            if (string.IsNullOrEmpty(searchPatterns))
            {
                return null;
            }

            if (directoryLocations == null || directoryLocations.Any() == false)
            {
                return null;
            }

            var allFileLocations = directoryLocations.SelectMany(directory => Directory.GetFiles(directory, searchPatterns));
            var fileReadTasks = allFileLocations.Select(ReadFileAsync);

            return await Task.WhenAll(fileReadTasks);
        }

        private async Task<FileData> ReadFileAsync(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var content = await streamReader.ReadToEndAsync();
                    return new FileData
                    {
                        FilePath = filePath,
                        FileContent = content
                    };
                }
            }
        }
    }
}