using System;
using System.Diagnostics;

namespace ERMPower.Business.Models
{
    [DebuggerDisplay("{Date} :: {DataValue}")]
    public class LpData
    {
        public string MeterPointCode { get; set; }
        public string SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public DateTime Date { get; set; }
        public string DataType { get; set; }
        public decimal DataValue { get; set; }
        public string Units { get; set; }
        public string Status { get; set; }
    }
}
