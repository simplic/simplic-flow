// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IODirectorySetLastAccessTimeUtc_String_DateTime), DisplayName = "SetLastAccessTimeUtc(String,DateTime)", Category = "System/Directory")]
    public class System_IODirectorySetLastAccessTimeUtc_String_DateTime : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.Directory.SetLastAccessTimeUtc(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.DateTime>(InPinLastAccessTimeUtc));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IODirectorySetLastAccessTimeUtc_String_DateTime: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IODirectorySetLastAccessTimeUtc_String_DateTime); 
        public override string FriendlyName => nameof(System_IODirectorySetLastAccessTimeUtc_String_DateTime); 

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
        Id = "dd233664-e2d8-471e-b065-c9711d186da0",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "520eb3e4-12d8-4588-a36d-f84f1b892be2",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.In,
        Name = nameof(InPinLastAccessTimeUtc),
        DisplayName = "LastAccessTimeUtc",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLastAccessTimeUtc { get; set; } 

    }
}