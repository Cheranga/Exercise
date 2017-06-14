using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ERMPower.Infrastructure.Interfaces;

namespace ERMPower.Infrastructure
{
    public class BasicFileLoader : IFileDataLoader
    {
        public Task<IEnumerable<FileData>> GetFileData(params string[] directoryLocations)
        {
            if (directoryLocations == null || directoryLocations.Any() == false)
            {
                return Task.FromResult <IEnumerable<FileData>>(null);
            }

            var fileDataInAllDirectories = directoryLocations.SelectMany(directoryPath =>
            {
                var files = Directory.GetFiles(directoryPath);
                var fileDataInDirectory = files.Select(filePath =>
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (var streamReader = new StreamReader(fileStream))
                        {
                            var fileContent = streamReader.ReadToEnd();
                            return new FileData
                            {
                                FilePath = filePath,
                                FileContent = fileContent
                            };
                        }
                    }
                });

                return fileDataInDirectory;
            });

            return Task.FromResult(fileDataInAllDirectories);
        }
    }
}
