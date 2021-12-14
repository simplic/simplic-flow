// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileReplace_String_String_String_Boolean), DisplayName = "Replace(String,String,String,Boolean)", Category = "System/File")]
    public class System_IOFileReplace_String_String_String_Boolean : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.File.Replace(
                scope.GetValue<System.String>(InPinSourceFileName),
                scope.GetValue<System.String>(InPinDestinationFileName),
                scope.GetValue<System.String>(InPinDestinationBackupFileName),
                scope.GetValue<System.Boolean>(InPinIgnoreMetadataErrors));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileReplace_String_String_String_Boolean: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileReplace_String_String_String_Boolean); 
        public override string FriendlyName => nameof(System_IOFileReplace_String_String_String_Boolean); 

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
        Id = "bdb9c2e2-ede7-4c8d-8b55-8dd38c10fc16",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceFileName),
        DisplayName = "SourceFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceFileName { get; set; } 

        [DataPinDefinition(
        Id = "ee75cc4b-94cc-42d7-afbb-3d82a8d38c37",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationFileName),
        DisplayName = "DestinationFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationFileName { get; set; } 

        [DataPinDefinition(
        Id = "edc00827-22c2-4b30-b03f-a89d88d57fbb",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationBackupFileName),
        DisplayName = "DestinationBackupFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationBackupFileName { get; set; } 

        [DataPinDefinition(
        Id = "9f513c1d-aac2-4ae7-ad84-2b2d2a053c3c",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.In,
        Name = nameof(InPinIgnoreMetadataErrors),
        DisplayName = "IgnoreMetadataErrors",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinIgnoreMetadataErrors { get; set; } 

    }
}