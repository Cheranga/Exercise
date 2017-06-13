using System.Collections.Generic;

namespace ERMPower.Infrastructure.Calculations
{
    public class MedianSummary<T> where T:class 
    {
        public decimal Percentage { get; set; }
        public decimal Median { get; set; }
        public IEnumerable<T> LowerAbnormalities { get; set; }
        public IEnumerable<T> HigherAbnormalities { get; set; }
    }
}