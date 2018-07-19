using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Configuration
{
    public class FlowConfigurationModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Configuration { get; set; }
    }
}
