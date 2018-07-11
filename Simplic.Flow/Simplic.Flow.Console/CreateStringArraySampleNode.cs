using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Console
{
    public class CreateStringArraySampleNode : ActionNode
    {
        public CreateStringArraySampleNode()
        {
            StringArrayOut = new DataPin()
            {
                ContainerType = DataPinContainerType.List,
                DataType = typeof(string),
                Description = "Create a sample array of 10 entries",
                Id = Guid.NewGuid(),
                Owner = this,
                Name = "StringArrayOut",
                Direction = PinDirection.Out,
                FriendlyName = "Array out"
            };
        }

        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var sampleList = new List<string>();
            for (int i = 0; i < 10; i++)
                sampleList.Add($"Entry |{i}| >> {Guid.NewGuid()}");

            scope.SetValue(StringArrayOut, sampleList);
            runtime.EnqueueNode(FlowOut, scope);

            return true;
        }

        public DataPin StringArrayOut { get; set; }
        public ActionNode FlowOut { get; set; }

        public override string FriendlyName
        {
            get
            {
                return nameof(CreateStringArraySampleNode);
            }
        }
    }
}
