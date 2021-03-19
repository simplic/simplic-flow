using System;
using System.IO;

namespace Simplic.Flow.Node.IO
{
    [ActionNodeDefinition(DisplayName = "Get Directory Content", Name = nameof(GetDirectoryContentNode), Category = "IO")]
    public class GetDirectoryContentNode : ActionNode
    {        
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var path = scope.GetValue<string>(InPinDirectoryPath);
            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    var childScope = scope.CreateChild();
                    childScope.SetValue(OutPinFilePath, file);

                    if (OutNodeEachFile != null)
                        runtime.EnqueueNode(OutNodeEachFile, childScope);
                }

                if (OutNodeCompleted != null)
                    runtime.EnqueueNode(OutNodeCompleted, scope);

                return true;
            }
            else
            {
                Console.WriteLine($"Could not find direcotry {path}");
                return false;
            }
        }

        [FlowPinDefinition(DisplayName = "Each File", Name = "OutNodeEachFile", PinDirection = PinDirection.Out)]
        public ActionNode OutNodeEachFile { get; set; }

        [FlowPinDefinition(DisplayName = "Completed", Name = "OutNodeCompleted", PinDirection = PinDirection.Out)]
        public ActionNode OutNodeCompleted { get; set; }

        [DataPinDefinition(
            Id = "9dd53fba-3a13-44d2-8f41-5abfd5820562",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.Out,
            Name = "OutPinFilePath",
            DisplayName = "File Path")]
        public DataPin OutPinFilePath { get; set; }

        [DataPinDefinition(
            Id = "488f578c-d77c-4bca-80e8-8c456208d8c8",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = "InPinDirectoryPath",
            DisplayName = "Directory Path")]
        public DataPin InPinDirectoryPath { get; set; }

        public override string FriendlyName
        {
            get
            {
                return nameof(GetDirectoryContentNode);
            }
        }

        public override string Name
        {
            get
            {
                return nameof(GetDirectoryContentNode);
            }
        }
    }
}
