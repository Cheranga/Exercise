using System;
using System.Diagnostics;
using System.IO;

namespace ERMPower.Business.Models
{
    [DebuggerDisplay("{FileName} :: {DataValue}")]
    public class LpData
    {
        public string FilePath { get; set; }
        public string MeterPointCode { get; set; }
        public string SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public DateTime Date { get; set; }
        public string DataType { get; set; }
        public decimal DataValue { get; set; }
        public string Units { get; set; }
        public string Status { get; set; }

        public string FileName
        {
            get { return string.IsNullOrEmpty(FilePath) ? string.Empty : Path.GetFileNameWithoutExtension(FilePath); }
        }
    }
}
