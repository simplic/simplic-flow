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
            // ==================================================================
            var flow = new Flow();
            flow.Id = Guid.NewGuid();

            var evt = flow.CreateNode<OnDocumentScannedNode>();
            var evt2 = flow.CreateNode<OnDocumentScannedNode>();
            evt.IsStartEvent = true;

            var seq = flow.CreateNode<SequenceNode>();
            var print1 = flow.CreateNode<ConsoleWriteLineNode>();
            var print2 = flow.CreateNode<ConsoleWriteLineNode>();
            var print3 = flow.CreateNode<ConsoleWriteLineNode>();

            // Flow direction
            evt.FlowOut = seq;
            seq.FlowOutNodes.Add(print1);
            seq.FlowOutNodes.Add(print2);

            print2.FlowOut = evt2;
            evt2.FlowOut = print3;

            // Data flow
            print1.ToPrint = evt.DocumentOut;
            print2.ToPrint = evt.DocumentOut;
            print3.ToPrint = evt.DocumentOut;
            // ==================================================================

            // ==================================================================
            var engine = new FlowEngineService();
            engine.Flows.Add(flow);

            engine.RefreshEventDelegates();

            engine.EnqueueEvent(new Event.FlowEventArgs { EventName = nameof(OnDocumentScannedNode), ObjectId = Guid.Empty });

            engine.Run();

            engine.EnqueueEvent(new Event.FlowEventArgs { EventName = nameof(OnDocumentScannedNode), ObjectId = Guid.Empty });
            engine.Run();
            // ==================================================================

            System.Console.ReadLine();
        }
    }
}
