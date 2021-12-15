// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IODirectorySetCreationTimeUtc_String_DateTime), DisplayName = "SetCreationTimeUtc(String,DateTime)", Category = "System/Directory")]
    public class System_IODirectorySetCreationTimeUtc_String_DateTime : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.Directory.SetCreationTimeUtc(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.DateTime>(InPinCreationTimeUtc));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IODirectorySetCreationTimeUtc_String_DateTime: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IODirectorySetCreationTimeUtc_String_DateTime); 
        public override string FriendlyName => nameof(System_IODirectorySetCreationTimeUtc_String_DateTime); 

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
        Id = "66512b3b-02a4-47b0-92da-dbb21ff3a9ef",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "787fec6b-9a83-4e4d-8d3d-eac55bc94e3d",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.DateTime),
        Direction = PinDirection.In,
        Name = nameof(InPinCreationTimeUtc),
        DisplayName = "CreationTimeUtc",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinCreationTimeUtc { get; set; } 

    }
}