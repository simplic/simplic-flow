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
        Id = "9eb84413-a271-46dd-8083-12e886fb16a1",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "dae668a3-24e7-4a6f-bf43-b88d480b926b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileMode),
        Direction = PinDirection.In,
        Name = nameof(InPinMode),
        DisplayName = "Mode",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinMode { get; set; } 

        [DataPinDefinition(
        Id = "1de84f2d-fdc9-4c21-897b-06ac06fb3387",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileAccess),
        Direction = PinDirection.In,
        Name = nameof(InPinAccess),
        DisplayName = "Access",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinAccess { get; set; } 

        [DataPinDefinition(
        Id = "21bd4c41-3a3c-4762-9ffd-7f0e91147143",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileShare),
        Direction = PinDirection.In,
        Name = nameof(InPinShare),
        DisplayName = "Share",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinShare { get; set; } 

        [DataPinDefinition(
        Id = "ca875473-b10e-4773-a762-3c2b91466148",
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