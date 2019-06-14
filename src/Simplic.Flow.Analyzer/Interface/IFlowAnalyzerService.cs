using System.Collections.Generic;

namespace Simplic.Flow.Analyzer
{
    public interface IFlowAnalyzerService
    {
        bool AnalyzeAll(IList<Flow> flowList);
    }
}
