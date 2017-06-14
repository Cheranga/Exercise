using System;
using ERMPower.Infrastructure;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ERMPower.Business.DataExtractors;
using ERMPower.Business.FileProcessors;
using ERMPower.Business.Models;
using ERMPower.Infrastructure.Calculations.Median;

namespace ERMPower.ConsoleApp
{
    class Program
    {
        private const decimal DefaultMedianPercentage = 20;


        static void Main(string[] args)
        {
            //var directoryLocation = ConfigurationManager.AppSettings.Get("DirectoryPath");
            //new FileLoader().GetFiles(directoryLocation).Wait();

            //System.Console.ReadLine();

            var directoryLocationsConfigValue = ConfigurationManager.AppSettings.Get("DirectoryPath");
            var supportedTypesConfigValue = ConfigurationManager.AppSettings.Get("SupportedTypes");
            var medianPercentageConfigValue = ConfigurationManager.AppSettings.Get("MedianPercentage");

            decimal medianPercentage;
            if (decimal.TryParse(medianPercentageConfigValue, out medianPercentage) == false)
            {
                medianPercentage = DefaultMedianPercentage;
            }

            var directoryLocations = directoryLocationsConfigValue.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            var searchPatterns = supportedTypesConfigValue.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            

            var fileLoader = new BasicFileLoader();
            var lpProcessor = new LpProcessor(new LpDataExtractor());
            var lpDataList = new List<LpData>();

            foreach (var searchPattern in searchPatterns)
            {
                var fileDataList = fileLoader.GetFileData(string.Format("{0}*.csv",searchPattern), directoryLocations).Result;

                switch (searchPattern.ToUpper())
                {
                    case "LP":
                        lpDataList.AddRange(lpProcessor.Get(fileDataList));
                        break;

                    // TODO: Provide support to TOU files
                    //case "TOU":
                    //    fullTouDataList.AddRange(touProcessor.Get(fileDataList));
                    //    break;
                }
            }

            var medianCalculator = new DefaultMedianStrategy();
            var lpMedianResult = medianCalculator.GetMedian(new ReadOnlyCollection<decimal>(lpDataList.Select(data => data.DataValue).ToList()));

            if (lpMedianResult.Status == ResultStatus.Success)
            {
                var median = lpMedianResult.Data;
                var rangeValue = (median*medianPercentage)/100.0m;

                var highAbnormalValue = median + rangeValue;
                var lowAbnormalValue = median - rangeValue;

                var abnormallpDataList = lpDataList.Where(data => data.DataValue < lowAbnormalValue || data.DataValue > highAbnormalValue).ToList();
                //
                // Printing abnormal values
                //
                if (abnormallpDataList.Any())
                {
                    var displayFormat = @"{{{0}}} {{{1}}} {{{2}}} {{{3}}}";

                    foreach (var abnormal in abnormallpDataList)
                    {
                        Console.WriteLine(displayFormat, abnormal.FileName, abnormal.Date, abnormal.DataValue, median);
                    }

                }
            }

            

            System.Console.ReadLine();


        }
    }
}
