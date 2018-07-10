using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var evt = new OnDocumentScannedNode();
            var seq = new SequenceNode();

            evt.FlowOut = seq;
        }
    }
}
