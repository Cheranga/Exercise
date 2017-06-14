using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERMPower.Business.Models;
using ERMPower.Infrastructure;
using ERMPower.Infrastructure.Interfaces;

namespace ERMPower.Business.DataExtractors
{
    //
    //  TODO: Violation of DRY! 
    //  This will have similar logic as its peer "LpDataExtractor". 
    //  Much better approach would be a generic class which accepts a string array of data, and a map containing how to
    //  extract data, for the requested type parameter.
    //  Then it can extract those values and create the required object of the requested type. (single responsibility, and if needed we could override certain
    //  behaviours of it
    //
    public class TouDataExtractor : IDataExtractor<TouData>
    {
        //
        // TODO: Same as LpDataExtractor, only the data extracting part will be different
        //
        public IEnumerable<TouData> Get(FileData fileData)
        {
            throw new NotImplementedException();
        }
    }
}
