using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERMPower.Business.Calculations.Median;
using ERMPower.Business.Models;

namespace ERMPower.UnitTests.Calculations
{
    [TestClass]
    public class DefaultMedianStrategyTests
    {
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        //public void WhenTheGivenPercentageIsNegativeMustThrowError()
        //{
        //    //
        //    // Arrange
        //    //
        //    var strategy = new DefaultMedianStrategy();
        //    var lpData = new List<LpData>
        //    {
        //        new LpData {DataValue = 10},
        //        new LpData {DataValue = 20},
        //        new LpData {DataValue = 30},
        //    };
        //    //
        //    // Act
        //    //
        //    strategy.GetMedianSummary(-1, lpData.AsReadOnly(), data => data.DataValue);
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WhenTheCollectionIsNullMustThrowError()
        {
            //
            // Arrange
            //
            var strategy = new DefaultMedianStrategy();
            List<LpData> lpData = null;
            //
            // Act
            //
            strategy.GetMedian(null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void WhenTheCollectionIsEmptyMustThrowError()
        {
            //
            // Arrange
            //
            var strategy = new DefaultMedianStrategy();
            var lpData = new List<LpData>();
            //
            // Act
            //
            var median =strategy.GetMedian(new ReadOnlyCollection<decimal>(lpData.Select(x=>x.DataValue).ToList()));
        }

        //[TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        //public void WhenThePropertyExpressionIsNullMustThrowError()
        //{
        //    //
        //    // Arrange
        //    //
        //    var strategy = new DefaultMedianStrategy();
        //    var lpData = new List<LpData>
        //    {
        //        new LpData {DataValue = 10},
        //        new LpData {DataValue = 20},
        //        new LpData {DataValue = 30},
        //    };
        //    //
        //    // Act
        //    //
        //    strategy.GetMedianSummary(10, lpData.AsReadOnly(), null);
        //}


        //[TestMethod]
        //public void WhenDataIsProperMustReturnTheMedianSummary()
        //{
        //    //
        //    // Arrange
        //    //
        //    var strategy = new DefaultMedianStrategy();
        //    var lpData = new List<LpData>
        //    {
        //        new LpData {DataValue = 60},
        //        new LpData {DataValue = 50},
        //        new LpData {DataValue = 40},
        //        new LpData {DataValue = 30},
        //        new LpData {DataValue = 20},
        //        new LpData {DataValue = 10},
        //    };
        //    //
        //    // Act
        //    //
        //    var medianSummary = strategy.GetMedianSummary(10, lpData.AsReadOnly(), data => data.DataValue);
        //    //
        //    // Assert
        //    //
        //    Assert.AreEqual(35, medianSummary.Median);
        //}
    }
}
