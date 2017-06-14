using System.Collections.Generic;
using System.Linq;
using ERMPower.Business.Models;
using ERMPower.Core;
using ERMPower.Core.Interfaces;

namespace ERMPower.Business.FileProcessors
{
    //
    // TODO: This code is same as the Lp processor, move it to a common class
    //

    public class TouProcessor : DefaultFileProcessor<TouData>
    {
        private readonly IDataExtractor<TouData> _dataExtractor;

        public TouProcessor(IDataExtractor<TouData> dataExtractor)
        {
            _dataExtractor = dataExtractor;
        }

        public IEnumerable<TouData> Get(IEnumerable<FileData> fileDataCollection)
        {
            var dataCollection = fileDataCollection as IList<FileData> ?? fileDataCollection.ToList();
            if (fileDataCollection == null || dataCollection.Any() == false)
            {
                return null;
            }

            var processedLpData = dataCollection.AsParallel().SelectMany(fileData => _dataExtractor.Get(fileData));
            return processedLpData;
        }
    }
}