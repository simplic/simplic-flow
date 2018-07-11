using Simplic.Flow.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow
{
    public interface IFlowEngineService
    {
        void Run();
        void EnqueueEvent(FlowEventArgs args);
    }
}
