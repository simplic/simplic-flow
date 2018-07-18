namespace Simplic.Flow.Analyzer.Service
{
    public class FlowAnalyzer : IFlowAnalyzer
    {
        public bool Analyze(Flow flow)
        {
            return flow.Nodes.Count > 0;
        }
    }
}
