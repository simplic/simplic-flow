using Simplic.Flow.Editor.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.NodeGenerator
{
    public class ActionNodeGenerationDefinition : ActionNodeDefinition
    {
        public string MethodName { get; set; }
        public string Namespace { get; set; }
    }
}
