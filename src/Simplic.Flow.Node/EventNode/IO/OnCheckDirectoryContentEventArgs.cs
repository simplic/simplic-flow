using Simplic.Flow.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Node.IO
{
    public class OnCheckDirectoryContentEventArgs : FlowEventArgs
    {
        public string DirectoryPath { get; set; }
    }
}
