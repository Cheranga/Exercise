using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERMPower.Infrastructure.Calculations
{
    public interface IMedianStrategy
    {
        MedianSummary<T> GetMedianSummary<T>(decimal percentage, IReadOnlyCollection<T> collection, Func<T, decimal> propertyFunc) where T : class;
    }
}
