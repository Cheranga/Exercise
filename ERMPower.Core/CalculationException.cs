using System;

namespace ERMPower.Core
{
    public class CalculationException : Exception
    {
        public string Name { get; set; }

        public CalculationException(string message) : base(message)
        {
            
        }
    }
}
