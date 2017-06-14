using System;
using ERMPower.Infrastructure;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using Autofac;
using Autofac.Core;
using ERMPower.Business.DataExtractors;
using ERMPower.Business.FileProcessors;
using ERMPower.Business.Models;
using ERMPower.Infrastructure.Calculations.Median;
using ERMPower.Core;
using ERMPower.Core.Interfaces;

namespace ERMPower.ConsoleApp
{
    //
    //  This class is a temporary class to store the dependencies for demo purposes.
    //  Idealy we should use a DI resolver (like autofac or ninject), and share it across the projects. For that first need to create an interface
    //  in "ERMPower.Core" project and then projects which needs to be bootstrapped must have a class implementing that interface.
    //  The starter application (in this case the console app) will create the main container, and will be passing it across the projects to
    //  register their own dependencies
    //
    public class Bootstrap
    {
        private static Bootstrap _instance;

        private IDictionary<Type, object> _dependencies;
        private IContainer _container;

        public static Bootstrap Instance
        {
            get { return _instance ?? (_instance = new Bootstrap()); }
        }

        private Bootstrap()
        {
            _dependencies = new Dictionary<Type, object>();
            RegisterDependencies();
        }

        public void RegisterDependencies()
        {
            //
            //  TODO:
            //  This is temporary. There should be a custom attribute which will be decorated for types which are necessary to be recognizable to be bootstrapped.
            //  In here actually the application knows its dependencies, meaning it knows it by implementation :) 
            //  But ideally it should be just through abstractions
            //
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<BasicFileLoader>().AsImplementedInterfaces();
            containerBuilder.RegisterType<LpDataExtractor>().AsImplementedInterfaces();
            containerBuilder.RegisterGeneric(typeof (DefaultFileProcessor<>)).AsImplementedInterfaces();
            containerBuilder.RegisterType<DefaultMedianStrategy>().AsImplementedInterfaces();

            _container = containerBuilder.Build();
        }

        public T Get<T>()
        {
            var dependency = _container.Resolve<T>();
            return dependency;
        }
    }

    class Program
    {
        private const decimal DefaultMedianPercentage = 20;


        static void Main(string[] args)
        {
            //
            // Register the dependencies
            //
            Bootstrap.Instance.RegisterDependencies();

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
            

            var fileLoader = Bootstrap.Instance.Get<IFileDataLoader>(); //new BasicFileLoader();
            var lpProcessor = Bootstrap.Instance.Get<IFileProcessor<LpData>>(); //new LpProcessor(new LpDataExtractor());
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

            var medianCalculator = Bootstrap.Instance.Get<IMedianStrategy>(); //new DefaultMedianStrategy();
            var lpMedianResult = medianCalculator.GetMedian(new ReadOnlyCollection<decimal>(lpDataList.Select(data => data.DataValue).ToList()));

            Console.BufferHeight = short.MaxValue - 1;
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
                var displayFormat = @"{{{0}}} {{{1,-23}}} {{{2,10}}} {{{3,-8}}}";
                var lowerAbnormalities = lpDataList.Where(data => data.DataValue < lowAbnormalValue).ToList();
                var higherAbnormalities = lpDataList.Where(data => data.DataValue > highAbnormalValue).ToList();

                if (lowerAbnormalities.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Lower Abnormalities");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    foreach (var abnormal in lowerAbnormalities)
                    {
                        Console.WriteLine(displayFormat, abnormal.FileName, abnormal.Date.ToString("dd/MM/yyyy HH:mm:ss"), abnormal.DataValue, median);
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
                        Console.WriteLine(displayFormat, abnormal.FileName, abnormal.Date, abnormal.DataValue, median);
                    }

                    Console.ResetColor();
                }
            }

            

            System.Console.ReadLine();


        }
    }

    
}


