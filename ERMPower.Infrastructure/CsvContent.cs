using System.Diagnostics;

namespace ERMPower.Infrastructure
{
    [DebuggerDisplay("{FilePath}")]
    public class CsvContent
    {
        public string FilePath { get; set; }
        public string CsvLineData { get; set; }
    }
}