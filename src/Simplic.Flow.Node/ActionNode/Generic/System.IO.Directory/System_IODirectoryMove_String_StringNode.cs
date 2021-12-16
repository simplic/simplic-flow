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
        Id = "b55911fb-0a9b-4b7d-9454-5cc788661361",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceDirName),
        DisplayName = "SourceDirName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceDirName { get; set; } 

        [DataPinDefinition(
        Id = "5d78ac33-2321-4545-8f90-f66a1ab8a8f6",
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