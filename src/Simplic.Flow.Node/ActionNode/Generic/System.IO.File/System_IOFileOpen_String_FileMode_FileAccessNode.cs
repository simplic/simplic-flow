// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileOpen_String_FileMode_FileAccess), DisplayName = "Open(String,FileMode,FileAccess)", Category = "System/File")]
    public class System_IOFileOpen_String_FileMode_FileAccess : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.File.Open(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.IO.FileMode>(InPinMode),
                scope.GetValue<System.IO.FileAccess>(InPinAccess));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileOpen_String_FileMode_FileAccess: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileOpen_String_FileMode_FileAccess); 
        public override string FriendlyName => nameof(System_IOFileOpen_String_FileMode_FileAccess); 

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
        Id = "8320a82c-906a-4c59-8e1c-aac5b7e1c0e4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "c04f3a37-40a9-4cf6-922f-90e922926677",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileMode),
        Direction = PinDirection.In,
        Name = nameof(InPinMode),
        DisplayName = "Mode",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinMode { get; set; } 

        [DataPinDefinition(
        Id = "d8bc324d-9a51-487d-841d-1be707461918",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileAccess),
        Direction = PinDirection.In,
        Name = nameof(InPinAccess),
        DisplayName = "Access",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinAccess { get; set; } 

        [DataPinDefinition(
        Id = "ce9d8ca8-cfcd-45ab-926a-5cd87269f0c8",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileStream),
        Direction = PinDirection.Out,
        Name = nameof(OutPinReturn),
        DisplayName = "Return",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin OutPinReturn { get; set; } 

    }
}