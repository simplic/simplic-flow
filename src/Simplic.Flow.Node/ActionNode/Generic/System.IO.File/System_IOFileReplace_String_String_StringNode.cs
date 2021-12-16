// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileReplace_String_String_String), DisplayName = "Replace(String,String,String)", Category = "System/File")]
    public class System_IOFileReplace_String_String_String : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.File.Replace(
                scope.GetValue<System.String>(InPinSourceFileName),
                scope.GetValue<System.String>(InPinDestinationFileName),
                scope.GetValue<System.String>(InPinDestinationBackupFileName));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileReplace_String_String_String: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileReplace_String_String_String); 
        public override string FriendlyName => nameof(System_IOFileReplace_String_String_String); 

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
        Id = "a60ffc6d-c908-4a95-8bf7-b94e9c29ee91",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceFileName),
        DisplayName = "SourceFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceFileName { get; set; } 

        [DataPinDefinition(
        Id = "5c64b7f5-71e7-4de3-ac39-7eda74870f85",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationFileName),
        DisplayName = "DestinationFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationFileName { get; set; } 

        [DataPinDefinition(
        Id = "b065a8a0-2580-4e05-b4be-870b17bc41c1",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationBackupFileName),
        DisplayName = "DestinationBackupFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationBackupFileName { get; set; } 

    }
}