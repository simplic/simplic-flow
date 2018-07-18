using System.Collections.Generic;
using Unity;

namespace Simplic.Flow.Analyzer.Service
{
    public class FlowAnalyzerService : IFlowAnalyzerService
    {
        private readonly IUnityContainer unityContainer;

        public FlowAnalyzerService()
        {
            unityContainer = new UnityContainer();
            unityContainer.RegisterType<IFlowAnalyzer, FlowAnalyzer>();
            unityContainer.RegisterType<IFlowNodeAnalyzer, FlowNodeAnalyzer>();
        }

        public bool AnalyzeAll(IList<Flow> flowList)
        {
            return false;
        }
    }
}
