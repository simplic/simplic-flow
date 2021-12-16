// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IODirectorySetAccessControl_String_DirectorySecurity), DisplayName = "SetAccessControl(String,DirectorySecurity)", Category = "System/Directory")]
    public class System_IODirectorySetAccessControl_String_DirectorySecurity : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.Directory.SetAccessControl(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.Security.AccessControl.DirectorySecurity>(InPinDirectorySecurity));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IODirectorySetAccessControl_String_DirectorySecurity: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IODirectorySetAccessControl_String_DirectorySecurity); 
        public override string FriendlyName => nameof(System_IODirectorySetAccessControl_String_DirectorySecurity); 

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
        Id = "67227858-301e-4e1a-b931-c4302407ae6c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "e40d99dc-5f0d-4e78-a7ae-99419732102c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Security.AccessControl.DirectorySecurity),
        Direction = PinDirection.In,
        Name = nameof(InPinDirectorySecurity),
        DisplayName = "DirectorySecurity",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDirectorySecurity { get; set; } 

    }
}