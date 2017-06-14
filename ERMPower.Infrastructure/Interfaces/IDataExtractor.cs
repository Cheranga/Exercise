using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMPower.Infrastructure.Interfaces
{
    public interface IDataExtractor<T> where T:class
    {
        IEnumerable<T> Get(FileData fileData);
    }
}
