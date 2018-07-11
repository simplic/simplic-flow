using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow
{
    public class NodeScope<T> where T : Node
    {
        public T Node { get; set; }
        public ValueScope Scope { get; set; }
    }
}
