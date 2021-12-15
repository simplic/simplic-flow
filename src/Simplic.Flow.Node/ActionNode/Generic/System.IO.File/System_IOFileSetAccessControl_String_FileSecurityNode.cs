// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileSetAccessControl_String_FileSecurity), DisplayName = "SetAccessControl(String,FileSecurity)", Category = "System/File")]
    public class System_IOFileSetAccessControl_String_FileSecurity : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.File.SetAccessControl(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.Security.AccessControl.FileSecurity>(InPinFileSecurity));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileSetAccessControl_String_FileSecurity: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileSetAccessControl_String_FileSecurity); 
        public override string FriendlyName => nameof(System_IOFileSetAccessControl_String_FileSecurity); 

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
        Id = "476d74e1-17e3-431d-a8f4-1494423d46d0",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "7a6ef333-e4be-4935-98fb-12707b9c94b4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Security.AccessControl.FileSecurity),
        Direction = PinDirection.In,
        Name = nameof(InPinFileSecurity),
        DisplayName = "FileSecurity",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFileSecurity { get; set; } 

    }
}