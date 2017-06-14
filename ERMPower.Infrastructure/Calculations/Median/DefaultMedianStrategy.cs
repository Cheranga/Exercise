using System.Collections.Generic;
using System.Linq;
using ERMPower.Core;
using ERMPower.Core.Interfaces;

namespace ERMPower.Infrastructure.Calculations.Median
{
    public class DefaultMedianStrategy : IMedianStrategy
    {
        public CalculationResult<decimal> GetMedian(IReadOnlyCollection<decimal> collection)
        {
            if (collection == null || collection.Any() == false)
            {
                return new CalculationResult<decimal>(0,new CalculationException("[collection] is NULL or empty"));
            }
            //
            // If there's only one element, it's the median
            //
            if (collection.Count == 1)
            {
                return new CalculationResult<decimal>(collection.First(), null);
            }
            //
            // A basic calculation of median
            //
            var numberCount = collection.Count;
            var halfIndex = numberCount/2;
            var dataPoints = collection.AsParallel().OrderBy(x => x).ToList();
            var isCountEven = numberCount%2 == 0;
            decimal median;

            if (isCountEven)
            {
                median = (dataPoints[halfIndex] + dataPoints[halfIndex - 1])/2;
            }
            else
            {
                median = dataPoints[halfIndex];
            }

            return new CalculationResult<decimal>(median, null);

        }
    }
}
