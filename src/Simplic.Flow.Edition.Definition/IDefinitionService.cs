using System.Collections.Generic;
using System.Reflection;

namespace Simplic.Flow.Editor.Definition
{
    public interface IDefinitionService
    {
        IList<NodeDefinition> Create(IList<Assembly> assemblies);
    }
}
