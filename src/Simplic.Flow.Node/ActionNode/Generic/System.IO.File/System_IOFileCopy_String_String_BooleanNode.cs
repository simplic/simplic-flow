// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileCopy_String_String_Boolean), DisplayName = "Copy(String,String,Boolean)", Category = "System/File")]
    public class System_IOFileCopy_String_String_Boolean : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.File.Copy(
                scope.GetValue<System.String>(InPinSourceFileName),
                scope.GetValue<System.String>(InPinDestFileName),
                scope.GetValue<System.Boolean>(InPinOverwrite));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileCopy_String_String_Boolean: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileCopy_String_String_Boolean); 
        public override string FriendlyName => nameof(System_IOFileCopy_String_String_Boolean); 

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
        Id = "4500e02e-a531-4994-88ff-90142c6f002a",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinSourceFileName),
        DisplayName = "SourceFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinSourceFileName { get; set; } 

        [DataPinDefinition(
        Id = "c64e3abd-b5bb-4e72-b150-67b62cead53e",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinDestFileName),
        DisplayName = "DestFileName",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinDestFileName { get; set; } 

        [DataPinDefinition(
        Id = "6396781b-8e4c-41b9-9dc3-9a85153b895f",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Boolean),
        Direction = PinDirection.In,
        Name = nameof(InPinOverwrite),
        DisplayName = "Overwrite",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinOverwrite { get; set; } 

    }
}