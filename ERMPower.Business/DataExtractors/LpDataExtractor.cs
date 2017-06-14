using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ERMPower.Business.Models;
using ERMPower.Core;
using ERMPower.Core.Interfaces;

namespace ERMPower.Business.DataExtractors
{
    public class LpDataExtractor : IDataExtractor<LpData>
    {
        //
        // Default values (TODO: All these values should be custom configurable)
        //
        private string[] _lineSeparator = new []{Environment.NewLine};
        private string[] _dataSeparator = new []{ "," };
        private string[] _dateTimeFormats = new [] { "dd/MM/yyyy HH:mm:ss" };//DateTimeFormatInfo.InvariantInfo.GetAllDateTimePatterns();
        private int _fileStartIndex = 0;

        protected virtual string[] LineSeparator
        {
            get { return _lineSeparator; }
            set { _lineSeparator = value; }
        }

        protected virtual string[] DataSeparator
        {
            get { return _dataSeparator; }
            set { _dataSeparator = value; }
        }

        protected virtual string[] DateTimeFormats
        {
            get { return _dateTimeFormats; }
            set { _dateTimeFormats = value; }
        }


        public virtual IEnumerable<LpData> Get(FileData fileData)
        {
            //
            // TODO: Make these data extraction configurable.
            //
            var csvLines = fileData.FileContent.Split(LineSeparator, StringSplitOptions.None);
            //
            // TODO: Perform validation
            //
            var collection = new BlockingCollection<LpData>();
            Parallel.ForEach(csvLines, (csvLine, status, index) =>
            {
                if (index > _fileStartIndex)
                {
                    var csvData = csvLine.Split(DataSeparator, StringSplitOptions.None);
                    var lpData = GetLpData(csvData);

                    if (lpData != null)
                    {
                        lpData.FilePath = fileData.FilePath;
                        collection.Add(lpData);
                    }
                }
            });

            return collection.ToList();
        }

        protected virtual bool IsArrayLengthCorrect(string[] csvData)
        {
            if (csvData == null || csvData.Any() == false)
            {
                return false;
            }

            return csvData.Length >= 8;
        }

        protected virtual bool IsDateValid(string dateValue)
        {
            if (string.IsNullOrEmpty(dateValue))
            {
                return false;
            }

            DateTime date;
            var status = DateTime.TryParseExact(dateValue, DateTimeFormats, null, DateTimeStyles.None, out date);
            
            return status;
        }

        protected virtual bool IsDisplayValueValid(string displayValue)
        {
            if (string.IsNullOrEmpty(displayValue))
            {
                return false;
            }

            decimal value;
            var status = decimal.TryParse(displayValue, out value);

            return status;
        }

        //
        //  TODO: 
        //  It's better if we could configure the validations to be performed on this as well. So this class can be responsible for data extraction only.
        //  Also we would not want to change this code, whenever there's a new validation or an existing validation changes
        //
        protected virtual bool IsValid(string[] csvData)
        {
            return IsArrayLengthCorrect(csvData) &&
                   IsDateValid(csvData[3]) &&
                   IsDisplayValueValid(csvData[5]);
        }

        private LpData GetLpData(string[] csvData)
        {
            
            //
            // TODO: Perform validation (length, data sanity checks etc...)
            //

            if (IsValid(csvData) == false)
            {
                return null;
            }

            var metaPointCode = csvData[0];
            var serialNumber = csvData[1];
            var plantCode = csvData[2];

            DateTime date;
            DateTime.TryParseExact(csvData[3], DateTimeFormats, null, DateTimeStyles.None, out date);

            var dataType = csvData[4];

            decimal dataValue;
            decimal.TryParse(csvData[5], out dataValue);

            var units = csvData[6];
            var status = csvData[7];

            var lpData = new LpData
            {
                MeterPointCode = metaPointCode,
                SerialNumber = serialNumber,
                PlantCode = plantCode,
                Date = date,
                DataType = dataType,
                DataValue = dataValue,
                Units = units,
                Status = status
            };

            return lpData;

        }
    }
}
