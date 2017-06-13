using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ERMPower.ConsoleApp;

namespace ERMPower.Infrastructure.Calculations
{
    public interface IMedianStrategy<T>
    {
        //MedianSummary<T> GetMedianSummary<T>(decimal percentage, IReadOnlyCollection<T> collection, Func<T, decimal> propertyFunc) where T : class;

        Result<T> GetMedian(IReadOnlyCollection<T> collection);
    }
}
