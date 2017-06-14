using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERMPower.Core.Interfaces
{
    public interface IFileDataLoader
    {
        Task<IEnumerable<FileData>> GetFileData(string searchPatterns, string[] directoryLocations);
    }
}
