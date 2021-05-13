using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(Category = "IO", DisplayName = "Get file extension", Name = "GetFileExtensionNode", Tooltip = "Returns the file extension of a gien file")]
    public class GetFileExtensionNode : ActionNode
    {
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var filePath = scope.GetValue<string>(InPinFilePath);
            
            try
            {
                if (filePath != null)
                {
                    var extension = Path.GetExtension(filePath);

                    scope.SetValue(OutPinFileExtension, extension);

                    if (OutNode != null)
                        runtime.EnqueueNode(OutNode, scope);
                }
                else
                {
                    if (OutNodeFailed != null)
                        runtime.EnqueueNode(OutNodeFailed, scope);
                }
            }
            catch
            {
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
            Id = "4940d108-a1ce-4f5f-be25-3d96320db2fd",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = "InPinFilePath",
            DisplayName = "File Path")]
        public DataPin InPinFilePath { get; set; }

        [DataPinDefinition(
            Id = "4232966c-c38f-4578-a277-da0494ad344e",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.Out,
            Name = "OutPinFileExtension",
            DisplayName = "File extension")]
        public DataPin OutPinFileExtension { get; set; }

        public override string Name { get { return "GetFileExtensionNode"; } }

        public override string FriendlyName => "Get file extension";
    }
}
