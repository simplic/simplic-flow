// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileSetLastWriteTimeUtc_String_DateTime), DisplayName = "SetLastWriteTimeUtc(String,DateTime)", Category = "System/File")]
    public class System_IOFileSetLastWriteTimeUtc_String_DateTime : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.File.SetLastWriteTimeUtc(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.DateTime>(InPinLastWriteTimeUtc));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileSetLastWriteTimeUtc_String_DateTime: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileSetLastWriteTimeUtc_String_DateTime); 
        public override string FriendlyName => nameof(System_IOFileSetLastWriteTimeUtc_String_DateTime); 

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
        Id = "17a46a84-5475-46b4-8d22-7ae4d9dcbde9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "33eaf881-5ec2-4ed4-b4ae-aa77c85d40e4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.In,
        Name = nameof(InPinLastWriteTimeUtc),
        DisplayName = "LastWriteTimeUtc",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinLastWriteTimeUtc { get; set; } 

    }
}