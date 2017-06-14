using System.Collections.Generic;

namespace ERMPower.Core.Interfaces
{
    public interface IDataExtractor<T> where T:class
    {
        IEnumerable<T> Get(FileData fileData);
    }
}
