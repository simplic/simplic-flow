// This file has been generated using the Simplic.Flow.NodeGenerator
using System; 
using Simplic.Flow; 

namespace Simplic.Flow.Node
{  
    [ActionNodeDefinition(Name = nameof(System_IOFileWriteAllText_String_String_Encoding), DisplayName = "WriteAllText(String,String,Encoding)", Category = "System/File")]
    public class System_IOFileWriteAllText_String_String_Encoding : ActionNode 
    { 
        public override bool Execute(IFlowRuntimeService runtime, DataPinScope scope) 
        { 
            try
            {
                System.IO.File.WriteAllText(
                scope.GetValue<System.String>(InPinPath),
                scope.GetValue<System.String>(InPinContents),
                scope.GetValue<System.Text.Encoding>(InPinEncoding));
                if (OutNodeSuccess != null) 
                {
                    runtime.EnqueueNode(OutNodeSuccess, scope);
                }
            }
            catch (Exception ex) 
            {
                Simplic.Log.LogManagerInstance.Instance.Error("Error in System_IOFileWriteAllText_String_String_Encoding: ", ex);
                if (OutNodeFailed != null)
                    runtime.EnqueueNode(OutNodeFailed, scope);
            }
            return true; 
        }  

        public override string Name => nameof(System_IOFileWriteAllText_String_String_Encoding); 
        public override string FriendlyName => nameof(System_IOFileWriteAllText_String_String_Encoding); 

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
        Id = "6af3adc9-94e1-4a7d-9669-fbe57550ca76",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinPath),
        DisplayName = "Path",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinPath { get; set; } 

        [DataPinDefinition(
        Id = "43502a88-9506-4927-bf4c-62a4fa6caf42",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.String),
        Direction = PinDirection.In,
        Name = nameof(InPinContents),
        DisplayName = "Contents",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinContents { get; set; } 

        [DataPinDefinition(
        Id = "cf338472-ed81-4a02-bb31-511802505a10",
        ContainerType = DataPinContainerType.Single,
        DataType = typeof(System.Text.Encoding),
        Direction = PinDirection.In,
        Name = nameof(InPinEncoding),
        DisplayName = "Encoding",
        IsGeneric = false,
        AllowedTypes = null)]
        public DataPin InPinEncoding { get; set; } 

    }
}