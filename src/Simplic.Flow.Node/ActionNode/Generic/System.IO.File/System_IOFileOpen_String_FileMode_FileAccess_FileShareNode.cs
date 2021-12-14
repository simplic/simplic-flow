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
        Id = "26a8316c-69df-4100-befa-e5a0abc0d081",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "6b0d48eb-f70e-4754-8cbb-a9b627b8b31e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileMode),
        Direction = PinDirection.In,
        Name = nameof(InPinMode),
        DisplayName = "Mode",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinMode { get; set; } 

        [DataPinDefinition(
        Id = "c4cd08e2-04d3-4e0d-a642-decefa88f565",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileAccess),
        Direction = PinDirection.In,
        Name = nameof(InPinAccess),
        DisplayName = "Access",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinAccess { get; set; } 

        [DataPinDefinition(
        Id = "bfb91f43-1f28-46ba-86e5-a2e79a8d6d7b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileShare),
        Direction = PinDirection.In,
        Name = nameof(InPinShare),
        DisplayName = "Share",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinShare { get; set; } 

        [DataPinDefinition(
        Id = "6d198709-5c90-45a8-b06a-322cd8eb5734",
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