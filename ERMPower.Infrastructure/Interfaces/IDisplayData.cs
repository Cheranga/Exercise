using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMPower.Infrastructure
{
    public interface IDisplayData<T> where T:class
    {
        void ShowDisplayContent(T objectToDisplay);
    }
}
