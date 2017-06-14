using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMPower.Infrastructure
{
    public class CalculationException : Exception
    {
        public string Name { get; set; }

        public CalculationException(string message) : base(message)
        {
            
        }
    }
}
