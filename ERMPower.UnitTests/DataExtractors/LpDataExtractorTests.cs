using System;
using System.Linq;
using ERMPower.Business.DataExtractors;
using ERMPower.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ERMPower.Core;

namespace ERMPower.Tests.DataExtractors
{
    [TestClass]
    public class LpDataExtractorTests
    {
        private const string Category = "Data Extractors";

        private LpDataExtractor _dataExtractor;
        private const string Header = @"MeterPoint Code,Serial Number,Plant Code,Date/Time,Data Type,Data Value,Units,Status";

        [TestInitialize]
        public void Init()
        {
            _dataExtractor = new LpDataExtractor();
        }

        private string GetLpContent(decimal value)
        {
            return string.Format(@"210095893,210095893,ED031000001,31/08/2015 08:45:00,Export varh Total,{0},kvarh,", value);
        }

        [TestCategory(Category)]
        [TestMethod]
        public void WhenValidDataIsProvidedMustReturnLpDataSuccessfully()
        {
            //
            // Arrange
            //
            var csvData = Enumerable.Range(1, 10000).Select(val => GetLpContent(val)).ToList();
            csvData.Insert(0, Header);
            var fileContent = string.Join(Environment.NewLine, csvData);
            var fileData = new FileData
            {
                FilePath = "Some File Path",
                FileContent = fileContent
            };
            //
            // Act
            //
            var lpFileData = _dataExtractor.Get(fileData);
            //
            // Assert
            //
            Assert.AreEqual(10000, lpFileData.Count());
            //
            // TODO: Get an object comparer like "Kellermen" and do the comparison
            //
            var lpData = lpFileData.First();
            Assert.AreEqual(lpData.MeterPointCode, "210095893");
            Assert.AreEqual(lpData.SerialNumber, "210095893");
            Assert.AreEqual(lpData.PlantCode, "ED031000001");
            Assert.AreEqual(lpData.Date, new DateTime(2015,8,31, 8,45,0));
            Assert.AreEqual(lpData.DataType, "Export varh Total");
            Assert.AreEqual(lpData.Units, "kvarh");
            Assert.AreEqual(lpData.Status, "");
        }
    }
}
