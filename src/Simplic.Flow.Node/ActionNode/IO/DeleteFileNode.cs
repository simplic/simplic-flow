using System;
using System.IO;

namespace Simplic.Flow.Node.IO
{
    [ActionNodeDefinition(DisplayName = "Delete File", Name = "DeleteFileNode", Category = "IO")]
    public class DeleteFileNode : ActionNode
    {
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            try
            {
                File.Delete(scope.GetValue<string>(InPinFilePath));

                if (OutNode != null)
                    runtime.EnqueueNode(OutNode, scope);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Removing file failed {ex}");

                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }

            return true;
        }

        [FlowPinDefinition(DisplayName = "Out", Name = "OutNode", PinDirection = PinDirection.Out)]
        public ActionNode OutNode { get; set; }

        [FlowPinDefinition(DisplayName = "Failed", Name = "OutNodeFailed", PinDirection = PinDirection.Out)]
        public ActionNode OutNodeFailed { get; set; }

        [DataPinDefinition(
            Id = "701a7e15-9ed0-4fe2-a641-031c461b1aaf",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = "InPinFilePath",
            DisplayName = "File Path")]
        public DataPin InPinFilePath { get; set; }

        public override string FriendlyName
        {
            get
            {
                return nameof(DeleteFileNode);
            }
        }

        public override string Name
        {
            get
            {
                return nameof(DeleteFileNode);
            }
        }
    }
}
