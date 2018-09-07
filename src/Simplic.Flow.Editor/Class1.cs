using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Editor
{
    public static class NodeDefinitionResolver
    {
        static NodeDefinitionResolver()
        {
            
        }

        public static IList<NodeDefinition> Resolve()
        {
            var assembly = Assembly.LoadFrom(@"C:\dev\simplic-flow\src\packages\Simplic.Flow.Node.1.0.718.730\lib\net451\Simplic.Flow.Node.dll");
            var definitions = new List<NodeDefinition>();

            var types = assembly.GetTypes();            

            foreach (var type in types)
            {
                if (typeof(Simplic.Flow.BaseNode).IsAssignableFrom(type))                
                {
                    var properties = type.GetProperties();
                    foreach (var prop in properties)
                    {

                    }
                }
            }


            return definitions;
        }
    }
}
