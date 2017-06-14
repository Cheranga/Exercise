using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMPower.Infrastructure.Interfaces
{
    public interface IFileDataLoader
    {
        Task<IEnumerable<FileData>> GetFileData(params string[] directoryLocations);
    }
}
