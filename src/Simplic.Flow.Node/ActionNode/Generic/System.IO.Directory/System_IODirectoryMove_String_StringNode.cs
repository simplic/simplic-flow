// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IODirectoryMove_String_String), DisplayName = "Move(String,String)", Category = "System/Directory")]
    public class System_IODirectoryMove_String_String : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.Directory.Move(
                scope.GetValue<System.String>(InPinSourceDirName),
                scope.GetValue<System.String>(InPinDestDirName));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IODirectoryMove_String_String: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IODirectoryMove_String_String); 
        public override string FriendlyName => nameof(System_IODirectoryMove_String_String); 

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
        Id = "59af963f-4f15-4d26-9314-28943ce9cdae",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceDirName),
        DisplayName = "SourceDirName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceDirName { get; set; } 

        [DataPinDefinition(
        Id = "739248c0-b5cf-42de-bf18-1eb807334e6e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinDestDirName),
        DisplayName = "DestDirName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestDirName { get; set; } 

    }
}