using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERMPower.Business.Models;
using ERMPower.Infrastructure;
using ERMPower.Infrastructure.Interfaces;

namespace ERMPower.Business.FileProcessors
{
    public class LpProcessor
    {
        private readonly IDataExtractor<LpData> _dataExtractor;

        public LpProcessor(IDataExtractor<LpData> dataExtractor )
        {
            _dataExtractor = dataExtractor;
        }

        public IEnumerable<LpData> Get(IEnumerable<FileData> fileDataCollection)
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
