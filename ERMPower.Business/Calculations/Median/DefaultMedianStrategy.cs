using System;
using System.Collections.Generic;
using System.Linq;
using ERMPower.ConsoleApp;
using ERMPower.Infrastructure.Calculations;

namespace ERMPower.Business.Calculations.Median
{


    public class DefaultMedianStrategy : IMedianStrategy<decimal>
    {
        protected virtual int RoundedNumOfDecimalPlaces 
        {
            get { return 2; }
        }

        public Result<decimal> GetMedian(IReadOnlyCollection<decimal> collection)
        {
            if (collection == null || collection.Any() == false)
            {
                throw new Exception("[collection] is either NULL or empty");
            }
            //
            // A basic calculation of median
            //
            var numberCount = collection.Count();
            var halfIndex = numberCount / 2;
            var dataPoints = collection.AsParallel().OrderBy(x=>x).ToList();

            decimal median;
            if ((numberCount % 2) == 0)
            {
                median = (dataPoints[halfIndex] + dataPoints[halfIndex - 1]) / 2;
            }
            else
            {
                median = dataPoints[halfIndex];
            }

            return new Result<decimal>(median);
        }

        //public MedianSummary<T> GetMedianSummary<T>(decimal percentage, IReadOnlyCollection<T> collection,
        //    Func<T, decimal> propertyFunc) where T : class
        //{
        //    if (percentage < 0)
        //    {
        //        throw new ArgumentOutOfRangeException("percentage", "normalization cannot be negative");
        //    }

        //    if (collection == null || collection.Any() == false)
        //    {
        //        throw new Exception("[collection] is either NULL or empty");
        //    }

        //    if (propertyFunc == null)
        //    {
        //        throw new NullReferenceException("Provide the property expression to get extract the data from");
        //    }
        //    //
        //    // If there's only one item, no need to calculate
        //    //
        //    if (collection.Count == 1)
        //    {
        //        return new MedianSummary<T>
        //        {
        //            Percentage = percentage,
        //            Median = collection.Select(propertyFunc).First(),
        //            HigherAbnormalities = null,
        //            LowerAbnormalities = null
        //        };
        //    }
        //    //
        //    // A basic calculation of median
        //    //
        //    var numberCount = collection.Count();
        //    var halfIndex = numberCount/2;
        //    var dataPoints = collection.AsParallel()
        //        .Select(propertyFunc)
        //        .OrderBy(x => x)
        //        .ToList();

        //    decimal median;
        //    if ((numberCount%2) == 0)
        //    {
        //        median = (dataPoints[halfIndex] + dataPoints[halfIndex - 1])/2;
        //    }
        //    else
        //    {
        //        median = dataPoints[halfIndex];
        //    }

        //    median = decimal.Round(median, RoundedNumOfDecimalPlaces);

        //    var medianPercentageValue = (median*percentage)/100;
        //    var higherAbnormalValue = decimal.Round(median + medianPercentageValue, RoundedNumOfDecimalPlaces);
        //    var lowerAbnormalValue = decimal.Round(median - medianPercentageValue, RoundedNumOfDecimalPlaces);

        //    var higherAbnormalities = collection.AsParallel()
        //        .Where(x => propertyFunc(x) > higherAbnormalValue)
        //        .ToList();

        //    var lowerAbnormalities = collection.AsParallel()
        //        .Where(x => propertyFunc(x) < lowerAbnormalValue)
        //        .ToList();

        //    return new MedianSummary<T>
        //    {
        //        Percentage = percentage,
        //        Median = median,
        //        HigherAbnormalities = higherAbnormalities,
        //        LowerAbnormalities = lowerAbnormalities
        //    };
        //}
    }
}