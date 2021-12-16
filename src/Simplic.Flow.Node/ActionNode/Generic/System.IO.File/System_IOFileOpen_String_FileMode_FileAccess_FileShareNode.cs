// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileOpen_String_FileMode_FileAccess_FileShare), DisplayName = "Open(String,FileMode,FileAccess,FileShare)", Category = "System/File")]
    public class System_IOFileOpen_String_FileMode_FileAccess_FileShare : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                var returnValue = System.IO.File.Open(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.IO.FileMode>(InPinMode),
                scope.GetValue<System.IO.FileAccess>(InPinAccess),
                scope.GetValue<System.IO.FileShare>(InPinShare));
                scope.SetValue(OutPinReturn, returnValue);

                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileOpen_String_FileMode_FileAccess_FileShare: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileOpen_String_FileMode_FileAccess_FileShare); 
        public override string FriendlyName => nameof(System_IOFileOpen_String_FileMode_FileAccess_FileShare); 

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
        Id = "24715f0c-86f9-40f3-9cb2-7e6fcd097008",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "fa7a6790-177e-4637-99b4-64875d87988c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileMode),
        Direction = PinDirection.In,
        Name = nameof(InPinMode),
        DisplayName = "Mode",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinMode { get; set; } 

        [DataPinDefinition(
        Id = "981a499e-b33a-4b44-b986-b08b770680cb",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileAccess),
        Direction = PinDirection.In,
        Name = nameof(InPinAccess),
        DisplayName = "Access",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinAccess { get; set; } 

        [DataPinDefinition(
        Id = "55906bfc-bfba-4833-bd18-9c81e7043c74",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileShare),
        Direction = PinDirection.In,
        Name = nameof(InPinShare),
        DisplayName = "Share",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinShare { get; set; } 

        [DataPinDefinition(
        Id = "a5c4b5f5-c357-46cb-8353-d433319933b1",
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