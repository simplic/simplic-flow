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
        Id = "32c7c57a-ad26-4b43-8b19-30c3c4f44791",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceFileName),
        DisplayName = "SourceFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceFileName { get; set; } 

        [DataPinDefinition(
        Id = "b949ba5e-385f-455b-8e66-1e0c72052ad4",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationFileName),
        DisplayName = "DestinationFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationFileName { get; set; } 

        [DataPinDefinition(
        Id = "a609ddfa-df4a-427f-b47e-c95a4cb4c327",
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