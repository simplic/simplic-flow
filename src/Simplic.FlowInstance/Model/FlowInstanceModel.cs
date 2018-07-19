using Newtonsoft.Json;
using System;

namespace Simplic.FlowInstance
{
    public class FlowInstanceModel
    {
        public Guid Id { get; set; }
        public byte[] Data { get; set; }
        public bool IsAlive { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
