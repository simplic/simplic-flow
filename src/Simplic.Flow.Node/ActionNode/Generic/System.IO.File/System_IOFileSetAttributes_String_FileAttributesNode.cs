// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileSetAttributes_String_FileAttributes), DisplayName = "SetAttributes(String,FileAttributes)", Category = "System/File")]
    public class System_IOFileSetAttributes_String_FileAttributes : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.File.SetAttributes(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.IO.FileAttributes>(InPinFileAttributes));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileSetAttributes_String_FileAttributes: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileSetAttributes_String_FileAttributes); 
        public override string FriendlyName => nameof(System_IOFileSetAttributes_String_FileAttributes); 

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
        Id = "3375c268-4bf9-4871-a40a-a1fd38e6e5d9",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "edf138b7-115d-4daa-a731-af5f7b284693",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.IO.FileAttributes),
        Direction = PinDirection.In,
        Name = nameof(InPinFileAttributes),
        DisplayName = "FileAttributes",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinFileAttributes { get; set; } 

    }
}