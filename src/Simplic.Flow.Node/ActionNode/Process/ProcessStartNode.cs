using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Simplic.Flow.Node
{
    [ActionNodeDefinition(DisplayName = "Process start", Name = "ProcessStartNode", Category = "Process")]
    public class ProcessStartNode : ActionNode
    {
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope)
        {
            try
            {
                var startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;
                startInfo.FileName = scope.GetValue<string>(InPinPath);
                startInfo.Arguments = scope.GetValue<string>(InPinArguments);
                startInfo.WorkingDirectory = scope.GetValue<string>(InPinWorkingDir) ?? Path.GetDirectoryName(startInfo.FileName);
                var process = new Process();

                process.StartInfo = startInfo;

                if (scope.GetValue<bool>(InPinWaitForExit))
                    process.WaitForExit();
                
                process.Start();

                if (OutSuccessNode != null)
                    runtime.EnqueueNode(OutSuccessNode, scope);
            }
            catch
            {
                if (OutFailedNode != null)
                    runtime.EnqueueNode(OutFailedNode, scope);
            }

            return true;
        }

        [FlowPinDefinition(DisplayName = "Success", Name = "OutNode", PinDirection = PinDirection.Out)]
        public ActionNode OutSuccessNode { get; set; }

        [FlowPinDefinition(DisplayName = "Failed", Name = "OutFailedNode", PinDirection = PinDirection.Out)]
        public ActionNode OutFailedNode { get; set; }

        [DataPinDefinition(
            Id = "0077e333-693f-41f2-9d76-989b71687c27",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = "InPinPath",
            DisplayName = "Path (exe)")]
        public DataPin InPinPath { get; set; }

        [DataPinDefinition(
            Id = "9e9f85a3-db83-49d3-b0db-1f61f043bc4c",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = "InPinArguments",
            DisplayName = "Arguments")]
        public DataPin InPinArguments { get; set; }

        [DataPinDefinition(
            Id = "ab738863-164e-4f91-be2b-cd87062bd2d0",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(string),
            Direction = PinDirection.In,
            Name = "InPinWorkingDir",
            DisplayName = "Working directory")]
        public DataPin InPinWorkingDir { get; set; }

        [DataPinDefinition(
            Id = "f53eee5d-68f9-4811-80ec-e8ebbb0cc061",
            ContainerType = DataPinContainerType.Single,
            DataType = typeof(bool),
            Direction = PinDirection.In,
            Name = "InPinWaitForExit",
            DefaultValue = true,
            DisplayName = "Wait for exit")]
        public DataPin InPinWaitForExit { get; set; }

        public override string FriendlyName { get { return nameof(ProcessStartNode); } }
        public override string Name { get { return nameof(ProcessStartNode); } }
    }
}
