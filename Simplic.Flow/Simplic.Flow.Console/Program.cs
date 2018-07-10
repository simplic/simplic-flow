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
            var flow = new Flow();

            var evt = flow.CreateNode<OnDocumentScannedNode>();
            var seq = flow.CreateNode<SequenceNode>();

            evt.FlowOut = seq;

            var engine = new FlowEngineService();
            engine.Flows.Add(flow);

            engine.EnqueueEvent(new Event.FlowEventArgs { EventName = nameof(OnDocumentScannedNode), ObjectId = Guid.Empty });

            engine.Run();
            engine.Run();
            engine.Run();
            engine.Run();
        }
    }
}
