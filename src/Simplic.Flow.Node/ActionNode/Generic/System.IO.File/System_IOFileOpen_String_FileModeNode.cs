// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileOpen_String_FileMode), DisplayName = "Open(String,FileMode)", Category = "System/File")]
    public class System_IOFileOpen_String_FileMode : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.File.Open(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.IO.FileMode>(InPinMode));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileOpen_String_FileMode: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileOpen_String_FileMode); 
        public override string FriendlyName => nameof(System_IOFileOpen_String_FileMode); 

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
        Id = "37ccdd6c-036b-4d80-bc96-80217ac01abf",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "ca422ba7-1735-4c07-a491-54a5703ffa52",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileMode),
        Direction = PinDirection.In,
        Name = nameof(InPinMode),
        DisplayName = "Mode",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinMode { get; set; } 

        [DataPinDefinition(
        Id = "a9c0384e-c4a1-499d-bcc1-56ca64d860dd",
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