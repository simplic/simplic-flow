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
        Id = "0d9060f8-363d-4046-b715-e0443156d779",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "bf7f2afa-1990-45f0-b5d7-8c18d9b6210f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileMode),
        Direction = PinDirection.In,
        Name = nameof(InPinMode),
        DisplayName = "Mode",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinMode { get; set; } 

        [DataPinDefinition(
        Id = "c274df44-ab29-4e3b-80d3-a4a1a6205415",
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