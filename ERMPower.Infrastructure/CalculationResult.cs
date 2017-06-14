using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMPower.Infrastructure
{
    public class CalculationResult<T> : Result<T>
    {
        private readonly CalculationException _exception;

        public CalculationResult(T data) : base(data)
        {
        }

        public CalculationException CalculationException
        {
            get { return _exception; }
        }

        public override ResultStatus Status
        {
            get { return _exception == null ? ResultStatus.Success : ResultStatus.Failure; }
        }

        public CalculationResult(T result, CalculationException exception) : base(result)
        {
            _exception = exception;
        }
    }
}
