﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        public virtual string FileType
        {
            get
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    return string.Empty;
                }

                var fileName = Path.GetFileNameWithoutExtension(FilePath);
                var underscoreIndex = fileName.IndexOf("_", StringComparison.OrdinalIgnoreCase);
                if (underscoreIndex < 0)
                {
                    return string.Empty;
                }

                return fileName.Substring(0, underscoreIndex);
            }
        }
    }
}
