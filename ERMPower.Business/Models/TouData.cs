using System;
using System.IO;
using ERMPower.Core.Interfaces;

namespace ERMPower.Business.Models
{
    public class TouData : IDisplayData
    {
        //
        //  NOTE
        //
        //  1.  Didn't implement any inheritance chain because although TouData might look like a specialised version of "LpData", but in reality
        //      it might not be.
        //
        public string FilePath { get; set; }
        public string MeterPointCode { get; set; }
        public string SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public DateTime Date { get; set; }
        public string DataType { get; set; }
        public decimal Energy { get; set; }
        public decimal MaximumDemand { get; set; }
        public DateTime TimeOfMaxDemand { get; set; }
        public string Units { get; set; }
        public string Status { get; set; }
        public string Period { get; set; }
        public string DlsActive { get; set; }
        public int BillingResetCount { get; set; }
        public DateTime BillingResetDateTime { get; set; }
        public string Rate { get; set; }

        public string FileName
        {
            get { return string.IsNullOrEmpty(FilePath) ? string.Empty : Path.GetFileNameWithoutExtension(FilePath); }
        }

        public string Display()
        {
            return string.Format("{0} {1,-23} {2,10}", Date.ToString("dd/MM/yyyy HH:mm:ss"), Energy, FileName);
        }
    }
}