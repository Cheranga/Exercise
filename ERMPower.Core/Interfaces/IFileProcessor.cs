using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMPower.Core.Interfaces
{

    public interface IFileProcessor<T> where T : class
    {
        IEnumerable<T> Get(IEnumerable<FileData> fileDataCollection);
    }
}
