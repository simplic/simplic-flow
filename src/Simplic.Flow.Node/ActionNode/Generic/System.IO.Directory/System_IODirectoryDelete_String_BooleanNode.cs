// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IODirectoryDelete_String_Boolean), DisplayName = "Delete(String,Boolean)", Category = "System/Directory")]
    public class System_IODirectoryDelete_String_Boolean : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.Directory.Delete(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.Boolean>(InPinRecursive));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IODirectoryDelete_String_Boolean: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IODirectoryDelete_String_Boolean); 
        public override string FriendlyName => nameof(System_IODirectoryDelete_String_Boolean); 

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
        Id = "0befe60e-f76f-4441-93fb-cd00521f84ad",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "976dda05-827b-4c0c-a0e1-24df784b9174",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.In,
        Name = nameof(InPinRecursive),
        DisplayName = "Recursive",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinRecursive { get; set; } 

    }
}