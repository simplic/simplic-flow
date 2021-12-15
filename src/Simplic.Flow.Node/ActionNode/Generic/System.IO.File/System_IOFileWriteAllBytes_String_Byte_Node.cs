// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileWriteAllBytes_String_Byte_), DisplayName = "WriteAllBytes(String,Byte[])", Category = "System/File")]
    public class System_IOFileWriteAllBytes_String_Byte_ : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.File.WriteAllBytes(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.Byte[]>(InPinBytes));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileWriteAllBytes_String_Byte_: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileWriteAllBytes_String_Byte_); 
        public override string FriendlyName => nameof(System_IOFileWriteAllBytes_String_Byte_); 

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
        Id = "d14a9b04-db74-4818-a24b-6e5c0dd4cb51",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "21f0f936-9d46-4fb6-8392-6a594829468a",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Byte[]),
        Direction = PinDirection.In,
        Name = nameof(InPinBytes),
        DisplayName = "Bytes",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinBytes { get; set; } 

    }
}