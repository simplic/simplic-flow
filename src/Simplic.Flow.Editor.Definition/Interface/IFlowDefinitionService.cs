using System.Collections.Generic;
using System.Reflection;

namespace Simplic.Flow.Editor.Definition
{
    public interface IFlowDefinitionService
    {
        IList<NodeDefinition> Create(IList<Assembly> assemblies);
    }
}
