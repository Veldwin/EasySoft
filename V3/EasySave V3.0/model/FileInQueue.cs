using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySaveApp.model
{
    internal class FileInQueue
    {
        public FileInfo MFileInfo { get; set; }
        public bool IsPrioritized { get; set; }

        public FileInQueue(FileInfo fileInfo, bool isPrioritized)
        {
            MFileInfo = fileInfo;
            IsPrioritized = isPrioritized;
        }
    }
}
