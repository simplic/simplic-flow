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
        Id = "8823159e-a349-4229-8368-9cb109e93296",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceFileName),
        DisplayName = "SourceFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceFileName { get; set; } 

        [DataPinDefinition(
        Id = "78122720-e78f-4907-b479-ce06f04f7030",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationFileName),
        DisplayName = "DestinationFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationFileName { get; set; } 

        [DataPinDefinition(
        Id = "d59c56e3-3a90-4a12-99a2-0cf6c0dc0d83",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinDestinationBackupFileName),
        DisplayName = "DestinationBackupFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestinationBackupFileName { get; set; } 

        [DataPinDefinition(
        Id = "6927ed59-0b52-4478-ac41-28a141626550",
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