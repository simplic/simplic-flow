// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileCopy_String_String), DisplayName = "Copy(String,String)", Category = "System/File")]
    public class System_IOFileCopy_String_String : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.File.Copy(
                scope.GetValue<System.String>(InPinSourceFileName),
                scope.GetValue<System.String>(InPinDestFileName));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileCopy_String_String: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileCopy_String_String); 
        public override string FriendlyName => nameof(System_IOFileCopy_String_String); 

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
        Id = "ca147b7b-5db6-492b-a383-c6df557f296a",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceFileName),
        DisplayName = "SourceFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceFileName { get; set; } 

        [DataPinDefinition(
        Id = "09515abe-35b0-4c08-91bc-41313d2f767b",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinDestFileName),
        DisplayName = "DestFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestFileName { get; set; } 

    }
}