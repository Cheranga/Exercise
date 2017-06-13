using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Text;

namespace ERMPower.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var directoryLocation = ConfigurationManager.AppSettings.Get("DirectoryPath");
            new FileLoader().GetFiles(directoryLocation).Wait();

            System.Console.ReadLine();

        }
    }
}
