using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ERMPower.Business.Models;
using ERMPower.Infrastructure.Calculations.Median;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERMPower.Core;

namespace ERMPower.Tests.Calculations
{
    //
    //  TODO: Add separate test projects
    //
    [TestClass]
    public class DefaultMedianStrategyTests
    {
        private const string Category = "Median Calculation";

        private DefaultMedianStrategy _strategy;

        [TestInitialize]
        public void Init()
        {
            _strategy = new DefaultMedianStrategy();

        }

        [TestCategory(Category)]
        [TestMethod]
        public void WhenTheCollectionIsNullMedianCalculatioMustFail()
        {
            // Act
            //
            var result = _strategy.GetMedian(null);
            //
            // Assert
            //
            Assert.AreEqual(result.Status, ResultStatus.Failure, "Must fail");
        }

        [TestCategory(Category)]
        [TestMethod]
        public void WhenTheCollectionIsEmptyMedianCalculationMustFail()
        {
            //
            // Arrange
            //
            var lpData = new List<LpData>();
            //
            // Act
            //
            var result = _strategy.GetMedian(new ReadOnlyCollection<decimal>(lpData.Select(x => x.DataValue).ToList()));
            //
            // Assert
            //
            Assert.AreEqual(result.Status, ResultStatus.Failure, "Must fail");
        }

        [TestCategory(Category)]
        [TestMethod]
        public void WhenEvenNumberOfSortedCollectionIsProvidedMedianCalculationMustBeSuccessful()
        {
            //
            // Arrange
            //
            var lpData = new List<LpData>
            {
                new LpData {DataValue = 10},
                new LpData {DataValue = 20},
                new LpData {DataValue = 30},
                new LpData {DataValue = 40},
                new LpData {DataValue = 50},
                new LpData {DataValue = 60}
            };
            //
            // Act
            //
            var result = _strategy.GetMedian(new ReadOnlyCollection<decimal>(lpData.Select(x => x.DataValue).ToList()));
            //
            // Assert
            //
            Assert.AreEqual(result.Status, ResultStatus.Success, "Must be success");
            Assert.AreEqual(result.Data, 35);
        }

        [TestCategory(Category)]
        [TestMethod]
        public void WhenEvenNumberOfUnSortedCollectionIsProvidedMedianCalculationMustBeSuccessful()
        {
            //
            // Arrange
            //
            var lpData = new List<LpData>
            {
                new LpData {DataValue = 10},
                new LpData {DataValue = 50},
                new LpData {DataValue = 60},
                new LpData {DataValue = 20},
                new LpData {DataValue = 40},
                new LpData {DataValue = 30}
            };
            //
            // Act
            //
            var result = _strategy.GetMedian(new ReadOnlyCollection<decimal>(lpData.Select(x => x.DataValue).ToList()));
            //
            // Assert
            //
            Assert.AreEqual(result.Status, ResultStatus.Success, "Must be success");
            Assert.AreEqual(result.Data, 35);
        }

        [TestCategory(Category)]
        [TestMethod]
        public void WhenOddNumberOfSortedCollectionIsProvidedMedianCalculationMustBeSuccessful()
        {
            //
            // Arrange
            //
            var lpData = new List<LpData>
            {
                new LpData {DataValue = 10},
                new LpData {DataValue = 20},
                new LpData {DataValue = 30},
                new LpData {DataValue = 40},
                new LpData {DataValue = 50},
                new LpData {DataValue = 60},
                new LpData {DataValue = 70}
            };
            //
            // Act
            //
            var result = _strategy.GetMedian(new ReadOnlyCollection<decimal>(lpData.Select(x => x.DataValue).ToList()));
            //
            // Assert
            //
            Assert.AreEqual(result.Status, ResultStatus.Success, "Must be success");
            Assert.AreEqual(result.Data, 40);
        }

        [TestCategory(Category)]
        [TestMethod]
        public void WhenOddNumberOfUnSortedCollectionIsProvidedMedianCalculationMustBeSuccessful()
        {
            //
            // Arrange
            //
            var lpData = new List<LpData>
            {
                new LpData {DataValue = 10},
                new LpData {DataValue = 50},
                new LpData {DataValue = 60},
                new LpData {DataValue = 20},
                new LpData {DataValue = 40},
                new LpData {DataValue = 30},
                new LpData {DataValue = 5}
            };
            //
            // Act
            //
            var result = _strategy.GetMedian(new ReadOnlyCollection<decimal>(lpData.Select(x => x.DataValue).ToList()));
            //
            // Assert
            //
            Assert.AreEqual(result.Status, ResultStatus.Success, "Must be success");
            Assert.AreEqual(result.Data, 30);
        }

        [TestCategory(Category)]
        [TestMethod]
        public void WhenThereIsOnlyOneItemInCollectionMedianCalculationMustBeSuccessful()
        {
            //
            // Arrange
            //
            var lpData = new List<LpData>
            {
                new LpData {DataValue = 10}
            };
            //
            // Act
            //
            var result = _strategy.GetMedian(new ReadOnlyCollection<decimal>(lpData.Select(x => x.DataValue).ToList()));
            //
            // Assert
            //
            Assert.AreEqual(result.Status, ResultStatus.Success, "Must be success");
            Assert.AreEqual(result.Data, 10);
        }
    }
}
