using System;
using System.Diagnostics;
using System.IO;
using ERMPower.Core.Interfaces;

namespace ERMPower.Business.Models
{
    //
    // TODO: Maybe create a view model for this, since these will be internal structures
    //
    [DebuggerDisplay("{FileName} :: {DataValue}")]
    public class LpData : IDisplayData
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

        public string Display()
        {
            return string.Format("{{{0}}} {{{1,-20}}} {{{2,10}}}", FileName, Date.ToString("dd/MM/yyyy HH:mm:ss"), DataValue);
        }
    }
}
