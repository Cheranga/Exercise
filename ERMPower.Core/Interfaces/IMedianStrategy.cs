using System.Collections.Generic;

namespace ERMPower.Core.Interfaces
{
    public interface IMedianStrategy
    {
        CalculationResult<decimal> GetMedian(IReadOnlyCollection<decimal> collection);
    }
}
