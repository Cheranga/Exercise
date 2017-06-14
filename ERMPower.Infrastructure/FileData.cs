using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMPower.Infrastructure
{
    [DebuggerDisplay("{FilePath}")]
    public class FileData
    {
        public string FilePath { get; set; }
        public string FileContent { get; set; }
    }
}
