using System.Collections.Generic;

namespace ERMPower.Infrastructure.Interfaces
{
    public interface IMedianStrategy
    {
        //MedianSummary<T> GetMedianSummary<T>(decimal percentage, IReadOnlyCollection<T> collection, Func<T, decimal> propertyFunc) where T : class;

        CalculationResult<decimal?> GetMedian(IReadOnlyCollection<decimal> collection);
    }
}
