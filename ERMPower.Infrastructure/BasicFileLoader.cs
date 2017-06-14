using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ERMPower.Core;
using ERMPower.Core.Interfaces;

namespace ERMPower.Infrastructure
{
    public class BasicFileLoader : IFileDataLoader
    {
        public Task<IEnumerable<FileData>> GetFileData(string searchPatterns, string[] directoryLocations)
        {
            if (string.IsNullOrEmpty(searchPatterns))
            {
                return Task.FromResult<IEnumerable<FileData>>(null);
            }

            if (directoryLocations == null || directoryLocations.Any() == false)
            {
                return Task.FromResult <IEnumerable<FileData>>(null);
            }

            var fileDataInAllDirectories = directoryLocations.SelectMany(directoryPath =>
            {
                var files = Directory.GetFiles(directoryPath, searchPatterns);
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
