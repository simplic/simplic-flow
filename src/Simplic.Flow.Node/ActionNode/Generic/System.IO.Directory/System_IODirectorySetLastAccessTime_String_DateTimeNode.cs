// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IODirectorySetLastAccessTime_String_DateTime), DisplayName = "SetLastAccessTime(String,DateTime)", Category = "System/Directory")]
    public class System_IODirectorySetLastAccessTime_String_DateTime : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.Directory.SetLastAccessTime(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.DateTime>(InPinLastAccessTime));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IODirectorySetLastAccessTime_String_DateTime: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IODirectorySetLastAccessTime_String_DateTime); 
        public override string FriendlyName => nameof(System_IODirectorySetLastAccessTime_String_DateTime); 

        [FlowPinDefinition(
        PinDirection = PinDirection.Out, 
        DisplayName = "Success", 
        Name = nameof(OutNodeSuccess), 
        AllowMultiple = false)] 
        public ActionNode OutNodeSuccess { get; set; } 

        [FlowPinDefinition(
        PinDirection = PinDirection.Out, 
        DisplayName = "Failed", 
        Name = nameof(OutNodeFailed), 
        AllowMultiple = false)] 
        public ActionNode OutNodeFailed { get; set; } 

        [DataPinDefinition(
        Id = "0eca9638-1f57-4af5-b8d3-a2f031115bcc",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "6d571469-9ec2-4800-92a7-4631e25335ca",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.In,
        Name = nameof(InPinLastAccessTime),
        DisplayName = "LastAccessTime",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLastAccessTime { get; set; } 

    }
}