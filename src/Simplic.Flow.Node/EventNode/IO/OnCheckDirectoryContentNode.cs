using System;
using System.IO;
using System.Linq;

namespace Simplic.Flow.Node.IO
{
    [EventNodeDefinition(DisplayName = "On Check Directory Content", Name = "OnCheckDirectoryContentNode", EventName = "OnCheckDirectoryContent", Category = "IO")]
    public class OnCheckDirectoryContentNode : EventNode
    {
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            var args = runtime.FlowEventArgs as OnCheckDirectoryContentEventArgs;

            if (args == null)
            {
                Console.WriteLine($"Arguments not found in {nameof(OnCheckDirectoryContentNode)}");
                return false;
            }
            if (string.IsNullOrWhiteSpace(args.DirectoryPath))
            {
                Console.WriteLine($"Path null or mpety in {nameof(OnCheckDirectoryContentNode)}");
                return false;
            }

            scope.SetValue(OutPinDirectoryPath, args.DirectoryPath);

            if (!Directory.Exists(args.DirectoryPath))
            {
                Console.WriteLine($"Directory not found `{args.DirectoryPath}` {nameof(OnCheckDirectoryContentNode)}");
                return false;
            }

            if (OutPinDirectoryPath != null && Directory.GetFiles(args.DirectoryPath).Any())
            {
                runtime.EnqueueNode(OutNode, scope);
            }

            return true;
        }

        [FlowPinDefinition(DisplayName = "Out", Name = "OutNode", PinDirection = PinDirection.Out)]
        public ActionNode OutNode { get; set; }

        [DataPinDefinition(
            Id = "f980c0e6-5dc3-4064-8db7-c95b08d90664",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.Out,
            Name = "OutPinDirectoryPath",
            DisplayName = "Pin Directory Path")]
        public DataPin OutPinDirectoryPath { get; set; }

        public override string EventName
        {
            get
            {
                return nameof(OnCheckDirectoryContentNode);
            }
        }

        public override string FriendlyName
        {
            get
            {
                return nameof(OnCheckDirectoryContentNode);
            }
        }

        public override string Name
        {
            get
            {
                return nameof(OnCheckDirectoryContentNode);
            }
        }
    }
}
