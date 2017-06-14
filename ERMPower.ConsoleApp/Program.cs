using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using Autofac.Core;
using ERMPower.Business.Models;
using ERMPower.Core;
using ERMPower.Core.Interfaces;

namespace ERMPower.ConsoleApp
{

    class Program
    {
        private const decimal DefaultMedianPercentage = 20;


        static void Main(string[] args)
        {
            //
            // Register the dependencies
            //
            Bootstrap.Instance.RegisterDependencies();
            //
            // TODO: Add a class to the Business project which take care of the following operations which the application will call
            //
            // Get the file handler meta data
            //
            var fileHandlers = GetFileHandlerMetaData();

            var fileLoader = Bootstrap.Instance.Get<IFileDataLoader>();
            var lpDataList = new List<LpData>();

            foreach (var fileHandler in fileHandlers)
            {
                var fileDataList = fileLoader.GetFileData(string.Format("{0}*.{1}", fileHandler.FilePrefix, fileHandler.Extension), fileHandler.Locations).Result;

                switch (fileHandler.FileType.ToUpper())
                {
                    case "LP":
                        var lpProcessor = Bootstrap.Instance.Get<IFileProcessor<LpData>>();
                        lpDataList.AddRange(lpProcessor.Get(fileDataList));
                        var lpMedianResult = GetMedianResult(new ReadOnlyCollection<decimal>(lpDataList.Select(data => data.DataValue).ToList()));
                        if (lpMedianResult.Status == ResultStatus.Success)
                        {
                            PrintMedian(lpMedianResult.Data, lpDataList, lpData=> lpData.DataValue);
                        }
                        break;

                        // TODO: Provide support to TOU files
                        //case "TOU":
                        //    break;
                }
            }


            System.Console.ReadLine();

        }

        private static void PrintMedian<T>(decimal median, List<T> lpDataList, Func<T, decimal> getValueFunc) where T : IDisplayData
        {
            Console.BufferHeight = short.MaxValue - 1;

            var medianPercentageConfigValue = ConfigurationManager.AppSettings.Get("MedianPercentage");
            decimal medianPercentage;
            if (decimal.TryParse(medianPercentageConfigValue, out medianPercentage) == false)
            {
                medianPercentage = DefaultMedianPercentage;
            }
            var rangeValue = (median * medianPercentage) / 100.0m;

            var highAbnormalValue = median + rangeValue;
            var lowAbnormalValue = median - rangeValue;

            const string displayFormat = @"{{{0}}} {{{1,-8}}}";
            var lowerAbnormalities = lpDataList.Where(data=>getValueFunc(data) < lowAbnormalValue).ToList();
            var higherAbnormalities = lpDataList.Where(data => getValueFunc(data) > highAbnormalValue).ToList();

            if (lowerAbnormalities.Any())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Lower Abnormalities");

                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var abnormal in lowerAbnormalities)
                {
                    Console.WriteLine(displayFormat, ((IDisplayData)(abnormal)).Display(), median);
                }

                Console.ResetColor();
            }

            if (higherAbnormalities.Any())
            {
                Console.WriteLine("\n\n==========================\n\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Higher Abnormalities");

                Console.ForegroundColor = ConsoleColor.Red;
                foreach (var abnormal in higherAbnormalities)
                {
                    Console.WriteLine(displayFormat, ((IDisplayData)(abnormal)).Display(), median);
                }

                Console.ResetColor();
            }
        }

        static CalculationResult<decimal> GetMedianResult(IEnumerable<decimal> values)
        {
            var medianCalculator = Bootstrap.Instance.Get<IMedianStrategy>(); //new DefaultMedianStrategy();
            var lpMedianResult = medianCalculator.GetMedian(new ReadOnlyCollection<decimal>(values.ToList()));

            return lpMedianResult;
        }

        static IEnumerable<FileHandlerMetaData> GetFileHandlerMetaData()
        {
            var customConfig = ConfigurationManager.AppSettings.Get("SupportedTypes");
            if (customConfig == null)
            {
                throw new NotSupportedException("Please provide the supported file types");
            }

            var configData = customConfig.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

            var fileHandlers = configData.Select(config =>
            {
                var data = config.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                //
                // TODO: Perform validation and all
                //
                return new FileHandlerMetaData
                {
                    FileType = data[0],
                    FilePrefix = data[1],
                    Extension = data[2],
                    TypeNameResponsibleFor = data[3],
                    Locations = data[4].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                };
            });

            return fileHandlers;

        }
    }


    public class FileHandlerMetaData
    {
        public string FileType { get; set; }
        public string FilePrefix { get; set; }
        public string Extension { get; set; }
        public string TypeNameResponsibleFor { get; set; }
        public string[] Locations { get; set; }
    }


}


