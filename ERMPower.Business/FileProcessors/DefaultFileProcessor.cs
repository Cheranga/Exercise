using System.Collections.Generic;
using System.Linq;
using ERMPower.Core;
using ERMPower.Core.Interfaces;

namespace ERMPower.Business.FileProcessors
{
    public class DefaultFileProcessor<T> : IFileProcessor<T> where T:class 
    {
        protected IDataExtractor<T> DataExtractor { get; set; }

        public DefaultFileProcessor(IDataExtractor<T> dataExtractor )
        {
            DataExtractor = dataExtractor;
        }

        public IEnumerable<T> Get(IEnumerable<FileData> fileDataCollection)
        {
            var dataCollection = fileDataCollection as IList<FileData> ?? fileDataCollection.ToList();
            if (fileDataCollection == null || dataCollection.Any() == false)
            {
                return null;
            }

            var processedLpData = dataCollection.AsParallel().SelectMany(fileData => DataExtractor.Get(fileData));
            return processedLpData;
        }
    }
}